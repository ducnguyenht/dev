using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Web.UI.WebControls;
using dnmvcschedule.Models;

namespace dnmvcschedule.Controllers {
    public class CalendarController : Controller {

        public ActionResult Index() {
            return View(new OutlookScheduler());
        }

        public ActionResult SchedulerPartial() {
            return PartialView("SchedulerPartial", new OutlookScheduler());
        }

        public ActionResult EditingPartialEditAppointment() {
            OutlookSchedulerHelper.UpdateModel();
            return PartialView("SchedulerPartial", new OutlookScheduler());
        }
    }

    public static class SchedulerHelper {
		// DXCOMMENT: Configure scheduler
        public static SchedulerSettings Settings {
            get {
                SchedulerSettings settings = new SchedulerSettings();
                settings.Name = "Scheduler";
				
				// DXCOMMENT: Configure DateNavigator for Scheduler
                settings.DateNavigatorExtensionSettings.Name = "DateNavigator";
                settings.DateNavigatorExtensionSettings.SchedulerName = "Scheduler";
                settings.DateNavigatorExtensionSettings.Properties.Rows = 2;
                settings.DateNavigatorExtensionSettings.Properties.DayNameFormat = DayNameFormat.FirstLetter;
                settings.DateNavigatorExtensionSettings.Properties.Style.Border.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(0);
                settings.DateNavigatorExtensionSettings.ControlStyle.CssClass = "datenavigator";

                settings.CallbackRouteValues = new { Controller = "Calendar", Action = "SchedulerPartial" };
                settings.EditAppointmentRouteValues = new { Controller = "Calendar", Action = "EditingPartialEditAppointment" };

                settings.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.WorkWeek;
                settings.Start = DateTime.Now;//new DateTime(2011, 4, 6);
                settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                settings.Views.DayView.VisibleTime.Start = new TimeSpan(8, 0, 0);
                settings.Views.DayView.VisibleTime.End = new TimeSpan(20, 0, 0);
                settings.Views.WorkWeekView.VisibleTime.Start = new TimeSpan(8, 0, 0);
                settings.Views.WorkWeekView.VisibleTime.End = new TimeSpan(20, 0, 0);
                settings.Views.WeekView.Enabled = false;
                settings.Views.MonthView.CompressWeekend = false;
                settings.Views.TimelineView.Enabled = false;
                settings.Storage.EnableReminders = false;
				
				// DXCOMMENT: Configure appointment mappings
                settings.Storage.Appointments.Mappings.AppointmentId = "ID";
                settings.Storage.Appointments.Mappings.Type = "EventType";
                settings.Storage.Appointments.Mappings.Start = "StartDate";
                settings.Storage.Appointments.Mappings.End = "EndDate";
                settings.Storage.Appointments.Mappings.AllDay = "AllDay";
                settings.Storage.Appointments.Mappings.Subject = "Subject";
                settings.Storage.Appointments.Mappings.Location = "Location";
                settings.Storage.Appointments.Mappings.Description = "Description";
                settings.Storage.Appointments.Mappings.Status = "Status";
                settings.Storage.Appointments.Mappings.Label = "Label";
                settings.Storage.Appointments.Mappings.ResourceId = "ResourceID";
                settings.Storage.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo";
				
				// DXCOMMENT: Configure resource mappings
                settings.Storage.Resources.Mappings.ResourceId = "ID";
                settings.Storage.Resources.Mappings.Caption = "Name";
                
				settings.OptionsBehavior.RecurrentAppointmentEditAction = DevExpress.XtraScheduler.RecurrentAppointmentAction.Ask;
                settings.ControlStyle.BorderLeft.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(0);

                return settings;
            }
        }
    }
}