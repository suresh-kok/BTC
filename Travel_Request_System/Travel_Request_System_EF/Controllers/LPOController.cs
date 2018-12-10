using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Travel_Request_System_EF.Models;

namespace Travel_Request_System_EF.Controllers
{
    public class LPOController : Controller
    {
        // GET: LPO
        public async Task<ActionResult> Index()
        {
            using (BTCEntities db = new BTCEntities())
            {
                var travelRequests = db.TravelRequests.Include(t => t.City).Include(t => t.City1).Include(t => t.Currency).Include(t => t.RFQ).Include(t => t.Users).Include(t => t.Users1);
                return View(await travelRequests.ToListAsync());
            }
        }

        public async Task<ActionResult> LPOProcessing(int? id)
        {
            return View();
        }
    }
}