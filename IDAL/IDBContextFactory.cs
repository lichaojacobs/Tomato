using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace IDAL
{
    /// <summary>
    /// EF数据上下文 工厂
    /// </summary>
    public interface IDBContextFactory
    {
        /// <summary>
        /// 获取 EF 上下文对象
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
    }
}
