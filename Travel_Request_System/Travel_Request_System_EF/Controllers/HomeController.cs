using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    public class HomeController : Controller
    {
        private HRWorksEntities HRWorks = new HRWorksEntities();
        private SessionContext context = new SessionContext();

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

        //[HttpPost]
        //public ActionResult Index(LoginViewModel model)
        //{
        //    var res = HRWorks.BTCLoginInfoes.Where(a => a.UserName == model.UserName && a.Password == model.Password && a.AccountLocked == false).FirstOrDefault();

        //    if (res.BTCEmployeeId > 0)
        //    {
        //        Session["UserID"] = res.BTCEmployeeId.ToString();
        //        Session["UserName"] = res.UserName.ToString();
        //        Session["HREmployeeID"] = res.HREmployeeID.ToString();
        //        return RedirectToAction("Index", "BTC");
        //    }
        //    return View("Index");
        //}

        [HttpPost]
        public ActionResult Index(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var res = HRWorks.BTCLoginInfoes.Where(a => a.UserName == user.UserName && a.Password == user.Password && a.AccountLocked == false).FirstOrDefault();
                if (res != null && res.BTCEmployeeId > 0)
                {
                    context.SetAuthenticationToken(res.HREmployeeID.ToString(), false, user);

                    Session["UserID"] = res.BTCEmployeeId.ToString();
                    Session["UserName"] = res.UserName.ToString();
                    Session["HREmployeeID"] = res.HREmployeeID.ToString();
                    return RedirectToAction("Index", "BTC");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}
