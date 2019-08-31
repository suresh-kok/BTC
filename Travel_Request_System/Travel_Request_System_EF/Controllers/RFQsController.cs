using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
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
    public class RFQsController : Controller
    {
        private static RFQ MasterRFQ = new RFQ();
        private static MembershipUser user;
        private static Users dbuser;
        private static string[] roles;
        private static string empCode;

        public RFQsController()
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

        // GET: RFQ
        public async Task<ActionResult> Index()
        {
            using (BTCEntities db = new BTCEntities())
            {
                //var RFQ = db.RFQ.Include(r => r.TravelAgency).Include(r => r.TravelRequests).Include(r => r.Users);
                //return View(await RFQ.ToListAsync());

                var travelRequests = db.TravelRequests.Include(t => t.City).Include(t => t.City1).Include(t => t.Currency).Include(t => t.RFQ).Include(t => t.Users).Include(t => t.Users1).Include(t => t.RFQ).Include("RFQ.TravelAgency");
                return View(await travelRequests.ToListAsync());
            }
        }

        // POST: RFQ/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TravelAgencyID,TravelRequestID,UserID,Remarks,Processing,ProcessingSection,IsDeleted")] RFQ rFQ)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (ModelState.IsValid)
                {
                    rFQ.RFQName = db.TravelRequests.Where(a => a.ID == rFQ.TravelRequestID).FirstOrDefault().ApplicationNumber;
                    db.RFQ.Add(rFQ);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.TravelAgencyID = new SelectList(db.TravelAgency, "AgencyID", "AgencyCode", rFQ.TravelAgencyID);
                ViewBag.TravelRequestID = new SelectList(db.TravelRequests, "TravelRequestID", "ApplicationNumber", rFQ.TravelRequestID);
                ViewBag.UserID = new SelectList(db.Users, "UserId", "Username", rFQ.UserID);
                return View(rFQ);
            }
        }

        // GET: RFQ/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RFQ rFQ = await db.RFQ.FindAsync(id);
                if (rFQ == null)
                {
                    return HttpNotFound();
                }
                ViewBag.TravelAgencyID = new SelectList(db.TravelAgency, "AgencyID", "AgencyCode", rFQ.TravelAgencyID);
                ViewBag.TravelRequestID = new SelectList(db.TravelRequests, "TravelRequestID", "ApplicationNumber", rFQ.TravelRequestID);
                ViewBag.UserID = new SelectList(db.Users, "UserId", "Username", rFQ.UserID);
                return View(rFQ);
            }
        }

        // POST: RFQ/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,TravelAgencyID,TravelRequestID,UserID,Remarks,Processing,ProcessingSection,IsDeleted")] RFQ rFQ)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(rFQ).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.TravelAgencyID = new SelectList(db.TravelAgency, "AgencyID", "AgencyCode", rFQ.TravelAgencyID);
                ViewBag.TravelRequestID = new SelectList(db.TravelRequests, "TravelRequestID", "ApplicationNumber", rFQ.TravelRequestID);
                ViewBag.UserID = new SelectList(db.Users, "UserId", "Username", rFQ.UserID);
                return View(rFQ);
            }
        }

        // GET: RFQ/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RFQ rFQ = await db.RFQ.FindAsync(id);
                if (rFQ == null)
                {
                    return HttpNotFound();
                }
                return View(rFQ);
            }
        }

        // POST: RFQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                RFQ rFQ = await db.RFQ.FindAsync(id);
                db.RFQ.Remove(rFQ);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        [HttpGet]
        public async Task<ActionResult> RFQProcessing(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                ViewBag.TravelAgency = db.TravelAgency.ToList();

                TravelRequests travelRequest = await db.TravelRequests.Include(a => a.RFQ).Where(a => a.ID == id).FirstOrDefaultAsync();

                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService("", travelRequest.Users1.HREmployeeID.ToString()))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }

                ViewBag.AvailableCombinations = db.RFQ.Where(a => a.TravelRequestID == id && a.IsDeleted == false).ToList();

                return View(travelRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RFQProcessing(TravelRequests travelRequest, FormCollection formCollection)
        {
            RFQ rfq = new RFQ
            {
                TravelRequestID = travelRequest.ID
            };

            if (formCollection.AllKeys.Contains("checkAll") && formCollection["checkAll"].ToString().ToLower() == "on")
            {
                rfq.ProcessingSection = (int)ProcessingSections.ATHSPC;
            }
            else
            {
                if ((formCollection.AllKeys.Contains("ATCheck") && formCollection["ATCheck"].ToString().ToLower() == "on") && (formCollection.AllKeys.Contains("HSCheck") && formCollection["HSCheck"].ToString().ToLower() == "on"))
                {
                    rfq.ProcessingSection = (int)ProcessingSections.ATHS;
                }
                else if ((formCollection.AllKeys.Contains("ATCheck") && formCollection["ATCheck"].ToString().ToLower() == "on") && (formCollection.AllKeys.Contains("PCCheck") && formCollection["PCCheck"].ToString().ToLower() == "on"))
                {
                    rfq.ProcessingSection = (int)ProcessingSections.ATPC;
                }
                else if ((formCollection.AllKeys.Contains("PCCheck") && formCollection["PCCheck"].ToString().ToLower() == "on") && (formCollection.AllKeys.Contains("HSCheck") && formCollection["HSCheck"].ToString().ToLower() == "on"))
                {
                    rfq.ProcessingSection = (int)ProcessingSections.HSPC;
                }
                else if ((formCollection.AllKeys.Contains("ATCheck") && formCollection["ATCheck"].ToString().ToLower() == "on"))
                {
                    rfq.ProcessingSection = (int)ProcessingSections.AT;
                } 
                else if ((formCollection.AllKeys.Contains("HSCheck") && formCollection["HSCheck"].ToString().ToLower() == "on"))
                {
                    rfq.ProcessingSection = (int)ProcessingSections.HS;
                }
                else if ((formCollection.AllKeys.Contains("PCCheck") && formCollection["PCCheck"].ToString().ToLower() == "on"))
                {
                    rfq.ProcessingSection = (int)ProcessingSections.PC;
                }
            }
            rfq.Processing = (int)ProcessingStatus.NotProcessed;
            rfq.IsDeleted = false;
            rfq.UserID = dbuser.ID;
            rfq.TravelAgencyID = Convert.ToInt32((formCollection["TravelAgencySelected"].ToString()).Split(',')[0]);

            using (BTCEntities db = new BTCEntities())
            {
                if (db.RFQ.Where(a => a.ProcessingSection == rfq.ProcessingSection && a.TravelRequestID == travelRequest.ID && a.TravelAgencyID == rfq.TravelAgencyID && a.IsDeleted == false).Count() < 1 && rfq.ProcessingSection != 0)
                {
                    if (db.RFQ.Where(a => a.ProcessingSection == rfq.ProcessingSection && a.TravelRequestID == travelRequest.ID && a.TravelAgencyID == rfq.TravelAgencyID).Count() < 1)
                    {
                        rfq.RFQName = db.TravelRequests.Where(a => a.ID == travelRequest.ID).FirstOrDefault().ApplicationNumber;
                        db.RFQ.Add(rfq);
                    }
                    else
                    {
                        RFQ rfqlist = db.RFQ.Where(a => a.ProcessingSection == rfq.ProcessingSection && a.TravelRequestID == travelRequest.ID && a.TravelAgencyID == rfq.TravelAgencyID).FirstOrDefault();
                        rfqlist.IsDeleted = false;

                        db.RFQ.Attach(rfqlist);
                        var entry = db.Entry(rfqlist);
                        entry.Property(a => a.IsDeleted).IsModified = true;
                    }
                    db.SaveChanges();
                }
                else if (rfq.ProcessingSection == 0)
                {
                    ViewBag.ErrorMessage = new List<string>() { "Selet Processing Section" };
                }
                else
                {
                    ViewBag.ErrorMessage = new List<string>() { "Record already exists" };
                }

                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                ViewBag.TravelAgency = db.TravelAgency.ToList();

                travelRequest = db.TravelRequests.Include(a => a.RFQ).Where(a => a.ID == rfq.TravelRequestID).FirstOrDefault();

                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService("", travelRequest.Users1.HREmployeeID.ToString()))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }

                ViewBag.AvailableCombinations = db.RFQ.Where(a => a.TravelRequestID == rfq.TravelRequestID && a.IsDeleted == false).ToList();
                return View(travelRequest);
            }
        }

        public ActionResult RemoveRFQ(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                RFQ rfqlist = db.RFQ.Where(a => a.ID == id).FirstOrDefault();

                if (ModelState.IsValid)
                {
                    rfqlist.IsDeleted = true;

                    db.RFQ.Attach(rfqlist);
                    var entry = db.Entry(rfqlist);
                    entry.Property(a => a.IsDeleted).IsModified = true;
                    db.SaveChanges();
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

                int TravelRequestID = rfqlist.TravelRequestID.Value;

                return RedirectToAction("RFQProcessing", new { id = TravelRequestID });
            }
        }

        public ActionResult RFQMerger(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                List<RFQ> rfqlist = db.RFQ.Include(a => a.TravelRequests).Include(a => a.Users).Include(a => a.TravelAgency).Include(a => a.LPO).Where(a => a.TravelRequestID == id && a.Processing == 0 && a.IsDeleted == false).ToList();
                ViewBag.TravelAgency = db.TravelAgency.ToList();

                return View(rfqlist);
            }
        }

        public ActionResult DeleteRFQProcessing(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var rfqVal = db.RFQ.Where(a => a.ID == id).FirstOrDefault();
                var travelRequestID = rfqVal.TravelRequestID;

                rfqVal.IsDeleted = true;
                db.RFQ.Add(rfqVal);
                db.Entry(rfqVal).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.SuccessMessage = "Entry Marked for Deletion";
                if (db.RFQ.Where(a => a.TravelRequestID == travelRequestID && a.Processing == 0 && a.IsDeleted == false).Count() <= 0)
                {
                    return RedirectToAction("RFQProcessing", new { id = travelRequestID });
                }
                return RedirectToAction("RFQMerger", new { id = rfqVal.TravelRequestID });
            }
        }

        public ActionResult RFQPostMerger(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                List<RFQ> rfqlist = db.RFQ.Include(a => a.TravelRequests).Include(a => a.Users).Include(a => a.TravelAgency).Include(a => a.LPO).Where(a => a.TravelRequestID == id && a.Processing > 0).ToList();
                ViewBag.TravelAgency = db.TravelAgency.ToList();

                return View(rfqlist);
            }
        }

        public async Task<ActionResult> RFQFinalPreview(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                RFQ rfqlist = await db.RFQ.Include(a => a.TravelRequests).Include(a => a.TravelAgency).Include(a => a.Users).Where(a => a.ID == id).FirstOrDefaultAsync();
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == (rfqlist.RFQName + "Pro" + rfqlist.ProcessingSection + "Trav" + rfqlist.TravelAgencyID)).Select(x => x.Attachments).ToList();
                MasterRFQ = rfqlist;
                if (rfqlist.ProcessingSection == (int)ProcessingSections.AT || rfqlist.ProcessingSection == (int)ProcessingSections.ATHS || rfqlist.ProcessingSection == (int)ProcessingSections.ATHSPC || rfqlist.ProcessingSection == (int)ProcessingSections.ATPC)
                {
                    ATQuotation atquot = new ATQuotation();
                    atquot.DestinationID = rfqlist.TravelRequests.PortOfDestinationID;
                    atquot.DepartureDate = rfqlist.TravelRequests.DepartureDate;
                    atquot.DepartureTime = rfqlist.TravelRequests.DepartureTime;
                    atquot.OriginID = rfqlist.TravelRequests.PortOfOriginID;
                    atquot.ReturnDate = rfqlist.TravelRequests.ReturnDate;
                    atquot.ReturnTime = rfqlist.TravelRequests.ReturnTime;
                    atquot.TicketClass = rfqlist.TravelRequests.TicketClass;
                    ViewBag.ATQuotation = atquot;
                }
                if (rfqlist.ProcessingSection == (int)ProcessingSections.ATHS || rfqlist.ProcessingSection == (int)ProcessingSections.ATHSPC || rfqlist.ProcessingSection == (int)ProcessingSections.HS || rfqlist.ProcessingSection == (int)ProcessingSections.HSPC)
                {
                    HSQuotation hsquot = new HSQuotation();
                    hsquot.CheckInDate = rfqlist.TravelRequests.CheckInDate;
                    hsquot.CheckInTime = rfqlist.TravelRequests.CheckInTime;
                    hsquot.CheckOutDate = rfqlist.TravelRequests.CheckOutDate;
                    hsquot.CheckOutTime = rfqlist.TravelRequests.CheckOutTime;
                    hsquot.HotelCategory = rfqlist.TravelRequests.HotelCategory;
                    hsquot.HotelName = rfqlist.TravelRequests.HotelName;
                    hsquot.RoomCategory = rfqlist.TravelRequests.RoomCategory;
                    hsquot.RoomType = rfqlist.TravelRequests.RoomType;
                    ViewBag.HSQuotation = hsquot;
                }
                if (rfqlist.ProcessingSection == (int)ProcessingSections.ATHSPC || rfqlist.ProcessingSection == (int)ProcessingSections.ATPC || rfqlist.ProcessingSection == (int)ProcessingSections.HSPC || rfqlist.ProcessingSection == (int)ProcessingSections.PC)
                {
                    PCQuotation pcquot = new PCQuotation();
                    pcquot.DropOffDate = rfqlist.TravelRequests.DropOffDate;
                    pcquot.DropoffLocation = rfqlist.TravelRequests.DropOffLocation;
                    pcquot.DropOffTime = rfqlist.TravelRequests.DropOffTime;
                    pcquot.PickUpDate = rfqlist.TravelRequests.PickUpDate;
                    pcquot.PickupLocation = rfqlist.TravelRequests.PickUpLocation;
                    pcquot.PickUpTime = rfqlist.TravelRequests.PickUpTime;
                    pcquot.PreferredVehicle = rfqlist.TravelRequests.PreferredVehicle;
                    ViewBag.PCQuotation = pcquot;
                }

                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService("", rfqlist.TravelRequests.Users1.HREmployeeID.ToString()))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                return View(rfqlist);
            }
        }

        public async Task<ActionResult> PreviewRFQ(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();


                RFQ rfqitem = await db.RFQ.Include(a => a.TravelRequests).Include(a => a.TravelAgency).Include(a => a.Users).Where(a => a.ID == id).FirstOrDefaultAsync();
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == (rfqitem.RFQName + "Pro" + rfqitem.ProcessingSection + "Trav" + rfqitem.TravelAgencyID)).Select(x => x.Attachments).ToList();
                MasterRFQ = rfqitem;
                if (rfqitem.ProcessingSection == (int)ProcessingSections.AT || rfqitem.ProcessingSection == (int)ProcessingSections.ATHS || rfqitem.ProcessingSection == (int)ProcessingSections.ATHSPC || rfqitem.ProcessingSection == (int)ProcessingSections.ATPC)
                {
                    ATQuotation atquot = new ATQuotation();
                    atquot.DestinationID = rfqitem.TravelRequests.PortOfDestinationID;
                    atquot.DepartureDate = rfqitem.TravelRequests.DepartureDate;
                    atquot.DepartureTime = rfqitem.TravelRequests.DepartureTime;
                    atquot.OriginID = rfqitem.TravelRequests.PortOfOriginID;
                    atquot.ReturnDate = rfqitem.TravelRequests.ReturnDate;
                    atquot.ReturnTime = rfqitem.TravelRequests.ReturnTime;
                    atquot.TicketClass = rfqitem.TravelRequests.TicketClass;
                    ViewBag.ATQuotation = atquot;
                }
                if (rfqitem.ProcessingSection == (int)ProcessingSections.ATHS || rfqitem.ProcessingSection == (int)ProcessingSections.ATHSPC || rfqitem.ProcessingSection == (int)ProcessingSections.HS || rfqitem.ProcessingSection == (int)ProcessingSections.HSPC)
                {
                    HSQuotation hsquot = new HSQuotation();
                    hsquot.CheckInDate = rfqitem.TravelRequests.CheckInDate;
                    hsquot.CheckInTime= rfqitem.TravelRequests.CheckInTime;
                    hsquot.CheckOutDate = rfqitem.TravelRequests.CheckOutDate;
                    hsquot.CheckOutTime = rfqitem.TravelRequests.CheckOutTime;
                    hsquot.HotelCategory = rfqitem.TravelRequests.HotelCategory;
                    hsquot.HotelName = rfqitem.TravelRequests.HotelName;
                    hsquot.RoomCategory = rfqitem.TravelRequests.RoomCategory;
                    hsquot.RoomType = rfqitem.TravelRequests.RoomType;
                    ViewBag.HSQuotation = hsquot;
                }
                if (rfqitem.ProcessingSection == (int)ProcessingSections.ATHSPC || rfqitem.ProcessingSection == (int)ProcessingSections.ATPC || rfqitem.ProcessingSection == (int)ProcessingSections.HSPC || rfqitem.ProcessingSection == (int)ProcessingSections.PC)
                {
                    PCQuotation pcquot = new PCQuotation();
                    pcquot.DropOffDate = rfqitem.TravelRequests.DropOffDate;
                    pcquot.DropoffLocation = rfqitem.TravelRequests.DropOffLocation;
                    pcquot.DropOffTime= rfqitem.TravelRequests.DropOffTime;
                    pcquot.PickUpDate= rfqitem.TravelRequests.PickUpDate;
                    pcquot.PickupLocation= rfqitem.TravelRequests.PickUpLocation;
                    pcquot.PickUpTime = rfqitem.TravelRequests.PickUpTime;
                    pcquot.PreferredVehicle= rfqitem.TravelRequests.PreferredVehicle;
                    ViewBag.PCQuotation = pcquot;
                }
                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService("", rfqitem.TravelRequests.Users1.HREmployeeID.ToString()))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                return View(rfqitem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreviewRFQ(RFQ rfq)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (ModelState.IsValid)
                {
                    var dbrfq = db.RFQ.Find(rfq.ID);

                    dbrfq.Processing = (int)ProcessingStatus.BeingProcessed;
                    dbrfq.UserID = dbuser.ID;
                    dbrfq.Remarks = rfq.Remarks;
                    dbrfq.IsDeleted = rfq.IsDeleted;

                    db.RFQ.Attach(dbrfq);
                    var entry = db.Entry(dbrfq);
                    entry.State = EntityState.Modified;
                    entry.Property(a => a.Remarks).IsModified = true;
                    entry.Property(a => a.UserID).IsModified = true;
                    entry.Property(a => a.Processing).IsModified = true;
                    entry.Property(a => a.IsDeleted).IsModified = true;
                    db.SaveChanges();

                    Quotation quote = new Quotation
                    {
                        TravelRequestID = (int)dbrfq.TravelRequestID,
                        IsDeleted = true,
                        RFQID = rfq.ID
                    };

                    db.Quotation.Add(quote);
                    db.SaveChanges();

                    db.Configuration.ValidateOnSaveEnabled = false;

                    var travelreq = db.TravelRequests.Where(a => a.ID == rfq.TravelRequestID).FirstOrDefault();

                    ATQuotation ATquote = new ATQuotation()
                    {
                        QuotationID = quote.ID,
                        QuotationName = travelreq.ApplicationNumber + " / AT - Q",
                        City = travelreq.City,
                        City1 = travelreq.City1,
                        DepartureDate = travelreq.DepartureDate,
                        DepartureTime = travelreq.DepartureTime,
                        DestinationID = travelreq.City.ID,
                        OriginID = travelreq.City1.ID,
                        ReturnDate = travelreq.ReturnDate,
                        ReturnTime = travelreq.ReturnTime,
                        TicketClass = travelreq.TicketClass,
                        IsActive = false,
                        IsDeleted = false
                    };
                    db.ATQuotation.Add(ATquote);
                    HSQuotation HSquote = new HSQuotation()
                    {
                        QuotationID = quote.ID,
                        QuotationName = db.TravelRequests.Where(a => a.ID == rfq.TravelRequestID).FirstOrDefault().ApplicationNumber + " / HS - Q",
                        CheckInDate = travelreq.CheckInDate,
                        CheckInTime = travelreq.CheckInTime,
                        CheckOutDate = travelreq.CheckOutDate,
                        CheckOutTime = travelreq.CheckOutTime,
                        HotelCategory = travelreq.HotelCategory,
                        RoomCategory = travelreq.RoomCategory,
                        RoomType = travelreq.RoomType,
                        IsActive = false,
                        IsDeleted = false
                    };
                    db.HSQuotation.Add(HSquote);
                    PCQuotation PCquote = new PCQuotation()
                    {
                        QuotationID = quote.ID,
                        QuotationName = db.TravelRequests.Where(a => a.ID == rfq.TravelRequestID).FirstOrDefault().ApplicationNumber + " / PC - Q",
                        DropOffDate = travelreq.DropOffDate,
                        DropoffLocation = travelreq.DropOffLocation,
                        DropOffTime = travelreq.DropOffTime,
                        PickUpDate = travelreq.PickUpDate,
                        PickupLocation = travelreq.PickUpLocation,
                        PickUpTime = travelreq.PickUpTime,
                        PreferredVehicle = travelreq.PreferredVehicle,
                        IsActive = false,
                        IsDeleted = false
                    };
                    db.PCQuotation.Add(PCquote);
                    db.SaveChanges();

                    MasterRFQ = dbrfq;
                    return RedirectToAction("RFQPostMerger", new { id = rfq.TravelRequestID });
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

                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                RFQ rfqitem = db.RFQ.Include(a => a.TravelRequests).Include(a => a.TravelAgency).Include(a => a.Users).Where(a => a.ID == rfq.ID).FirstOrDefault();
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == (rfqitem.RFQName + "Pro" + rfqitem.ProcessingSection + "Trav" + rfqitem.TravelAgencyID)).Select(x => x.Attachments).ToList();
                MasterRFQ = rfqitem;
                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService("", rfqitem.TravelRequests.Users1.HREmployeeID.ToString()))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                return View(rfqitem);
            }
        }

        public ActionResult DeleteRFQ(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                RFQ rfqitem = db.RFQ.Include(a => a.TravelRequests).Include(a => a.TravelAgency).Include(a => a.Users).Where(a => a.ID == id).FirstOrDefault();

                if (ModelState.IsValid)
                {
                    rfqitem.IsDeleted = true;

                    db.RFQ.Attach(rfqitem);
                    var entry = db.Entry(rfqitem);
                    entry.Property(a => a.IsDeleted).IsModified = true;
                    db.SaveChanges();
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

                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == (rfqitem.RFQName + "Pro" + rfqitem.ProcessingSection + "Trav" + rfqitem.TravelAgencyID)).Select(x => x.Attachments).ToList();
                MasterRFQ = rfqitem;
                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService("", rfqitem.TravelRequests.Users1.HREmployeeID.ToString()))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                return View(rfqitem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FileUpload(FormCollection formCollection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    string fileName = string.Empty;
                    string destinationPath = string.Empty;
                    List<Attachments> uploadFileModel = new List<Attachments>();

                    fileName = Path.GetFileName(Request.Files[0].FileName);
                    if (formCollection.AllKeys.Contains("RFQname") && !string.IsNullOrEmpty(formCollection["RFQname"].ToString()) && formCollection.AllKeys.Contains("RFQSections") && !string.IsNullOrEmpty(formCollection["RFQSections"].ToString()))
                    {
                        destinationPath = Path.Combine(Server.MapPath("~/UploadedFiles/" + MasterRFQ.RFQName + "Pro" + MasterRFQ.ProcessingSection + "Trav" + MasterRFQ.TravelAgencyID) + "/", fileName);
                        Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/" + MasterRFQ.RFQName + "Pro" + MasterRFQ.ProcessingSection + "Trav" + MasterRFQ.TravelAgencyID));
                        Request.Files[0].SaveAs(destinationPath);


                        var isFileNameRepete = (db.Attachments.AsEnumerable().Where(x => x.FileName == destinationPath).ToList());
                        if (isFileNameRepete == null || isFileNameRepete.Count <= 0)
                        {
                            Attachments attachments = new Attachments { FileName = fileName, FileType = Request.Files[0].ContentType, FilePath = destinationPath, UploadedDate = DateTime.Now, UploadedBy = dbuser.ID };
                            db.Attachments.Add(attachments);
                            db.SaveChanges();

                            AttachmentLink attachmentLink = new AttachmentLink { AttachmentFor = formCollection["RFQname"].ToString() + "Pro" + formCollection["RFQSections"].ToString() + "Trav" + MasterRFQ.TravelAgencyID, AttachmentForID = attachments.ID };
                            db.AttachmentLink.Add(attachmentLink);
                            db.SaveChanges();

                            ViewBag.SuccessMessage = "File Uploaded Successfully";
                        }
                        else
                        {
                            ViewBag.WarningMessage = "File is already exists";
                        }
                    }
                }
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == (MasterRFQ.RFQName + "Pro" + MasterRFQ.ProcessingSection + "Trav" + MasterRFQ.TravelAgencyID)).Select(x => x.Attachments).ToList();
                return RedirectToAction("PreviewRFQ", new { id = MasterRFQ.ID });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUploadFile(string fileName)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (fileName != null && fileName != string.Empty)
                {
                    var dbfile = db.Attachments.Include(a => a.AttachmentLink).Include(a => a.Users).Where(x => x.FileName == fileName).FirstOrDefault();
                    db.AttachmentLink.RemoveRange(dbfile.AttachmentLink);
                    db.Attachments.Remove(dbfile);
                    db.SaveChanges();

                    var destinationPath = Path.Combine(Server.MapPath("~/UploadedFiles/" + MasterRFQ.RFQName + "Pro" + MasterRFQ.ProcessingSection + "Trav" + MasterRFQ.TravelAgencyID) + "/", fileName);

                    FileInfo file = new FileInfo(destinationPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    ViewBag.SuccessMessage = "File Deleted Successfully";
                }
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor == (MasterRFQ.RFQName + "Pro" + MasterRFQ.ProcessingSection + "Trav" + MasterRFQ.TravelAgencyID)).Select(x => x.Attachments).ToList();
                return RedirectToAction("PreviewRFQ", new { id = MasterRFQ.ID });
            }
        }

        public FileResult OpenFile(string fileName)
        {
            try
            {
                var destinationPath = Path.Combine(Server.MapPath("~/UploadedFiles/" + MasterRFQ.RFQName + "Pro" + MasterRFQ.ProcessingSection + "Trav" + MasterRFQ.TravelAgencyID) + "/", fileName);

                return File(new FileStream(destinationPath, FileMode.Open), "application/octetstream", fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PrintRFQ(int id)
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

            adp = new SqlDataAdapter("select * FROM RFQDetails WHERE RFQID = " + id, conx);
            ds = new BTCDataSet();
            adp.Fill(ds, ds.RFQDetails.TableName);
            reportViewer.LocalReport.ReportPath = Path.Combine(@"C:\Users\kanniyappans\Documents\GitHub\BTC\Travel_Request_System\Travel_Request_System_EF\Reports\", "RFQReport.rdlc");
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("detailsDataset", ds.Tables["RFQDetails"]));
            bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename= outputreport" + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();
        }

        public async Task<ActionResult> TravelRequestsList()
        {
            using (BTCEntities db = new BTCEntities())
            {
                var travelRequests = db.TravelRequests.Include(t => t.City).Include(t => t.City1).Include(t => t.Currency).Include(t => t.Users1).Include(t => t.Users).Where(a => a.IsSubmitted);
                return View(await travelRequests.ToListAsync());
            }
        }

        private void checkErrorMessages()
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
    }
}