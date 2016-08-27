using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace dnmvcschedulev02.Controllers
{
    public class UserSchedulerController : Controller
    {
        // GET: UserScheduler
        public ActionResult Index()
        {
            return View();
        }

        dnmvcschedulev02.CarsEFCFDB.CarsDBContext db = new dnmvcschedulev02.CarsEFCFDB.CarsDBContext();
        //dnmvcschedulev02.CarsEFCFDB.CarsDBContext resourceContext = new dnmvcschedulev02.CarsEFCFDB.CarsDBContext();

        public ActionResult SchedulerPartial()
        {
            System.Collections.IEnumerable AppointmentBinding = db.CarSchedulings.ToList();
            System.Collections.IEnumerable ResourceBinding = db.Cars.ToList();

            ViewData["Appointments"] = AppointmentBinding;
            ViewData["Resources"] = ResourceBinding;

            return PartialView("_SchedulerPartial");
        }
        public ActionResult SchedulerPartialEditAppointment()
        {
            var AppointmentBinding = db.CarSchedulings.ToList();
            var ResourceBinding = db.Cars.ToList();

            try
            {
                UserSchedulerControllerSchedulerSettings.UpdateEditableDataObject(db);
            }
            catch (Exception e)
            {
                ViewData["SchedulerErrorText"] = e.Message;
            }

            ViewData["Appointments"] = AppointmentBinding;
            ViewData["Resources"] = ResourceBinding;

            return PartialView("_SchedulerPartial");
        }
    }
    public class UserSchedulerControllerSchedulerSettings
    {
        static DevExpress.Web.Mvc.MVCxAppointmentStorage appointmentStorage;
        public static DevExpress.Web.Mvc.MVCxAppointmentStorage AppointmentStorage
        {
            get
            {
                if (appointmentStorage == null)
                {
                    appointmentStorage = new DevExpress.Web.Mvc.MVCxAppointmentStorage();
                    appointmentStorage.Mappings.AppointmentId = "ID";
                    appointmentStorage.Mappings.Start = "StartTime";
                    appointmentStorage.Mappings.End = "EndTime";
                    appointmentStorage.Mappings.Subject = "Subject";
                    appointmentStorage.Mappings.Description = "Description";
                    appointmentStorage.Mappings.Location = "Location";
                    appointmentStorage.Mappings.AllDay = "AllDay";
                    appointmentStorage.Mappings.Type = "EventType";
                    appointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
                    appointmentStorage.Mappings.ReminderInfo = "ReminderInfo";
                    appointmentStorage.Mappings.Label = "Label";
                    appointmentStorage.Mappings.Status = "Status";
                    appointmentStorage.Mappings.ResourceId = "CarId";
                }
                return appointmentStorage;
            }
        }

        static DevExpress.Web.Mvc.MVCxResourceStorage resourceStorage;
        public static DevExpress.Web.Mvc.MVCxResourceStorage ResourceStorage
        {
            get
            {
                if (resourceStorage == null)
                {
                    resourceStorage = new DevExpress.Web.Mvc.MVCxResourceStorage();
                    resourceStorage.Mappings.ResourceId = "ID";
                    resourceStorage.Mappings.Caption = "Model";
                }
                return resourceStorage;
            }
        }

        public static void UpdateEditableDataObject(dnmvcschedulev02.CarsEFCFDB.CarsDBContext db)
        {
            InsertAppointment(db);
            UpdateAppointments(db);
            DeleteAppointments(db);
        }

        static void InsertAppointment(dnmvcschedulev02.CarsEFCFDB.CarsDBContext db)
        {
            var appointments = db.CarSchedulings.ToList();
            var resources = db.Cars.ToList();

            var newAppointment = DevExpress.Web.Mvc.SchedulerExtension.GetAppointmentToInsert<dnmvcschedulev02.CarsEFCFDB.CarScheduling>("Scheduler", appointments, resources,
                AppointmentStorage, ResourceStorage);
            if (newAppointment == null)
                return;
            db.CarSchedulings.Add(newAppointment);
            db.SaveChanges();
         //   appointments.Add(newAppointment);
         //var t =   appointmentContext.SaveChanges();
        }
        static void UpdateAppointments(dnmvcschedulev02.CarsEFCFDB.CarsDBContext db)
        {
            var appointments = db.CarSchedulings.ToList();
            var resources = db.Cars.ToList();

            var updAppointments = DevExpress.Web.Mvc.SchedulerExtension.GetAppointmentsToUpdate<dnmvcschedulev02.CarsEFCFDB.CarScheduling>("Scheduler", appointments, resources,
                AppointmentStorage, ResourceStorage);
            var id_up=updAppointments[0].ID;
            var objCarsScheduler = db.CarSchedulings.Where(t => t.ID == id_up).FirstOrDefault();
            if (objCarsScheduler!=null)
            {
                //db.CarSchedulings.Attach(updAppointments[0]);
                db.Entry(objCarsScheduler).CurrentValues.SetValues(updAppointments[0]);   
                //db.Entry(updAppointments).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            
            //foreach (var appointment in updAppointments)
            //{
            //}
            
        }

        static void DeleteAppointments(dnmvcschedulev02.CarsEFCFDB.CarsDBContext db)
        {
            var appointments = db.CarSchedulings.ToList();
            var resources = db.Cars.ToList();

            var delAppointments = DevExpress.Web.Mvc.SchedulerExtension.GetAppointmentsToRemove<dnmvcschedulev02.CarsEFCFDB.CarScheduling>("Scheduler", appointments, resources,
                AppointmentStorage, ResourceStorage);
            foreach (var appointment in delAppointments)
            {
                var delAppointment = appointments.FirstOrDefault(a => a.ID == appointment.ID);
                if (delAppointment != null)
                    appointments.Remove(appointment);
            }
            db.SaveChanges();
        }
    }

}