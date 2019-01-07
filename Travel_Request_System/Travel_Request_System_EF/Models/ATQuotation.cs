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
    using Travel_Request_System_EF.Models.DataAnnotations;

    [MetadataType(typeof(ATQuotationMetaData))]
    public partial class ATQuotation
    {
        public int ID { get; set; }
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
        public string QuotationName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual City City { get; set; }
        public virtual City City1 { get; set; }
        public virtual Quotation Quotation { get; set; }
    }
}
