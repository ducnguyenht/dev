using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.IO;
using DevExpress.XtraReports.Web;

namespace WebModule.Accounting.Report
{
    public partial class TaxReport : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_DIARYGENERAL_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxComboBox1.DataSource = new[] { "Tháng 1", "Tháng 2", "Quý 1", "6 tháng đầu năm" };
            ASPxComboBox1.DataBind();
            gvData.DataSource = ReportMappingConstant.getTaxReportDS();
            gvData.DataBind();
            if (this.hf.Value != string.Empty)
            {
            
            }
            

        }

        protected void gvData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            gvData.CancelEdit();
        }

   

       
      

      
    }
}