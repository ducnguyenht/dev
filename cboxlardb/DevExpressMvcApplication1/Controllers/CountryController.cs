using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpressMvcApplication1.DAL;
using DevExpressMvcApplication1.Models;

namespace DevExpressMvcApplication1.Controllers
{
    public class CountryController : Controller
    {
        //
        // GET: /Country/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MultiColumnCountryComboBoxPartial()
        {
            //using (MyDbContext myDbContext = new MyDbContext())
            //{
            //    ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
            //    return PartialView();
            //}
            return PartialView();
        }

    }
}
