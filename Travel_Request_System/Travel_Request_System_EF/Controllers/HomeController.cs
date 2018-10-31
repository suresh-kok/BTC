using System.Linq;
using System.Web.Mvc;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.messages = false;
            ViewBag.notifications = false;
            ViewBag.tasks = false;
            ViewBag.userdetails = false;
            ViewBag.LoggedOut = true;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            HRWorksEntities HRWorks = new HRWorksEntities();
            var res = HRWorks.BTCLoginInfoes.Where(a => a.UserName == model.UserName && a.Password == model.Password && a.AccountLocked == false).FirstOrDefault();

            if (res.BTCEmployeeId > 0)
            {
                Session["UserID"] = res.BTCEmployeeId.ToString();
                Session["UserName"] = res.UserName.ToString();
                Session["HREmployeeID"] = res.HREmployeeID.ToString();
                return RedirectToAction("Index", "BTC");
            }
            return View("Index");
        }
    }
}
