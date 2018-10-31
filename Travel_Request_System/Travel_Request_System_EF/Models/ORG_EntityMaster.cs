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
    
    public partial class ORG_EntityMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORG_EntityMaster()
        {
            this.HRW_EmpEntityParamValues = new HashSet<HRW_EmpEntityParamValues>();
            this.HRW_EmpEntityParamValues1 = new HashSet<HRW_EmpEntityParamValues>();
            this.ORG_EmpEntityLink = new HashSet<ORG_EmpEntityLink>();
        }
    
        public int TransactType { get; set; }
        public Nullable<long> TransactUserID { get; set; }
        public System.DateTime TransactDateTime { get; set; }
        public string MenuID { get; set; }
        public long EntityId { get; set; }
        public Nullable<long> EntityTypeId { get; set; }
        public string EntityCode { get; set; }
        public string Description { get; set; }
        public Nullable<long> Parent { get; set; }
        public Nullable<System.DateTime> EffectiveFrom { get; set; }
        public Nullable<System.DateTime> EffectiveTo { get; set; }
        public bool IsActive { get; set; }
        public bool IncludeEmployees { get; set; }
        public int SortOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRW_EmpEntityParamValues> HRW_EmpEntityParamValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRW_EmpEntityParamValues> HRW_EmpEntityParamValues1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORG_EmpEntityLink> ORG_EmpEntityLink { get; set; }
        public virtual ORG_EntityType ORG_EntityType { get; set; }
    }
}
