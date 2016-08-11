using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
//using BLL.PurchasingBLO;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Purchasing
{
    public partial class ActiveElement : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        //ActiveElementBLO activeElementBLO = new ActiveElementBLO();
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_ACTIVEELEMENT;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            //ActiveElementBLO activeElementBLO = new ActiveElementBLO();
            //this.grdDataActiveElement.DataSource = activeElementBLO.getActiveElementList();
            //this.grdDataActiveElement.DataBind();
        }

        protected void grdDataManufacturerGroup_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            switch (args[0])
            {
                case "delete":
                    try
                    {
                        if (args.Length > 1)
                        {
                            //Guid recordId = Guid.Parse(args[1]);
                            //activeElementBLO.DeleteLogical(recordId);
                            //grdDataActiveElement.JSProperties.Add("cpEvent", "deleted");
                        }
                        else
                        {
                            throw new Exception("Must be pass id of the record");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                default:
                    break;
            }
        }


        protected void grdDataManufacturerGroup_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }

        protected void grdDataManufacturerGroup_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

        }

        protected void grdDataManufacturerGroup_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdDataActiveElement_DataBinding(object sender, EventArgs e)
        {
            //(sender as ASPxGridView).ForceDataRowType(typeof(vwCustomerCustomerDetailCustomerProperty));
        }
    }
}