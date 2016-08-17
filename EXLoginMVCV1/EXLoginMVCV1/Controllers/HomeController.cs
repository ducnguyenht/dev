using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EXLoginMVCV1.Models;

namespace EXLoginMVCV1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			// DXCOMMENT: Pass a data model for GridView
            return View(NorthwindDataProvider.GetCustomers());	
        }
		
		public ActionResult GridViewPartialView() 
		{
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
			return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        }
    
	}
}