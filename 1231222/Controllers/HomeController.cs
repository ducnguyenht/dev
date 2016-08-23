using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using DevExpress.Web.Mvc;

namespace MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			// DXCOMMENT: Pass a data model for GridView
            return View();//View(NorthwindDataProvider.GetCustomers());	
        }
		
        //public ActionResult GridViewPartialView() 
        //{
        //    // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
        //    return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        //}


        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            //var model = new object[0];
            return PartialView("_GridViewPartial", SampleModelList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(MVC.Models.SampleModel item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    SampleModelList.AddItem(item); 
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", SampleModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(MVC.Models.SampleModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    SampleModelList.UpdateItem(item);  
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", SampleModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 ID)
        {
           // var model = new object[0];
            if (ID != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    SampleModelList.DeleteItem(ID); 
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", SampleModelList.GetData());
        }
	}
}