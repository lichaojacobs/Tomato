using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using ClassLibrary;

namespace MVCAPP.Controllers
{
    public class PartyActivitiesController : Controller
    {
        // GET: PartyActivities

        int pageSize = 6;
        /// <summary>
        /// 社团开始默认页面
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult PartyIndex(int page = 1)
        {
            return View(new PartyActivities().GetAllPartyInfo(pageSize, page));

        }
        /// <summary>
        /// 进入我的社团主页(这个作为了一个子动作在三个页面中都有它)
        /// </summary>
        /// <param name="partyName"></param>
        /// <returns></returns>
        /// 先注释掉权限测试完再开启
        [Authorize(Roles = "社团")]
        public ActionResult MyPartyInformation(string partyName)
        {
            partyName = Request["partyName"];
            if (partyName == null)
            {
                Response.Redirect("PartyIndex");
                return null;
            }
            PartyInformationModel partyInformationModel = new PartyInformationModel
            {

                partyInfo = new PartyActivities().GetPartyInfo(partyName),
                listPostInfo = new PartyActivities().GetPartyActivities(partyName)

            };

            return View(partyInformationModel);
        }
        /// <summary>
        /// 添加社团活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "社团")]

        public ActionResult AddActivities()
        {
            //to do 添加活动页
            PostInfo postInfo = new PostInfo();
            //postInfo.postPartyName = Request.Cookies[0].ToString();
            postInfo.postPartyName = User.Identity.Name;
            return View(postInfo);
        }

        [HttpPost]
        [Authorize(Roles = "社团")]
        public ActionResult AddActivities(PostInfo postInfo, HttpPostedFileBase image)
        {

            //to do 提交添加
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    postInfo.postPartyName = Request.Cookies[0].ToString();
                    string upLoadPath = Server.MapPath("~/Content/Images/PartyActivities");

                    Random rn = new Random();
                    string unique = rn.Next(1000).ToString();
                    string fileName = postInfo.postName + unique + System.IO.Path.GetExtension(image.FileName);
                    ///获取文件物理地址存入数据库
                    string fileAdress = upLoadPath + fileName;

                    string photoAdress = "/content/Images/PartyActivities/" + fileName;
                    image.SaveAs(fileAdress);
                    //将图片URL赋值模型的属性
                    postInfo.postPhotoURL = fileAdress;
                    if (new PartyActivities().AddActivities(postInfo))
                    {
                        Response.Write("活动添加成功！");
                        return RedirectToAction("MyPartyInformation");
                    }

                }


            }
            return View(postInfo);
        }
        /// <summary>
        /// 活动信息
        /// </summary>
        /// <param name="postName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostInformation(string postName)
        {
            //to do 返回海报活动详情
            postName = Request["postName"];
            if (postName == null)
            {
                Response.Redirect("PartyIndex");
                return null;
            }
            PostInfo postInfo = new PostInfo();
            postInfo = new PartyActivities().GetPostInfo(postName);
            return View(postInfo);
        }

        [HttpGet]
        [Authorize(Roles = "社团")]
        public ActionResult SetPartyInfo(string partyName)
        {
            partyName = Request["partyName"];
            if (partyName == null)
            {
                Response.Redirect("PartyIndex");
                return null;
            }
            @ViewBag.partyName = partyName;
            ClassLibrary.PartyActivities setpartyinfo = new PartyActivities();
            return View(setpartyinfo.GetPartyInfo(partyName));

        }
        [HttpPost]
        public ActionResult SetPartyInfo(ViewModel.PartyInfo partyInfo, HttpPostedFileBase image)
        {
            if (image != null)
            {

                string upLoadPath = Server.MapPath("~/Content/Images/PartyLogo/");

                Random rn = new Random();
                string unique = rn.Next(1000).ToString();
                string fileName = partyInfo.partyName + unique + System.IO.Path.GetExtension(image.FileName);
                ///获取文件物理地址存入数据库
                string fileAdress = upLoadPath + fileName;

                string photoAdress = "/content/Images/PartyLogo/" + fileName;
                image.SaveAs(fileAdress);
                //将图片URL赋值模型的属性
                partyInfo.partyLogo = photoAdress;
                if (new PartyActivities().UpdataPartyInfo(partyInfo))
                {

                    return Content("<script type='text/javascript'>var truth=window.confirm('基本信息修改成功！！');if(truth){window.location='/PartyActivities/SetPartyInfo'}</script>");

                }

            }
            else
            {
                if (new ClassLibrary.PartyActivities().UpdataPartyInfo(partyInfo))
                {
                    return Content("<script type='text/javascript'>var truth=window.confirm('基本信息修改成功！！');if(truth){window.location='/PartyActivities/SetPartyInfo'}</script>");
                }

            }

            return View(partyInfo);

        }

        [HttpGet]
        [Authorize(Roles = "社团")]
        public ActionResult MyPartyActivities()
        {

            return View(new ClassLibrary.PartyActivities().GetPartyActivities(User.Identity.Name));

        }
        [HttpPost]
        public ActionResult MyPartyActivities(PostInfo postinfo)
        {
            //To Do
            return View();

        }

        [HttpGet]
        [Authorize(Roles = "社团")]
        public ActionResult Notice()
        {

            return View();

        }

        [HttpGet]
        [Authorize(Roles = "社团")]
        public ActionResult UpdatePassword()
        {

            return View();

        }

        [HttpPost]
        [Authorize(Roles = "社团")]
        public ActionResult UpdatePassword(partyUpdatePasswordModel model)
        {
            model.partyName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (new PartyActivities().ReSetPartyPassword(model))
                {
                    Response.Write("密码修改成功！");
                }
                else
                {
                    Response.Write("原密码输入有误！");
                }
            }
            return View();
        }

        /// <summary>
        /// 点击viewDetails后进入的页面，属于普通用户或者游客可以进入的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewPartyDetails(string partyName)
        {

            PartyInformationModel partyInformationModel = new PartyInformationModel();
            partyInformationModel.partyInfo = new PartyActivities().GetPartyInfo(partyName);
            partyInformationModel.listPostInfo = new PartyActivities().GetPartyActivities(partyName);
            return View(partyInformationModel);

        }
    }
}