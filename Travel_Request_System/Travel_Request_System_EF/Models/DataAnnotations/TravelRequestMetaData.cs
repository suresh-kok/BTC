using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class TravelRequestMetaData
    {
        [DisplayName("Travel Request ID")]
        [Required(ErrorMessage = "Travel Request ID is Required")]
        public int ID { get; set; }

        [DisplayName("Application Number")]
        [Required(ErrorMessage = "Application Number is Required")]
        public string ApplicationNumber { get; set; }

        [DisplayName("Port Of Origin")]
        [Required(ErrorMessage = "Origin Port is Required")]
        public Nullable<int> PortOfOriginID { get; set; }

        [DisplayName("Port Of Destination")]
        [Required(ErrorMessage = "Destination Port is Required")]
        public Nullable<int> PortOfDestinationID { get; set; }

        [DisplayName("Ticket Class")]
        [Required(ErrorMessage = "Ticket Class is Required")]
        [MaxLength(100)]
        public string TicketClass { get; set; }

        [DisplayName("Daily Allowance")]
        public Nullable<decimal> DailyAllowance { get; set; }

        [DisplayName("Currency")]
        [Required(ErrorMessage = "Currency is Required")]
        public Nullable<int> CurrencyID { get; set; }

        [DisplayName("Travel Days")]
        [Required(ErrorMessage = "Travel Days is Required")]
        [Range(1, 365, ErrorMessage = "Travel Days should be greater than or equal to 1")]
        public Nullable<int> TravelDays { get; set; }

        [DisplayName("Travel Remarks")]
        [MaxLength(500)]
        public string TravelRemarks { get; set; }

        [DisplayName("Travel Sector")]
        [MaxLength(500)]
        public string TravelSector { get; set; }

        [DisplayName("Purpose Of Visit")]
        [Required(ErrorMessage = "Purpose Of Visit is Required")]
        [MaxLength(5000)]
        public string PurposeOfVisit { get; set; }

        [DisplayName("Departure Date")]
        [Required(ErrorMessage = "Departure Date is Required")]
        [CheckDateRange]
        public Nullable<DateTime> DepartureDate { get; set; }

        [DisplayName("Departure Time")]
        public Nullable<TimeSpan> DepartureTime { get; set; }

        [DisplayName("Return Date")]
        [Required(ErrorMessage = "Return Date is Required")]
        [IsDateAfterAttribute("DepartureDate", true, ErrorMessage = "Return Date Cannot be less than Departure Date")]
        public Nullable<DateTime> ReturnDate { get; set; }

        [DisplayName("Return Time")]
        public Nullable<TimeSpan> ReturnTime { get; set; }

        [DisplayName("First Business Day")]
        public Nullable<DateTime> FirstBusinessDay { get; set; }

        [DisplayName("Last Business Day")]
        public Nullable<DateTime> LastBusinessDay { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("Air Ticket Management")]
        public string AirTicketManagement { get; set; }

        [DisplayName("Hotel Name")]
        public string HotelName { get; set; }

        [DisplayName("Travel Allowance")]
        public string TravelAllowance { get; set; }

        [DisplayName("Hotel Stay")]
        public string HotelStay { get; set; }

        [DisplayName("Hotel Category")]
        public string HotelCategory { get; set; }

        [DisplayName("Room Category")]
        public string RoomCategory { get; set; }

        [DisplayName("Room Type")]
        public string RoomType { get; set; }

        [DisplayName("Additional Allowance")]
        public Nullable<decimal> AdditionalAllowance { get; set; }

        [DisplayName("Airport PickUp")]
        [Range(0, 1, ErrorMessage = "Airport Pick Up field cannot be blank")]
        public string AirportPickUp { get; set; }

        [DisplayName("PickUp Location")]
        public string PickUpLocation { get; set; }

        [DisplayName("PickUp Date")]
        [RequiredIf("AirportPickUp", "0", ErrorMessage = "Pick Up Date is Required")]
        [CheckDateRange]
        public Nullable<DateTime> PickUpDate { get; set; }

        [DisplayName("PickUp Time")]
        public Nullable<TimeSpan> PickUpTime { get; set; }

        [DisplayName("DropOff Location")]
        public string DropOffLocation { get; set; }

        [DisplayName("DropOff Date")]
        [RequiredIf("AirportPickUp", "0", ErrorMessage = "Drop Off Date is Required")]
        [IsDateAfter("PickUpDate", true, ErrorMessage = "Drop Off Date Cannot be less than Pick Up Date")]
        public Nullable<DateTime> DropOffDate { get; set; }

        [DisplayName("DropOff Time")]
        public Nullable<TimeSpan> DropOffTime { get; set; }

        [DisplayName("Preferred Vehicle")]
        public string PreferredVehicle { get; set; }

        [DisplayName("Check-In Date")]
        [CheckDateRange]
        public Nullable<DateTime> CheckInDate { get; set; }

        [DisplayName("Check-Out Date")]
        [IsDateAfter("CheckInDate", true, ErrorMessage = "Check-Out Date Cannot be less than Check-In Date")]
        public Nullable<DateTime> CheckOutDate { get; set; }

        [DisplayName("Check-In Time")]
        public Nullable<TimeSpan> CheckInTime { get; set; }

        [DisplayName("Check-Out Time")]
        public Nullable<TimeSpan> CheckOutTime { get; set; }

        [DisplayName("Approval Level")]
        public int ApprovalLevel { get; set; }

        [DisplayName("Approval By")]
        public Nullable<int> ApprovalBy { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Approval Remarks")]
        public string ApprovalRemarks { get; set; }

        [DisplayName("Created On")]
        public Nullable<DateTime> CreateOn { get; set; }

        [DisplayName("Created By")]
        public Nullable<int> CreatedBy { get; set; }

        [DisplayName("Modified On")]
        public Nullable<DateTime> ModifiedOn { get; set; }

        [DisplayName("Modified By")]
        public Nullable<int> ModifiedBy { get; set; }

        [DisplayName("Is Deleted")]
        public Nullable<bool> IsDeleted { get; set; }

        [DisplayName("Is Submitted")]
        public bool IsSubmitted { get; set; }
    }
}