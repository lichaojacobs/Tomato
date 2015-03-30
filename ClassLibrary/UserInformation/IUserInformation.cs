using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace ClassLibrary.UserInformation
{
    public interface IUserInformation
    {
        #region 用户信息模块

        UserInfo GetUserInfo(string emailAdress);

        bool AlterUserInfo(UserInfo userinfo);

        bool CheckEmailUnique(string emailAddress);
        bool CheckPartyNameUnique(string name);

        bool SendConfirmEmail(string emailAdress);

        bool CheckToConfirmUser(string email, string code);

        bool SendResetPasswordEmail(string emailAdress);
        bool CheckToResetPassword(string email, string code);


        /// <summary>
        /// 写入数据库
        /// </summary>
        /// <returns></returns>
        bool Register(RegisterModel registerModel);
        bool PartyRegister(PartyLoginModel registerModel);

        int LogIn(string emailAdress, string password);
        string GetPhotoUrl(string emailAdress);
        bool PartyLogin(string partyName, string password);

        string CheckAuthority(string emailAdress);

        bool KeepNewPassword(string emailAdress, string newPassword);

        bool ReadInNewPassword();

        bool ResetPassword(string emailAdress,UpdatePasswordModel model);

        
       

        bool UpLoadUserImage(string emailAdress,string photoUrl);

        #endregion
    }
}
