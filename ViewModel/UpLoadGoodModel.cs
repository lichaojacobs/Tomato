using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   public class UpLoadGoodModel
    {
       public int shopId { get; set; }
       public string GoodName { get; set; }

       public string Introduction { get; set; }
       public int GoodNumber { get; set; }
       public decimal GoodPrice { get; set; }

       public string GoodPhoto { get; set; }
    }
}
