
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DI;




namespace MVCAPP.Controllers
{
    
    public class HomeController : Controller
    {
        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //@ViewBag.name = User.Identity.Name;
            return View();
        }
        
        
    }

}