using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAPP
{
  public  class SettingsHelper
    {
      public static string IndexPath
      {
          get
          {
              return ConfigurationManager.AppSettings["IndexPath"];
          }
      }

    }
}
