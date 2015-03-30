using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace DAL
{
    public class DBContextFactory:IDAL.IDBContextFactory
    {
        #region 创建 EF上下文 对象，在线程中共享 一个 上下文对象 + DbContext GetDbContext()
        /// <summary>
        /// 创建 EF上下文 对象，在线程中共享 一个 上下文对象
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            //从当前线程中 获取 EF上下文对象
            DbContext dbContext = CallContext.GetData(typeof(DBContextFactory).Name) as DbContext;
            if (dbContext == null)
            {
                dbContext = new Model.TomatoEntities();
                //将新创建的 ef上下文对象 存入线程
                CallContext.SetData(typeof(DBContextFactory).Name, dbContext);
            }
            return dbContext;
        } 
        #endregion
    }
}
