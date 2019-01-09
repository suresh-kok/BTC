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
    
    public partial class TravelRequestDetails
    {
        public int ID { get; set; }
        public string ApplicationNumber { get; set; }
        public Nullable<int> PortOfOriginID { get; set; }
        public string OriginCIty { get; set; }
        public string OriginCountry { get; set; }
        public Nullable<int> PortOfDestinationID { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationCountry { get; set; }
        public string TicketClass { get; set; }
        public Nullable<decimal> DailyAllowance { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public string CurrencyDesc { get; set; }
        public string CurrencySymbol { get; set; }
        public Nullable<int> TravelDays { get; set; }
        public string TravelRemarks { get; set; }
        public string PurposeOfVisit { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string ReturnDate { get; set; }
        public string ReturnTime { get; set; }
        public string FirstBusinessDay { get; set; }
        public string LastBusinessDay { get; set; }
        public string Remarks { get; set; }
        public string AirTicketManagement { get; set; }
        public string HotelName { get; set; }
        public string TravelAllowance { get; set; }
        public string HotelStay { get; set; }
        public string HotelCategory { get; set; }
        public string RoomCategory { get; set; }
        public string RoomType { get; set; }
        public Nullable<decimal> AdditionalAllowance { get; set; }
        public string AirportPickUp { get; set; }
        public string PickUpLocation { get; set; }
        public string PickUpDate { get; set; }
        public string PickUpTime { get; set; }
        public string DropOffLocation { get; set; }
        public string DropOffDate { get; set; }
        public string DropOffTime { get; set; }
        public string PreferredVehicle { get; set; }
        public string TravelSector { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public int ApprovalLevel { get; set; }
        public Nullable<int> ApprovalBy { get; set; }
        public string ApprovedByName { get; set; }
        public string ApprovalRemarks { get; set; }
        public string CreateOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public bool IsSubmitted { get; set; }
    }
}