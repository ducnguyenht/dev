using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using System.Data;
using NAS.DAL.System.ShareDim;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using DevExpress.XtraPrintingLinks;
using NAS.DAL.BI.Accounting.Account;
using DevExpress.Xpo.DB;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a4_DN : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        Session session;
        NAS.BO.Accounting.Report.BO_S04a4_dn BO = new NAS.BO.Accounting.Report.BO_S04a4_dn();
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS04a4dn.Contains("show"))
            {
                load_data();
            }
        }



        public void load_data()
        {
            WebModule.Accounting.Report.S04a4_DN S04a4_DN = new WebModule.Accounting.Report.S04a4_DN();
            try
            {
                #region parametter
                string account = this.hS04a4dnAcc.Get("account_id").ToString();
                int month = Int32.Parse(this.hS04a4dnMonth.Get("month_id").ToString());
                int year = Int32.Parse(this.hS04a4dnYear.Get("year_id").ToString());
                string owner = this.hS04a4dnOwnerOrg.Get("owner_id").ToString();
                string asset = "VND";//chua su dung gia tri nay neu co t

                int lastmonth = 0;
                int lastyear = year;

                if (month == 1)
                {
                    lastmonth = 12;
                    lastyear = year - 1;
                }
                else
                {
                    lastmonth = month - 1;
                }

                #endregion

                #region get Value
                FinancialAccountDim FAD = BO.get_FinancialAccountDim(session, account, Utility.Constant.ROWSTATUS_ACTIVE);

                MonthDim MD = BO.get_MonthDimId(session, month.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                YearDim YD = BO.get_YearDimId(session, year.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                //OwnerOrgDim OOD = BO.get_OwnerOrgDimId(session, owner, Utility.Constant.ROWSTATUS_ACTIVE);
                #endregion
                if (FAD == null || MD == null || YD == null)  //|| OOD == null)
                    return;
                #region gan label
                S04a4_DN.lbl_month.Text = month.ToString();
                S04a4_DN.lbl_year.Text = year.ToString();
                S04a4_DN.lbl_tk.Text = "Ghi Có các tài khoản " + account;
                //DiaryJournal_Fact djf = BO.get_DiaryJournal_Fact_1(session, FAD.FinancialAccountDimId, OOD.OwnerOrgDimId, MD.MonthDimId, YD.YearDimId);
                //if (djf != null && djf.BeginCreditBalance > 0) { S04a4_DN.lbl_BeginCreditBalance.Text = djf.BeginCreditBalance.ToString("#,#"); } else { S04a4_DN.lbl_BeginCreditBalance.Text = djf.BeginCreditBalance.ToString(); }
                //if (djf != null && djf.EndCreditBalance > 0) { S04a4_DN.lbl_EndCreditBalance.Text = djf.EndCreditBalance.ToString("#,#"); } else { S04a4_DN.lbl_EndCreditBalance.Text = djf.EndCreditBalance.ToString(); }

                double beginBalance = 0;
                double endBalance = 0;

                #region begin balance
                string sql = "" +
                  "select sum(beginbalance) as beginbalance " +
                "from " +
                "(select distinct d.IssueDate, -b.Credit as beginbalance, e.Code, d.FinancialTransactionDimId " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c, " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and c.Code like '" + account + "%' " +
                "and (month(d.IssueDate) <= " + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + " ) " +
                "and (b.Credit > 0) " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "union all " +
                "select distinct d.IssueDate, b.Debit as beginbalance, e.Name, d.FinancialTransactionDimId " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c, " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and c.Code like '" + account + "%' " +
                "and (month(d.IssueDate) <= " + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + " ) " +
                "and (b.Debit > 0) " +
                 "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "union all " +
                "select null as IssueDate, b.Debit as beginbalance, null as Name, null as FinancialTransactionDimId " +
                "from BalanceForwardTransaction a, GeneralJournal b, " +
                        "Account c, AccountingPeriod d, [Transaction] e " +
                "where a.TransactionId = b.TransactionId " +
                "and b.AccountId = c.AccountId " +
                "and a.TransactionId = e.TransactionId " +
                "and d.AccountingPeriodId = e.AccountingPeriodId " +
                "and b.Debit > 0 " +
                "and c.Code like '" + account + "%' " +
                "and (month(d.FromDateTime) = 1" + " and year(d.FromDateTime) = " + year.ToString() + ") " +
                ") aa ";

                SelectedData seletectedData = session.ExecuteQuery(sql);

                foreach (var row in seletectedData.ResultSet)
                {
                    foreach (var col in row.Rows)
                    {
                        if (col.Values[0] != null)
                        {
                            S04a4_DN.lbl_BeginCreditBalance.Text = String.Format("{0:#,#}", col.Values[0]);
                        }
                    }
                }
                #endregion
                #region endBalance
                sql = "" +
                  "select sum(beginbalance) as beginbalance " +
                "from " +
                "(select distinct d.IssueDate, -b.Credit as beginbalance, e.Code, d.FinancialTransactionDimId " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c, " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and c.Code like '" + account + "%' " +
                "and (month(d.IssueDate) <= " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
                "and (b.Credit > 0) " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "union all " +
                "select distinct d.IssueDate, b.Debit as beginbalance, e.Name, d.FinancialTransactionDimId " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c, " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and c.Code like '" + account + "%' " +
                "and (month(d.IssueDate) <= " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
                "and (b.Debit > 0) " +
                 "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "union all " +
                "select null as IssueDate, b.Debit as beginbalance, null as Name, null as FinancialTransactionDimId " +
                "from BalanceForwardTransaction a, GeneralJournal b, " +
                        "Account c, AccountingPeriod d, [Transaction] e " +
                "where a.TransactionId = b.TransactionId " +
                "and b.AccountId = c.AccountId " +
                "and a.TransactionId = e.TransactionId " +
                "and d.AccountingPeriodId = e.AccountingPeriodId " +
                "and b.Debit > 0 " +
                "and c.Code like '" + account + "%' " +
                "and (month(d.FromDateTime) = 1" + " and year(d.FromDateTime) = " + year.ToString() + ") " +
                ") aa ";

                seletectedData = session.ExecuteQuery(sql);

                foreach (var row in seletectedData.ResultSet)
                {
                    foreach (var col in row.Rows)
                    {
                        if (col.Values[0] != null)
                        {
                            S04a4_DN.lbl_EndCreditBalance.Text = String.Format("{0:#,#}", col.Values[0]);
                        }
                    }
                }
                #endregion

                #endregion

                #region GridView header và table báo cáo
                GridView_header(FAD.FinancialAccountDimId, MD.MonthDimId, YD.YearDimId); //, OOD.OwnerOrgDimId
                DataTable dt = DT_Header(FAD.FinancialAccountDimId, MD.MonthDimId, YD.YearDimId); //, OOD.OwnerOrgDimId
                #endregion

                #region tao dong STT ngang cho tung cot
                DT_STT(dt);
                #endregion

                #region do du lieu vao DataTable
                DT_rowgetvalue(dt, FAD.FinancialAccountDimId, MD.MonthDimId, YD.YearDimId); //, OOD.OwnerOrgDimId
                #endregion

                #region Binđât vao Gridview
                xGridView.DataSource = dt;
                xGridView.DataBind();
                #endregion
            }
            catch { }

            #region xuat report
            GridViewExporter.GridViewID = "xGridView";
            S04a4_DN.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = GridViewExporter };
            S04a4dnReportViewer.Report = S04a4_DN;
            #endregion
        }

        public void GridView_header(int FinancialAccountDimId, int MonthDimId, int YearDimId)//, int OwnerOrgDimId
        {
            try
            {
                GridViewBandColumn bandColumn = new GridViewBandColumn();
                GridViewDataTextColumn GVDTC = new GridViewDataTextColumn();
                FinancialAccountDim FAD = session.GetObjectByKey<FinancialAccountDim>(FinancialAccountDimId);
                if (FAD != null)
                {
                    #region ghi Co tk 311
                    GVDTC = new GridViewDataTextColumn { FieldName = "STT", Caption = "Số TT" };
                    xGridView.Columns.Add(GVDTC);

                    bandColumn = new GridViewBandColumn("Chứng Từ");
                    GVDTC = new GridViewDataTextColumn { FieldName = "sohieu", Caption = "Số Hiệu" };
                    bandColumn.Columns.Add(GVDTC);
                    GVDTC = new GridViewDataTextColumn { FieldName = "ngaythang", Caption = "Ngày Tháng" };
                    bandColumn.Columns.Add(GVDTC);
                    xGridView.Columns.Add(bandColumn);

                    GVDTC = new GridViewDataTextColumn { FieldName = "diengiai", Caption = "Diễn giải" };
                    xGridView.Columns.Add(GVDTC);

                    bandColumn = new GridViewBandColumn("Ghi Có TK " + FAD.Code + ", ghi Nợ các TK");
                    foreach (DataColumn dc in DT_ListAcc_CreditOrDebit(FinancialAccountDimId, MonthDimId, YearDimId, false).Columns) //, OwnerOrgDimId
                    {
                        GVDTC = new GridViewDataTextColumn { FieldName = dc.ColumnName };
                        GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                        bandColumn.Columns.Add(GVDTC);
                    }
                    GVDTC = new GridViewDataTextColumn { FieldName = "congco", Caption = "Cộng Có TK " + FAD.Code };
                    GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                    bandColumn.Columns.Add(GVDTC);
                    xGridView.Columns.Add(bandColumn);
                    #endregion

                    #region ghi No tk 311
                    GVDTC = new GridViewDataTextColumn { FieldName = "STT_L", Caption = "Số TT" };
                    xGridView.Columns.Add(GVDTC);

                    bandColumn = new GridViewBandColumn("Chứng Từ");
                    GVDTC = new GridViewDataTextColumn { FieldName = "sohieu_L", Caption = "Số Hiệu" };
                    bandColumn.Columns.Add(GVDTC);
                    GVDTC = new GridViewDataTextColumn { FieldName = "ngaythang_L", Caption = "Ngày Tháng" };
                    bandColumn.Columns.Add(GVDTC);
                    xGridView.Columns.Add(bandColumn);

                    bandColumn = new GridViewBandColumn("Ghi Nợ TK " + FAD.Code + ", ghi Có các TK");
                    foreach (DataColumn dc in DT_ListAcc_CreditOrDebit(FinancialAccountDimId, MonthDimId, YearDimId, true).Columns) //, OwnerOrgDimId
                    {
                        GVDTC = new GridViewDataTextColumn { FieldName = dc.ColumnName + "_L", Caption = dc.ColumnName };
                        GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                        bandColumn.Columns.Add(GVDTC);
                    }
                    GVDTC = new GridViewDataTextColumn { FieldName = "congno", Caption = "Cộng Nợ TK " + FAD.Code };
                    GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                    bandColumn.Columns.Add(GVDTC);
                    xGridView.Columns.Add(bandColumn);
                    #endregion
                }
            }
            catch { }
        }

        public DataTable DT_Header(int FinancialAccountDimId, int MonthDimId, int YearDimId) //, int OwnerOrgDimId
        {
            try
            {
                DataTable dt = new DataTable();
                #region ghi co tk 311
                dt.Columns.Add("STT", typeof(double));
                dt.Columns.Add("sohieu");
                dt.Columns.Add("ngaythang");
                dt.Columns.Add("diengiai");
                foreach (DataColumn dc in DT_ListAcc_CreditOrDebit(FinancialAccountDimId, MonthDimId, YearDimId, false).Columns)    //, OwnerOrgDimId
                {
                    dt.Columns.Add(dc.ColumnName, typeof(double));
                }
                dt.Columns.Add("congco", typeof(double));
                #endregion

                #region ghi no tk 311
                dt.Columns.Add("STT_L", typeof(double));
                dt.Columns.Add("sohieu_L");
                dt.Columns.Add("ngaythang_L");
                foreach (DataColumn dc in DT_ListAcc_CreditOrDebit(FinancialAccountDimId, MonthDimId, YearDimId, true).Columns)  //, OwnerOrgDimId
                {
                    dt.Columns.Add(dc.ColumnName + "_L", typeof(double));
                }
                dt.Columns.Add("congno", typeof(double));
                #endregion
                return dt;
            }
            catch (Exception) { return null; }
        }

        #region Datable lay danh sach cac tai khoan
        public DataTable DT_ListAcc_CreditOrDebit(int FinancialAccountDimId, int MonthDimId, int YearDimId, bool CreditOrDebit)   //, int OwnerOrgDimId
        {
            try
            {
                DataTable DT = new DataTable();

                XPCollection<DiaryJournal_Fact> DJF_xp = BO.get_xp_DiaryJournal_Fact(session, FinancialAccountDimId, MonthDimId, YearDimId); //, OwnerOrgDimId
                if (DJF_xp != null)
                {
                    foreach (DiaryJournal_Fact DJF in DJF_xp)
                    {
                        XPCollection<DiaryJournal_Detail> DJD_xp = BO.get_xp_DiaryJournal_Detail_2(session, DJF.DiaryJournal_FactId);
                        if (DJD_xp != null)
                        {
                            foreach (DiaryJournal_Detail DJD in DJD_xp)
                            {
                                try
                                {
                                    if (DJD.Credit > 0 && CreditOrDebit == true)
                                        DT.Columns.Add(DJD.CorrespondFinancialAccountDimId.Code);
                                    else if (DJD.Debit > 0 && CreditOrDebit == false)
                                        DT.Columns.Add(DJD.CorrespondFinancialAccountDimId.Code);
                                }
                                catch { continue; }
                            }
                        }
                    }
                }
                return DT;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region DataTable get all XPCollection
        public DataTable DT_Xp_all(int FinancialAccountDimId, int MonthDimId, int YearDimId, Boolean getXP_DiaryJournal_Fact, bool getXp_DiaryJournal_Detail)  //, int OwnerOrgDimId
        {
            try
            {
                DataTable dt = new DataTable();
                XPCollection<DiaryJournal_Fact> DJF_xp = BO.get_xp_DiaryJournal_Fact(session, FinancialAccountDimId, MonthDimId, YearDimId);   //, OwnerOrgDimId
                if (DJF_xp != null && getXP_DiaryJournal_Fact == true)
                {
                    foreach (DiaryJournal_Fact DJF in DJF_xp)
                    {
                        #region DiaryJournal_Detail
                        XPCollection<DiaryJournal_Detail> DJD_xp = BO.get_Xp_DiaryJournal_Detail(session, FinancialAccountDimId, DJF.DiaryJournal_FactId);
                        if (DJD_xp != null && getXp_DiaryJournal_Detail == true)
                        {
                            foreach (DiaryJournal_Detail DJD in DJD_xp)
                            {
                                dt.Columns.Add(DJD.DiaryJournal_DetailId.ToString());
                            }
                        }
                        #endregion
                        else if (getXp_DiaryJournal_Detail == false)
                        {
                            try { dt.Columns.Add(DJF.DiaryJournal_FactId.ToString()); }
                            catch { continue; }
                        }
                    }
                }
                else if (getXP_DiaryJournal_Fact == false) return null;
                return dt;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region tao DataTable Row SoTT
        public DataTable DT_STT(DataTable dt)
        {
            try
            {
                int stt = 1;

                DataRow dr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    dr[dc.ColumnName] = stt++;
                }
                dt.Rows.Add(dr);
                return dt;
            }
            catch (Exception) { throw; }
        }
        #endregion

        public DataTable DT_xp_FT(int FinancialAccountDimId, int DiaryJournal_FactId)
        {
            try
            {
                DataTable dt = new DataTable();
                XPCollection<DiaryJournal_Detail> DJD_xp = BO.get_Xp_DiaryJournal_Detail(session, FinancialAccountDimId, DiaryJournal_FactId);
                if (DJD_xp != null)
                {
                    foreach (DiaryJournal_Detail djd in DJD_xp)
                    {
                        try
                        {
                            dt.Columns.Add(djd.FinancialTransactionDimId.FinancialTransactionDimId.ToString());
                        }
                        catch { continue; }
                    }
                }
                return dt;
            }
            catch (Exception) { return null; }
        }

        #region tao DT_getvalue lay du lieu tung transaction & Tai khoan co - no
        public DataTable DT_getvalue(int FinancialAccountDimId, int MonthDimId, int YearDimId) //, int OwnerOrgDimId
        {
            try
            {
                double stt = 1;
                FinancialAccountDim fad = BO.get_FinancialAccountDim_1(session, FinancialAccountDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                DataTable dt = DT_Header(FinancialAccountDimId, MonthDimId, YearDimId); //, OwnerOrgDimId
                DataTable dt_xp_all = DT_Xp_all(FinancialAccountDimId, MonthDimId, YearDimId, true, false); //, OwnerOrgDimId

                if (dt_xp_all != null && dt != null)
                {
                    foreach (DataColumn dc_all in dt_xp_all.Columns)
                    {
                        DataTable DT_xp_ft = DT_xp_FT(FinancialAccountDimId, int.Parse(dc_all.ColumnName.ToString()));


                        if (DT_xp_ft != null)
                        {
                            foreach (DataColumn dtxpft in DT_xp_ft.Columns)
                            {
                                XPCollection<DiaryJournal_Detail> DJD_FT = BO.get_xp_DiaryJournal_Detail_4(session, int.Parse(dtxpft.ColumnName.ToString()), int.Parse(dc_all.ColumnName.ToString()));
                                if (DJD_FT != null)
                                {
                                    DataRow dr = dt.NewRow();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        int flagACC = 0;
                                        foreach (DiaryJournal_Detail DJD in DJD_FT)
                                        {
                                            #region ghi tk no 331
                                            if (dc.ColumnName.Equals("STT")) { dr[dc.ColumnName] = stt; }
                                            if (dc.ColumnName.Equals("sohieu")) { if (DJD.Credit > 0 && DJD.FinancialAccountDimId != null) { dr[dc.ColumnName] = DJD.FinancialTransactionDimId.Name; } }
                                            if (dc.ColumnName.Equals("ngaythang")) { if (DJD.Credit > 0 && DJD.FinancialAccountDimId != null) { dr[dc.ColumnName] = String.Format("{0:d}", DJD.FinancialTransactionDimId.IssueDate); } }
                                            if (dc.ColumnName.Equals("diengiai")) { if (DJD.Credit > 0 && DJD.FinancialAccountDimId != null) { dr[dc.ColumnName] = DJD.FinancialTransactionDimId.Description; } }
                                            try
                                            {
                                                { if (dc.ColumnName.Equals(DJD.CorrespondFinancialAccountDimId.Code)) { if (DJD.Debit > 0 && DJD.CorrespondFinancialAccountDimId != null) dr[dc.ColumnName] = SumAcc(session, DJD.CorrespondFinancialAccountDimId.Code, DJD.FinancialTransactionDimId.FinancialTransactionDimId, DJD.DiaryJournal_FactId.DiaryJournal_FactId, false); flagACC++; } }
                                            }
                                            catch { }
                                            if (dc.ColumnName.Equals("congco")) { if (DJD.Credit > 0 && DJD.CorrespondFinancialAccountDimId == null) { dr[dc.ColumnName] = SumAcc(session, fad.Code, DJD.FinancialTransactionDimId.FinancialTransactionDimId, DJD.DiaryJournal_FactId.DiaryJournal_FactId, true); } }
                                            #endregion

                                            #region ghi tk co 331
                                            if (dc.ColumnName.Equals("STT_L")) { dr[dc.ColumnName] = stt; }
                                            if (dc.ColumnName.Equals("sohieu_L")) { if (DJD.Debit > 0 && DJD.FinancialAccountDimId != null) { dr[dc.ColumnName] = DJD.FinancialTransactionDimId.Name; } }
                                            if (dc.ColumnName.Equals("ngaythang_L")) { if (DJD.Debit > 0 && DJD.FinancialAccountDimId != null) { dr[dc.ColumnName] = String.Format("{0:d}", DJD.FinancialTransactionDimId.IssueDate); } }
                                            try
                                            {
                                                if (dc.ColumnName.ToString().Replace("_L", "").Equals(DJD.CorrespondFinancialAccountDimId.Code))
                                                {
                                                    int flag = 0;
                                                    XPCollection<DiaryJournal_Detail> DJD_xp = BO.get_Xp_DiaryJournal_Detail_7(session, DJD.CorrespondFinancialAccountDimId.CorrespondFinancialAccountDimId, DJD.DiaryJournal_FactId.DiaryJournal_FactId);
                                                    if (DJD_xp != null)
                                                    {
                                                        if (DJD_xp.Count > 1) { foreach (DiaryJournal_Detail djd in DJD_xp) { flag++; } }

                                                        if (DJD.Credit > 0 && DJD.CorrespondFinancialAccountDimId != null && flag > 1)
                                                        {
                                                            for (int b = 1; b <= flag; b++)
                                                            {
                                                                if (b > 1 && flagACC == 0) { dr[dc.ColumnName] = SumAcc(session, DJD.CorrespondFinancialAccountDimId.Code, DJD.FinancialTransactionDimId.FinancialTransactionDimId, DJD.DiaryJournal_FactId.DiaryJournal_FactId, true); }
                                                            }
                                                        }

                                                        if (DJD_xp.Count == 1 && DJD.Credit > 0 && DJD.CorrespondFinancialAccountDimId != null)
                                                        {
                                                            dr[dc.ColumnName] = SumAcc(session, DJD.CorrespondFinancialAccountDimId.Code, DJD.FinancialTransactionDimId.FinancialTransactionDimId, DJD.DiaryJournal_FactId.DiaryJournal_FactId, true);
                                                        }
                                                    }
                                                }
                                            }
                                            catch { }
                                            if (dc.ColumnName.Equals("congno")) { if (DJD.Debit > 0 && DJD.CorrespondFinancialAccountDimId == null) { dr[dc.ColumnName] = SumAcc(session, fad.Code, DJD.FinancialTransactionDimId.FinancialTransactionDimId, DJD.DiaryJournal_FactId.DiaryJournal_FactId, false); } }
                                            #endregion
                                        }
                                    }
                                    dt.Rows.Add(dr);
                                    stt++;
                                }
                            }
                        }
                    }
                }
                return dt;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region dua DT_getvalue du lieu vao dong bo voi column gridview
        public DataTable DT_rowgetvalue(DataTable dt, int FinancialAccountDimId, int MonthDimId, int YearDimId) //, int OwnerOrgDimId
        {
            try
            {
                int s = 0, r = 0;
                DataTable dt_getvalue = DT_getvalue(FinancialAccountDimId, MonthDimId, YearDimId); //, OwnerOrgDimId

                if (dt_getvalue != null)
                {
                    foreach (DataRow dr in dt_getvalue.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["STT"] = dr["STT"];
                        row["sohieu"] = dr["sohieu"];
                        row["ngaythang"] = dr["ngaythang"];
                        row["diengiai"] = dr["diengiai"];
                        foreach (DataColumn dc in DT_ListAcc_CreditOrDebit(FinancialAccountDimId, MonthDimId, YearDimId, false).Columns) //, OwnerOrgDimId
                        {
                            row[dc.ColumnName] = dr[dc.ColumnName];
                            if (r == 0) s++;
                        }
                        row["congco"] = dr["congco"];
                        row["STT_L"] = dr["STT_L"];
                        row["sohieu_L"] = dr["sohieu_L"];
                        row["ngaythang_L"] = dr["ngaythang_L"];
                        foreach (DataColumn dc in DT_ListAcc_CreditOrDebit(FinancialAccountDimId, MonthDimId, YearDimId, true).Columns)//, OwnerOrgDimId
                        {
                            row[dc.ColumnName + "_L"] = dr[dc.ColumnName + "_L"];
                        }
                        row["congno"] = dr["congno"];
                        dt.Rows.Add(row);
                        r++;
                    }
                }
                dt.Rows.Add(SumlTotal_column(dt, s));
                return dt;
            }
            catch (Exception) { return null; }
        }
        #endregion

        private double SumAcc(Session session, string CorrANDFnanicialACC, int FinancialTransactionDimId, int DiaryJournal_FactId, bool CreditOrDebit)
        {
            double sum = 0;
            if (!CorrANDFnanicialACC.Equals("311")
               || !CorrANDFnanicialACC.Equals("315")
               || !CorrANDFnanicialACC.Equals("341")
               || !CorrANDFnanicialACC.Equals("342")
               || !CorrANDFnanicialACC.Equals("343"))
            {
                CorrespondFinancialAccountDim corr = BO.get_CorrespondFinancialAccountDim(session, CorrANDFnanicialACC, Utility.Constant.ROWSTATUS_ACTIVE);
                if (corr != null)
                    if (corr != null)
                    {
                        XPCollection<DiaryJournal_Detail> DJD = BO.get_Xp_DiaryJournal_Detail_6(session, corr.CorrespondFinancialAccountDimId, FinancialTransactionDimId, DiaryJournal_FactId);
                        if (DJD != null)
                        {
                            foreach (DiaryJournal_Detail djd in DJD)
                            {
                                if (CreditOrDebit == true && djd.Credit > 0)
                                    sum += djd.Credit;
                                else if (CreditOrDebit == false && djd.Debit > 0)
                                    sum += djd.Debit;
                            }
                        }
                    }
            }
            if (CorrANDFnanicialACC.Equals("311")
                || CorrANDFnanicialACC.Equals("315")
                || CorrANDFnanicialACC.Equals("341")
                || CorrANDFnanicialACC.Equals("342")
                || CorrANDFnanicialACC.Equals("343"))
            {
                FinancialAccountDim FAD = BO.get_FinancialAccountDim(session, CorrANDFnanicialACC, Utility.Constant.ROWSTATUS_ACTIVE);

                {
                    XPCollection<DiaryJournal_Detail> DJD = BO.get_Xp_DiaryJournal_Detail_5(session, FAD.FinancialAccountDimId, FinancialTransactionDimId, DiaryJournal_FactId);
                    if (DJD != null)
                    {
                        foreach (DiaryJournal_Detail djd in DJD)
                        {
                            if (CreditOrDebit == true && djd.Credit > 0)
                                sum += djd.Credit;
                            else if (CreditOrDebit == false && djd.Debit > 0)
                                sum += djd.Debit;
                        }
                    }
                }
            }

            return sum;
        }

        private DataRow SumlTotal_column(DataTable datatable, int s)
        {
            try
            {
                DataRow row1 = datatable.NewRow();
                row1["diengiai"] = "Tổng Cộng";

                int column_count = datatable.Columns.Count - 1;
                int row_count = datatable.Rows.Count - 1;
                for (int c = 4; c <= column_count; c++)
                {
                    if (c != s + 4 + 1)
                        if (c != s + 4 + 2)
                            if (c != s + 4 + 3)
                            {
                                double sumT = 0;
                                for (int r = 1; r <= row_count; r++)
                                {
                                    if (datatable.Rows[r][c].ToString() != null && !datatable.Rows[r][c].ToString().Equals(""))
                                    {
                                        double tt = double.Parse(datatable.Rows[r][c].ToString());
                                        sumT += tt;
                                    }
                                }
                                row1[datatable.Columns[c]] = sumT;
                            }
                }
                return row1;
            }
            catch (Exception) { return null; }
        }
    }
}