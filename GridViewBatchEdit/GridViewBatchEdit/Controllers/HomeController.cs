using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Models;
using System.Globalization;
using System.ComponentModel;

namespace GridViewBatchEdit.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial() {
            return PartialView("_GridViewPartial", BatchEditRepository.GridData);
        }

        public ActionResult ChangeCulture(string lang, string returnUrl = "")
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BatchUpdatePartial(MVCxGridViewBatchUpdateValues<GridDataItem, int> batchValues) {
            if(ModelState.IsValid) {
                try {
                    foreach(var item in batchValues.Insert) {
                        if(batchValues.IsValid(item))
                            BatchEditRepository.InsertNewItem(item);
                    }
                    foreach(var item in batchValues.Update) {
                        if(batchValues.IsValid(item))
                            BatchEditRepository.UpdateItem(item);
                    }
                    foreach(var itemKey in batchValues.DeleteKeys) {
                        BatchEditRepository.DeleteItem(itemKey);
                    }
                } catch(Exception e) {
                    ViewData["EditError"] = e.Message;
                }
            } else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", BatchEditRepository.GridData);
        }
    }
}