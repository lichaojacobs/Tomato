using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PostTicketInfo
    {
        [Required]
        public int ticketId { get; set; }

        [Required]
        public string postName { get; set; }
    }
}
