//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Travel_Request_System_EF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ORG_EmpEntityLink
    {
        public int TransactType { get; set; }
        public long TransactUserID { get; set; }
        public System.DateTime TransactDateTime { get; set; }
        public string MenuID { get; set; }
        [Key]
        public long EmpEntityId { get; set; }
        public long EmployeeId { get; set; }
        public long EntityId { get; set; }
        public System.DateTime EffectiveFrom { get; set; }
        public Nullable<System.DateTime> EffectiveTo { get; set; }
        public string RecordStatus { get; set; }
    
        public virtual HRW_Employee HRW_Employee { get; set; }
        public virtual ORG_EntityMaster ORG_EntityMaster { get; set; }
    }
}
