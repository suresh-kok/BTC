using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Travel_Request_System_EF.Models.ViewModel
{
    public class SessionContext
    {
        public void SetAuthenticationToken(string name, bool isPersistant, LoginViewModel userData)
        {
            string data = null;
            if (userData != null)
            {
                data = new JavaScriptSerializer().Serialize(userData);
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddYears(1), isPersistant, userData.UserId.ToString());

            string cookieData = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieData)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public LoginViewModel GetUserData()
        {
            LoginViewModel userData = null;

            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                    userData = new JavaScriptSerializer().Deserialize(ticket.UserData, typeof(LoginViewModel)) as LoginViewModel;
                }
            }
            catch (Exception)
            {
            }

            return userData;
        }
    }
}