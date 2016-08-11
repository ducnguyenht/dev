using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;

namespace WebModule.ImExporting
{
    public partial class CombineProduct : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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


        private class datasample
        {
            public int ProductId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string ManufacturerName { get; set; }
            public string RowStatus { get; set; }
        }

        private class dataProductgroup
        {
            public int SupplierId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string RowStatus { get; set; }
        }


        private class dataUnit
        {
            public int SupplierId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string RowStatus { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { ProductId = 1, Code = "TANATRIL", Name = "Tanatril", ManufacturerName = "Công ty OPV", RowStatus = "Sử Dụng" });
            data.Add(new datasample() { ProductId = 2, Code = "KLACID", Name = "Klacid", ManufacturerName = "Công ty cổ phần Imexpharm", RowStatus = "Sử Dụng" });
            data.Add(new datasample() { ProductId = 3, Code = "BETASERC", Name = "Betaserc", ManufacturerName = "Công ty cổ phần Dược Hậu Giang", RowStatus = "Sử Dụng" });
            data.Add(new datasample() { ProductId = 4, Code = "INDAPAMIDE", Name = "Indapamide ", ManufacturerName = "Công ty cổ phần Imexpharm", RowStatus = "Sử Dụng" });

            grdDataProduct.DataSource = data;
            grdDataProduct.DataBind();


            ArrayList dataproductgroup = new ArrayList();
            dataproductgroup.Add(new dataProductgroup() { SupplierId = 1, Code = "NHOM01", Name = "Nhóm Zidovudine", Description = "", RowStatus = "Sử Dụng" });
            dataproductgroup.Add(new dataProductgroup() { SupplierId = 2, Code = "NHOM02", Name = "Morphine", Description = "", RowStatus = "Sử Dụng" });
            dataproductgroup.Add(new dataProductgroup() { SupplierId = 3, Code = "NHOM03", Name = "Penicillin", Description = "", RowStatus = "Sử Dụng" });
            dataproductgroup.Add(new dataProductgroup() { SupplierId = 4, Code = "NHOM04", Name = "Nhóm Thuốc Ngủ", Description = "", RowStatus = "Sử Dụng" });

            grdDataProductGroup.DataSource = dataproductgroup;
            grdDataProductGroup.DataBind();


            ArrayList dataunit = new ArrayList();
            dataunit.Add(new dataUnit() { SupplierId = 5, Code = "VIEN", Name = "Viên", Description = "Đơn vị Viên", RowStatus = "Sử Dụng" });
            dataunit.Add(new dataUnit() { SupplierId = 6, Code = "HOP", Name = "Hộp", Description = "Đơn vị Hộp", RowStatus = "Sử Dụng" });
            dataunit.Add(new dataUnit() { SupplierId = 7, Code = "VI", Name = "Vỉ", Description = "Đơn vị Vỉ", RowStatus = "Sử Dụng" });
            dataunit.Add(new dataUnit() { SupplierId = 8, Code = "THUNG", Name = "Thùng", Description = "Đơn vị Thùng", RowStatus = "Sử Dụng" });

            grdDataUnit.DataSource = dataunit;
            grdDataUnit.DataBind();

        }

        protected void grdDataProduct_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataProduct.CancelEdit();
            grdDataProduct.JSProperties.Add("cpEditProduct", "new");
        }

        protected void grdDataProduct_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
        }

        protected void grdDataProduct_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdDataProduct.CancelEdit();
            grdDataProduct.JSProperties.Add("cpEditProduct", "edit");
        }

        protected void grdDataProductGroup_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataProductGroup.CancelEdit();
            grdDataProductGroup.JSProperties.Add("cpEditProductGroup", "new");
        }

        protected void grdDataProductGroup_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            grdDataProductGroup.CancelEdit();
            grdDataProductGroup.JSProperties.Add("cpEditProductGroup", "new");
        }

        protected void grdDataProductGroup_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdDataProductGroup.CancelEdit();
            grdDataProductGroup.JSProperties.Add("cpEditProductGroup", "edit");
        }

        protected void grdDataUnit_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataUnit.CancelEdit();
            grdDataUnit.JSProperties.Add("cpEditUnit", "new");
        }

        protected void grdDataUnit_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
        }

        protected void grdDataUnit_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdDataUnit.CancelEdit();
            grdDataUnit.JSProperties.Add("cpEditUnit", "edit");
        }
    }
}