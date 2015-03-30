using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI
{
    public class SpringHelper
    {
        #region 1.0 Spring容器上下文 -IApplicationContext SpringContext
        /// <summary>
        /// Spring容器上下文
        /// </summary>
        private static IApplicationContext SpringContext
        {
            get
            {

                 return ContextRegistry.GetContext();
               
            }
        }
        #endregion

        #region 2.0 获取配置文件 配置的 对象 +T GetObject<T>(string objName) where T : class
        /// <summary>
        /// 获取配置文件 配置的 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static T GetObject<T>(string objName) where T : class
        {
            return (T)SpringContext.GetObject(objName);
        }
        #endregion
    }
}
