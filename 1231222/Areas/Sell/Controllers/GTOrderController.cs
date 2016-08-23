using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Sell.Models;

namespace MVC.Areas.Sell.Controllers
{
    public class GTOrderController : Controller
    {
        // GET: Sell/GTOrder
        APIGTOrderList _APISalesList = new APIGTOrderList();
        APIGTOrderItemList _APIGTOrderItemList = new APIGTOrderItemList();
        APIGTOrderPromotionList _APIGTOrderPromotionList = new APIGTOrderPromotionList();
        public ActionResult SalesGTOrder()
        {
            return View();
        }
     
        #region GTOrderPartial
   
        [ValidateInput(false)]
        public ActionResult GTOrderPartial()
        {
            var model = new object[0];
            return PartialView("_GTOrderPartial", _APISalesList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderPartialAddNew(MVC.Areas.Sell.Models.APIGTOrder item)
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
            return PartialView("_GTOrderPartial", _APISalesList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderPartialUpdate(MVC.Areas.Sell.Models.APIGTOrder item)
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
            return PartialView("_GTOrderPartial", _APISalesList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderPartialDelete(System.Int32 Code)
        {
            var model = new object[0];
            if (Code >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _APISalesList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GTOrderPartial", _APISalesList.GetData());
        }
        #endregion

        #region GTViewNew
        [HttpGet]
        public ActionResult GTViewNew(int code)
        {
            APIGTOrder GTOrder = _APISalesList.GetData().FirstOrDefault(U => U.Code == code);
            if (GTOrder == null)
            {
                GTOrder = new APIGTOrder();
                GTOrder.Code = code;
            }
            return View("GTViewNew", GTOrder);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GTViewNew(APIGTOrder GTOrder)
        {
            if (!ModelState.IsValid)
                return View("GTViewNew", GTOrder);

            _APISalesList.GetData().Add(GTOrder);
            return RedirectToAction("SalesGTOrder");
        }

        #endregion

        #region GTViewEdit
        [HttpGet]
        public ActionResult GTViewEdit(int code)
        {
            APIGTOrder GTOrder = _APISalesList.GetData().FirstOrDefault(U => U.Code == code);
            return View("GTViewEdit", GTOrder);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GTViewEdit(APIGTOrder GTOrder)
        {
            if (!ModelState.IsValid)
                return View("GTViewEdit", GTOrder);

            var model = _APISalesList.GetData();
            var modelItem = model.FirstOrDefault(it => it.Code == GTOrder.Code);
            if (modelItem != null)
            {
                this.UpdateModel(modelItem);
            }
            return RedirectToAction("SalesGTOrder");
        }

        #endregion

        #region GTViewDelete
        public ActionResult GTViewDelete(int code)
        {
            var model = _APISalesList.GetData();
            var modelItem = model.FirstOrDefault(it => it.Code == code);
            if (modelItem != null)
            {
                _APISalesList.GetData().Remove(modelItem);
            };
            return RedirectToAction("SalesGTOrder");
        }
        #endregion

        #region GTOrderItem
        [ValidateInput(false)]
        public ActionResult GTOrderItemPartial()
        {
            var model = new object[0];
            return PartialView("_GTOrderItemPartial", _APIGTOrderItemList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderItemPartialAddNew(MVC.Areas.Sell.Models.APIGTOrderItem item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    _APIGTOrderItemList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GTOrderItemPartial", _APIGTOrderItemList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderItemPartialUpdate(MVC.Areas.Sell.Models.APIGTOrderItem item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    _APIGTOrderItemList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GTOrderItemPartial", _APIGTOrderItemList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderItemPartialDelete(System.Int32 Code)
        {
            var model = new object[0];
            if (Code >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _APIGTOrderItemList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GTOrderItemPartial", _APIGTOrderItemList.GetData());
        }
        #endregion


        #region GTOrderPromotion


        [ValidateInput(false)]
        public ActionResult GTOrderPromotionPartial()
        {
            var model = new object[0];
            return PartialView("_GTOrderPromotionPartial", _APIGTOrderPromotionList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderPromotionPartialAddNew(MVC.Areas.Sell.Models.APIGTOrderPromotion item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    _APIGTOrderPromotionList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GTOrderPromotionPartial", _APIGTOrderPromotionList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderPromotionPartialUpdate(MVC.Areas.Sell.Models.APIGTOrderPromotion item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    _APIGTOrderPromotionList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GTOrderPromotionPartial", _APIGTOrderPromotionList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GTOrderPromotionPartialDelete(System.Int32 Code)
        {
            var model = new object[0];
            if (Code >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    _APIGTOrderPromotionList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GTOrderPromotionPartial", _APIGTOrderPromotionList.GetData());
        }
        #endregion




    }      
}
