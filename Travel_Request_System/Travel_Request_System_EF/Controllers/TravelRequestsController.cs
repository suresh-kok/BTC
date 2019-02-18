using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.DataAnnotations;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    [HandleError]
    [RedirectingAction]
    [CustomAuthorize(Roles = "Employee,HR,Manager,Admin")]
    public class TravelRequestsController : Controller
    {
        private static MembershipUser user;
        private static Users dbuser;
        private static string[] roles;

        public TravelRequestsController()
        {
            try
            {
                user = Membership.GetUser();
                CustomRole customRole = new CustomRole();
                roles = customRole.GetRolesForUser(user.UserName);
                ViewBag.RoleDetails = roles.ToList()[0];
                IsLoggedIn(roles.ToList());
                using (BTCEntities db = new BTCEntities())
                {
                    dbuser = db.Users.Where(a => a.Username == user.UserName).Include(a => a.Roles).Include(a => a.TravelRequests).Include(a => a.TravelRequests1).FirstOrDefault();
                    ViewBag.UserDetails = dbuser;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Your Login has expired!! Please login again.";
            }
        }

        public async Task<ActionResult> Index()
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (roles.ToList()[0] == "Admin")
                {
                    return View(await db.TravelRequests.Include(t => t.City).Include(t => t.City1).Include(t => t.Currency).Include(t => t.Users1).Include(t => t.Users).ToListAsync());
                }
                else
                {
                    return View(await db.TravelRequests.Include(t => t.City).Include(t => t.City1).Include(t => t.Currency).Include(t => t.Users1).Include(t => t.Users).Where(a => a.CreatedBy == dbuser.ID).ToListAsync());
                }
            }
        }

        public async Task<ActionResult> Details(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (id == null)
                {
                    ViewBag.ErrorMessage = "No Travel Request ID";
                    return View();
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Travel Request ID");
                }
                TravelRequests travelRequest = await db.TravelRequests.FindAsync(id);
                if (travelRequest == null)
                {
                    ViewBag.ErrorMessage = "Invalid Travel Request ID";
                    return View();
                    //return HttpNotFound("Invalid Travel Request ID");
                }

                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                return View(travelRequest);
            }
        }

        public ActionResult Create()
        {
            using (BTCEntities db = new BTCEntities())
            {
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                ViewBag.applicationNumber = db.TravelRequests.Count() > 0 ? GenerateNextRFQ(db.TravelRequests.OrderByDescending(a => a.ID).FirstOrDefault().ApplicationNumber) : "HRD-BTC-CC-0001";

                checkErrorMessages();

                if (TempData["tempTravelRequest"] != null)
                {
                    var travelRequest = (TravelRequests)TempData["tempTravelRequest"];
                    return View(travelRequest);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> SubmitTravelRequest([System.Web.Http.FromBody][Bind(Include = "ID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequests travelRequest, FormCollection collection)
        {
            using (var dbcontext = new BTCEntities())
            {
                MapUserValues(ref travelRequest, ref collection);
                travelRequest.CreatedBy = dbuser.ID;
                travelRequest.CreateOn = DateTime.Now;
                travelRequest.ApplicationNumber = string.IsNullOrEmpty(collection["applicationNumber"]) ? "" : collection["applicationNumber"].ToString();
                travelRequest.IsSubmitted = true;
                travelRequest.ApprovalLevel = (int)ApprovalLevels.ToBeApproved;

                if (ModelState.IsValid)
                {
                    if (travelRequest.ID > 0)
                    {
                        var travelRequestData = dbcontext.TravelRequests.Where(x => x.ID == travelRequest.ID).FirstOrDefault();
                        travelRequestData.AdditionalAllowance = travelRequest.AdditionalAllowance;
                        travelRequestData.AirportPickUp = travelRequest.AirportPickUp;
                        travelRequestData.AirTicketManagement = travelRequest.AirTicketManagement;
                        travelRequestData.ApprovalBy = travelRequest.ApprovalBy;
                        travelRequestData.ApprovalLevel = travelRequest.ApprovalLevel;
                        travelRequestData.ApprovalRemarks = travelRequest.ApprovalRemarks;
                        travelRequestData.CheckInDate = travelRequest.CheckInDate;
                        travelRequestData.CheckInTime = travelRequest.CheckInTime;
                        travelRequestData.CheckOutDate = travelRequest.CheckOutDate;
                        travelRequestData.CheckOutTime = travelRequest.CheckOutTime;
                        travelRequestData.CreatedBy = travelRequest.CreatedBy;
                        travelRequestData.CreateOn = travelRequest.CreateOn;
                        travelRequestData.CurrencyID = travelRequest.CurrencyID;
                        travelRequestData.DailyAllowance = travelRequest.DailyAllowance;
                        travelRequestData.DepartureDate = travelRequest.DepartureDate;
                        travelRequestData.DepartureTime = travelRequest.DepartureTime;
                        travelRequestData.DropOffDate = travelRequest.DropOffDate;
                        travelRequestData.DropOffLocation = travelRequest.DropOffLocation;
                        travelRequestData.DropOffTime = travelRequest.DropOffTime;
                        travelRequestData.ExpensesHandledBy = travelRequest.ExpensesHandledBy;
                        travelRequestData.FirstBusinessDay = travelRequest.FirstBusinessDay;
                        travelRequestData.HotelCategory = travelRequest.HotelCategory;
                        travelRequestData.HotelName = travelRequest.HotelName;
                        travelRequestData.HotelStay = travelRequest.HotelStay;
                        travelRequestData.LastBusinessDay = travelRequest.LastBusinessDay;
                        travelRequestData.IsSubmitted = travelRequest.IsSubmitted;
                        travelRequestData.ModifiedBy = travelRequest.ModifiedBy;
                        travelRequestData.ModifiedOn = travelRequest.ModifiedOn;
                        travelRequestData.PickUpDate = travelRequest.PickUpDate;
                        travelRequestData.PickUpLocation = travelRequest.PickUpLocation;
                        travelRequestData.PickUpTime = travelRequest.PickUpTime;
                        travelRequestData.PortOfDestinationID = travelRequest.PortOfDestinationID;
                        travelRequestData.PortOfOriginID = travelRequest.PortOfOriginID;
                        travelRequestData.PreferredVehicle = travelRequest.PreferredVehicle;
                        travelRequestData.PurposeOfVisit = travelRequest.PurposeOfVisit;
                        travelRequestData.Remarks = travelRequest.Remarks;
                        travelRequestData.ReturnDate = travelRequest.ReturnDate;
                        travelRequestData.ReturnTime = travelRequest.ReturnTime;
                        travelRequestData.RoomCategory = travelRequest.RoomCategory;
                        travelRequestData.RoomType = travelRequest.RoomType;
                        travelRequestData.TicketClass = travelRequest.TicketClass;
                        travelRequestData.TravelAllowance = travelRequest.TravelAllowance;
                        travelRequestData.TravelDays = travelRequest.TravelDays;
                        travelRequestData.TravelRemarks = travelRequest.TravelRemarks;
                        travelRequestData.TravelSector = travelRequest.TravelSector;

                        dbcontext.Entry(travelRequestData).State = EntityState.Modified;
                        dbcontext.TravelRequests.Attach(travelRequestData);
                        dbcontext.Entry(travelRequestData).Property(x => x.AdditionalAllowance).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.AirportPickUp).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.AirTicketManagement).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ApprovalBy).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ApprovalLevel).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ApprovalRemarks).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CheckInDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CheckInTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CheckOutDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CheckOutTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CreatedBy).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CreateOn).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CurrencyID).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DailyAllowance).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DepartureDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DepartureTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DropOffDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DropOffLocation).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DropOffTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ExpensesHandledBy).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.FirstBusinessDay).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.HotelCategory).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.HotelName).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.HotelStay).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.LastBusinessDay).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.IsSubmitted).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ModifiedBy).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ModifiedOn).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PickUpDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PickUpLocation).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PickUpTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PortOfDestinationID).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PortOfOriginID).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PreferredVehicle).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PurposeOfVisit).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.Remarks).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ReturnDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ReturnTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.RoomCategory).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.RoomType).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TicketClass).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TravelAllowance).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TravelDays).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TravelRemarks).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TravelSector).IsModified = true;
                    }
                    else
                    {
                        dbcontext.TravelRequests.Add(travelRequest);
                    }
                    await dbcontext.SaveChangesAsync();
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

                ViewBag.Cities = dbcontext.City.ToList();
                ViewBag.Currencies = dbcontext.Currency.ToList();
                ViewBag.ApprovalBy = dbcontext.Users.ToList();

                TempData["tempTravelRequest"] = travelRequest;

                return RedirectToAction("Create");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveTravelRequest([System.Web.Http.FromBody][Bind(Include = "ID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequests travelRequest, FormCollection collection)
        {
            using (var dbcontext = new BTCEntities())
            {
                MapUserValues(ref travelRequest, ref collection);
                travelRequest.CreatedBy = dbuser.ID;
                travelRequest.CreateOn = DateTime.Now;
                travelRequest.IsSubmitted = false;
                travelRequest.ApplicationNumber = string.IsNullOrEmpty(collection["applicationNumber"]) ? "" : collection["applicationNumber"].ToString();

                if (ModelState.IsValid)
                {
                    if (travelRequest.ID > 0)
                    {
                        var travelRequestData = dbcontext.TravelRequests.Where(x => x.ID == travelRequest.ID).FirstOrDefault();
                        travelRequestData.AdditionalAllowance = travelRequest.AdditionalAllowance;
                        travelRequestData.AirportPickUp = travelRequest.AirportPickUp;
                        travelRequestData.AirTicketManagement = travelRequest.AirTicketManagement;
                        travelRequestData.ApprovalBy = travelRequest.ApprovalBy;
                        travelRequestData.ApprovalLevel = travelRequest.ApprovalLevel;
                        travelRequestData.ApprovalRemarks = travelRequest.ApprovalRemarks;
                        travelRequestData.CheckInDate = travelRequest.CheckInDate;
                        travelRequestData.CheckInTime = travelRequest.CheckInTime;
                        travelRequestData.CheckOutDate = travelRequest.CheckOutDate;
                        travelRequestData.CheckOutTime = travelRequest.CheckOutTime;
                        travelRequestData.CreatedBy = travelRequest.CreatedBy;
                        travelRequestData.CreateOn = travelRequest.CreateOn;
                        travelRequestData.CurrencyID = travelRequest.CurrencyID;
                        travelRequestData.DailyAllowance = travelRequest.DailyAllowance;
                        travelRequestData.DepartureDate = travelRequest.DepartureDate;
                        travelRequestData.DepartureTime = travelRequest.DepartureTime;
                        travelRequestData.DropOffDate = travelRequest.DropOffDate;
                        travelRequestData.DropOffLocation = travelRequest.DropOffLocation;
                        travelRequestData.DropOffTime = travelRequest.DropOffTime;
                        travelRequestData.ExpensesHandledBy = travelRequest.ExpensesHandledBy;
                        travelRequestData.FirstBusinessDay = travelRequest.FirstBusinessDay;
                        travelRequestData.HotelCategory = travelRequest.HotelCategory;
                        travelRequestData.HotelName = travelRequest.HotelName;
                        travelRequestData.HotelStay = travelRequest.HotelStay;
                        travelRequestData.LastBusinessDay = travelRequest.LastBusinessDay;
                        travelRequestData.IsSubmitted = travelRequest.IsSubmitted;
                        travelRequestData.ModifiedBy = travelRequest.ModifiedBy;
                        travelRequestData.ModifiedOn = travelRequest.ModifiedOn;
                        travelRequestData.PickUpDate = travelRequest.PickUpDate;
                        travelRequestData.PickUpLocation = travelRequest.PickUpLocation;
                        travelRequestData.PickUpTime = travelRequest.PickUpTime;
                        travelRequestData.PortOfDestinationID = travelRequest.PortOfDestinationID;
                        travelRequestData.PortOfOriginID = travelRequest.PortOfOriginID;
                        travelRequestData.PreferredVehicle = travelRequest.PreferredVehicle;
                        travelRequestData.PurposeOfVisit = travelRequest.PurposeOfVisit;
                        travelRequestData.Remarks = travelRequest.Remarks;
                        travelRequestData.ReturnDate = travelRequest.ReturnDate;
                        travelRequestData.ReturnTime = travelRequest.ReturnTime;
                        travelRequestData.RoomCategory = travelRequest.RoomCategory;
                        travelRequestData.RoomType = travelRequest.RoomType;
                        travelRequestData.TicketClass = travelRequest.TicketClass;
                        travelRequestData.TravelAllowance = travelRequest.TravelAllowance;
                        travelRequestData.TravelDays = travelRequest.TravelDays;
                        travelRequestData.TravelRemarks = travelRequest.TravelRemarks;
                        travelRequestData.TravelSector = travelRequest.TravelSector;


                        dbcontext.Entry(travelRequestData).State = EntityState.Modified;
                        dbcontext.TravelRequests.Attach(travelRequestData);
                        dbcontext.Entry(travelRequestData).Property(x => x.AdditionalAllowance).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.AirportPickUp).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.AirTicketManagement).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ApprovalBy).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ApprovalLevel).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ApprovalRemarks).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CheckInDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CheckInTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CheckOutDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CheckOutTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CreatedBy).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CreateOn).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.CurrencyID).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DailyAllowance).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DepartureDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DepartureTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DropOffDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DropOffLocation).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.DropOffTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ExpensesHandledBy).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.FirstBusinessDay).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.HotelCategory).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.HotelName).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.HotelStay).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.LastBusinessDay).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.IsSubmitted).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ModifiedBy).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ModifiedOn).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PickUpDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PickUpLocation).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PickUpTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PortOfDestinationID).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PortOfOriginID).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PreferredVehicle).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.PurposeOfVisit).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.Remarks).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ReturnDate).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.ReturnTime).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.RoomCategory).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.RoomType).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TicketClass).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TravelAllowance).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TravelDays).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TravelRemarks).IsModified = true;
                        dbcontext.Entry(travelRequestData).Property(x => x.TravelSector).IsModified = true;
                    }
                    else
                    {
                        dbcontext.TravelRequests.Add(travelRequest);
                    }
                    await dbcontext.SaveChangesAsync();
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

                ViewBag.Cities = dbcontext.City.ToList();
                ViewBag.Currencies = dbcontext.Currency.ToList();
                ViewBag.ApprovalBy = dbcontext.Users.ToList();

                TempData["tempTravelRequest"] = travelRequest;

                return RedirectToAction("Create");

            }
        }

        public async Task<ActionResult> Edit(int? id)
        {
            using (var db = new BTCEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TravelRequests travelRequest = await db.TravelRequests.FindAsync(id);
                if (travelRequest == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                checkErrorMessages();
                return View(travelRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequests travelRequest, FormCollection collection)
        {
            using (var dbcontext = new BTCEntities())
            {
                MapUserValues(ref travelRequest, ref collection);
                travelRequest.CreatedBy = dbuser.ID;
                travelRequest.CreateOn = DateTime.Now;
                travelRequest.IsSubmitted = false;
                travelRequest.ApplicationNumber = string.IsNullOrEmpty(collection["applicationNumber"]) ? "" : collection["applicationNumber"].ToString();

                if (ModelState.IsValid)
                {
                    var travelRequestData = dbcontext.TravelRequests.Where(x => x.ID == travelRequest.ID).FirstOrDefault();
                    travelRequestData.AdditionalAllowance = travelRequest.AdditionalAllowance;
                    travelRequestData.AirportPickUp = travelRequest.AirportPickUp;
                    travelRequestData.AirTicketManagement = travelRequest.AirTicketManagement;
                    travelRequestData.ApprovalBy = travelRequest.ApprovalBy;
                    travelRequestData.ApprovalLevel = travelRequest.ApprovalLevel;
                    travelRequestData.ApprovalRemarks = travelRequest.ApprovalRemarks;
                    travelRequestData.CheckInDate = travelRequest.CheckInDate;
                    travelRequestData.CheckInTime = travelRequest.CheckInTime;
                    travelRequestData.CheckOutDate = travelRequest.CheckOutDate;
                    travelRequestData.CheckOutTime = travelRequest.CheckOutTime;
                    travelRequestData.CreatedBy = travelRequest.CreatedBy;
                    travelRequestData.CreateOn = travelRequest.CreateOn;
                    travelRequestData.CurrencyID = travelRequest.CurrencyID;
                    travelRequestData.DailyAllowance = travelRequest.DailyAllowance;
                    travelRequestData.DepartureDate = travelRequest.DepartureDate;
                    travelRequestData.DepartureTime = travelRequest.DepartureTime;
                    travelRequestData.DropOffDate = travelRequest.DropOffDate;
                    travelRequestData.DropOffLocation = travelRequest.DropOffLocation;
                    travelRequestData.DropOffTime = travelRequest.DropOffTime;
                    travelRequestData.FirstBusinessDay = travelRequest.FirstBusinessDay;
                    travelRequestData.HotelCategory = travelRequest.HotelCategory;
                    travelRequestData.HotelName = travelRequest.HotelName;
                    travelRequestData.HotelStay = travelRequest.HotelStay;
                    travelRequestData.LastBusinessDay = travelRequest.LastBusinessDay;
                    travelRequestData.IsSubmitted = travelRequest.IsSubmitted;
                    travelRequestData.ModifiedBy = travelRequest.ModifiedBy;
                    travelRequestData.ModifiedOn = travelRequest.ModifiedOn;
                    travelRequestData.PickUpDate = travelRequest.PickUpDate;
                    travelRequestData.PickUpLocation = travelRequest.PickUpLocation;
                    travelRequestData.PickUpTime = travelRequest.PickUpTime;
                    travelRequestData.PortOfDestinationID = travelRequest.PortOfDestinationID;
                    travelRequestData.PortOfOriginID = travelRequest.PortOfOriginID;
                    travelRequestData.PreferredVehicle = travelRequest.PreferredVehicle;
                    travelRequestData.PurposeOfVisit = travelRequest.PurposeOfVisit;
                    travelRequestData.Remarks = travelRequest.Remarks;
                    travelRequestData.ReturnDate = travelRequest.ReturnDate;
                    travelRequestData.ReturnTime = travelRequest.ReturnTime;
                    travelRequestData.RoomCategory = travelRequest.RoomCategory;
                    travelRequestData.RoomType = travelRequest.RoomType;
                    travelRequestData.TicketClass = travelRequest.TicketClass;
                    travelRequestData.TravelAllowance = travelRequest.TravelAllowance;
                    travelRequestData.TravelDays = travelRequest.TravelDays;
                    travelRequestData.TravelRemarks = travelRequest.TravelRemarks;
                    travelRequestData.TravelSector = travelRequest.TravelSector;


                    dbcontext.Entry(travelRequestData).State = EntityState.Modified;
                    dbcontext.TravelRequests.Attach(travelRequestData);
                    dbcontext.Entry(travelRequestData).Property(x => x.AdditionalAllowance).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.AirportPickUp).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.AirTicketManagement).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.ApprovalBy).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.ApprovalLevel).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.ApprovalRemarks).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.CheckInDate).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.CheckInTime).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.CheckOutDate).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.CheckOutTime).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.CreatedBy).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.CreateOn).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.CurrencyID).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.DailyAllowance).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.DepartureDate).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.DepartureTime).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.DropOffDate).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.DropOffLocation).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.DropOffTime).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.FirstBusinessDay).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.HotelCategory).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.HotelName).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.HotelStay).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.LastBusinessDay).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.IsSubmitted).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.ModifiedBy).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.ModifiedOn).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.PickUpDate).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.PickUpLocation).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.PickUpTime).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.PortOfDestinationID).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.PortOfOriginID).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.PreferredVehicle).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.PurposeOfVisit).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.Remarks).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.ReturnDate).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.ReturnTime).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.RoomCategory).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.RoomType).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.TicketClass).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.TravelAllowance).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.TravelDays).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.TravelRemarks).IsModified = true;
                    dbcontext.Entry(travelRequestData).Property(x => x.TravelSector).IsModified = true;

                    await dbcontext.SaveChangesAsync();
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

                ViewBag.Cities = dbcontext.City.ToList();
                ViewBag.Currencies = dbcontext.Currency.ToList();
                ViewBag.ApprovalBy = dbcontext.Users.ToList();

                checkErrorMessages();
                return View(travelRequest);
            }
        }

        public async Task<ActionResult> Delete(int? id)
        {
            using (var db = new BTCEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TravelRequests travelRequest = await db.TravelRequests.FindAsync(id);
                if (travelRequest == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                checkErrorMessages();
                return View(travelRequest);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var db = new BTCEntities())
            {
                TravelRequests travelRequest = await db.TravelRequests.FindAsync(id);
                travelRequest.IsDeleted = true;
                db.TravelRequests.Attach(travelRequest);
                db.Entry(travelRequest).State = EntityState.Modified;
                db.Entry(travelRequest).Property(x => x.IsDeleted).IsModified = true;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public void PrintTravelRequest(int id)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            var connectionString = ConfigurationManager.ConnectionStrings["AuthenticationDB"].ConnectionString;
            BTCDataSet ds = new BTCDataSet();
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            Byte[] bytes;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp;

            adp = new SqlDataAdapter("select * FROM TravelRequestDetails where id = " + id, conx);
            ds = new BTCDataSet();
            adp.Fill(ds, ds.TravelRequestDetails.TableName);
            reportViewer.LocalReport.ReportPath = Path.Combine(@"C:\Users\kanniyappans\Documents\GitHub\BTC\Travel_Request_System\Travel_Request_System_EF\Reports\", "TravelRequestReport.rdlc");
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("detailsDataset", ds.Tables["TravelRequestDetails"]));
            bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename= outputreport" + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();

        }

        private void IsLoggedIn(List<string> Role)
        {
            var Val = true;
            ViewBag.LoggedOut = !Val;
            ViewBag.messages = Val;
            ViewBag.notifications = Val;
            ViewBag.tasks = Val;
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

        private void MapUserValues(ref TravelRequests travelRequest, ref FormCollection collection)
        {
            travelRequest.AirportPickUp = string.IsNullOrEmpty(collection["airportPickUp"]) ? "" : collection["airportPickUp"].ToString();
            travelRequest.ExpensesHandledBy = string.IsNullOrEmpty(collection["expensesHandledBy"]) ? "" : collection["expensesHandledBy"].ToString();
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

            travelRequest.ModifiedBy = dbuser.ID;
            travelRequest.ModifiedOn = DateTime.Now;
        }

        private string GenerateNextRFQ(string currentRFQ)
        {
            string[] RFQno = currentRFQ.Split('-');
            return RFQno[0] + '-' + RFQno[1] + '-' + RFQno[2] + '-' + String.Format("{0:D4}", (Convert.ToInt32(RFQno[3]) + 1));
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