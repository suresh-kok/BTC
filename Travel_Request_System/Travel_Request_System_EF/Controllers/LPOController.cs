using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    public class LPOController : Controller
    {
        // GET: LPO
        public async Task<ActionResult> Index()
        {
            using (BTCEntities db = new BTCEntities())
            {
                var lpos = db.LPO.Include(t => t.Quotation).Include(t => t.RFQ);
                return View(await lpos.ToListAsync());
            }
        }

        public async Task<ActionResult> LPOProcessing(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = true;
                var RFQList = await db.RFQ.Include(a => a.TravelRequests).Include(a=>a.LPO).Include(a => a.TravelAgency).Include(a => a.Users).Include(a => a.Quotation).Include("Quotation.ATQuotation").Include("Quotation.HSQuotation").Include("Quotation.PCQuotation").Where(a => a.Processing == (int)ProcessingStatus.Processed).Distinct().ToListAsync();
                ViewBag.RFQList = RFQList;
                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                return View(RFQList.ToList());
            }
        }

        public ActionResult GetQuotationList(string RFQID)
        {
            List<QuotationListViewModel> lstquotes = new List<QuotationListViewModel>();
            int RFQIDVal = Convert.ToInt32(RFQID);
            if (RFQIDVal > 0)
            {
                using (BTCEntities db = new BTCEntities())
                {
                    int travelRequestIDVal = (int)db.RFQ.Where(a => a.ID == RFQIDVal).Select(a => a.TravelRequestID).FirstOrDefault();
                    Quotation QuotationVal = db.Quotation.Include(a => a.TravelRequests).Where(a => a.TravelRequestID == travelRequestIDVal).FirstOrDefault();

                    lstquotes.AddRange(db.ATQuotation.Where(a => a.QuotationID == QuotationVal.ID)
                        .Select(a => new QuotationListViewModel
                        {
                            QuotationID = a.QuotationID,
                            RFQID = RFQIDVal,
                            TravelRequestID = travelRequestIDVal,
                            QuotationType = 1,
                            QuotationTypeID = a.ID,
                            QuotationName = QuotationVal.TravelRequests.ApplicationNumber + " / AT - Q"
                        }).ToList());

                    lstquotes.AddRange(db.HSQuotation.Where(a => a.QuotationID == QuotationVal.ID)
                        .Select(a => new QuotationListViewModel
                        {
                            QuotationID = a.QuotationID,
                            RFQID = RFQIDVal,
                            TravelRequestID = travelRequestIDVal,
                            QuotationType = 2,
                            QuotationTypeID = a.ID,
                            QuotationName = QuotationVal.TravelRequests.ApplicationNumber + " / HS - Q"
                        }).ToList());

                    lstquotes.AddRange(db.PCQuotation.Where(a => a.QuotationID == QuotationVal.ID)
                        .Select(a => new QuotationListViewModel
                        {
                            QuotationID = a.QuotationID,
                            RFQID = RFQIDVal,
                            TravelRequestID = travelRequestIDVal,
                            QuotationType = 3,
                            QuotationTypeID = a.ID,
                            QuotationName = QuotationVal.TravelRequests.ApplicationNumber + " / PC - Q"
                        }).ToList());
                }
            }
            else
            {
                lstquotes.Add(new QuotationListViewModel
                {
                    QuotationID = 0,
                    RFQID = RFQIDVal,

                    TravelRequestID = 0,
                    QuotationType = 0,
                    QuotationTypeID = 0,
                    QuotationName = "No Quotations"
                });
            }
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstquotes);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> LPOCreation(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var LPO = await db.LPO.Where(a => a.ID == id).FirstOrDefaultAsync();
                return View(LPO);
            }
        }
    }
}