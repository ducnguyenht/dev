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
using DevExpress.Web.ASPxGridView;
using System.Data;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.BO.Accounting.Report;
using NAS.DAL.BI.Accounting.CustomerLiability;
using NAS.DAL.BI.Actor;
using DevExpress.XtraPrintingLinks;
using System.Globalization;


namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04b11_DN : System.Web.UI.UserControl
    {
        Session session;
        FinancialCustomerLiabilitySummary_FactBO BO = new FinancialCustomerLiabilitySummary_FactBO();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hs04b11dn.Contains("show"))
                LoadData();

        }

        public void LoadData()
        {
            WebModule.Accounting.Report.S04b11_DN s04b11_dn = new WebModule.Accounting.Report.S04b11_DN();
            try
            {
                string FinancialAccountDim_Code = "131";
                int MonthDim = int.Parse(this.hs04b11dnMonth.Get("month_Name").ToString());
                int YearDim = int.Parse(this.hs04b11dnYear.Get("year_Name").ToString());
                string OwnerOrgDim = "QUASAPHARCO";

                #region get_value
                FinancialAccountDim FAD = BO.get_FinancialAccountDimId(session, FinancialAccountDim_Code, Utility.Constant.ROWSTATUS_ACTIVE);

                MonthDim MD = BO.get_MonthDimId(session, MonthDim.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                YearDim YD = BO.get_YearDimId(session, YearDim.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                OwnerOrgDim OOD = BO.get_OwnerOrgDimId(session, OwnerOrgDim, Utility.Constant.ROWSTATUS_ACTIVE);
                #endregion

                if (MD == null || YD == null || OOD == null) return;

                if (int.Parse(MD.Name.ToString()) < 10)
                    s04b11_dn.lbthang.Text = "0" + MD.Name;
                s04b11_dn.lbthang.Text = MD.Name;
                s04b11_dn.lbnam.Text = YD.Name;


                #region tao header
                gridview_header(FAD.FinancialAccountDimId, OOD.OwnerOrgDimId, MD.MonthDimId, YD.YearDimId);
                DataTable dt = DT_Header(FAD.FinancialAccountDimId, OOD.OwnerOrgDimId, MD.MonthDimId, YD.YearDimId);
                #endregion

                #region dòng STT cột
                DT_STT(dt);
                #endregion

                #region do du lieu
                DT_getRowValue(dt, FAD.FinancialAccountDimId, OOD.OwnerOrgDimId, MD.MonthDimId, YD.YearDimId);
                //DT_getvalue(FAD.FinancialAccountDimId, OOD.OwnerOrgDimId, MD.MonthDimId, YD.YearDimId);
                #endregion

                #region bind data vào gridview
                xGridView.DataSource = dt;
                xGridView.DataBind();
                #endregion

            }
            catch { }

            #region xuất report
            xGridViewExporter.GridViewID = "xGridView";
            s04b11_dn.printableCC.PrintableComponent = new PrintableComponentLinkBase() { Component = xGridViewExporter };
            ReportViewerS04b11.Report = s04b11_dn;
            #endregion
        }

        public void gridview_header(int FinancialAccountDim_Id, int OwnerOrgDim_Id, int month, int year)
        {
            try
            {
                GridViewBandColumn bandColumn;
                GridViewDataTextColumn GVTC;
                GVTC = new GridViewDataTextColumn { FieldName = "STT", Caption = "Số TT" };
                xGridView.Columns.Add(GVTC);

                GVTC = new GridViewDataTextColumn { FieldName = "tennguoimua", Caption = "Tên Người Mua" };
                xGridView.Columns.Add(GVTC);

                GVTC = new GridViewDataTextColumn { FieldName = "sodunodauthang", Caption = "Số Dư Nợ Đầu Tháng" };
                GVTC.PropertiesEdit.DisplayFormatString = ("#,#");
                xGridView.Columns.Add(GVTC);

                #region danh sach cot tai khoan ghi Co
                DataTable dT_get_xp_ALL_C = DT_get_xp_ALL(FinancialAccountDim_Id, OwnerOrgDim_Id, month, year, true, true, true, "Credit");
                if (dT_get_xp_ALL_C != null)
                {
                    bandColumn = new GridViewBandColumn("Ghi Nợ TK 131, ghi Có các TK");
                    foreach (DataColumn dt in dT_get_xp_ALL_C.Columns)
                    {
                        if (!dt.ColumnName.Equals("NAAN_DEFAULT"))
                        {
                            GVTC = new GridViewDataTextColumn { FieldName = dt.ColumnName };
                            GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                            bandColumn.Columns.Add(GVTC);
                        }
                    }
                    GVTC = new GridViewDataTextColumn { FieldName = "congno", Caption = "Cộng Nợ TK 131" };
                    GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                    bandColumn.Columns.Add(GVTC);
                    xGridView.Columns.Add(bandColumn);
                }
                #endregion

                #region danh sach cot tai khoan ghi No
                DataTable dT_get_xp_ALL_D = DT_get_xp_ALL(FinancialAccountDim_Id, OwnerOrgDim_Id, month, year, true, true, true, "Debit");
                if (dT_get_xp_ALL_D != null)
                {
                    bandColumn = new GridViewBandColumn("Ghi Có TK 131, ghi Nợ các TK");
                    foreach (DataColumn dt in dT_get_xp_ALL_D.Columns)
                    {
                        if (!dt.ColumnName.Equals("NAAN_DEFAULT"))
                        {
                            GVTC = new GridViewDataTextColumn { FieldName = dt.ColumnName + "D" };
                            GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                            GVTC.Caption = dt.ColumnName;
                            bandColumn.Columns.Add(GVTC);
                        }
                    }
                    GVTC = new GridViewDataTextColumn { FieldName = "congco", Caption = "Cộng Có TK 131" };
                    GVTC.PropertiesEdit.DisplayFormatString = "#,#";
                    bandColumn.Columns.Add(GVTC);
                    xGridView.Columns.Add(bandColumn);
                }
                #endregion


                GVTC = new GridViewDataTextColumn { FieldName = "sodunocuoithang", Caption = "Số Dư Nợ Cuối Tháng" };
                GVTC.PropertiesEdit.DisplayFormatString = ("#,#");
                xGridView.Columns.Add(GVTC);
            }
            catch (Exception) { }
        }

        public DataTable DT_Header(int FinancialAccountDimId, int OwnerOrgDimId, int month, int year)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("STT", typeof(double));
                dt.Columns.Add("tennguoimua");
                dt.Columns.Add("sodunodauthang", typeof(double));

                #region tai khoan no cua 131
                DataTable dT_get_xp_ALL_C = DT_get_xp_ALL(FinancialAccountDimId, OwnerOrgDimId, month, year, true, true, true, "Credit");
                if (dT_get_xp_ALL_C != null)
                    foreach (DataColumn dc in dT_get_xp_ALL_C.Columns)
                    {
                        if (!dc.ColumnName.Equals("NAAN_DEFAULT"))
                        {
                            try
                            {
                                dt.Columns.Add(dc.ColumnName, typeof(double));
                            }
                            catch { continue; }
                        }
                    }
                #endregion
                dt.Columns.Add("congno", typeof(double));

                #region tai khoan co cua 131
                DataTable dT_get_xp_ALL_D = DT_get_xp_ALL(FinancialAccountDimId, OwnerOrgDimId, month, year, true, true, true, "Debit");
                if (dT_get_xp_ALL_D != null)
                    foreach (DataColumn dc in dT_get_xp_ALL_D.Columns)
                    {
                        if (!dc.ColumnName.Equals("NAAN_DEFAULT"))
                            try
                            {
                                dt.Columns.Add(dc.ColumnName + "D", typeof(double));
                            }
                            catch { continue; }
                    }
                #endregion
                dt.Columns.Add("congco", typeof(double));

                dt.Columns.Add("sodunocuoithang", typeof(double));

                return dt;
            }
            catch (Exception) { return null; }
        }

        #region DataTable get XPCollection All
        public DataTable DT_get_xp_ALL(int FinancialAccountDimId, int OwnerOrgDimId, int month, int year, bool FinancialCustomerLiabilitySummary_Fact, bool FinancialCustomerLiabilityDetail, bool CorrespondFinancialAccountDimId, string Credit_Debit_NULL)
        {
            try
            {
                DataTable dt = new DataTable();

                XPCollection<FinancialCustomerLiabilitySummary_Fact> FCLSF = BO.get_xp_FinancialCustomerLiabilitySummary_Fact_3(session, FinancialAccountDimId, OwnerOrgDimId, month, year, Utility.Constant.ROWSTATUS_ACTIVE);
                if (FCLSF != null && FinancialCustomerLiabilitySummary_Fact == true)
                {
                    foreach (FinancialCustomerLiabilitySummary_Fact fclsf in FCLSF)
                    {
                        if (fclsf.FinancialCustomerLiabilitySummary_FactId != null)
                        {
                            #region
                            XPCollection<FinancialCustomerLiabilityDetail> FCLD = BO.get_xp_FinancialCustomerLiabilityDetailId_1(session, fclsf.FinancialCustomerLiabilitySummary_FactId, Utility.Constant.ROWSTATUS_ACTIVE);

                            if (FCLD != null && FinancialCustomerLiabilityDetail == true)
                            {
                                foreach (FinancialCustomerLiabilityDetail fcld in FCLD)
                                {
                                    #region
                                    if (fcld.CorrespondFinancialAccountDimId != null && fcld.CorrespondFinancialAccountDimId.Code != null)
                                    {
                                        CorrespondFinancialAccountDim CFAD = BO.get_CorrespondFinancialAccountDimId(session, fcld.CorrespondFinancialAccountDimId.Code, Utility.Constant.ROWSTATUS_ACTIVE);

                                        if (CFAD != null && CorrespondFinancialAccountDimId == true)
                                        {
                                            if (fcld.Credit > 0 && Credit_Debit_NULL.Equals("Credit"))
                                            {
                                                try { if (!CFAD.Code.Equals("")) dt.Columns.Add(CFAD.Code); }
                                                catch { continue; }
                                            }
                                            else if (fcld.Debit > 0 && Credit_Debit_NULL.Equals("Debit"))
                                            {
                                                try { if (!CFAD.Code.Equals(""))dt.Columns.Add(CFAD.Code); }
                                                catch { continue; }
                                            }
                                        }
                                    }
                                    #endregion
                                    else if (CorrespondFinancialAccountDimId == false)
                                    {
                                        try { dt.Columns.Add(fcld.FinancialCustomerLiabilityDetailId.ToString()); }
                                        catch { continue; }
                                    }
                                }
                            }
                            #endregion
                            else if (FinancialCustomerLiabilityDetail == false)
                            {
                                try { dt.Columns.Add(fclsf.FinancialCustomerLiabilitySummary_FactId.ToString()); }
                                catch { continue; }
                            }
                        }
                    }
                }
                else { return null; }
                return dt;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region datatable so tien tung tai khoan cua moi khach hang
        public DataTable DT_getvalue(int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId)
        {
            try
            {
                int stt = 2;
                DataTable dt = DT_Header(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId);              

                #region load value tung row
                DataTable DT_get_xp_all = DT_get_xp_ALL(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId, true, false, false, "NULL");
                if (DT_get_xp_all != null)
                {
                    foreach (DataColumn dc_all in DT_get_xp_all.Columns)
                    {
                        FinancialCustomerLiabilitySummary_Fact fclsf = BO.get_FinancialCustomerLiabilitySummary_FactId_1(session, Guid.Parse(dc_all.ColumnName.ToString()), Utility.Constant.ROWSTATUS_ACTIVE);
                        if (fclsf != null)
                        {
                            DataRow dr = dt.NewRow();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                if (dc.ColumnName.Equals("STT")) { dr[dc.ColumnName] = stt++; }
                                if (dc.ColumnName.Equals("tennguoimua")) { if (fclsf.CustomerOrgDimId.Code.Equals("NAAN_DEFAULT")) dr[dc.ColumnName] = "Khách hàng mặc định"; else dr[dc.ColumnName] = fclsf.CustomerOrgDimId.Name; }
                                if (dc.ColumnName.Equals("sodunodauthang")) { dr[dc.ColumnName] = fclsf.BeginDebitBalance; }

                                XPCollection<FinancialCustomerLiabilityDetail> fcld_xp = BO.get_xp_FinancialCustomerLiabilityDetailId_1(session, fclsf.FinancialCustomerLiabilitySummary_FactId, Utility.Constant.ROWSTATUS_ACTIVE);
                                if (fcld_xp != null)
                                {
                                    #region set row no 131

                                    foreach (FinancialCustomerLiabilityDetail fcld in fcld_xp)
                                    {
                                        if (fcld.Credit > 0 && !fcld.CorrespondFinancialAccountDimId.Code.Equals("NAAN_DEFAULT") && dc.ColumnName.Equals(fcld.CorrespondFinancialAccountDimId.Code))
                                        {
                                            double sum_column = 0;
                                            if (fclsf.FinancialCustomerLiabilitySummary_FactId != null)
                                            {
                                                sum_column = SumAcc(dc.ColumnName, fclsf.FinancialCustomerLiabilitySummary_FactId, true);
                                            }
                                            dr[dc.ColumnName] = sum_column;
                                        }
                                        if (fcld.Debit > 0 && fcld.CorrespondFinancialAccountDimId.Code.Equals("NAAN_DEFAULT") && dc.ColumnName.Equals("congno"))
                                        {
                                            double sum_column = 0;
                                            if (fcld.CorrespondFinancialAccountDimId.Code != null && fclsf.FinancialCustomerLiabilitySummary_FactId != null)
                                            {
                                                sum_column = SumAcc(fcld.CorrespondFinancialAccountDimId.Code, fclsf.FinancialCustomerLiabilitySummary_FactId, false);
                                            }
                                            if (dc.ColumnName.Equals("congno")) { dr[dc.ColumnName] = sum_column; }
                                        }

                                    }

                                    #endregion
                                    #region set row co 131
                                    foreach (FinancialCustomerLiabilityDetail fcld in fcld_xp)
                                    {
                                        if (fcld.Debit > 0 && !fcld.CorrespondFinancialAccountDimId.Code.Equals("NAAN_DEFAULT") && dc.ColumnName.Equals(fcld.CorrespondFinancialAccountDimId.Code + "D"))
                                        {
                                            double sum_column = 0;
                                            if (fclsf.FinancialCustomerLiabilitySummary_FactId != null)
                                            {
                                                sum_column = SumAcc(dc.ColumnName.ToString().Replace("D", ""), fclsf.FinancialCustomerLiabilitySummary_FactId, false);
                                            }
                                            dr[dc.ColumnName] = sum_column;
                                        }
                                        if (fcld.Credit > 0 && fcld.CorrespondFinancialAccountDimId.Code.Equals("NAAN_DEFAULT") && dc.ColumnName.Equals("congco"))
                                        {
                                            double sum_column = 0;
                                            if (fcld.CorrespondFinancialAccountDimId.Code != null && fclsf.FinancialCustomerLiabilitySummary_FactId != null)
                                            {
                                                sum_column = SumAcc(fcld.CorrespondFinancialAccountDimId.Code, fclsf.FinancialCustomerLiabilitySummary_FactId, true);
                                            }
                                            if (dc.ColumnName.Equals("congco")) { dr[dc.ColumnName] = sum_column; }
                                        }
                                    }
                                    #endregion
                                }

                                if (dc.ColumnName.Equals("sodunocuoithang")) { dr[dc.ColumnName] = fclsf.EndDebitBalance; }
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
                #endregion
                return dt;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region DataTable dua du lieu vao tung dong
        public DataTable DT_getRowValue(DataTable dt, int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId)
        {
            try
            {
                DataTable dT_getvalue = DT_getvalue(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId);
                if (dT_getvalue != null)
                {
                    foreach (DataRow dr in dT_getvalue.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["STT"] = dr["STT"].ToString();
                        row["tennguoimua"] = dr["tennguoimua"].ToString();
                        row["sodunodauthang"] = dr["sodunodauthang"];

                        DataTable dT_get_xp_ALL_C = DT_get_xp_ALL(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId, true, true, true, "Credit");
                        if (dT_get_xp_ALL_C != null)
                        {
                            foreach (DataColumn dc in dT_get_xp_ALL_C.Columns)
                            {
                                if (!dc.ColumnName.Equals("NAAN_DEFAULT"))
                                    try
                                    {
                                        row[dc.ColumnName] = dr[dc.ColumnName];
                                    }
                                    catch { continue; }
                            }
                        }
                        row["congno"] = dr["congno"];

                        DataTable dT_get_xp_ALL_D = DT_get_xp_ALL(FinancialAccountDimId, OwnerOrgDimId, MonthDimId, YearDimId, true, true, true, "Debit");
                        if (dT_get_xp_ALL_D != null)
                        {
                            foreach (DataColumn dc in dT_get_xp_ALL_D.Columns)
                            {
                                if (!dc.ColumnName.Equals("NAAN_DEFAULT"))
                                    try
                                    {
                                        row[dc.ColumnName + "D"] = dr[dc.ColumnName + "D"];
                                    }
                                    catch { continue; }

                            }
                        }
                        row["congco"] = dr["congco"];
                        row["sodunocuoithang"] = dr["sodunocuoithang"];
                        dt.Rows.Add(row);
                    }
                    dt.Rows.Add(SumlTotal_column(dt));
                }
                return dt;
            }
            catch (Exception) { return null; }
        }
        #endregion

        private DataRow SumlTotal_column(DataTable datatable)
        {
            try
            {
                DataRow row1 = datatable.NewRow();
                row1["tennguoimua"] = "Tổng Cộng";

                int column_count = datatable.Columns.Count - 1;
                int row_count = datatable.Rows.Count - 1;
                for (int c = 2; c <= column_count; c++)
                {
                    double sumT = 0;
                    for (int r = 1; r <= row_count; r++)
                    {
                        if (!datatable.Rows[r][c].ToString().Equals("") && datatable.Rows[r][c].ToString() != null)
                        {
                            double tt = double.Parse(datatable.Rows[r][c].ToString());
                            sumT += tt;
                        }
                    }
                    row1[datatable.Columns[c]] = sumT;
                }

                return row1;
            }
            catch (Exception) { return null; }
        }

        private double SumAcc(string CorrespondFinancialAccountDimId_Code, Guid FinancialCustomerLiabilitySummary_FactId, bool Credit_Debit)
        {
            double sum_column = 0;
            CorrespondFinancialAccountDim corr = BO.get_CorrespondFinancialAccountDimId(session, CorrespondFinancialAccountDimId_Code, Utility.Constant.ROWSTATUS_ACTIVE);
            if (corr != null)
            {
                XPCollection<FinancialCustomerLiabilityDetail> fcld_xp1 = BO.get_xp_FinancialCustomerLiabiltiyDetailId_8(session, FinancialCustomerLiabilitySummary_FactId, corr.CorrespondFinancialAccountDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                if (fcld_xp1 != null)
                {
                    foreach (FinancialCustomerLiabilityDetail FCLD1 in fcld_xp1)
                    {
                        if (Credit_Debit == true)
                            sum_column += (double)FCLD1.Credit;
                        else
                            sum_column += (double)FCLD1.Debit;
                    }
                }
            }
            return sum_column;
        }

        public DataTable DT_STT(DataTable data_table)
        {
            try
            {
                int STT = 1;

                DataRow dr = data_table.NewRow();
                foreach (DataColumn dc in data_table.Columns)
                {
                    dr[dc.ColumnName] = STT++;
                }
                data_table.Rows.Add(dr);
                return data_table;
            }
            catch (Exception) { return null; }
        }

        protected void xGridViewExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            GridViewDataColumn datacolumn = e.Column as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data && datacolumn.FieldName.Equals("sodunodauthang"))
            {
                object value = e.GetValue("sodunodauthang");
                if (value != null && value is double)
                {
                    if ((double)value == 0)
                        e.Text = "0";
                }
            }
            if (e.RowType == GridViewRowType.Data && datacolumn.FieldName.Equals("sodunocuoithang"))
            {
                object value = e.GetValue("sodunocuoithang");
                if (value != null && value is double)
                {
                    if ((double)value == 0)
                        e.Text = "0";
                }
            }
        }
    }
}