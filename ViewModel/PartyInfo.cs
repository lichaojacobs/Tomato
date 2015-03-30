using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   public class PartyInfo
    {
        [Required]
        public string partyName { get; set; }

        public string partyPurpose { get; set; }

        public string partyProperty { get; set; }

        public string partyTask { get; set; }

        public string partyActivities { get; set; }

        public string partyFoundingTime { get; set; }

        public string partyMembers { get; set; }

        public string partyDescribe { get; set; }

        public string partyLogo { get; set; }

       

    }
}
