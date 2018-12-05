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
using Roles = Travel_Request_System_EF.Models.Roles;

namespace Travel_Request_System_EF.Controllers
{
    [HandleError]
    public class BTCController : Controller
    {
        private static MembershipUser user;
        private static string[] roles;
        private static Users dbuser;

        public BTCController()
        {
            user = Membership.GetUser();
            CustomRole customRole = new CustomRole();
            roles = customRole.GetRolesForUser(user.UserName);
            ViewBag.RoleDetails = roles.ToList()[0];
            IsLoggedIn(roles.ToList());
            using (BTCEntities db = new BTCEntities())
            {
                dbuser = db.Users.Where(a => a.Username == user.UserName).Include(a => a.Roles).Include(a => a.TravelRequests).Include(a => a.TravelRequests1).FirstOrDefault();
                ViewBag.UserDetails = dbuser;
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
            return View(ViewApprovalTravelRequests((int)ApprovalLevels.ApprovedByManager));
        }

        [CustomAuthorize(Roles = "HR")]
        public ActionResult HRApprovalForm(int id)
        {
            try
            {
                if (id <= 0)
                {
                    ViewBag.ErrorMessage = "No Travel Request ID";
                    return View();
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Travel Request ID");
                }
                using (BTCEntities db = new BTCEntities())
                {
                    TravelRequests travelRequest = db.TravelRequests.Find(id);
                    if (travelRequest == null)
                    {
                        ViewBag.ErrorMessage = "Invalid Travel Request ID";
                        return View();
                        //return HttpNotFound("Invalid Travel Request ID");
                    }

                    ViewBag.Cities = db.City.ToList();
                    ViewBag.Currencies = db.Currency.ToList();
                    ViewBag.ApprovalBy = db.Users.ToList();
                    return View(travelRequest);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveHRRequest(TravelRequests travelRequest, FormCollection collection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (ModelState.IsValid)
                {
                    travelRequest.ApprovalLevel = (int)ApprovalLevels.ApprovedByHR;
                    db.TravelRequests.Add(travelRequest);
                    db.SaveChanges();
                    NotificationEmail(travelRequest, true);
                    return RedirectToAction("HRDashboard");
                }
                else
                {
                    var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                    List<string> sberr = new List<string>();
                    foreach (var item in errlist)
                    {
                        sberr.Add(item[0].ErrorMessage);
                    }
                    TempData["ErrorMessage"] = sberr.ToList();
                }

                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                return View(travelRequest);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectHRRequest(TravelRequests travelRequest, FormCollection collection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (ModelState.IsValid)
                {
                    travelRequest.ApprovalLevel = (int)ApprovalLevels.RejectedByHR;
                    db.TravelRequests.Add(travelRequest);
                    db.SaveChanges();
                    NotificationEmail(travelRequest, false);
                    return RedirectToAction("HRDashboard");
                }
                else
                {
                    var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                    List<string> sberr = new List<string>();
                    foreach (var item in errlist)
                    {
                        sberr.Add(item[0].ErrorMessage);
                    }
                    TempData["ErrorMessage"] = sberr.ToList();
                }

                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                return View(travelRequest);
            }
        }


        #endregion

        #region Manager

        [CustomAuthorize(Roles = "Manager")]
        public ActionResult ManagerDashboard()
        {
            return View(ViewApprovalTravelRequests((int)ApprovalLevels.ToBeApproved));
        }

        [CustomAuthorize(Roles = "Manager")]
        public ActionResult ManagerApprovalForm(int id)
        {
            try
            {
                if (id <= 0)
                {
                    ViewBag.ErrorMessage = "No Travel Request ID";
                    return View();
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Travel Request ID");
                }
                using (BTCEntities db = new BTCEntities())
                {
                    TravelRequests travelRequest = db.TravelRequests.Find(id);
                    if (travelRequest == null)
                    {
                        ViewBag.ErrorMessage = "Invalid Travel Request ID";
                        return View();
                        //return HttpNotFound("Invalid Travel Request ID");
                    }

                    ViewBag.Cities = db.City.ToList();
                    ViewBag.Currencies = db.Currency.ToList();
                    ViewBag.ApprovalBy = db.Users.ToList();
                    return View(travelRequest);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveManagerRequest(TravelRequests travelRequest, FormCollection collection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (ModelState.IsValid)
                {
                    travelRequest.ApprovalLevel = (int)ApprovalLevels.ApprovedByManager;
                    db.TravelRequests.Add(travelRequest);
                    db.SaveChanges();
                    NotificationEmail(travelRequest, true);
                    return RedirectToAction("ManagerDashboard");
                }
                else
                {
                    var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                    List<string> sberr = new List<string>();
                    foreach (var item in errlist)
                    {
                        sberr.Add(item[0].ErrorMessage);
                    }
                    TempData["ErrorMessage"] = sberr.ToList();
                }

                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                return View(travelRequest);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectManagerRequest(TravelRequests travelRequest, FormCollection collection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (ModelState.IsValid)
                {
                    travelRequest.ApprovalLevel = (int)ApprovalLevels.RejectedByManager;
                    db.TravelRequests.Add(travelRequest);
                    db.SaveChanges();
                    NotificationEmail(travelRequest, false);
                    return RedirectToAction("ManagerDashboard");
                }
                else
                {
                    var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                    List<string> sberr = new List<string>();
                    foreach (var item in errlist)
                    {
                        sberr.Add(item[0].ErrorMessage);
                    }
                    TempData["ErrorMessage"] = sberr.ToList();
                }

                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                return View(travelRequest);
            }
        }

        #endregion

        #region TravelCo

        [CustomAuthorize(Roles = "TravelCo")]
        public ActionResult TravelCoDashboard()
        {
            return View(ViewApprovalTravelRequests((int)ApprovalLevels.ApprovedByHR));
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
            using (BTCEntities db = new BTCEntities())
            {
                return View(await db.TravelAgency.ToListAsync());
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult CreateTravelAgency()
        {
            using (BTCEntities db = new BTCEntities())
            {
                TravelAgency travelAgency = new TravelAgency();
                travelAgency.AgencyCode = db.TravelAgency.Count() > 0 ? GenerateNextAgencyID(db.TravelAgency.OrderByDescending(a => a.ID).FirstOrDefault().AgencyCode) : "HRD-BTC-0HQ-001";

                checkErrorMessages();
                return View(travelAgency);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTravelAgency([Bind(Include = "ID,AgencyCode,CompanyName,Address,Telephone,Fax,Mobile,Landline,ContactPerson,Email")] TravelAgency travelAgency)
        {
            if (ModelState.IsValid)
            {
                using (BTCEntities db = new BTCEntities())
                {
                    db.TravelAgency.Add(travelAgency);
                    await db.SaveChangesAsync();

                    return RedirectToAction("ManageTravelAgency");
                }
            }
            else
            {
                var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                List<string> sberr = new List<string>();
                foreach (var item in errlist)
                {
                    sberr.Add(item[0].ErrorMessage);
                }
                TempData["ErrorMessage"] = sberr.ToList();
            }

            checkErrorMessages();
            return View(travelAgency);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> EditTravelAgency(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (BTCEntities db = new BTCEntities())
            {
                TravelAgency travelAgency = await db.TravelAgency.FindAsync(id);
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
        public async Task<ActionResult> EditTravelAgency([Bind(Include = "ID,AgencyCode,CompanyName,Address,Telephone,Fax,Mobile,Landline,ContactPerson,Email")] TravelAgency travelAgency)
        {
            if (ModelState.IsValid)
            {
                using (BTCEntities db = new BTCEntities())
                {
                    db.Entry(travelAgency).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("ManageTravelAgency");
                }
            }
            else
            {
                var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                List<string> sberr = new List<string>();
                foreach (var item in errlist)
                {
                    sberr.Add(item[0].ErrorMessage);
                }
                TempData["ErrorMessage"] = sberr.ToList();
            }
            checkErrorMessages();
            return View(travelAgency);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> ViewTravelAgency(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (BTCEntities db = new BTCEntities())
            {
                TravelAgency travelAgency = await db.TravelAgency.FindAsync(id);
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
            using (BTCEntities db = new BTCEntities())
            {
                TravelAgency travelAgency = await db.TravelAgency.FindAsync(id);
                if (travelAgency == null)
                {
                    return HttpNotFound();
                }

                checkErrorMessages();
                return View(travelAgency);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteTravelAgency")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTravelAgencyConfirmed(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                TravelAgency travelAgency = await db.TravelAgency.FindAsync(id);
                db.TravelAgency.Remove(travelAgency);
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
            using (BTCEntities db = new BTCEntities())
            {
                TravelAgency travelAgency = await db.TravelAgency.FindAsync(id);
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
            List<Users> usersList = new List<Users>();
            using (BTCEntities db = new BTCEntities())
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
            using (BTCEntities db = new BTCEntities())
            {
                Users user = await db.Users.FindAsync(id);
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
            using (BTCEntities db = new BTCEntities())
            {
                List<Roles> allRoles = db.Roles.ToList();
                ViewBag.allRoles = allRoles;
                return View();
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "ID,Username,FirstName,LastName,Email,Password,IsActive,ActivationCode")] Users userObj, FormCollection form)
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

                using (BTCEntities db = new BTCEntities())
                {
                    var role = db.Roles.Where(a => a.ID == DDLValue).FirstOrDefault();
                    var user = new Users()
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
            using (BTCEntities db = new BTCEntities())
            {
                Users user = await db.Users.FindAsync(id);
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
        public async Task<ActionResult> EditUser([Bind(Include = "ID,Username,FirstName,LastName,Email,Password,IsActive,ActivationCode")] Users user, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int DDLValue = Convert.ToInt32(form["RoleID"].ToString());

                using (BTCEntities db = new BTCEntities())
                {
                    var role = db.Roles.Where(a => a.ID == DDLValue).FirstOrDefault();
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
            using (BTCEntities db = new BTCEntities())
            {
                Users user = await db.Users.FindAsync(id);
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
            using (BTCEntities db = new BTCEntities())
            {
                Users user = await db.Users.FindAsync(id);
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

        private List<TravelRequests> ViewMyTravelRequests()
        {
            using (BTCEntities db = new BTCEntities())
            {
                return db.TravelRequests.Include(a => a.City).Include(a => a.City1).Include(a => a.Users).Include(a => a.Users1).Where(a => a.CreatedBy == (db.Users.Where(u => u.Username == user.UserName).FirstOrDefault()).ID).OrderBy(a => a.CreateOn).ToList();
            }
        }

        private List<TravelRequests> ViewApprovalTravelRequests(int level)
        {
            using (BTCEntities db = new BTCEntities())
            {
                return db.TravelRequests.Include(a => a.City).Include(a => a.City1).Include(a => a.Users).Include(a => a.Users1).Where(a => a.ApprovalLevel == level).ToList();
            }
        }

        private List<TravelRequests> ViewAllTravelRequests()
        {
            using (BTCEntities db = new BTCEntities())
            {
                return db.TravelRequests.Include(a => a.City).Include(a => a.City1).Include(a => a.Users).Include(a => a.Users1).OrderBy(a => a.CreateOn).ToList();
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

        public void NotificationEmail(TravelRequests travelRequests, bool IsApproved)
        {
            var url = string.Format("/TravelRequests/Details/{0}", travelRequests.ID);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            SendMail mail = new SendMail();
            mail.ToAddresses.Add(travelRequests.Users1.Email);
            if (IsApproved)
            {
                mail.MailSubject = "Travel Request " + travelRequests.ApplicationNumber + " Approved!";
            }
            else
            {
                mail.MailSubject = "Travel Request " + travelRequests.ApplicationNumber + " Rejected!";
            }
            mail.MailBody = "<br/> Please click on the following link to view the details of the Travel Request." + "<br/>Travel Request: <a href='" + link + "'>" + travelRequests.ApplicationNumber + "</a>";

            mail.Send();
        }

        private string GenerateNextAgencyID(string currentID)
        {
            string[] RFQno = currentID.Split('-');
            return RFQno[0] + '-' + RFQno[1] + '-' + RFQno[2] + '-' + String.Format("{0:D4}", (Convert.ToInt32(RFQno[3]) + 1));
        }

        private void checkErrorMessages()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
        }

        #endregion
    }
}