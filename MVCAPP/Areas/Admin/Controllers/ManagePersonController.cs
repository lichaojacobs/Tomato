using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace MVCAPP.Areas.Admin.Controllers
{
    public class ManagePersonController : Controller
    {
        // GET: Admin/ManagePerson
        
        int pageSize = 10;////初始化页面个数
        [Authorize(Roles = "管理员")]
        public ActionResult Index(int page=1)
        {
            UserInfoList userinfo = new UserInfoList 
            {
                userinfoList = new ClassLibrary.ManagePerson().getALLNormalUser(),
                pageinfo = new ViewModel.PagingInfo { currentpage = page, itemperpage = pageSize, Totalitems = new ClassLibrary.ManagePerson().getUserCount() }

            
            };
            return View(userinfo);

        }
        [HttpPost]
        public ActionResult Index(UserInfo user)
        {

            return View();
 
        }

        /// <summary>
        /// 封锁与激活用户权限
        /// </summary>
        /// <param name="emailFromView"></param>
        /// <returns></returns>
        [Authorize(Roles = "管理员")]
        public ActionResult BlockUser(string emailFromView)
        {
            string email = Server.HtmlDecode(emailFromView);
            //string email = Request.Form["Block"];
            if (new ClassLibrary.ManagePerson().blockUser(email))
            {

                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index");
                
            }

 
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="emailFromView"></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "管理员")]
        public ActionResult DeleteUser(string emailFromView)
        {
            string email = Server.HtmlDecode(emailFromView);
            if (new ClassLibrary.ManagePerson().deleteUser(email))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
 
        }

        
    }
}