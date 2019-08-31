using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.DataAnnotations;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    [HandleError]
    [RedirectingAction]
    public class QuotationController : Controller
    {
        private static MembershipUser user;
        private static Users dbuser;
        private static string[] roles;
        private static string empCode;
        private static int pageNo;
        private static Quotation quotation = new Quotation();

        public QuotationController()
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
                    dbuser = db.Users.Where(a => a.Username == user.UserName && a.IsActive && a.IsDeleted == false).Include(a => a.Roles).Include(a => a.TravelRequests).Include(a => a.TravelRequests1).FirstOrDefault();
                    ViewBag.UserDetails = dbuser;
                    empCode = dbuser.HRW_Employee.EmployeeCode;
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

                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                var RFQ = db.RFQ.Include(r => r.TravelAgency).Include(r => r.TravelRequests).Include(r => r.Users).Where(a => a.Processing > 0 && a.IsDeleted == false);
                return View(await RFQ.ToListAsync());
            }
        }

        public async Task<ActionResult> AddQuotation(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RFQ rFQ = await db.RFQ.Include(a => a.TravelRequests).Include(a => a.TravelAgency).Include(a => a.Users).Include(a => a.TravelRequests.Quotation).Include("Quotation.ATQuotation").Include("Quotation.HSQuotation").Include("Quotation.PCQuotation").Where(a => a.ID == id).FirstOrDefaultAsync();
                if (rFQ == null)
                {
                    return HttpNotFound();
                }
                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService("", rFQ.TravelRequests.Users1.HREmployeeID.ToString()))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                //ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(rFQ.TravelRequests.ApplicationNumber)).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                return View(rFQ);
            }
        }

        public ActionResult AddATQuotation(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                ATQuotation atQuote = new ATQuotation();
                ViewBag.Cities = db.City.ToList();
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();
                if (TempData["ATQuotationVal"] != null)
                {
                    atQuote = (ATQuotation)TempData["ATQuotationVal"];
                    ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".AT-Q" + atQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".AT-Q" + atQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                }
                else
                {
                    if (db.ATQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == false && a.IsDeleted == false).Count() > 0)
                    {
                        atQuote = db.ATQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == false && a.IsDeleted == false).FirstOrDefault();
                        ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".AT-Q" + atQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".AT-Q" + atQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    }
                    else
                    {
                        var tempatQuote = db.ATQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id).FirstOrDefault();
                        atQuote.IsActive = false;
                        atQuote.IsDeleted = false;
                        atQuote.IsLowest = false;
                        atQuote.OriginID = tempatQuote.OriginID;
                        atQuote.QuotationID = tempatQuote.QuotationID;
                        atQuote.QuotationName = tempatQuote.QuotationName;
                        atQuote.ReturnDate = tempatQuote.ReturnDate;
                        atQuote.ReturnTime = tempatQuote.ReturnTime;
                        atQuote.TicketClass = tempatQuote.TicketClass;
                        atQuote.DepartureDate = tempatQuote.DepartureDate;
                        atQuote.DepartureTime = tempatQuote.DepartureTime;
                        atQuote.DestinationID = tempatQuote.DestinationID;
                        atQuote.City = tempatQuote.City;
                        atQuote.City1 = tempatQuote.City1;
                        atQuote.Quotation = tempatQuote.Quotation;
                        db.ATQuotation.Add(atQuote);
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();

                        ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".AT-Q" + atQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".AT-Q" + atQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    }
                    pageNo = 1;
                    quotation.ATQuotation.Add(atQuote);
                    quotation.HSQuotation.Clear();
                    quotation.PCQuotation.Clear();

                    CheckErrorMessages();
                }
                return View(atQuote);
            }
        }

        public ActionResult ViewATQuotations(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();
                List<ATQuotation> atQuotes = new List<ATQuotation>();

                ViewBag.Cities = db.City.ToList();
                if (id > 0 && quotation.ATQuotation.Count > 0)
                {
                    foreach (var item in db.ATQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == true && a.IsDeleted == false))
                    {
                        ATQuotation atQuote = item;
                        var ATID = atQuote.ID;
                        ViewData.Add("fileUploader" + ATID, db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".AT-Q" + atQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList());
                        ViewData.Add("ATfileUploader" + ATID, db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".AT-Q" + atQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList());
                        atQuotes.Add(atQuote);
                    }
                }
                return View(atQuotes);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddATQuotation(ATQuotation atquote, FormCollection formCollection)
        {
            ModelState.Remove("DepartureTime");
            ModelState.Remove("ReturnTime");
            if (ModelState.IsValid)
            {
                using (BTCEntities db = new BTCEntities())
                {
                    if ((bool)atquote.IsLowest && db.ATQuotation.Include(a => a.Quotation).Where(a => a.QuotationID == atquote.QuotationID && (bool)a.IsLowest).Count() > 0)
                    {
                        TempData["ErrorMessage"] = new List<string>() { "Only One quotaiton can be the lowest" };
                        TempData["ATQuotationVal"] = atquote;
                        return View(new { id = atquote.QuotationID });
                    }
                    atquote.ID = string.IsNullOrEmpty(Convert.ToString(formCollection["ATId"])) ? 0 : Convert.ToInt32(formCollection["ATId"]);
                    var dbatquote = db.ATQuotation.Include(a => a.Quotation).Where(a => a.ID == atquote.ID).FirstOrDefault();
                    dbatquote.Airlines = string.IsNullOrEmpty(atquote.Airlines) ? "" : atquote.Airlines;
                    dbatquote.TicketClass = string.IsNullOrEmpty(atquote.TicketClass) ? "" : atquote.TicketClass;
                    dbatquote.TicketNo = string.IsNullOrEmpty(atquote.TicketNo) ? "" : atquote.TicketNo;

                    dbatquote.DepartureTime = string.IsNullOrEmpty(Convert.ToString(formCollection["DepartureTime"])) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(formCollection["DepartureTime"]),
                                    "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    dbatquote.ReturnTime = string.IsNullOrEmpty(Convert.ToString(formCollection["ReturnTime"])) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(formCollection["ReturnTime"]),
                                    "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;

                    dbatquote.Amount = atquote.Amount;
                    dbatquote.DepartureDate = atquote.DepartureDate;
                    dbatquote.ReturnDate = atquote.ReturnDate;
                    dbatquote.DestinationID = atquote.DestinationID;
                    dbatquote.OriginID = atquote.OriginID;
                    dbatquote.TicketClass = atquote.TicketClass;
                    dbatquote.TicketNo = atquote.TicketNo;
                    dbatquote.IsDeleted = false;
                    dbatquote.IsActive = true;
                    dbatquote.IsLowest = atquote.IsLowest;

                    db.ATQuotation.Attach(dbatquote);
                    var entry = db.Entry(dbatquote);
                    entry.Property(a => a.Airlines).IsModified = true;
                    entry.Property(a => a.Amount).IsModified = true;
                    entry.Property(a => a.DepartureDate).IsModified = true;
                    entry.Property(a => a.DepartureTime).IsModified = true;
                    entry.Property(a => a.DestinationID).IsModified = true;
                    entry.Property(a => a.ReturnDate).IsModified = true;
                    entry.Property(a => a.ReturnTime).IsModified = true;
                    entry.Property(a => a.OriginID).IsModified = true;
                    entry.Property(a => a.QuotationID).IsModified = true;
                    entry.Property(a => a.TicketClass).IsModified = true;
                    entry.Property(a => a.TicketNo).IsModified = true;
                    entry.Property(a => a.IsDeleted).IsModified = true;
                    entry.Property(a => a.IsActive).IsModified = true;
                    entry.Property(a => a.IsLowest).IsModified = true;
                    db.SaveChanges();

                    return RedirectToAction("AddQuotation", new { id = dbatquote.Quotation.RFQID });
                }
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
                TempData["ATQuotationVal"] = atquote;
            }
            return RedirectToAction("AddATQuotation", new { id = atquote.QuotationID });
        }

        public ActionResult AddHSQuotation(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                HSQuotation hsQuote = new HSQuotation();
                ViewBag.Cities = db.City.ToList();
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();
                if (TempData["HSQuotationVal"] != null)
                {
                    hsQuote = (HSQuotation)TempData["HSQuotationVal"];
                    ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".HS-Q" + hsQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".HS-Q" + hsQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                }
                else
                {
                    if (db.HSQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == false && a.IsDeleted == false).Count() > 0)
                    {
                        hsQuote = db.HSQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == false && a.IsDeleted == false).FirstOrDefault();
                        ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".HS-Q" + hsQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        ViewBag.HSfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".HS-Q" + hsQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    }
                    else
                    {
                        var tempatQuote = db.HSQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id).FirstOrDefault();
                        hsQuote.IsActive = false;
                        hsQuote.IsDeleted = false;
                        hsQuote.IsLowest = false;
                        hsQuote.CheckInDate = tempatQuote.CheckInDate;
                        hsQuote.CheckInTime = tempatQuote.CheckInTime;
                        hsQuote.CheckOutDate = tempatQuote.CheckOutDate;
                        hsQuote.CheckOutTime = tempatQuote.CheckOutTime;
                        hsQuote.HotelCategory = tempatQuote.HotelCategory;
                        hsQuote.QuotationID = tempatQuote.QuotationID;
                        hsQuote.QuotationName = tempatQuote.QuotationName;
                        hsQuote.RoomCategory = tempatQuote.RoomCategory;
                        hsQuote.RoomType = tempatQuote.RoomType;
                        hsQuote.TravelSector = tempatQuote.TravelSector;
                        hsQuote.Quotation = tempatQuote.Quotation;
                        db.HSQuotation.Add(hsQuote);
                        db.SaveChanges();

                        ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".HS-Q" + hsQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        ViewBag.HSfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".HS-Q" + hsQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    }
                    pageNo = 2;
                    quotation.HSQuotation.Add(hsQuote);
                    quotation.ATQuotation.Clear();
                    quotation.PCQuotation.Clear();

                    CheckErrorMessages();
                }
                return View(hsQuote);
            }
        }

        public ActionResult ViewHSQuotations(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();
                List<HSQuotation> hsQuotes = new List<HSQuotation>();

                ViewBag.Cities = db.City.ToList();
                if (id > 0 && quotation.HSQuotation.Count > 0)
                {
                    foreach (var item in db.HSQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == true && a.IsDeleted == false))
                    {
                        HSQuotation hsQuote = item;
                        ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".HS-Q" + hsQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        ViewBag.HSfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".HS-Q" + hsQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        hsQuotes.Add(hsQuote);
                    }
                }
                return View(hsQuotes);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddHSQuotation(HSQuotation hsquote, FormCollection formCollection)
        {
            ModelState.Remove("CheckInTime");
            ModelState.Remove("CheckOutTime");
            if (ModelState.IsValid)
            {
                using (BTCEntities db = new BTCEntities())
                {
                    if ((bool)hsquote.IsLowest && db.ATQuotation.Include(a => a.Quotation).Where(a => a.QuotationID == hsquote.QuotationID && (bool)a.IsLowest).Count() > 0)
                    {
                        TempData["ErrorMessage"] = new List<string>() { "Only One quotaiton can be the lowest" };
                        TempData["HSQuotationVal"] = hsquote;
                        return View(new { id = hsquote.QuotationID });
                    }
                    hsquote.ID = string.IsNullOrEmpty(Convert.ToString(formCollection["HSId"])) ? 0 : Convert.ToInt32(formCollection["HSId"]);
                    var dbhsquote = db.HSQuotation.Include(a => a.Quotation).Where(a => a.ID == hsquote.ID).FirstOrDefault();
                    dbhsquote.HotelName = string.IsNullOrEmpty(hsquote.HotelName) ? "" : hsquote.HotelName;
                    dbhsquote.HotelCategory = string.IsNullOrEmpty(hsquote.HotelCategory) ? "" : hsquote.HotelCategory;
                    dbhsquote.RoomCategory = string.IsNullOrEmpty(hsquote.RoomCategory) ? "" : hsquote.RoomCategory;
                    dbhsquote.RoomType = string.IsNullOrEmpty(hsquote.RoomType) ? "" : hsquote.RoomType;
                    dbhsquote.TravelSector = string.IsNullOrEmpty(hsquote.TravelSector) ? "" : hsquote.TravelSector;
                    dbhsquote.CheckInTime = string.IsNullOrEmpty(Convert.ToString(formCollection["CheckInTime"])) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(formCollection["CheckInTime"]),
                                    "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    dbhsquote.CheckOutTime = string.IsNullOrEmpty(Convert.ToString(formCollection["CheckOutTime"])) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(formCollection["CheckOutTime"]),
                                    "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    dbhsquote.Amount = hsquote.Amount;
                    dbhsquote.CheckInDate = hsquote.CheckInDate;
                    dbhsquote.CheckOutDate = hsquote.CheckOutDate;
                    dbhsquote.IsDeleted = false;
                    dbhsquote.IsActive = true;
                    dbhsquote.IsLowest = hsquote.IsLowest;

                    db.HSQuotation.Attach(dbhsquote);
                    var entry = db.Entry(dbhsquote);
                    entry.Property(a => a.Amount).IsModified = true;
                    entry.Property(a => a.CheckInDate).IsModified = true;
                    entry.Property(a => a.CheckInTime).IsModified = true;
                    entry.Property(a => a.CheckOutDate).IsModified = true;
                    entry.Property(a => a.CheckOutTime).IsModified = true;
                    entry.Property(a => a.HotelCategory).IsModified = true;
                    entry.Property(a => a.HotelName).IsModified = true;
                    entry.Property(a => a.IsActive).IsModified = true;
                    entry.Property(a => a.IsDeleted).IsModified = true;
                    entry.Property(a => a.RoomCategory).IsModified = true;
                    entry.Property(a => a.RoomType).IsModified = true;
                    entry.Property(a => a.TravelSector).IsModified = true;
                    entry.Property(a => a.IsLowest).IsModified = true;
                    db.SaveChanges();

                    return RedirectToAction("AddQuotation", new { id = dbhsquote.Quotation.RFQID });
                }
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
                TempData["HSQuotationVal"] = hsquote;
            }
            return RedirectToAction("AddHSQuotation", new { id = hsquote.QuotationID });
        }

        public ActionResult AddPCQuotation(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                PCQuotation pcQuote = new PCQuotation();
                ViewBag.Cities = db.City.ToList();
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();
                if (TempData["PCQuotationVal"] != null)
                {
                    pcQuote = (PCQuotation)TempData["PCQuotationVal"];
                    ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".PC-Q" + pcQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".PC-Q" + pcQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                }
                else
                {
                    if (db.PCQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == false && a.IsDeleted == false).Count() > 0)
                    {
                        pcQuote = db.PCQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == false && a.IsDeleted == false).FirstOrDefault();
                        ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".PC-Q" + pcQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".PC-Q" + pcQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    }
                    else
                    {
                        var tempatQuote = db.PCQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id).FirstOrDefault();
                        pcQuote.IsActive = false;
                        pcQuote.IsDeleted = false;
                        pcQuote.IsLowest = false;
                        pcQuote.DropOffDate = tempatQuote.DropOffDate;
                        pcQuote.DropoffLocation = tempatQuote.DropoffLocation;
                        pcQuote.DropOffTime = tempatQuote.DropOffTime;
                        pcQuote.PickUpDate = tempatQuote.PickUpDate;
                        pcQuote.PickupLocation = tempatQuote.PickupLocation;
                        pcQuote.PickUpTime = tempatQuote.PickUpTime;
                        pcQuote.PreferredVehicle = tempatQuote.PreferredVehicle;
                        pcQuote.QuotationID = tempatQuote.QuotationID;
                        pcQuote.QuotationName = tempatQuote.QuotationName;
                        pcQuote.TravelSector = tempatQuote.TravelSector;
                        pcQuote.Quotation = tempatQuote.Quotation;
                        db.PCQuotation.Add(pcQuote);
                        db.SaveChanges();

                        ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".PC-Q" + pcQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".PC-Q" + pcQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                    }
                    pageNo = 3;
                    quotation.PCQuotation.Add(pcQuote);
                    quotation.HSQuotation.Clear();
                    quotation.ATQuotation.Clear();

                    CheckErrorMessages();
                }
                return View(pcQuote);
            }
        }

        public ActionResult ViewPCQuotations(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();
                List<PCQuotation> pcQuotes = new List<PCQuotation>();

                ViewBag.Cities = db.City.ToList();
                if (id > 0 && quotation.ATQuotation.Count > 0)
                {
                    foreach (var item in db.PCQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id && a.IsActive == true && a.IsDeleted == false))
                    {
                        PCQuotation pcQuote = item;
                        ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".PC-Q" + pcQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == quotation.TravelRequests.ApplicationNumber + ".PC-Q" + pcQuote.ID).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                        pcQuotes.Add(pcQuote);
                    }
                }
                return View(pcQuotes);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPCQuotation(PCQuotation pcquote, FormCollection formCollection)
        {
            ModelState.Remove("PickUpTime");
            ModelState.Remove("DropOffTime");
            if (ModelState.IsValid)
            {
                using (BTCEntities db = new BTCEntities())
                {
                    if ((bool)pcquote.IsLowest && db.PCQuotation.Include(a => a.Quotation).Where(a => a.QuotationID == pcquote.QuotationID && (bool)a.IsLowest).Count() > 0)
                    {
                        TempData["ErrorMessage"] = new List<string>() { "Only One quotaiton can be the lowest" };
                        TempData["PCQuotationVal"] = pcquote;
                        return View(new { id = pcquote.QuotationID });
                    }
                    pcquote.ID = string.IsNullOrEmpty(Convert.ToString(formCollection["PCId"])) ? 0 : Convert.ToInt32(formCollection["PCId"]);
                    var dbpcquote = db.PCQuotation.Include(a => a.Quotation).Where(a => a.ID == pcquote.ID).FirstOrDefault();
                    dbpcquote.PickupLocation = string.IsNullOrEmpty(pcquote.PickupLocation) ? "" : pcquote.PickupLocation;
                    dbpcquote.DropoffLocation = string.IsNullOrEmpty(pcquote.DropoffLocation) ? "" : pcquote.DropoffLocation;
                    dbpcquote.PreferredVehicle = string.IsNullOrEmpty(pcquote.PreferredVehicle) ? "" : pcquote.PreferredVehicle;
                    dbpcquote.TravelSector = string.IsNullOrEmpty(pcquote.TravelSector) ? "" : pcquote.TravelSector;
                    dbpcquote.PickUpTime = string.IsNullOrEmpty(Convert.ToString(formCollection["PickUpTime"])) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(formCollection["PickUpTime"]),
                                    "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    dbpcquote.DropOffTime = string.IsNullOrEmpty(Convert.ToString(formCollection["DropOffTime"])) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(formCollection["DropOffTime"]),
                                    "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    dbpcquote.Amount = pcquote.Amount;
                    dbpcquote.PickUpDate = pcquote.PickUpDate;
                    dbpcquote.DropOffDate = pcquote.DropOffDate;
                    dbpcquote.IsDeleted = false;
                    dbpcquote.IsActive = true;
                    dbpcquote.IsLowest = pcquote.IsLowest;

                    db.PCQuotation.Attach(dbpcquote);
                    var entry = db.Entry(dbpcquote);
                    entry.Property(a => a.Amount).IsModified = true;
                    entry.Property(a => a.DropOffDate).IsModified = true;
                    entry.Property(a => a.DropoffLocation).IsModified = true;
                    entry.Property(a => a.DropOffTime).IsModified = true;
                    entry.Property(a => a.PickUpDate).IsModified = true;
                    entry.Property(a => a.PickupLocation).IsModified = true;
                    entry.Property(a => a.PickUpTime).IsModified = true;
                    entry.Property(a => a.IsActive).IsModified = true;
                    entry.Property(a => a.IsDeleted).IsModified = true;
                    entry.Property(a => a.PreferredVehicle).IsModified = true;
                    entry.Property(a => a.TravelSector).IsModified = true;
                    entry.Property(a => a.IsLowest).IsModified = true;
                    db.SaveChanges();

                    return RedirectToAction("AddQuotation", new { id = dbpcquote.Quotation.RFQID });
                }
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
                TempData["PCQuotationVal"] = pcquote;
            }
            return RedirectToAction("AddPCQuotation", new { id = pcquote.QuotationID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FileUpload()
        {
            using (BTCEntities db = new BTCEntities())
            {
                string attachmentsForVal = "Unknown", retrunToVal = "Index";
                int attachmentsForIDVal = 1;

                if (Request.Files != null && Request.Files.Count > 0)
                {
                    string fileName = string.Empty;
                    string destinationPath = string.Empty;
                    List<Attachments> uploadFileModel = new List<Attachments>();

                    SetQuotation(pageNo, out attachmentsForVal, out attachmentsForIDVal, out retrunToVal);

                    fileName = Path.GetFileName(Request.Files[0].FileName);
                    destinationPath = Path.Combine(Server.MapPath("~/UploadedFiles/" + attachmentsForVal) + "/", fileName);
                    Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/" + attachmentsForVal));
                    Request.Files[0].SaveAs(destinationPath);

                    var isFileNameRepete = (db.Attachments.AsEnumerable().Where(x => x.FileName == fileName).ToList());
                    if (isFileNameRepete == null || isFileNameRepete.Count <= 0)
                    {
                        Attachments attachments = new Attachments { FileName = fileName, FileType = Request.Files[0].ContentType, FilePath = destinationPath, UploadedDate = DateTime.Now, UploadedBy = dbuser.ID };
                        db.Attachments.Add(attachments);
                        db.SaveChanges();

                        AttachmentLink attachmentLink = new AttachmentLink { AttachmentFor = attachmentsForVal, AttachmentForID = attachments.ID };
                        db.AttachmentLink.Add(attachmentLink);
                        db.SaveChanges();

                        ViewBag.SuccessMessage = "File Uploaded Successfully";
                    }
                    else
                    {
                        ViewBag.WarningMessage = "File is already exists";
                    }
                }
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(attachmentsForVal)).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                return RedirectToAction(retrunToVal, new { id = attachmentsForIDVal });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUploadFile(string fileName)
        {
            using (BTCEntities db = new BTCEntities())
            {
                string attachmentsForVal = "Unknown", retrunToVal = "Index";
                int attachmentsForIDVal = 1;

                if (fileName != null && fileName != string.Empty)
                {
                    var dbfile = db.Attachments.Include(a => a.AttachmentLink).Include(a => a.Users).Where(x => x.FileName == fileName).FirstOrDefault();
                    db.AttachmentLink.RemoveRange(dbfile.AttachmentLink);
                    db.Attachments.Remove(dbfile);
                    db.SaveChanges();

                    SetQuotation(pageNo, out attachmentsForVal, out attachmentsForIDVal, out retrunToVal);

                    FileInfo file = new FileInfo(Server.MapPath("~/UploadedFiles/" + fileName));
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    ViewBag.SuccessMessage = "File Deleted Successfully";
                }
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(attachmentsForVal)).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                return RedirectToAction(retrunToVal, new { id = attachmentsForIDVal });
            }
        }

        [ValidateAntiForgeryToken]
        public ActionResult DeletePCQuotation(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var dbpcquote = db.PCQuotation.Find(id);
                dbpcquote.IsDeleted = false;

                db.PCQuotation.Attach(dbpcquote);
                var entry = db.Entry(dbpcquote);
                entry.Property(a => a.IsDeleted).IsModified = true;

                return RedirectToAction("AddQuotation", new { id = dbpcquote.Quotation.RFQID });
            }
        }

        [ValidateAntiForgeryToken]
        public ActionResult DeleteHSQuotation(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var dbhsquote = db.HSQuotation.Find(id);
                dbhsquote.IsDeleted = false;

                db.HSQuotation.Attach(dbhsquote);
                var entry = db.Entry(dbhsquote);
                entry.Property(a => a.IsDeleted).IsModified = true;

                return RedirectToAction("AddQuotation", new { id = dbhsquote.Quotation.RFQID });
            }
        }

        [ValidateAntiForgeryToken]
        public ActionResult DeleteATQuotation(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var dbatquote = db.ATQuotation.Find(id);
                dbatquote.IsDeleted = false;

                db.ATQuotation.Attach(dbatquote);
                var entry = db.Entry(dbatquote);
                entry.Property(a => a.IsDeleted).IsModified = true;

                return RedirectToAction("AddQuotation", new { id = dbatquote.Quotation.RFQID });
            }
        }

        public FileResult OpenFile(string fileName)
        {
            try
            {
                return File(new FileStream(Server.MapPath("~/UploadedFiles/" + fileName), FileMode.Open), "application/octetstream", fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckErrorMessages()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
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

        private void SetQuotation(int QuotationType, out string attachmentsForVal, out int attachmentsForIDVal, out string retrunToVal)
        {
            switch (QuotationType)
            {
                case 1:
                    attachmentsForVal = quotation.TravelRequests.ApplicationNumber + ".AT-Q" + quotation.ATQuotation.FirstOrDefault().ID;
                    attachmentsForIDVal = quotation.ID;
                    retrunToVal = "AddATQuotation";
                    break;
                case 2:
                    attachmentsForVal = quotation.TravelRequests.ApplicationNumber + ".HS-Q" + quotation.HSQuotation.FirstOrDefault().ID;
                    attachmentsForIDVal = quotation.ID;
                    retrunToVal = "AddHSQuotation";
                    break;
                case 3:
                    attachmentsForVal = quotation.TravelRequests.ApplicationNumber + ".PC-Q" + quotation.PCQuotation.FirstOrDefault().ID;
                    attachmentsForIDVal = quotation.ID;
                    retrunToVal = "AddPCQuotation";
                    break;
                default:
                    attachmentsForVal = quotation.TravelRequests.ApplicationNumber + ".AT-Q" + quotation.ATQuotation.FirstOrDefault().ID;
                    attachmentsForIDVal = quotation.ID;
                    retrunToVal = "Index";
                    break;
            }
        }
    }
}