using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using Model;

namespace ClassLibrary
{
    public class PartyActivities
    {
        #region 所有社团信息

        /// <summary>
        /// 所有社团信息
        /// </summary>
        /// <returns></returns>
        public PartyInfoList GetAllPartyInfo(int pagesize,int pageNow)
        {
            
            var t004 = operateContext.BLLSession.IT004社团信息表BLL.GetListBy(m => m.PartyName != null);

            PartyInfoList partyInfoList = new PartyInfoList
            {
                PartyList = t004.Skip((pageNow-1)*pagesize).Take(pagesize).ToList(),
                pageinfo = new PagingInfo {  itemperpage=pagesize, currentpage=pageNow, Totalitems=t004.Count()}

            };
            //foreach (var item in t004)
            //{
            //    PartyInfo partyInfo = new PartyInfo();
            //    partyInfo.partyName = item.PartyName;
            //    partyInfo.partyPurpose = item.Purpose;
            //    partyInfo.partyProperty = item.Property;
            //    partyInfo.partyTask = item.Task;
            //    partyInfo.partyActivities = item.Activities;
            //    partyInfo.partyFoundingTime = item.FoundingTime;
            //    partyInfo.partyMembers = item.Members;
            //    partyInfo.partyDescribe = item.Describe;
            //    listPartyInfo.Add(partyInfo);
            //}
            return partyInfoList;
        }

        #endregion

        #region 获取社团信息

        /// <summary>
        /// 获取社团信息
        /// </summary>
        /// <param name="partyName"></param>
        /// <returns></returns>
        public PartyInfo GetPartyInfo(string partyName)
        {
            PartyInfo partyInfo = new PartyInfo();
            var t004 = operateContext.BLLSession.IT004社团信息表BLL.GetListBy(m => m.PartyName == partyName)[0];
            partyInfo.partyName = t004.PartyName;
            partyInfo.partyPurpose = t004.Purpose;
            partyInfo.partyProperty = t004.Property;
            partyInfo.partyTask = t004.Task;
            partyInfo.partyActivities = t004.Activities;
            partyInfo.partyFoundingTime = t004.FoundingTime;
            partyInfo.partyMembers = t004.Members;
            partyInfo.partyDescribe = t004.Describe;
            partyInfo.partyLogo = t004.PartyLogoUrl;
            return partyInfo;
        }

        #endregion

        #region 更新社团信息

        /// <summary>
        /// 更新社团信息
        /// </summary>
        /// <param name="partyInfo"></param>
        /// <returns></returns>
        public bool UpdataPartyInfo(PartyInfo partyInfo)
        {
            var t004 = operateContext.BLLSession.IT004社团信息表BLL.GetListBy(m => m.PartyName == partyInfo.partyName).FirstOrDefault();
            t004.Activities = partyInfo.partyActivities;
            t004.Describe = partyInfo.partyDescribe;
            t004.FoundingTime = partyInfo.partyFoundingTime;
            t004.Members = partyInfo.partyMembers;
            t004.PartyLogoUrl = partyInfo.partyLogo;
            t004.Property = partyInfo.partyProperty;
            t004.Purpose = partyInfo.partyPurpose;
            t004.Task = partyInfo.partyTask;
            string[] allstring = { "Activities", "FoundingTime", "Describe", "Members", "PartyLogoUrl", "Property", "Purpose", "Task" };
            operateContext.BLLSession.IT004社团信息表BLL.Modify(t004, allstring);
            return true;
        }

        #endregion

        #region 获取社团活动信息
        /// <summary>
        /// 获取社团活动信息
        /// </summary>
        /// <param name="partyName"></param>
        /// <returns></returns>
        public List<PostInfo> GetPartyActivities(string partyName)
        {
            List<PostInfo> listPostInfo = new List<PostInfo>();
            var t008 = operateContext.BLLSession.IT008海报信息表BLL.GetListBy(m => m.PartyName == partyName).ToList();
            foreach (var item in t008)
            {
                PostInfo postInfo = new PostInfo();
                postInfo.postName = item.PostName;
                postInfo.postPhotoURL = item.PhotosURL;
                postInfo.postBeginTime = item.PostBeginTime;
                postInfo.postEndTime = item.PostEndTime;
                postInfo.postTicketNumber = item.TicketNumber;
                postInfo.postTicketBeginTime = item.TicketBeginTime;
                postInfo.postTicketEndTime = item.TicketEndTime;
                postInfo.postPartyName = item.PartyName;
                listPostInfo.Add(postInfo);
            }

            return listPostInfo;
        }

        #endregion

        #region 获取海报信息

        /// <summary>
        /// 获取海报信息
        /// </summary>
        /// <param name="postName"></param>
        /// <returns></returns>
        public PostInfo GetPostInfo(string postName)
        {
            PostInfo postInfo = new PostInfo();
            var t008 = operateContext.BLLSession.IT008海报信息表BLL.GetListBy(m => m.PostName == postName)[0];
            postInfo.postName = t008.PostName;
            postInfo.postPhotoURL = t008.PhotosURL;
            postInfo.postBeginTime = t008.PostBeginTime;
            postInfo.postEndTime = t008.PostEndTime;
            postInfo.postTicketNumber = t008.TicketNumber;
            postInfo.postTicketBeginTime = t008.TicketBeginTime;
            postInfo.postTicketEndTime = t008.TicketEndTime;
            postInfo.postPartyName = t008.PartyName;
            return postInfo;
        }

        #endregion

        #region 增加活动

        /// <summary>
        /// 增加活动
        /// </summary>
        /// <param name="postInfo"></param>
        /// <returns></returns>
        public bool AddActivities(PostInfo postInfo)
        {
            T008海报信息表 t008 = new T008海报信息表();
            
            t008.PostName = postInfo.postName;
            t008.PhotosURL = postInfo.postPhotoURL;
            t008.PostBeginTime = postInfo.postBeginTime;
            t008.PostEndTime = postInfo.postEndTime;
            t008.TicketNumber = postInfo.postTicketNumber;
            t008.TicketBeginTime = postInfo.postTicketBeginTime;
            t008.TicketEndTime = postInfo.postTicketEndTime;
            t008.PartyName = postInfo.postPartyName;

            operateContext.BLLSession.IT008海报信息表BLL.Add(t008);
            return true;
        }

        #endregion

        #region 删除社团

        public bool DeleteParty(string partyName)
        {
            try
            {
                var t008 = operateContext.BLLSession.IT008海报信息表BLL.GetListBy(m => m.PartyName == partyName);
                if (t008 != null)
                {
                    //该社团有活动
                    foreach (var item in t008)
                    {
                        operateContext.BLLSession.IT005票务表BLL.DelBy(m => m.PostName == item.PostName);
                        operateContext.BLLSession.IT008海报信息表BLL.DelBy(m => m.PostName == item.PostName);
                    }
                }
                operateContext.BLLSession.IT004社团信息表BLL.DelBy(m => m.PartyName == partyName);
                operateContext.BLLSession.IT009社团账号表BLL.DelBy(m => m.PartyName == partyName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        #endregion

        #region 删除社团活动
        public bool DeletePost(string postName)
        {
            try
            {
                operateContext.BLLSession.IT005票务表BLL.DelBy(m => m.PostName == postName);
                operateContext.BLLSession.IT008海报信息表BLL.DelBy(m => m.PostName == postName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region 封锁社团
        public bool BlockParty(string partyName)
        {
            var t001 = operateContext.BLLSession.IT009社团账号表BLL.GetListBy(m => m.PartyName == partyName).FirstOrDefault();
            if (t001 != null)
            {
                if (t001.State == "未认证")
                {
                    
                    return false;
                }
                else
                {
                    t001.State = "未认证";
                    operateContext.BLLSession.IT009社团账号表BLL.Modify(t001, "State");
                    return true;

                }
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 认证社团

        public bool CertifyParty(string partyName)
        {
            var t001 = operateContext.BLLSession.IT009社团账号表BLL.GetListBy(m => m.PartyName == partyName).FirstOrDefault();
            if (t001 != null)
            {
                if (t001.State == "未认证")
                {
                    t001.State = "已认证";
                    operateContext.BLLSession.IT009社团账号表BLL.Modify(t001, "State");
                    return true;
                }
                else
                {
                   
                    return false;

                }
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 修改社团账号密码

        /// <summary>
        /// 修改社团账号密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns>false:旧密码错误</returns>
        public bool ReSetPartyPassword(partyUpdatePasswordModel model)
        {
            string md5Password = Encryption.Encrype.GetMD5(model.oldPassword);
            var t009 = operateContext.BLLSession.IT009社团账号表BLL.GetListBy(m => m.PartyName == model.partyName && m.Password == md5Password).FirstOrDefault();
            if (t009 != null)
            {
                t009.Password = Encryption.Encrype.GetMD5(model.newPassword);
                t009.PartyName = model.partyName;
                operateContext.BLLSession.IT009社团账号表BLL.Modify(t009, "Password");
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}
