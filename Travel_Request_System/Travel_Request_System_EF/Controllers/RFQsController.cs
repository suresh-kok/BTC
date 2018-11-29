using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    public class RFQsController : Controller
    {
        private HRWorksEntities db = new HRWorksEntities();

        // GET: RFQs
        public async Task<ActionResult> Index()
        {
            var rFQs = db.RFQs.Include(r => r.TravelAgency).Include(r => r.TravelRequest).Include(r => r.User);
            return View(await rFQs.ToListAsync());
        }

        // POST: RFQs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RFQID,TravelAgencyID,TravelRequestID,UserID,Remarks,Processing,ProcessingSection,IsDeleted")] RFQ rFQ)
        {
            if (ModelState.IsValid)
            {
                db.RFQs.Add(rFQ);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TravelAgencyID = new SelectList(db.TravelAgencies, "AgencyID", "AgencyCode", rFQ.TravelAgencyID);
            ViewBag.TravelRequestID = new SelectList(db.TravelRequests, "TravelRequestID", "ApplicationNumber", rFQ.TravelRequestID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "Username", rFQ.UserID);
            return View(rFQ);
        }

        // GET: RFQs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RFQ rFQ = await db.RFQs.FindAsync(id);
            if (rFQ == null)
            {
                return HttpNotFound();
            }
            ViewBag.TravelAgencyID = new SelectList(db.TravelAgencies, "AgencyID", "AgencyCode", rFQ.TravelAgencyID);
            ViewBag.TravelRequestID = new SelectList(db.TravelRequests, "TravelRequestID", "ApplicationNumber", rFQ.TravelRequestID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "Username", rFQ.UserID);
            return View(rFQ);
        }

        // POST: RFQs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RFQID,TravelAgencyID,TravelRequestID,UserID,Remarks,Processing,ProcessingSection,IsDeleted")] RFQ rFQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rFQ).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TravelAgencyID = new SelectList(db.TravelAgencies, "AgencyID", "AgencyCode", rFQ.TravelAgencyID);
            ViewBag.TravelRequestID = new SelectList(db.TravelRequests, "TravelRequestID", "ApplicationNumber", rFQ.TravelRequestID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "Username", rFQ.UserID);
            return View(rFQ);
        }

        // GET: RFQs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RFQ rFQ = await db.RFQs.FindAsync(id);
            if (rFQ == null)
            {
                return HttpNotFound();
            }
            return View(rFQ);
        }

        // POST: RFQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RFQ rFQ = await db.RFQs.FindAsync(id);
            db.RFQs.Remove(rFQ);
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

        [HttpGet]
        public async Task<ActionResult> RFQProcessing(int? id)
        {

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
            return View(travelRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void RFQProcessing(TravelRequest travelRequest, FormCollection formCollection)
        {
            RFQ rfq = new RFQ
            {
                TravelRequestID = travelRequest.TravelRequestID,
                TravelAgencyID = 3,
                UserID = 1
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
            rfq.Processing = (int)ProcessingStatus.BeingProcessed;
            db.RFQs.Add(rfq);
            db.SaveChanges();
            //RedirectToAction("RFQMerger", rFQ.RFQID);
        }

        public async Task<ActionResult> RFQMerger(int? id)
        {
            ViewBag.TravelAgencies = db.TravelAgencies.ToList();

            List<RFQ> rfqlist = db.RFQs.Where(a => a.TravelRequestID == id).ToList();
            return View(rfqlist);
        }

        public async Task<ActionResult> RFQPostMerger(int? id)
        {
            ViewBag.TravelAgencies = db.TravelAgencies.ToList();

            List<RFQ> rfqlist = new List<RFQ>();
            rfqlist.Add(new RFQ() { TravelAgencyID = 3, TravelRequestID = 1 });
            return View(rfqlist);
        }

        public async Task<ActionResult> RFQFinalPreview(int? id)
        {
            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();
            ViewBag.TravelAgencies = db.TravelAgencies.ToList();

            RFQ rFQ = new RFQ();
            rFQ.TravelAgency = new TravelAgency();
            rFQ.TravelAgencyID = 3;
            rFQ.TravelRequest = new TravelRequest()
            {
                PortOfDestinationID = 1,
                PortOfOriginID = 1,
                CurrencyID = 1,
                UserID = 1,
            };
            rFQ.User = new User();
            return View(rFQ);
        }
    }
}
