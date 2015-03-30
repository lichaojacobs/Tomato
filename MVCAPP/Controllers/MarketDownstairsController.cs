using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace MVCAPP.Controllers
{
    public class MarketDownstairsController : Controller
    {
        // GET: MarketDownstairs
         //分页返回所有商品，每页六个
         int pageSize = 6;
        public ActionResult MarketIndex(int page = 1)
        {

            ViewModel.GoodsListModel goodsList = new ViewModel.GoodsListModel
            {
                goodsList = new ClassLibrary.MarketDownstairs().getAllGoods(page, pageSize),
                pageInfo = new ViewModel.PagingInfo { currentpage = page, itemperpage = pageSize, Totalitems = new ClassLibrary.MarketDownstairs().getTotalCount() }
            };
            return View(goodsList);

        }
        /// <summary>
        /// 我的商店
        /// </summary>
        /// <returns>返回商店信息</returns>
        [Authorize(Roles = "普通用户")]
        public ActionResult MyMall()
        {
            string owner = User.Identity.Name;
            ViewModel.MyMallInfo myMallInfo = new ClassLibrary.MarketDownstairs().GetMyMallInfo(owner);
            return View(myMallInfo);
        }

        [HttpPost]
        public ActionResult MyMall(ViewModel.MyMallInfo myMallInfo)
        {
            myMallInfo.Owner = User.Identity.Name;
            if (new ClassLibrary.MarketDownstairs().UpdateMyMallInfo(myMallInfo))
            {
                ViewModel.MyMallInfo myNewMallInfo = new ClassLibrary.MarketDownstairs().GetMyMallInfo(myMallInfo.Owner);
                return View(myNewMallInfo);
            }
            return View(myMallInfo);
        }
        /// <summary>
        /// 获取具体的商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetDetail(int id)
        {
            ViewModel.GoodDetailModel goodDetail = new ViewModel.GoodDetailModel
            {
                MallGood = new ClassLibrary.MarketDownstairs().GetGoodDetail(id),
                MallInfo = new ClassLibrary.MarketDownstairs().GetGoodMall(id)
            };
            return View(goodDetail);


        }
        /// <summary>
        /// 用户上传商品
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadGood()
        {
            ViewModel.UpLoadGoodModel model = new ViewModel.UpLoadGoodModel();
            model.shopId = new ClassLibrary.MarketDownstairs().GetNowMallID(User.Identity.Name);

            return View(model);

        }

        [HttpPost]
        public ActionResult UploadGood(ViewModel.UpLoadGoodModel uploadModel, HttpPostedFileBase image)
        {
            //if (image != null)
            //{
            //    string upLoadPath = Server.MapPath("~/Content/Images/");

            //    Random rn = new Random();
            //    string unique = rn.Next(1000).ToString();

            //    string fileName = uploadModel.GoodName + unique + System.IO.Path.GetExtension(image.FileName);
            //    ///获取存储物理路径

            //    string fileAdress = upLoadPath + fileName;

            //    string photoAdress = "/content/Images/" + fileName;
            //    image.SaveAs(fileAdress);
               ///获取提交上来的图片路径
              string photoAddress = Request.Form["imageAddress"];
              if (photoAddress != null)
              {
                  bool flag = new ClassLibrary.MarketDownstairs().UpLoadGoodInfo(uploadModel, photoAddress);
                  if (flag)
                  {

                      return RedirectToAction("MarketIndex");

                  }
                  else
                  {
                      return View(uploadModel);
                  }
              }
              else
              {

                  return View(uploadModel);
 
              }

            //}
            //else
            //{
            //    return View(uploadModel);
            //}



        }

        /// <summary>
        /// 根据关键词获得指定的货物
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRightGoods(int page = 1)
        {

            string keyWords = Server.HtmlDecode(this.Request.Form["keyWords"]);
            if (keyWords != null)
            {
                
               
                Session["key"] = keyWords;
                return View(new ClassLibrary.MarketDownstairs().getRightGoods(keyWords,page,pageSize));

            }
            else
            {
                if (Session["key"] != null)
                {
                   
                    return View(new ClassLibrary.MarketDownstairs().getRightGoods(keyWords, page, pageSize));

                }
                return RedirectToAction("MarketIndex");
            }

        }

        /// <summary>
        /// 获取自己商店的货物
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMyMallGoods()
        {
            string userNow = User.Identity.Name;

            return View(new ClassLibrary.MarketDownstairs().getMyMallGoods(userNow));


        }

        /// <summary>
        /// 根据货物Id删去指定货物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteGood(int id)
        {
            if (new ClassLibrary.MarketDownstairs().DeleteGoods(id))
            {
                return RedirectToAction("MyMall");

            }
            else

            {
                ///之后设置错误页面
                return View();
            }
           
        }
        /// <summary>
        /// 显示系统通知
        /// </summary>
        /// <returns></returns>
        public ActionResult Notice()
        {

            return View();
 
        }
       

    }
}