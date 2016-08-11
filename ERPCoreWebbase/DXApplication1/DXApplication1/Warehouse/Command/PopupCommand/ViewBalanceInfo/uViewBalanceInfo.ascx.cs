using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Warehouse.Command.PopupCommand.ViewBalanceInfo
{
    public partial class uViewBalanceInfo : System.Web.UI.UserControl
    {
        public Guid ItemUnitId
        {
            get 
            {
                if (Session["ItemUnitId" + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["ItemUnitId" + ViewStateControlId].ToString());
            }

            set 
            {
                Session["ItemUnitId" + ViewStateControlId] = value;
            }
        }

        public Guid InventoryId
        {
            get
            {
                if (Session["InventoryId" + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["InventoryId" + ViewStateControlId].ToString());
            }

            set
            {
                Session["InventoryId" + ViewStateControlId] = value;
            }
        }

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get { return (string)ViewState["ViewStateControlId"]; }
            set { ViewState["ViewStateControlId"] = value; }
        }
        
        protected void cpItemUnitBalanceDetail_OnCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}