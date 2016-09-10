using MVCLargeDB01.DAL;
using MVCLargeDB01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DevExpress.Web.Mvc;
using System.Data;
namespace MVCLargeDB01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to DevExpress Extensions for ASP.NET MVC!";
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IEnumerable<Premise> premises = myDbContext.Premises.ToList();
                ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
                return View(premises);
            }
        }

        public ActionResult GridViewPartial()
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IEnumerable<Premise> premises = myDbContext.Premises.ToList();
                ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
                return PartialView(premises);
            }
        }

        public ActionResult MCCComboBoxPartialForGridView(int id)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
                return PartialView("MCCComboBoxPartialForGridView", id);
            }
        }

        public ActionResult InlineEditingUpdatePartial([ModelBinder(typeof(DevExpressEditorsBinder))] Premise premise)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        myDbContext.Entry(premise).State = EntityState.Modified;
                        myDbContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";
                ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
                return PartialView("GridViewPartial", myDbContext.Premises.ToList());
            }

        }


        public ActionResult InlineEditingNewPartial([ModelBinder(typeof(DevExpressEditorsBinder))] Premise premise)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        myDbContext.Premises.Add(premise);
                        myDbContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                {
                    ViewData["EditError"] = "Please, correct all errors.";
                }
                ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
                return PartialView("GridViewPartial", myDbContext.Premises.ToList());
            }

        }

        public ActionResult InlineEditingDeletePartial(int premiseID)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                if (premiseID != 0)
                {
                    try
                    {
                        Premise premise = myDbContext.Premises.Find(premiseID);
                        myDbContext.Premises.Remove(premise);
                        myDbContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";

                ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
                return PartialView("GridViewPartial", myDbContext.Premises.ToList());
            }
        }


        public ActionResult Edit(int premiseID)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {

                Premise premise = myDbContext.Premises.Find(premiseID);
                //ViewBag.CountryID = myDbContext.Countries.Where(c => c.CountryID >= premise.CountryID).OrderBy(c => c.CountryID).ToList();  
                return View(premise);
            }
        }

        [HttpPost]
        public ActionResult Edit([ModelBinder(typeof(DevExpressEditorsBinder))] Premise premise)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                //ViewBag.CountryID = myDbContext.Countries.Where(c => c.CountryID >= premise.CountryID).OrderBy(c => c.CountryID).ToList();
                if (ModelState.IsValid)
                {
                    try
                    {
                        myDbContext.Entry(premise).State = EntityState.Modified;
                        myDbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DataException e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
                return View(premise);
            }
        }

        public ActionResult Create()
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
                Premise premise = new Premise { System_Code = "TT0001" };
                return View(premise);
            }
        }

        [HttpPost]
        public ActionResult Create(Premise premise)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                ViewBag.CountryID = myDbContext.Countries.OrderBy(c => c.CountryID).ToList();
                if (ModelState.IsValid)
                {
                    try
                    {
                        myDbContext.Premises.Add(premise);
                        myDbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DataException e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
                return View(premise);
            }
        }

    }
}