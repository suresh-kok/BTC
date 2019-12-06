using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.Mail;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.DataAnnotations;
using Travel_Request_System_EF.Models.ViewModel;
using Roles = Travel_Request_System_EF.Models.Roles;

namespace Travel_Request_System_EF.Controllers
{
    [HandleError]
    [RedirectingAction]
    public class BTCController : Controller
    {
        private static MembershipUser user;
        private static string[] roles;
        private static Users dbuser;
        private static string empCode;

        public BTCController()
        {
            try
            {
                user = Membership.GetUser();
                CustomRole customRole = new CustomRole();
                roles = customRole.GetRolesForUser(user.UserName);
                ViewBag.RoleDetails = roles.ToList()[0];
                IsLoggedIn();
                using (BTCEntities db = new BTCEntities())
                {
                    dbuser = db.Users.Where(a => a.Username == user.UserName && a.IsActive && a.IsDeleted == false).Include(a => a.Roles).Include(a => a.TravelRequests).Include(a => a.TravelRequests1).FirstOrDefault();
                    ViewBag.UserDetails = dbuser;
                    empCode = dbuser.HRW_Employee.EmployeeCode;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Your Login has expired!! Please login again.";
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

        [CustomAuthorize(Roles = "Employee,HR,Manager,Admin")]
        public ActionResult TravelRequest()
        {
            return View();
        }

        #endregion Employee

        #region HR

        [CustomAuthorize(Roles = "HR")]
        public ActionResult HRDashboard()
        {
            return View(ViewApprovalTravelRequests((int)ApprovalLevels.ApprovedByManager, empCode));
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
                    TravelRequests travelRequest = db.TravelRequests.Include(a => a.Users).Include(a => a.Users1).Include("Users1.HRW_Employee").Include("Users.HRW_Employee").SingleOrDefault(s => s.ID == id);
                    if (travelRequest == null)
                    {
                        ViewBag.ErrorMessage = "Invalid Travel Request ID";
                        return View();
                        //return HttpNotFound("Invalid Travel Request ID");
                    }

                    ViewBag.Cities = db.City.ToList();
                    ViewBag.Currencies = db.Currency.ToList();
                    ViewBag.ApprovalBy = db.Users.ToList();
                    using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService(travelRequest.Users1.HRW_Employee.EmployeeCode))
                    {
                        ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                    }
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
        public ActionResult ApproveHRRequest(FormCollection collection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var id = Convert.ToInt32(collection["ID"]);
                TravelRequests travelRequest = db.TravelRequests.Include(a => a.Users).Include(a => a.Users1).Include("Users1.HRW_Employee").Include("Users.HRW_Employee").SingleOrDefault(s => s.ID == id);

                if (ModelState.IsValid)
                {
                    travelRequest.ApprovalLevel = (int)ApprovalLevels.ApprovedByHR;
                    travelRequest.ApprovalBy = dbuser.ID;
                    travelRequest.ApprovalRemarks = travelRequest.ApprovalRemarks + "\n HR Remarks: Approved By -" + dbuser.FirstName + ", " + dbuser.LastName + " \n Updated On: " + DateTime.Now.ToLongDateString() + " \n Comments: " + collection["ApprovalRemarksUser"];
                    db.Entry(travelRequest).State = EntityState.Modified;
                    db.SaveChanges();
                    //NotificationEmail(travelRequest, true);
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
                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService(travelRequest.Users1.HRW_Employee.EmployeeCode))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                return View(travelRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectHRRequest(FormCollection collection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var id = Convert.ToInt32(collection["ID"]);
                TravelRequests travelRequest = db.TravelRequests.Include(a => a.Users).Include(a => a.Users1).Include("Users1.HRW_Employee").Include("Users.HRW_Employee").SingleOrDefault(s => s.ID == id);

                if (ModelState.IsValid)
                {
                    travelRequest.ApprovalLevel = (int)ApprovalLevels.RejectedByHR;
                    travelRequest.ApprovalBy = dbuser.ID;
                    travelRequest.ApprovalRemarks = travelRequest.ApprovalRemarks + "\n HR Remarks: Rejected By -" + dbuser.FirstName + ", " + dbuser.LastName + " \n Updated On: " + DateTime.Now.ToLongDateString() + " \n Comments: " + collection["ApprovalRemarksUser"];
                    db.Entry(travelRequest).State = EntityState.Modified;
                    db.SaveChanges();
                    //NotificationEmail(travelRequest, false);
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
                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService(travelRequest.Users1.HRW_Employee.EmployeeCode))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                return View(travelRequest);
            }
        }

        #endregion HR

        #region Manager

        [CustomAuthorize(Roles = "Manager")]
        public ActionResult ManagerDashboard()
        {
            using (var dbcontext = new BTCEntities())
            {
                return View(ViewApprovalTravelRequests((int)ApprovalLevels.ToBeApproved, empCode));
            }
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
                    TravelRequests travelRequest = db.TravelRequests.Include(a => a.Users).Include(a => a.Users1).Include("Users1.HRW_Employee").Include("Users.HRW_Employee").SingleOrDefault(s => s.ID == id);
                    if (travelRequest == null)
                    {
                        ViewBag.ErrorMessage = "Invalid Travel Request ID";
                        return View();
                        //return HttpNotFound("Invalid Travel Request ID");
                    }

                    ViewBag.Cities = db.City.ToList();
                    ViewBag.Currencies = db.Currency.ToList();
                    ViewBag.ApprovalBy = db.Users.ToList();
                    using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService(travelRequest.Users1.HRW_Employee.EmployeeCode))
                    {
                        ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                    }
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
        public ActionResult ApproveManagerRequest(FormCollection collection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var id = Convert.ToInt32(collection["ID"]);
                TravelRequests travelRequest = db.TravelRequests.Include(a => a.Users).Include(a => a.Users1).Include("Users1.HRW_Employee").Include("Users.HRW_Employee").SingleOrDefault(s => s.ID == id);

                if (ModelState.IsValid)
                {
                    travelRequest.AdditionalAllowance = Convert.ToInt32(collection["AdditionalAllowance"]);
                    travelRequest.ApprovalLevel = (int)ApprovalLevels.ApprovedByManager;
                    travelRequest.ApprovalBy = dbuser.ID;
                    travelRequest.ApprovalRemarks = travelRequest.ApprovalRemarks + "\n Manager Remarks: Approved By -" + dbuser.FirstName + ", " + dbuser.LastName + " \n Updated On: " + DateTime.Now.ToLongDateString() + " \n Comments: " + collection["ApprovalRemarksUser"];
                    db.Entry(travelRequest).State = EntityState.Modified;
                    db.SaveChanges();
                    //NotificationEmail(travelRequest, true);
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
                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService(travelRequest.Users1.HRW_Employee.EmployeeCode))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                return View(travelRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectManagerRequest(FormCollection collection)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var id = Convert.ToInt32(collection["ID"]);
                TravelRequests travelRequest = db.TravelRequests.Include(a => a.Users).Include(a => a.Users1).Include("Users1.HRW_Employee").Include("Users.HRW_Employee").SingleOrDefault(s => s.ID == id);

                if (ModelState.IsValid)
                {
                    travelRequest.ApprovalLevel = (int)ApprovalLevels.RejectedByManager;
                    travelRequest.ApprovalBy = dbuser.ID;
                    travelRequest.ApprovalRemarks = travelRequest.ApprovalRemarks + "\n Manager Remarks: Rejected By -" + dbuser.FirstName + ", " + dbuser.LastName + " \n Updated On: " + DateTime.Now.ToLongDateString() + " \n Comments: " + collection["ApprovalRemarksUser"];
                    db.Entry(travelRequest).State = EntityState.Modified;
                    db.SaveChanges();
                    //NotificationEmail(travelRequest, false);
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
                using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService(travelRequest.Users1.HRW_Employee.EmployeeCode))
                {
                    ViewBag.FullEmployeeDetails = EmpDBService.FullEmployeeDetails();
                }
                return View(travelRequest);
            }
        }

        #endregion Manager

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

        #endregion TravelCo

        #region Admin

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AdminDashboard()
        {
            List<Users> usersList = new List<Users>();
            using (BTCEntities db = new BTCEntities())
            {
                ViewBag.usersList = db.Users.Include(a => a.Roles).Include(a => a.HRW_Employee).ToList();
            }
            List<FullEmployeeDetail> employeeList = new List<FullEmployeeDetail>();
            using (EmployeeDetailsDBService db = new EmployeeDetailsDBService(empCode))
            {
                ViewBag.employeeList = db.AllEmployeeDetails(50);
            }
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

                CheckErrorMessages();
                return View(travelAgency);
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTravelAgency([Bind(Include = "ID,AgencyCode,CompanyName,Address,Telephone,Fax,Mobile,Landline,ContactPerson,Email,IsActive")] TravelAgency travelAgency)
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

            CheckErrorMessages();
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
        public async Task<ActionResult> EditTravelAgency([Bind(Include = "ID,AgencyCode,CompanyName,Address,Telephone,Fax,Mobile,Landline,ContactPerson,Email,IsActive")] TravelAgency travelAgency)
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
            CheckErrorMessages();
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
                CheckErrorMessages();
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
                travelAgency.IsActive = false;
                db.Entry(travelAgency).State = EntityState.Modified;
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
            List<FullEmployeeDetail> employeeList = new List<FullEmployeeDetail>();
            using (EmployeeDetailsDBService db = new EmployeeDetailsDBService(empCode))
            {
                ViewBag.employeeList = db.AllEmployeeDetails();
            }
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult ManageUsers()
        {
            List<Users> usersList = new List<Users>();
            using (BTCEntities db = new BTCEntities())
            {
                usersList = db.Users.Include(a => a.Roles).Include(a => a.HRW_Employee).Where(a => a.IsDeleted == false).OrderByDescending(a => a.ID).ToList();
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
                Users user = await db.Users.Include(a => a.HRW_Employee).FirstOrDefaultAsync(i => i.ID == id.Value);
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
                List<FullEmployeeDetail> allEmployees = new List<FullEmployeeDetail>();
                using (EmployeeDetailsDBService employeeDetailsDBService = new EmployeeDetailsDBService(empCode))
                {
                    allEmployees = employeeDetailsDBService.AllEmployeeDetails();
                }
                ViewBag.allRoles = allRoles;
                ViewBag.allEmployees = allEmployees;
                CheckErrorMessages();
                return View();
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "ID,Username,FirstName,LastName,Email,Password,IsActive,ActivationCode")] Users userObj, FormCollection form)
        {
            try
            {
                bool statusRegistration = false;
                string messageRegistration = string.Empty;
                int empValue = Convert.ToInt32(form["EmployeeID"].ToString());
                userObj.HREmployeeID = empValue;

                int DDLValue = Convert.ToInt32(form["RoleID"].ToString());

                string userName = Membership.GetUserNameByEmail(userObj.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    var errlist = ModelState.Values.Where(e => e.Errors.Count > 0).Select(a => a.Errors);
                    List<string> sberr = new List<string>();
                    foreach (var item in errlist)
                    {
                        sberr.Add(item[0].ErrorMessage);
                    }
                    TempData["ErrorMessage"] = sberr.ToList();
                    CheckErrorMessages();
                    return RedirectToAction("CreateUser", userObj);
                }

                using (BTCEntities db = new BTCEntities())
                {
                    List<Roles> allRoles = db.Roles.ToList();
                    List<FullEmployeeDetail> allEmployees = new List<FullEmployeeDetail>();
                    using (EmployeeDetailsDBService employeeDetailsDBService = new EmployeeDetailsDBService(empCode))
                    {
                        allEmployees = employeeDetailsDBService.AllEmployeeDetails();
                    }
                    ViewBag.allRoles = allRoles;
                    ViewBag.allEmployees = allEmployees;

                    var role = db.Roles.Where(a => a.ID == DDLValue).FirstOrDefault();
                    var user = new Users()
                    {
                        Username = userObj.Username,
                        FirstName = userObj.FirstName,
                        LastName = userObj.LastName,
                        Email = userObj.Email,
                        Password = userObj.Password,
                        HREmployeeID = userObj.HREmployeeID,
                        ActivationCode = Guid.NewGuid(),
                    };

                    user.Roles.Add(role);
                    db.Users.Add(user);

                    db.SaveChanges();
                }

                //VerificationEmail(userObj.Email, userObj.ActivationCode.ToString());
                messageRegistration = "Your account has been created successfully. ^_^";
                statusRegistration = true;

                ViewBag.Message = messageRegistration;
                ViewBag.Status = statusRegistration;

                return RedirectToAction("ManageUsers");
            }
            catch (Exception)
            {
                return View("CreateUser", userObj);
            }
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
                Users user = await db.Users.Include(a => a.HRW_Employee).FirstOrDefaultAsync(i => i.ID == id.Value);
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
        public async Task<ActionResult> EditUser(Users user, FormCollection form)
        {
            using (BTCEntities db = new BTCEntities())
            {
                ModelState.Remove("HREmployeeID");
                if (ModelState.IsValid)
                {
                    string DDLValue = form["RoleName"].ToString();

                    var role = db.Roles.Where(a => a.RoleName == DDLValue).FirstOrDefault();
                    if (!user.Roles.Contains(role))
                    {
                        user.Roles.Add(role);
                    }
                    user.HREmployeeID = Convert.ToInt32(form["HREmployeeID"]);
                    db.Entry(user).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("ManageUsers");
                }
                user = await db.Users.Include(a => a.HRW_Employee).FirstOrDefaultAsync(i => i.ID == user.ID);
                ViewBag.RoleName = user.Roles.FirstOrDefault().RoleName;
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
                Users user = await db.Users.Include(a => a.HRW_Employee).FirstOrDefaultAsync(i => i.ID == id.Value);
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
                user.IsActive = false;
                user.IsDeleted = true;
                db.Entry(user).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                await db.SaveChangesAsync();
                return RedirectToAction("ManageUsers");
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AnyEmployeeDetails(string id)
        {
            using (EmployeeDetailsDBService employeeDetailsDBService = new EmployeeDetailsDBService("", id))
            {
                var FullEmployeeDetails = employeeDetailsDBService.FullEmployeeDetails();
                return View(FullEmployeeDetails);
            }
        }

        #endregion Admin

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
            EmployeeDetailsDBService employeeDetailsDBService = new EmployeeDetailsDBService(empCode);
            ViewBag.FullEmployeeDetails = employeeDetailsDBService.FullEmployeeDetails();
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
                MembershipUser userdet = customMembership.GetUser(user.UserName, true);

                if (userdet == null || (userdet != null && string.Compare(oldPassword, newPassword, StringComparison.OrdinalIgnoreCase) == 0))
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
                        messageReset = "Your account password has been changed.";
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

        private List<TravelRequests> ViewApprovalTravelRequests(int level, string employeeCode = "")
        {
            using (BTCEntities db = new BTCEntities())
            {
                if (!string.IsNullOrEmpty(employeeCode) && level != 2)
                {
                    using (EmployeeDetailsDBService EmpDBService = new EmployeeDetailsDBService(empCode))
                    {
                        List<string> EmpCodes = EmpDBService.EmployeesUnderDepartmentHeadDetails(empCode);
                        var tr = db.TravelRequests.Include(a => a.City).Include(a => a.City1).Include(a => a.Users).Include(a => a.Users1).Where(a => a.ApprovalLevel == level).ToList();
                        var trf = tr.Where(a => EmpCodes.Any(s => s.Equals(a.Users1.HRW_Employee.EmployeeCode))).ToList();
                        return trf;
                    }
                }
                else if (level == 2)
                {
                    var tr = db.TravelRequests.Include(a => a.City).Include(a => a.City1).Include(a => a.Users).Include(a => a.Users1).Where(a => a.ApprovalLevel == level).ToList();
                    return tr;
                }
                else
                {
                    var tr = db.TravelRequests.Include(a => a.City).Include(a => a.City1).Include(a => a.Users).Include(a => a.Users1).Where(a => a.ApprovalLevel == level).ToList();
                    return tr;
                }
            }
        }

        private List<TravelRequests> ViewAllTravelRequests()
        {
            using (BTCEntities db = new BTCEntities())
            {
                return db.TravelRequests.Include(a => a.City).Include(a => a.City1).Include(a => a.Users).Include(a => a.Users1).OrderBy(a => a.CreateOn).ToList();
            }
        }

        #endregion MultiRole

        #region General

        public ActionResult TermsnCond()
        {
            return View();
        }

        private void IsLoggedIn()
        {
            bool Val = true;
            ViewBag.LoggedOut = !Val;
            ViewBag.messages = Val;
            ViewBag.notifications = Val;
            ViewBag.tasks = Val;
            ViewBag.IsEmployee = !Val;
            ViewBag.IsHR = !Val;
            ViewBag.IsTravelCo = !Val;
            ViewBag.IsManager = !Val;
            ViewBag.IsAdmin = !Val;
            ViewBag.username = user.UserName;
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
            string url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            string link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            SendMail mail = new SendMail
            {
                ToAddresses = new List<string> { email },
                MailSubject = "Activation Account !",
                MailBody = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>"
            };

            try
            {
                mail.Send();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = new List<string>() { ex.Message, "Unable to send Message" };
            }
        }

        public void NotificationEmail(TravelRequests travelRequests, bool IsApproved)
        {
            string url = string.Format("/TravelRequests/Details/{0}", travelRequests.ID);
            string link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            SendMail mail = new SendMail
            {
                ToAddresses = new List<string> { travelRequests.Users1.Email },
                MailSubject = IsApproved
                ? "Travel Request " + travelRequests.ApplicationNumber + " Approved!"
                : "Travel Request " + travelRequests.ApplicationNumber + " Rejected!",
                MailBody = "<br/> Please click on the following link to view the details of the Travel Request." + "<br/>Travel Request: <a href='" + link + "'>" + travelRequests.ApplicationNumber + "</a>"
            };

            try
            {
                mail.Send();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = new List<string>() { ex.Message, "Unable to send Message" };
            }
        }

        private string GenerateNextAgencyID(string currentID)
        {
            string[] RFQno = currentID.Split('-');
            return RFQno[0] + '-' + RFQno[1] + '-' + RFQno[2] + '-' + String.Format("{0:D4}", (Convert.ToInt32(RFQno[3]) + 1));
        }

        private void CheckErrorMessages()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
        }

        public string GetEmployeeDetails(string id)
        {
            using (EmployeeDetailsDBService employeeDetailsDBService = new EmployeeDetailsDBService("", id))
            {
                return new JavaScriptSerializer().Serialize(employeeDetailsDBService.FullEmployeeDetails());
            }
        }

        #endregion General
    }
}