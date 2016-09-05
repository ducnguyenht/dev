using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.Mvc;
using DevExpress.XtraScheduler;
using System.Collections;
using dnSchedulev01.Models;
using dnSchedulev01.EFCFFDB;

namespace dnSchedulev01.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(SchedulerDataHelper.DataObject);
        }

        public ActionResult SchedulerPartial()
        {
            return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject);
        }

        public ActionResult EditAppointment()
        {
            UpdateAppointment();
            return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject);
        }

        void UpdateAppointment()
        {
            ScheduleCalendar insertedAppt = SchedulerExtension.GetAppointmentToInsert<ScheduleCalendar>("scheduler", SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage);
            SchedulerDataHelper.InsertAppointment(insertedAppt);

            ViewData["EditableSchedule"] = insertedAppt;

            ScheduleCalendar[] updatedAppt = SchedulerExtension.GetAppointmentsToUpdate<ScheduleCalendar>("scheduler", SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage);
            foreach (var appt in updatedAppt)
            {
                SchedulerDataHelper.UpdateAppointment(appt);
            }

            ScheduleCalendar[] removedAppt = SchedulerExtension.GetAppointmentsToRemove<ScheduleCalendar>("scheduler", SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage);
            foreach (var appt in removedAppt)
            {
                SchedulerDataHelper.RemoveAppointment(appt);
            }
        }
    }
}