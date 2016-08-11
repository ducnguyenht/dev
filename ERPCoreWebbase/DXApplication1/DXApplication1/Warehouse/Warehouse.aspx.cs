using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Inventory;
//using BLL.PurchasingBLO;

namespace WebModule.Warehouse
{
    public partial class Warehouse : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_WAREHOUSE_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        //WarehouseBLO toolBLO = new WarehouseBLO();
        private void BindGrid()
        {

            //grdDataWarehouse.DataSource = this.toolBLO.getWarehouseList();
            grdDataWarehouse.DataBind();
        }
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            this.dsInventory.Session = session;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void grdDataWarehouse_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
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
                            Inventory editManufacturerOrg =
                                session.GetObjectByKey<Inventory>(recordId);
                            editManufacturerOrg.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                            editManufacturerOrg.Save();
                            grdDataWarehouse.JSProperties.Add("cpEvent", "deleted");
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




        protected void grdDataWarehouse_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("RowStatus"))
            {
                if (e.Value.Equals(Constant.ROWSTATUS_ACTIVE))
                {
                    e.DisplayText = "Hoạt động";
                }
                else if (e.Value.Equals(Constant.ROWSTATUS_INACTIVE))
                {
                    e.DisplayText = "Ngừng hoạt động";
                }
            }
        }
    }
}