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
    
    public partial class T008海报信息表
    {
        public T008海报信息表()
        {
            this.T005票务表 = new HashSet<T005票务表>();
        }
    
        public string PostName { get; set; }
        public string PhotosURL { get; set; }
        public string PostBeginTime { get; set; }
        public string PostEndTime { get; set; }
        public Nullable<int> TicketNumber { get; set; }
        public string TicketBeginTime { get; set; }
        public string TicketEndTime { get; set; }
        public string PartyName { get; set; }
    
        public virtual T004社团信息表 T004社团信息表 { get; set; }
        public virtual ICollection<T005票务表> T005票务表 { get; set; }
    }
}
