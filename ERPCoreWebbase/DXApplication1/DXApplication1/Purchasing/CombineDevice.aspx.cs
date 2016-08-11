using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;


namespace WebModule.Purchasing
{
    public partial class CombineDevice : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        //ToolBLO toolBLO = new ToolBLO();
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_DEVICE_ID;
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

        private void BindGrid()
        {
            //grdDataDevice.DataSource = this.toolBLO.getToolList();
            //grdDataDevice.DataBind();

            //grdDataDeviceCategory.DataSource = this.toolBLO.getBuyingToolCategoryList();
            //grdDataDeviceCategory.DataBind();

            //this.grdDataDeviceUnit.DataSource = this.toolBLO.getToolUnitList();
            //grdDataDeviceUnit.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack || !Page.IsCallback)
            //{
            BindGrid();
            //}
            //else
            //{

            //}
            
        }


        protected void grdDataDeviceUnit_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
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
                            //toolBLO.DeleteDeviceUnit(recordId);
                            this.grdDataDeviceUnit.JSProperties.Add("cpEvent", "deleted");
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

        protected void grdDataDeviceCategory_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
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
                            //toolBLO.DeleteDeviceCategory(recordId);
                            this.grdDataDeviceCategory.JSProperties.Add("cpEvent", "deleted");
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

        protected void grdDataDevice_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
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
                            //toolBLO.DeleteDevice(recordId);
                            grdDataDevice.JSProperties.Add("cpEvent", "deleted");
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

      
        protected void cpHeaderDevice_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //switch (e.Parameter)
            //{
            //    case "refresh":
            //        BindGrid();
            //        break;
            //    default:
            //        break;
            //}
        }


        //Device
    }
}