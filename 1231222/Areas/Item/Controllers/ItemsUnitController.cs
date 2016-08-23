using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Item.Models;

namespace MVC.Areas.Item.Controllers
{
    public class ItemsUnitController : Controller
    {
        // GET: Item/ItemsUnit
        public ActionResult ItemsUnitIndex()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ItemsUnitPartial()
        {
            //var model = new object[0];
            return PartialView("_ItemsUnitPartial", ItemUnitList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsUnitPartialAddNew(APIItemUnit item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    ItemUnitList.AddItemUnit(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ItemsUnitPartial", ItemUnitList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsUnitPartialUpdate(APIItemUnit item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    ItemUnitList.UpdateItemUnit(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ItemsUnitPartial", ItemUnitList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsUnitPartialDelete(System.String Code)
        {
            var model = new object[0];
            if (Code != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    ItemUnitList.DeleteItemUnit(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ItemsUnitPartial", ItemUnitList.GetData());
        }
    }
}