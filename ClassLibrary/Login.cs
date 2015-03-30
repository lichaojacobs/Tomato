using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Login
    {
        public bool IsValid(ViewModel.LoginModel user)
        {
           // BLL.T001账号表BLL u = new BLL.T001账号表BLL();

            int number = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.UserName == user.UserName).Count();
            if (number != 0)
            {
                return true;

            }
            else
            {
                return false;
            }
 
        }


 
    }

}
