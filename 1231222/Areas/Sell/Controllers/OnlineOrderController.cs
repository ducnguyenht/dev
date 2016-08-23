using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Sell.Models;

namespace MVC.Areas.Sell.Controllers
{
    public class OnlineOrderController : Controller
    {
        //
        // GET: /Sell/OnlineOrder/
        public ActionResult SalesOnline()
        {
            return View();
        }

        #region SalesOnline
        [ValidateInput(false)]
        public ActionResult SalesOnlineGridViewPartial()
        {
            var model = APISalesOnlineList.GetData();
            return PartialView("_SalesOnlineGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineGridViewPartialAddNew(MVC.Areas.Sell.Models.APISalesOnline item)
        {
            var model = APISalesOnlineList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    APISalesOnlineList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_SalesOnlineGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineGridViewPartialUpdate(MVC.Areas.Sell.Models.APISalesOnline item)
        {
            var model = APISalesOnlineList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    APISalesOnlineList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_SalesOnlineGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineGridViewPartialDelete(System.String Code)
        {
            var model = APISalesOnlineList.GetData();
            if (Code != null)
            {
                try
                {
                    APISalesOnlineList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_SalesOnlineGridViewPartial", model);
        }





        #region New in Detail

        [HttpGet]
        public ActionResult SalesOnlineNew(string code)
        {
            APISalesOnline saleOnline = APISalesOnlineList.GetData().FirstOrDefault(U => U.Code == code);
            if (saleOnline == null)
            {
                saleOnline = new APISalesOnline();
                saleOnline.Code = code;
            }
            return View("SalesOnlineNew", saleOnline);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineNew(APISalesOnline saleOnline)
        {
            if (!ModelState.IsValid)
                return View("SalesOnlineNew", saleOnline);

            APISalesOnlineList.GetData().Add(saleOnline);
            return RedirectToAction("SalesOnline");
        }

        #endregion New in Detail

        #region Edit in Detail
        [HttpGet]
        public ActionResult SalesOnlineEdit(string code)
        {
            APISalesOnline salesOnline = APISalesOnlineList.GetData().FirstOrDefault(U => U.Code == code);
            return View("SalesOnlineEdit", salesOnline);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineEdit(APISalesOnline salesOnline)
        {
            if (!ModelState.IsValid)
                return View("SalesOnlineEdit", salesOnline);

            var model = APISalesOnlineList.GetData();
            var modelItem = model.FirstOrDefault(it => it.Code == salesOnline.Code);
            if (modelItem != null)
            {
                this.UpdateModel(modelItem);
            }
            return RedirectToAction("SalesOnline");
        }
        public ActionResult SalesOnlineDelete(string code)
        {
            var model = APISalesOnlineList.GetData();
            var modelItem = model.FirstOrDefault(it => it.Code == code);
            if (modelItem != null)
            {
                APISalesOnlineList.GetData().Remove(modelItem);
            };
            return RedirectToAction("SalesOnline");
        }
        #endregion Edit in Detail


        #region Item list
        [ValidateInput(false)]
        public ActionResult SalesOnlineItemsGridViewPartial()
        {
            var model = SalesOnlineItemsList.GetData();
            return PartialView("_SalesOnlineItemsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineItemsGridViewPartialAddNew(MVC.Areas.Sell.Models.APISalesOnlineItems item)
        {
            var model = SalesOnlineItemsList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    SalesOnlineItemsList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_SalesOnlineItemsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineItemsGridViewPartialUpdate(MVC.Areas.Sell.Models.APISalesOnlineItems item)
        {
            var model = SalesOnlineItemsList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    SalesOnlineItemsList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_SalesOnlineItemsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineItemsGridViewPartialDelete(System.Int32 ID)
        {
            var model = SalesOnlineItemsList.GetData();
            if (ID >= 0)
            {
                try
                {
                    SalesOnlineItemsList.DeleteItem(ID);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_SalesOnlineItemsGridViewPartial", model);
        }
        #endregion Item list


        #region Item Promotion

        [ValidateInput(false)]
        public ActionResult SalesOnlineItemsPromotionGridViewPartial()
        {
            var model = SalesOnlineItemsPromotionList.GetData();
            return PartialView("_SalesOnlineItemsPromotionGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineItemsPromotionGridViewPartialAddNew(MVC.Areas.Sell.Models.APISalesOnlineItemsPromotion item)
        {
            var model = SalesOnlineItemsPromotionList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    SalesOnlineItemsPromotionList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_SalesOnlineItemsPromotionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineItemsPromotionGridViewPartialUpdate(MVC.Areas.Sell.Models.APISalesOnlineItemsPromotion item)
        {
            var model = SalesOnlineItemsPromotionList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    SalesOnlineItemsPromotionList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_SalesOnlineItemsPromotionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SalesOnlineItemsPromotionGridViewPartialDelete(System.Int32 NumberOrder)
        {
            var model = SalesOnlineItemsPromotionList.GetData();
            if (NumberOrder >= 0)
            {
                try
                {
                    SalesOnlineItemsPromotionList.DeleteItem(NumberOrder);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_SalesOnlineItemsPromotionGridViewPartial", model);
        }

        #endregion Item Promotion
        #endregion SalesOnline


    }
}