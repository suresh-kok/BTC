using System.Collections.Generic;
using BTC_Web_API.Business;
using BTC_Web_API.Models;

namespace BTC_Web_API
{
    internal interface IBTCBusiness
    {
        bool UserCheck(string Email);
        HRW_Employee LoginCheck(string Email, string Password);
        bool SetUserActive(int EmpID);
        bool ForgotPassword(string Email);
        bool ChangePassword(string Email, string Password);

        HRW_Employee SaveEmployee(HRW_Employee EmpData);
        void DeleteEmployee(int EmpID);
        HRW_Employee GetEmployee(int EmpID);
        List<HRW_Employee> GetEmployees();

        ORG_TravelRequest SaveRequest(ORG_TravelRequest TravelRequestData);
        bool DeleteRequest(int TravelRequestID);
        ORG_TravelRequest GetTravelRequest(int TravelRequestID);
        List<ORG_TravelRequest> GetTravelRequests();

        bool IsJobGradeValid(string JobGrade);
        bool ApproveRequest(string RequestID, int ApproverID, bool IsApproved);
        bool HRApproval(int RequestID);

        ORG_Quote SaveQuote(ORG_Quote QuoteData);
        bool DeleteQuote(int QuoteID);
        ORG_Quote GetQuote(int QuoteID);
        List<ORG_Quote> GetQuotes();

        bool SendEmail(ORG_Quote QuoteData, string to, string subject, params MailAttachment[] attachments);

        ORG_LPO SaveLPO(ORG_LPO LPOData);
        bool DeleteLPO(int LPOID);
        ORG_LPO GetLPO(int LPOID);
        List<ORG_LPO> GetLPOs();

        ORG_TravelAgency SaveTravelAgency(ORG_TravelAgency TravelAgencyData);
        bool DeleteTravelAgency(int TravelAgencyID);
        ORG_TravelAgency GetTravelAgency(int TravelAgencyID);
        List<ORG_TravelAgency> GetTravelAgencys();

        HRW_Notification SaveNotification(HRW_Notification NotificationData);
        bool DeleteNotification(int NotificationID);
        HRW_Notification GetNotification(int NotificationID);
        List<HRW_Notification> GetNotifications();

        List<Destinations> GetDestinations();
        List<Destinations> GetDestinations(string ForJobGrade);
        List<HotelCategory> GetHotelCategory();
        List<HotelCategory> GetHotelCategory(string ForJobGrade);
        List<RoomType> GetRoomType();
        List<RoomType> GetRoomType(string ForJobGrade);

        void SendMail(string to, string body, string subject, params MailAttachment[] attachments);

        void GenerateReport(int EmpID, int QuoteID, int LPOID, int RequestID);

    }
}
