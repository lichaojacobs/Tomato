using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace DAL
{
   public partial class DBSession:IDAL.IDBSession
    {
       public int saveChanges()
       {
           ////通过线程完善
           return -1;
       }
    }
}
