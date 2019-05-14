using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel_Experts_MVC.Controllers
{
    public class HomeController : Controller
    {
        private TravelExpertsEntities db = new TravelExpertsEntities(); // context object
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

        public ActionResult Packages()
        {
            List<Package> packages = db.Packages.ToList(); // gets data from the database via context object
            return View(packages);
        }
    }
}