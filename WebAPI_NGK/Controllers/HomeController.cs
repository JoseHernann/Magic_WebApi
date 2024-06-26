using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI_NGK.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //string en = Utilities.Security.EncryptData("SELECT * DATA");

            //Console.WriteLine(en);
            return View();
        }

        public ActionResult Help()
        {
            ViewBag.Title = "Help";
            return View();
        }
    }
}
