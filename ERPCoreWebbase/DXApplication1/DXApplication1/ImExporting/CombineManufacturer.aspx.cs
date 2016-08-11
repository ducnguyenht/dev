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
    public partial class CombineManufacturer : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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

        private class dataManufacturer
        {
            public int SupplierId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string RowStatus { get; set; }
        }

        private class dataManufacturerCategory
        {
            public int SupplierId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string RowStatus { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList datamanufacturer = new ArrayList();
            datamanufacturer.Add(new dataManufacturer() { SupplierId = 1, Code = "OPV", Name = "Công ty OPV", Description = "Trụ Sở: 437/2 Lê Đức Thọ, P. 16, Q. Gò Vấp", RowStatus = "Sử Dụng" });
            datamanufacturer.Add(new dataManufacturer() { SupplierId = 2, Code = "IMEXPHARM", Name = "Công ty cổ phần Imexpharm", Description = "Lô A18/D7 Khu Đô Thị Mới Cầu Giấy", RowStatus = "Sử Dụng" });
            datamanufacturer.Add(new dataManufacturer() { SupplierId = 3, Code = "HAUGIANG", Name = "Công ty cổ phần Dược Hậu Giang", Description = "10 Tô Hiệu, P. Ngô Quyền", RowStatus = "Sử Dụng" });
            datamanufacturer.Add(new dataManufacturer() { SupplierId = 4, Code = "SANOFISYNTHELABO", Name = "Công ty cổ phần dược phẩm Sanofisynthelabo Việt Nam", Description = "66 Quốc Lộ 30", RowStatus = "Sử Dụng" });

            grdDataManufacturer.DataSource = datamanufacturer;
            grdDataManufacturer.DataBind();

            ArrayList datamanufacturercategory = new ArrayList();
            datamanufacturercategory.Add(new dataManufacturerCategory() { SupplierId = 1, Code = "NHOMNSX01", Name = "Nhóm nhà sản xuất 1", Description = "Nhóm 1", RowStatus = "Sử Dụng" });
            datamanufacturercategory.Add(new dataManufacturerCategory() { SupplierId = 2, Code = "NHOMNSX02", Name = "Nhóm nhà sản xuất 2", Description = "Nhóm 2", RowStatus = "Sử Dụng" });
            datamanufacturercategory.Add(new dataManufacturerCategory() { SupplierId = 3, Code = "NHOMNSX03", Name = "Nhóm nhà sản xuất 3", Description = "Nhóm 3", RowStatus = "Sử Dụng" });
            datamanufacturercategory.Add(new dataManufacturerCategory() { SupplierId = 4, Code = "NHOMNSX04", Name = "Nhóm nhà sản xuất 4", Description = "Nhóm 4", RowStatus = "Sử Dụng" });

            grdDataManufacturerGroup.DataSource = datamanufacturercategory;
            grdDataManufacturerGroup.DataBind();
        }

        //Manufacturer

        protected void grdDataManufacturer_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataManufacturer.CancelEdit();
            grdDataManufacturer.JSProperties.Add("cpEditManufacturer", "new");
        }

        protected void grdDataManufacturer_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
        }

        protected void grdDataManufacturer_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdDataManufacturer.CancelEdit();
            grdDataManufacturer.JSProperties.Add("cpEditManufacturer", "edit");
        }

        //Manufacturer

        //ManufacturerCategory
        protected void grdDataManufacturerGroup_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataManufacturerGroup.CancelEdit();
            grdDataManufacturerGroup.JSProperties.Add("cpEditManufacturerGroup", "new");

        }

        protected void grdDataManufacturerGroup_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
        }

        protected void grdDataManufacturerGroup_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdDataManufacturerGroup.CancelEdit();
            grdDataManufacturerGroup.JSProperties.Add("cpEditManufacturerGroup", "edit");
        }

        protected void cpHeaderManufacturerGroup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

     
    }
}