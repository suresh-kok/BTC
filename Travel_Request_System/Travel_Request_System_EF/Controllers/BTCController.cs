using System;
using System.Web.Mvc;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    public class BTCController : Controller
    {
        public ActionResult Index()
        {
            string Role;
            GetRole(Convert.ToInt32(Session["HREmployeeID"]), out Role);
            IsLoggedIn(true, Role);
            return View(Role + "Dashboard");
        }

        public ActionResult EmployeeDashboard()
        {
            return View();
        }

        public ActionResult HRDashboard()
        {
            return View();
        }

        public ActionResult ManagerDashboard()
        {
            return View();
        }

        public ActionResult TravelCoDashboard()
        {
            return View();
        }

        public ActionResult TravelRequest()
        {
            return View();
        }

        public ActionResult ManagerApprovalForm()
        {
            return View();
        }

        public ActionResult HRApprovalForm()
        {
            return View();
        }

        public ActionResult ManageRFQ()
        {
            return View();
        }

        public ActionResult ManagaTravelAgency()
        {
            return View();
        }

        public ActionResult ManagerLPO()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        private void GetRole(long hREmployeeID, out string role)
        {
            role = Constants.Employee;
        }

        public void IsLoggedIn(bool Val, string Role)
        {
            ViewBag.LoggedOut = !Val;
            ViewBag.messages = Val;
            ViewBag.notifications = Val;
            ViewBag.tasks = Val;
            ViewBag.userdetails = Val;
            switch (Role)
            {
                case Constants.Employee:
                    ViewBag.IsEmployee = Val;
                    ViewBag.IsHR = !Val;
                    ViewBag.IsTravelCo = !Val;
                    ViewBag.IsManager = !Val;
                    break;
                case Constants.HR:
                    ViewBag.IsEmployee = !Val;
                    ViewBag.IsHR = Val;
                    ViewBag.IsTravelCo = !Val;
                    ViewBag.IsManager = !Val;
                    break;
                case Constants.TravelCorordinator:
                    ViewBag.IsEmployee = !Val;
                    ViewBag.IsHR = !Val;
                    ViewBag.IsTravelCo = Val;
                    ViewBag.IsManager = !Val;
                    break;
                case Constants.Manager:
                    ViewBag.IsEmployee = !Val;
                    ViewBag.IsHR = !Val;
                    ViewBag.IsTravelCo = !Val;
                    ViewBag.IsManager = Val;
                    break;
                default:
                    break;
            }
        }
    }
}