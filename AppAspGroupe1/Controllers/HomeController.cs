using AppAspGroupe1.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppAspGroupe1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Utils.WriteLogSystem("Juste un test", "HomeController-Index");
            this.Flash("Bienvenue :)", Flashlevel.Info);
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
    }
}