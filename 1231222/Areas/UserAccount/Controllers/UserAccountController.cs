using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.UserAccount.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: UserAccount/UserAccount
        public ActionResult Index()
        {
            return View();
        }
    }
}