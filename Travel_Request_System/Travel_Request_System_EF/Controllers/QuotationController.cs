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

                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                var RFQ = db.RFQ.Include(r => r.TravelAgency).Include(r => r.TravelRequests).Include(r => r.Users);
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
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(rFQ.TravelRequests.ApplicationNumber)).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                return View(rFQ);
            }
        }

        public ActionResult AddATQuotation(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();

                ViewBag.Cities = db.City.ToList();
                ATQuotation atQuote;
                if (id == null || id == 0 || db.ATQuotation.Where(a => a.QuotationID == id).Count() == 0)
                {
                    atQuote = new ATQuotation() { QuotationID = (int)id };
                }
                else
                {
                    atQuote = db.ATQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id).FirstOrDefault();
                    ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(quotation.TravelRequests.ApplicationNumber + "/AT-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();

                }
                pageNo = 1;
                quotation.ATQuotation.Add(atQuote);
                quotation.HSQuotation.Clear();
                quotation.PCQuotation.Clear();
                return View(atQuote);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddATQuotation(ATQuotation atquote)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var dbatquote = db.ATQuotation.Find(atquote.ID);
                dbatquote.Airlines = string.IsNullOrEmpty(atquote.Airlines) ? "" : atquote.Airlines;
                dbatquote.TicketClass = string.IsNullOrEmpty(atquote.TicketClass) ? "" : atquote.TicketClass;
                dbatquote.TicketNo = string.IsNullOrEmpty(atquote.TicketNo) ? "" : atquote.TicketNo;

                dbatquote.DepartureTime = string.IsNullOrEmpty(Convert.ToString(atquote.DepartureTime)) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(atquote.DepartureTime),
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                dbatquote.ReturnTime = string.IsNullOrEmpty(Convert.ToString(atquote.ReturnTime)) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(atquote.ReturnTime),
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
                db.SaveChanges();

                return RedirectToAction("AddQuotation", new { id = dbatquote.Quotation.RFQID });
            }
        }

        public ActionResult AddHSQuotation(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();

                ViewBag.Cities = db.City.ToList();
                HSQuotation hsQuote;
                if (id == null || id == 0 || db.HSQuotation.Where(a => a.QuotationID == id).Count() == 0)
                {
                    hsQuote = new HSQuotation() { QuotationID = (int)id };
                }
                else
                {
                    hsQuote = db.HSQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id).FirstOrDefault();
                    ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(quotation.TravelRequests.ApplicationNumber + "/HS-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();

                }
                pageNo = 1;
                quotation.HSQuotation.Add(hsQuote);
                quotation.ATQuotation.Clear();
                quotation.PCQuotation.Clear();
                return View(hsQuote);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddHSQuotation(HSQuotation hsquote)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var dbhsquote = db.HSQuotation.Find(hsquote.ID);
                dbhsquote.HotelName = string.IsNullOrEmpty(hsquote.HotelName) ? "" : hsquote.HotelName;
                dbhsquote.HotelCategory = string.IsNullOrEmpty(hsquote.HotelCategory) ? "" : hsquote.HotelCategory;
                dbhsquote.RoomCategory = string.IsNullOrEmpty(hsquote.RoomCategory) ? "" : hsquote.RoomCategory;
                dbhsquote.RoomType = string.IsNullOrEmpty(hsquote.RoomType) ? "" : hsquote.RoomType;
                dbhsquote.TravelSector = string.IsNullOrEmpty(hsquote.TravelSector) ? "" : hsquote.TravelSector;
                dbhsquote.CheckInTime = string.IsNullOrEmpty(Convert.ToString(hsquote.CheckInTime)) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(hsquote.CheckInTime),
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                dbhsquote.CheckOutTime = string.IsNullOrEmpty(Convert.ToString(hsquote.CheckOutTime)) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(hsquote.CheckOutTime),
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                dbhsquote.Amount = hsquote.Amount;
                dbhsquote.CheckInDate = hsquote.CheckInDate;
                dbhsquote.CheckOutDate = hsquote.CheckOutDate;
                dbhsquote.IsDeleted = false;
                dbhsquote.IsActive = true;

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
                db.SaveChanges();

                return RedirectToAction("AddQuotation", new { id = dbhsquote.Quotation.RFQID });
            }
        }

        public ActionResult AddPCQuotation(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                quotation = db.Quotation.Include(a => a.TravelRequests).Where(x => x.ID == id).FirstOrDefault();

                ViewBag.Cities = db.City.ToList();
                PCQuotation pcQuote;
                if (id == null || id == 0 || db.PCQuotation.Where(a => a.QuotationID == id).Count() == 0)
                {
                    pcQuote = new PCQuotation() { QuotationID = (int)id };
                }
                else
                {
                    pcQuote = db.PCQuotation.Include(x => x.Quotation).Include(x => x.Quotation.TravelRequests).Where(a => a.QuotationID == id).FirstOrDefault();
                    ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(quotation.TravelRequests.ApplicationNumber + "/PC-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();

                }
                pageNo = 1;
                quotation.PCQuotation.Add(pcQuote);
                quotation.ATQuotation.Clear();
                quotation.HSQuotation.Clear();
                return View(pcQuote);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPCQuotation(PCQuotation pcquote)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var dbpcquote = db.PCQuotation.Find(pcquote.ID);
                dbpcquote.PickupLocation = string.IsNullOrEmpty(pcquote.PickupLocation) ? "" : pcquote.PickupLocation;
                dbpcquote.DropoffLocation = string.IsNullOrEmpty(pcquote.DropoffLocation) ? "" : pcquote.DropoffLocation;
                dbpcquote.PreferredVehicle = string.IsNullOrEmpty(pcquote.PreferredVehicle) ? "" : pcquote.PreferredVehicle;
                dbpcquote.TravelSector = string.IsNullOrEmpty(pcquote.TravelSector) ? "" : pcquote.TravelSector;
                dbpcquote.PickUpTime = string.IsNullOrEmpty(Convert.ToString(pcquote.PickUpTime)) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(pcquote.PickUpTime),
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                dbpcquote.DropOffTime = string.IsNullOrEmpty(Convert.ToString(pcquote.DropOffTime)) ? new TimeSpan() : DateTime.ParseExact(Convert.ToString(pcquote.DropOffTime),
                                "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                dbpcquote.Amount = pcquote.Amount;
                dbpcquote.PickUpDate = pcquote.PickUpDate;
                dbpcquote.DropOffDate = pcquote.DropOffDate;
                dbpcquote.IsDeleted = false;
                dbpcquote.IsActive = true;

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
                db.SaveChanges();

                return RedirectToAction("AddQuotation", new { id = dbpcquote.Quotation.RFQID });
            }
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

                    setQuotation(pageNo, out attachmentsForVal, out attachmentsForIDVal, out retrunToVal);

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

                    setQuotation(pageNo, out attachmentsForVal, out attachmentsForIDVal, out retrunToVal);

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

        private void setQuotation(int QuotationType, out string attachmentsForVal, out int attachmentsForIDVal, out string retrunToVal)
        {
            switch (QuotationType)
            {
                case 1:
                    attachmentsForVal = quotation.TravelRequests.ApplicationNumber + ".AT-Q";
                    attachmentsForIDVal = quotation.ID;
                    retrunToVal = "AddATQuotation";
                    break;
                case 2:
                    attachmentsForVal = quotation.TravelRequests.ApplicationNumber + ".HS-Q";
                    attachmentsForIDVal = quotation.ID;
                    retrunToVal = "AddHSQuotation";
                    break;
                case 3:
                    attachmentsForVal = quotation.TravelRequests.ApplicationNumber + ".AT-Q";
                    attachmentsForIDVal = quotation.ID;
                    retrunToVal = "AddPCQuotation";
                    break;
                default:
                    attachmentsForVal = quotation.TravelRequests.ApplicationNumber + ".AT-Q";
                    attachmentsForIDVal = quotation.ID;
                    retrunToVal = "Index";
                    break;
            }
        }
    }
}