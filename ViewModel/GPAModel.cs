using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace ViewModel
{

    public class GPAModel
    {
        public string uid { get; set; }
        public string password { get; set; }
        //public int term { get; set; }
        ////term=0表示所有学期，
        //public int grade { get; set; }

        public List<List<decimal>> result = new List<List<decimal>>();
        public int[] chartdata = new int[5] { 0, 0, 0, 0, 0 };
        public List<string>urlsuffix=new List<string>();
        public int keyforterm;
        public int checkpassword;
        public void GPAInit()
        {
            keyforterm = getterm(uid, password);
            getHtml(uid, password, keyforterm);


        }


        public int getterm(string uid, string password)
        {
            string loginUrl = "http://e.tju.edu.cn/Main/logon.do";
            string termUrl = "http://e.tju.edu.cn/Education/stuachv.do";
            HttpHeader header = new HttpHeader();
            header.accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            header.contentType = "application/x-www-form-urlencoded";
            header.method = "POST";
            header.userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            header.maxTry = 300;
            string postdata = "uid=" + uid + "&" + "password=" + password;
            string html = HTMLHelper.GetHtml(termUrl, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);

            Regex R = new Regex(@"(?<=""titlelink"">)(\d*)(?=</a>)"); 
            MatchCollection Rmc = R.Matches(html);

            Regex check = new Regex(@"修改登录密码");
            MatchCollection checkmc = check.Matches(html);
            checkpassword = checkmc.Count;

            foreach (Match match in Rmc)
            {
                urlsuffix.Add(match.Value);
            }
            int i = Rmc.Count;

            return i;
        }


        public void getHtml(string uid, string password,int i)
        {
            string loginUrl = "http://e.tju.edu.cn/Main/logon.do";


            HttpHeader header = new HttpHeader();
            header.accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            header.contentType = "application/x-www-form-urlencoded";
            header.method = "POST";
            header.userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            header.maxTry = 300;
            string postdata = "uid=" + uid + "&" + "password=" + password;

            if (i == 8)
            {

                
                string resultUrl1 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[0];
                string resultUrl2 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[1];
                string resultUrl3 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[2];
                string resultUrl4 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[3];
                string resultUrl5 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[4];
                string resultUrl6 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[5];
                string resultUrl7 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[6];
                string resultUrl8 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[7];


                string html1 = HTMLHelper.GetHtml(resultUrl1, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html2 = HTMLHelper.GetHtml(resultUrl2, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html3 = HTMLHelper.GetHtml(resultUrl3, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html4 = HTMLHelper.GetHtml(resultUrl4, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html5 = HTMLHelper.GetHtml(resultUrl5, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html6 = HTMLHelper.GetHtml(resultUrl6, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html7 = HTMLHelper.GetHtml(resultUrl7, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html8 = HTMLHelper.GetHtml(resultUrl8, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);

                List<decimal> get1 = getdata(html1);
                List<decimal> get2 = getdata(html2);
                List<decimal> get3 = getdata(html3);
                List<decimal> get4 = getdata(html4);
                List<decimal> get5 = getdata(html5);
                List<decimal> get6 = getdata(html6);
                List<decimal> get7 = getdata(html7);
                List<decimal> get8 = getdata(html8);



                decimal sumofgpa = get1[2] + get2[2] + get3[2] + get4[2] + get5[2] + get6[2]+get7[2]+get8[2];
                decimal sumofpoint = get1[3] + get2[3] + get3[3] + get4[3] + get5[3] + get6[3] + get7[3] + get8[3];
                decimal sumofweight = get1[4] + get2[4] + get3[4] + get4[4] + get5[4] + get6[4] + get7[4] + get8[4];
                List<decimal> get = new List<decimal> { decimal.Round((sumofgpa / sumofweight), 2), decimal.Round((sumofpoint / sumofweight), 2) };

                result.Add(get1);
                result.Add(get2);
                result.Add(get3);
                result.Add(get4);
                result.Add(get5);
                result.Add(get6);
                result.Add(get7);
                result.Add(get8);
                result.Add(get);
            
            }
            else if (i == 7)
            {

                string resultUrl1 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[0];
                string resultUrl2 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[1];
                string resultUrl3 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[2];
                string resultUrl4 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[3];
                string resultUrl5 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[4];
                string resultUrl6 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[5];
                string resultUrl7 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[6];


                string html1 = HTMLHelper.GetHtml(resultUrl1, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html2 = HTMLHelper.GetHtml(resultUrl2, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html3 = HTMLHelper.GetHtml(resultUrl3, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html4 = HTMLHelper.GetHtml(resultUrl4, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html5 = HTMLHelper.GetHtml(resultUrl5, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html6 = HTMLHelper.GetHtml(resultUrl6, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html7 = HTMLHelper.GetHtml(resultUrl7, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);


                List<decimal> get1 = getdata(html1);
                List<decimal> get2 = getdata(html2);
                List<decimal> get3 = getdata(html3);
                List<decimal> get4 = getdata(html4);
                List<decimal> get5 = getdata(html5);
                List<decimal> get6 = getdata(html6);
                List<decimal> get7 = getdata(html7);



                decimal sumofgpa = get1[2] + get2[2] + get3[2] + get4[2] + get5[2] + get6[2] + get7[2];
                decimal sumofpoint = get1[3] + get2[3] + get3[3] + get4[3] + get5[3] + get6[3] + get7[3];
                decimal sumofweight = get1[4] + get2[4] + get3[4] + get4[4] + get5[4] + get6[4] + get7[4];
                List<decimal> get = new List<decimal> { decimal.Round((sumofgpa / sumofweight), 2), decimal.Round((sumofpoint / sumofweight), 2) ,sumofgpa,sumofpoint,sumofweight};

                result.Add(get1);
                result.Add(get2);
                result.Add(get3);
                result.Add(get4);
                result.Add(get5);
                result.Add(get6);
                result.Add(get7);
                result.Add(get);
            
            
            
            
            }
            else if (i == 6)
            {
                                
                string resultUrl1 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[0];
                string resultUrl2 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[1];
                string resultUrl3 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[2];
                string resultUrl4 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[3];
                string resultUrl5 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[4];
                string resultUrl6 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[5];

                string html1 = HTMLHelper.GetHtml(resultUrl1, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html2 = HTMLHelper.GetHtml(resultUrl2, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html3 = HTMLHelper.GetHtml(resultUrl3, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html4 = HTMLHelper.GetHtml(resultUrl4, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html5 = HTMLHelper.GetHtml(resultUrl5, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html6 = HTMLHelper.GetHtml(resultUrl6, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);

                List<decimal> get1 = getdata(html1);
                List<decimal> get2 = getdata(html2);
                List<decimal> get3 = getdata(html3);
                List<decimal> get4 = getdata(html4);
                List<decimal> get5 = getdata(html5);
                List<decimal> get6 = getdata(html6);




                decimal sumofgpa = get1[2] + get2[2] + get3[2] + get4[2] + get5[2] + get6[2];
                decimal sumofpoint = get1[3] + get2[3] + get3[3] + get4[3] + get5[3] + get6[3];
                decimal sumofweight = get1[4] + get2[4] + get3[4] + get4[4] + get5[4] + get6[4];
                List<decimal> get = new List<decimal> { decimal.Round((sumofgpa / sumofweight), 2), decimal.Round((sumofpoint / sumofweight), 2), sumofgpa, sumofpoint, sumofweight };

                result.Add(get1);
                result.Add(get2);
                result.Add(get3);
                result.Add(get4);
                result.Add(get5);
                result.Add(get6);
                result.Add(get);
            }
            else if (i == 5)
            {

                                
                string resultUrl1 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[0];
                string resultUrl2 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[1];
                string resultUrl3 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[2];
                string resultUrl4 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[3];
                string resultUrl5 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[4];

                string html1 = HTMLHelper.GetHtml(resultUrl1, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html2 = HTMLHelper.GetHtml(resultUrl2, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html3 = HTMLHelper.GetHtml(resultUrl3, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html4 = HTMLHelper.GetHtml(resultUrl4, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html5 = HTMLHelper.GetHtml(resultUrl5, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);

                List<decimal> get1 = getdata(html1);
                List<decimal> get2 = getdata(html2);
                List<decimal> get3 = getdata(html3);
                List<decimal> get4 = getdata(html4);
                List<decimal> get5 = getdata(html5);




                decimal sumofgpa = get1[2] + get2[2] + get3[2] + get4[2] + get5[2];
                decimal sumofpoint = get1[3] + get2[3] + get3[3] + get4[3] + get5[3];
                decimal sumofweight = get1[4] + get2[4] + get3[4] + get4[4] + get5[4];
                List<decimal> get = new List<decimal> { decimal.Round((sumofgpa / sumofweight), 2), decimal.Round((sumofpoint / sumofweight), 2), sumofgpa, sumofpoint, sumofweight };

                result.Add(get1);
                result.Add(get2);
                result.Add(get3);
                result.Add(get4);
                result.Add(get5);
                result.Add(get);
            
            
            }
            else if (i == 4)
            {                
                string resultUrl1 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[0];
                string resultUrl2 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[1];
                string resultUrl3 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[2];
                string resultUrl4 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[3];


                string html1 = HTMLHelper.GetHtml(resultUrl1, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html2 = HTMLHelper.GetHtml(resultUrl2, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html3 = HTMLHelper.GetHtml(resultUrl3, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html4 = HTMLHelper.GetHtml(resultUrl4, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                List<decimal> get1 = getdata(html1);
                List<decimal> get2 = getdata(html2);
                List<decimal> get3 = getdata(html3);
                List<decimal> get4 = getdata(html4);




                decimal sumofgpa = get1[2] + get2[2] + get3[2] + get4[2];
                decimal sumofpoint = get1[3] + get2[3] + get3[3] + get4[3];
                decimal sumofweight = get1[4] + get2[4] + get3[4] + get4[4];
                List<decimal> get = new List<decimal> { decimal.Round((sumofgpa / sumofweight), 2), decimal.Round((sumofpoint / sumofweight), 2), sumofgpa, sumofpoint, sumofweight };

                result.Add(get1);
                result.Add(get2);
                result.Add(get3);
                result.Add(get4);
                result.Add(get);
            
            
            }
            else if (i == 3)
            {
                                
                string resultUrl1 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[0];
                string resultUrl2 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[1];
                string resultUrl3 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[2];

                string html1 = HTMLHelper.GetHtml(resultUrl1, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html2 = HTMLHelper.GetHtml(resultUrl2, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html3 = HTMLHelper.GetHtml(resultUrl3, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);

                List<decimal> get1 = getdata(html1);
                List<decimal> get2 = getdata(html2);
                List<decimal> get3 = getdata(html3);




                decimal sumofgpa = get1[2] + get2[2] + get3[2];
                decimal sumofpoint = get1[3] + get2[3] + get3[3];
                decimal sumofweight = get1[4] + get2[4] + get3[4];
                List<decimal> get = new List<decimal> { decimal.Round((sumofgpa / sumofweight), 2), decimal.Round((sumofpoint / sumofweight), 2), sumofgpa, sumofpoint, sumofweight };

                result.Add(get1);
                result.Add(get2);
                result.Add(get3);
                result.Add(get);
            
            }
            else if (i == 2)
            {                
                string resultUrl1 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[0];
                string resultUrl2 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[1];

                string html1 = HTMLHelper.GetHtml(resultUrl1, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                string html2 = HTMLHelper.GetHtml(resultUrl2, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);

                List<decimal> get1 = getdata(html1);
                List<decimal> get2 = getdata(html2);




                decimal sumofgpa = get1[2] + get2[2];
                decimal sumofpoint = get1[3] + get2[3];
                decimal sumofweight = get1[4] + get2[4];
                List<decimal> get = new List<decimal> { decimal.Round((sumofgpa / sumofweight), 2), decimal.Round((sumofpoint / sumofweight), 2), sumofgpa, sumofpoint, sumofweight };

                result.Add(get1);
                result.Add(get2);
                result.Add(get);
            
            }
            else if (i == 1)
            {                string resultUrl1 = "http://e.tju.edu.cn/Education/stuachv.do?todo=display&term="+urlsuffix[0];

                string html1 = HTMLHelper.GetHtml(resultUrl1, HTMLHelper.GetCooKie(loginUrl, postdata, header), header);
                List<decimal> get1 = getdata(html1);




                decimal sumofgpa = get1[2];
                decimal sumofpoint = get1[3];
                decimal sumofweight = get1[4];
                List<decimal> get = new List<decimal> { decimal.Round((sumofgpa / sumofweight), 2), decimal.Round((sumofpoint / sumofweight), 2), sumofgpa, sumofpoint, sumofweight };

                result.Add(get1);
                result.Add(get);
            }
            else
            { }




        }
        public List<decimal> getdata(string html)
        {
            Regex weightR = new Regex(@"(?<=<font class=ContextText2>[^-]*</font></td>.*\n*.*<td class="".*""  align=""center""><font class=ContextText2>.*\n*.*</font></td>.*\n*.*<td class="".*""  align=""center""><font class=ContextText2>)(\d?\.?\d?)(?=</font></td>)");
            Regex pointR = new Regex(@"(?<=<font class=ContextText2>[^-]*</font></td>.*\n*.*<td class="".*""  align=""center""><font class=ContextText2>.*\n*.*</font></td>.*\n*.*<td class="".*""  align=""center""><font class=ContextText2>\d?\.?\d?</font></td>.*\n*.*\n*.*<td class="".*""  align=""center""><font class=ContextText2>\s*\n*\s*\n*\s*)\d{1,3}(?=\s)");
            MatchCollection weightmc = weightR.Matches(html);
            MatchCollection pointmc = pointR.Matches(html);
            List<decimal> weightlist = new List<decimal>();
            List<decimal> pointlist = new List<decimal>();
            foreach (Match match in weightmc)
            {
                weightlist.Add(Convert.ToDecimal(match.Value));
            }
            foreach (Match match in pointmc)
            {
                pointlist.Add(Convert.ToDecimal(match.Value));
            }

            return handledata(weightlist, pointlist);

        }
        public List<decimal> handledata(List<decimal> weightlist, List<decimal> pointlist)
        {
            decimal averagetemp;
            decimal gpatemp;
            try
            {
                decimal sumofweight = 0;
                for (int i = 0; i < pointlist.Count; i++)
                {
                    if (pointlist[i] > 100) { }
                    else
                    {
                        sumofweight += weightlist[i];
                    }
                }

                decimal sumofpoint = 0;
                for (int i = 0; i < pointlist.Count; i++)
                {
                    if (pointlist[i] > 100) { }
                    else
                    {
                        sumofpoint += pointlist[i] * weightlist[i];
                    }
                }
                averagetemp = sumofpoint / sumofweight;



                decimal sumofgpa = 0;
                for (int i = 0; i < pointlist.Count; i++)
                {
                    if (pointlist[i] <= 100 && pointlist[i] >= 90)
                    {
                        sumofgpa += 4 * weightlist[i];
                    }
                    else if (pointlist[i] <= 89 && pointlist[i] >= 85)
                    {

                        sumofgpa += Convert.ToDecimal(3.7) * weightlist[i];

                    }
                    else if (pointlist[i] <= 84 && pointlist[i] >= 82)
                    {

                        sumofgpa += Convert.ToDecimal(3.3) * weightlist[i];

                    }
                    else if (pointlist[i] <= 81 && pointlist[i] >= 78)
                    {

                        sumofgpa += Convert.ToDecimal(3.0) * weightlist[i];

                    }
                    else if (pointlist[i] <= 77 && pointlist[i] >= 75)
                    {

                        sumofgpa += Convert.ToDecimal(2.7) * weightlist[i];

                    }
                    else if (pointlist[i] <= 74 && pointlist[i] >= 72)
                    {

                        sumofgpa += Convert.ToDecimal(2.3) * weightlist[i];

                    }
                    else if (pointlist[i] <= 71 && pointlist[i] >= 68)
                    {

                        sumofgpa += Convert.ToDecimal(2.0) * weightlist[i];
                    }
                    else if (pointlist[i] <= 67 && pointlist[i] >= 64)
                    {

                        sumofgpa += Convert.ToDecimal(1.5) * weightlist[i];

                    }
                    else if (pointlist[i] <= 63 && pointlist[i] >= 60)
                    {

                        sumofgpa += Convert.ToDecimal(1.0) * weightlist[i];

                    }

                }

                gpatemp = sumofgpa / sumofweight;





                for (int i = 0; i < pointlist.Count; i++)
                {

                    if (pointlist[i] < 60)
                    {

                        chartdata[0]++;


                    }
                    else if (pointlist[i] >= 60 && pointlist[i] < 70)
                    {
                        chartdata[1]++;

                    }
                    else if (pointlist[i] >= 70 && pointlist[i] < 80)
                    {
                        chartdata[2]++;

                    }
                    else if (pointlist[i] >= 80 && pointlist[i] < 90)
                    {
                        chartdata[3]++;

                    }
                    else if (pointlist[i] >= 90)
                    {
                        chartdata[4]++;

                    }
                }




                List<decimal> temp = new List<decimal> { decimal.Round(gpatemp, 2), decimal.Round(averagetemp, 2), sumofgpa, sumofpoint, sumofweight };
                return temp;



            }

            catch
            {





                gpatemp = 0;
                averagetemp = 0;


                List<decimal> temp = new List<decimal> { decimal.Round(gpatemp, 2), decimal.Round(averagetemp, 2), 0, 0, 0 };
                return temp;

            }
        }



    }
    public class HTMLHelper
    {
        public static CookieContainer GetCooKie(string loginUrl, string postdata, HttpHeader header)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create(loginUrl);
                request.Method = header.method;
                request.ContentType = header.contentType;
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = postdatabyte.Length;
                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //提交请求
                Stream stream;
                stream = request.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();

                //接收响应
                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

                CookieCollection cook = response.Cookies;
                //Cookie字符串格式
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);

                return cc;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取html
        /// </summary>
        /// <param name="getUrl"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string GetHtml(string getUrl, CookieContainer cookieContainer, HttpHeader header)
        {
            //Thread.Sleep(1000);
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(getUrl);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = header.contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = header.maxTry;
                httpWebRequest.Referer = getUrl;
                httpWebRequest.Accept = header.accept;
                httpWebRequest.UserAgent = header.userAgent;
                httpWebRequest.Method = "GET";
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();
                return html;
            }
            catch (Exception e)
            {
                if (httpWebRequest != null) httpWebRequest.Abort();
                if (httpWebResponse != null) httpWebResponse.Close();
                return string.Empty;
            }
        }




    }




    public class HttpHeader
    {
        public string contentType { get; set; }

        public string accept { get; set; }

        public string userAgent { get; set; }

        public string method { get; set; }

        public int maxTry { get; set; }
    }



}



