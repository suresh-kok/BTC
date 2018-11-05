using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    public class BTCController : Controller
    {
        private static MembershipUser user;
        private static string[] roles;
        private HRWorksEntities db = new HRWorksEntities();

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

        #region Employee

        [CustomAuthorize(Roles = "Employee")]
        public ActionResult EmployeeDashboard()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Employee")]
        public ActionResult TravelRequest()
        {
            return View();
        }

        #endregion

        #region HR

        [CustomAuthorize(Roles = "HR")]
        public ActionResult HRDashboard()
        {
            ViewBag.NotificationsList = new List<NotificationsGridDetails>() { new NotificationsGridDetails() { Name = "1" }, new NotificationsGridDetails() { Name = "2" }, new NotificationsGridDetails() { Name = "3" }, new NotificationsGridDetails() { Name = "4" } };
            return View();
        }

        [CustomAuthorize(Roles = "HR")]
        public ActionResult HRApprovalForm()
        {
            return View();
        }

        #endregion

        #region Manager

        [CustomAuthorize(Roles = "Manager")]
        public ActionResult ManagerDashboard()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Manager")]
        public ActionResult ManagerApprovalForm()
        {
            return View();
        }

        #endregion

        #region TravelCo

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult TravelCoDashboard()
        {
            return View();
        }

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult ManageRFQ()
        {
            return View();
        }

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult ManagerLPO()
        {
            return View();
        }

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult ManagerQuotations()
        {
            return View();
        }

        #endregion

        #region Admin

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AdminDashboard()
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
        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> ManageTravelAgency()
        {
            return View(await db.TravelAgencies.ToListAsync());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult CreateTravelAgency()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTravelAgency([Bind(Include = "AgencyID,AgencyCode,CompanyName,Address,Telephone,Fax,Mobile,Landline,ContactPerson,Email")] TravelAgency travelAgency)
        {
            if (ModelState.IsValid)
            {
                db.TravelAgencies.Add(travelAgency);
                await db.SaveChangesAsync();
                return RedirectToAction("ManageTravelAgency");
            }

            return View(travelAgency);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> EditTravelAgency(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
            if (travelAgency == null)
            {
                return HttpNotFound();
            }
            return View(travelAgency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTravelAgency([Bind(Include = "AgencyID,AgencyCode,CompanyName,Address,Telephone,Fax,Mobile,Landline,ContactPerson,Email")] TravelAgency travelAgency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelAgency).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ManageTravelAgency");
            }
            return View(travelAgency);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> ViewTravelAgency(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
            if (travelAgency == null)
            {
                return HttpNotFound();
            }
            return View(travelAgency);
        }

        public async Task<ActionResult> DeleteTravelAgency(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
            if (travelAgency == null)
            {
                return HttpNotFound();
            }
            return View(travelAgency);
        }

        [HttpPost, ActionName("DeleteTravelAgency")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
            db.TravelAgencies.Remove(travelAgency);
            await db.SaveChangesAsync();
            return RedirectToAction("ManageTravelAgency");
        }

        public async Task<ActionResult> TravelAgencyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
            if (travelAgency == null)
            {
                return HttpNotFound();
            }
            return View(travelAgency);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult ManageEmployees()
        {
            return View();
        }

        #endregion

        #region MultiRole

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

        #endregion

        #region General

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

        #endregion
    }
}