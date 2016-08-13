using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using GridViewBatchEdit.Models;

namespace GridViewBatchEdit.Controllers
{
    public class TestGridController : Controller
    {
        //
        // GET: /TestGrid/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            var model = new object[0];
            return PartialView("_GridView1Partial", SampleModelList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(GridViewBatchEdit.Models.SampleModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    SampleModelList.AddItem(item); 
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView1Partial", SampleModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate(GridViewBatchEdit.Models.SampleModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    SampleModelList.UpdateItem(item);  
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView1Partial", SampleModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int32 ID)
        {
            var model = new object[0];
            if (ID >= 0)
            {
                try
                {
                    SampleModelList.DeleteItem(ID); 
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView1Partial", SampleModelList.GetData());
        }
    }
}
