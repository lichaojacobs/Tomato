using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(12,MinimumLength=6)]
        public string Password { get; set; }
        //[Common.验证类.V00001验证码验证(ErrorMessage = "验证码错误")]
        //public string ConfirmCode { get; set; }

        //public bool IsAlways { get; set; }
        
    }
}
