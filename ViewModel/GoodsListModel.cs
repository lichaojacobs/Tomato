using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   public class GoodsListModel
    {
       
       public List<Model.T007店铺货物表> goodsList { get; set; }
       public PagingInfo pageInfo { get; set; }

    }
}
