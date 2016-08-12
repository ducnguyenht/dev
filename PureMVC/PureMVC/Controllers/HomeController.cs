using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PureMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: http://localhost:5913/
        //public string Index()
        //{
        //    return "Hello from Home";
        //}
        public ActionResult Index()
        {
            return View();
        }
    }
}