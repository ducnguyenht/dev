using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.BI.Accounting.Account;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Actor;
using DevExpress.Web.ASPxGridView;
using System.Data;
using DevExpress.XtraPrintingLinks;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04b4_DN : System.Web.UI.UserControl
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS04b4dn.Contains("show"))
            {
                load_data();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public void load_data()
        {
            WebModule.Accounting.Report.S04b4_DN s04b4_dn = new Report.S04b4_DN();

            #region tham số truyền
            int month = Int32.Parse(this.hS04b4DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b4DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            //string asset = "";
            #endregion
            s04b4_dn.xrMonth.Text = month.ToString();
            s04b4_dn.xrYear.Text = year.ToString();

            #region object
            MonthDim md = session.FindObject<MonthDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", month)));
            YearDim yd = session.FindObject<YearDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", year)));
            OwnerOrgDim ood = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(String.Format("Code='{0}'",
                owner)));
            int CorrespondFinancialAccountDimId_default = CorrespondFinancialAccountDim.GetDefault(session,
                CorrespondFinancialAccountDimEnum.NAAN_DEFAULT).CorrespondFinancialAccountDimId;
            FinancialSalesOrManufactureExpenseSummary_Fact FinancialFact_General = null;
            #endregion

            #region header và table báo cáo
            grid_header();
            DataTable datatable = table_pri();
            #endregion

            #region all row
            List<string> all_row_f_c = new List<string>();
            if (md != null && yd != null && ood != null)
            {
                // chung
                FinancialFact_General =
                session.FindObject<FinancialSalesOrManufactureExpenseSummary_Fact>(
                    CriteriaOperator.Parse(String.Format("MonthDimId='{0}' AND YearDimId='{1}' AND "
                    + "OwnerOrgDimId='{2}' AND RowStatus='1'",
                    md.MonthDimId,
                    yd.YearDimId,
                    ood.OwnerOrgDimId)));
                // f & c
                all_row_f_c.AddRange(support_find_row_TK(FinancialFact_General, "154"));
                all_row_f_c.AddRange(support_find_row_TK(FinancialFact_General, "621"));
                all_row_f_c.AddRange(support_find_row_TK(FinancialFact_General, "622"));
                all_row_f_c.AddRange(support_find_row_TK(FinancialFact_General, "623"));
                all_row_f_c.AddRange(support_find_row_TK(FinancialFact_General, "627"));
                all_row_f_c.AddRange(support_find_row_TK(FinancialFact_General, "631"));
                //
            }
            #endregion

            #region đổ dữ liệu



            int STTu = 1;
            // từng dòng
            foreach (string each_row in all_row_f_c)
            {
                #region
                FinancialAccountDim fFinancialAccountDim = session.FindObject<FinancialAccountDim>(
                            CriteriaOperator.Parse(String.Format("Code='{0}'", each_row.Substring(0, 3))));
                //
                DataRow dr = datatable.NewRow();
                if (each_row == "154" || each_row == "621" || each_row == "622" || each_row == "623"
                    || each_row == "627" || each_row == "631")
                {
                    dr["stt"] = STTu++;
                }

                FinancialAccountDim get_Description = session.FindObject<FinancialAccountDim>(
                    CriteriaOperator.Parse(String.Format("Code='{0}'", each_row)));
                if (each_row == "154" || each_row == "621" || each_row == "622" || each_row == "623"
                    || each_row == "627" || each_row == "631")
                {
                    dr["tk_no"] = String.Format("TK {0} - {1}", each_row, get_Description.Description);
                }
                else
                {
                    dr["tk_no"] = get_Description.Description;
                }
                #endregion
                double sum_CPTT = 0;
                // từng cột
                foreach (string each_column in list_header())
                {
                    #region
                    int TK_column_CorrespondFinancialAccountDimId = session.FindObject<CorrespondFinancialAccountDim>(
                        CriteriaOperator.Parse(String.Format("Code='{0}'", each_column))).CorrespondFinancialAccountDimId;
                    ////
                    if (md != null && yd != null && ood != null)
                    {
                        // only
                        FinancialFact_General =
                        session.FindObject<FinancialSalesOrManufactureExpenseSummary_Fact>(
                            CriteriaOperator.Parse(String.Format("MonthDimId='{0}' AND YearDimId='{1}' AND "
                            + "OwnerOrgDimId='{2}' AND RowStatus='1'",
                            md.MonthDimId,
                            yd.YearDimId,
                            ood.OwnerOrgDimId)));



                        if (FinancialFact_General != null && fFinancialAccountDim != null)
                        {
                            SalesOrManufactureExpenseByGroup SalesByGroup = session.FindObject<
                                SalesOrManufactureExpenseByGroup>(CriteriaOperator.Parse(
                                    String.Format("FinancialSalesOrManufactureExpenseSummary_FactId='{0}' AND "
                                        + "FinancialAccountDimId='{1}' AND RowStatus='1'",
                                        FinancialFact_General.FinancialSalesOrManufactureExpenseSummary_FactId,
                                        fFinancialAccountDim.FinancialAccountDimId
                                        )));
                            if (SalesByGroup != null)
                            {
                                //tìm tập hợp của tài khoản cha, con với từng tk header
                                XPCollection<FinancialSalesOrManufactureExpenseDetail> all_detail =
                                    new XPCollection<FinancialSalesOrManufactureExpenseDetail>(session,
                                        CriteriaOperator.Parse(String.Format("SalesOrManufactureExpenseByGroupId='{0}' AND "
                                        + "CorrespondFinancialAccountDimId='{1}' AND "
                                        + "Credit>0 AND "
                                        + "RowStatus='1'",
                                        SalesByGroup.SalesOrManufactureExpenseByGroupId,
                                        TK_column_CorrespondFinancialAccountDimId
                                        )));
                                if (all_detail.Count != 0)
                                {
                                    if (each_row == "154" || each_row == "621" || each_row == "622" || each_row == "623"
                                                || each_row == "627" || each_row == "631")
                                    {
                                        double sum_fFinancialAccountDim = 0;
                                        foreach (FinancialSalesOrManufactureExpenseDetail each_detail in all_detail)
                                        {
                                            // tổng
                                            sum_fFinancialAccountDim += (double)each_detail.Credit;
                                            //chi phí thực tế
                                            sum_CPTT += (double)each_detail.Credit;
                                        }
                                        dr[each_column] = sum_fFinancialAccountDim;
                                    }
                                    else
                                    {
                                        double cell = 0;
                                        foreach (FinancialSalesOrManufactureExpenseDetail each_detail in all_detail)
                                        {
                                            if (each_row == each_detail.FinancialAccountDimId.Code)
                                            {
                                                cell += (double)each_detail.Credit;
                                                //chi phí thực tế
                                                sum_CPTT += (double)each_detail.Credit;
                                            }
                                        }
                                        dr[each_column] = cell;
                                    }
                                }

                                if (each_row == "154" || each_row == "621" || each_row == "622" || each_row == "623"
                                                || each_row == "627" || each_row == "631")
                                {
                                    dr["cong_tt"] = SalesByGroup.SumExpense;
                                }
                                else
                                {
                                    dr["cong_tt"] = sum_CPTT;
                                }

                            }
                        }
                    }
                    ////
                    #endregion
                }
                datatable.Rows.Add(dr);
            }





            #endregion

            #region dòng cộng
            DataRow dr_c = datatable.NewRow();
            dr_c["tk_no"] = "CỘNG";
            List<string> all_column = list_header();
            all_column.Add("cong_tt");
            foreach (string each_column in all_column)
            {
                double sum = 0;
                try
                {
                    sum += (from DataRow dr1 in datatable.Rows where dr1["stt"].Equals("1") select (double)dr1[each_column]).FirstOrDefault();
                }
                catch { }
                try
                {
                    sum += (from DataRow dr1 in datatable.Rows where dr1["stt"].Equals("2") select (double)dr1[each_column]).FirstOrDefault();
                }
                catch
                { }
                try
                {
                    sum += (from DataRow dr1 in datatable.Rows where dr1["stt"].Equals("3") select (double)dr1[each_column]).FirstOrDefault();
                }
                catch
                { }
                try
                {
                    sum += (from DataRow dr1 in datatable.Rows where dr1["stt"].Equals("4") select (double)dr1[each_column]).FirstOrDefault();
                }
                catch
                { }
                try
                {
                    sum += (from DataRow dr1 in datatable.Rows where dr1["stt"].Equals("5") select (double)dr1[each_column]).FirstOrDefault();
                }
                catch
                { }
                try
                {
                    sum += (from DataRow dr1 in datatable.Rows where dr1["stt"].Equals("6") select (double)dr1[each_column]).FirstOrDefault();
                }
                catch
                { }
                dr_c[each_column] = sum;
            }
            datatable.Rows.Add(dr_c);
            #endregion

            #region out gridview
            GridView_S04b4DN.DataSource = datatable;
            GridView_S04b4DN.DataBind();
            #endregion

            #region export report
            s04b4_dn.printableCC_S04b4DN.PrintableComponent = new PrintableComponentLinkBase() { Component = GridViewExporter_S04b4DN };
            ReportViewer_S04b4DN.Report = s04b4_dn;
            #endregion
        }

        //TK header (f & c)
        public List<string> list_header()
        {
            #region tham số truyền
            int month = Int32.Parse(this.hS04b4DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b4DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            //string asset = "";
            #endregion
            List<string> list_header = new List<string>();
            MonthDim md = session.FindObject<MonthDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", month)));
            YearDim yd = session.FindObject<YearDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", year)));
            OwnerOrgDim ood = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(String.Format("Code='{0}'",
                owner)));
            int CorrespondFinancialAccountDimId_default = CorrespondFinancialAccountDim.GetDefault(session,
                CorrespondFinancialAccountDimEnum.NAAN_DEFAULT).CorrespondFinancialAccountDimId;

            if (md != null && yd != null && ood != null)
            {
                // chung
                FinancialSalesOrManufactureExpenseSummary_Fact FinancialFact_General =
                session.FindObject<FinancialSalesOrManufactureExpenseSummary_Fact>(
                    CriteriaOperator.Parse(String.Format("MonthDimId='{0}' AND YearDimId='{1}' AND "
                    + "OwnerOrgDimId='{2}' AND RowStatus='1'",
                    md.MonthDimId,
                    yd.YearDimId,
                    ood.OwnerOrgDimId)));

                // 154 621 622 623 627 631
                support_list_header("154", FinancialFact_General, CorrespondFinancialAccountDimId_default, list_header);
                support_list_header("621", FinancialFact_General, CorrespondFinancialAccountDimId_default, list_header);
                support_list_header("622", FinancialFact_General, CorrespondFinancialAccountDimId_default, list_header);
                support_list_header("623", FinancialFact_General, CorrespondFinancialAccountDimId_default, list_header);
                support_list_header("627", FinancialFact_General, CorrespondFinancialAccountDimId_default, list_header);
                support_list_header("631", FinancialFact_General, CorrespondFinancialAccountDimId_default, list_header);
                //
            }
            list_header.Sort();
            return list_header;
        }


        public void grid_header()
        {
            GridViewBandColumn bandColumn;

            GridViewDataTextColumn fieldColumn = new GridViewDataTextColumn { FieldName = "stt", Caption = "Số TT" };
            GridView_S04b4DN.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "tk_no", Caption = "Các TK ghi nợ" };
            GridView_S04b4DN.Columns.Add(fieldColumn);

            bandColumn = new GridViewBandColumn("Các TK ghi có");
            foreach (string column_name in list_header())
            {
                fieldColumn = new GridViewDataTextColumn { FieldName = column_name, Caption = column_name };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(fieldColumn);
            }
            fieldColumn = new GridViewDataTextColumn { FieldName = "cong_tt", Caption = "Tổng cộng" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            GridView_S04b4DN.Columns.Add(bandColumn);
        }

        public DataTable table_pri()
        {
            DataTable table_pri = new DataTable();
            DataColumn dc = table_pri.Columns.Add("stt");
            dc = table_pri.Columns.Add("tk_no");
            foreach (string column_name in list_header())
            {
                dc = table_pri.Columns.Add(column_name, typeof(double));
            }
            dc = table_pri.Columns.Add("cong_tt", typeof(double));

            //first row
            DataRow dr = table_pri.NewRow();
            dr[0] = "A";
            dr[1] = "B";
            int STTc = 1;
            for (int c = 2; c < table_pri.Columns.Count; c++)
            {
                dr[c] = STTc++;
            }
            table_pri.Rows.Add(dr);
            return table_pri;
        }



        #region support
        public void support_list_header(string TK_fFinancialAccountDim,
            FinancialSalesOrManufactureExpenseSummary_Fact FinancialFact_General,
            int CorrespondFinancialAccountDimId_default, List<string> list_header
            )
        {
            FinancialAccountDim fFinancialAccountDim = session.FindObject<FinancialAccountDim>(
                    CriteriaOperator.Parse(String.Format(
                    "Code='{0}'", TK_fFinancialAccountDim)));

            if (FinancialFact_General != null && fFinancialAccountDim != null)
            {
                SalesOrManufactureExpenseByGroup SalesByGroup = session.FindObject<
                    SalesOrManufactureExpenseByGroup>(CriteriaOperator.Parse(
                    String.Format("FinancialSalesOrManufactureExpenseSummary_FactId='{0}' AND "
                    + "FinancialAccountDimId='{1}' AND RowStatus='1'",
                    FinancialFact_General.FinancialSalesOrManufactureExpenseSummary_FactId,
                    fFinancialAccountDim.FinancialAccountDimId
                    )));

                if (SalesByGroup != null)
                {
                    XPCollection<FinancialSalesOrManufactureExpenseDetail> collect_Detail =
                        new XPCollection<FinancialSalesOrManufactureExpenseDetail>(session, CriteriaOperator.Parse(
                            String.Format("SalesOrManufactureExpenseByGroupId='{0}' AND "
                            + "CorrespondFinancialAccountDimId!='{1}' AND "
                            + "Credit>0 AND "
                            + "RowStatus='1'",
                            SalesByGroup.SalesOrManufactureExpenseByGroupId,
                            CorrespondFinancialAccountDimId_default
                            )));
                    if (collect_Detail.Count != 0)
                    {
                        foreach (FinancialSalesOrManufactureExpenseDetail each_Detail in collect_Detail)
                        {
                            if (!list_header.Contains(each_Detail.CorrespondFinancialAccountDimId.Code))
                            {
                                list_header.Add(each_Detail.CorrespondFinancialAccountDimId.Code);
                            }
                        }
                    }
                }
            }
        }

        public List<string> support_find_row_TK(FinancialSalesOrManufactureExpenseSummary_Fact FinancialFact_General,
            string TK)
        {
            List<string> contain_fc = new List<string>();
            FinancialAccountDim fFinancialAccountDim = session.FindObject<FinancialAccountDim>(
                CriteriaOperator.Parse(String.Format(
                "Code='{0}'", TK)));

            if (FinancialFact_General != null && fFinancialAccountDim != null)
            {
                SalesOrManufactureExpenseByGroup SalesByGroup = session.FindObject<
                SalesOrManufactureExpenseByGroup>(CriteriaOperator.Parse(
                String.Format("FinancialSalesOrManufactureExpenseSummary_FactId='{0}' AND "
                + "FinancialAccountDimId='{1}' AND RowStatus='1'",
                FinancialFact_General.FinancialSalesOrManufactureExpenseSummary_FactId,
                fFinancialAccountDim.FinancialAccountDimId
                )));
                if (SalesByGroup != null)
                {
                    XPCollection<FinancialSalesOrManufactureExpenseDetail> find_fc_throw_detail =
                        new XPCollection<FinancialSalesOrManufactureExpenseDetail>(session, CriteriaOperator.Parse(
                            String.Format("SalesOrManufactureExpenseByGroupId='{0}' AND "
                            + "CorrespondFinancialAccountDimId='128' AND "
                            + "Debit>0 AND "
                            + "RowStatus='1'",
                            SalesByGroup.SalesOrManufactureExpenseByGroupId
                            )));
                    if (find_fc_throw_detail.Count != 0)
                    {
                        foreach (FinancialSalesOrManufactureExpenseDetail each_detail in find_fc_throw_detail)
                        {
                            if (!contain_fc.Contains(each_detail.FinancialAccountDimId.Code))
                            {
                                contain_fc.Add(each_detail.FinancialAccountDimId.Code);
                                // nếu tồn tại con thì lấy cha
                                if (!contain_fc.Contains(TK))
                                {
                                    contain_fc.Add(TK);
                                }
                            }
                        }
                    }
                }
            }
            contain_fc.Sort();
            return contain_fc;
        }
        #endregion
    }
}