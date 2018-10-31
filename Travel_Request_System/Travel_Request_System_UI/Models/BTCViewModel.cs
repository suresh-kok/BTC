using System;
using System.Collections.Generic;

namespace Travel_Request_System.Models
{
    public class Employee
    {
        public string EmployeeID;
        public string BTCEmployeeCode;
        public string EmployeeName;
        public string EmployeePositionCode;
        public string Department;
        public string Designation;
        public string CostCenter;
        public string CostCenterHead;
        public string PassportNumber;
        public string PassportExpiry;
        public string QatarID;
        public string QatarExpiry;
        public string Location;
        public string Section;
        public string ContactDetails;
    }

    public class TravelRequest
    {
        public Employee employee;
        public List<TravelDetails> travelDetails = new List<TravelDetails>();
        public DateTime DepartureDate;
        public DateTime ReturnDate;
        public DateTime DepartureTime;
        public DateTime ReturnTime;
        public DateTime FirstDayOfBusiness;
        public DateTime LastDayOfBusiness;
        public string AirTicketArrangement;
        public string HotelName;
        public string TravelAllowance;
        public DateTime CheckInDate;
        public DateTime CheckInTime;
        public DateTime CheckOutDate;
        public DateTime CheckOutTime;
        public string HotelStay;
        public string HotelCategory;
        public string RoomCategory;
        public string RoomType;
        public string AirportPickUp;
        public string AdditionalExpenses;
        public string PickUpBy;
    }

    public class TravelDetails
    {
        public string OriginPort;
        public string DesitnationPort;
        public string TicketClass;
        public string DailyAllowance;
        public string Currency;
        public string TravelDays;
        public string Remarks;
        public string PurposeOfVisit;
    }

    public class RequestForQuote { }

    public class LPO { }

    public class TravelAgency { }

    public class ApproveObject { }

    public class UserProfile
    {
        public string UserID;
        public string UserName;
        public string Password;
        public string IsActive;
    }


}
