using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Travel_Request_System.Controllers
{
    public class TravelDetailsController : Controller
    {
        // GET: TravelDetails
        public ActionResult Index()
        {
            return View();
        }

        // GET: TravelDetails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TravelDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TravelDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TravelDetails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TravelDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TravelDetails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TravelDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}