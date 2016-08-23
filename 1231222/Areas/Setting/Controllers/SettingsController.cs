using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Setting.Models;

namespace MVC.Areas.Setting.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class SettingsController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            var model = new Data { ID = 0, Name = string.Empty };
            return View(model);
        }
        //public string Index()
        //{
        //    return "sadfsadf";
        //}
        [HttpPost]
        public ActionResult Post(Data model)
        {
            return View("Index", model);
        }

       
    }
}