using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class HSQuotationMetaData
    {
        [DisplayName("HS Quotation ID")]
        [Required(ErrorMessage = "HS Quotation ID is Required")]
        public int ID { get; set; }

        [DisplayName("Quotation ID")]
        [Required(ErrorMessage = "Quotation ID is Required")]
        public int QuotationID { get; set; }

        [DisplayName("Travel Sector")]
        public string TravelSector { get; set; }

        [DisplayName("Hotel Name")]
        [Required(ErrorMessage = "Hotel Name is Required")]
        public string HotelName { get; set; }

        [DisplayName("Hotel Category")]
        public string HotelCategory { get; set; }

        [DisplayName("Room Category")]
        public string RoomCategory { get; set; }

        [DisplayName("Room Type")]
        [Required(ErrorMessage = "Destination is Required")]
        public string RoomType { get; set; }

        [DisplayName("Check In Date")]
        [Required(ErrorMessage = "Check In Date is Required")]
        public Nullable<System.DateTime> CheckInDate { get; set; }

        [DisplayName("Check In Time")]
        public Nullable<System.TimeSpan> CheckInTime { get; set; }

        [DisplayName("Check Out Date")]
        [Required(ErrorMessage = "Check Out Date is Required")]
        [IsDateAfterAttribute("CheckInDate", true, ErrorMessage = "Check Out Date Cannot be less than Check In Date")]
        public Nullable<System.DateTime> CheckOutDate { get; set; }

        [DisplayName("Check Out Time")]
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }

        [DisplayName("Additional Expenses")]
        [Required(ErrorMessage = "Additional Expenses is Required")]
        [Range(1, 99999999, ErrorMessage = "Additional Expenses is Invalid")]
        public Nullable<decimal> Amount { get; set; }

        [DisplayName("Is Deleted")]
        public Nullable<bool> IsDeleted { get; set; }

        [DisplayName("Quotation Name")]
        public string QuotationName { get; set; }

        [DisplayName("Is Active")]
        public Nullable<bool> IsActive { get; set; }
    }
}