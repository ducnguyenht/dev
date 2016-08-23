using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Item.Models;

namespace MVC.Areas.Item.Controllers
{
    public class ItemsTypeController : Controller
    {
        // GET: Item/ItemsType
        public ActionResult ItemsTypeIndex()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ItemsTypePartial()
        {
            var model = new object[0];
            return PartialView("_ItemsTypePartial", ItemTypeList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsTypePartialAddNew(MVC.Areas.Item.Models.APIItemType item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    ItemTypeList.AddItemType(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ItemsTypePartial", ItemTypeList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsTypePartialUpdate(MVC.Areas.Item.Models.APIItemType item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    ItemTypeList.UpdateItemType(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ItemsTypePartial", ItemTypeList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ItemsTypePartialDelete(System.String Code)
        {
            var model = new object[0];
            if (Code != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    ItemTypeList.DeleteItemType(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ItemsTypePartial", ItemTypeList.GetData());
        }
    }
}