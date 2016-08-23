using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Sell.Models;

namespace MVC.Areas.Sell.Controllers
{
    public class FSOrderController : Controller
    {
        APIFSOrderItemList _FSOrderItem = new APIFSOrderItemList();
        APIFSOrderPromotionList _FSOrderPro = new APIFSOrderPromotionList();
        // GET: Sell/FSOrder
        public ActionResult FSOrder()
        {
            return View();
        }

        #region Grid main
        [ValidateInput(false)]
        public ActionResult GridView_FSOrderPartial()
        {
            //var model = new object[0];
            return PartialView("_GridView_FSOrderPartial", APIFSOrderList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView_FSOrderPartialAddNew(MVC.Areas.Sell.Models.APIFSOrder item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    APIFSOrderList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView_FSOrderPartial", APIFSOrderList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView_FSOrderPartialUpdate(MVC.Areas.Sell.Models.APIFSOrder item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    APIFSOrderList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView_FSOrderPartial", APIFSOrderList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView_FSOrderPartialDelete(System.Decimal Code)
        {
            var model = new object[0];
            if (Code != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    APIFSOrderList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView_FSOrderPartial", APIFSOrderList.GetData());
        }
        #endregion

        #region New in Detail
        [HttpGet]
        public ActionResult FSOrderNew(Decimal Code)
        {
            APIFSOrder FSOrder = APIFSOrderList.GetData().FirstOrDefault(u=>u.Code == Code);
            if (FSOrder == null)
            {
                FSOrder = new APIFSOrder();
                FSOrder.Code = Code;
            };
            return View("FSOrderNew", FSOrder);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FSOrderNew(APIFSOrder FSOrder)
        {
            if (!ModelState.IsValid)
                return View("FSOrderNew", FSOrder);

            APIFSOrderList.AddItem(FSOrder);
            return RedirectToAction("FSOrder");
        }

        #endregion

        #region Edit Detail

        [HttpGet]
        public ActionResult FSOrderEdit(decimal code)
        {
            APIFSOrder FSOrder = APIFSOrderList.GetData().FirstOrDefault(U => U.Code == code);
            return View("FSOrderEdit", FSOrder);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FSOrderEdit(APIFSOrder FSOrder)
        {
            if (!ModelState.IsValid)
                return View("FSOrderEdit", FSOrder);

            var model = APIFSOrderList.GetData();
            var modelItem = model.FirstOrDefault(it => it.Code == FSOrder.Code);
            if (modelItem != null)
            {
                this.UpdateModel(modelItem);
            }
            return RedirectToAction("FSOrder");
        }


        #endregion

        #region Delete Detail
        public ActionResult FSOrderDelete(decimal code)
        {
            var model = APIFSOrderList.GetData();
            var modelItem = model.FirstOrDefault(it => it.Code == code);
            if (modelItem != null)
            {
                APIFSOrderList.GetData().Remove(modelItem);
            };
            return RedirectToAction("FSOrder");
        }

        #endregion

        #region Grid Item
        [ValidateInput(false)]
        public ActionResult FSOrderItemPartial()
        {
            var model = new object[0];
            return PartialView("_FSOrderItemPartial", _FSOrderItem.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FSOrderItemPartialAddNew(MVC.Areas.Sell.Models.APIFSOrderItem item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    _FSOrderItem.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_FSOrderItemPartial", _FSOrderItem.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FSOrderItemPartialUpdate(MVC.Areas.Sell.Models.APIFSOrderItem item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    _FSOrderItem.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_FSOrderItemPartial", _FSOrderItem.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FSOrderItemPartialDelete(int Code)
        {
            var model = new object[0];
            if (Code >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _FSOrderItem.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_FSOrderItemPartial", _FSOrderItem.GetData());
        }
        #endregion\

        #region FSOrderPromotion
        [ValidateInput(false)]
        public ActionResult FSOrderPromotionPartial()
        {
            return PartialView("_FSOrderPromotionPartial", _FSOrderPro.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FSOrderPromotionPartialAddNew(APIFSOrderPromotion item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    _FSOrderPro.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_FSOrderPromotionPartial", _FSOrderPro.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FSOrderPromotionPartialUpdate(APIFSOrderPromotion item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    _FSOrderPro.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_FSOrderPromotionPartial", _FSOrderPro.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FSOrderPromotionPartialDelete(int Code)
        {
            var model = new object[0];
            if (Code >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _FSOrderPro.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_FSOrderPromotionPartial", _FSOrderPro.GetData());
        }
        #endregion
    }
}