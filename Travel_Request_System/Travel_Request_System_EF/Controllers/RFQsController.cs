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
        private BTCEntities db = new BTCEntities();

        // GET: RFQ
        public async Task<ActionResult> Index()
        {
            var RFQ = db.RFQ.Include(r => r.TravelAgency).Include(r => r.TravelRequests).Include(r => r.Users);
            return View(await RFQ.ToListAsync());
        }

        // POST: RFQ/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TravelAgencyID,TravelRequestID,UserID,Remarks,Processing,ProcessingSection,IsDeleted")] RFQ rFQ)
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

        // GET: RFQ/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: RFQ/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,TravelAgencyID,TravelRequestID,UserID,Remarks,Processing,ProcessingSection,IsDeleted")] RFQ rFQ)
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

        // GET: RFQ/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: RFQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RFQ rFQ = await db.RFQ.FindAsync(id);
            db.RFQ.Remove(rFQ);
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

            ViewBag.Cities = db.City.ToList();
            ViewBag.CurrencyID = db.Currency.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            TravelRequests travelRequest = await db.TravelRequests.FindAsync(id);
            return View(travelRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void RFQProcessing(TravelRequests travelRequest, FormCollection formCollection)
        {
            RFQ rfq = new RFQ
            {
                TravelRequestID = travelRequest.ID,
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
            db.RFQ.Add(rfq);
            db.SaveChanges();
            //RedirectToAction("RFQMerger", rFQ.ID);
        }

        public async Task<ActionResult> RFQMerger(int? id)
        {
            ViewBag.TravelAgency = db.TravelAgency.ToList();

            List<RFQ> rfqlist = db.RFQ.Where(a => a.TravelRequestID == id).ToList();
            return View(rfqlist);
        }

        public async Task<ActionResult> RFQPostMerger(int? id)
        {
            ViewBag.TravelAgency = db.TravelAgency.ToList();

            List<RFQ> rfqlist = new List<RFQ>();
            rfqlist.Add(new RFQ() { TravelAgencyID = 3, TravelRequestID = 1 });
            return View(rfqlist);
        }

        public async Task<ActionResult> RFQFinalPreview(int? id)
        {
            ViewBag.Cities = db.City.ToList();
            ViewBag.CurrencyID = db.Currency.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();
            ViewBag.TravelAgency = db.TravelAgency.ToList();

            RFQ rFQ = new RFQ();
            rFQ.TravelAgency = new TravelAgency();
            rFQ.TravelAgencyID = 3;
            rFQ.TravelRequests = new TravelRequests()
            {
                PortOfDestinationID = 1,
                PortOfOriginID = 1,
                CurrencyID = 1
            };
            rFQ.Users = new Users();
            return View(rFQ);
        }
    }
}
