using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.DataAccess;
using Travel_Request_System_EF.Models.ViewModel;
using User = Travel_Request_System_EF.Models.User;

namespace Travel_Request_System_EF.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
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
            return RedirectToAction("Login", "Account", null);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

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
                            1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, JsonConvert.SerializeObject(userModel)
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
                VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
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

            var fromEmail = new MailAddress("mehdi.rami2012@gmail.com", "Activation Account - AKKA");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "******************";
            string subject = "Activation Account !";

            string body = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
            {
                smtp.Send(message);
            }
        }

        [HttpPost]
        public ActionResult ChangePasswordClick(string oldPassword, string newPassword)
        {
            bool statusReset = false;
            string messageReset = string.Empty;

            if (ModelState.IsValid)
            {
                var user = Membership.GetUser();

                if (user == null || (user != null && string.Compare(oldPassword, user.GetPassword(), StringComparison.OrdinalIgnoreCase) != 0))
                {
                    ModelState.AddModelError("Warning", "Sorry: The old password is incorrect");
                    return View();
                }
                else
                {
                    if (user.ChangePassword(oldPassword, newPassword))
                    {
                        messageReset = "Your account has password has been changed.";
                        statusReset = true;
                    }
                    else
                    {
                        messageReset = "Something Went Wrong!";
                        statusReset = false;
                    }
                }
            }
            else
            {
                messageReset = "Something Went Wrong!";
            }
            ViewBag.Message = messageReset;
            ViewBag.Status = statusReset;

            return View();
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
                    ForgotPasswordEmail(user.Email, user.UserName, user.GetPassword());
                    messageReset = "Your account has password has been sent to your email.";
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
            var fromEmail = new MailAddress("noreply@btc.com", "Travel Request information");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "******************";
            string subject = "Travel Request Password information !";

            string body = "<br/> This mail is an auto-generated Email to your response to Forgot Password" + "<br/>" +
                "UserName: " + userName + "<br/> Password: " + password + "<br/>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
            {
                smtp.Send(message);
            }
        }

    }
}