using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class ATQuotationMetaData
    {
        [DisplayName("AT Quotation ID")]
        [Required(ErrorMessage = "AT Quotation ID is Required")]
        public int ID { get; set; }

        [DisplayName("Quotation ID")]
        [Required(ErrorMessage = "Quotation ID is Required")]
        public int QuotationID { get; set; }

        [DisplayName("Ticket Class")]
        [Required(ErrorMessage = "Ticket Class is Required")]
        public string TicketClass { get; set; }

        [DisplayName("Origin")]
        [Required(ErrorMessage = "Origin is Required")]
        public Nullable<int> OriginID { get; set; }

        [DisplayName("Destination")]
        [Required(ErrorMessage = "Destination is Required")]
        public Nullable<int> DestinationID { get; set; }

        [DisplayName("Departure Date")]
        [Required(ErrorMessage = "Departure Date is Required")]
        public Nullable<System.DateTime> DepartureDate { get; set; }

        [DisplayName("Departure Time")]
        public Nullable<System.TimeSpan> DepartureTime { get; set; }

        [DisplayName("Return Date")]
        [Required(ErrorMessage = "Return Date is Required")]
        [IsDateAfterAttribute("DepartureDate", true, ErrorMessage = "Return Date Cannot be less than Departure Date")]
        public Nullable<System.DateTime> ReturnDate { get; set; }

        [DisplayName("Return Time")]
        public Nullable<System.TimeSpan> ReturnTime { get; set; }

        [DisplayName("Airlines")]
        public string Airlines { get; set; }

        [DisplayName("Ticket No")]
        [Required(ErrorMessage = "Ticket No is Required")]
        public string TicketNo { get; set; }

        [DisplayName("Amount")]
        [Required(ErrorMessage = "Amount is Required")]
        public Nullable<decimal> Amount { get; set; }

        [DisplayName("Is Deleted")]
        public Nullable<bool> IsDeleted { get; set; }
    }
}