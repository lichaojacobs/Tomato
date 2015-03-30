using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using ClassLibrary;

namespace MVCAPP.Areas.Admin.Controllers
{
    public class ManagePartiesController : Controller
    {
        // GET: Admin/ManageParties
        /// <summary>
        /// 社团管理首页
        /// </summary>
        /// <returns></returns>

        // GET: Admin/ManageParties

        int pageSize = 5;
        public ActionResult Index(int page = 1)
        {
            PartyInfoList partyList = new PartyInfoList();
            {
                partyList = new PartyActivities().GetAllPartyInfo(pageSize, page);
            };
            return View(partyList);

        }

        public ActionResult GetDetail(string partyName)
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

        public ActionResult DeleteParty(string partyName)
        {
            partyName = Request["partyName"];
            if (partyName == null)
            {
                return RedirectToAction("Index");
            }
            if (new ClassLibrary.PartyActivities().DeleteParty(partyName))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult DeletePost(string postName, string partyName)
        {
            postName = Request["postName"];
            if (postName == null)
            {
                return RedirectToAction("GetDetail(" + partyName + ")");
            }
            if (new ClassLibrary.PartyActivities().DeletePost(postName))
            {
                return RedirectToAction("GetDetail(" + partyName + ")");
            }
            else
            {
                return RedirectToAction("GetDetail(" + partyName + ")");
            }

        }
        /// <summary>
        /// 封锁社团
        /// </summary>
        /// <param name="partyName"></param>
        /// <returns></returns>
        public ActionResult BlockParty(string partyName)
        {
            string party = Server.HtmlDecode(partyName);
            if (new ClassLibrary.PartyActivities().BlockParty(party))
            {
                return Content("<script type='text/javascript'>var truth=window.confirm('修改成功！！');if(truth){window.location='/Admin/ManageParties/Index'}</script>");
            }
            else
            {
                return Content("<script type='text/javascript'>var truth=window.confirm('该社团认证已经解除！！');if(truth){window.location='/Admin/ManageParties/Index'}</script>");
 
            }
            

 
        }
        /// <summary>
        /// 认证社团
        /// </summary>
        /// <param name="partyName"></param>
        /// <returns></returns>
        public ActionResult CertifyParty(string partyName)
        {
            string party = Server.HtmlDecode(partyName);
            if (new ClassLibrary.PartyActivities().CertifyParty(party))
            {
                return Content("<script type='text/javascript'>var truth=window.confirm('修改成功！！');if(truth){window.location='/Admin/ManageParties/Index'}</script>");
            }
            else
            {
                return Content("<script type='text/javascript'>var truth=window.confirm('该社团已经被认证！！');if(truth){window.location='/Admin/ManageParties/Index'}</script>");

            }
            
 
        }

    }
}