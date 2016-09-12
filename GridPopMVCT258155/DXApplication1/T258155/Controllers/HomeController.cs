using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T258155.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index() {
			return View();
		}

		public ActionResult GridViewPartial() {
			return PartialView(NorthwindDataProvider.GetProducts());
		}

		public ActionResult GridViewAddNewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]Product product) {
			if (ModelState.IsValid) {
				try {
					NorthwindDataProvider.InsertProduct(product);
				}
				catch (Exception e) {
					ViewData["EditError"] = e.Message;
				}
			}
			else
				ViewData["EditError"] = "Please, correct all errors.";

			return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts());
		}

		public ActionResult GridViewUpdatePartial([ModelBinder(typeof(DevExpressEditorsBinder))]Product product) {
			if (ModelState.IsValid) {
				try {
					NorthwindDataProvider.UpdateProduct(product);
				}
				catch (Exception e) {
					ViewData["EditError"] = e.Message;
				}
			}
			else
				ViewData["EditError"] = "Please, correct all errors.";

			return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts());
		}

		public ActionResult GridViewDeletePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int productID) {
			try {
				NorthwindDataProvider.DeleteProduct(productID);
			}
			catch (Exception e) {
				ViewData["EditError"] = e.Message;
			}

			return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts());
		}
	}
}