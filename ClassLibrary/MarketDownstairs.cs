using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;
using System.Data;
using Lucene.Net.Store;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.PanGu;


namespace ClassLibrary
{
    public class MarketDownstairs
    {

        public ViewModel.MyMallInfo GetMyMallInfo(string ownerName)
        {
            ViewModel.MyMallInfo myMallInfo = new ViewModel.MyMallInfo();
            var t006 = operateContext.BLLSession.IT006店铺信息表BLL.GetListBy(m => m.Owners == ownerName)[0];
            myMallInfo.MallName = t006.Name;
            myMallInfo.Owner = t006.Owners;
            myMallInfo.PhoneNumber = t006.Contaction;
            myMallInfo.QQ = t006.QQ;
            myMallInfo.Introduction = t006.Introduction;
            myMallInfo.WeChat = t006.WeChat;
            myMallInfo.MallId = t006.MallID;
            return myMallInfo;
        }

        /// <summary>
        /// 修改个人商店信息 
        /// </summary>
        /// <param name="myMallInfo"></param>
        /// <returns></returns>
        public bool UpdateMyMallInfo(ViewModel.MyMallInfo myMallInfo)
        {
            
           //T006店铺信息表 t006 = new T006店铺信息表();
            var t006 = operateContext.BLLSession.IT006店铺信息表BLL.GetListBy(m => m.MallID == myMallInfo.MallId).First();
            if (t006 != null)
            {
                t006.MallID = myMallInfo.MallId;
                t006.Name = myMallInfo.MallName;
                t006.Introduction = myMallInfo.Introduction;
                t006.QQ = myMallInfo.QQ;
                t006.WeChat = myMallInfo.WeChat;
                t006.Contaction = myMallInfo.PhoneNumber;
                string[] allString = { "Introduction", "QQ", "WeChat", "Contaction", "Name" };
                var t = operateContext.BLLSession.IT006店铺信息表BLL.Modify(t006, allString);
                return true;

            }
            else
            {
                return false;
            }
           
            

        }

        /// <summary>
        /// 获取所有商品信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T007店铺货物表> getAllGoods(int page,int pageSize )
        {
          
            return operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodID != null).Skip((page-1)*pageSize).Take(pageSize).ToList();
 
        }
        /// <summary>
        /// 获取所有商品数量
        /// </summary>
        /// <returns></returns>
        public int getTotalCount()
        {
            return operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodID != null).Count();
        }
        /// <summary>
        /// 获取某个商品具体信息
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public T007店铺货物表 GetGoodDetail(int goodId)
        {

            return operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodID == goodId).Single();
 
        }
        /// <summary>
        /// 在获取商品具体信息后获取对应商店的信息
        /// </summary>
        /// <returns></returns>
        public T006店铺信息表 GetGoodMall(int goodId)
        {
            int shopId = operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodID == goodId).Single().ShopID;

            return operateContext.BLLSession.IT006店铺信息表BLL.GetListBy(m => m.MallID == shopId).FirstOrDefault();
 
        }
        /// <summary>
        /// 根据票据中的用户获取当前的商店ID
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public int GetNowMallID(string userEmail)
        {
            return operateContext.BLLSession.IT006店铺信息表BLL.GetListBy(m => m.Owners == userEmail).FirstOrDefault().MallID;

        }

        /// <summary>
        ///上传货物信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="imageAdress"></param>
        /// <returns></returns>
        public bool UpLoadGoodInfo(ViewModel.UpLoadGoodModel model ,string imageAdress)
        {
            try
            {
                T007店铺货物表 newGood = new T007店铺货物表();
                newGood.GoodIntroduction = model.Introduction;
                newGood.GoodName = model.GoodName;
                newGood.GoodPhoto = imageAdress;
                newGood.GoodPrice = model.GoodPrice;
                newGood.ShopID = model.shopId;
                newGood.GoodNumber = model.GoodNumber;
                //将记录加入数据库
                operateContext.BLLSession.IT007店铺货物表BLL.Add(newGood);

                ///下面是创建索引的代码。

                JobInfo jobinfo = new JobInfo { GoodId = newGood.GoodID, JobType = JobType.Add };
                IndexManager.Instance.PutJob(jobinfo);//把索引任务放入队列。
                return true;

            }
            catch (Exception)
            {
                
                throw;
            }
           
            

        }
        /// <summary>
        /// 获取搜索到的商品数据
        /// </summary>
        /// <param name="keyWords"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public GoodsListModel getRightGoods(string keyWords, int pageSize,int page)
        {
            ///从词库中获得分词
          List<string> kw= splitWordsHelper.SplitWords(keyWords)as List<string>;
          FSDirectory directory = FSDirectory.Open(new DirectoryInfo(@"G:\index"),
              new NoLockFactory());
          IndexReader reader = IndexReader.Open(directory, true);
          IndexSearcher searcher = new IndexSearcher(reader);

          PhraseQuery queryMsg = new PhraseQuery();//查询条件    
          foreach (string word in kw)
          {
              queryMsg.Add(new Term("GoodName", word));//Contains("body",word)
          }
          queryMsg.SetSlop(100);//词的距离超过100就不匹配

          PhraseQuery queryTitle = new PhraseQuery();//查询条件    
          foreach (string word in kw)
          {
              queryTitle.Add(new Term("GoodIntroduction", word));//Contains("body",word)
          }
          queryTitle.SetSlop(100);//词的距离超过100就不匹配

          //复合查询，在标题和正文中搜索
          //Should是Or，Must是And
          BooleanQuery boolQuery = new BooleanQuery();
          boolQuery.Add(queryMsg, BooleanClause.Occur.SHOULD);
          boolQuery.Add(queryTitle, BooleanClause.Occur.SHOULD);

          //Stopwatch stopwatch = new Stopwatch();
          //stopwatch.Start();

          //foreach (string word in kw.Split(' '))//先用空格，让用户去分词，空格分隔的就是词“计算机   专业”
          //{
          //    query.Add(new Term("msg", word));//Contains("body",word)
          //}
          //BooleanQuery
          //where Contains("body","计算机") and Contains("body","专业")


          TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);//盛放搜索结果的容器
          searcher.Search(boolQuery, null, collector);//用query这个查询条件进行搜索，搜索结果放入collector容器中

          List<T007店铺货物表> list = new List<T007店铺货物表>();

          // collector.GetTotalHits()查询结果的总条数
          ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;
          for (int i = 0; i < docs.Length; i++)
          {
              int docId = docs[i].doc;//文档编号（lucene.net内部分配的，和number无关）
              Document doc = searcher.Doc(docId);//根据文档编号拿到文档对象
              int id = Convert.ToInt32(doc.Get("GoodID"));//取出文档的number字段的值。必须是Field.Store.YES才能取出来
              string goodIntroduction = doc.Get("GoodIntroduction");
              string goodName = doc.Get("GoodName");
              int goodNumber=Convert.ToInt32( doc.Get("GoodNumber"));
              string goodPhotoUrl=doc.Get("GoodPhoto");
              decimal goodPrice=Convert.ToDecimal( doc.Get("GoodPrice"));
              int  shopid=Convert.ToInt32( doc.Get("ShopID"));
              

              T007店铺货物表 good = new T007店铺货物表();

              good.GoodID=id;
              good.GoodName=goodName;
              good.GoodIntroduction=goodIntroduction;
              good.GoodNumber=goodNumber;
              good.GoodPhoto=goodPhotoUrl;
              good.GoodPrice=goodPrice;
              good.ShopID=shopid;

              //sr.Id = id;
              //sr.Msg = HighLight(kw, msg);
              //sr.Title = title;
              //sr.Url = url;

              list.Add(good);
          }
         
           GoodsListModel goodlist=new GoodsListModel()
           {
                goodsList=list,
                 pageInfo=new PagingInfo{ currentpage=page, itemperpage=pageSize, Totalitems=list.Count()}
           }; 
            
            return goodlist;
         // return  operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodName==keyWords).Skip((page-1)*pageSize).Take(pageSize).ToList();

         
 
        }
        /// <summary>
        /// 依据关键字获得货物总数量
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int getRightCount(string keyword)
        {
            return operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodName==keyword).Count();

        }

        /// <summary>
        /// 删除对应的货物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteGoods(int id)
        {
            var t007 = operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.GoodID==id).FirstOrDefault();
            if (operateContext.BLLSession.IT007店铺货物表BLL.Del(t007) == 1)
            {
                ///下面是创建索引的代码。
                JobInfo jobinfo = new JobInfo {  GoodId = t007.GoodID, JobType = JobType.Delete };
                IndexManager.Instance.PutJob(jobinfo);//把索引任务放入队列。

                return true;
            }
            else
            {
                return false;
            }

 
        }

        /// <summary>
        /// 返回我的商品信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ViewModel.MyGoodModel getMyMallGoods(string email)
        {
            int shopId=operateContext.BLLSession.IT006店铺信息表BLL.GetListBy(m=>m.Owners==email).FirstOrDefault().MallID;
            MyGoodModel myGoodModel = new MyGoodModel();
            myGoodModel.MyMallGood=  operateContext.BLLSession.IT007店铺货物表BLL.GetListBy(m => m.ShopID == shopId);
            return myGoodModel;
 
        }
        

       
        
    }
}

