using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
   public class splitWordsHelper
    {
        /// <summary>
        /// 把输入的msg进行分词
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitWords(string msg)
        {
            List<string> list = new List<string>();

            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("",
                new StringReader(msg));
            Lucene.Net.Analysis.Token token = null;
            //Next()取分到的下一个词
            while ((token = tokenStream.Next()) != null)
            {
                string word = token.TermText();//分到的词
                list.Add(word);
            }
            return list;
        }

    }
}
