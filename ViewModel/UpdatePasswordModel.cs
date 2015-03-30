using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
   public class UpdatePasswordModel
    {
       [Required(ErrorMessage="旧密码不能为空")]

       public string OldPassword { get; set; }

        [Required(ErrorMessage="新密码不能为空")]
        [StringLength(12, MinimumLength = 6)]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "密码和确认密码不匹配")]
        public string NewPasswordAgain { get; set; }
        [Common.验证类.V00001验证码验证(ErrorMessage = "验证码错误")]
        public string ConfirmCode { get; set; }

    }
}
