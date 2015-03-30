using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace DAL
{
    public class DBSessionFactory:IDAL.IDBSessionFactory
    {
        /// <summary>
        /// 此方法的作用： 提高效率，在线程中 共用一个 DBSession 对象！
        /// </summary>
        /// <returns></returns>
        public IDAL.IDBSession GetDBSession()
        {
            //从当前线程中 获取 DBContext 数据仓储 对象
            IDAL.IDBSession dbSesion = CallContext.GetData(typeof(DBSessionFactory).Name) as DBSession;
            if (dbSesion == null)
            {
                dbSesion = new DBSession();
                CallContext.SetData(typeof(DBSessionFactory).Name, dbSesion);
            }
            return dbSesion;
        }
    }
}
