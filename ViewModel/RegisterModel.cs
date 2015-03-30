using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class RegisterModel
    {
        //[Required]
        //public string username { get; set; }

        //[Required]
        //[StringLength(12,MinimumLength=6)]
        public string password { get; set; }

        //[Required]
        //[StringLength(12,MinimumLength=6)]
        //[System.Web.Mvc.Compare("password", ErrorMessage = "两次密码不匹配。")]
        //public string confirmPassword { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        public string email { get; set; }


        //[Common.验证类.V00001验证码验证(ErrorMessage="验证码错误")]
        //public string ConfirmCode { get; set; }
       
      
    }
}
