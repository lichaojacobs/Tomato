using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserInfo
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public string[] Roles { get; set; }

        public string UserState { get; set; }

        public string photoUrl { get; set; }
    }
}
