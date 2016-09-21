using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.Mvc;
using CS.Model;
using DevExpress.Web.ASPxGridView;

namespace CS.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "Welcome to DevExpress Extensions for ASP.NET MVC!";
            ViewData["CategoryList"] = MyModel.GetCategories();
            ViewData["ProductList"] = MyModel.GetProducts();

            return View();
        }

        public ActionResult GridCategoriesAction() {
            return PartialView("GridCategories", MyModel.GetCategories());
        }
        public ActionResult GridProductsAction(int? CategoryID) {
            return PartialView("GridProducts",  MyModel.GetProductsByCategory(CategoryID));
        }	
    }

}
