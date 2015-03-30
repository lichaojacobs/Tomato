using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class PagingInfo
    {
        public int Totalitems { get; set; }
        public int itemperpage { get; set; }
        public int currentpage { get; set; }
        public int totalpages { get{return(int)Math.Ceiling((decimal)Totalitems/itemperpage);} }
        
    }
}