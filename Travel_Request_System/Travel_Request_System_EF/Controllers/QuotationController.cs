using System.Linq;
using System.Web.Mvc;
using Travel_Request_System_EF.Models;

namespace Travel_Request_System_EF.Controllers
{
    public class QuotationController : Controller
    {
        private HRWorksEntities db = new HRWorksEntities();

        // GET: Quotation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQuotation()
        {
            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            Quotation quotation = new Quotation();
            quotation.ATQuotations.Add(new ATQuotation());
            quotation.HSQuotations.Add(new HSQuotation());
            quotation.PCQuotations.Add(new PCQuotation());
            quotation.LPOes.Add(new LPO());
            quotation.TravelRequest = new TravelRequest();
            return View(quotation);
        }
    }
}