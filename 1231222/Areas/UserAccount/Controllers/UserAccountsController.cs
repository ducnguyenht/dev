using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using WebMatrix.WebData;
using MVC.Filters;
namespace MVC.Areas.UserAccount.Controllers
{//dn3
    [Authorize]
    [InitializeSimpleMembership]
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
                        switch (item.Status)
                        {
                            case 2:
                                var membership = db.webpages_Memberships.Where(t => t.UserId == item.UserId).FirstOrDefault();
                                    membership.IsConfirmed = true;
                                    //WebSecurity.ConfirmAccount(username, confirmToken);
                                break;
                            case 3:
                                membership = db.webpages_Memberships.Where(t => t.UserId == item.UserId).FirstOrDefault();
                                    membership.IsConfirmed = false;
                                break;
                            default:
                                break;
                        }
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

        //MVC.LinQ.DBAuthorizationDataContext db1 = new MVC.LinQ.DBAuthorizationDataContext();

        //[ValidateInput(false)]
        //public ActionResult PartialGridViewUserInRole(int idUser)
        //{
        //    ViewData["UserAccountIdUser"] = idUser;
        //    var model = db1.webpages_UsersInRoles.Where(i=>i.UserId==idUser);
        //    return PartialView("_PartialGridViewUserInRole", model);
        //}

        //[HttpPost, ValidateInput(false)]
        //public ActionResult PartialGridViewUserInRoleAddNew(MVC.LinQ.webpages_UsersInRole item)
        //{
        //    var model = db1.webpages_UsersInRoles;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            model.InsertOnSubmit(item);
        //            db1.SubmitChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    return PartialView("_PartialGridViewUserInRole", model);
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult PartialGridViewUserInRoleUpdate(MVC.LinQ.webpages_UsersInRole item)
        //{
        //    var model = db1.webpages_UsersInRoles;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var modelItem = model.FirstOrDefault(it => it.UserId == item.UserId);
        //            if (modelItem != null)
        //            {
        //                this.UpdateModel(modelItem);
        //                db1.SubmitChanges();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    return PartialView("_PartialGridViewUserInRole", model);
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult PartialGridViewUserInRoleDelete(System.Int32 UserId)
        //{
        //    var model = db1.webpages_UsersInRoles;
        //    if (UserId >= 0)
        //    {
        //        try
        //        {
        //            var item = model.FirstOrDefault(it => it.UserId == UserId);
        //            if (item != null)
        //                model.DeleteOnSubmit(item);
        //            db1.SubmitChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    return PartialView("_PartialGridViewUserInRole", model);
        //}
    }
}