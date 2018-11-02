using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    public class BTCController : Controller
    {
        private static MembershipUser user;
        private static string[] roles;

        public BTCController()
        {
            user = Membership.GetUser();
            CustomRole customRole = new CustomRole();
            roles = customRole.GetRolesForUser(user.UserName);
            IsLoggedIn(true, roles[0]);
        }

        public ActionResult Index()
        {
            return RedirectToAction(roles[0] + "Dashboard", "BTC");
        }

        [CustomAuthorize(Roles = "Employee")]
        public ActionResult EmployeeDashboard()
        {
            return View();
        }

        [CustomAuthorize(Roles = "HR")]
        public ActionResult HRDashboard()
        {
            ViewBag.NotificationsList = new List<NotificationsGridDetails>() { new NotificationsGridDetails() { Name = "1" }, new NotificationsGridDetails() { Name = "2" }, new NotificationsGridDetails() { Name = "3" }, new NotificationsGridDetails() { Name = "4" } };
            return View();
        }

        [CustomAuthorize(Roles = "Manager")]
        public ActionResult ManagerDashboard()
        {
            return View();
        }

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult TravelCoDashboard()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Manager")]
        public ActionResult ManageEmployees()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult ManageUsers()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult OrganisationalChart()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Employee")]
        public ActionResult TravelRequest()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Manager")]
        public ActionResult ManagerApprovalForm()
        {
            return View();
        }

        [CustomAuthorize(Roles = "HR")]
        public ActionResult HRApprovalForm()
        {
            return View();
        }

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult ManageRFQ()
        {
            return View();
        }

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult ManagaTravelAgency()
        {
            return View();
        }

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult ManagerLPO()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Manager,HR,Admin,TravelCo")]
        public ActionResult Reports()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Employee,Manager,HR,Admin,TravelCo")]
        public ActionResult ViewTravelRequests()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Employee,Manager,HR,Admin,TravelCo")]
        public ActionResult EmployeeDetails()
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
                    ViewBag.IsAdmin = !Val;
                    break;
                case Constants.HR:
                    ViewBag.IsEmployee = !Val;
                    ViewBag.IsHR = Val;
                    ViewBag.IsTravelCo = !Val;
                    ViewBag.IsManager = !Val;
                    ViewBag.IsAdmin = !Val;
                    break;
                case Constants.TravelCorordinator:
                    ViewBag.IsEmployee = !Val;
                    ViewBag.IsHR = !Val;
                    ViewBag.IsTravelCo = Val;
                    ViewBag.IsManager = !Val;
                    ViewBag.IsAdmin = !Val;
                    break;
                case Constants.Manager:
                    ViewBag.IsEmployee = !Val;
                    ViewBag.IsHR = !Val;
                    ViewBag.IsTravelCo = !Val;
                    ViewBag.IsManager = Val;
                    ViewBag.IsAdmin = !Val;
                    break;
                case Constants.Admin:
                    ViewBag.IsEmployee = !Val;
                    ViewBag.IsHR = !Val;
                    ViewBag.IsTravelCo = !Val;
                    ViewBag.IsManager = !Val;
                    ViewBag.IsAdmin = Val;
                    break;
                default:
                    break;
            }
        }
    }
}