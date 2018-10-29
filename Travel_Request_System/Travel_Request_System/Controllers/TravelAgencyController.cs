using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Travel_Request_System.Controllers
{
    public class TravelAgencyController : Controller
    {
        // GET: TravelAgency
        public ActionResult Index()
        {
            return View();
        }

        // GET: TravelAgency/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TravelAgency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TravelAgency/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TravelAgency/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TravelAgency/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: TravelAgency/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TravelAgency/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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