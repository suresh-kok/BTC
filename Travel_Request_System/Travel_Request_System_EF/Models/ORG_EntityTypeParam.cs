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

    public partial class ORG_EntityTypeParam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORG_EntityTypeParam()
        {
            this.HRW_EmpEntityParamValues = new HashSet<HRW_EmpEntityParamValues>();
            this.HRW_EntityParamValues = new HashSet<HRW_EntityParamValues>();
            this.ORG_EntityMasterParamValues = new HashSet<ORG_EntityMasterParamValues>();
        }
    
        public int TransactType { get; set; }
        public long TransactUserID { get; set; }
        public System.DateTime TransactDateTime { get; set; }
        public string MenuID { get; set; }
        [Key]
        public long EntityTypeParamID { get; set; }
        public long EntityTypeId { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public Nullable<long> DataTypeEntityID { get; set; }
        public int SortOrder { get; set; }
        public string ParamLinkLevel { get; set; }
        public bool IsMandatory { get; set; }
        public Nullable<int> CheckDuplicate { get; set; }
        public string Validations { get; set; }
        public string ESSFieldAccessRight { get; set; }
        public string DataTypeProperty { get; set; }
        public string DataTypeProperty1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRW_EmpEntityParamValues> HRW_EmpEntityParamValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRW_EntityParamValues> HRW_EntityParamValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORG_EntityMasterParamValues> ORG_EntityMasterParamValues { get; set; }
        public virtual ORG_EntityType ORG_EntityType { get; set; }
    }
}