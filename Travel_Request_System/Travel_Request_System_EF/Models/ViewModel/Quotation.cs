﻿namespace Travel_Request_System_EF.Models.ViewModel
{
    //public class Quotation
    //{
    //    public int QuotationID { get; set; }
    //    public int TravelRequestID { get; set; }
    //    public string ApplicationNumber { get; set; }
    //    public TravelAgency travelAgency { get; set; }
    //    public TravelRequests travelRequest { get; set; }
    //    public HRW_Employee employee { get; set; }
    //    public string Remarks { get; set; }
    //    public List<string> RFQAttachment { get; set; }
    //    public bool IsDeleted { get; set; }

    //    //Employee
    //    public int UserID { get; set; }
    //    public string EmployeeID { get; set; }
    //    public string BTCEmployeeCode { get; set; }
    //    public string EmployeeName { get; set; }
    //    public string Designation { get; set; }
    //    public string CostCenter { get; set; }
    //    public string PassportNumber { get; set; }
    //    public string PassportExpiry { get; set; }
    //}

    public class QuotationListViewModel
    {
        public int QuotationID { get; set; }
        public int RFQID { get; set; }
        public int TravelRequestID { get; set; }
        public int QuotationType { get; set; }
        public int QuotationTypeID { get; set; }
        public string QuotationName { get; set; }
    }
}