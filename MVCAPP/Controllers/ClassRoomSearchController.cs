using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.ClassRoomModel;

namespace MVCAPP.Controllers
{
    public class ClassRoomSearchController : Controller
    {
        //
        // GET: /ClassRoomSearch/
        [HttpGet]
        public ActionResult Index(ClassRoomSearchModel model)
        {

            //23 0015
            //24 1042
            //26A 1084
            //26B 1085
        
            model.Init(1);
            @ViewBag.weeknum = model.weeknum;
            @ViewBag.weekday = model.weekday;
            @ViewBag.resultlist3 = model.resultlist3;
           

            @ViewBag.resultlist4 = model.resultlist4;


            @ViewBag.resultlist1 = model.resultlist1;

            @ViewBag.resultlist2 = model.resultlist2;



            return View();
            
        }


        [HttpPost]
        public ActionResult Index(ClassRoomSearchModel amodel,string nil)
        {
            amodel.Init(-1);
            @ViewBag.weeknum = amodel.weeknum;
            @ViewBag.weekday = amodel.weekday;
            @ViewBag.resultlist3 = amodel.resultlist3;


            @ViewBag.resultlist4 = amodel.resultlist4;


            @ViewBag.resultlist1 = amodel.resultlist1;

            @ViewBag.resultlist2 = amodel.resultlist2;

            return View("Tomorrow");
            
        }

        public ActionResult Tomorrow(ClassRoomSearchModel amodel)
        {




            return View();
        }

    }
}
