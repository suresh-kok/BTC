using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    [CustomAuthorize(Roles = "Employee")]
    public class TravelRequestsController : Controller
    {
        private HRWorksEntities db = new HRWorksEntities();
        private static MembershipUser user;
        private static string[] roles;

        public TravelRequestsController()
        {
            user = Membership.GetUser();
            CustomRole customRole = new CustomRole();
            roles = customRole.GetRolesForUser(user.UserName);
            IsLoggedIn(roles.ToList());
            using (HRWorksEntities db = new HRWorksEntities())
            {
                User userobj = new User();
                userobj = db.Users.Where(a => a.Username == user.UserName).Include(a => a.Roles).Include(a => a.TravelRequestsApprover).Include(a => a.TravelRequestsUser).Include(a => a.TravelRequestCreatedBy).FirstOrDefault();
                ViewBag.FirstName = userobj.FirstName;
                ViewBag.LastName = userobj.LastName;
                ViewBag.RoleName = roles.ToList()[0];
            }
        }

        public async Task<ActionResult> Index()
        {
            var travelRequests = db.TravelRequests.Include(t => t.DestinationCity).Include(t => t.OriginCity).Include(t => t.Currency).Include(t => t.User);
            return View(await travelRequests.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.ErrorMessage = "No Travel Request ID";
                    return View();
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Travel Request ID");
                }
                TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
                if (travelRequest == null)
                {
                    ViewBag.ErrorMessage = "Invalid Travel Request ID";
                    return View();
                    //return HttpNotFound("Invalid Travel Request ID");
                }

                ViewBag.Cities = db.Cities.ToList();
                ViewBag.CurrencyID = db.Currencies.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                return View(travelRequest);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public ActionResult Create()
        {
            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();
            ViewBag.applicationNumber = db.TravelRequests.Count() > 0 ? GenerateNextRFQ(db.TravelRequests.OrderByDescending(a => a.TravelRequestID).FirstOrDefault().ApplicationNumber) : "HRD-BTC-CC-0001";

            checkErrorMessages();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitTravelRequest([Bind(Include = "TravelRequestID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequest travelRequest, FormCollection collection)
        {
            MapUserValues(ref travelRequest, ref collection);
            if (ModelState.IsValid)
            {
                travelRequest.IsSubmitted = true;
                db.TravelRequests.Add(travelRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                List<string> sberr = new List<string>();
                foreach (var item in errlist)
                {
                    sberr.Add(item[0].ErrorMessage);
                }
                TempData["ErrorMessage"] = sberr.ToList();
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            return RedirectToAction("Create", travelRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveTravelRequest([Bind(Include = "TravelRequestID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequest travelRequest, FormCollection collection)
        {
            MapUserValues(ref travelRequest, ref collection);
            if (ModelState.IsValid)
            {
                travelRequest.IsSubmitted = false;
                db.TravelRequests.Add(travelRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                List<string> sberr = new List<string>();
                foreach (var item in errlist)
                {
                    sberr.Add(item[0].ErrorMessage);
                }
                TempData["ErrorMessage"] = sberr.ToList();
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            return RedirectToAction("Create", travelRequest);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
            if (travelRequest == null)
            {
                return HttpNotFound();
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            checkErrorMessages();
            return View(travelRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TravelRequestID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequest travelRequest, FormCollection collection)
        {
            MapUserValues(ref travelRequest, ref collection);
            if (ModelState.IsValid)
            {
                db.Entry(travelRequest).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                List<string> sberr = new List<string>();
                foreach (var item in errlist)
                {
                    sberr.Add(item[0].ErrorMessage);
                }
                TempData["ErrorMessage"] = sberr.ToList();
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            checkErrorMessages();
            return View(travelRequest);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
            if (travelRequest == null)
            {
                return HttpNotFound();
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            checkErrorMessages();
            return View(travelRequest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
            db.TravelRequests.Remove(travelRequest);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void IsLoggedIn(List<string> Role)
        {
            var Val = true;
            ViewBag.LoggedOut = !Val;
            ViewBag.messages = Val;
            ViewBag.notifications = Val;
            ViewBag.tasks = Val;
            ViewBag.userdetails = Val;
            ViewBag.IsEmployee = !Val;
            ViewBag.IsHR = !Val;
            ViewBag.IsTravelCo = !Val;
            ViewBag.IsManager = !Val;
            ViewBag.IsAdmin = !Val;
            if (roles.Contains(Constants.Employee))
            {
                ViewBag.IsEmployee = Val;
            }
            if (roles.Contains(Constants.HR))
            {
                ViewBag.IsHR = Val;
            }
            if (roles.Contains(Constants.Admin))
            {
                ViewBag.IsAdmin = Val;
            }
            if (roles.Contains(Constants.Manager))
            {
                ViewBag.IsManager = Val;
            }
            if (roles.Contains(Constants.TravelCorordinator))
            {
                ViewBag.IsTravelCo = Val;
            }
        }

        private void MapUserValues(ref TravelRequest travelRequest, ref FormCollection collection)
        {
            travelRequest.ApplicationNumber = string.IsNullOrEmpty(collection["applicationNumber"]) ? "" : collection["applicationNumber"].ToString();
            travelRequest.AirportPickUp = string.IsNullOrEmpty(collection["airportPickUp"]) ? "" : collection["airportPickUp"].ToString();
            travelRequest.AirTicketManagement = string.IsNullOrEmpty(collection["airTicketManagement"]) ? "" : collection["airTicketManagement"].ToString();
            travelRequest.HotelCategory = string.IsNullOrEmpty(collection["hotelCategory"]) ? "" : collection["hotelCategory"].ToString();
            travelRequest.HotelStay = string.IsNullOrEmpty(collection["hotelStay"]) ? "" : collection["hotelStay"].ToString();
            travelRequest.PreferredVehicle = string.IsNullOrEmpty(collection["preferredVehicle"]) ? "" : collection["preferredVehicle"].ToString();
            travelRequest.RoomCategory = string.IsNullOrEmpty(collection["roomCategory"]) ? "" : collection["roomCategory"].ToString();
            travelRequest.RoomType = string.IsNullOrEmpty(collection["roomType"]) ? "" : collection["roomType"].ToString();
            travelRequest.TicketClass = string.IsNullOrEmpty(collection["ticketClass"]) ? "" : collection["ticketClass"].ToString();
            travelRequest.TravelAllowance = string.IsNullOrEmpty(collection["travelAllowance"]) ? "" : collection["travelAllowance"].ToString();
            travelRequest.CheckInTime = string.IsNullOrEmpty(collection["checkInTime"]) ? new TimeSpan() : DateTime.ParseExact(collection["checkInTime"],
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            travelRequest.CheckOutTime = string.IsNullOrEmpty(collection["checkOutTime"]) ? new TimeSpan() : DateTime.ParseExact(collection["checkOutTime"],
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            travelRequest.DepartureTime = string.IsNullOrEmpty(collection["departureTime"]) ? new TimeSpan() : DateTime.ParseExact(collection["departureTime"],
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            travelRequest.DropOffTime = string.IsNullOrEmpty(collection["dropOffTime"]) ? new TimeSpan() : DateTime.ParseExact(collection["dropOffTime"],
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            travelRequest.PickUpTime = string.IsNullOrEmpty(collection["pickUpTime"]) ? new TimeSpan() : DateTime.ParseExact(collection["pickUpTime"],
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            travelRequest.ReturnTime = string.IsNullOrEmpty(collection["returnTime"]) ? new TimeSpan() : DateTime.ParseExact(collection["returnTime"],
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;

            using (HRWorksEntities db = new HRWorksEntities())
            {
                travelRequest.ModifiedBy = (db.Users.Where(a => a.Username == user.UserName).Include(a => a.Roles).Include(a => a.TravelRequestsUser).Include(a => a.TravelRequestsApprover).Include(a => a.TravelRequestCreatedBy).FirstOrDefault()).UserId;
                travelRequest.ModifiedOn = DateTime.Now;
                travelRequest.UserID = (int)travelRequest.ModifiedBy;
            }
        }

        private string GenerateNextRFQ(string currentRFQ)
        {
            string[] RFQno = currentRFQ.Split('-');
            return RFQno[0] + '-' + RFQno[1] + '-' + RFQno[2] + '-' + String.Format("{0:D4}", (Convert.ToInt32(RFQno[3]) + 1));
        }

        private void PrintTravelRequest(TravelRequest travelRequest)
        {

        }

        private void checkErrorMessages()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
        }

    }
}
