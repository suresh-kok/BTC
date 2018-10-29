using System.Web.Mvc;

namespace Travel_Request_System.Controllers
{
    public class TravelRequestController : Controller
    {
        // GET: TravelRequest
        public ActionResult Index()
        {
            return View();
        }

        // GET: TravelRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TravelRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TravelRequest/Create
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

        // GET: TravelRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TravelRequest/Edit/5
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

        // GET: TravelRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TravelRequest/Delete/5
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