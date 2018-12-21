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
    
    public partial class RFQ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RFQ()
        {
            this.LPO = new HashSet<LPO>();
            this.Quotation = new HashSet<Quotation>();
        }
    
        public int ID { get; set; }
        public Nullable<int> TravelAgencyID { get; set; }
        public Nullable<int> TravelRequestID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Remarks { get; set; }
        public int Processing { get; set; }
        public int ProcessingSection { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string RFQName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LPO> LPO { get; set; }
        public virtual TravelAgency TravelAgency { get; set; }
        public virtual TravelRequests TravelRequests { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quotation> Quotation { get; set; }
    }
}
