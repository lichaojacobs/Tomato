using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Common
{
    /// <summary>
    /// 360 安全助手
    /// </summary>
    public class SecurityHelper
    {
        #region 1.0 使用 票据对象 将 用户数据 加密成字符串 +string EncryptUserInfo(string userInfo)
        /// <summary>
        /// 使用 票据对象 将 用户数据 加密成字符串
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static string EncryptUserInfo(string userInfo)
        {
            //1.1 将用户数据 存入 票据对象
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "哈哈", DateTime.Now, DateTime.Now, true, userInfo);
            //1.2 将票据对象 加密成字符串（可逆）
            string strData = FormsAuthentication.Encrypt(ticket);
            return strData;
        }
        #endregion

        #region 2.0 加密字符串 解密 +string DecryptUserInfo(string cryptograph)
        /// <summary>
        /// 加密字符串 解密
        /// </summary>
        /// <param name="cryptograph">加密字符串</param>
        /// <returns></returns>
        public static string DecryptUserInfo(string cryptograph)
        {
            //1.1 将 加密字符串 解密成 票据对象
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cryptograph);
            //1.2 将票据里的 用户数据 返回
            return ticket.UserData;
        }
        #endregion
    }
}
