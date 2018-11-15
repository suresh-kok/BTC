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
        }
        // GET: TravelRequests
        public async Task<ActionResult> Index()
        {
            var travelRequests = db.TravelRequests.Include(t => t.City).Include(t => t.Currency).Include(t => t.User);
            return View(await travelRequests.ToListAsync());
        }

        // GET: TravelRequests/Details/5
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

        // GET: TravelRequests/Create
        public ActionResult Create()
        {
            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            return View();

        }

        // POST: TravelRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TravelRequestID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequest travelRequest, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
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

                db.TravelRequests.Add(travelRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            return View(travelRequest);
        }

        // GET: TravelRequests/Edit/5
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

            return View(travelRequest);
        }

        // POST: TravelRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TravelRequestID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequest travelRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelRequest).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();
            return View(travelRequest);
        }

        // GET: TravelRequests/Delete/5
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
            return View(travelRequest);
        }

        // POST: TravelRequests/Delete/5
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
    }
}
