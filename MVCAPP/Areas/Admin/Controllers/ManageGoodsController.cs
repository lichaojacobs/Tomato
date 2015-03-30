using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAPP.Areas.Admin.Controllers
{
    public class ManageGoodsController : Controller
    {
        // GET: Admin/ManageGoods
        int pageSize = 5;
        [Authorize(Roles="管理员")]
        public ActionResult Index(int page=1)
        {
            ViewModel.GoodsListModel goodsList = new ViewModel.GoodsListModel
            {
                goodsList = new ClassLibrary.MarketDownstairs().getAllGoods(page, pageSize),
                pageInfo = new ViewModel.PagingInfo { currentpage = page, itemperpage = pageSize, Totalitems = new ClassLibrary.MarketDownstairs().getTotalCount() }
            };
            return View(goodsList);
            
        }

        /// <summary>
        /// 获得详细商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       [Authorize(Roles = "管理员")]
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
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "管理员")]
        public ActionResult DeleteGoods(int id)
        {
            if (new ClassLibrary.MarketDownstairs().DeleteGoods(id))
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