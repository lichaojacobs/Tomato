using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace MVCAPP.Controllers
{
    public class GPAController : Controller
    {
        //
        // GET: /GPA/
        [HttpGet]
        public ActionResult GPA()
        {

            return View();
        }


        [HttpPost]
        public ActionResult GPA(GPAModel model)
        {



                model.GPAInit();

                int checkpassword = model.checkpassword;
                if (checkpassword == 0)
                {
                    ViewBag.chartdata = model.chartdata;
                    int keyforterm = model.keyforterm;
                    @ViewBag.key = keyforterm;

                    if (keyforterm == 8)
                    {
                        ViewBag.averageview1 = model.result[0][1];
                        ViewBag.averageview2 = model.result[1][1];
                        ViewBag.averageview3 = model.result[2][1];
                        ViewBag.averageview4 = model.result[3][1];
                        ViewBag.averageview5 = model.result[4][1];
                        ViewBag.averageview6 = model.result[5][1];
                        ViewBag.averageview7 = model.result[6][1];
                        ViewBag.averageview8 = model.result[7][1];

                        ViewBag.gpaview1 = model.result[0][0];
                        ViewBag.gpaview2 = model.result[1][0];
                        ViewBag.gpaview3 = model.result[2][0];
                        ViewBag.gpaview4 = model.result[3][0];
                        ViewBag.gpaview5 = model.result[4][0];
                        ViewBag.gpaview6 = model.result[5][0];
                        ViewBag.gpaview7 = model.result[6][0];
                        ViewBag.gpaview8 = model.result[7][0];

                        
                        ViewBag.sumofaverage = model.result[8][1];
                        ViewBag.sumofgpa = model.result[8][0];
                        ViewBag.sumofweight = model.result[8][4];

                    }
                    else if (keyforterm == 7)
                    {
                        ViewBag.averageview1 = model.result[0][1];
                        ViewBag.averageview2 = model.result[1][1];
                        ViewBag.averageview3 = model.result[2][1];
                        ViewBag.averageview4 = model.result[3][1];
                        ViewBag.averageview5 = model.result[4][1];
                        ViewBag.averageview6 = model.result[5][1];
                        ViewBag.averageview7 = model.result[6][1];

                        ViewBag.gpaview1 = model.result[0][0];
                        ViewBag.gpaview2 = model.result[1][0];
                        ViewBag.gpaview3 = model.result[2][0];
                        ViewBag.gpaview4 = model.result[3][0];
                        ViewBag.gpaview5 = model.result[4][0];
                        ViewBag.gpaview6 = model.result[5][0];
                        ViewBag.gpaview7 = model.result[6][0];

                        ViewBag.sumofaverage = model.result[7][1];
                        ViewBag.sumofgpa = model.result[7][0];
                        ViewBag.sumofweight = model.result[7][4];

                    }
                    else if (keyforterm == 6)
                    {
                        ViewBag.averageview1 = model.result[0][1];
                        ViewBag.averageview2 = model.result[1][1];
                        ViewBag.averageview3 = model.result[2][1];
                        ViewBag.averageview4 = model.result[3][1];
                        ViewBag.averageview5 = model.result[4][1];
                        ViewBag.averageview6 = model.result[5][1];

                        ViewBag.gpaview1 = model.result[0][0];
                        ViewBag.gpaview2 = model.result[1][0];
                        ViewBag.gpaview3 = model.result[2][0];
                        ViewBag.gpaview4 = model.result[3][0];
                        ViewBag.gpaview5 = model.result[4][0];
                        ViewBag.gpaview6 = model.result[5][0];

                        ViewBag.sumofaverage = model.result[6][1];
                        ViewBag.sumofgpa = model.result[6][0];
                        ViewBag.sumofweight = model.result[6][4];

                    }
                    else if (keyforterm == 5)
                    {
                        ViewBag.averageview1 = model.result[0][1];
                        ViewBag.averageview2 = model.result[1][1];
                        ViewBag.averageview3 = model.result[2][1];
                        ViewBag.averageview4 = model.result[3][1];
                        ViewBag.averageview5 = model.result[4][1];

                        ViewBag.gpaview1 = model.result[0][0];
                        ViewBag.gpaview2 = model.result[1][0];
                        ViewBag.gpaview3 = model.result[2][0];
                        ViewBag.gpaview4 = model.result[3][0];
                        ViewBag.gpaview5 = model.result[4][0];

                        ViewBag.sumofaverage = model.result[5][1];
                        ViewBag.sumofgpa = model.result[5][0];
                        ViewBag.sumofweight = model.result[5][4];

                    }
                    else if (keyforterm == 4)
                    {
                        ViewBag.averageview1 = model.result[0][1];
                        ViewBag.averageview2 = model.result[1][1];
                        ViewBag.averageview3 = model.result[2][1];
                        ViewBag.averageview4 = model.result[3][1];

                        ViewBag.gpaview1 = model.result[0][0];
                        ViewBag.gpaview2 = model.result[1][0];
                        ViewBag.gpaview3 = model.result[2][0];
                        ViewBag.gpaview4 = model.result[3][0];

                        ViewBag.sumofaverage = model.result[4][1];
                        ViewBag.sumofgpa = model.result[4][0];
                        ViewBag.sumofweight = model.result[4][4];

                    }
                    else if (keyforterm == 3)
                    {
                        ViewBag.averageview1 = model.result[0][1];
                        ViewBag.averageview2 = model.result[1][1];
                        ViewBag.averageview3 = model.result[2][1];

                        ViewBag.gpaview1 = model.result[0][0];
                        ViewBag.gpaview2 = model.result[1][0];
                        ViewBag.gpaview3 = model.result[2][0];

                        ViewBag.sumofaverage = model.result[3][1];
                        ViewBag.sumofgpa = model.result[3][0];

                    }
                    else if (keyforterm == 2)
                    {
                        ViewBag.averageview1 = model.result[0][1];
                        ViewBag.averageview2 = model.result[1][1];


                        ViewBag.gpaview1 = model.result[0][0];
                        ViewBag.gpaview2 = model.result[1][0];


                        ViewBag.sumofaverage = model.result[2][1];
                        ViewBag.sumofgpa = model.result[2][0];
                        ViewBag.sumofweight = model.result[2][4];

                    }
                    else if (keyforterm == 1)
                    {
                        ViewBag.averageview1 = model.result[0][1];


                        ViewBag.gpaview1 = model.result[0][0];


                        ViewBag.sumofaverage = model.result[1][1];
                        ViewBag.sumofgpa = model.result[1][0];
                        ViewBag.sumofweight = model.result[1][4];

                    }
                    else { }

                    ////ViewBag.sumofgpa = decimal.Round((ViewBag.gpaview1 * sumofweight1 + ViewBag.gpaview2 * sumofweight2 + ViewBag.gpaview3 * sumofweight3 + ViewBag.gpaview4 * sumofweight4) / (sumofweight1 + sumofweight2 + sumofweight3 + sumofweight4), 2);


                    //model.GPAInit();
                    //@ViewBag.gpaview = model.gpa;
                    return View("GPA2");

                }
                else { return View("passworderror"); }





        }

        public ActionResult test()
        {
           
            return View();


        }


        public ActionResult GPA2()
        {

       
            return View();
        }




        [HttpPost]
        public ActionResult GPA2(SelectListItem item)
        {


            return View("test");

        }


    }
}