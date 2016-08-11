using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.ImExporting
{
    public partial class CombineMaterial : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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

        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {

            grdDataMaterial.DataSource =
               new[] { 
                     new { code = "MAT001", name = "Nguyên vật liệu 1", manuname="Nhà sản xuất 1", rowstatus = "Kích hoạt",
                             description = "Nguyên vật liệu 1"
                    },
                    new { code = "MAT002", name = "Nguyên vật liệu 2", manuname="Nhà sản xuất 2", rowstatus = "Tạm ngưng",
                             description = "Nguyên vật liệu 2"
                    },
                };
            grdDataMaterial.DataBind();

            grdDataMaterialCategory.DataSource =
               new[] { 
                    new { code = "MTR001", name = "Nguyên Vật Liệu 1", rowstatus = "Kích hoạt",
                             description = "Nguyên Vật Liệu 1"
                    },
                    new { code = "MTR001", name = "Nguyên Vật Liệu 2", rowstatus = "Tạm ngưng",
                             description = "Nguyên Vật Liệu 2"
                    },
                };
            grdDataMaterialCategory.DataBind();

            grdDataMaterialUnit.DataSource =
               new[] { 
                     new { code = "UNR001", name = "Đơn vị tính 1", rowstatus = "Kích hoạt",
                             description = "Đơn vị tính 1"
                    },
                    new { code = "UNR0012", name = "Đơn vị tính 2", rowstatus = "Tạm ngưng",
                             description = "Đơn vị tính 2"
                    },
                };
            grdDataMaterialUnit.DataBind();

        }

        protected void grdDataMaterial_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataMaterial.CancelEdit();
            grdDataMaterial.JSProperties.Add("cpEditMaterial", "new");

            Session["productMode"] = null;
        }

        protected void grdDataMaterial_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            BindGrid();

            Guid guid = new Guid(e.EditingKeyValue.ToString());


            //Session["productMode"] = vs;

            grdDataMaterial.CancelEdit();
            grdDataMaterial.JSProperties.Add("cpEditMaterial", "edit");
        }

        protected void grdDataMaterial_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            e.Cancel = true;
            BindGrid();
        }

        protected void cpHeaderMaterial_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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


        //MaterialCategory
        protected void grdDataMaterialCategory_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataMaterialCategory.CancelEdit();
            grdDataMaterialCategory.JSProperties.Add("cpEditMaterialCategory", "new");

            Session["supplierMode"] = null;
        }

        protected void grdDataMaterialCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdDataMaterialCategory_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdDataMaterialCategory.CancelEdit();
            grdDataMaterialCategory.JSProperties.Add("cpEditMaterialCategory", "edit");
        }

        protected void cpHeaderMaterialCategory_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

        //MaterialUnit


        protected void cpHeaderMaterialUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "refresh":
                    //BindGrid();

                    break;
                default:
                    break;
            }
        }
        protected void grdDataMaterialUnit_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataMaterialUnit.CancelEdit();
            grdDataMaterialUnit.JSProperties.Add("cpEditMaterialUnit", "new");

            Session["supplierMode"] = null;
        }

        protected void grdDataMaterialUnit_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdDataMaterialUnit.CancelEdit();
            grdDataMaterialUnit.JSProperties.Add("cpEditMaterialUnit", "edit");
        }

        protected void grdDataMaterialUnit_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }
    }
}