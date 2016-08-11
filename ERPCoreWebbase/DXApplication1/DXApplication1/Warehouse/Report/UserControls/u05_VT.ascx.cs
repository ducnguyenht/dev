using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxGridView;
using System.Data;
using NAS.DAL.Inventory.Audit;
using NAS.DAL.Inventory.Command;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Nomenclature.Inventory;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Warehouse.Report.UserControls
{
    public partial class u05_VT : System.Web.UI.UserControl
    {
        #region the first

        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            try { load_data(); }
            catch { }
        }

        protected void Page_Load(object sender, EventArgs e)
        { }
        #endregion

        
        public void load_data()
        {
            if (!hfReportAudit.Contains("id"))
            {
                return;
            }

            #region header và dòng đầu gridview
            header_column_GridView();
            DataTable data_table = table_template();
            #endregion

            // truyền tham số mã kho và mã phiếu kiểm kê
            string code_repository, code_inventory;
            
            InventoryAuditArtifact a = session.GetObjectByKey<InventoryAuditArtifact>(Guid.Parse(hfReportAudit.Get("id").ToString()));

            code_repository = a.InventoryId.Code;     //"KHO_TNTX";
            code_inventory = a.Code; //"BB01";

            #region xử lý data
            int STT = 1;
            XPView list_ItemUnitId = new XPView(session, typeof(InventoryAuditItemUnit));
            list_ItemUnitId.AddProperty("itemUnit", "ItemUnitId", true);

            double sum_thanh_tien_kt = 0, sum_thanh_tien_kk = 0, sum_thanh_tien_thua = 0, sum_thanh_tien_thieu = 0;
            foreach (ViewRecord each_ItemUnitId in list_ItemUnitId)
            {
                XPCollection<InventoryAuditItemUnit> iaiu = new XPCollection<InventoryAuditItemUnit>(session,CriteriaOperator.Parse("ItemUnitId = '"+each_ItemUnitId["itemUnit"].ToString()+"'"));
                foreach (InventoryAuditItemUnit each_iaiu in iaiu)
                {
                    try
                    {
                        if (each_iaiu.InventoryAuditArtifactId.InventoryId.Code == code_repository && each_iaiu.InventoryAuditArtifactId.RowStatus >= 1 && each_iaiu.InventoryAuditArtifactId.Code == code_inventory) //xét kho 
                        {
                            XPCollection<QualityItem> qi = each_iaiu.QualityItems;
                            double kem = 0, mat = 0;
                            foreach (QualityItem each_qi in qi)
                            {
                                if (each_qi.QualityItemType.Name == "LOSS_QUANLITY")
                                {
                                    mat += each_qi.AuditAmount;
                                }
                                else if (each_qi.QualityItemType.Name == "LESS_QUANLITY")
                                {
                                    kem += each_qi.AuditAmount;
                                }
                            }

                            double so_luong_kt = each_iaiu.BookingAmount;
                            double so_luong_kk = each_iaiu.RealAmount;
                            XPCollection<COGS> cogs = new XPCollection<COGS>(session, CriteriaOperator.Parse("InventoryId='" + each_iaiu.InventoryAuditArtifactId.InventoryId.InventoryId + "' AND ItemUnitId='" + each_iaiu.ItemUnitId.ItemUnitId + "'"));
                            double money;
                            try { money = cogs[0].COGSPrice; }
                            catch { money = 0; }

                            sum_thanh_tien_kk += money * so_luong_kk;
                            sum_thanh_tien_kt += money * so_luong_kt;

                            DataRow dr = data_table.NewRow();
                            dr["stt"] = STT++;
                            dr["ten"] = each_iaiu.ItemUnitId.ItemId.Name;
                            dr["ma_so"] = each_iaiu.ItemUnitId.ItemId.Code;
                            dr["don_vi_tinh"] = each_iaiu.ItemUnitId.UnitId.Name;
                            dr["don_gia"] = money;
                            dr["so_luong_kt"] = so_luong_kt;
                            dr["thanh_tien_kt"] = money * so_luong_kt;
                            dr["so_luong_kk"] = so_luong_kk;
                            dr["thanh_tien_kk"] = money * so_luong_kk;
                            if (so_luong_kk > so_luong_kt)
                            {
                                dr["so_luong_thua"] = so_luong_kk - so_luong_kt;
                                dr["thanh_tien_thua"] = money * (so_luong_kk - so_luong_kt);
                                sum_thanh_tien_thua += money * (so_luong_kk - so_luong_kt);
                            }
                            else if (so_luong_kk < so_luong_kt)
                            {
                                dr["so_luong_thieu"] = so_luong_kt - so_luong_kk;
                                dr["thanh_tien_thieu"] = money * (so_luong_kt - so_luong_kk);
                                sum_thanh_tien_thieu += money * (so_luong_kt - so_luong_kk);
                            }
                            dr["tot"] = so_luong_kk - kem - mat;
                            dr["kem"] = kem;
                            dr["mat"] = mat;
                            data_table.Rows.Add(dr);
                        }
                    }
                    catch { }
                }
            }
            DataRow dr_sum = data_table.NewRow();
            dr_sum["ten"] = "Cộng  ";
            dr_sum["thanh_tien_kt"] = sum_thanh_tien_kt + "  ";
            dr_sum["thanh_tien_kk"] = sum_thanh_tien_kk + "  ";
            dr_sum["thanh_tien_thua"] = sum_thanh_tien_thua + "  ";
            dr_sum["thanh_tien_thieu"] = sum_thanh_tien_thieu + "  ";
            data_table.Rows.Add(dr_sum);

            #endregion


            #region bind data vào gridview

            xGridView.DataSource = data_table;
            xGridView.DataBind();
            #endregion

            #region xuất report

            WebModule.Warehouse.Report._05_VT _05_VT = new WebModule.Warehouse.Report._05_VT();
            xGridViewExporter.GridViewID = "xGridView";
            _05_VT.printableCC.PrintableComponent = new PrintableComponentLinkBase() { Component = xGridViewExporter };
            ReportViewer.Report = _05_VT;

            #endregion
        }

        //danh sách cột : [stt] [ten] [ma_so] [don_vi_tinh] [don_gia] [so_luong_kt] [thanh_tien_kt] [so_luong_kk] 

        //                [thanh_tien_kk] [so_luong_thua] [thanh_tien_thua] [so_luong_thieu] [thanh_tien_thieu]

        //                [tot] [kem] [mat]


        public void header_column_GridView()
        {
            GridViewColumn column = new GridViewBandColumn();
            column = new GridViewDataTextColumn { FieldName = "stt", Caption = "STT" };
            xGridView.Columns.Add(column);

            column = new GridViewDataTextColumn { FieldName = "ten", Caption = "Tên, nhãn hiệu, quy cách vật tư, dụng cụ ..." };
            xGridView.Columns.Add(column);

            column = new GridViewDataTextColumn { FieldName = "ma_so", Caption = "Mã số" };
            xGridView.Columns.Add(column);

            column = new GridViewDataTextColumn { FieldName = "don_vi_tinh", Caption = "Đơn vị tính" };
            xGridView.Columns.Add(column);

            column = new GridViewDataTextColumn { FieldName = "don_gia", Caption = "Đơn giá" };
            xGridView.Columns.Add(column);

            column = new GridViewBandColumn("Theo sổ kế toán");
            (column as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "so_luong_kt", Caption = "Số lượng" });
            (column as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "thanh_tien_kt", Caption = "Thành tiền" });
            xGridView.Columns.Add(column);

            column = new GridViewBandColumn("Theo kiểm kê");
            (column as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "so_luong_kk", Caption = "Số lượng" });
            (column as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "thanh_tien_kk", Caption = "Thành tiền" });
            xGridView.Columns.Add(column);

            GridViewColumn column_small;
            column = new GridViewBandColumn("Chênh lệch");
            column_small = new GridViewBandColumn("Thừa");
            (column as GridViewBandColumn).Columns.Add(column_small);
            (column_small as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "so_luong_thua", Caption = "Số lượng" });
            (column_small as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "thanh_tien_thua", Caption = "Thành tiền" });

            column_small = new GridViewBandColumn("Thiếu");
            (column as GridViewBandColumn).Columns.Add(column_small);
            (column_small as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "so_luong_thieu", Caption = "Số lượng" });
            (column_small as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "thanh_tien_thieu", Caption = "Thành tiền" });
            xGridView.Columns.Add(column);


            column = new GridViewBandColumn("Phẩm chất");
            (column as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "tot", Caption = "Tốt 100%" });
            (column as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "kem", Caption = "Kém phẩm chất" });
            (column as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "mat", Caption = "Mất phẩm chất" });
            xGridView.Columns.Add(column);
        }

        public DataTable table_template()
        {
            DataTable template = new DataTable();
            DataColumn dc;
            dc = template.Columns.Add("stt");
            dc = template.Columns.Add("ten");
            dc = template.Columns.Add("ma_so");
            dc = template.Columns.Add("don_vi_tinh");
            dc = template.Columns.Add("don_gia");
            dc = template.Columns.Add("so_luong_kt");
            dc = template.Columns.Add("thanh_tien_kt");
            dc = template.Columns.Add("so_luong_kk");
            dc = template.Columns.Add("thanh_tien_kk");
            dc = template.Columns.Add("so_luong_thua");
            dc = template.Columns.Add("thanh_tien_thua");
            dc = template.Columns.Add("so_luong_thieu");
            dc = template.Columns.Add("thanh_tien_thieu");
            dc = template.Columns.Add("tot");
            dc = template.Columns.Add("kem");
            dc = template.Columns.Add("mat");

            DataRow dr;
            dr = template.NewRow();
            dr["stt"] = "A  ";
            dr["ten"] = "B  ";
            dr["ma_so"] = "C  ";
            dr["don_vi_tinh"] = "D  ";
            dr["don_gia"] = "1  ";
            dr["so_luong_kt"] = "2  ";
            dr["thanh_tien_kt"] = "3  ";
            dr["so_luong_kk"] = "4  ";
            dr["thanh_tien_kk"] = "5  ";
            dr["so_luong_thua"] = "6  ";
            dr["thanh_tien_thua"] = "7  ";
            dr["so_luong_thieu"] = "8  ";
            dr["thanh_tien_thieu"] = "9  ";
            dr["tot"] = "10  ";
            dr["kem"] = "11  ";
            dr["mat"] = "12  ";

            template.Rows.Add(dr);
            return template;
        }

        protected void xGridViewExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            GridViewDataColumn dataColumn = e.Column as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data && dataColumn.FieldName == "stt")
            {
                var currFont = e.BrickStyle.Font;
                e.BrickStyle.Font = new System.Drawing.Font(currFont, System.Drawing.FontStyle.Bold);
            }

            if (e.Text.Contains("  "))
            {
                var currFont = e.BrickStyle.Font;
                e.BrickStyle.Font = new System.Drawing.Font(currFont, System.Drawing.FontStyle.Bold);
            }
        }
    }
}