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
    
    public partial class HRW_EmpEntityParamValues
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRW_EmpEntityParamValues()
        {
            this.HRW_EmpEntityParamValues1 = new HashSet<HRW_EmpEntityParamValues>();
        }
    
        public int TransactType { get; set; }
        public long TransactUserID { get; set; }
        public System.DateTime TransactDateTime { get; set; }
        public string MenuID { get; set; }
        public long EmpEntityParamID { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<long> EntityID { get; set; }
        public long EntityTypeParamID { get; set; }
        public string ParamValue { get; set; }
        public Nullable<long> ParamValueEntityID { get; set; }
        public Nullable<long> EmpEntityParamIDLink { get; set; }
        public Nullable<long> ESSID { get; set; }
        public string CompanyCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRW_EmpEntityParamValues> HRW_EmpEntityParamValues1 { get; set; }
        public virtual HRW_EmpEntityParamValues HRW_EmpEntityParamValues2 { get; set; }
        public virtual ORG_EntityMaster ORG_EntityMaster { get; set; }
        public virtual ORG_EntityMaster ORG_EntityMaster1 { get; set; }
        public virtual ORG_EntityTypeParam ORG_EntityTypeParam { get; set; }
    }
}
