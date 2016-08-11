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
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using DevExpress.Web.ASPxGridView;
using System.Data;
using NAS.DAL.BI.Accounting;
using DevExpress.XtraPrintingLinks;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04b6_DN : System.Web.UI.UserControl
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS04b6dn.Contains("show"))
            {
                load_data();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void load_data()
        {
            WebModule.Accounting.Report.S04b6_DN s04b6_dn = new Report.S04b6_DN();
            #region tham số truyền
            int month = Int32.Parse(this.hS04b6DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b6DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            string fAccount = this.hS04b6dnAccount.Get("account_id").ToString();
            //string asset = "";
            #endregion

            if (fAccount != "142" && fAccount != "242" && fAccount != "335") return;

            s04b6_dn.xrMonth.Text = month.ToString();
            s04b6_dn.xrYear.Text = year.ToString();
            if (fAccount == "142" || fAccount == "242")
            {
                s04b6_dn.xrTitle.Text = String.Format("Tập hợp chi phí trả trước (TK {0})", fAccount);
            }
            if (fAccount == "335")
            {
                s04b6_dn.xrTitle.Text = String.Format("Tập hợp chi phí phải trả (TK {0})", fAccount);
            }

            try
            {
                #region object
                MonthDim md = session.FindObject<MonthDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", month)));
                YearDim yd = session.FindObject<YearDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", year)));
                OwnerOrgDim ood = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(String.Format("Code='{0}'",
                    owner)));
                int CorrespondFinancialAccountDimId_default = CorrespondFinancialAccountDim.GetDefault(session,
                    CorrespondFinancialAccountDimEnum.NAAN_DEFAULT).CorrespondFinancialAccountDimId;
                string rowStatusActive = Utility.Constant.ROWSTATUS_ACTIVE.ToString();
                XPCollection<FinancialAccountDim> f_c_FinancialAccountDim = new XPCollection<FinancialAccountDim>(session,
                    CriteriaOperator.Parse(String.Format("Code like '{0}%' AND RowStatus='{1}'", fAccount, rowStatusActive)));

                #endregion

                #region header và table báo cáo
                grid_header();
                DataTable datatable = table_pri();
                #endregion

                #region all row list_transaction
                List<int> list_transaction = new List<int>();
                if (f_c_FinancialAccountDim.Count != 0)
                {
                    foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                    {
                        if (md != null && yd != null && ood != null)
                        {
                            FinancialPrepaidExpenseSummary_Fact FinancialSummary_Fact =
                                session.FindObject<FinancialPrepaidExpenseSummary_Fact>(CriteriaOperator.Parse(
                                String.Format("MonthDimId='{0}' AND "
                                + "YearDimId='{1}' AND "
                                + "OwnerOrgDimId='{2}' AND "
                                + "FinancialAccountDimId='{3}' AND "
                                + "RowStatus='{4}'",
                                md.MonthDimId,
                                yd.YearDimId,
                                ood.OwnerOrgDimId,
                                each_tk.FinancialAccountDimId,
                                rowStatusActive
                                )));
                            if (FinancialSummary_Fact != null)
                            {
                                ////
                                XPCollection<FinancialPrepaidExpenseDetail> collec_detail_credit =
                                    new XPCollection<FinancialPrepaidExpenseDetail>(session, CriteriaOperator.Parse(
                                        String.Format("FinancialPrepaidExpenseSummary_FactId='{0}' AND "
                                        + "Credit>0 AND "
                                        + "CorrespondFinancialAccountDimId!='{1}' AND "
                                        + "RowStatus='{2}'",
                                        FinancialSummary_Fact.FinancialPrepaidExpenseSummary_FactId,
                                        CorrespondFinancialAccountDimId_default,
                                        rowStatusActive
                                        )));
                                if (collec_detail_credit.Count != 0)
                                {
                                    foreach (FinancialPrepaidExpenseDetail each_detail in collec_detail_credit)
                                    {
                                        if (!list_transaction.Contains(each_detail.FinancialTransactionDimId.FinancialTransactionDimId))
                                        {
                                            list_transaction.Add(each_detail.FinancialTransactionDimId.FinancialTransactionDimId);
                                        }
                                    }
                                }
                                ////
                                XPCollection<FinancialPrepaidExpenseDetail> collec_detail_debit =
                                    new XPCollection<FinancialPrepaidExpenseDetail>(session, CriteriaOperator.Parse(
                                        String.Format("FinancialPrepaidExpenseSummary_FactId='{0}' AND "
                                        + "Debit>0 AND "
                                        + "CorrespondFinancialAccountDimId!='{1}' AND "
                                        + "RowStatus='{2}'",
                                        FinancialSummary_Fact.FinancialPrepaidExpenseSummary_FactId,
                                        CorrespondFinancialAccountDimId_default,
                                        rowStatusActive
                                        )));
                                if (collec_detail_debit.Count != 0)
                                {
                                    foreach (FinancialPrepaidExpenseDetail each_detail in collec_detail_debit)
                                    {
                                        if (!list_transaction.Contains(each_detail.FinancialTransactionDimId.FinancialTransactionDimId))
                                        {
                                            list_transaction.Add(each_detail.FinancialTransactionDimId.FinancialTransactionDimId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region đổ dữ liệu
                #region dòng số dư đầu tháng
                DataRow dr = datatable.NewRow();
                dr["dien_giai"] = "Kỳ trước chuyển sang";
                double no_dau = 0, co_dau = 0;
                if (f_c_FinancialAccountDim.Count != 0)
                {
                    foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                    {
                        if (md != null && yd != null && ood != null)
                        {
                            FinancialPrepaidExpenseSummary_Fact FinancialSummary_Fact =
                                session.FindObject<FinancialPrepaidExpenseSummary_Fact>(CriteriaOperator.Parse(
                                String.Format("MonthDimId='{0}' AND "
                                + "YearDimId='{1}' AND "
                                + "OwnerOrgDimId='{2}' AND "
                                + "FinancialAccountDimId='{3}' AND "
                                + "RowStatus='{4}'",
                                md.MonthDimId,
                                yd.YearDimId,
                                ood.OwnerOrgDimId,
                                each_tk.FinancialAccountDimId,
                                rowStatusActive
                                )));
                            if (FinancialSummary_Fact != null)
                            {
                                no_dau += (double)FinancialSummary_Fact.BeginDebitBalance;
                                co_dau += (double)FinancialSummary_Fact.BeginCreditBalance;
                            }
                        }
                    }
                }
                dr["no_dau"] = no_dau;
                dr["co_dau"] = co_dau;
                datatable.Rows.Add(dr);
                #endregion

                int STTu = 1;
                // từng dòng
                foreach (int each_row in list_transaction)
                {
                    #region
                    FinancialTransactionDim transaction = session.FindObject<FinancialTransactionDim>(
                        CriteriaOperator.Parse(String.Format("FinancialTransactionDimId='{0}' AND "
                        + "RowStatus='{1}'",
                        each_row,
                        rowStatusActive
                        )));
                    //
                    dr = datatable.NewRow();
                    dr["stt"] = STTu++;
                    dr["dien_giai"] = transaction.Description;
                    #endregion

                    //từng cột
                    #region credit correspond
                    double cong_no = 0;
                    foreach (string header_column in header_credit_correspond())
                    {
                        double cell = 0;
                        // 
                        CorrespondFinancialAccountDim CorrespondId = session.FindObject<CorrespondFinancialAccountDim>(
                            CriteriaOperator.Parse(String.Format("Code='{0}'", header_column)));
                        //
                        if (f_c_FinancialAccountDim.Count != 0)
                        {
                            foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                            {
                                if (md != null && yd != null && ood != null)
                                {
                                    FinancialPrepaidExpenseSummary_Fact FinancialSummary_Fact =
                                        session.FindObject<FinancialPrepaidExpenseSummary_Fact>(CriteriaOperator.Parse(
                                        String.Format("MonthDimId='{0}' AND "
                                        + "YearDimId='{1}' AND "
                                        + "OwnerOrgDimId='{2}' AND "
                                        + "FinancialAccountDimId='{3}' AND "
                                        + "RowStatus='{4}'",
                                        md.MonthDimId,
                                        yd.YearDimId,
                                        ood.OwnerOrgDimId,
                                        each_tk.FinancialAccountDimId,
                                        rowStatusActive
                                        )));
                                    if (FinancialSummary_Fact != null)
                                    {
                                        XPCollection<FinancialPrepaidExpenseDetail> collec_detail_credit =
                                            new XPCollection<FinancialPrepaidExpenseDetail>(session, CriteriaOperator.Parse(
                                                String.Format("FinancialPrepaidExpenseSummary_FactId='{0}' AND "
                                                + "Credit>0 AND "
                                                + "CorrespondFinancialAccountDimId!='{1}' AND "
                                                + "RowStatus='{2}' AND "
                                                + "FinancialTransactionDimId='{3}' AND "
                                                + "CorrespondFinancialAccountDimId='{4}'",
                                                FinancialSummary_Fact.FinancialPrepaidExpenseSummary_FactId,
                                                CorrespondFinancialAccountDimId_default,
                                                rowStatusActive,
                                                each_row,
                                                CorrespondId.CorrespondFinancialAccountDimId
                                                )));
                                        if (collec_detail_credit.Count != 0)
                                        {

                                            foreach (FinancialPrepaidExpenseDetail each_detail in collec_detail_credit)
                                            {
                                                cell += (double)each_detail.Credit;
                                                cong_no += (double)each_detail.Credit;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //
                        dr[header_column + "_co"] = cell;
                    }
                    dr["cong_no"] = cong_no;
                    #endregion

                    // từng cột
                    #region debit correspond
                    double cong_co = 0;
                    foreach (string header_column in header_debit_correspond())
                    {
                        double cell = 0;
                        // 
                        CorrespondFinancialAccountDim CorrespondId = session.FindObject<CorrespondFinancialAccountDim>(
                            CriteriaOperator.Parse(String.Format("Code='{0}'", header_column)));
                        //
                        if (f_c_FinancialAccountDim.Count != 0)
                        {
                            foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                            {
                                if (md != null && yd != null && ood != null)
                                {
                                    FinancialPrepaidExpenseSummary_Fact FinancialSummary_Fact =
                                        session.FindObject<FinancialPrepaidExpenseSummary_Fact>(CriteriaOperator.Parse(
                                        String.Format("MonthDimId='{0}' AND "
                                        + "YearDimId='{1}' AND "
                                        + "OwnerOrgDimId='{2}' AND "
                                        + "FinancialAccountDimId='{3}' AND "
                                        + "RowStatus='{4}'",
                                        md.MonthDimId,
                                        yd.YearDimId,
                                        ood.OwnerOrgDimId,
                                        each_tk.FinancialAccountDimId,
                                        rowStatusActive
                                        )));
                                    if (FinancialSummary_Fact != null)
                                    {
                                        XPCollection<FinancialPrepaidExpenseDetail> collec_detail_debit =
                                            new XPCollection<FinancialPrepaidExpenseDetail>(session, CriteriaOperator.Parse(
                                                String.Format("FinancialPrepaidExpenseSummary_FactId='{0}' AND "
                                                + "Debit>0 AND "
                                                + "CorrespondFinancialAccountDimId!='{1}' AND "
                                                + "RowStatus='{2}' AND "
                                                + "FinancialTransactionDimId='{3}' AND "
                                                + "CorrespondFinancialAccountDimId='{4}'",
                                                FinancialSummary_Fact.FinancialPrepaidExpenseSummary_FactId,
                                                CorrespondFinancialAccountDimId_default,
                                                rowStatusActive,
                                                each_row,
                                                CorrespondId.CorrespondFinancialAccountDimId
                                                )));
                                        if (collec_detail_debit.Count != 0)
                                        {
                                            foreach (FinancialPrepaidExpenseDetail each_detail in collec_detail_debit)
                                            {
                                                cell += (double)each_detail.Debit;
                                                cong_co += (double)each_detail.Debit;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        dr[header_column + "_no"] = cell;
                    }
                    dr["cong_co"] = cong_co;
                    #endregion

                    datatable.Rows.Add(dr);

                }
                #endregion

                #region dòng cộng
                dr = datatable.NewRow();
                dr["dien_giai"] = "Tổng cộng";

                int column_count = datatable.Columns.Count - 3;
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
                //
                double no_cuoi = 0;
                if (f_c_FinancialAccountDim.Count != 0)
                {
                    foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                    {
                        if (md != null && yd != null && ood != null)
                        {
                            FinancialPrepaidExpenseSummary_Fact FinancialSummary_Fact =
                                session.FindObject<FinancialPrepaidExpenseSummary_Fact>(CriteriaOperator.Parse(
                                String.Format("MonthDimId='{0}' AND "
                                + "YearDimId='{1}' AND "
                                + "OwnerOrgDimId='{2}' AND "
                                + "FinancialAccountDimId='{3}' AND "
                                + "RowStatus='{4}'",
                                md.MonthDimId,
                                yd.YearDimId,
                                ood.OwnerOrgDimId,
                                each_tk.FinancialAccountDimId,
                                rowStatusActive
                                )));
                            no_cuoi += (double)FinancialSummary_Fact.EndDebitBalance;
                        }
                    }
                }
                //
                dr["no_cuoi"] = no_cuoi;
                datatable.Rows.Add(dr);
                #endregion

                #region out gridview
                GridView_S04b6DN.DataSource = datatable;
                GridView_S04b6DN.DataBind();
                #endregion
            }
            catch { }

            #region export report
            s04b6_dn.printableCC_S04b6DN.PrintableComponent = new PrintableComponentLinkBase() { Component = GridViewExporter_S04b6DN };
            ReportViewer_S04b6DN.Report = s04b6_dn;
            #endregion
        }

        public List<string> header_credit_correspond()
        {
            List<string> credit_correspond = new List<string>();
            #region tham số
            int month = Int32.Parse(this.hS04b6DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b6DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            string fAccount = this.hS04b6dnAccount.Get("account_id").ToString();
            #endregion

            #region object
            MonthDim md = session.FindObject<MonthDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", month)));
            YearDim yd = session.FindObject<YearDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", year)));
            OwnerOrgDim ood = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(String.Format("Code='{0}'",
                owner)));
            int CorrespondFinancialAccountDimId_default = CorrespondFinancialAccountDim.GetDefault(session,
                CorrespondFinancialAccountDimEnum.NAAN_DEFAULT).CorrespondFinancialAccountDimId;
            string rowStatusActive = Utility.Constant.ROWSTATUS_ACTIVE.ToString();
            #endregion

            //// tk 142
            XPCollection<FinancialAccountDim> f_c_FinancialAccountDim = new XPCollection<FinancialAccountDim>(session,
                CriteriaOperator.Parse(String.Format("Code like '{0}%' AND RowStatus='{1}'", fAccount, rowStatusActive)));
            if (f_c_FinancialAccountDim.Count != 0)
            {
                foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                {
                    if (md != null && yd != null && ood != null)
                    {
                        FinancialPrepaidExpenseSummary_Fact FinancialSummary_Fact =
                            session.FindObject<FinancialPrepaidExpenseSummary_Fact>(CriteriaOperator.Parse(
                            String.Format("MonthDimId='{0}' AND "
                            + "YearDimId='{1}' AND "
                            + "OwnerOrgDimId='{2}' AND "
                            + "FinancialAccountDimId='{3}' AND "
                            + "RowStatus='{4}'",
                            md.MonthDimId,
                            yd.YearDimId,
                            ood.OwnerOrgDimId,
                            each_tk.FinancialAccountDimId,
                            rowStatusActive
                            )));
                        if (FinancialSummary_Fact != null)
                        {
                            XPCollection<FinancialPrepaidExpenseDetail> collec_detail =
                                new XPCollection<FinancialPrepaidExpenseDetail>(session, CriteriaOperator.Parse(
                                    String.Format("FinancialPrepaidExpenseSummary_FactId='{0}' AND "
                                    + "Credit>0 AND "
                                    + "CorrespondFinancialAccountDimId!='{1}' AND "
                                    + "RowStatus='{2}'",
                                    FinancialSummary_Fact.FinancialPrepaidExpenseSummary_FactId,
                                    CorrespondFinancialAccountDimId_default,
                                    rowStatusActive
                                    )));
                            if (collec_detail.Count != 0)
                            {
                                foreach (FinancialPrepaidExpenseDetail each_detail in collec_detail)
                                {
                                    if (!credit_correspond.Contains(each_detail.CorrespondFinancialAccountDimId.Code))
                                    {
                                        credit_correspond.Add(each_detail.CorrespondFinancialAccountDimId.Code);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ////
            credit_correspond.Sort();
            return credit_correspond;
        }

        public List<string> header_debit_correspond()
        {
            List<string> debit_correspond = new List<string>();
            #region tham số
            int month = Int32.Parse(this.hS04b6DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b6DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            string fAccount = this.hS04b6dnAccount.Get("account_id").ToString();
            #endregion

            #region object
            MonthDim md = session.FindObject<MonthDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", month)));
            YearDim yd = session.FindObject<YearDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", year)));
            OwnerOrgDim ood = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(String.Format("Code='{0}'",
                owner)));
            int CorrespondFinancialAccountDimId_default = CorrespondFinancialAccountDim.GetDefault(session,
                CorrespondFinancialAccountDimEnum.NAAN_DEFAULT).CorrespondFinancialAccountDimId;
            string rowStatusActive = Utility.Constant.ROWSTATUS_ACTIVE.ToString();
            #endregion

            //// tk 142
            XPCollection<FinancialAccountDim> f_c_FinancialAccountDim = new XPCollection<FinancialAccountDim>(session,
                CriteriaOperator.Parse(String.Format("Code like '{0}%' AND RowStatus='{1}'", fAccount, rowStatusActive)));
            if (f_c_FinancialAccountDim.Count != 0)
            {
                foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                {
                    if (md != null && yd != null && ood != null)
                    {
                        FinancialPrepaidExpenseSummary_Fact FinancialSummary_Fact =
                            session.FindObject<FinancialPrepaidExpenseSummary_Fact>(CriteriaOperator.Parse(
                            String.Format("MonthDimId='{0}' AND "
                            + "YearDimId='{1}' AND "
                            + "OwnerOrgDimId='{2}' AND "
                            + "FinancialAccountDimId='{3}' AND "
                            + "RowStatus='{4}'",
                            md.MonthDimId,
                            yd.YearDimId,
                            ood.OwnerOrgDimId,
                            each_tk.FinancialAccountDimId,
                            rowStatusActive
                            )));
                        if (FinancialSummary_Fact != null)
                        {
                            XPCollection<FinancialPrepaidExpenseDetail> collec_detail =
                                new XPCollection<FinancialPrepaidExpenseDetail>(session, CriteriaOperator.Parse(
                                    String.Format("FinancialPrepaidExpenseSummary_FactId='{0}' AND "
                                    + "Debit>0 AND "
                                    + "CorrespondFinancialAccountDimId!='{1}' AND "
                                    + "RowStatus='{2}'",
                                    FinancialSummary_Fact.FinancialPrepaidExpenseSummary_FactId,
                                    CorrespondFinancialAccountDimId_default,
                                    rowStatusActive
                                    )));
                            if (collec_detail.Count != 0)
                            {
                                foreach (FinancialPrepaidExpenseDetail each_detail in collec_detail)
                                {
                                    if (!debit_correspond.Contains(each_detail.CorrespondFinancialAccountDimId.Code))
                                    {
                                        debit_correspond.Add(each_detail.CorrespondFinancialAccountDimId.Code);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ////
            debit_correspond.Sort();
            return debit_correspond;
        }

        public void grid_header()
        {
            string fAccount = this.hS04b6dnAccount.Get("account_id").ToString();

            GridViewBandColumn bandColumn;
            GridViewDataTextColumn fieldColumn = new GridViewDataTextColumn { FieldName = "stt", Caption = "Số TT" };
            GridView_S04b6DN.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "dien_giai", Caption = "Diễn giải" };
            GridView_S04b6DN.Columns.Add(fieldColumn);
            //
            bandColumn = new GridViewBandColumn("Số dư đầu tháng");
            fieldColumn = new GridViewDataTextColumn { FieldName = "no_dau", Caption = "Nợ" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "co_dau", Caption = "Có" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            GridView_S04b6DN.Columns.Add(bandColumn);
            //
            bandColumn = new GridViewBandColumn(String.Format("Ghi nợ TK {0}, ghi có các TK", fAccount));
            foreach (string credit_correspond in header_credit_correspond())
            {
                fieldColumn = new GridViewDataTextColumn
                {
                    FieldName = credit_correspond + "_co",
                    Caption = credit_correspond
                };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(fieldColumn);
            }
            fieldColumn = new GridViewDataTextColumn
            {
                FieldName = "cong_no",
                Caption = String.Format("Cộng nợ TK {0}", fAccount)
            };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            GridView_S04b6DN.Columns.Add(bandColumn);
            //
            bandColumn = new GridViewBandColumn(String.Format("Ghi có TK {0}, ghi nợ các TK", fAccount));
            foreach (string debit_correspond in header_debit_correspond())
            {
                fieldColumn = new GridViewDataTextColumn
                {
                    FieldName = debit_correspond + "_no",
                    Caption = debit_correspond
                };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(fieldColumn);
            }
            fieldColumn = new GridViewDataTextColumn
            {
                FieldName = "cong_co",
                Caption = String.Format("Cộng có TK {0}", fAccount)
            };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            GridView_S04b6DN.Columns.Add(bandColumn);
            //
            bandColumn = new GridViewBandColumn("Số dư cuối tháng");
            fieldColumn = new GridViewDataTextColumn { FieldName = "no_cuoi", Caption = "Nợ" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "co_cuoi", Caption = "Có" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            GridView_S04b6DN.Columns.Add(bandColumn);
        }

        public DataTable table_pri()
        {
            DataTable table_pri = new DataTable();
            DataColumn dc = table_pri.Columns.Add("stt");
            dc = table_pri.Columns.Add("dien_giai");
            dc = table_pri.Columns.Add("no_dau", typeof(double));
            dc = table_pri.Columns.Add("co_dau", typeof(double));
            foreach (string credit_correspond in header_credit_correspond())
            {
                dc = table_pri.Columns.Add(credit_correspond + "_co", typeof(double));
            }
            dc = table_pri.Columns.Add("cong_no", typeof(double));
            foreach (string debit_correspond in header_debit_correspond())
            {
                dc = table_pri.Columns.Add(debit_correspond + "_no", typeof(double));
            }
            dc = table_pri.Columns.Add("cong_co", typeof(double));
            dc = table_pri.Columns.Add("no_cuoi", typeof(double));
            dc = table_pri.Columns.Add("co_cuoi", typeof(double));

            // frist row
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
    }
}