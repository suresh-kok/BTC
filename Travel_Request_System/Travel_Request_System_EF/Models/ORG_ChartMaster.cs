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

    public partial class ORG_ChartMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORG_ChartMaster()
        {
            this.ORG_EntityType = new HashSet<ORG_EntityType>();
        }
    
        public int TransactType { get; set; }
        public long TransactUserID { get; set; }
        public System.DateTime TransactDateTime { get; set; }
        public string MenuID { get; set; }
        [Key]
        public long OrgChartId { get; set; }
        public string CompanyCode { get; set; }
        public string Description { get; set; }
        public string ChartRecordType { get; set; }
        public Nullable<long> ManagerEmployeeID { get; set; }
        public Nullable<bool> IsChartDataReady { get; set; }
        public Nullable<long> OrgChartBy { get; set; }
    
        public virtual HRW_Company HRW_Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORG_EntityType> ORG_EntityType { get; set; }
    }
}
