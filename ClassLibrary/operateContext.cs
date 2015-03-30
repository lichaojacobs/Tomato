using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
  public  class operateContext
    {

      public static IBLL.IBLLSession BLLSession = DI.SpringHelper.GetObject<IBLL.IBLLSession>("BLLSession");

    }
}
