using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class PCQuotationMetaData
    {
        [DisplayName("PC Quotation ID")]
        [Required(ErrorMessage = "PC Quotation ID is Required")]
        public int ID { get; set; }

        [DisplayName("Quotation ID")]
        [Required(ErrorMessage = "Quotation ID is Required")]
        public int QuotationID { get; set; }

        [DisplayName("Travel Sector")]
        public string TravelSector { get; set; }

        [DisplayName("Pick Up Location")]
        [Required(ErrorMessage = "Pick Up Location is Required")]
        public string PickupLocation { get; set; }

        [DisplayName("Drop Off Location")]
        [Required(ErrorMessage = "Drop Off Location is Required")]
        public string DropoffLocation { get; set; }

        [DisplayName("Preferred Vehicle")]
        public string PreferredVehicle { get; set; }

        [DisplayName("Pick Up Date")]
        [Required(ErrorMessage = "Pick Up Date is Required")]
        public Nullable<System.DateTime> PickUpDate { get; set; }

        [DisplayName("Pick Up Time")]
        public Nullable<System.TimeSpan> PickUpTime { get; set; }

        [DisplayName("Drop Off Date")]
        [Required(ErrorMessage = "Drop Off Date is Required")]
        [IsDateAfterAttribute("PickUpDate", true, ErrorMessage = "Drop OFf Date Cannot be less than Pick Up Date")]
        public Nullable<System.DateTime> DropOffDate { get; set; }

        [DisplayName("Drop Off Time")]
        public Nullable<System.TimeSpan> DropOffTime { get; set; }

        [DisplayName("Amount")]
        [Required(ErrorMessage = "Amount is Required")]
        [Range(1, 99999999, ErrorMessage = "Amount is Invalid")]
        public Nullable<decimal> Amount { get; set; }

        [DisplayName("Is Deleted")]
        public Nullable<bool> IsDeleted { get; set; }

        [DisplayName("Quotation Name")]
        public string QuotationName { get; set; }

        [DisplayName("Is Active")]
        public Nullable<bool> IsActive { get; set; }
    }
}