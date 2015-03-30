using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Common

{
    public class 验证类
    {
        //验证码 
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
        public sealed class V00001验证码验证Attribute : ValidationAttribute
        {
            private const string _defaultErrorMessage = "验证码错误";

            public override bool IsValid(object value)
            {

                try
                {
                    string st = value.ToString();
                    if (st == HttpContext.Current.Session["code"].ToString())
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}