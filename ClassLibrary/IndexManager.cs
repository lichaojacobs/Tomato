using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Lucene.Net.Store;
using Lucene.Net.Index;
using System.IO;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using ViewModel;
using Model;
using ClassLibrary;
using log4net;

namespace ClassLibrary
{
 

        public class IndexManager
        {


             private static ILog logger = LogManager.GetLogger(typeof(IndexManager));

            //盛放任务的队列
            private Queue<JobInfo> queue = new Queue<JobInfo>();


            private static IndexManager instance = new IndexManager();
            public static IndexManager Instance
            {
                get
                {
                    return instance;
                }
            }


            private IndexManager()
            {
            }

            public void PutJob(JobInfo jobInfo)
            {
                queue.Enqueue(jobInfo);
            }

            public void Start()
            {
                Thread thread = new Thread(ThreadStart);
                thread.IsBackground = true;
                thread.Start();
            }

            private void ThreadStart()
            {
                while (true)
                {
                    if (queue.Count <= 0)
                    {
                        logger.Debug("队列中没有任务，休息一会！ ");
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        //打开索引库，把所有的queue中的Job写入索引库，关闭
                        ProessQueue();
                    }
                }
            }

            private void ProessQueue()
            {
                string indexPath = @"G:/index";//注意和磁盘上文件夹的大小写一致，否则会报错。
                FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
                bool isUpdate = IndexReader.IndexExists(directory);
                logger.Debug("打开索引库：" + indexPath);
                if (isUpdate)
                {
                    //暂时规定：同时只能有一段代码操作索引库
                    //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁
                    if (IndexWriter.IsLocked(directory))
                    {
                         logger.Debug("索引库被锁定，进行解锁");
                        IndexWriter.Unlock(directory);
                    }
                }
                //IndexWriter负责把数据向索引库中写入
                IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
                while (queue.Count > 0)
                {
                    JobInfo jobInfo = queue.Dequeue();
                    if (jobInfo.JobType == JobType.Add)
                    {
                        
                        Model.T007店铺货物表 good = operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodID == jobInfo.GoodId).FirstOrDefault();
                        Document document = new Document();//文档对象。相当于表的一行记录
                        document.Add(new Field("GoodID", good.GoodID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                        document.Add(new Field("GoodIntroduction", good.GoodIntroduction, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("GoodName", good.GoodName, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("GoodNumber", good.GoodNumber.ToString(), Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("GoodPhoto", good.GoodPhoto, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("GoodPrice", good.GoodPrice.ToString(), Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("ShopID", good.ShopID.ToString(), Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        writer.AddDocument(document);
                    }
                    else if (jobInfo.JobType == JobType.Edit)
                    {
                        Model.T007店铺货物表 good = operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodID == jobInfo.GoodId).FirstOrDefault();
                        Document document = new Document();//文档对象。相当于表的一行记录
                        document.Add(new Field("GoodID", good.GoodID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                        document.Add(new Field("GoodIntroduction", good.GoodIntroduction, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("GoodName", good.GoodName, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("GoodNumber", good.GoodNumber.ToString(), Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("GoodPhoto", good.GoodPhoto, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("GoodPrice", good.GoodPrice.ToString(), Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        document.Add(new Field("ShopID", good.ShopID.ToString(), Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        writer.UpdateDocument(new Term("GoodID", good.GoodID.ToString()), document);
                        //update index set .... where id=art.id

                    }
                    else if (jobInfo.JobType == JobType.Delete)
                    {
                         logger.Debug("删除文章的任务，Id=" + jobInfo.GoodId);
                         writer.DeleteDocuments(new Term("GoodID", jobInfo.GoodId.ToString()));
                    }
                }
                writer.Optimize();
                writer.Close();
                directory.Close();//不要忘了Close，否则索引结果搜不到
            }
        }

        //对索引任务进行描述
        public class JobInfo
        {
            public int GoodId { get; set; }
            public JobType JobType { get; set; }//操作类型
        }

        public enum JobType
        {
            Add, Edit, Delete
        }
        

    }

