using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Item.Models;

namespace MVC.Areas.Item.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Item/Items

        public ActionResult ItemsIndex()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ItemsPartial()
        {
            //var model = new object[0];
            return PartialView("_ItemsPartial", ItemList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsPartialAddNew(APIItem item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    ItemList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ItemsPartial", ItemList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsPartialUpdate(APIItem item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    ItemList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ItemsPartial", ItemList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsPartialDelete(System.String Code)
        {
            var model = new object[0];
            if (Code != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    ItemList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ItemsPartial", ItemList.GetData());
        }
    }
}