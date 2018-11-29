using System;
using System.Web.Mvc;

namespace Travel_Request_System_EF.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;
            ViewBag.StatusCode = statusCode + " Error";
            ViewBag.ErrorText = exception.Message;
            return View();
        }
    }
}