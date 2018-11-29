using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.Mail;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    [HandleError]
    public class BTCController : Controller
    {
        private static MembershipUser user;
        private static string[] roles;

        public BTCController()
        {
            user = Membership.GetUser();
            CustomRole customRole = new CustomRole();
            roles = customRole.GetRolesForUser(user.UserName);
            IsLoggedIn(roles.ToList());
            using (HRWorksEntities db = new HRWorksEntities())
            {
                User userobj = new User();
                userobj = db.Users.Where(a => a.Username == user.UserName).Include(a => a.Roles).Include(a => a.TravelRequests).Include(a => a.TravelRequests1).FirstOrDefault();
                ViewBag.FirstName = userobj.FirstName;
                ViewBag.LastName = userobj.LastName;
                ViewBag.RoleName = roles.ToList()[0];
            }
        }

        public ActionResult Index()
        {
            return RedirectToAction(roles[0] + "Dashboard", "BTC");
        }

        #region Employee

        [CustomAuthorize(Roles = "Employee")]
        public ActionResult EmployeeDashboard()
        {
            return View(ViewMyTravelRequests());
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
            return View(ViewApprovalTravelRequests(2));
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
            return View(ViewApprovalTravelRequests(1));
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
        public ActionResult OrganisationalChart()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> ManageTravelAgency()
        {
            using (HRWorksEntities db = new HRWorksEntities())
            {
                return View(await db.TravelAgencies.ToListAsync());
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult CreateTravelAgency()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTravelAgency([Bind(Include = "AgencyID,AgencyCode,CompanyName,Address,Telephone,Fax,Mobile,Landline,ContactPerson,Email")] TravelAgency travelAgency)
        {
            if (ModelState.IsValid)
            {
                using (HRWorksEntities db = new HRWorksEntities())
                {
                    db.TravelAgencies.Add(travelAgency);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ManageTravelAgency");
                }
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
            using (HRWorksEntities db = new HRWorksEntities())
            {
                TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
                if (travelAgency == null)
                {
                    return HttpNotFound();
                }
                return View(travelAgency);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTravelAgency([Bind(Include = "AgencyID,AgencyCode,CompanyName,Address,Telephone,Fax,Mobile,Landline,ContactPerson,Email")] TravelAgency travelAgency)
        {
            if (ModelState.IsValid)
            {
                using (HRWorksEntities db = new HRWorksEntities())
                {
                    db.Entry(travelAgency).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("ManageTravelAgency");
                }
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
            using (HRWorksEntities db = new HRWorksEntities())
            {
                TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
                if (travelAgency == null)
                {
                    return HttpNotFound();
                }
                return View(travelAgency);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTravelAgency(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (HRWorksEntities db = new HRWorksEntities())
            {
                TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
                if (travelAgency == null)
                {
                    return HttpNotFound();
                }
                return View(travelAgency);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteTravelAgency")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTravelAgencyConfirmed(int id)
        {
            using (HRWorksEntities db = new HRWorksEntities())
            {
                TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
                db.TravelAgencies.Remove(travelAgency);
                await db.SaveChangesAsync();
                return RedirectToAction("ManageTravelAgency");
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> TravelAgencyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (HRWorksEntities db = new HRWorksEntities())
            {
                TravelAgency travelAgency = await db.TravelAgencies.FindAsync(id);
                if (travelAgency == null)
                {
                    return HttpNotFound();
                }
                return View(travelAgency);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult ManageEmployees()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult ManageUsers()
        {
            List<User> usersList = new List<User>();
            using (HRWorksEntities db = new HRWorksEntities())
            {
                usersList = db.Users.Include(a => a.Roles).ToList();
            }
            return View(usersList);

        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> ViewUserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (HRWorksEntities db = new HRWorksEntities())
            {
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RoleName = user.Roles.FirstOrDefault().RoleName;
                return View(user);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult CreateUser()
        {
            using (HRWorksEntities db = new HRWorksEntities())
            {
                List<Role> allRoles = db.Roles.ToList();
                ViewBag.allRoles = allRoles;
                return View();
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "UserId,Username,FirstName,LastName,Email,Password,IsActive,ActivationCode")] User userObj, FormCollection form)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                int DDLValue = Convert.ToInt32(form["RoleID"].ToString());

                string userName = Membership.GetUserNameByEmail(userObj.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    return RedirectToAction("CreateUser", userObj);
                }

                using (HRWorksEntities db = new HRWorksEntities())
                {
                    var role = db.Roles.Where(a => a.RoleId == DDLValue).FirstOrDefault();
                    var user = new User()
                    {
                        Username = userObj.Username,
                        FirstName = userObj.FirstName,
                        LastName = userObj.LastName,
                        Email = userObj.Email,
                        Password = userObj.Password,
                        ActivationCode = Guid.NewGuid(),
                    };

                    user.Roles.Add(role);
                    db.Users.Add(user);

                    db.SaveChanges();
                }

                VerificationEmail(userObj.Email, userObj.ActivationCode.ToString());
                messageRegistration = "Your account has been created successfully. ^_^";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something Wrong!";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return RedirectToAction("CreateUser", userObj);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (HRWorksEntities db = new HRWorksEntities())
            {
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RoleName = user.Roles.FirstOrDefault().RoleName;
                return View(user);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser([Bind(Include = "UserId,Username,FirstName,LastName,Email,Password,IsActive,ActivationCode")] User user, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int DDLValue = Convert.ToInt32(form["RoleID"].ToString());

                using (HRWorksEntities db = new HRWorksEntities())
                {
                    var role = db.Roles.Where(a => a.RoleId == DDLValue).FirstOrDefault();
                    if (!user.Roles.Contains(role))
                    {
                        user.Roles.Add(role);
                    }

                    db.Entry(user).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("ManageUsers");
                }
            }
            return View(user);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (HRWorksEntities db = new HRWorksEntities())
            {
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RoleName = user.Roles.FirstOrDefault().RoleName;
                return View(user);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteUsers")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUserConfirmed(int id)
        {
            using (HRWorksEntities db = new HRWorksEntities())
            {
                User user = await db.Users.FindAsync(id);
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return RedirectToAction("ManageUsers");
            }
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

        [CustomAuthorize(Roles = "Employee,Manager,HR,Admin,TravelCo")]
        public ActionResult ChangePassword()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                TempData.Remove("Message");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult ChangePasswordClick(string oldPassword, string newPassword)
        {
            string messageReset = string.Empty;

            if (ModelState.IsValid)
            {
                CustomMembership customMembership = new CustomMembership();
                var userdet = customMembership.GetUser(user.UserName, true);

                if (userdet == null || (userdet != null && string.Compare(oldPassword, userdet.GetPassword(), StringComparison.OrdinalIgnoreCase) == 0))
                {
                    messageReset = "Sorry: The old password is same as new password";
                }
                else if ((userdet != null && string.Compare(oldPassword, userdet.GetPassword(), StringComparison.OrdinalIgnoreCase) != 0))
                {
                    messageReset = "Sorry: The old password is incorrect";
                }
                else
                {
                    if (customMembership.ChangePassword(userdet.UserName, oldPassword, newPassword))
                    {
                        messageReset = "Your account has password has been changed.";
                    }
                    else
                    {
                        messageReset = "Something Went Wrong!";
                    }
                }
            }
            else
            {
                messageReset = "Something Went Wrong!";
            }

            TempData["Message"] = messageReset;

            return RedirectToAction("ChangePassword");
        }

        private List<TravelRequest> ViewMyTravelRequests()
        {
            using (HRWorksEntities db = new HRWorksEntities())
            {
                return db.TravelRequests.Where(a => a.UserID == (db.Users.Where(u => u.Username == user.UserName).FirstOrDefault()).UserId).OrderBy(a => a.CreateOn).ToList();
            }
        }

        private List<TravelRequest> ViewApprovalTravelRequests(int level)
        {
            using (HRWorksEntities db = new HRWorksEntities())
            {
                return db.TravelRequests.Where(a => a.ApprovalLevel == level).ToList();
            }
        }

        private List<TravelRequest> ViewAllTravelRequests()
        {
            using (HRWorksEntities db = new HRWorksEntities())
            {
                return db.TravelRequests.OrderBy(a => a.CreateOn).ToList();
            }
        }

        #endregion

        #region General

        private void IsLoggedIn(List<string> Role)
        {
            var Val = true;
            ViewBag.LoggedOut = !Val;
            ViewBag.messages = Val;
            ViewBag.notifications = Val;
            ViewBag.tasks = Val;
            ViewBag.userdetails = Val;
            ViewBag.IsEmployee = !Val;
            ViewBag.IsHR = !Val;
            ViewBag.IsTravelCo = !Val;
            ViewBag.IsManager = !Val;
            ViewBag.IsAdmin = !Val;
            if (roles.Contains(Constants.Employee))
            {
                ViewBag.IsEmployee = Val;
            }
            if (roles.Contains(Constants.HR))
            {
                ViewBag.IsHR = Val;
            }
            if (roles.Contains(Constants.Admin))
            {
                ViewBag.IsAdmin = Val;
            }
            if (roles.Contains(Constants.Manager))
            {
                ViewBag.IsManager = Val;
            }
            if (roles.Contains(Constants.TravelCorordinator))
            {
                ViewBag.IsTravelCo = Val;
            }
        }

        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            SendMail mail = new SendMail();
            mail.ToAddresses.Add(email);
            mail.MailSubject = "Activation Account !";
            mail.MailBody = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";

            mail.Send();
        }
        #endregion
    }
}