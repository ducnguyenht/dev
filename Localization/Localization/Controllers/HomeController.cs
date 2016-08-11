using Localization.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Localization.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            var model = new Data { ID = 0, Name = string.Empty };
            return View(model);
        }

        [HttpPost]
        public ActionResult Post(Data model) {
            return View("Index", model);
        }
    }
}
