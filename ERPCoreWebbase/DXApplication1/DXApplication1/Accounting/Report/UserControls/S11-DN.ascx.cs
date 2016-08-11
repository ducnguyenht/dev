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
using NAS.DAL.BI.Accounting.ItemInventory;
using NAS.DAL.BI.Item;
using NAS.DAL.BI.Accounting.Account;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S11_DN : System.Web.UI.UserControl
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS11dn.Contains("show"))
            {
                load_data();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void load_data()
        {
            WebModule.Accounting.Report.S11_DN s11_dn = new Report.S11_DN();

            #region tham số truyền
            int month = Int32.Parse(this.hS11DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS11DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            string fAccount = this.hS11dnAccount.Get("account_id").ToString();
            //string asset = "";
            short rowstatus = Utility.Constant.ROWSTATUS_ACTIVE;
            #endregion

            #region display report
            s11_dn.xrTaiKhoan.Text = String.Format("Tài khoản: {0}", fAccount);
            s11_dn.xrTime.Text = String.Format("Tháng {0} năm {1}", month, year);
            #endregion

            try
            {
                #region object
                MonthDim md = session.FindObject<MonthDim>(CriteriaOperator.Parse(
                    String.Format("Name='{0}' AND RowStatus='{1}'", month, rowstatus)));
                YearDim yd = session.FindObject<YearDim>(CriteriaOperator.Parse(
                    String.Format("Name='{0}' AND RowStatus='{1}'", year, rowstatus)));
                OwnerOrgDim ood = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(
                    String.Format("Code='{0}' AND RowStatus='{1}'", owner, rowstatus)));
                FinancialAccountDim accountDim = session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse(
                    String.Format("Code='{0}' AND RowStatus='{1}'", fAccount, rowstatus)));
                XPCollection<FinancialItemInventorySummary_Fact> Summary_Fact;
                #endregion


                #region header và table báo cáo
                grid_header();
                DataTable datatable = table_pri();
                #endregion

                #region list hàng hóa
                List<string> items = new List<string>();
                if (md != null && yd != null && ood != null && accountDim != null)
                {
                    Summary_Fact =
                        new XPCollection<FinancialItemInventorySummary_Fact>(session, CriteriaOperator.Parse(
                            String.Format("MonthDimId='{0}' AND "
                            + "YearDimId='{1}' AND "
                            + "OwnerOrgDimId='{2}' AND "
                            + "RowStatus='{3}' AND "
                            + "FinancialAccountDimId='{4}'",
                            md.MonthDimId,
                            yd.YearDimId,
                            ood.OwnerOrgDimId,
                            rowstatus,
                            accountDim.FinancialAccountDimId
                            )));
                    if (Summary_Fact.Count != 0)
                    {
                        foreach (FinancialItemInventorySummary_Fact each_Fact in Summary_Fact)
                        {
                            if (!items.Contains(String.Format("{0}|{1}", each_Fact.ItemDimId.Code, each_Fact.UnitDimId.Code)))
                            {
                                items.Add(String.Format("{0}|{1}", each_Fact.ItemDimId.Code, each_Fact.UnitDimId.Code));
                            }
                        }
                    }
                }
                #endregion

                #region đổ dữ liệu
                DataRow dr;
                // từng dòng
                int STT = 1;
                foreach (string item in items)
                {
                    string itemCode = item.Split('|')[0];
                    string unitCode = item.Split('|')[1];

                    ItemDim itemDim = session.FindObject<ItemDim>(CriteriaOperator.Parse(
                        String.Format("Code='{0}'", itemCode)));
                    UnitDim unitDim = session.FindObject<UnitDim>(CriteriaOperator.Parse(
                        String.Format("Code='{0}'", unitCode)));

                    dr = datatable.NewRow();
                    dr["stt"] = STT++;
                    dr["ten"] = String.Format("{0} ({1})", itemDim.Name, unitDim.Name);

                    if (md != null && yd != null && ood != null && itemDim != null && unitDim != null && accountDim != null)
                    {
                        Summary_Fact =
                            new XPCollection<FinancialItemInventorySummary_Fact>(session, CriteriaOperator.Parse(
                                String.Format("MonthDimId='{0}' AND "
                                + "YearDimId='{1}' AND "
                                + "OwnerOrgDimId='{2}' AND "
                                + "RowStatus='{3}' AND "
                                + "ItemDimId='{4}' AND "
                                + "UnitDimId='{5}' AND "
                                + "FinancialAccountDimId='{6}'",
                                md.MonthDimId,
                                yd.YearDimId,
                                ood.OwnerOrgDimId,
                                rowstatus,
                                itemDim.ItemDimId,
                                unitDim.UnitDimId,
                                accountDim.FinancialAccountDimId
                                )));
                        if (Summary_Fact.Count != 0)
                        {
                            double debit_sum = 0, credit_sum = 0, begin = 0, end = 0;
                            foreach (FinancialItemInventorySummary_Fact each_fact in Summary_Fact)
                            {
                                begin += (double)each_fact.BeginDebitBalance;
                                debit_sum += (double)each_fact.DebitSum;
                                credit_sum += (double)each_fact.CreditSum;
                                end += (double)each_fact.EndDebitBalance;
                            }


                            dr["ton_dau"] = begin;
                            dr["nhap"] = debit_sum;
                            dr["xuat"] = credit_sum;
                            dr["ton_cuoi"] = end;
                        }
                    }
                    datatable.Rows.Add(dr);
                }
                #endregion

                #region dòng cộng
                dr = datatable.NewRow();
                dr["ten"] = "Cộng";

                int column_count = datatable.Columns.Count - 1;
                int row_count = datatable.Rows.Count - 1;
                for (int c = 2; c <= column_count; c++)
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
                GridView_S11DN.DataSource = datatable;
                GridView_S11DN.DataBind();
                #endregion
            }
            catch { }

            #region export report
            s11_dn.printableCC_S11DN.PrintableComponent = new PrintableComponentLinkBase() { Component = GridViewExporter_S11DN };
            ReportViewer_S11DN.Report = s11_dn;
            #endregion

        }

        public void grid_header()
        {
            GridViewBandColumn bandColumn;

            GridViewDataTextColumn fieldColumn = new GridViewDataTextColumn { FieldName = "stt", Caption = "Số TT" };
            GridView_S11DN.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "ten", Caption = "Tên, quy cách vật liệu, dụng cụ, sản phẩm, hàng hóa" };
            GridView_S11DN.Columns.Add(fieldColumn);

            bandColumn = new GridViewBandColumn("Số tiền");
            fieldColumn = new GridViewDataTextColumn { FieldName = "ton_dau", Caption = "Tồn đầu kỳ" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "nhap", Caption = "Nhập trong kỳ" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "xuat", Caption = "Xuất trong kỳ" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "ton_cuoi", Caption = "Tồn cuối kỳ" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);

            GridView_S11DN.Columns.Add(bandColumn);
        }

        public DataTable table_pri()
        {
            DataTable table_pri = new DataTable();
            DataColumn dc = table_pri.Columns.Add("stt");
            dc = table_pri.Columns.Add("ten");
            dc = table_pri.Columns.Add("ton_dau", typeof(double));
            dc = table_pri.Columns.Add("nhap", typeof(double));
            dc = table_pri.Columns.Add("xuat", typeof(double));
            dc = table_pri.Columns.Add("ton_cuoi", typeof(double));

            // first row
            DataRow dr = table_pri.NewRow();
            dr["stt"] = "A";
            dr["ten"] = "B";
            dr["ton_dau"] = 1;
            dr["nhap"] = 2;
            dr["xuat"] = 3;
            dr["ton_cuoi"] = 4;
            table_pri.Rows.Add(dr);
            return table_pri;
        }
    }
}