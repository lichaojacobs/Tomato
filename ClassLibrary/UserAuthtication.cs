using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// 获取用户角色
    /// </summary>
   public class UserAuthtication
    {
       public  string[] GetRoles(string UserName )
       {
           string[] roles = new string[2];

           var tempRole1 = operateContext.BLLSession.IT003用户角色表BLL.GetListBy(m => m.Email == UserName).Select(m => m.UserAuthority).FirstOrDefault();
           roles[0] = tempRole1;
           var tempRole2 = operateContext.BLLSession.IT009社团账号表BLL.GetListBy(m => m.PartyName == UserName).Count();
           if (tempRole2 != 0)
           {
               roles[1] = "社团";
           }

           return roles;
       }
    }
}
