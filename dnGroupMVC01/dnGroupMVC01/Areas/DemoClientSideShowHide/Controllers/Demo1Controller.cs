﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using dnGroupMVC01.Areas.DemoClientSideShowHide.Models;

namespace dnGroupMVC01.Areas.DemoClientSideShowHide.Controllers
{
    public class Demo1Controller : Controller
    {
        // GET: DemoClientSideShowHide/Demo1
        public ActionResult Index()
        {
            ViewData["CategoryList"] = MasterList.GetData();
            ViewData["ProductList"] = DetailList.GetData();
            return View();
        }
        public ActionResult MasterDetailPopup()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult MasterGridViewPartial()
        {
            var model = MasterList.GetData();
            return PartialView("_MasterGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MasterGridViewPartialAddNew(dnGroupMVC01.Areas.DemoClientSideShowHide.Models.Master item)
        {
            var model = MasterList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    MasterList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MasterGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MasterGridViewPartialUpdate(dnGroupMVC01.Areas.DemoClientSideShowHide.Models.Master item)
        {
            var model = MasterList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    MasterList.UpdateItem(item);
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MasterGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MasterGridViewPartialDelete(System.Guid Id_Master)
        {
            var model = MasterList.GetData();
            if (Id_Master != null)
            {
                try
                {
                    MasterList.DeleteItem(Id_Master);
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_MasterGridViewPartial", model);
        }

        [ValidateInput(false)]
        //public ActionResult DetailPartial()
        //{
        //    var model = DetailList.GetData();
        //public ActionResult DetailPartial(string Id_Master)
        //var model = DetailList.GetDataByIdRef(new Guid(Id_Master));
        public ActionResult DetailPartial(Guid Id_Master)
        {
            var model = DetailList.GetDataByIdRef(Id_Master);
            
            
            return PartialView("_DetailPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DetailPartialAddNew(dnGroupMVC01.Areas.DemoClientSideShowHide.Models.Detail item)
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
            return PartialView("_DetailPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DetailPartialUpdate(dnGroupMVC01.Areas.DemoClientSideShowHide.Models.Detail item)
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
            return PartialView("_DetailPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DetailPartialDelete(System.Guid Id_Detail)
        {
            var model = new object[0];
            if (Id_Detail != null)
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
            return PartialView("_DetailPartial", model);
        }

      
    }
}