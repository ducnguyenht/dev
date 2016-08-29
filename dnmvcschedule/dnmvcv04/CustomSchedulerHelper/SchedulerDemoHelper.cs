using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.Mvc;
using System.Collections;
using DevExpress.XtraScheduler;
using System.Web.UI.WebControls;
public class SchedulerDemoHelper
{
    public const string ImageQueryKey = "DXImage";

    public static string GetCarImageRouteUrl()
    {
        return DevExpressHelper.GetUrl(new { Controller = "Customization", Action = "CarImage" });
    }
    //public static bool IsDemoWithEditableActions()
    //{
    //    var editingDemos = new ArrayList { "Editing", "Reminders", "CustomForms" };
    //    return editingDemos.Contains(Utils.CurrentDemo.Key);
    //}

    static MVCxAppointmentStorage defaultAppointmentStorage;
    public static MVCxAppointmentStorage DefaultAppointmentStorage
    {
        get
        {
            if (defaultAppointmentStorage == null)
                defaultAppointmentStorage = CreateDefaultAppointmentStorage();
            return defaultAppointmentStorage;
        }
    }

    static MVCxAppointmentStorage CreateDefaultAppointmentStorage()
    {
        MVCxAppointmentStorage appointmentStorage = new MVCxAppointmentStorage();
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
        return appointmentStorage;
    }

    static MVCxResourceStorage defaultResourceStorage;
    public static MVCxResourceStorage DefaultResourceStorage
    {
        get
        {
            if (defaultResourceStorage == null)
                defaultResourceStorage = CreateDefaultResourceStorage();
            return defaultResourceStorage;
        }
    }
    static MVCxResourceStorage CreateDefaultResourceStorage()
    {
        MVCxResourceStorage resourceStorage = new MVCxResourceStorage();
        resourceStorage.Mappings.ResourceId = "ID";
        resourceStorage.Mappings.Caption = "Model";
        return resourceStorage;
    }

    static MVCxAppointmentStorage customAppointmentStorage;
    public static MVCxAppointmentStorage CustomAppointmentStorage
    {
        get
        {
            if (customAppointmentStorage == null)
                customAppointmentStorage = CreateCustomAppointmentStorage();
            return customAppointmentStorage;
        }
    }
    static MVCxAppointmentStorage CreateCustomAppointmentStorage()
    {
        MVCxAppointmentStorage appointmentStorage = CreateDefaultAppointmentStorage();
        appointmentStorage.CustomFieldMappings.Add("Price", "Price");
        appointmentStorage.CustomFieldMappings.Add("ContactInfo", "ContactInfo");
        return appointmentStorage;
    }

    
    static SchedulerSettings dateNavigatorSchedulerSettings;
    public static SchedulerSettings DateNavigatorSchedulerSettings
    {
        get
        {
            if (dateNavigatorSchedulerSettings == null)
                dateNavigatorSchedulerSettings = CreateDateNavigatorSchedulerSettings();
            return dateNavigatorSchedulerSettings;
        }
    }
    static SchedulerSettings CreateDateNavigatorSchedulerSettings()
    {
        SchedulerSettings settings = new SchedulerSettings();
        settings.Name = "scheduler";
        settings.CallbackRouteValues = new { Controller = "CalendarFeatures", Action = "DateNavigatorPartial" };
        settings.Start = new DateTime(2010, 7, 13);
        settings.Width = Unit.Pixel(580);
        settings.Views.DayView.ResourcesPerPage = 2;
        settings.Views.DayView.Styles.ScrollAreaHeight = Unit.Pixel(400);
        settings.OptionsBehavior.ShowViewNavigator = false;
        settings.OptionsBehavior.ShowViewSelector = false;

        settings.Storage.EnableReminders = false;
        settings.Storage.Resources.Assign(SchedulerDemoHelper.DefaultResourceStorage);
        settings.Storage.Appointments.Assign(SchedulerDemoHelper.DefaultAppointmentStorage);
        settings.OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.None;
        settings.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.None;
        settings.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.None;

        settings.DateNavigatorExtensionSettings.Name = "dateNavigator";
        settings.DateNavigatorExtensionSettings.Width = 220;
        settings.DateNavigatorExtensionSettings.Properties.Rows = 2;
        settings.DateNavigatorExtensionSettings.Properties.DayNameFormat = DayNameFormat.FirstLetter;
        settings.DateNavigatorExtensionSettings.Properties.BoldAppointmentDates = true;
        return settings;
    }
}