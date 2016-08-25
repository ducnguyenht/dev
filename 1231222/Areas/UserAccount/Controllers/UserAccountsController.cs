using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace MVC.Areas.UserAccount.Controllers
{//dn3
    public class UserAccountsController : Controller
    {
        // GET: UserAccount/UserAccounts
        public ActionResult Index()
        {
            return View();
        }

        MVC.LinQ.DBAuthorizationDataContext db = new MVC.LinQ.DBAuthorizationDataContext();

        [ValidateInput(false)]
        public ActionResult UserProfile()
        {
            var model = db.UserProfiles;
            return PartialView("_UserProfile", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UserProfileAddNew(MVC.LinQ.UserProfile item)
        {
            var model = db.UserProfiles;
            if (ModelState.IsValid)
            {
                try
                {
                    model.InsertOnSubmit(item);
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_UserProfile", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserProfileUpdate(MVC.LinQ.UserProfile item)
        {
            var model = db.UserProfiles;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.UserId == item.UserId);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_UserProfile", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserProfileDelete(System.Int32 UserId)
        {
            var model = db.UserProfiles;
            if (UserId >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.UserId == UserId);
                    if (item != null)
                        model.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_UserProfile", model);
        }
    }
}