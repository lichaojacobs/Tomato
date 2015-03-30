using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ViewModel;
namespace MVCAPP.Helper
{
    public static class PagingHelpers
    {
        public static MvcHtmlString pagelinks(this HtmlHelper html, ViewModel.PagingInfo  paginginfo, Func<int, string> pageurl)
        {
           
         StringBuilder result=new StringBuilder();
            for(int i=1;i<=paginginfo.totalpages;i++)
            {
               
                TagBuilder tag=new TagBuilder("a");//构造一个<a>标签
                tag.MergeAttribute("href",pageurl(i));
                tag.InnerHtml=i.ToString();
                if(i==paginginfo.currentpage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

      
    }
}