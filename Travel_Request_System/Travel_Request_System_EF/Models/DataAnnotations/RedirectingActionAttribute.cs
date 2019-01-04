using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Travel_Request_System_EF.CustomAuthentication;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class RedirectingActionAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            MembershipUser user;
            base.OnActionExecuting(filterContext);
            user = Membership.GetUser();
        
            if (user == null)
            {
                //filterContext.Controller.TempData["ErrorMessage"] = "The Session has expired. Please login again";
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Index",
                    ErrorID = 1
                }));
            }
        }

    }
}