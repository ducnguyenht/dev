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
    public partial class CombineSupplier : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase 
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

        private class dataSupplier
        {
            public int SupplierId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string RowStatus { get; set; }
        }

        private class dataSupplierGroup
        {
            public int SupplierId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string RowStatus { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList datasupplier = new ArrayList();
            datasupplier.Add(new dataSupplier() { SupplierId = 1, Code = "MYCHAU", Name = "Nhà cung cấp Mỹ Châu", Description = "Trụ Sở: 437/2 Lê Đức Thọ, P. 16, Q. Gò Vấp", RowStatus = "Sử Dụng" });
            datasupplier.Add(new dataSupplier() { SupplierId = 2, Code = "KIENVIET", Name = "Nhà cung cấp Kiến Việt", Description = "Lô A18/D7 Khu Đô Thị Mới Cầu Giấy, P. Dịch Vọng, Q. Cầu Giấy, Hà Nội", RowStatus = "Sử Dụng" });
            datasupplier.Add(new dataSupplier() { SupplierId = 3, Code = "ICHNHAN", Name = "Nhà cung cấp Ích Nhân", Description = "10 Tô Hiệu, P. Ngô Quyền, H. Vĩnh Yên, Vĩnh Phúc", RowStatus = "Sử Dụng" });
            datasupplier.Add(new dataSupplier() { SupplierId = 4, Code = "VINHPHUC", Name = "Nhà cung cấp Vĩnh Phúc", Description = "66 Quốc Lộ 30, P. Phú Mỹ, Tp. Cao Lãnh, Đồng Tháp", RowStatus = "Sử Dụng" });

            grdDataSupplier.DataSource = datasupplier;
            grdDataSupplier.DataBind();


            ArrayList datasuppliergroup = new ArrayList();
            datasuppliergroup.Add(new dataSupplierGroup() { SupplierId = 1, Code = "NHOMNCC01", Name = "Nhóm nhà cung cấp 1", Description = "Nhóm 1", RowStatus = "Sử Dụng" });
            datasuppliergroup.Add(new dataSupplierGroup() { SupplierId = 2, Code = "NHOMNCC02", Name = "Nhóm nhà cung cấp 2", Description = "Nhóm 2", RowStatus = "Sử Dụng" });
            datasuppliergroup.Add(new dataSupplierGroup() { SupplierId = 3, Code = "NHOMNCC03", Name = "Nhóm nhà cung cấp 3", Description = "Nhóm 3", RowStatus = "Sử Dụng" });
            datasuppliergroup.Add(new dataSupplierGroup() { SupplierId = 4, Code = "NHOMNCC04", Name = "Nhóm nhà cung cấp 4", Description = "Nhóm 4", RowStatus = "Sử Dụng" });

            grdDataSupplierGroup.DataSource = datasuppliergroup;
            grdDataSupplierGroup.DataBind();

            /*
            if (!Page.IsPostBack || !Page.IsCallback)
            {
                BindGrid();
            }
            else
            {
                Session["dataXDS"] = SupplierXDS;
            } 
            */
        }
        //Supplier
        protected void grdDataSupplier_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdDataSupplier.CancelEdit();
            grdDataSupplier.JSProperties.Add("cpEditSupplier", "new");

            Session["supplierMode"] = null;
        }

        protected void grdDataSupplier_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            /*
            BindGrid();

            Guid guid = new Guid(e.EditingKeyValue.ToString());

            CriteriaOperator co = CriteriaOperator.Parse("[SupplierId] = ?", e.EditingKeyValue);

            ViewSupplier vs = SupplierXDS.Session.FindObject<ViewSupplier>(co);

            Session["supplierMode"] = vs;

           
            
             */
            e.Cancel = true;
            grdDataSupplier.CancelEdit();
            grdDataSupplier.JSProperties.Add("cpEditSupplier", "edit");
        }

        protected void grdDataSupplier_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            /*
            using (uow = new UnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists)))
            {
                Supplier s = uow.FindObject<Supplier>(new BinaryOperator("SupplierId", e.Values["SupplierId"]));
                if (s != null) 
                {
                    uow.Delete(s);
                    uow.CommitChanges();
                }
            }
            
            
            BindGrid();
            
             */
            e.Cancel = true;
        }

        protected void cpHeaderSupplier_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            /*
            switch (e.Parameter)
            {
                case "refresh":
                    BindGrid();     
                    
                    break;
                default:
                    break;
            }
            */
        }


        //Supplier

        //SupplierGroup
        protected void grdDataSupplierGroup_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            grdDataSupplierGroup.CancelEdit();
            grdDataSupplierGroup.JSProperties.Add("cpSupplierGroup", "");
        }

        protected void grdDataSupplierGroup_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdDataSupplierGroup_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

            e.Cancel = true;
            grdDataSupplierGroup.CancelEdit();
            grdDataSupplierGroup.JSProperties.Add("cpSupplierGroup", "");
        }

        protected void cpHeaderSupplierGroup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
        //SupplierGroup
    }
}