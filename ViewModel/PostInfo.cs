using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class PostInfo
    {
        [Required]
        public string postName { get; set; }

        public string postPhotoURL { get; set; }

        public string postBeginTime { get; set; }

        public string postEndTime { get; set; }

        public int? postTicketNumber { get; set; }

        public string postTicketBeginTime { get; set; }

        public string postTicketEndTime { get; set; }

        [Required]
        public string postPartyName { get; set; }
    }
}
