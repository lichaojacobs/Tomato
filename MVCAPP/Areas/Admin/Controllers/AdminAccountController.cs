using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.UserInformation;
using System.Web.Security;

namespace MVCAPP.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
 
        }
    }
}