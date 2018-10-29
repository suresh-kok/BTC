using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using BTC_Web_API.Business;
using BTC_Web_API.Models;
using Newtonsoft.Json;

namespace BTC_Web_API.Controllers
{
    [RoutePrefix("api/Travel")]
    public class TravelController : ApiController
    {
        private IBTCBusiness BusinessObj = new BTCBusiness();
        IHttpActionResult httpActionResult = (IHttpActionResult)new HttpResponseMessage();
        [Route("GetEmployee")]
        public string GetEmployee()
        {
            var json = JsonConvert.SerializeObject(BusinessObj.GetEmployees(), Formatting.Indented,
                       new JsonSerializerSettings
                       {
                           DateFormatHandling = DateFormatHandling.IsoDateFormat
                       });
            return json;
        }

        [Route("GetEmployee/{EmpId}")]
        public string GetEmployee(int EmpId)
        {
            var json = JsonConvert.SerializeObject(BusinessObj.GetEmployee(EmpId), Formatting.Indented,
              new JsonSerializerSettings
              {
                  DateFormatHandling = DateFormatHandling.IsoDateFormat
              });
            return json;
        }

        [Route("DeleteEmployee/{EmpId}")]
        public void DeleteEmployee(int EmpId)
        {
            BusinessObj.DeleteEmployee(EmpId);
        }

        [HttpGet]
        [Route("CheckLogin/{Email}/{Password}")]
        public string CheckLogin(string Email, string Password)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.LoginCheck(Email, Password));
            return json;
        }

        [HttpGet]
        [Route("ForgotPassowrd")]
        public void ForgotPassowrd([FromBody]string Email)
        {
            BusinessObj.ForgotPassword(Email);
        }

        [HttpGet]
        [Route("ChangePassword")]
        public void ChangePassword([FromBody]string Email, [FromBody]string Password)
        {
            BusinessObj.ChangePassword(Email, Password);
        }

        [HttpGet]
        [Route("ActivateEmployee/{EmpId}")]
        public string ActivateEmployee(int EmpId)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SetUserActive(EmpId));
            return json;
        }

        [Route("UserCheck")]
        public string UserCheck(string Email)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.UserCheck(Email));
            return json;
        }

        [Route("LoginCheck")]
        public string LoginCheck(string Email, string Password)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.LoginCheck(Email, Password));
            return json;
        }

        [Route("SetUserActive")]
        public string SetUserActive(int EmpID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SetUserActive(EmpID));
            return json;
        }

        [Route("ForgotPassword")]
        public string ForgotPassword(string Email)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.ForgotPassword(Email));
            return json;
        }

        [Route("SaveEmployee")]
        public IHttpActionResult SaveEmployee(HttpRequestMessage obj)
        {
            var value = obj.Content.ReadAsStringAsync().Result;
            var EmpData = new JavaScriptSerializer().Deserialize(value, typeof(HRW_Employee));
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SaveEmployee((HRW_Employee)EmpData));
            return httpActionResult;
        }

        [Route("GetEmployees")]
        public string GetEmployees()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetEmployees());
            return json;
        }

        [Route("SaveRequest")]
        public IHttpActionResult SaveRequest(HttpRequestMessage obj)
        {
            var value = obj.Content.ReadAsStringAsync().Result;
            var TravelRequestData = new JavaScriptSerializer().Deserialize(value, typeof(ORG_TravelRequest));
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SaveRequest((ORG_TravelRequest)TravelRequestData));
            return httpActionResult;
        }

        [Route("DeleteRequest")]
        public IHttpActionResult DeleteRequest(int TravelRequestID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.DeleteRequest(TravelRequestID));
            return httpActionResult;
        }

        [Route("GetTravelRequest")]
        public string GetTravelRequest(int TravelRequestID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetTravelRequest(TravelRequestID));
            return json;
        }

        [Route("GetTravelRequests")]
        public string GetTravelRequests()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetTravelRequests());
            return json;
        }

        [Route("IsJobGradeValid")]
        public IHttpActionResult IsJobGradeValid(string JobGrade)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.IsJobGradeValid(JobGrade));
            return httpActionResult;
        }

        [Route("ApproveRequest")]
        public IHttpActionResult ApproveRequest(string RequestID, int ApproverID, bool IsApproved)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.ApproveRequest(RequestID, ApproverID, IsApproved));
            return httpActionResult;
        }

        [Route("HRApproval")]
        public IHttpActionResult HRApproval(int RequestID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.HRApproval(RequestID));
            return httpActionResult;
        }

        [Route("SaveQuote")]
        public IHttpActionResult SaveQuote(HttpRequestMessage obj)
        {
            var value = obj.Content.ReadAsStringAsync().Result;
            var QuoteData = new JavaScriptSerializer().Deserialize(value, typeof(ORG_Quote));
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SaveQuote((ORG_Quote)QuoteData));
            return httpActionResult;
        }

        [Route("DeleteQuote")]
        public IHttpActionResult DeleteQuote(int QuoteID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.DeleteQuote(QuoteID));
            return httpActionResult;
        }

        [Route("GetQuote")]
        public string GetQuote(int QuoteID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetQuote(QuoteID));
            return json;
        }

        [Route("GetQuotes")]
        public string GetQuotes()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetQuotes());
            return json;
        }

        [Route("SendEmail")]
        public string SendEmail(HttpRequestMessage obj, string to, string subject)
        {
            var value = obj.Content.ReadAsStringAsync().Result;
            var TravelRequestData = new JavaScriptSerializer().Deserialize(value, typeof(ORG_Quote));
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SendEmail((ORG_Quote)TravelRequestData, to, subject));
            return json;
        }

        [Route("SaveLPO")]
        public IHttpActionResult SaveLPO(HttpRequestMessage obj)
        {
            var value = obj.Content.ReadAsStringAsync().Result;
            var LPOData = new JavaScriptSerializer().Deserialize(value, typeof(ORG_LPO));
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SaveLPO((ORG_LPO)LPOData));
            return httpActionResult;
        }

        [Route("DeleteLPO")]
        public IHttpActionResult DeleteLPO(int LPOID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.DeleteLPO(LPOID));
            return httpActionResult;
        }

        [Route("GetLPO")]
        public string GetLPO(int LPOID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetLPO(LPOID));
            return json;
        }

        [Route("GetLPOs")]
        public string GetLPOs()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetLPOs());
            return json;
        }

        [Route("SaveTravelAgency")]
        public IHttpActionResult SaveTravelAgency(HttpRequestMessage obj)
        {
            var value = obj.Content.ReadAsStringAsync().Result;
            var TravelAgencyData = new JavaScriptSerializer().Deserialize(value, typeof(ORG_TravelAgency));
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SaveTravelAgency((ORG_TravelAgency)TravelAgencyData));
            return httpActionResult;
        }

        [Route("DeleteTravelAgency")]
        public IHttpActionResult DeleteTravelAgency(int TravelAgencyID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.DeleteTravelAgency(TravelAgencyID));
            return httpActionResult;
        }

        [Route("GetTravelAgency")]
        public string GetTravelAgency(int TravelAgencyID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetTravelAgency(TravelAgencyID));
            return json;
        }

        [Route("GetTravelAgencys")]
        public string GetTravelAgencys()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetTravelAgencys());
            return json;
        }

        [Route("SaveNotification")]
        public IHttpActionResult SaveNotification(HttpRequestMessage obj)
        {
            var value = obj.Content.ReadAsStringAsync().Result;
            var NotificationData = new JavaScriptSerializer().Deserialize(value, typeof(HRW_Notification));
            var json = new JavaScriptSerializer().Serialize(BusinessObj.SaveNotification((HRW_Notification)NotificationData));
            return httpActionResult;
        }

        [Route("DeleteNotification")]
        public string DeleteNotification(int NotificationID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.DeleteNotification(NotificationID));
            return json;
        }

        [Route("GetNotification")]
        public string GetNotification(int NotificationID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.DeleteNotification(NotificationID));
            return json;
        }

        [Route("GetNotifications")]
        public string GetNotifications()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetNotifications());
            return json;
        }

        [Route("GetDestinations")]
        public string GetDestinations()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetDestinations());
            return json;
        }

        [Route("GetDestinations")]
        public string GetDestinations(string ForJobGrade)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetDestinations(ForJobGrade));
            return json;
        }

        [Route("GetHotelCategory")]
        public string GetHotelCategory()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetHotelCategory());
            return json;
        }

        [Route("GetHotelCategory")]
        public string GetHotelCategory(string ForJobGrade)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetHotelCategory(ForJobGrade));
            return json;
        }

        [Route("GetRoomType")]
        public string GetRoomType()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetRoomType());
            return json;
        }

        [Route("GetRoomType")]
        public string GetRoomType(string ForJobGrade)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetRoomType());
            return json;
        }

        [Route("GenerateReport")]
        public void GenerateReport(int EmpID, int QuoteID, int LPOID, int RequestID)
        {
            BusinessObj.GenerateReport(EmpID, QuoteID, LPOID, RequestID);
        }

        #region TBUL
        //[Route("SearchOrders/{EmpId}/{FilterBy}/{SearchCriteria}/{OrderBy}")]
        //public string GetSearchOrders(int EmpId, string FilterBy, string SearchCriteria, string OrderBy)
        //{
        //    var json = JsonConvert.SerializeObject(BusinessObj.GetCustOrders(EmpId, FilterBy, SearchCriteria, OrderBy), Formatting.Indented,
        //                 new JsonSerializerSettings
        //                 {
        //                     DateFormatHandling = DateFormatHandling.IsoDateFormat
        //                 });
        //    return json;
        //}

        //[Route("SearchUtilityOrders/{EmpId}/{FilterBy}/{SearchCriteria}/{OrderBy}")]
        //public string GetSearchUtilityOrders(int EmpId, string FilterBy, string SearchCriteria, string OrderBy)
        //{
        //    var json = JsonConvert.SerializeObject(BusinessObj.GetEmployeeUtilityOrders(EmpId, FilterBy, SearchCriteria, OrderBy), Formatting.Indented,
        //                 new JsonSerializerSettings
        //                 {
        //                     DateFormatHandling = DateFormatHandling.IsoDateFormat
        //                 });
        //    return json;
        //}

        //[Route("OrderInitiation")]
        //public string PostOrder(HttpRequestMessage order)
        //{
        //    var value = order.Content.ReadAsStringAsync().Result;

        //    var orderObj = new JavaScriptSerializer().Deserialize(value, typeof(HRW_EmpEntityParamValues));

        //    return new JavaScriptSerializer().Serialize(BusinessObj.SaveOrder((HRW_EmpEntityParamValues)orderObj));
        //}

        //[Route("PostUtilityOrder")]
        //public string PostUtilityOrder([FromBody]string value)
        //{
        //    var utilityOrderObj = new JavaScriptSerializer().Deserialize(value, typeof(UtilityOrder));
        //    return new JavaScriptSerializer().Serialize(BusinessObj.SaveUtilityOrder((UtilityOrder)utilityOrderObj));
        //}

        //[HttpPost]
        //[Route("ChangeUtilityOrderStatus/{OrderIDs}/{Status}")]
        //public string ChangeUtilityOrderStatus([FromBody]List<int> OrderIDs, int StatusID)
        //{
        //    return BusinessObj.ChangeUtilityOrderStatus(OrderIDs, StatusID);
        //}

        //[Route("GetCordStyle/{For}")]
        //public string GetCordStyle(string For)
        //{
        //    var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCordStyle(For));
        //    return json; ;
        //}

        //[Route("GetBlindType")]
        //public string GetBlindType()
        //{
        //    var json = new JavaScriptSerializer().Serialize(BusinessObj.GetBlindType());
        //    return json; ;
        //}
        #endregion
    }
}