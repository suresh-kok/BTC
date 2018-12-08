using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    public class RFQsController : Controller
    {
        private static RFQ MasterRFQ = new RFQ();
        private static MembershipUser user;
        private static Users dbuser;
        private static string[] roles;

        public RFQsController()
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

        // GET: RFQ
        public async Task<ActionResult> Index()
        {
            using (BTCEntities db = new BTCEntities())
            {
                //var RFQ = db.RFQ.Include(r => r.TravelAgency).Include(r => r.TravelRequests).Include(r => r.Users);
                //return View(await RFQ.ToListAsync());

                var travelRequests = db.TravelRequests.Include(t => t.City).Include(t => t.City1).Include(t => t.Currency).Include(t => t.RFQ).Include(t => t.Users).Include(t => t.Users1);
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
                ViewBag.CurrencyID = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                TravelRequests travelRequest = await db.TravelRequests.FindAsync(id);
                return View(travelRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void RFQProcessing(TravelRequests travelRequest, FormCollection formCollection)
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

            using (BTCEntities db = new BTCEntities())
            {
                if (db.RFQ.Where(a => a.ProcessingSection == rfq.ProcessingSection && a.IsDeleted == false).Count() < 1)
                {
                    db.RFQ.Add(rfq);
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "Record Added";
                }
                else
                {
                    ViewBag.ErrorMessage = "Record already exists";
                }
            }
            RedirectToAction("RFQMerger", new { id = rfq.TravelRequestID });
        }

        public ActionResult RFQMerger(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                List<RFQ> rfqlist = db.RFQ.Include(a => a.TravelRequests).Include(a => a.Users).Include(a => a.TravelAgency).Include(a => a.LPO).Where(a => a.TravelRequestID == id).ToList();
                ViewBag.TravelAgency = db.TravelAgency.ToList();
                var travelreq = rfqlist.FirstOrDefault().TravelRequests.ApplicationNumber;
                ViewBag.AttachmentCount = db.AttachmentLink.Where(x => x.AttachmentFor.Contains(travelreq)).Count();

                return View(rfqlist);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void DeleteRFQProcessing(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var rfqVal = db.RFQ.Where(a => a.ID == id).FirstOrDefault();
                rfqVal.IsDeleted = true;
                db.RFQ.Add(rfqVal);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Entry Marked for Deletion";
            }
        }

        public async Task<ActionResult> RFQPostMerger(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                List<RFQ> rfqlist = await db.RFQ.Include(a => a.TravelRequests).Include(a => a.Users).Include(a => a.TravelAgency).Include(a => a.LPO).Where(a => a.TravelRequestID == id).ToListAsync();
                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.AttachmentCount = await db.AttachmentLink.Where(x => x.AttachmentFor.Contains(rfqlist[0].TravelRequests.ApplicationNumber)).CountAsync();

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
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(rfqlist.TravelRequests.ApplicationNumber)).Select(x => x.Attachments).ToList();
                MasterRFQ = rfqlist;
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

                RFQ rfqlist = await db.RFQ.Include(a => a.TravelRequests).Include(a => a.TravelAgency).Include(a => a.Users).Where(a => a.ID == id).FirstOrDefaultAsync();
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(rfqlist.TravelRequests.ApplicationNumber)).Select(x => x.Attachments).ToList();
                MasterRFQ = rfqlist;
                return View(rfqlist);
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
                    MasterRFQ.Processing = (int)ProcessingStatus.BeingProcessed;
                    MasterRFQ.UserID = dbuser.ID;
                    MasterRFQ.Remarks = rfq.Remarks;
                    MasterRFQ.TravelAgencyID = rfq.TravelAgencyID;
                    MasterRFQ.IsDeleted = rfq.IsDeleted;

                    db.RFQ.Attach(MasterRFQ);
                    var entry = db.Entry(MasterRFQ);
                    entry.Property(a => a.Remarks).IsModified = true;
                    entry.Property(a => a.UserID).IsModified = true;
                    entry.Property(a => a.Processing).IsModified = true;
                    entry.Property(a => a.TravelAgencyID).IsModified = true;
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

                RFQ rfqlist = db.RFQ.Include(a => a.TravelRequests).Include(a => a.TravelAgency).Include(a => a.Users).Where(a => a.ID == rfq.ID).FirstOrDefault();
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(rfqlist.TravelRequests.ApplicationNumber)).Select(x => x.Attachments).ToList();
                MasterRFQ = rfqlist;
                return View(rfqlist);
            }
        }

        public ActionResult DeleteRFQ(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                RFQ rfqlist = db.RFQ.Include(a => a.TravelRequests).Include(a => a.TravelAgency).Include(a => a.Users).Where(a => a.ID == id).FirstOrDefault();

                if (ModelState.IsValid)
                {
                    rfqlist.IsDeleted = true;

                    db.RFQ.Attach(rfqlist);
                    var entry = db.Entry(rfqlist);
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

                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(rfqlist.TravelRequests.ApplicationNumber)).Select(x => x.Attachments).ToList();
                MasterRFQ = rfqlist;
                return View(rfqlist);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FileUpload()
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    string fileName = string.Empty;
                    string destinationPath = string.Empty;
                    List<Attachments> uploadFileModel = new List<Attachments>();

                    fileName = Path.GetFileName(Request.Files[0].FileName);
                    destinationPath = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                    Request.Files[0].SaveAs(destinationPath);


                    var isFileNameRepete = (db.Attachments.AsEnumerable().Where(x => x.FileName == fileName).ToList());
                    if (isFileNameRepete == null || isFileNameRepete.Count <= 0)
                    {
                        Attachments attachments = new Attachments { FileName = fileName, FileType = Request.Files[0].ContentType, FilePath = destinationPath, UploadedDate = DateTime.Now, UploadedBy = dbuser.ID };
                        db.Attachments.Add(attachments);
                        db.SaveChanges();

                        AttachmentLink attachmentLink = new AttachmentLink { AttachmentFor = MasterRFQ.TravelRequests.ApplicationNumber, AttachmentForID = attachments.ID };
                        db.AttachmentLink.Add(attachmentLink);
                        db.SaveChanges();

                        ViewBag.SuccessMessage = "File Uploaded Successfully";
                    }
                    else
                    {
                        ViewBag.WarningMessage = "File is already exists";
                    }
                }
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(MasterRFQ.TravelRequests.ApplicationNumber)).Select(x => x.Attachments).ToList();
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

                    FileInfo file = new FileInfo(Server.MapPath("~/UploadedFiles/" + fileName));
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    ViewBag.SuccessMessage = "File Deleted Successfully";
                }
                ViewBag.fileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(MasterRFQ.TravelRequests.ApplicationNumber)).Select(x => x.Attachments).ToList();
                return RedirectToAction("PreviewRFQ", new { id = MasterRFQ.ID });
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