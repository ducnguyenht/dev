using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.ImExporting
{
    public partial class CombineService : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_IMEXPORT_GROUPID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_IMEXPORT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }


        private void BindGrid()
        {

            grdDataService.DataSource =
               new[] { 
                     new { code = "SRV001", name = "Dịch vụ 1", rowstatus = "Kích hoạt",
                             description = "Dịch vụ 1"
                    },
                    new { code = "MAT002", name = "Dịch vụ 2", rowstatus = "Tạm ngưng",
                             description = "Dịch vụ 2"
                    },
                };
            grdDataService.DataBind();

            grdDataServiceCategory.DataSource =
             new[] { 
                    new { code = "SRV001", name = "Dịch vụ 1", rowstatus = "Kích hoạt",
                             description = "Dịch vụ 1"
                    },
                    new { code = "SRV002", name = "Dịch vụ 2", rowstatus = "Tạm ngưng",
                             description = "Dịch vụ 2"
                    },
                };
            grdDataServiceCategory.DataBind();

            //       Session["dataXDS"] = ProductXDS;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || !Page.IsCallback)
            {
                BindGrid();
            }
            else
            {
                //    Session["dataXDS"] = ProductXDS;
            }
        }
        //Service
        protected void grdDataService_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataService.CancelEdit();
            grdDataService.JSProperties.Add("cpEditService", "new");

            Session["productMode"] = null;
        }

        protected void grdDataService_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            BindGrid();

            //  Guid guid = new Guid(e.EditingKeyValue.ToString());


            //Session["productMode"] = vs;

            grdDataService.CancelEdit();
            grdDataService.JSProperties.Add("cpEditService", "edit");
        }

        protected void grdDataService_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            e.Cancel = true;
            BindGrid();
        }

        protected void cpHeaderService_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "refresh":
                    BindGrid();
                    break;
                default:
                    break;
            }
        }

        //Service

        //ServiceCategory
        protected void grdDataServiceCategory_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataServiceCategory.CancelEdit();
            grdDataServiceCategory.JSProperties.Add("cpEditServiceCategory", "new");

            Session["supplierMode"] = null;
        }

        protected void grdDataServiceCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdDataServiceCategory_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdDataServiceCategory.CancelEdit();
            grdDataServiceCategory.JSProperties.Add("cpEditServiceCategory", "edit");
        }

        protected void cpHeaderServiceCategory_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
    }
}