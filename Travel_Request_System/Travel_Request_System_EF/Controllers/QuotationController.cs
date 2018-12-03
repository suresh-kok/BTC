using System.Linq;
using System.Web.Mvc;
using Travel_Request_System_EF.Models;

namespace Travel_Request_System_EF.Controllers
{
    public class QuotationController : Controller
    {
        private BTCEntities db = new BTCEntities();

        // GET: Quotation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQuotation()
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
}