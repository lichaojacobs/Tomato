using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class partyUpdatePasswordModel
    {
        [Required]
        public string partyName { get; set; }

        [Required(ErrorMessage = "旧密码不能为空")]
        public string oldPassword { get; set; }

        [Required(ErrorMessage = "新密码不能为空")]
        [StringLength(12, MinimumLength = 6)]
        public string newPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "密码和确认密码不匹配")]
        public string newPasswordAgain { get; set; }

        [Common.验证类.V00001验证码验证(ErrorMessage = "验证码错误")]
        public string confirmCode { get; set; }
    }
}
