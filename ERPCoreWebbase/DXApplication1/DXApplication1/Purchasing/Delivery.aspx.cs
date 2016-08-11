using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using Utility;
using System.Drawing;

namespace ERPCore.Purchasing
{
    public partial class delivery : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        
        ASPxSchedulerStorage Storage { get { return this.ASPxScheduler1.Storage; } }
        public static Random RandomInstance = new Random();

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_PRODUCT_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private class datasample
        {
            public string Code { get; set; }                      
            public string Date { get; set; }
            public string Department { get; set; }
            public string Supplier { get; set; }
            public string Amount { get; set; }   
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { Code = "PO001", Date = "19/07/2013", Department = "Phòng kế hoạch vật tư", Supplier = "Nhà cung cấp Mỹ Châu", Amount="100000000" });
            data.Add(new datasample() { Code = "PO002", Date = "29/07/2013", Department = "Phòng Marketing", Supplier = "Nhà cung cấp Vĩnh Phú", Amount = "50000000" });
            data.Add(new datasample() { Code = "PO003", Date = "19/08/2013", Department = "Phòng Giám Đốc", Supplier = "Nhà cung cấp Vĩnh Phú", Amount = "800000000" });
            data.Add(new datasample() { Code = "PO004", Date = "19/07/2013", Department = "Phòng kế hoạch vật tư", Supplier = "Nhà cung cấp Mỹ Châu", Amount = "400000" });
            data.Add(new datasample() { Code = "PO005", Date = "19/07/2013", Department = "Phòng kế hoạch vật tư", Supplier = "Nhà cung cấp Mỹ Châu", Amount = "1000000" });
            data.Add(new datasample() { Code = "PO006", Date = "29/07/2013", Department = "Phòng Marketing", Supplier = "Nhà cung cấp Vĩnh Phú", Amount = "50000" });
            data.Add(new datasample() { Code = "PO007", Date = "19/08/2013", Department = "Phòng Giám Đốc", Supplier = "Nhà cung cấp Vĩnh Phú", Amount = "18800000000" });
            data.Add(new datasample() { Code = "PO008", Date = "19/07/2013", Department = "Phòng kế hoạch vật tư", Supplier = "Nhà cung cấp Mỹ Châu", Amount = "2000" });

            grdData.DataSource = data;
            grdData.DataBind();


            ASPxAppointmentMappingInfo mappings = Storage.Appointments.Mappings;
            Storage.BeginUpdate();
            try
            {
                mappings.AppointmentId = "Id";
                mappings.Start = "StartTime";
                mappings.End = "EndTime";
                mappings.Subject = "Subject";
                mappings.AllDay = "AllDay";
                mappings.Description = "Description";
                mappings.Label = "Label";
                mappings.Location = "Location";
                mappings.RecurrenceInfo = "RecurrenceInfo";
                mappings.ReminderInfo = "ReminderInfo";
                mappings.ResourceId = "OwnerId";
                mappings.Status = "Status";
                mappings.Type = "EventType";
            }
            finally
            {
                Storage.EndUpdate();
            }

            string[] Users = new string[] { "Sarah Brighton", "Ryan Fischer", "Andrew Miller" };
            string[] Usernames = new string[] { "sbrighton", "rfischer", "amiller" };

            ResourceCollection resources = Storage.Resources.Items;
            Storage.BeginUpdate();
            try
            {
                int cnt = Math.Min(3, Users.Length);
                for (int i = 1; i <= cnt; i++)
                {
                    resources.Add(new Resource(Usernames[i - 1], Users[i - 1]));
                }
            }
            finally
            {
                Storage.EndUpdate();
            }


            //ASPxScheduler1.AppointmentDataSource = ObjectDataSource1;
            //ASPxScheduler1.DataBind();
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {            
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
        }

        protected void ObjectDataSource1_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = new Utility.CustomEventDataSource(GetCustomEvents());

        }

        CustomEventList GetCustomEvents()
        {
            CustomEventList events = Session["ListBoundModeObjects"] as CustomEventList;
            if (events == null)
            {
                events = GenerateCustomEventList();
                Session["ListBoundModeObjects"] = events;
            }
            return events;
        }

        CustomEventList GenerateCustomEventList()
        {
            CustomEventList eventList = new CustomEventList();
            int count = Storage.Resources.Count;
            for (int i = 0; i < count; i++)
            {
                Resource resource = Storage.Resources[i];
                string subjPrefix = resource.Caption + "'s ";

                eventList.Add(CreateEvent(resource.Id, subjPrefix + "meeting", 2, 5));
                eventList.Add(CreateEvent(resource.Id, subjPrefix + "travel", 3, 6));
                eventList.Add(CreateEvent(resource.Id, subjPrefix + "phone call", 0, 10));
            }
            return eventList;
        }

        CustomEvent CreateEvent(object resourceId, string subject, int status, int label)
        {
            CustomEvent customEvent = new CustomEvent();
            customEvent.Subject = subject;
            customEvent.OwnerId = resourceId;
            Random rnd = RandomInstance;
            int rangeInHours = 48;
            customEvent.StartTime = DateTime.Today + TimeSpan.FromHours(rnd.Next(0, rangeInHours));
            customEvent.EndTime = customEvent.StartTime + TimeSpan.FromHours(rnd.Next(0, rangeInHours / 8));
            customEvent.Status = status;
            customEvent.Label = label;
            customEvent.Id = "ev" + customEvent.GetHashCode();
            return customEvent;
        }


        protected void ASPxScheduler1_PrepareAppointmentFormPopupContainer(object sender, ASPxSchedulerPrepareFormPopupContainerEventArgs e)
        {
            e.Popup.HeaderText = "ASPxScheduler1_PrepareAppointmentFormPopupContainer";
            e.Popup.Width = Unit.Pixel(800);
            e.Popup.BackColor = Color.Wheat;
            e.Popup.ContentStyle.Paddings.Padding = Unit.Pixel(0);
        }


    }
}