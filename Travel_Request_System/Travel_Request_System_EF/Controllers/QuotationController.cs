using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Travel_Request_System_EF.Models;

namespace Travel_Request_System_EF.Controllers
{
    public class QuotationController : Controller
    {
        // GET: Quotation
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

        public ActionResult Adduotation()
        {
            using (BTCEntities db = new BTCEntities())
            {
                ViewBag.Cities = db.City.ToList();
                ViewBag.CurrencyID = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                Quotation quotation = new Quotation();
                quotation.ATQuotation.Add(new ATQuotation());
                quotation.HSQuotation.Add(new HSQuotation());
                quotation.PCQuotation.Add(new PCQuotation());
                quotation.LPO.Add(new LPO());
                quotation.TravelRequests = new TravelRequests();
                return View(quotation);
            }
        }

        public ActionResult AddATQuotation()
        {
            using (BTCEntities db = new BTCEntities())
            {

            }

        }
    }
}