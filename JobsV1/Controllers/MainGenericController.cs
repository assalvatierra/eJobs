using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsV1.Controllers
{
    public class MainGenericController : Controller, IMainPage
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult navHome()
        {
            return RedirectToAction("index","Default");
        }

        public ActionResult ApplicationPanel()
        {
            return RedirectToAction("Index", "CarRental");
        }

    }



}