using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Travel_Request_System_EF.Models;

namespace Travel_Request_System_EF.Controllers
{
    public class TravelRequestsController : Controller
    {
        private HRWorksEntities db = new HRWorksEntities();

        // GET: TravelRequests
        public async Task<ActionResult> Index()
        {
            var travelRequests = db.TravelRequests.Include(t => t.City).Include(t => t.Currency).Include(t => t.User);
            return View(await travelRequests.ToListAsync());
        }

        // GET: TravelRequests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
            if (travelRequest == null)
            {
                return HttpNotFound();
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();
            return View(travelRequest);
        }

        // GET: TravelRequests/Create
        public ActionResult Create()
        {
            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();
            return View();
        }

        // POST: TravelRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TravelRequestID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequest travelRequest)
        {
            if (ModelState.IsValid)
            {
                db.TravelRequests.Add(travelRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            return View(travelRequest);
        }

        // GET: TravelRequests/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
            if (travelRequest == null)
            {
                return HttpNotFound();
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();

            return View(travelRequest);
        }

        // POST: TravelRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TravelRequestID,UserID,ApplicationNumber,PortOfOriginID,PortOfDestinationID,TicketClass,DailyAllowance,CurrencyID,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,DropOffLocation,DropOffDate,PreferrefVehicle,CheckInDate,CheckOutDate,ApprovalLevel,ApprovalBy,ApprovalRemarks,CreateOn,CreatedBy,ModifiedOn,ModifiedBy,ReturnDate")] TravelRequest travelRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelRequest).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = db.Cities.ToList();
            ViewBag.CurrencyID = db.Currencies.ToList();
            ViewBag.ApprovalBy = db.Users.ToList();
            return View(travelRequest);
        }

        // GET: TravelRequests/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
            if (travelRequest == null)
            {
                return HttpNotFound();
            }
            return View(travelRequest);
        }

        // POST: TravelRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TravelRequest travelRequest = await db.TravelRequests.FindAsync(id);
            db.TravelRequests.Remove(travelRequest);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
