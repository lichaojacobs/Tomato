using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
namespace ViewModel.ClassRoomModel
{
    public class ClassRoomSearchModel
    {
        public string html;
        public int weekday = 0;
        public int weeknum = 0;
        public List<List<string>> resultlist1 = new List<List<string>>();
        public List<List<string>> resultlist2 = new List<List<string>>();
        public List<List<string>> resultlist3 = new List<List<string>>();
        public List<List<string>> resultlist4 = new List<List<string>>();



        public void Init(int i)
        {

            //weekday
            if (DateTime.Now.DayOfWeek.ToString() == "Monday") weekday = 1;
            else if (DateTime.Now.DayOfWeek.ToString() == "Tuesday") weekday = 2;
            else if (DateTime.Now.DayOfWeek.ToString() == "Wednesday") weekday = 3;
            else if (DateTime.Now.DayOfWeek.ToString() == "Thursday") weekday = 4;
            else if (DateTime.Now.DayOfWeek.ToString() == "Friday") weekday = 5;
            else if (DateTime.Now.DayOfWeek.ToString() == "Saturday") weekday = 6;
            else if (DateTime.Now.DayOfWeek.ToString() == "Sunday") weekday = 7;

           //weeknum
            weeknum = (DateTime.Now.DayOfYear - 55) / 7 + 1;



            if (i == -1)
            {
                if (weekday == 7) { weeknum++; weekday = 1; }
                else { weekday++; }
            
            }
            if (weeknum >= 26) weeknum = 26;
            if (weeknum <= 0) weeknum = 1;

            resultlist1 = Getdata(weeknum, weekday, "0015");
            resultlist2 = Getdata(weeknum, weekday, "1042");
            resultlist3 = Getdata(weeknum, weekday, "1084");
            resultlist4 = Getdata(weeknum, weekday, "1085");
        }

        public List<List<string>> Getdata(int weeknum, int weekday, string building_no)
        {
            HttpHeader header = new HttpHeader();
            header.accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            header.contentType = "application/x-www-form-urlencoded";
            header.method = "POST";
            header.userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            header.maxTry = 300;
            string postdata = "todo=displayWeekBuilding&week=" + weeknum.ToString() + "&building_no=" + building_no;

            html = HTMLHelper.HtmlResponse("http://e.tju.edu.cn/Education/toModule.do?prefix=/Education&page=/schedule.do?todo=displayWeekBuilding&schekind=6",postdata, header);
            //23 0015
            //24 1042
            //26A 1084
            //26B 1085


            Regex state = new Regex("(?<=<font color=\")(.*)(?=\">●</font>)");
            MatchCollection statemc = state.Matches(html);
            List<string> statelist = new List<string>();

            Regex roomno = new Regex("(?<=<strong><font color=\"White\">)(.*)(?=</font></strong>)");
            MatchCollection roomnomc = roomno.Matches(html);
            List<string> roomnolist = new List<string>();


            foreach (Match match in statemc)
            {
                statelist.Add(match.Value);

            }

            foreach (Match match in roomnomc)
            {
                roomnolist.Add(match.Value);
            }

            return GetResult(statelist, roomnolist);
        
        
        }


        public List<List<string>> GetResult(List<string> statelist, List<string> roomnolist)
        {
            List<List<string>> temp = new List<List<string>>();
            //整块时间
            List<string> temp1 = new List<string>();//上午
            List<string> temp2 = new List<string>();//下午
            List<string> temp3 = new List<string>();//晚上
            //整节时间
            List<string> temp4 = new List<string>();//上午第一节
            List<string> temp5 = new List<string>();//上午第二节
            List<string> temp6 = new List<string>();//下午第一节
            List<string> temp7 = new List<string>();//下午第二节
            List<string> temp8 = new List<string>();//晚上第一节
            List<string> temp9 = new List<string>();//晚上第二节

            for (int i = 0; i < roomnolist.Count; ++i)
            {
                if (weekday == 1)
                {
                    if (statelist[i * 42 + 2] == "#00dd00" && statelist[i * 42 + 3] == "#00dd00")
                        temp1.Add(roomnolist[i] + " 上午");
                    if (statelist[i * 42 + 4] == "#00dd00" && statelist[i * 42 + 5] == "#00dd00")
                        temp2.Add(roomnolist[i] + " 下午");
                    if (statelist[i * 42 + 6] == "#00dd00" && statelist[i * 42 + 7] == "#00dd00")
                        temp3.Add(roomnolist[i] + " 晚上");

                    if (statelist[i * 42 + 2] == "#00dd00")
                        temp4.Add(roomnolist[i] + "上午第一节");
                    if (statelist[i * 42 + 3] == "#00dd00")
                        temp5.Add(roomnolist[i] + "上午第二节");
                    if (statelist[i * 42 + 4] == "#00dd00")
                        temp6.Add(roomnolist[i] + "下午第一节");
                    if (statelist[i * 42 + 5] == "#00dd00")
                        temp7.Add(roomnolist[i] + "下午第二节");
                    if (statelist[i * 42 + 6] == "#00dd00")
                        temp8.Add(roomnolist[i] + "晚上第一节");
                    if (statelist[i * 42 + 7] == "#00dd00")
                        temp9.Add(roomnolist[i] + "晚上第二节");

                }

                if (weekday == 2)
                {
                    if (statelist[i * 42 + 8] == "#00dd00" && statelist[i * 42 + 9] == "#00dd00")
                        temp1.Add(roomnolist[i] + " 上午");
                    if (statelist[i * 42 + 10] == "#00dd00" && statelist[i * 42 + 11] == "#00dd00")
                        temp2.Add(roomnolist[i] + " 下午");
                    if (statelist[i * 42 + 12] == "#00dd00" && statelist[i * 42 + 13] == "#00dd00")
                        temp3.Add(roomnolist[i] + " 晚上");

                    if (statelist[i * 42 + 8] == "#00dd00")
                        temp4.Add(roomnolist[i] + "上午第一节");
                    if (statelist[i * 42 + 9] == "#00dd00")
                        temp5.Add(roomnolist[i] + "上午第二节");
                    if (statelist[i * 42 + 10] == "#00dd00")
                        temp6.Add(roomnolist[i] + "下午第一节");
                    if (statelist[i * 42 + 11] == "#00dd00")
                        temp7.Add(roomnolist[i] + "下午第二节");
                    if (statelist[i * 42 + 12] == "#00dd00")
                        temp8.Add(roomnolist[i] + "晚上第一节");
                    if (statelist[i * 42 + 13] == "#00dd00")
                        temp9.Add(roomnolist[i] + "晚上第二节");

                }
                if (weekday == 3)
                {
                    if (statelist[i * 42 + 14] == "#00dd00" && statelist[i * 42 + 15] == "#00dd00")
                        temp1.Add(roomnolist[i] + " 上午");
                    if (statelist[i * 42 + 16] == "#00dd00" && statelist[i * 42 + 17] == "#00dd00")
                        temp2.Add(roomnolist[i] + " 下午");
                    if (statelist[i * 42 + 18] == "#00dd00" && statelist[i * 42 + 19] == "#00dd00")
                        temp3.Add(roomnolist[i] + " 晚上");

                    if (statelist[i * 42 + 14] == "#00dd00")
                        temp4.Add(roomnolist[i] + " 上午第一节");
                    if (statelist[i * 42 + 15] == "#00dd00")
                        temp5.Add(roomnolist[i] + " 上午第二节");
                    if (statelist[i * 42 + 16] == "#00dd00")
                        temp6.Add(roomnolist[i] + " 下午第一节");
                    if (statelist[i * 42 + 17] == "#00dd00")
                        temp7.Add(roomnolist[i] + " 下午第二节");
                    if (statelist[i * 42 + 18] == "#00dd00")
                        temp8.Add(roomnolist[i] + " 晚上第一节");
                    if (statelist[i * 42 + 19] == "#00dd00")
                        temp9.Add(roomnolist[i] + " 晚上第二节");

                }
                if (weekday == 4)
                {
                    if (statelist[i * 42 + 20] == "#00dd00" && statelist[i * 42 + 21] == "#00dd00")
                        temp1.Add(roomnolist[i] + "上午");
                    if (statelist[i * 42 + 22] == "#00dd00" && statelist[i * 42 + 23] == "#00dd00")
                        temp2.Add(roomnolist[i] + "下午");
                    if (statelist[i * 42 + 24] == "#00dd00" && statelist[i * 42 + 25] == "#00dd00")
                        temp3.Add(roomnolist[i] + "晚上");

                    if (statelist[i * 42 + 20] == "#00dd00")
                        temp4.Add(roomnolist[i] + "上午第一节");
                    if (statelist[i * 42 + 21] == "#00dd00")
                        temp5.Add(roomnolist[i] + "上午第二节");
                    if (statelist[i * 42 + 22] == "#00dd00")
                        temp6.Add(roomnolist[i] + "下午第一节");
                    if (statelist[i * 42 + 23] == "#00dd00")
                        temp7.Add(roomnolist[i] + "下午第二节");
                    if (statelist[i * 42 + 24] == "#00dd00")
                        temp8.Add(roomnolist[i] + "晚上第一节");
                    if (statelist[i * 42 + 25] == "#00dd00")
                        temp9.Add(roomnolist[i] + "晚上第二节");

                }
                if (weekday == 5)
                {
                    if (statelist[i * 42 + 26] == "#00dd00" && statelist[i * 42 + 27] == "#00dd00")
                        temp1.Add(roomnolist[i] + "上午");
                    if (statelist[i * 42 + 28] == "#00dd00" && statelist[i * 42 + 29] == "#00dd00")
                        temp2.Add(roomnolist[i] + "下午");
                    if (statelist[i * 42 + 30] == "#00dd00" && statelist[i * 42 + 31] == "#00dd00")
                        temp3.Add(roomnolist[i] + "晚上");

                    if (statelist[i * 42 + 26] == "#00dd00")
                        temp4.Add(roomnolist[i] + "上午第一节");
                    if (statelist[i * 42 + 27] == "#00dd00")
                        temp5.Add(roomnolist[i] + "上午第二节");
                    if (statelist[i * 42 + 28] == "#00dd00")
                        temp6.Add(roomnolist[i] + "下午第一节");
                    if (statelist[i * 42 + 29] == "#00dd00")
                        temp7.Add(roomnolist[i] + "下午第二节");
                    if (statelist[i * 42 + 30] == "#00dd00")
                        temp8.Add(roomnolist[i] + "晚上第一节");
                    if (statelist[i * 42 + 31] == "#00dd00")
                        temp9.Add(roomnolist[i] + "晚上第二节");

                }
                if (weekday == 6)
                {
                    if (statelist[i * 42 + 32] == "#00dd00" && statelist[i * 42 + 33] == "#00dd00")
                        temp1.Add(roomnolist[i] + "上午");
                    if (statelist[i * 42 + 34] == "#00dd00" && statelist[i * 42 + 35] == "#00dd00")
                        temp2.Add(roomnolist[i] + "下午");
                    if (statelist[i * 42 + 36] == "#00dd00" && statelist[i * 42 + 37] == "#00dd00")
                        temp3.Add(roomnolist[i] + "晚上");

                    if (statelist[i * 42 + 32] == "#00dd00")
                        temp4.Add(roomnolist[i] + "上午第一节");
                    if (statelist[i * 42 + 33] == "#00dd00")
                        temp5.Add(roomnolist[i] + "上午第二节");
                    if (statelist[i * 42 + 34] == "#00dd00")
                        temp6.Add(roomnolist[i] + "下午第一节");
                    if (statelist[i * 42 + 35] == "#00dd00")
                        temp7.Add(roomnolist[i] + "下午第二节");
                    if (statelist[i * 42 + 36] == "#00dd00")
                        temp8.Add(roomnolist[i] + "晚上第一节");
                    if (statelist[i * 42 + 37] == "#00dd00")
                        temp9.Add(roomnolist[i] + "晚上第二节");

                }
                if (weekday == 7)
                {
                    if (statelist[i * 42 + 38] == "#00dd00" && statelist[i * 42 + 39] == "#00dd00")
                        temp1.Add(roomnolist[i] + "上午");
                    if (statelist[i * 42 + 40] == "#00dd00" && statelist[i * 42 + 41] == "#00dd00")
                        temp2.Add(roomnolist[i] + "下午");
                    if (statelist[i * 42 + 42] == "#00dd00" && statelist[i * 42 + 43] == "#00dd00")
                        temp3.Add(roomnolist[i] + "晚上");

                    if (statelist[i * 42 + 38] == "#00dd00")
                        temp4.Add(roomnolist[i] + "上午第一节");
                    if (statelist[i * 42 + 39] == "#00dd00")
                        temp5.Add(roomnolist[i] + "上午第二节");
                    if (statelist[i * 42 + 40] == "#00dd00")
                        temp6.Add(roomnolist[i] + "下午第一节");
                    if (statelist[i * 42 + 41] == "#00dd00")
                        temp7.Add(roomnolist[i] + "下午第二节");
                    if (statelist[i * 42 + 42] == "#00dd00")
                        temp8.Add(roomnolist[i] + "晚上第一节");
                    if (statelist[i * 42 + 43] == "#00dd00")
                        temp9.Add(roomnolist[i] + "晚上第二节");

                }
            }
            temp.Add(temp1);
            temp.Add(temp2);
            temp.Add(temp3);
            temp.Add(temp4);
            temp.Add(temp5);
            temp.Add(temp6);
            temp.Add(temp7);
            temp.Add(temp8);
            temp.Add(temp9);


            return temp;
        }
    }
}