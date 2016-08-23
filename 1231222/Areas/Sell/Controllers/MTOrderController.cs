using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Sell.Models;

namespace MVC.Areas.Sell.Controllers
{
    public class MTOrderController : Controller
    {
        // GET: Sell/MTOrder
        APIMTOrderList _MTOrderList = new APIMTOrderList();
        APIMTOrderItemList _MTOrderItem = new APIMTOrderItemList();
        APIMTOrderPromotionList _MTOrderPro = new APIMTOrderPromotionList();
        public ActionResult SalesMTOrder()
        {
            return View();
        }

        #region MTOrder
        [ValidateInput(false)]
        public ActionResult MTOrderPartial()
        {
            var model = new object[0];
            return PartialView("_MTOrderPartial", _MTOrderList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderPartialAddNew(MVC.Areas.Sell.Models.APIMTOrder item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    _MTOrderList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MTOrderPartial", _MTOrderList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderPartialUpdate(MVC.Areas.Sell.Models.APIMTOrder item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    _MTOrderList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MTOrderPartial", _MTOrderList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderPartialDelete(int Code)
        {
            var model = new object[0];
            if (Code != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _MTOrderList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_MTOrderPartial", _MTOrderList.GetData());
        }
        #endregion

        #region MTOrderNew
       
        [HttpGet]
        public ActionResult MTOrderNew(int code)
        {
            APIMTOrder MTOrder = _MTOrderList.GetData().FirstOrDefault(U => U.Code == code);
            if (MTOrder == null)
            {
                MTOrder = new APIMTOrder();
                MTOrder.Code = code;
            }
            return View("MTOrderNew", MTOrder);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderNew(APIMTOrder MTOrder)
        {
            if (!ModelState.IsValid)
                return View("MTOrderNew", MTOrder);

            _MTOrderList.GetData().Add(MTOrder);
            return RedirectToAction("SalesMTOrder");
        }

        
        #endregion

        #region MTOrderEdit
       
        [HttpGet]
        public ActionResult MTOrderEdit(int code)
        {
            APIMTOrder MTOrder = _MTOrderList.GetData().FirstOrDefault(U => U.Code == code);
            return View("MTOrderEdit", MTOrder);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderEdit(APIMTOrder GTOrder)
        {
            if (!ModelState.IsValid)
                return View("MTOrderEdit", GTOrder);

            var model = _MTOrderList.GetData();
            var modelItem = model.FirstOrDefault(it => it.Code == GTOrder.Code);
            if (modelItem != null)
            {
                this.UpdateModel(modelItem);
            }
            return RedirectToAction("SalesMTOrder");
        }

       
        #endregion

        #region MTOrderDelete

        public ActionResult MTOrderDelete(int code)
        {
            var model = _MTOrderList.GetData();
            var modelItem = model.FirstOrDefault(it => it.Code == code);
            if (modelItem != null)
            {
                _MTOrderList.GetData().Remove(modelItem);
            };
            return RedirectToAction("SalesMTOrder");
        }
       
        #endregion

        #region MTOrderItem
        
        [ValidateInput(false)]
        public ActionResult MTOrderItemPartial()
        {
            var model = new object[0];
            return PartialView("_MTOrderItemPartial", _MTOrderItem.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderItemPartialAddNew(MVC.Areas.Sell.Models.APIMTOrderItem item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    _MTOrderItem.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MTOrderItemPartial", _MTOrderItem.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderItemPartialUpdate(MVC.Areas.Sell.Models.APIMTOrderItem item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    _MTOrderItem.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MTOrderItemPartial", _MTOrderItem.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderItemPartialDelete(int Code)
        {
            var model = new object[0];
            if (Code >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _MTOrderItem.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_MTOrderItemPartial", _MTOrderItem.GetData());
        }
        #endregion

        #region MTOrderPromotion
        [ValidateInput(false)]
        public ActionResult MTOrderPromotionPartial()
        {
            var model = new object[0];
            return PartialView("_MTOrderPromotionPartial", _MTOrderPro.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderPromotionPartialAddNew(MVC.Areas.Sell.Models.APIMTOrderPromotion item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    _MTOrderPro.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MTOrderPromotionPartial", _MTOrderPro.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderPromotionPartialUpdate(MVC.Areas.Sell.Models.APIMTOrderPromotion item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    _MTOrderPro.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MTOrderPromotionPartial", _MTOrderPro.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MTOrderPromotionPartialDelete(int Code)
        {
            var model = new object[0];
            if (Code >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _MTOrderPro.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_MTOrderPromotionPartial", _MTOrderPro.GetData());
}
        #endregion
    }
}