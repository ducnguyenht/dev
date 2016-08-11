using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Accounting.Report;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.System.ShareDim;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxGridView;
using System.Data;
using NAS.DAL.BI.Accounting.GeneralLedger;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class GeneralLedger : System.Web.UI.UserControl
    {
        Session session;
        BO_GeneralLedger BO = new BO_GeneralLedger();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (GLedger.Contains("show"))
                LoadData();
        }

        public void LoadData()
        {
            WebModule.Accounting.Report.GeneralLedger generalLedger = new WebModule.Accounting.Report.GeneralLedger();
            try
            {
                #region Parametter
                string account = this.GeneralLedgerAcc.Get("account_id").ToString();
                int month = int.Parse(this.GeneralLedgerMonth.Get("month_id").ToString());
                int year = int.Parse(this.GeneralLedgerYear.Get("year_id").ToString());
                string asset = "VND"; //chua su dung gia tri nay neu co
                #endregion

                #region get value
                FinancialAccountDim FAD = BO.get_FinancialAccountDim(session, account, Utility.Constant.ROWSTATUS_ACTIVE);

                MonthDim MD = BO.get_MonthDimId(session, month.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                YearDim YD = BO.get_YearDimId(session, year.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                if (FAD != null && YD != null)
                {
                    FinancialGeneralLedgerByYear_Fact fgly = BO.get_FinancialGeneralLedgerByYear_Fact_1(session, FAD.FinancialAccountDimId, YD.YearDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                    if (fgly != null)
                    {
                #endregion

                        #region gan label
                        generalLedger.lblYear.Text = year.ToString();
                        generalLedger.lbl_Account.Text = account;
                        if (fgly.BeginDebitBalance > 0) { generalLedger.lbl_beginDebit.Text = fgly.BeginDebitBalance.ToString("#,#"); } else { generalLedger.lbl_beginDebit.Text = "0"; }
                        if (fgly.BeginCreditBalance > 0) { generalLedger.lbl_beginCredit.Text = fgly.BeginCreditBalance.ToString("#,#"); } else { generalLedger.lbl_beginCredit.Text = "0"; }
                        #endregion

                        #region GridView_header va DT_header
                        GridView_header(FAD.FinancialAccountDimId);
                        DataTable dt = DT_header();
                        #endregion

                        #region do du lieu vao DataTable
                        DT_RowGetValue(dt, FAD.FinancialAccountDimId, YD.YearDimId, fgly.BeginCreditBalance, fgly.BeginDebitBalance);

                        #endregion

                        #region Bindata vao Gridview
                        xGridView.DataSource = dt;
                        xGridView.DataBind();
                        #endregion
                    }
                }
            }
            catch { }

            #region xuat report
            ASPxGridViewExporter1.GridViewID = "xGridView";
            generalLedger.printableCC.PrintableComponent = new PrintableComponentLinkBase() { Component = ASPxGridViewExporter1 };
            ReportViewerGLedger.Report = generalLedger;
            #endregion
        }

        public void GridView_header(int FinancialAccountDimId)
        {
            try
            {
                GridViewDataTextColumn GVDTC = new GridViewDataTextColumn();

                FinancialAccountDim FAD = session.GetObjectByKey<FinancialAccountDim>(FinancialAccountDimId);

                if (FAD != null)
                {
                    GVDTC = new GridViewDataTextColumn { FieldName = "TK", Caption = "Ghi Có các TK, đối xứng Nợ TK " + FAD.Code };
                    xGridView.Columns.Add(GVDTC);

                    for (int i = 1; i <= 12; i++)
                    {
                        GVDTC = new GridViewDataTextColumn { FieldName = i.ToString(), Caption = "Tháng " + i };
                        GVDTC.PropertiesEdit.DisplayFormatString = ("#,#");
                        xGridView.Columns.Add(GVDTC);
                    }
                }
            }
            catch (Exception) { }
        }

        public DataTable DT_header()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("TK");
                for (int i = 1; i <= 12; i++)
                {
                    dt.Columns.Add(i.ToString(), typeof(double));
                }
                return dt;
            }
            catch (Exception) { return null; }
        }

        public DataTable DT_xp_ALL(int FinancialAccountDimId, int YearDimId, bool FinancialGeneralLedgerByYear_Fact, bool FinancialGeneralLedgerByMonth, bool CorrespondFinancialAccountDim, bool CreditOrDebit)
        {
            try
            {
                DataTable dt = new DataTable();
                XPCollection<FinancialGeneralLedgerByYear_Fact> FGLY = BO.get_xp_FinancialGeneralLedgerByYear_Fact(session, FinancialAccountDimId, YearDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                if (FGLY != null && FinancialGeneralLedgerByYear_Fact == true)
                {
                    foreach (FinancialGeneralLedgerByYear_Fact fgly in FGLY)
                    {
                        XPCollection<FinancialGeneralLedgerByMonth> FGLM = BO.get_xp_FinancialGeneralLedgerByMonth(session, fgly.FinancialGeneralLedgerByYear_FactId, Utility.Constant.ROWSTATUS_ACTIVE);
                        if (FGLM != null && FinancialGeneralLedgerByMonth == true)
                        {
                            FGLM.Sorting.Add(new SortProperty("CorrespondFinancialAccountDimId", DevExpress.Xpo.DB.SortingDirection.Ascending));
                            foreach (FinancialGeneralLedgerByMonth fglm in FGLM)
                            {
                                CorrespondFinancialAccountDim corr = BO.get_CorrespondFinancialAccountDim(session, fglm.CorrespondFinancialAccountDimId.Code, Utility.Constant.ROWSTATUS_ACTIVE);
                                if (corr != null && CorrespondFinancialAccountDim == true)
                                {
                                    try
                                    {
                                        if (fglm.CreditSum > 0 && CreditOrDebit == true)
                                            dt.Columns.Add(corr.CorrespondFinancialAccountDimId.ToString());
                                        if (fglm.DebitSum > 0 && CreditOrDebit == false)
                                            dt.Columns.Add(corr.CorrespondFinancialAccountDimId.ToString());
                                    }
                                    catch { continue; }
                                }
                                else if (CorrespondFinancialAccountDim == false)
                                {
                                    try
                                    {
                                        if (fglm.CreditSum > 0 && CreditOrDebit == true)
                                            dt.Columns.Add(fglm.FinancialGeneralLedgerByMonthId.ToString());
                                        if (fglm.DebitSum > 0 && CreditOrDebit == false)
                                            dt.Columns.Add(fglm.FinancialGeneralLedgerByMonthId.ToString());
                                    }
                                    catch { continue; }
                                }
                            }
                        }
                        else if (FinancialGeneralLedgerByMonth == false)
                        {
                            try
                            {
                                dt.Columns.Add(fgly.FinancialGeneralLedgerByYear_FactId.ToString());
                            }
                            catch { continue; }
                        }
                    }
                }
                else return null;
                return dt;
            }
            catch (Exception) { return null; }
        }

        public DataTable DT_getvalue_Credit(int FinancialAccountDimId, int YearDimId)
        {
            try
            {
                DataTable dt = DT_header();
                DataTable dt_xp_all = DT_xp_ALL(FinancialAccountDimId, YearDimId, true, true, true, true);

                if (dt_xp_all == null || dt == null)
                    return null;

                foreach (DataColumn dc_all in dt_xp_all.Columns)
                {
                    DataRow dr = dt.NewRow();

                    CorrespondFinancialAccountDim Corr = BO.get_CorrespondFinancialAccountDim_1(session, int.Parse(dc_all.ColumnName), Utility.Constant.ROWSTATUS_ACTIVE);
                    FinancialGeneralLedgerByYear_Fact FGLY = BO.get_FinancialGeneralLedgerByYear_Fact_1(session, FinancialAccountDimId, YearDimId, Utility.Constant.ROWSTATUS_ACTIVE);

                    #region
                    if (Corr != null)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName.Equals("TK")) { dr[dc.ColumnName] = Corr.Code; }
                            if (!dc.ColumnName.Equals("TK"))
                                for (int i = 1; i <= 12; i++)
                                {
                                    if (FGLY != null && dc.ColumnName.Equals(i.ToString()))
                                    {
                                        FinancialGeneralLedgerByMonth FGLM = BO.get_FinancialGeneralLedgerByMonth_Credit(session, FGLY.FinancialGeneralLedgerByYear_FactId, int.Parse(dc_all.ColumnName), i, Utility.Constant.ROWSTATUS_ACTIVE);
                                        MonthDim MD = BO.get_MonthDimId(session, i.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                                        if (FGLM != null && MD != null)
                                        {
                                            if (FGLM.CreditSum > 0)
                                            {
                                                dr[dc.ColumnName] = sumAcc(Corr.Code, FGLM.FinancialGeneralLedgerByYear_FactId.FinancialGeneralLedgerByYear_FactId, MD.MonthDimId, true);
                                            }
                                            else { dr[dc.ColumnName] = 0; }
                                        }
                                    }
                                }
                        }
                        dt.Rows.Add(dr);
                    }
                    #endregion
                }
                return dt;
            }
            catch (Exception) { return null; }
        }

        public DataTable DT_getvalueDebit(int FinancialAccountDimId, int YearDimId)
        {
            try
            {
                DataTable dt = DT_header();
                DataTable dt_xp_all = DT_xp_ALL(FinancialAccountDimId, YearDimId, true, true, true, false);

                if (dt_xp_all == null || dt == null)
                    return null;
                foreach (DataColumn dc_all in dt_xp_all.Columns)
                {
                    DataRow dr = dt.NewRow();

                    CorrespondFinancialAccountDim Corr = BO.get_CorrespondFinancialAccountDim_1(session, int.Parse(dc_all.ColumnName), Utility.Constant.ROWSTATUS_ACTIVE);
                    FinancialGeneralLedgerByYear_Fact FGLY = BO.get_FinancialGeneralLedgerByYear_Fact_1(session, FinancialAccountDimId, YearDimId, Utility.Constant.ROWSTATUS_ACTIVE);

                    #region
                    if (Corr != null)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName.Equals("TK")) { dr[dc.ColumnName] = Corr.Code; }
                            if (!dc.ColumnName.Equals("TK"))
                                for (int i = 1; i <= 12; i++)
                                {
                                    if (FGLY != null && dc.ColumnName.Equals(i.ToString()))
                                    {
                                        FinancialGeneralLedgerByMonth FGLM = BO.get_FinancialGeneralLedgerByMonth_Debit(session, FGLY.FinancialGeneralLedgerByYear_FactId, int.Parse(dc_all.ColumnName), i, Utility.Constant.ROWSTATUS_ACTIVE);
                                        MonthDim MD = BO.get_MonthDimId(session, i.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                                        if (FGLM != null && MD != null)
                                        {
                                            if (FGLM.DebitSum > 0)
                                            {
                                                dr[dc.ColumnName] = sumAcc(FGLM.CorrespondFinancialAccountDimId.Code, FGLM.FinancialGeneralLedgerByYear_FactId.FinancialGeneralLedgerByYear_FactId, MD.MonthDimId, false);
                                            }
                                            else { dr[dc.ColumnName] = 0; }
                                        }
                                    }
                                }
                        }
                        dt.Rows.Add(dr);
                    }
                    #endregion
                }

                return dt;
            }
            catch (Exception) { return null; }

        }

        #region DT_getvalue gan vao column GridView
        public DataTable DT_RowGetValue(DataTable dt, int FinancialAccountDimId, int YearDimId, double BeginCredit, double BeginDebit)
        {
            try
            {
                DataTable dt_getvalue = DT_getvalue_Credit(FinancialAccountDimId, YearDimId);

                if (dt_getvalue != null)
                {
                    foreach (DataRow dr in dt_getvalue.Rows)
                    {
                        DataRow row = dt.NewRow();

                        row["TK"] = dr["TK"];
                        for (int i = 1; i <= 12; i++)
                        {
                            row[i] = dr[i];
                        }
                        dt.Rows.Add(row);
                    }
                }
                dt.Rows.Add(sumTotal_column(dt, DT_getvalue_Credit(FinancialAccountDimId, YearDimId), "Tổng Nợ"));
                dt.Rows.Add(sumTotal_column(dt, DT_getvalueDebit(FinancialAccountDimId, YearDimId), "Tổng Có"));
                dt.Rows.Add(sumTotal_colum_1(dt, DT_getvalue_Credit(FinancialAccountDimId, YearDimId), DT_getvalueDebit(FinancialAccountDimId, YearDimId), BeginCredit, BeginDebit, "Số dư cuối tháng Nợ"));
                dt.Rows.Add(sumTotal_colum_1(dt, DT_getvalue_Credit(FinancialAccountDimId, YearDimId), DT_getvalueDebit(FinancialAccountDimId, YearDimId), BeginCredit, BeginDebit, "Số dư cuối tháng Có"));
                return dt;
            }
            catch (Exception) { return null; }
        }
        #endregion

        public double sumAcc(string CorrespondFinancialAccountDim_code, int FinancialGeneralLedgerByYear_FactId, int MonthDimId, bool CreditOrDebit)
        {
            double sum = 0;
            CorrespondFinancialAccountDim CFA = BO.get_CorrespondFinancialAccountDim(session, CorrespondFinancialAccountDim_code, Utility.Constant.ROWSTATUS_ACTIVE);
            if (CFA != null)
            {
                XPCollection<FinancialGeneralLedgerByMonth> FGLM = BO.get_xp_FinancialGeneralLedgerByMonth_1(session, CFA.CorrespondFinancialAccountDimId, FinancialGeneralLedgerByYear_FactId, MonthDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                if (FGLM != null)
                {
                    foreach (FinancialGeneralLedgerByMonth fglm in FGLM)
                    {
                        if (fglm.CreditSum > 0 && CreditOrDebit == true)
                            sum += fglm.CreditSum;
                        if (fglm.DebitSum > 0 && CreditOrDebit == false)
                            sum += fglm.DebitSum;
                    }
                }
            }
            return sum;
        }

        private DataRow sumTotal_column(DataTable dt, DataTable data_getvalue, string nameColumn)
        {
            try
            {
                DataRow row1 = dt.NewRow();
                row1["TK"] = nameColumn;
                int column_count = data_getvalue.Columns.Count - 1;
                int row_count = data_getvalue.Rows.Count - 1;
                for (int c = 1; c <= column_count; c++)
                {
                    double sumT = 0;
                    for (int r = 0; r <= row_count; r++)
                    {
                        if (data_getvalue.Rows[r][c].ToString() != null && !data_getvalue.Rows[r][c].ToString().Equals(""))
                        {
                            double tt = double.Parse(data_getvalue.Rows[r][c].ToString());
                            sumT += tt;
                        }
                    }
                    row1[dt.Columns[c]] = sumT;
                }
                return row1;
            }
            catch (Exception) { return null; }
        }

        private DataRow sumTotal_colum_1(DataTable dt, DataTable dt_credit, DataTable dt_debit, double BeginCredit, double BeginDebit, string name)
        {
            try
            {
                DataRow row = dt.NewRow();
                row["TK"] = name;
                int column_count = dt_credit.Columns.Count - 1;
                int row_count_credit = dt_credit.Rows.Count - 1;
                int row_count_debit = dt_debit.Rows.Count - 1;
                double sumN = 0, sumC = 0;
                for (int c = 1; c <= column_count; c++)
                {
                    double sum_credit = 0, sum_debit = 0;
                    for (int r = 0; r <= row_count_credit; r++)
                    {
                        if (dt_credit.Rows[r][c].ToString() != null && !dt_credit.Rows[r][c].ToString().Equals(""))
                        {
                            double tt = double.Parse(dt_credit.Rows[r][c].ToString());
                            sum_debit += tt;
                        }
                    }
                    for (int r = 0; r <= row_count_debit; r++)
                    {
                        if (dt_debit.Rows[r][c].ToString() != null && !dt_debit.Rows[r][c].ToString().Equals(""))
                        {
                            double tt = double.Parse(dt_debit.Rows[r][c].ToString());
                            sum_credit += tt;
                        }
                    }
                    if (c == 1)
                    {
                        sumN = BeginDebit - BeginCredit + sum_debit - sum_credit;
                        if (sumN > 0) { sumC = 0; } else { sumC = Math.Abs(sumN); sumN = 0; }
                    }
                    if (c > 1 && sum_debit + sum_credit > 0)
                    {
                        sumN = sumN - sumC + sum_debit - sum_credit;
                        if (sumN > 0) { sumC = 0; } else { sumC = Math.Abs(sumN); sumN = 0; }
                    }

                    if (c == 1 && name.Equals("Số dư cuối tháng Nợ")) { if (sumN > 0) { row[dt.Columns[c]] = sumN; } }
                    if (c > 1 && sum_debit + sum_credit > 0 && name.Equals("Số dư cuối tháng Nợ")) { if (sumN > 0) { row[dt.Columns[c]] = sumN; } }
                    if (c == 1 && name.Equals("Số dư cuối tháng Có")) { if (sumC > 0) { row[dt.Columns[c]] = sumC; } }
                    if (c > 1 && sum_debit + sum_credit > 0 && name.Equals("Số dư cuối tháng Có")) { if (sumC > 0) { row[dt.Columns[c]] = sumC; } }
                }
                return row;
            }
            catch (Exception) { return null; }
        }
    }
}