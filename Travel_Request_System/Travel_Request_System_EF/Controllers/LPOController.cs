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
                var travelRequests = db.TravelRequests.Include(t => t.City).Include(t => t.City1).Include(t => t.Currency).Include(t => t.RFQ).Include(t => t.Users).Include(t => t.Users1);
                return View(await travelRequests.ToListAsync());
            }
        }

        public async Task<ActionResult> LPOProcessing(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var RFQList = await db.RFQ.Include(a => a.TravelRequests).Where(a => a.Processing == (int)ProcessingStatus.Processed).Distinct().ToListAsync();
                ViewBag.RFQList = RFQList;

                return View();
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
    }
}