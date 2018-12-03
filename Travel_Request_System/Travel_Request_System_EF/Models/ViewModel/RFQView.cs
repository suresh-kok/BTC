using System;
using System.Collections.Generic;

namespace Travel_Request_System_EF.Models.ViewModel
{
    public class RFQView
    {
        public int RFQID { get; set; }
        public int TravelRequestID { get; set; }
        public string ApplicationNumber { get; set; }
        public TravelAgency travelAgency { get; set; }
        public TravelRequests travelRequest { get; set; }
        public HRW_Employee employee { get; set; }
        public string Remarks { get; set; }
        public List<string> RFQAttachment { get; set; }

        //Employee
        public int UserID { get; set; }
        public string EmployeeID { get; set; }
        public string BTCEmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string CostCenter { get; set; }
        public string PassportNumber { get; set; }
        public string PassportExpiry { get; set; }

        //AT
        public string TicketClass { get; set; }
        public Nullable<int> PortOfOriginID { get; set; }
        public Nullable<int> PortOfDestinationID { get; set; }
        public Nullable<DateTime> DepartureDate { get; set; }
        public Nullable<TimeSpan> DepartureTime { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
        public Nullable<TimeSpan> ReturnTime { get; set; }
        public string Airlines { get; set; }

        //HS
        public string TravelSectorHS { get; set; }
        public string HotelName { get; set; }
        public string HotelCategory { get; set; }
        public string RoomCategory { get; set; }
        public string RoomType { get; set; }
        public Nullable<DateTime> CheckInDate { get; set; }
        public Nullable<DateTime> CheckOutDate { get; set; }
        public Nullable<TimeSpan> CheckInTime { get; set; }
        public Nullable<TimeSpan> CheckOutTime { get; set; }

        //PC
        public string TravelSectorPC { get; set; }
        public string PickUpLocation { get; set; }
        public Nullable<DateTime> PickUpDate { get; set; }
        public Nullable<TimeSpan> PickUpTime { get; set; }
        public string DropOffLocation { get; set; }
        public Nullable<DateTime> DropOffDate { get; set; }
        public Nullable<TimeSpan> DropOffTime { get; set; }
        public string PreferredVehicle { get; set; }

    }
}