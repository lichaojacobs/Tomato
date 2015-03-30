using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ViewModel;



namespace ClassLibrary.UserInformation
{
   public class UserInformation : IUserInformation
    {
        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <param name="emailID">邮箱ID</param>
        /// <returns>用户信息类</returns>
       public UserInfo GetUserInfo(string emailAdress)
        {
            var t001 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == emailAdress)[0];
            UserInfo userInfor = new UserInfo();
            if (t001 != null)
            {
                userInfor.username = t001.UserName;
                userInfor.nickname = t001.NickName;
                userInfor.email = t001.Email;
                userInfor.password = t001.Password;
                userInfor.Roles = new UserAuthtication().GetRoles(t001.Email);
                userInfor.UserState = t001.UserState;
                userInfor.photoUrl = t001.PhotoUrl;
                return userInfor;
            }
            return null;
        }

        /// <summary>
        /// 修改个人信息（密码除外）
        /// </summary>
        /// <param name="userInfo">用户信息类</param>
        /// <returns>修改是否成功</returns>
        public bool AlterUserInfo(UserInfo userinfo)
        {
            var t001 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == userinfo.email).FirstOrDefault();
            if (t001 != null)
            {
                t001.UserName = userinfo.username;
                t001.NickName = userinfo.nickname;
                string[] alterInfo = { "UserName", "NickName" };
                operateContext.BLLSession.IT001账号表BLL.Modify(t001, alterInfo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查邮件地址是否重复
        /// </summary>
        /// <param name="EmailAddress">所查邮件地址</param>
        /// <returns>true为“该地址可用”</returns>
        public bool CheckEmailUnique(string emailAddress)
        {
            var unique = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == emailAddress).Count();
            if (unique == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 给目标邮箱发送激活码 并将激活码记录DB
        /// </summary>
        /// <param name="myEmail">注册邮箱</param>
        /// <returns>是否发送成功</returns>
       
        public bool SendConfirmEmail(string emailAdress)
        {
            try
            {
                try
                {
                    Random rnd = new Random();
                    int activeCode = rnd.Next(1000, 9999);
                    string code = Encryption.Encrype.GetMD5(activeCode.ToString());

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("tju_team@163.com", "Vivion");
                    mailMessage.To.Add(emailAdress);
                    mailMessage.Subject = "Tomato激活验证";
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.Body = "点击以下链接进行账号激活<br/>"+"<a href =' http://localhost:13138/Account/ReceiveEmail?email=" + emailAdress + "&code=" + code + "'>" + "http://localhost:13138/Account/ReceiveEmail"+"</a>";
                    mailMessage.Priority = MailPriority.High;
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.163.com", 25);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("tju_team@163.com", "tjuteam");
                    smtp.Timeout = 100000;

                    object userState = mailMessage;
                    smtp.Send(mailMessage);

                    //将加密字符写入DB                   
                    T002验证表 t002 = new T002验证表();
                    t002.Email = emailAdress;
                    t002.Code = code;
                    t002.RiseDateTime = DateTime.Now;
                    operateContext.BLLSession.IT002验证表BLL.Add(t002);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

       /// <summary>
       /// 收到来自客户端的链接，进行比对并激活用户
       /// </summary>
       /// <param name="email"></param>
       /// <param name="password"></param>
       /// <returns></returns>
        public bool CheckToConfirmUser(string email, string code)
        {
            if (operateContext.BLLSession.IT002验证表BLL.GetListBy(m => m.Email == email && m.Code == code).Count() != 0)
            {
                operateContext.BLLSession.IT002验证表BLL.DelBy(m => m.Email == email);
                var t001 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == email).FirstOrDefault();
                t001.UserState = "已激活";
                operateContext.BLLSession.IT001账号表BLL.Modify(t001, "UserState");
                return true;

            }
            else
            {
                return false;
            }
 
        }


       /// <summary>
       /// 向邮箱发送重置密码确认
       /// </summary>
       /// <param name="emailAdress"></param>
       /// <returns></returns>

        public bool SendResetPasswordEmail(string emailAdress)
        {
            try
            {
                try
                {

                    Random rnd = new Random();
                    int activeCode = rnd.Next(1000, 9999);
                    string code = Encryption.Encrype.GetMD5(activeCode.ToString());
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("tju_team@163.com", "Vivion");
                    mailMessage.To.Add(emailAdress);
                    mailMessage.Subject = "Tomato激活验证";
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.Body = "点击以下链接进行重置密码确认<br/>" + "<a href =' http://localhost:13138/Account/ReceiveResetMsg?email=" + emailAdress + "&code=" + code + "'>" + "http://localhost:13138/Account/ReceiveEmail" + "</a>";
                    mailMessage.Priority = MailPriority.High;
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.163.com", 25);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("tju_team@163.com", "tjuteam");
                    smtp.Timeout = 100000;

                    object userState = mailMessage;
                    smtp.Send(mailMessage);

                    //将加密字符写入DB                   
                    T002验证表 t002 = new T002验证表();
                    t002.Email = emailAdress;
                    t002.Code = code;
                    t002.RiseDateTime = DateTime.Now;
                    operateContext.BLLSession.IT002验证表BLL.Add(t002);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }

 
        }


       /// <summary>
       /// 收到客户端的验证数据重置密码
       /// </summary>
       /// <returns></returns>
        public bool CheckToResetPassword(string email,string code)
        {
            if (operateContext.BLLSession.IT002验证表BLL.GetListBy(m => m.Email == email && m.Code == code).Count() != 0)
            {
                operateContext.BLLSession.IT002验证表BLL.DelBy(m => m.Email == email);
                var t001 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == email).FirstOrDefault();
                string password = "000000";
                t001.Password = Encryption.Encrype.GetMD5(password);
                operateContext.BLLSession.IT001账号表BLL.Modify(t001, "Password");
                return true;

            }
            else
            {
                return false;
            }

 
        }
       /// <summary>
       /// 检查社团名字是否重复
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
        public bool CheckPartyNameUnique(string name)
        {
            var unique = operateContext.BLLSession.IT009社团账号表BLL.GetListBy(m => m.PartyName == name).Count();
            if (unique == 0)
            {
                return true;
            }
            return false;

        }
        
        /// <summary>
        /// 写入数据库
        /// 1,表示成功，2表示邮箱无效
        /// </summary>
        /// <returns></returns>
        public bool Register(RegisterModel registerModel)
        {
           
                T001账号表 t001 = new T001账号表();
                t001.Email = registerModel.email;
                t001.UserName = null;
                t001.Password = Encryption.Encrype.GetMD5(registerModel.password);
                t001.NickName = null;
                t001.UserState = "未激活";
                t001.PhotoUrl = null;
                T003用户角色表 t003 = new T003用户角色表();
                t003.Email = registerModel.email;
                t003.UserAuthority = "普通用户";
                T006店铺信息表 t006 = new T006店铺信息表();
                t006.Owners = registerModel.email;
                operateContext.BLLSession.IT001账号表BLL.Add(t001);
                operateContext.BLLSession.IT003用户角色表BLL.Add(t003);
                operateContext.BLLSession.IT006店铺信息表BLL.Add(t006);
                if (SendConfirmEmail(registerModel.email))
                {

                    return true;

                }
                else
                {
                    return false;
 
                }
                
            
          

        }

       /// <summary>
       /// 社团注册
       /// </summary>
       /// <param name="registerModel"></param>
       /// <returns></returns>
        public bool PartyRegister(PartyLoginModel registerModel)
        {

            try
            {
                
                T009社团账号表 t001 = new T009社团账号表();
                t001.PartyName = registerModel.PartyName;
                t001.Password = Encryption.Encrype.GetMD5(registerModel.Password);
                
                t001.State = "未认证";
                operateContext.BLLSession.IT009社团账号表BLL.Add(t001);

                T004社团信息表 t004 = new T004社团信息表();
                t004.PartyName = registerModel.PartyName;
                t004.Property = Properties.Resources.PartyDefaultValue.ToString();
                t004.Purpose = Properties.Resources.PartyDefaultValue.ToString();
                t004.Task = Properties.Resources.PartyDefaultValue.ToString();
                t004.FoundingTime = Properties.Resources.PartyDefaultValue.ToString();
                t004.Members = Properties.Resources.PartyDefaultValue.ToString();
                t004.Activities = Properties.Resources.PartyDefaultValue.ToString();
                t004.Describe = Properties.Resources.PartyDefaultValue.ToString();
                operateContext.BLLSession.IT004社团信息表BLL.Add(t004);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
 
        }

        /// <summary>
        /// 登陆
        /// 返回的数据1表示普通用户  2表示管理员,0 表示账号或者密码错误
        /// </summary>
        /// <param name="emailAdress">邮箱ID</param>
        /// <param name="password">邮箱密码</param>
        /// <returns>用户名与密码是否一致</returns>
        public int  LogIn(string emailAdress, string password)
        {
            
            string passwordFinal = Encryption.Encrype.GetMD5(password);
            var t001 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == emailAdress&&m.Password==passwordFinal).Count();

            if (t001 != 0)
            {
                string userAuthor = operateContext.BLLSession.IT003用户角色表BLL.GetListBy(m => m.Email == emailAdress).FirstOrDefault().UserAuthority;
                if (userAuthor == "普通用户")
                {
                    return 1;
                }

                else
                {
                    return 2;
                }
            }
            return 0;
        }

       /// <summary>
       /// 获取用户头像
       /// </summary>
       /// <param name="emailAdress"></param>
       /// <returns></returns>
        public string GetPhotoUrl(string emailAdress)
        {
            return operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == emailAdress).FirstOrDefault().PhotoUrl;
        }

       /// <summary>
       /// 社团登陆
       /// </summary>
       /// <param name="partyName"></param>
       /// <param name="password"></param>
       /// <returns></returns>
        public bool PartyLogin(string partyName, string password)
        {
            string passwordFinal = Encryption.Encrype.GetMD5(password);
            var t001 = operateContext.BLLSession.IT009社团账号表BLL.GetListBy(m => m.PartyName ==partyName && m.Password == passwordFinal).Count();
            if (t001 != 0)
            {
                return true;
            }
            return false;
 
        }
        /// <summary>
        /// 更改新密码 发送验证邮件
        /// </summary>
        /// <param name="emailAdress">邮箱ID</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>是否发送验证邮件成功</returns>
        public bool KeepNewPassword(string emailAdress, string newPassword)
        {
            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns>修改密码是否成功</returns>
        public bool ReadInNewPassword()
        {
            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="emailAdress">邮箱ID</param>
        /// <returns>修改密码是否成功</returns>
        public bool ResetPassword(string emailAdress,UpdatePasswordModel model)
        {
            string md5Password = Encryption.Encrype.GetMD5(model.OldPassword);//之后真正测试改过来
            
            var t001 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == emailAdress&&m.Password==md5Password).FirstOrDefault();
            if (t001 != null)
            {
                t001.Password = Encryption.Encrype.GetMD5(model.NewPassword);
                t001.Email = emailAdress;
                operateContext.BLLSession.IT001账号表BLL.Modify(t001, "Password");
                return true;
            }
            else
            {
                return false;
            }
            

        }

       /// <summary>
       /// 上传用户头像
       /// </summary>
       /// <param name="emailAdress"></param>
       /// <param name="photoUrl"></param>
       /// <returns></returns>
        public bool UpLoadUserImage(string emailAdress, string photoUrl)
        {
            var t001 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == emailAdress).FirstOrDefault();
            if (t001 != null)
            {
                t001.PhotoUrl = photoUrl;
                operateContext.BLLSession.IT001账号表BLL.Modify(t001, "PhotoUrl");
                return true;
            }
            else
            {
                return false;
            }
 
        }

        /// <summary>
        /// 检查用户权限
        /// </summary>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        public string CheckAuthority(string emailAdress)
        {
            throw new NotImplementedException();
        }


        }
}
