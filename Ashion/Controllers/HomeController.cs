using Ashion.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ashion.Controllers
{
    public class HomeController : Controller
    {
        AshionEntities5 db = new AshionEntities5();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult Contact(Query model)
        {
            Query query = new Query();
            query.Name = model.Name;
            query.EmailId = model.EmailId;
            query.Query1 = model.Query1;

             ViewBag.QueryId = query.QueryId;

             db.Queries.Add(query);
             db.SaveChanges();

            
            return RedirectToAction("Blog");

        }

        
         public ActionResult Blog()
        {
            return View();

        }
    }
}