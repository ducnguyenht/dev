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
using NAS.DAL.System.ShareDim;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Item;
using NAS.DAL.BI.Accounting.ItemInventory;
using NAS.DAL.BI.Inventory;
using NAS.DAL.BI.Accounting.Account;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S12_DN : System.Web.UI.UserControl
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS12dn.Contains("show"))
            {
                load_data();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void load_data()
        {
            WebModule.Accounting.Report.S12_DN s12_dn = new Report.S12_DN();

            #region truyền data
            int month = Int32.Parse(this.hS12DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS12DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            short rowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
            string item = this.hs12dnItem.Get("Item_id").ToString(); //"ACAPELLA S";
            string unit = "Thùng";
            #endregion

            #region display report
            s12_dn.xrDate.Text = String.Format("Ngày lập thẻ: {0:dd/MM/yyyy}", DateTime.Now);
            s12_dn.xrNumber.Text = "Tờ số: ..............";
            s12_dn.xrName.Text = String.Format("Tên, nhãn hiệu, quy cách vật tư: {0}", item);
            s12_dn.xrUnit.Text = String.Format("Đơn vị tính: {0}", unit);
            s12_dn.xrCode.Text = "Mã số: ................";
            #endregion

            #region object
            MonthDim monthDim = session.FindObject<MonthDim>(CriteriaOperator.Parse(String.Format(
                "Name='{0}' AND RowStatus='{1}'", month, rowStatus)));
            YearDim yearDim = session.FindObject<YearDim>(CriteriaOperator.Parse(String.Format(
                "Name='{0}' AND RowStatus='{1}'", year, rowStatus)));
            ItemDim itemDim = session.FindObject<ItemDim>(CriteriaOperator.Parse(String.Format(
                "Name='{0}' AND RowStatus='{1}'", item, rowStatus)));
            UnitDim unitDim = session.FindObject<UnitDim>(CriteriaOperator.Parse(String.Format(
                "Code='{0}' AND RowStatus='{1}'", unit, rowStatus)));
            OwnerOrgDim ownerOrgDim = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(
                String.Format("Code='{0}' AND RowStatus='{1}'", owner, rowStatus)));
            int CorrespondFinancialAccountDimId_default = CorrespondFinancialAccountDim.GetDefault(session,
                CorrespondFinancialAccountDimEnum.NAAN_DEFAULT).CorrespondFinancialAccountDimId;
            #endregion

            #region header và dataTable
            grid_header();
            DataTable datatable = table_pri();
            #endregion

            #region row
            XPCollection<ItemInventoryByArtifact> ItemArtifacts =
                new XPCollection<ItemInventoryByArtifact>(session, CriteriaOperator.Parse(
                    String.Format("RowStatus='{0}' AND CorrespondFinancialAccountDimId='{1}'",
                    rowStatus, CorrespondFinancialAccountDimId_default)));
            DataRow dr;
            if (ItemArtifacts.Count != 0)
            {
                int STT = 1;
                foreach (ItemInventoryByArtifact fact in ItemArtifacts)
                {
                    if (itemDim != null && unitDim != null && ownerOrgDim != null)
                    {
                        if (fact.FinancialItemInventorySummary_FactId.ItemDimId.Code == itemDim.Code
                            && fact.FinancialItemInventorySummary_FactId.UnitDimId.Code == unitDim.Code
                            && fact.FinancialItemInventorySummary_FactId.OwnerOrgDimId.Code == ownerOrgDim.Code
                            && fact.FinancialItemInventorySummary_FactId.RowStatus == rowStatus
                            && fact.FinancialItemInventorySummary_FactId.MonthDimId.MonthDimId == monthDim.MonthDimId
                            && fact.FinancialItemInventorySummary_FactId.YearDimId.YearDimId == yearDim.YearDimId)
                        {
                            dr = datatable.NewRow();
                            dr["stt"] = STT++;
                            //dr["date"] = String.Format("");
                            if (fact.InventoryCommandDimId.Code.Contains("ICMD"))
                            {
                                dr["nhap_dau"] = fact.InventoryCommandDimId.Code;
                            }
                            if (fact.InventoryCommandDimId.Code.Contains("OUTCMD"))
                            {
                                dr["xuat_dau"] = fact.InventoryCommandDimId.Code;
                            }
                            dr["dien_giai"] = fact.InventoryCommandDimId.Description;
                            dr["date_nhap_xuat"] = String.Format("{0:dd/MM/yyyy}", fact.InventoryCommandDimId.IssueDate);
                            dr["nhap_cuoi"] = fact.FinancialItemInventorySummary_FactId.DebitItemSum;
                            dr["xuat_cuoi"] = fact.FinancialItemInventorySummary_FactId.CreditItemSum;
                            dr["ton_cuoi"] = fact.CurrentBalanceItem;
                            datatable.Rows.Add(dr);
                        }
                    }
                }
            }

            #endregion

            #region dòng cộng
            dr = datatable.NewRow();
            dr["dien_giai"] = "Cộng";

            int column_count = datatable.Columns.Count - 2;
            int row_count = datatable.Rows.Count - 1;
            for (int c = 6; c <= column_count; c++)
            {
                double sumT = 0;
                for (int r = 1; r <= row_count; r++)
                {
                    double tt;
                    double.TryParse(datatable.Rows[r][c].ToString(), out tt);
                    sumT += tt;
                }
                dr[datatable.Columns[c]] = sumT;
            }
            datatable.Rows.Add(dr);
            #endregion

            #region out gridview
            GridView_S12DN.DataSource = datatable;
            GridView_S12DN.DataBind();
            #endregion

            #region export report
            s12_dn.printableCC_S12DN.PrintableComponent =
                new PrintableComponentLinkBase() { Component = GridViewExporter_S12DN };
            ReportViewer_S12DN.Report = s12_dn;
            #endregion
        }

        public void grid_header()
        {
            GridViewBandColumn bandColumn;

            GridViewDataTextColumn fieldColumn = new GridViewDataTextColumn { FieldName = "stt", Caption = "Số TT" };
            GridView_S12DN.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "date", Caption = "Ngày tháng" };
            GridView_S12DN.Columns.Add(fieldColumn);

            bandColumn = new GridViewBandColumn("Số hiệu chứng từ");
            fieldColumn = new GridViewDataTextColumn { FieldName = "nhap_dau", Caption = "Nhập" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "xuat_dau", Caption = "Xuất" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            GridView_S12DN.Columns.Add(bandColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "dien_giai", Caption = "Diễn giải" };
            GridView_S12DN.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "date_nhap_xuat", Caption = "Ngày nhập xuất" };
            GridView_S12DN.Columns.Add(fieldColumn);

            bandColumn = new GridViewBandColumn("Số lượng");
            fieldColumn = new GridViewDataTextColumn { FieldName = "nhap_cuoi", Caption = "Nhập" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "xuat_cuoi", Caption = "Xuất" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "ton_cuoi", Caption = "Tồn" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            GridView_S12DN.Columns.Add(bandColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "ky", Caption = "Ký xác nhận của kế toán" };
            GridView_S12DN.Columns.Add(fieldColumn);
        }

        public DataTable table_pri()
        {
            DataTable table_pri = new DataTable();
            DataColumn dc = table_pri.Columns.Add("stt");
            dc = table_pri.Columns.Add("date");
            dc = table_pri.Columns.Add("nhap_dau");
            dc = table_pri.Columns.Add("xuat_dau");
            dc = table_pri.Columns.Add("dien_giai");
            dc = table_pri.Columns.Add("date_nhap_xuat");
            dc = table_pri.Columns.Add("nhap_cuoi", typeof(double));
            dc = table_pri.Columns.Add("xuat_cuoi", typeof(double));
            dc = table_pri.Columns.Add("ton_cuoi", typeof(double));
            dc = table_pri.Columns.Add("ky");

            // first row
            DataRow dr = table_pri.NewRow();
            dr["stt"] = "A";
            dr["date"] = "B";
            dr["nhap_dau"] = "C";
            dr["xuat_dau"] = "D";
            dr["dien_giai"] = "E";
            dr["date_nhap_xuat"] = "G";
            dr["nhap_cuoi"] = "1";
            dr["xuat_cuoi"] = "2";
            dr["ton_cuoi"] = "3";
            dr["ky"] = "4";
            table_pri.Rows.Add(dr);
            return table_pri;
        }
    }
}