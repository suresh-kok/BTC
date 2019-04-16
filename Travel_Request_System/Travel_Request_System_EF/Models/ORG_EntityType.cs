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
    
    public partial class ORG_EntityType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORG_EntityType()
        {
            this.ORG_EntityMaster = new HashSet<ORG_EntityMaster>();
            this.ORG_EntityTypeLevel = new HashSet<ORG_EntityTypeLevel>();
            this.ORG_EntityTypeParam = new HashSet<ORG_EntityTypeParam>();
        }
    
        public int TransactType { get; set; }
        public long TransactUserID { get; set; }
        public System.DateTime TransactDateTime { get; set; }
        public string MenuID { get; set; }
        public long EntityTypeId { get; set; }
        public long OrgChartId { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsCodeRequired { get; set; }
        public int SortOrder { get; set; }
        public int EntityGroup { get; set; }
        public Nullable<long> MasterEntityTypeId { get; set; }
        public bool HaveDataSecurity { get; set; }
        public bool ShowInReportFilter { get; set; }
        public bool RestrictDuplicate { get; set; }
    
        public virtual ORG_ChartMaster ORG_ChartMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORG_EntityMaster> ORG_EntityMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORG_EntityTypeLevel> ORG_EntityTypeLevel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORG_EntityTypeParam> ORG_EntityTypeParam { get; set; }
    }
}
