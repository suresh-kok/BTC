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
    
    public partial class HSQuotation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HSQuotation()
        {
            this.LPOes = new HashSet<LPO>();
        }
    
        public int HSQuotationID { get; set; }
        public int QuotationID { get; set; }
        public string TravelSector { get; set; }
        public string HotelName { get; set; }
        public string HotelCategory { get; set; }
        public string RoomCategory { get; set; }
        public string RoomType { get; set; }
        public Nullable<System.DateTime> CheckInDate { get; set; }
        public Nullable<System.TimeSpan> CheckInTime { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual Quotation Quotation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LPO> LPOes { get; set; }
    }
}
