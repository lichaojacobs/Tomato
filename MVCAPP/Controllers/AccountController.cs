using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using ClassLibrary.UserInformation;
using System.Web.Security;

namespace MVCAPP.Controllers
{
    public class AccountController : BaseController
    {

       
        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                IUserInformation user = new UserInformation();
                // Session["code"] = registerModel.ConfirmCode;
                if (user.CheckEmailUnique(registerModel.email))
                {
                    if (user.Register(registerModel))
                    {
                        return Content("<script type='text/JavaScript'>alert('注册成功！请前往邮箱激活');window.location='/Account/Reviselogin'</script>");
                        // return RedirectToAction("Login");
                    }
                    else
                    {
                        return Content("<script type='text/JavaScript'>alert('输入的邮箱为无效邮箱！！');window.location='/Account/Reviselogin'</script>");
 
                    }
                }
                else
                {
                    Response.Write("<script type='text/JavaScript'>alert('该用户名已经被注册过了！')</script>");
                }




            }

            return View("Reviselogin");

        }

        /// <summary>
        /// 接受激活验证消息
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveEmail(string email,string code)
        {
            IUserInformation user =new  UserInformation();
            if (user.CheckToConfirmUser(email, code))
            {
                return View();
 
            }
            else
            {
                return RedirectToAction("ConfirmError");
            }
            
            

        }
        /// <summary>
        /// 接收验证消息出现参数错误时返回的错误页面
        /// </summary>
        /// <returns></returns>

        public ActionResult ConfirmError()
        {

            return View();
 
        }


        /// <summary>
        /// 忘记密码进入的也页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPasswordBack()
        {
            return View();

        }

        [HttpPost]
        public ActionResult GetPasswordBack(string email)
        {
            email =Server.HtmlDecode( Request.Form["email"]);
            IUserInformation user = new UserInformation();
            if (user.SendResetPasswordEmail(email))
            {
                return Content("<script type='text/javascript'>var truth=window.confirm('发送成功，请前往邮箱确认修改！');if(truth){window.location='/Account/Reviselogin'}</script>");
            }
            else
            {
                return Content("<script type='text/javascript'>var truth=window.confirm('发送失败，请重试！');if(truth){window.location='/Account/GetPasswordBack'}</script>");
            }


 
        }


        /// <summary>
        /// 接受重置验证消息
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult ReceiveResetMsg(string email,string code)
        {
             IUserInformation user =new  UserInformation();
            if (user.CheckToResetPassword(email, code))
            {
                return View();
 
            }
            else
            {
                return RedirectToAction("ResetError");
            }
 
        }


        public ActionResult ResetError()
        {
            return View();
        }

        /// <summary>
        /// 普通用户与管理员登陆的统一路径
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            
            if (ModelState.IsValid)
            {
                
                IUserInformation user = new UserInformation();
                
                int state = user.LogIn(loginModel.UserName, loginModel.Password);
                if (state == 1)
                {

                    FormsAuthentication.SetAuthCookie(loginModel.UserName, true);
                    return RedirectToAction("Index", "Home");

                }
                else
                    if (state == 2)
                    {
                        FormsAuthentication.SetAuthCookie(loginModel.UserName, true);
                        return RedirectToAction("Index", "AdminHome", new { area = "Admin" });

                    }
                    else
                    {
                        Response.Write("<script type='text/JavaScript'>alert('用户名或密码错误！')</script>");

                    }


            }

            return View("Reviselogin");
        }

        /// <summary>
        /// 用户登陆注册统一的入口
        /// </summary>
        /// <returns></returns>
        public ActionResult Reviselogin()
        {
            return View();
        }

        /// <summary>
        /// 社团登陆注册统一入口
        /// </summary>
        /// <returns></returns>
        public ActionResult PartyLoginIndex()
        {
            return View();

        }

        /// <summary>
        /// 社团登陆处理函数
        /// </summary>
        public ActionResult LoginParty()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginParty( PartyLoginModel  loginmodel)
        {

            IUserInformation user = new UserInformation();
            if (user.PartyLogin(loginmodel.PartyName, loginmodel.Password))
            {

                FormsAuthentication.SetAuthCookie(loginmodel.PartyName, true);

                return RedirectToAction("PartyIndex", "PartyActivities");

            }
            else
            {
                Response.Write("<script type='text/JavaScript'>alert('用户名或密码错误！')</script>");
            }

            return View("PartyLoginIndex");
            

        }
        /// <summary>
        /// 社团注册(两个都用视图登陆模型)
        /// </summary>

        [HttpPost]
        public ActionResult RegisterParty(PartyLoginModel registerModel)
        {
            IUserInformation user = new UserInformation();
            if (ModelState.IsValid)
            {

                if (user.CheckPartyNameUnique(registerModel.PartyName))
                {
                    if (user.PartyRegister(registerModel))
                    {

                        return Content("<script type='text/JavaScript'>alert('注册成功！');window.location='/Account/PartyLoginIndex'</script>");
                    }
                    else
                    {
                        Response.Write("<script type='text/JavaScript'>alert('未按规定的格式注册！')</script>");

                    }
                }
                else
                {
                    Response.Write("<script type='text/JavaScript'>alert('该社团已经被注册过了！')</script>");

                }
            }

            return View("PartyLoginIndex");


        }

       

        /// <summary>
        /// 个人信息展示
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "普通用户")]
        public ActionResult PersonalInfo()
        {
            ///返回用户信息的函数
            IUserInformation userInfo = new UserInformation();
            UserInfo user = userInfo.GetUserInfo(User.Identity.Name);
            return View(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "普通用户")]
        public ActionResult PersonalInfo(HttpPostedFileBase image, UserInfo userinfo)
        {
            if (image != null)
            {
                string upLoadPath = Server.MapPath("~/Content/Images/UserImages/");

                Random rn = new Random();
                string unique = User.Identity.Name;

                string fileName = unique + System.IO.Path.GetExtension(image.FileName);
                ///获取存储物理路径

                string fileAdress = upLoadPath + fileName;

                string photoAddress = "/content/Images/UserImages/" + fileName;
                image.SaveAs(fileAdress);
                IUserInformation userInfo = new UserInformation();
                userInfo.UpLoadUserImage(unique, photoAddress);
                return RedirectToAction("PersonalInfo");
            }
            else
            {

                Response.Write("<script type='text/javascript'>alert('没有选择任何文件！')</script>");
            }
            if (userinfo != null)
            {
                IUserInformation userInfoFunction = new UserInformation();
                if (userInfoFunction.AlterUserInfo(userinfo))
                {
                    return RedirectToAction("PersonalInfo");
                }


            }
            return RedirectToAction("PersonalInfo");

        }
        /// <summary>
        /// 用户注销
        /// </summary>
        /// <returns></returns>

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePassword()
        {

            return View();

        }
        [HttpPost]
        [Authorize(Roles = "普通用户")]
        public ActionResult UpdatePassword(UpdatePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                IUserInformation user = new UserInformation();
                Session["code"] = model.ConfirmCode;
                string username = User.Identity.Name;
                if (user.ResetPassword(username, model))
                {

                    return RedirectToAction("PersonalInfo");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('旧密码输入错误！')</script>");
                }



            }
            return View(model);

        }




    }
}