//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class T003用户角色表
    {
        public string Email { get; set; }
        public string UserAuthority { get; set; }
    
        public virtual T001账号表 T001账号表 { get; set; }
    }
}
