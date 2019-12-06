using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.DataAccess;
using Travel_Request_System_EF.Mail;
using Travel_Request_System_EF.Models.ViewModel;
using User = Travel_Request_System_EF.Models.Users;

namespace Travel_Request_System_EF.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            int ErrorID = Convert.ToInt32(Request.Params["ErrorID"]);
            if (ErrorID == 1)
            {
                ViewBag.ErrorMessage = new string[] { "The Session has expired. Please login again" };
            }
            return View();
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("authCookie", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account", null);
        }

        public ActionResult ForgotPassword() => View();

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.UserName, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.UserName, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Roles.Select(r => r.RoleName).ToList()
                        };

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(60), loginView.RememberMe, JsonConvert.SerializeObject(userModel)
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("authCookie", enTicket)
                        {
                            Expires = authTicket.Expiration
                        };
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "BTC");
                    }
                }
            }
            ModelState.AddModelError("", "Something Wrong : Username or Password invalid");
            return View(loginView);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult TermsnCond()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationView registrationView)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                // Email Verification
                string userName = Membership.GetUserNameByEmail(registrationView.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    return View(registrationView);
                }

                //Save User Data
                using (AuthenticationDB dbContext = new AuthenticationDB())
                {
                    var user = new User()
                    {
                        Username = registrationView.Username,
                        FirstName = registrationView.FirstName,
                        LastName = registrationView.LastName,
                        Email = registrationView.Email,
                        Password = registrationView.Password,
                        ActivationCode = Guid.NewGuid(),
                    };

                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }

                //Verification Email
                //VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                messageRegistration = "Your account has been created successfully. ^_^";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something Wrong!";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registrationView);
        }

        [HttpGet]
        public ActionResult ActivationAccount(string id)
        {
            bool statusAccount = false;
            using (AuthenticationDB dbContext = new DataAccess.AuthenticationDB())
            {
                var userAccount = dbContext.Users.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();

                if (userAccount != null)
                {
                    userAccount.IsActive = true;
                    dbContext.SaveChanges();
                    statusAccount = true;
                }
                else
                {
                    ViewBag.Message = "Something Wrong !!";
                }
            }
            ViewBag.Status = statusAccount;
            return View();
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

            try
            {
                mail.Send();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = new List<string>() { ex.Message, "Unable to send Message" };
            }
        }

        [HttpGet]
        public ActionResult ForgotPasswordClick(string EmailID)
        {
            bool statusReset = false;
            string messageReset = string.Empty;

            if (ModelState.IsValid)
            {
                // Email Verification
                string userName = Membership.GetUserNameByEmail(EmailID);
                if (string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email does not Exist");
                    return View();
                }
                else
                {
                    var user = Membership.GetUser(userName);
                    //Verification Email
                    //ForgotPasswordEmail(user.Email, user.UserName, user.GetPassword());
                    messageReset = "Your account password has been sent to your email.";
                    statusReset = true;
                }
            }
            else
            {
                messageReset = "Something Wrong!";
            }
            ViewBag.Message = messageReset;
            ViewBag.Status = statusReset;

            return View();
        }

        [NonAction]
        public void ForgotPasswordEmail(string email, string userName, string password)
        {
            SendMail mail = new SendMail();
            mail.ToAddresses.Add(email);
            mail.MailSubject = "Travel Request Password information !";
            mail.MailBody = "<br/> This mail is an auto-generated Email to your response to Forgot Password" + "<br/>" +
                "UserName: " + userName + "<br/> Password: " + password + "<br/>";

            try
            {
                mail.Send();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = new List<string>() { ex.Message, "Unable to send Message" };
            }
        }
    }
}