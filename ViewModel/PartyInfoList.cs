using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
  public class PartyInfoList
    {
      public List<Model.T004社团信息表> PartyList { get; set; }

      public PagingInfo pageinfo { get; set; }
    }
}
