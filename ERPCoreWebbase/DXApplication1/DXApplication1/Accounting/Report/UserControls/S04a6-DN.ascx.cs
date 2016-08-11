using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using NAS.DAL.BI.Accounting.Account;
using System.Data;
using NAS.DAL.BI.Actor;
using NAS.BO.Accounting.Report;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using DevExpress.XtraPrintingLinks;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a6_DN : System.Web.UI.UserControl
    {
        Session session = XpoHelper.GetNewSession();
        BO_S04a6_dn BO = new BO_S04a6_dn();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (hS04a6dn.Contains("show"))
                LoadData();
        }

        public void LoadData()
        {
            WebModule.Accounting.Report.S04a6_DN s04a6_dn = new WebModule.Accounting.Report.S04a6_DN();
            string FinancialAccountDim_Code = "151";
            int MonthDim = Int32.Parse(this.hs04a6dnMonth.Get("month_Name").ToString());
            int YearDim = Int32.Parse(this.hs04a6dnYear.Get("year_Name").ToString());
            string OwnerOrgDim = "QUASAPHARCO";

            #region get value
            FinancialAccountDim FAD = BO.get_FinancialAccountDim(session, FinancialAccountDim_Code, Utility.Constant.ROWSTATUS_ACTIVE);

            MonthDim MD = BO.get_MonthDimId(session, MonthDim.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

            YearDim YD = BO.get_YearDimId(session, YearDim.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

            OwnerOrgDim OOD = BO.get_OwnerOrgDimId(session, OwnerOrgDim, Utility.Constant.ROWSTATUS_ACTIVE);

            if (FAD == null || MD == null || YD == null || OOD == null) return;
            #endregion

            s04a6_dn.lblMonth.Text = MD.Name;
            s04a6_dn.lblYear.Text = YD.Name;

            try
            {
                #region tao cac cot header table
                Gridview_header(FAD.FinancialAccountDimId, OOD.OwnerOrgDimId, MD.MonthDimId, YD.YearDimId);
                DataTable dt = DT_Header(FAD.FinancialAccountDimId, OOD.OwnerOrgDimId, MD.MonthDimId, YD.YearDimId);
                #endregion

                #region tao dong STT ngang cho tung cot
                DT_STT(dt);
                #endregion

                #region do du lieu vao DataTable
                DT_getRowValue(dt, FAD.FinancialAccountDimId, OOD.OwnerOrgDimId, MD.MonthDimId, YD.YearDimId);
                #endregion

                #region BindData vao GridView
                xGridView.DataSource = dt;
                xGridView.DataBind();
                #endregion
            }
            catch (Exception) { }

            #region xuat report
            GridViewExporter.GridViewID = "xGridView";
            s04a6_dn.printableCC.PrintableComponent = new PrintableComponentLinkBase() { Component = GridViewExporter };
            ReportViewers04a6.Report = s04a6_dn;
            #endregion

        }

        public void Gridview_header(int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId)
        {
            try
            {
                GridViewColumn column = new GridViewBandColumn();
                GridViewBandColumn bandColumn;
                GridViewDataTextColumn GVTC;
                FinancialAccountDim FAD = session.GetObjectByKey<FinancialAccountDim>(FinancialAccountDimId);
                if (FAD != null)
                {
                    GVTC = new GridViewDataTextColumn { FieldName = "STT", Caption = "Số TT" };
                    xGridView.Columns.Add(GVTC);

                    GVTC = new GridViewDataTextColumn { FieldName = "Description", Caption = "Diễn giải" };
                    xGridView.Columns.Add(GVTC);

                    GVTC = new GridViewDataTextColumn { FieldName = "BeginBalance", Caption = "Số dư đầu tháng" };
                    GVTC.PropertiesEdit.DisplayFormatString = ("#,#");
                    xGridView.Columns.Add(GVTC);

                    #region column hoa don
                    bandColumn = new GridViewBandColumn("Hóa đơn");
                    GVTC = new GridViewDataTextColumn { FieldName = "LegalInvoiceCode", Caption = "Số hiệu" };
                    bandColumn.Columns.Add(GVTC);
                    GVTC = new GridViewDataTextColumn { FieldName = "LegalInvoiceIssuedDate", Caption = "Ngày Tháng" };
                    bandColumn.Columns.Add(GVTC);
                    xGridView.Columns.Add(bandColumn);
                    #endregion

                    #region column phieu nhap
                    bandColumn = new GridViewBandColumn("Phiếu nhập");
                    GVTC = new GridViewDataTextColumn { FieldName = "InvoiceCode", Caption = "Số hiệu" };
                    bandColumn.Columns.Add(GVTC);
                    GVTC = new GridViewDataTextColumn { FieldName = "InvoiceIssuedDate", Caption = "Ngày Tháng" };
                    bandColumn.Columns.Add(GVTC);
                    xGridView.Columns.Add(bandColumn);
                    #endregion

                    #region danh sach cot tai khoan ghi no
                    bandColumn = new GridViewBandColumn("Ghi Có TK " + FAD.Code + " ghi Nợ các tài khoản");

                    GridViewBandColumn band_Column;
                    foreach (DataColumn dc in DT_Credit151(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId).Columns)
                    {

                        switch (dc.ColumnName.Substring(0, 3))
                        {
                            case "152":
                                band_Column = new GridViewBandColumn(dc.ColumnName);
                                GVTC = new GridViewDataTextColumn { FieldName = "GTT", Caption = "Giá TT" };
                                GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                                band_Column.Columns.Add(GVTC);
                                GVTC = new GridViewDataTextColumn { FieldName = "GHT", Caption = "Giá HT" };
                                GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                                band_Column.Columns.Add(GVTC);
                                bandColumn.Columns.Add(band_Column);
                                break;
                            case "153":
                                band_Column = new GridViewBandColumn(dc.ColumnName);
                                GVTC = new GridViewDataTextColumn { FieldName = "GTT", Caption = "Giá TT" };
                                GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                                band_Column.Columns.Add(GVTC);
                                GVTC = new GridViewDataTextColumn { FieldName = "GHT", Caption = "Giá HT" };
                                GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                                band_Column.Columns.Add(GVTC);
                                bandColumn.Columns.Add(band_Column);
                                break;
                            default:
                                if (!dc.ColumnName.Equals("NAAN_DEFAULT"))
                                {
                                    GVTC = new GridViewDataTextColumn { FieldName = dc.ColumnName };
                                    GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                                    bandColumn.Columns.Add(GVTC);
                                }
                                break;
                        }
                    }

                    GVTC = new GridViewDataTextColumn { FieldName = "congco", Caption = "Cộng Có Tk " + FAD.Code };
                    GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                    bandColumn.Columns.Add(GVTC);
                    xGridView.Columns.Add(bandColumn);
                    #endregion

                    GVTC = new GridViewDataTextColumn { FieldName = "EndBalance", Caption = "Số dư cuối tháng" };
                    GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                    xGridView.Columns.Add(GVTC);
                }
            }
            catch (Exception) { throw; }
        }

        public DataTable DT_Header(int FinancialAccountDimId, int OwnerOrgDimId, int MonthdDimId, int YearDimId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("STT", typeof(double));
                dt.Columns.Add("Description");
                dt.Columns.Add("BeginBalance", typeof(double));
                dt.Columns.Add("LegalInvoiceCode");
                dt.Columns.Add("LegalInvoiceIssuedDate");
                dt.Columns.Add("InvoiceCode");
                dt.Columns.Add("InvoiceIssuedDate");

                foreach (DataColumn dc in DT_Credit151(FinancialAccountDimId, OwnerOrgDimId, MonthdDimId, YearDimId).Columns)
                {
                    try
                    {
                        switch (dc.ColumnName.Substring(0, 3))
                        {
                            case "152":
                                dt.Columns.Add("GTT", typeof(double));
                                dt.Columns.Add("GHT", typeof(double));
                                break;
                            case "153":
                                dt.Columns.Add("GTT", typeof(double));
                                dt.Columns.Add("GHT", typeof(double));
                                break;
                            default:
                                if (!dc.ColumnName.Equals("NAAN_DEFAULT"))
                                {
                                    dt.Columns.Add(dc.ColumnName, typeof(double));
                                }
                                break;
                        }
                    }
                    catch { continue; }
                }

                dt.Columns.Add("congco", typeof(double));
                dt.Columns.Add("EndBalance", typeof(double));

                return dt;
            }
            catch (Exception) { return null; }
        }

        #region DataTable cac tai khoan no cua 151
        private DataTable DT_Credit151(int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId)
        {
            try
            {
                DataTable dt_debit = new DataTable();

                XPCollection<FinancialOnTheWayBuyingGoodSummary> FOTW = BO.get_xp_FinancialOnTheWayBuyingGoodSummary(session, FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId);
                if (FOTW != null && FOTW.Count > 0)
                {
                    foreach (FinancialOnTheWayBuyingGoodSummary fotw1 in FOTW)
                    {
                        XPCollection<OnTheWayBuyingGoodArtifact> OTWB = BO.get_xp_OnTheWayBuyingGoodArtifact(session, fotw1.FinancialOnTheWayBuyingGoodSummaryId, Utility.Constant.ROWSTATUS_ACTIVE);
                        if (OTWB != null)
                        {
                            foreach (OnTheWayBuyingGoodArtifact otwb1 in OTWB)
                            {
                                XPCollection<FinancialOnTheWayBuyingGoodDetail> FOTWBGD = BO.get_xp_FinancialOnTheWayBuyingGoodDetail(session, FinancialAccountDimId, otwb1.OnTheWayBuyingGoodArtifactId, Utility.Constant.ROWSTATUS_ACTIVE);
                                if (FOTWBGD != null)
                                {
                                    foreach (FinancialOnTheWayBuyingGoodDetail fotwbgd1 in FOTWBGD)
                                    {
                                        CorrespondFinancialAccountDim CFAD = BO.get_CorrespondFinancialAccountDim(session, fotwbgd1.CorrespondFinancialAccountDimId.CorrespondFinancialAccountDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                                        if (CFAD != null)
                                        {
                                            try
                                            {
                                                dt_debit.Columns.Add(CFAD.Code);
                                            }
                                            catch { continue; }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return dt_debit;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region DataTable get all xpcollection table
        public DataTable DT_get_xp_AllId(int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId, bool getXp_FinancialOnTheWayBuyingGoodSummary, bool getXp_OnTheWayBuyingGoodArtifact, bool getXp_FinancialOnTheWayBuyingSummaryDetail, bool get_CrossepondFinancialAccountDim)
        {
            try
            {
                DataTable dt = new DataTable();

                XPCollection<FinancialOnTheWayBuyingGoodSummary> FOTW = BO.get_xp_FinancialOnTheWayBuyingGoodSummary(session, FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId);
                if (FOTW != null && getXp_FinancialOnTheWayBuyingGoodSummary == true)
                {
                    foreach (FinancialOnTheWayBuyingGoodSummary fotw in FOTW)
                    {
                        #region OnTheWayBuyingGoodArtifact
                        XPCollection<OnTheWayBuyingGoodArtifact> OTWB = BO.get_xp_OnTheWayBuyingGoodArtifact(session, fotw.FinancialOnTheWayBuyingGoodSummaryId, Utility.Constant.ROWSTATUS_ACTIVE);
                        if (OTWB != null && getXp_OnTheWayBuyingGoodArtifact == true)
                        {
                            foreach (OnTheWayBuyingGoodArtifact otwb in OTWB)
                            {
                                #region FinancialOnTheWayBuyingGoodDetail
                                XPCollection<FinancialOnTheWayBuyingGoodDetail> FOTWBGD = BO.get_xp_FinancialOnTheWayBuyingGoodDetail(session, FinancialAccountDimId, otwb.OnTheWayBuyingGoodArtifactId, Utility.Constant.ROWSTATUS_ACTIVE);
                                if (FOTWBGD != null && getXp_FinancialOnTheWayBuyingSummaryDetail == true)
                                {
                                    foreach (FinancialOnTheWayBuyingGoodDetail fotwbgd in FOTWBGD)
                                    {
                                        #region CorrespondFinancialAccountDim
                                        CorrespondFinancialAccountDim CFAD = BO.get_CorrespondFinancialAccountDim(session, fotwbgd.CorrespondFinancialAccountDimId.CorrespondFinancialAccountDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                                        if (CFAD != null && get_CrossepondFinancialAccountDim == true)
                                        {
                                            try { dt.Columns.Add(CFAD.CorrespondFinancialAccountDimId.ToString()); }
                                            catch { continue; }
                                        }
                                        #endregion
                                        else if (get_CrossepondFinancialAccountDim == false)
                                        {
                                            try { dt.Columns.Add(fotwbgd.FinancialOnTheWayBuyingGoodDetailId.ToString()); }
                                            catch { continue; }
                                        }
                                    }
                                }
                                #endregion
                                else if (getXp_FinancialOnTheWayBuyingSummaryDetail == false)
                                {
                                    try { dt.Columns.Add(otwb.OnTheWayBuyingGoodArtifactId.ToString()); }
                                    catch { continue; }
                                }
                            }
                        }
                        #endregion
                        else if (getXp_FinancialOnTheWayBuyingGoodSummary == true)
                        {
                            try { dt.Columns.Add(fotw.FinancialOnTheWayBuyingGoodSummaryId.ToString()); }
                            catch { continue; }
                        }
                    }
                }
                else { return null; }

                return dt;
            }
            catch (Exception) { throw; }
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

        public DataTable DT_getValue(int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId)
        {
            try
            {
                int stt = 2;
                DataTable dt = new DataTable();
                dt.Columns.Add("STT");
                dt.Columns.Add("Description");
                dt.Columns.Add("BeginBalance");
                dt.Columns.Add("LegalInvoiceCode");
                dt.Columns.Add("LegalInvoiceIssuedDate");
                dt.Columns.Add("InvoiceCode");
                dt.Columns.Add("InvoiceIssuedDate");

                #region cac cot tai khoan no 151
                foreach (DataColumn dt_all in DT_get_xp_AllId(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId, true, true, true, false).Columns)
                {
                    FinancialOnTheWayBuyingGoodDetail FTBGD = BO.get_FinancialOnTheWayBuyingGoodDetail(session, Guid.Parse(dt_all.ColumnName.ToString()), Utility.Constant.ROWSTATUS_ACTIVE);
                    if (FTBGD != null)
                    {
                        CorrespondFinancialAccountDim CFAD = BO.get_CorrespondFinancialAccountDim(session, FTBGD.CorrespondFinancialAccountDimId.CorrespondFinancialAccountDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                        if (CFAD != null && FTBGD.Debit > 0)
                        {
                            try
                            {
                                if (CFAD.Code.Equals("152"))
                                {
                                    dt.Columns.Add("152ActualPrice");
                                    dt.Columns.Add("152BookingPrice");
                                }
                                else if (CFAD.Code.Equals("153"))
                                {
                                    dt.Columns.Add("153ActualPrice");
                                    dt.Columns.Add("153BookingPrice");
                                }
                                else
                                {
                                    if (!CFAD.Code.Equals("NAAN_DEFAULT"))
                                        dt.Columns.Add(CFAD.Code);
                                }
                            }
                            catch { continue; }
                        }
                    }
                }
                dt.Columns.Add("congco");
                dt.Columns.Add("EndBalance");
                #endregion

                #region load money Debit vao tung tai khoan - tug Row
                DataRow dr = dt.NewRow();
                #region gan 1 lan BeginBalance
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.Equals("STT")) { dr[dc.ColumnName] = stt++; }
                    foreach (DataColumn dt_all in DT_get_xp_AllId(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId, true, false, false, false).Columns)
                    {
                        FinancialOnTheWayBuyingGoodSummary fotwbgosu = BO.get_FinancialOntheWayBuyingGoodSummary(session, Guid.Parse(dt_all.ToString()),Utility.Constant.ROWSTATUS_ACTIVE);
                        if (dc.ColumnName.Equals("BeginBalance")) { dr[dc.ColumnName] = fotwbgosu.BeginBalance; }
                    }
                }
                dt.Rows.Add(dr);
                #endregion

                #region gan cac tai khoan no cua 151
                foreach (DataColumn dt_all in DT_get_xp_AllId(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId, true, true, false, false).Columns)
                {
                    OnTheWayBuyingGoodArtifact OTWGA = BO.get_OnTheWayBuyingGoodArtifact(session, Guid.Parse(dt_all.ColumnName.ToString()), Utility.Constant.ROWSTATUS_ACTIVE);
                    if (OTWGA != null)
                    {
                        try
                        {
                            dr = dt.NewRow();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                #region lay thong tin chi tiet
                                if (dc.ColumnName.Equals("STT")) { dr[dc.ColumnName] = stt++; }
                                if (dc.ColumnName.Equals("Description")) { dr[dc.ColumnName] = OTWGA.Description; }

                                if (dc.ColumnName.Equals("LegalInvoiceCode")) { dr[dc.ColumnName] = OTWGA.LegalInvoiceCode; }
                                if (dc.ColumnName.Equals("LegalInvoiceIssuedDate")) { dr[dc.ColumnName] = String.Format("{0:d}", OTWGA.LegalInvoiceIssuedDate); }
                                if (dc.ColumnName.Equals("InvoiceCode")) { dr[dc.ColumnName] = OTWGA.InvoiceCode; }
                                if (dc.ColumnName.Equals("InvoiceIssuedDate")) { dr[dc.ColumnName] = String.Format("{0:d}", OTWGA.InvoiceIssuedDate); }
                                XPCollection<FinancialOnTheWayBuyingGoodDetail> fotwbgd = BO.get_xp_FinancialOnTheWayBuyingGoodDetail(session, FinancialAccountDimId, OTWGA.OnTheWayBuyingGoodArtifactId, Utility.Constant.ROWSTATUS_ACTIVE);
                                if (fotwbgd != null)
                                {
                                    foreach (FinancialOnTheWayBuyingGoodDetail ftd in fotwbgd)
                                    {
                                        if (dc.ColumnName.Equals("152ActualPrice")) { if (ftd.CorrespondFinancialAccountDimId.Code.Equals("152")) { if (ftd.Debit > 0) dr[dc.ColumnName] = SumAcc(session, ftd.CorrespondFinancialAccountDimId.Code, OTWGA.OnTheWayBuyingGoodArtifactId, false); } }
                                        if (dc.ColumnName.Equals("152BookingPrice")) { if (ftd.CorrespondFinancialAccountDimId.Code.Equals("152")) { dr[dc.ColumnName] = 0; } }
                                        if (dc.ColumnName.Equals("153ActualPrice")) { if (ftd.CorrespondFinancialAccountDimId.Code.Equals("153")) { if (ftd.Debit > 0) dr[dc.ColumnName] = SumAcc(session, ftd.CorrespondFinancialAccountDimId.Code, OTWGA.OnTheWayBuyingGoodArtifactId, false); } }
                                        if (dc.ColumnName.Equals("153BookingPrice")) { if (ftd.CorrespondFinancialAccountDimId.Code.Equals("153")) { dr[dc.ColumnName] = 0; } }
                                        if (!dc.ColumnName.Equals("NAAN_DEFAULT")) { if (ftd.CorrespondFinancialAccountDimId.Code.Equals(dc.ColumnName)) { if (ftd.Debit > 0) { dr[dc.ColumnName] = SumAcc(session, dc.ColumnName, OTWGA.OnTheWayBuyingGoodArtifactId, false); } } }
                                        
                                    }
                                    if (dc.ColumnName.Equals("congco")) { dr[dc.ColumnName] = SumAcc(session, "NAAN_DEFAULT", OTWGA.OnTheWayBuyingGoodArtifactId, true); }
                                }
                                
                                #endregion
                            }
                            dt.Rows.Add(dr);
                        }
                        catch { continue; }
                    }
                }
                #endregion

                #region gan 1 lan EndBanlance
                dr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.Equals("STT")) { dr[dc.ColumnName] = stt++; }
                    foreach (DataColumn dt_all in DT_get_xp_AllId(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId, true, false, false, false).Columns)
                    {
                        FinancialOnTheWayBuyingGoodSummary fotwbgosu = BO.get_FinancialOntheWayBuyingGoodSummary(session, Guid.Parse(dt_all.ToString()), Utility.Constant.ROWSTATUS_ACTIVE);
                        if (dc.ColumnName.Equals("EndBalance")) { dr[dc.ColumnName] = fotwbgosu.EndBalance; }
                    }
                }
                dt.Rows.Add(dr);
                #endregion

                #endregion
                return dt;
            }
            catch (Exception) { throw; }
        }

        #region DataTable dua du lieu vao tung dong
        public DataTable DT_getRowValue(DataTable dt, int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId)
        {
            try
            {
                double row_sum = 0;
                DataTable dt_left = DT_getValue(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId);
                foreach (DataRow dr_left in dt_left.Rows)
                {
                    DataRow row = dt.NewRow();
                    row["STT"] = dr_left["STT"].ToString(); ;
                    row["Description"] = dr_left["Description"].ToString();
                    row["BeginBalance"] = dr_left["BeginBalance"];
                    row["LegalInvoiceCode"] = dr_left["LegalInvoiceCode"].ToString();
                    row["LegalInvoiceIssuedDate"] = dr_left["LegalInvoiceIssuedDate"].ToString();
                    row["InvoiceCode"] = dr_left["InvoiceCode"].ToString();
                    row["InvoiceIssuedDate"] = dr_left["InvoiceIssuedDate"].ToString();

                    #region cac tai khoan no 151
                    foreach (DataColumn dt_all in DT_get_xp_AllId(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId, true, true, true, true).Columns)
                    {
                        CorrespondFinancialAccountDim cfad = BO.get_CorrespondFinancialAccountDim(session, int.Parse(dt_all.ColumnName.ToString()), Utility.Constant.ROWSTATUS_ACTIVE);
                        if (cfad != null)
                        {
                            if (cfad.Code.Equals("152"))
                            {
                                row["GTT"] = dr_left["152ActualPrice"];
                                row["GHT"] = dr_left["152BookingPrice"];
                            }
                            else if (cfad.Code.Equals("153"))
                            {
                                row["GTT"] = dr_left["153ActualPrice"];
                                row["GHT"] = dr_left["153BookingPrice"];
                            }
                            else
                            {
                                if (!cfad.Code.Equals("NAAN_DEFAULT")) { row[cfad.Code] = dr_left[cfad.Code]; }
                            }
                        }
                    }
                    #endregion
                    row["congco"] = dr_left["congco"];
                    row["EndBalance"] = dr_left["EndBalance"];
                    dt.Rows.Add(row);
                }
                dt.Rows.Add(SumlTotal_column(dt));
                return dt;
            }
            catch (Exception) { throw; }
        }
        #endregion

        private DataRow SumlTotal_column(DataTable datatable)
        {
            try
            {
                DataRow row1 = datatable.NewRow();
                row1["Description"] = "Tổng Cộng";

                int column_count = datatable.Columns.Count - 1;
                int row_count = datatable.Rows.Count - 1;
                for (int c = 2; c <= column_count; c++)
                {
                    if (c != 3)
                        if (c != 4)
                            if (c != 5)
                                if (c != 6)
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

        private double SumAcc(Session session,string CorrespondFinancialAccountDim_Code, Guid OnTheWayBuyingGoodArtifactId, bool Credit_Debit)
        {
            double sum = 0;
            CorrespondFinancialAccountDim corr = BO.get_CorrespondFinancialAccountDim1(session, CorrespondFinancialAccountDim_Code, Utility.Constant.ROWSTATUS_ACTIVE);
            if (corr != null)
            {
                XPCollection<FinancialOnTheWayBuyingGoodDetail> FOTWB = BO.get_xp_FinancialOnTheWayBuyingGoodDetail1(session, corr.CorrespondFinancialAccountDimId, OnTheWayBuyingGoodArtifactId, Utility.Constant.ROWSTATUS_ACTIVE);
                if (FOTWB != null)
                {
                    foreach (FinancialOnTheWayBuyingGoodDetail fotwb in FOTWB)
                    {
                        if (Credit_Debit == true && fotwb.Credit>0) 
                            sum += fotwb.Credit;
                        else if( Credit_Debit == false && fotwb.Debit>0)
                            sum += fotwb.Debit;
                    }
                }
            }
            return sum;
        }

        protected void GridViewExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            GridViewDataColumn datacolumn = e.Column as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data && datacolumn.FieldName.Equals("BeginBalance"))
            {
                object value = e.GetValue("BeginBalance");
                if (value !=null && value is double)
                {
                    if ((double)value == 0)
                        e.Text = "0";
                }
            }
            if (e.RowType == GridViewRowType.Data && datacolumn.FieldName.Equals("EndBalance"))
            {
                object value = e.GetValue("EndBalance");
                if (value != null && value is double)
                {
                    if ((double)value == 0)
                        e.Text = "0";
                }
            }
        }
    }
}