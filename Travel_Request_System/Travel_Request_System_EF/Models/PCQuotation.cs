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
    
    public partial class PCQuotation
    {
        public int ID { get; set; }
        public int QuotationID { get; set; }
        public string TravelSector { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string PreferredVehicle { get; set; }
        public Nullable<System.DateTime> PickUpDate { get; set; }
        public Nullable<System.TimeSpan> PickUpTime { get; set; }
        public Nullable<System.DateTime> DropOffDate { get; set; }
        public Nullable<System.TimeSpan> DropOffTime { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string QuotationName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual Quotation Quotation { get; set; }
    }
}
