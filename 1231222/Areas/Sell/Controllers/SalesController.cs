using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Sell.Models;

namespace MVC.Areas.Sell.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        // GET: Sell/Sales

        APISalesList _APISalesList = new APISalesList();
            
        public ActionResult Sales()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SalesPartial()
        {
            var model = new object[0];
            return PartialView("_SalesPartial", _APISalesList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SalesPartialAddNew(MVC.Areas.Sell.Models.APISales item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model

                    _APISalesList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_SalesPartial", _APISalesList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesPartialUpdate(MVC.Areas.Sell.Models.APISales item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    _APISalesList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_SalesPartial", _APISalesList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesPartialDelete(System.Int32 ID)
        {
            var model = new object[0];
            if (ID >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _APISalesList.DeleteItem(ID);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_SalesPartial", _APISalesList.GetData());
        }
        #region Detail Panel 

        [ValidateInput(false)]
        public ActionResult DetailPanelPartial()
        {
            var model = new object[0];
            return PartialView("_DetailPanelPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DetailPanelPartialAddNew(MVC.Areas.Sell.Models.APIDetailPanel item)
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
            return PartialView("_DetailPanelPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DetailPanelPartialUpdate(MVC.Areas.Sell.Models.APIDetailPanel item)
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
            return PartialView("_DetailPanelPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DetailPanelPartialDelete(System.Int32 ID)
        {
            var model = new object[0];
            if (ID >= 0)
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
    return PartialView("_DetailPanelPartial", model);
}
        #endregion
        
        #region Post Panel
        [ValidateInput(false)]
        public ActionResult PostPanelPartial()
        {
            var model = new object[0];
            return PartialView("_PostPanelPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PostPanelPartialAddNew(MVC.Areas.Sell.Models.APIPostPanel item)
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
            return PartialView("_PostPanelPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PostPanelPartialUpdate(MVC.Areas.Sell.Models.APIPostPanel item)
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
            return PartialView("_PostPanelPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PostPanelPartialDelete(System.Int32 ID)
        {
            var model = new object[0];
            if (ID >= 0)
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
    return PartialView("_PostPanelPartial", model);
        }
        #endregion
    }
}