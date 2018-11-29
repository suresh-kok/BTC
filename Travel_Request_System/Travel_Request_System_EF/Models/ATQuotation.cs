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
    
    public partial class ATQuotation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ATQuotation()
        {
            this.LPOes = new HashSet<LPO>();
        }
    
        public int ATQuotationID { get; set; }
        public int QuotationID { get; set; }
        public string TicketClass { get; set; }
        public Nullable<int> OriginID { get; set; }
        public Nullable<int> DestinationID { get; set; }
        public Nullable<System.DateTime> DepartureDate { get; set; }
        public Nullable<System.TimeSpan> DepartureTime { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<System.TimeSpan> ReturnTime { get; set; }
        public string Airlines { get; set; }
        public string TicketNo { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual City DestinationCity { get; set; }
        public virtual City OriginCity { get; set; }
        public virtual Quotation Quotation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LPO> LPOes { get; set; }
    }
}
