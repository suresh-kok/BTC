using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Request_System_EF.CustomAuthentication;

namespace Travel_Request_System_EF.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}