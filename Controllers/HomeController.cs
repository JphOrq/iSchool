using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application About page here.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your application Contact page here.";

            return View();
        }
    }
}