using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAPP.Handers
{
    /// <summary>
    /// robTicket 的摘要说明
    /// </summary>
    public class robTicket : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("想抢票？敬请期待！");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}