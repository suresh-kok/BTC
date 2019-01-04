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
    
    public partial class RFQDetails
    {
        public int RFQID { get; set; }
        public string Processing { get; set; }
        public string ProcessingSection { get; set; }
        public string RFQRemarks { get; set; }
        public string AgencyCode { get; set; }
        public string username { get; set; }
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
        public Nullable<System.DateTime> DepartureDate { get; set; }
        public Nullable<System.TimeSpan> DepartureTime { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<System.TimeSpan> ReturnTime { get; set; }
        public Nullable<System.DateTime> FirstBusinessDay { get; set; }
        public Nullable<System.DateTime> LastBusinessDay { get; set; }
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
        public Nullable<System.DateTime> PickUpDate { get; set; }
        public Nullable<System.TimeSpan> PickUpTime { get; set; }
        public string DropOffLocation { get; set; }
        public Nullable<System.DateTime> DropOffDate { get; set; }
        public Nullable<System.TimeSpan> DropOffTime { get; set; }
        public string PreferredVehicle { get; set; }
        public string TravelSector { get; set; }
        public Nullable<System.DateTime> CheckInDate { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
        public Nullable<System.TimeSpan> CheckInTime { get; set; }
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }
        public int ApprovalLevel { get; set; }
        public Nullable<int> ApprovalBy { get; set; }
        public string ApprovedByName { get; set; }
        public string ApprovalRemarks { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
