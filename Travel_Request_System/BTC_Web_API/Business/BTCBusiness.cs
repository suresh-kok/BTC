using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using BTC_Web_API.Models;

namespace BTC_Web_API.Business
{
    public class BTCBusiness : IBTCBusiness
    {
        private readonly Employee_Mapper EmployeeDB;
        private readonly EmployeeDetail_Mapper EmployeeDetailDB;
        private readonly EmpOrgDetails_Mapper EmpOrgDetailsDB;
        private readonly Payroll_Mapper PayrollDB;
        private readonly EntityMaster_Mapper EntityMasterDB;
        private readonly TravelRequest_Mapper TravelRequestDB;
        private readonly Quote_Mapper QuoteDB;
        private readonly LPO_Mapper LPODB;
        private readonly Notification_Mapper NotificationDB;
        private readonly TravelAgency_Mapper TravelAgencyDB;
        private readonly Destinations_Mapper DestinationsDB;
        private readonly HotelCategory_Mapper HotelCategoryDB;
        private readonly RoomType_Mapper RoomTypeDB;
        private Exception CustomException;

        public BTCBusiness()
        {
            EmployeeDB = new Employee_Mapper();
            EmployeeDetailDB = new EmployeeDetail_Mapper();
            EmpOrgDetailsDB = new EmpOrgDetails_Mapper();
            PayrollDB = new Payroll_Mapper();
            EntityMasterDB = new EntityMaster_Mapper();
            TravelRequestDB = new TravelRequest_Mapper();
            QuoteDB = new Quote_Mapper();
            LPODB = new LPO_Mapper();
            NotificationDB = new Notification_Mapper();
            TravelRequestDB = new TravelRequest_Mapper();

            CustomException = new Exception();
        }

        public bool ApproveRequest(string RequestID, int ApproverID, bool IsApproved)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int EmpID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLPO(int LPOID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNotification(int NotificationID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteQuote(int QuoteID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRequest(int TravelRequestID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTravelAgency(int TravelAgencyID)
        {
            throw new NotImplementedException();
        }

        public bool ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }

        public void GenerateReport(int EmpID, int QuoteID, int LPOID, int RequestID)
        {
            throw new NotImplementedException();
        }

        public List<Destinations> GetDestinations()
        {
            throw new NotImplementedException();
        }

        public List<Destinations> GetDestinations(string ForJobGrade)
        {
            return DestinationsDB.SelectFor(ForJobGrade, out CustomException);
        }

        public HRW_Employee GetEmployee(int EmpID)
        {
            throw new NotImplementedException();
        }

        public List<HRW_Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public List<HotelCategory> GetHotelCategory()
        {
            throw new NotImplementedException();
        }

        public List<HotelCategory> GetHotelCategory(string ForJobGrade)
        {
            throw new NotImplementedException();
        }

        public ORG_LPO GetLPO(int LPOID)
        {
            throw new NotImplementedException();
        }

        public List<ORG_LPO> GetLPOs()
        {
            throw new NotImplementedException();
        }

        public HRW_Notification GetNotification(int NotificationID)
        {
            throw new NotImplementedException();
        }

        public List<HRW_Notification> GetNotifications()
        {
            throw new NotImplementedException();
        }

        public ORG_Quote GetQuote(int QuoteID)
        {
            throw new NotImplementedException();
        }

        public List<ORG_Quote> GetQuotes()
        {
            throw new NotImplementedException();
        }

        public List<RoomType> GetRoomType()
        {
            throw new NotImplementedException();
        }

        public List<RoomType> GetRoomType(string ForJobGrade)
        {
            throw new NotImplementedException();
        }

        public ORG_TravelAgency GetTravelAgency(int TravelAgencyID)
        {
            throw new NotImplementedException();
        }

        public List<ORG_TravelAgency> GetTravelAgencys()
        {
            throw new NotImplementedException();
        }

        public ORG_TravelRequest GetTravelRequest(int TravelRequestID)
        {
            throw new NotImplementedException();
        }

        public List<ORG_TravelRequest> GetTravelRequests()
        {
            throw new NotImplementedException();
        }

        public bool HRApproval(int RequestID)
        {
            throw new NotImplementedException();
        }

        public bool IsJobGradeValid(string JobGrade)
        {
            throw new NotImplementedException();
        }

        public HRW_Employee LoginCheck(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public HRW_Employee SaveEmployee(HRW_Employee EmpData)
        {
            if (!EmployeeDB.UserCheck(EmpData.Email, out CustomException))
            {
                if (EmpData.CustomerID > 0)
                {
                    return EmployeeDB.Update(EmpData, out CustomException);
                }
                else
                {
                    HRW_Employee Cust = EmployeeDB.Create(EmpData, out CustomException);
                    SendMail(EmpData.Email, "Please click the below link to Activate User. <br/>" + ConfigurationManager.AppSettings["WebLink"] + "/api/Customer/ActivateCustomer/" + Cust.CustomerID, "User Activation");
                    return Cust;
                }
            }
            else
            {
                throw new Exception("User with same Email existing already");
            }
        }

        public ORG_LPO SaveLPO(ORG_LPO LPOData)
        {
            throw new NotImplementedException();
        }

        public HRW_Notification SaveNotification(HRW_Notification NotificationData)
        {
            throw new NotImplementedException();
        }

        public ORG_Quote SaveQuote(ORG_Quote QuoteData)
        {
            throw new NotImplementedException();
        }

        public ORG_TravelRequest SaveRequest(ORG_TravelRequest TravelRequestData)
        {
            throw new NotImplementedException();
        }

        public ORG_TravelAgency SaveTravelAgency(ORG_TravelAgency TravelAgencyData)
        {
            throw new NotImplementedException();
        }

        public bool SendEmail(ORG_Quote QuoteData, string to, string subject, params MailAttachment[] attachments)
        {
            throw new NotImplementedException();
        }

        public void SendMail(string to, string body, string subject, params MailAttachment[] attachments)
        {
            throw new NotImplementedException();
        }

        public bool SetUserActive(int EmpID)
        {
            throw new NotImplementedException();
        }

        public bool UserCheck(string Email)
        {
            throw new NotImplementedException();
        }

    }

    public class MailAttachment
    {
        #region Fields
        private readonly MemoryStream stream;
        private readonly string filename;
        private readonly string mediaType;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the data stream for this attachment
        /// </summary>
        public Stream Data { get { return stream; } }
        /// <summary>
        /// Gets the original filename for this attachment
        /// </summary>
        public string Filename { get { return filename; } }
        /// <summary>
        /// Gets the attachment type: Bytes or String
        /// </summary>
        public string MediaType { get { return mediaType; } }
        /// <summary>
        /// Gets the file for this attachment (as a new attachment)
        /// </summary>
        public Attachment File { get { return new Attachment(Data, Filename, MediaType); } }
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a mail attachment form a byte array
        /// </summary>
        /// <param name="data">Bytes to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(byte[] data, string filename)
        {
            stream = new MemoryStream(data);
            this.filename = filename;
            mediaType = System.Net.Mime.MediaTypeNames.Application.Octet;
        }
        /// <summary>
        /// Construct a mail attachment from a string
        /// </summary>
        /// <param name="data">String to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(string data, string filename)
        {
            stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data));
            this.filename = filename;
            mediaType = System.Net.Mime.MediaTypeNames.Text.Html;
        }
        #endregion
    }
}