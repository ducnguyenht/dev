using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Produce.Config
{
    public partial class Phase : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_PHASE_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            grdataPhase.DataSource = new[] { new { PhaseID = "CD001", Phase = "Công Đoạn 1",PhaseRowStatus = "Đã ngưng Sử Dụng", PhaseTimeUnit = "Giờ", PhaseTime = "8 Giờ", Start = "20/10/2011",End = "20/4/2012",PhaseDescription = "Example 1" },
                                              new { PhaseID = "CD002", Phase = "Công Đoạn 2",PhaseRowStatus = "Đã ngưng Sử Dụng", PhaseTimeUnit = "Giờ", PhaseTime = "8 Giờ", Start = "10/4/2012",End = "20/8/2012", PhaseDescription = "Example 2" },
                                             new { PhaseID = "CD003", Phase = "Công Đoạn 3",PhaseRowStatus = "Sử Dụng", PhaseTimeUnit = "Giờ", PhaseTime = "8 Giờ", Start = "20/1/2013",End = "", PhaseDescription = "Example 3" },
 new { PhaseID = "CD004", Phase = "Công Đoạn 4",PhaseRowStatus = "Sử Dụng", PhaseTimeUnit = "Giờ", PhaseTime = "8 Giờ", Start = "28/10/2012",End = "", PhaseDescription = "Example 4" }
            };
            grdataPhase.DataBind();
        }

        protected void grdataPhase_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdataPhase.CancelEdit();
            grdataPhase.JSProperties.Add("cpEditPhase", "new");
        }
    }
}