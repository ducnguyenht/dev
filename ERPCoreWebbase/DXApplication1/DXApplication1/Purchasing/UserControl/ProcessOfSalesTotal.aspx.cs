using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Purchasing
{
    public partial class ProcessOfSalesTotal : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grdMonitorProcessOfSalesTotal.DataSource = new[] { 
                new{CustomerId = "KH0001", CustomerName = "Nguyễn Văn Ba", CooperativePrincipleId = "HTNT0001",
                    CurrentSalesTotal = "200.000.000", Status = "Có phạt", NumOfDaysAlarm = "20" },
                new{CustomerId = "KH0002", CustomerName = "Cao Ánh Trinh", CooperativePrincipleId = "HTNT0002",
                    CurrentSalesTotal = "300.000.000", Status = "Không phạt", NumOfDaysAlarm = "20" },
                new{CustomerId = "KH0003", CustomerName = "Vũ Văn Hiếu", CooperativePrincipleId = "HTNT0003",
                    CurrentSalesTotal = "300.000.000", Status = "Không phạt", NumOfDaysAlarm = "20" }
            };

            grdMonitorProcessOfSalesTotal.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_PURCHASE_PROCESSOFSALESTOTAL_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_PURCHASE_GROUPID; }
        }
    }
}