using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dnmvcv04.Controllers
{
    public class CustomizationController : Controller
    {
        public ActionResult CustomForms()
        {
            return View("CustomForms", SchedulerDataHelper.CustomDataObject);
        }
        public ActionResult CustomFormsPartial()
        {
            return PartialView("CustomFormsPartial", SchedulerDataHelper.CustomDataObject);
        }
        public ActionResult CustomFormsPartialEditAppointmen()
        {
            try
            {
                UpdateAppointmentsDataObject();
            }
            catch (Exception e)
            {
                ViewBag.SchedulerErrorText = e.Message;
            }

            return PartialView("CustomFormsPartial", SchedulerDataHelper.CustomDataObject);
        }

        void UpdateAppointmentsDataObject()
        {
            InsertAppointment();
            UpdateAppointment();
            RemoveAppointments();
        }
        void InsertAppointment()
        {
            ValidationSchedule schedule = SchedulerDataHelper.GetAppointmentToInsert<ValidationSchedule>(
                SchedulerDataHelper.CustomDataObject, SchedulerDemoHelper.CustomAppointmentStorage);
            if (schedule != null && TryValidateModel(schedule))
                CarsDataProvider.InsertSchedule<ValidationSchedule>(schedule);
            else
                ViewData["EditableSchedule"] = schedule;
        }
        void UpdateAppointment()
        {
            if (!ModelState.IsValid)
                return;

            ValidationSchedule[] schedules = SchedulerDataHelper.GetAppointmentsToUpdate<ValidationSchedule>(
                SchedulerDataHelper.CustomDataObject, SchedulerDemoHelper.CustomAppointmentStorage);
            foreach (var schedule in schedules)
            {
                if (!TryValidateModel(schedule))
                {
                    ViewData["EditableSchedule"] = schedule;
                    break;
                }
                CarsDataProvider.UpdateSchedule<ValidationSchedule>(schedule);
            }
        }
        void RemoveAppointments()
        {
            if (!ModelState.IsValid)
                return;

            ValidationSchedule[] schedules = SchedulerDataHelper.GetAppointmentsToRemove<ValidationSchedule>(
                SchedulerDataHelper.CustomDataObject, SchedulerDemoHelper.CustomAppointmentStorage);
            foreach (var schedule in schedules)
            {
                CarsDataProvider.DeleteCarScheduling<ValidationSchedule>(schedule);
            }
        }
    }
}