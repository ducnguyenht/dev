using MVC.Areas.Purchasing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace MVC.Areas.Purchasing.Controllers
{
    [Authorize]
    public class PurchasingController : Controller
    {
        // GET: Purchasing/Purchasing
        PurchasingList _purlist = new PurchasingList();
        public ActionResult Index()
        {
            return View();
        }

        #region Purchasing
      
        [ValidateInput(false)]
        public ActionResult PurchasingPartial()
        {
            var model = new object[0];
            return PartialView("_PurchasingPartial", PurchasingList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PurchasingPartialAddNew(MVC.Areas.Purchasing.Models.APIPurchasing item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model

                    PurchasingList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_PurchasingPartial", PurchasingList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PurchasingPartialUpdate(MVC.Areas.Purchasing.Models.APIPurchasing item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model

                    PurchasingList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_PurchasingPartial", PurchasingList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PurchasingPartialDelete(System.Int32 ID)
        {
            var model = new object[0];
            if (ID >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    PurchasingList.DeleteItem(ID);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_PurchasingPartial", PurchasingList.GetData());
        }

        #endregion

        #region PurchasingDetail
        [ValidateInput(false)]
        public ActionResult PurchasingDetailPartial()
        {
            var model = new object[0];
            return PartialView("_PurchasingDetailPartial", PurchasingDetailList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PurchasingDetailPartialAddNew(MVC.Areas.Purchasing.Models.APIPurchasingDetail item)
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
            return PartialView("_PurchasingDetailPartial", PurchasingDetailList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PurchasingDetailPartialUpdate(MVC.Areas.Purchasing.Models.APIPurchasingDetail item)
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
            return PartialView("_PurchasingDetailPartial", PurchasingDetailList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PurchasingDetailPartialDelete(System.Int32 ID)
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
            return PartialView("_PurchasingDetailPartial", PurchasingDetailList.GetData());
        }
        #endregion

        #region DetailPanelPartial
        [ValidateInput(false)]
        public ActionResult DetailPanelPartial()
        {
            var model = new object[0];
            return PartialView("_DetailPanelPartial", DetailPanelList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DetailPanelPartialAddNew(MVC.Areas.Purchasing.Models.APIDetailPanel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    DetailPanelList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_DetailPanelPartial", DetailPanelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DetailPanelPartialUpdate(MVC.Areas.Purchasing.Models.APIDetailPanel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    DetailPanelList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_DetailPanelPartial", DetailPanelList.GetData());
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
                    DetailPanelList.DeleteItem(ID);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_DetailPanelPartial", DetailPanelList.GetData());
        }

        #endregion

        #region PrinciplePanel
        [ValidateInput(false)]
        public ActionResult PrinciplePanelPartial()
        {
            var model = new object[0];
            return PartialView("_PrinciplePanelPartial", PrinciplePanelList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PrinciplePanelPartialAddNew(MVC.Areas.Purchasing.Models.APIPrinciplePanel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    PrinciplePanelList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_PrinciplePanelPartial", PrinciplePanelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PrinciplePanelPartialUpdate(MVC.Areas.Purchasing.Models.APIPrinciplePanel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    PrinciplePanelList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_PrinciplePanelPartial", PrinciplePanelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PrinciplePanelPartialDelete(System.Int32 ID)
        {
            var model = new object[0];
            if (ID >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    PrinciplePanelList.DeleteItem(ID);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_PrinciplePanelPartial", PrinciplePanelList.GetData());
        }
        #endregion

        #region PurchasingViewNew
        [HttpGet]
        public ActionResult PurchasingViewNew(int id)
        {
            APIPurchasing purchase = PurchasingList.GetData().FirstOrDefault(U => U.ID == id);
            if (purchase == null)
            {
                purchase = new APIPurchasing();
                purchase.ID = id;
            }
            return View("PurchasingViewNew", purchase);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PurchasingViewNew(APIPurchasing purchase)
        {
            if (!ModelState.IsValid)
                return View("PurchasingViewNew", purchase);

            PurchasingList.GetData().Add(purchase);
            return RedirectToAction("Index");
        }
        #endregion

        #region PurchaingViewEdit
        [HttpGet]
        public ActionResult PurchasingViewEdit(int id)
        {
            APIPurchasing purchase = PurchasingList.GetData().FirstOrDefault(U => U.ID == id);
            return View("PurchasingViewEdit", purchase);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PurchasingViewEdit(APIPurchasing purchase)
        {
            if (!ModelState.IsValid)
                return View("PurchasingViewEdit", purchase);

            var model = PurchasingList.GetData();
            var modelItem = model.FirstOrDefault(it => it.ID == purchase.ID);
            if (modelItem != null)
            {
                this.UpdateModel(modelItem);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region PurchasingViewDelete
        public ActionResult PurchasingViewDelete(int id)
        {
            var model = PurchasingList.GetData();
            var modelItem = model.FirstOrDefault(it => it.ID == id);
            if (modelItem != null)
            {
                PurchasingList.GetData().Remove(modelItem);
            };
            return RedirectToAction("Index");
        }
        #endregion

    }
}