using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T258155.Areas.PopUpGrid.Controllers
{
    public class ProductsController : Controller
    {
        // GET: PopUpGrid/Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GridViewPartial()
        {
            return PartialView(NorthwindDataProvider.GetProducts());
        }

        public ActionResult GridViewAddNewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    NorthwindDataProvider.InsertProduct(product);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts());
        }

        public ActionResult GridViewUpdatePartial([ModelBinder(typeof(DevExpressEditorsBinder))]Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    NorthwindDataProvider.UpdateProduct(product);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts());
        }

        public ActionResult GridViewDeletePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int productID)
        {
            try
            {
                NorthwindDataProvider.DeleteProduct(productID);
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }

            return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts());
        }

        [ValidateInput(false)]
        public ActionResult POPGridViewPartial()
        {
            var model = new object[0];
            return PartialView("_POPGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult POPGridViewPartialAddNew(Product item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_POPGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult POPGridViewPartialUpdate(Product item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_POPGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult POPGridViewPartialDelete(System.Int32 ProductID)
        {
            var model = new object[0];
            if (ProductID >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_POPGridViewPartial", model);
        }
    }
}