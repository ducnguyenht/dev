using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
//using BLL.PurchasingBLO;

namespace WebModule
{
    public partial class WarehouseCategory : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_CATEGORY_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }
        //WarehouseBLO toolBLO = new WarehouseBLO();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //grdDataWarehouseCategory.DataSource = this.toolBLO.getWarehouseCategoryList();
            grdDataWarehouseCategory.DataBind();
        }
        protected void grdDataWarehouseCategory_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            switch (args[0])
            {
                case "delete":
                    try
                    {
                        if (args.Length > 1)
                        {
                            Guid recordId = Guid.Parse(args[1]);
                            //toolBLO.DeleteWarehouseCategory(recordId);
                            this.grdDataWarehouseCategory.JSProperties.Add("cpEvent", "deleted");
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
       
    }
}