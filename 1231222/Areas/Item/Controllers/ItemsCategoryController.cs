using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Item.Models;

namespace MVC.Areas.Item.Controllers
{
    public class ItemsCategoryController : Controller
    {
        // GET: Item/ItemsCategory
        public ActionResult ItemsCategoryIndex()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ItemCategoryPartial()
        {
            //var model = new object[0];
            return PartialView("_ItemCategoryPartial", ItemCategoryList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ItemCategoryPartialAddNew(MVC.Areas.Item.Models.APIItemCategory item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    ItemCategoryList.AddItemCategory(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ItemCategoryPartial", ItemCategoryList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ItemCategoryPartialUpdate(MVC.Areas.Item.Models.APIItemCategory item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    ItemCategoryList.UpdateItemCategory(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ItemCategoryPartial", ItemCategoryList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ItemCategoryPartialDelete(System.String Code)
        {
            var model = new object[0];
            if (Code != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    ItemCategoryList.DeleteItemCategory(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ItemCategoryPartial", ItemCategoryList.GetData());
        }
    }
}