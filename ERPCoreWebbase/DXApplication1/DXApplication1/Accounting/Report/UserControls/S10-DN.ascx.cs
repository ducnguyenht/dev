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
using NAS.DAL.BI.Accounting.ItemInventory;
using NAS.BO.Accounting.Report;
using NAS.DAL.BI.Inventory;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Item;
using NAS.DAL.BI.Actor;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S10_DN : System.Web.UI.UserControl
    {
        Session session;
        BO_s10_dn BO = new BO_s10_dn();
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hs10dn.Contains("show"))
            { Load_Data(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Load_Data()
        {
            WebModule.Accounting.Report.S10_DN s10dn = new WebModule.Accounting.Report.S10_DN();
            try
            {
                #region Parameter
                string account = this.hs10dnAcc.Get("account_id").ToString();
                int month = int.Parse(this.hs10dnMonth.Get("month_id").ToString());
                int year = int.Parse(this.hs10dnYear.Get("year_id").ToString());
                string unitDim = this.hs10dnUnitDim.Get("unit_id").ToString();
                string item = this.hs10dnItem.Get("Item_id").ToString();
                string inventory = this.hs10dnInventory.Get("Inventory_id").ToString();
                string OwnerOrg = "QUASAPHARCO";
                #endregion

                #region Select *
                FinancialAccountDim FAD = BO.get_FinancialAccountDim(session, account, Utility.Constant.ROWSTATUS_ACTIVE);

                MonthDim MD = BO.get_MonthDim(session, month.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                YearDim YD = BO.get_YearDim(session, year.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                ItemDim ID = BO.get_ItemDim_1(session, item, Utility.Constant.ROWSTATUS_ACTIVE);

                InventoryDim InDim = BO.get_InventoryDim_1(session, inventory, Utility.Constant.ROWSTATUS_ACTIVE);

                UnitDim UD = BO.get_UnitDim(session, unitDim, Utility.Constant.ROWSTATUS_ACTIVE);

                OwnerOrgDim OOD = BO.get_OwnerOrgDim(session, OwnerOrg, Utility.Constant.ROWSTATUS_ACTIVE);

                #endregion

                #region label
                s10dn.lbl_Inventory.Text = inventory;
                s10dn.lbl_Item.Text = item;
                s10dn.lbl_year.Text = year.ToString();
                s10dn.lbl_account.Text = account;
                s10dn.lbl_unitDim.Text = unitDim;
                #endregion

                #region GridView_header vao DataTable
                GridView_header();
                DataTable dt = DT_header();
                #endregion

                if (FAD == null || MD == null || YD == null || ID == null || InDim == null || UD == null || OOD == null)
                    return;

                #region do du lieu vao DataTable
                DT_rowgetvalue(dt, FAD.FinancialAccountDimId, InDim.InventoryDimId, OOD.OwnerOrgDimId, ID.ItemDimId, MD.MonthDimId, YD.YearDimId, UD.UnitDimId);
                #endregion

                #region do du lieu vao DataTable
                xGridView.DataSource = dt;
                xGridView.DataBind();
                #endregion
            }
            catch { }

            #region xuat Report
            xGridViewExporter.GridViewID = "xGridView";
            s10dn.printableCC.PrintableComponent = new PrintableComponentLinkBase() { Component = xGridViewExporter };
            ReportViewerS10.Report = s10dn;
            #endregion
        }

        #region cac ham su ly load du lieu cho Load_Data()
        public void GridView_header()
        {
            try
            {
                GridViewBandColumn bandColumn = new GridViewBandColumn();
                GridViewDataTextColumn GVDTC = new GridViewDataTextColumn();
                GVDTC = new GridViewDataTextColumn { FieldName = "STT", Caption = "STT" };
                xGridView.Columns.Add(GVDTC);

                bandColumn = new GridViewBandColumn("Chứng Từ");
                GVDTC = new GridViewDataTextColumn { FieldName = "sohieu", Caption = "Số hiệu" };
                bandColumn.Columns.Add(GVDTC);
                GVDTC = new GridViewDataTextColumn { FieldName = "ngaythang", Caption = "Ngày tháng" };
                bandColumn.Columns.Add(GVDTC);
                xGridView.Columns.Add(bandColumn);

                GVDTC = new GridViewDataTextColumn { FieldName = "diengiai", Caption = "Diễn giải" };
                xGridView.Columns.Add(GVDTC);
                GVDTC = new GridViewDataTextColumn { FieldName = "taikhoandoixung", Caption = "Tài khoản đối ứng" };
                xGridView.Columns.Add(GVDTC);
                GVDTC = new GridViewDataTextColumn { FieldName = "dongia", Caption = "Đơn giá" };
                GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                xGridView.Columns.Add(GVDTC);

                bandColumn = new GridViewBandColumn("Nhập");
                GVDTC = new GridViewDataTextColumn { FieldName = "soluongN", Caption = "Số lượng" };
                GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(GVDTC);
                GVDTC = new GridViewDataTextColumn { FieldName = "thanhtienN", Caption = "Thành tiền" };
                GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(GVDTC);
                xGridView.Columns.Add(bandColumn);

                bandColumn = new GridViewBandColumn("Xuất");
                GVDTC = new GridViewDataTextColumn { FieldName = "soluongX", Caption = "Số lượng" };
                GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(GVDTC);
                GVDTC = new GridViewDataTextColumn { FieldName = "thanhtienX", Caption = "Thành tiền" };
                GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(GVDTC);
                xGridView.Columns.Add(bandColumn);

                bandColumn = new GridViewBandColumn("Tồn");
                GVDTC = new GridViewDataTextColumn { FieldName = "soluongT", Caption = "Số lượng" };
                GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(GVDTC);
                GVDTC = new GridViewDataTextColumn { FieldName = "thanhtienT", Caption = "Thành tiền" };
                GVDTC.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(GVDTC);
                xGridView.Columns.Add(bandColumn);

                GVDTC = new GridViewDataTextColumn { FieldName = "ghichu", Caption = "Ghi chú" };
                xGridView.Columns.Add(GVDTC);
            }
            catch { }
        }

        public DataTable DT_header()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("STT", typeof(double));
                dt.Columns.Add("sohieu");
                dt.Columns.Add("ngaythang");
                dt.Columns.Add("diengiai");
                dt.Columns.Add("taikhoandoixung");
                dt.Columns.Add("dongia", typeof(double));
                dt.Columns.Add("soluongN", typeof(double));
                dt.Columns.Add("thanhtienN", typeof(double));
                dt.Columns.Add("soluongX", typeof(double));
                dt.Columns.Add("thanhtienX", typeof(double));
                dt.Columns.Add("soluongT", typeof(double));
                dt.Columns.Add("thanhtienT", typeof(double));
                dt.Columns.Add("ghichu");
                return dt;
            }
            catch (Exception) { return null; }
        }

        public DataTable DT_getValue(int FinancialAccountDimId, int InventoryDimId, int OwnerOrgDimId, int ItemDimId, int MonthDimId, int YearDimId, int UnitDimId)
        {
            try
            {
                int stt = 1;
                DataTable dt = DT_header();
                DataTable dt_xp_FIISF = DT_xp_getValue(FinancialAccountDimId, InventoryDimId, OwnerOrgDimId, ItemDimId, UnitDimId, MonthDimId, YearDimId, true, false);
                CorrespondFinancialAccountDim CFAD = BO.get_CorrespondFinancialAccountDim(session, "NAAN_DEFAULT", Utility.Constant.ROWSTATUS_ACTIVE);

                if (dt_xp_FIISF != null && dt != null && CFAD != null)
                {
                    DataRow dr = dt.NewRow();
                    #region (1)
                    foreach (DataColumn dc in dt.Columns)
                    {
                        foreach (DataColumn dc_FII in dt_xp_FIISF.Columns)
                        {
                            FinancialItemInventorySummary_Fact FIIS = BO.get_FinancialItemInventorySummary_Fact(session, int.Parse(dc_FII.ColumnName.ToString()), Utility.Constant.ROWSTATUS_ACTIVE);
                            if (dc.ColumnName.Equals("STT")) { dr[dc.ColumnName] = stt; }
                            if (dc.ColumnName.Equals("diengiai")) { dr[dc.ColumnName] = "Số dư đầu tháng"; }
                            if (dc.ColumnName.Equals("soluongT")) { dr[dc.ColumnName] = FIIS.BeginBalanceItem; }
                            if (dc.ColumnName.Equals("thanhtienT")) { dr[dc.ColumnName] = FIIS.BeginDebitBalance; }
                        }
                    }
                    dt.Rows.Add(dr);
                    stt++;
                    #endregion

                    #region (2)
                    foreach (DataColumn dc_all in dt_xp_FIISF.Columns)
                    {
                        DataTable dt_xp_IIBA = DT_getInventoryCommandDimId(int.Parse(dc_all.ColumnName.ToString()));
                        if (dt_xp_IIBA != null)
                        {
                            foreach (DataColumn dt_IIBA in dt_xp_IIBA.Columns)
                            {
                                XPCollection<ItemInventoryByArtifact> IIBA_xp = BO.get_xp_InventoryByArtifact_1(session, int.Parse(dc_all.ColumnName.ToString()), int.Parse(dt_IIBA.ColumnName.ToString()), Utility.Constant.ROWSTATUS_ACTIVE);
                                if (IIBA_xp != null)
                                {
                                    foreach (ItemInventoryByArtifact IIBA in IIBA_xp)
                                    {
                                        if (!IIBA.CorrespondFinancialAccountDimId.CorrespondFinancialAccountDimId.Equals(CFAD.CorrespondFinancialAccountDimId))
                                        {
                                            dr = dt.NewRow();
                                            foreach (DataColumn dc in dt.Columns)
                                            {
                                                if (dc.ColumnName.Equals("STT")) { dr[dc.ColumnName] = stt; }
                                                if (dc.ColumnName.Equals("sohieu")) { dr[dc.ColumnName] = IIBA.InventoryCommandDimId.Code; }
                                                if (dc.ColumnName.Equals("ngaythang")) { dr[dc.ColumnName] = String.Format("{0:d}", IIBA.InventoryCommandDimId.IssueDate); }
                                                if (dc.ColumnName.Equals("diengiai")) { dr[dc.ColumnName] = IIBA.InventoryCommandDimId.Description; }
                                                if (dc.ColumnName.Equals("taikhoandoixung")) { dr[dc.ColumnName] = IIBA.CorrespondFinancialAccountDimId.Code; }
                                                if (dc.ColumnName.Equals("dongia")) { dr[dc.ColumnName] = IIBA.Price; }
                                                if (IIBA.CreditItemSum > 0)
                                                {
                                                    if (dc.ColumnName.Equals("soluongN")) { dr[dc.ColumnName] = IIBA.CreditItemSum; }
                                                    if (dc.ColumnName.Equals("thanhtienN")) { dr[dc.ColumnName] = IIBA.CreditSum; }
                                                }
                                                if (IIBA.DebitItemSum > 0)
                                                {
                                                    if (dc.ColumnName.Equals("soluongX")) { dr[dc.ColumnName] = IIBA.DebitItemSum; }
                                                    if (dc.ColumnName.Equals("thanhtienX")) { dr[dc.ColumnName] = IIBA.DebitSum; }
                                                }
                                                if (dc.ColumnName.Equals("soluongT")) { dr[dc.ColumnName] = IIBA.CurrentBalanceItem; }
                                                if (dc.ColumnName.Equals("thanhtienT")) { dr[dc.ColumnName] = IIBA.CurrentBalance; }
                                            }
                                            dt.Rows.Add(dr);
                                            stt++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region (3)
                    dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        foreach (DataColumn dc_FII in dt_xp_FIISF.Columns)
                        {
                            FinancialItemInventorySummary_Fact FIIS = BO.get_FinancialItemInventorySummary_Fact(session, int.Parse(dc_FII.ColumnName.ToString()), Utility.Constant.ROWSTATUS_ACTIVE);
                            if (dc.ColumnName.Equals("STT")) { dr[dc.ColumnName] = stt; }
                            if (dc.ColumnName.Equals("diengiai")) { dr[dc.ColumnName] = "Số dư cuối tháng"; }
                            if (dc.ColumnName.Equals("soluongT")) { dr[dc.ColumnName] = FIIS.EndBalanceItem; }
                            if (dc.ColumnName.Equals("thanhtienT")) { dr[dc.ColumnName] = FIIS.EndDebitBalance; }
                        }
                    }
                    dt.Rows.Add(dr);
                    #endregion

                }
                return dt;
            }
            catch (Exception) { return null; }
        }

        private DataTable DT_rowgetvalue(DataTable dt, int FinancialAccountDimId, int InventoryDimId, int OwnerOrgDimId, int ItemDimId, int MonthDimId, int YearDimId, int UnitDimId)
        {
            try
            {
                DT_STT(dt);
                DataTable dt_getvalue = DT_getValue(FinancialAccountDimId, InventoryDimId, OwnerOrgDimId, ItemDimId, MonthDimId, YearDimId, UnitDimId);
                if (dt_getvalue != null)
                {
                    foreach (DataRow dr in dt_getvalue.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["STT"] = dr["STT"];
                        row["sohieu"] = dr["sohieu"];
                        row["ngaythang"] = dr["ngaythang"];
                        row["diengiai"] = dr["diengiai"];
                        row["taikhoandoixung"] = dr["taikhoandoixung"];
                        row["dongia"] = dr["dongia"];
                        row["soluongN"] = dr["soluongN"];
                        row["thanhtienN"] = dr["thanhtienN"];
                        row["soluongX"] = dr["soluongX"];
                        row["thanhtienX"] = dr["thanhtienX"];
                        row["soluongT"] = dr["soluongT"];
                        row["thanhtienT"] = dr["thanhtienT"];
                        row["ghichu"] = dr["ghichu"];
                        dt.Rows.Add(row);
                    }
                }
                dt.Rows.Add(SumlTotal_column(dt));
                return dt;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region cac ham su ly logic
        private DataTable DT_xp_getValue(int FinancialAccountDimId, int InventoryDimId, int OwnerOrgDimId, int ItemDimId, int UnitDimId, int MonthDimId, int YearDimId, bool getXPFIISF, bool geXPIIBA)
        {
            try
            {
                DataTable dt = new DataTable();
                XPCollection<FinancialItemInventorySummary_Fact> FIISF = BO.get_xp_FinancialItemInventorySummary_Fact(session, FinancialAccountDimId, InventoryDimId, OwnerOrgDimId, ItemDimId, UnitDimId, MonthDimId, YearDimId, Utility.Constant.ROWSTATUS_ACTIVE);
                if (FIISF != null && getXPFIISF == true)
                {
                    foreach (FinancialItemInventorySummary_Fact fiisf in FIISF)
                    {
                        XPCollection<ItemInventoryByArtifact> IIBA = BO.get_xp_ItemInventoryByArtifact(session, fiisf.FinancialItemInventorySummary_FactId, Utility.Constant.ROWSTATUS_ACTIVE);
                        if (IIBA != null && geXPIIBA == true)
                        {
                            foreach (ItemInventoryByArtifact iiba in IIBA)
                            {
                                try
                                {
                                    dt.Columns.Add(iiba.ItemInventoryByArtifactId.ToString());
                                }
                                catch { }
                            }
                        }
                        if (geXPIIBA == false)
                        {
                            try
                            {
                                dt.Columns.Add(fiisf.FinancialItemInventorySummary_FactId.ToString());
                            }
                            catch { }
                        }
                    }
                }
                else return null;
                return dt;
            }
            catch (Exception) { return null; }
        }

        private DataTable DT_STT(DataTable dt)
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

        private DataRow SumlTotal_column(DataTable datatable)
        {
            try
            {
                DataRow row1 = datatable.NewRow();
                row1["diengiai"] = "Tổng Cộng";

                int column_count = datatable.Columns.Count - 1;
                int row_count = datatable.Rows.Count - 1;
                for (int c = 6; c <= column_count; c++)
                {
                    if (c != 10)
                        if (c != 11)
                            if (c != 12)
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

        private DataTable DT_getInventoryCommandDimId(int FinancialItemInventorySummary_FactId)
        {
            try
            {
                DataTable dt = new DataTable();
                XPCollection<ItemInventoryByArtifact> FIISF_xp = BO.get_xp_ItemInventoryByArtifact(session, FinancialItemInventorySummary_FactId, Utility.Constant.ROWSTATUS_ACTIVE);
                if (FIISF_xp != null)
                {
                    FIISF_xp.Sorting.Add(new SortProperty("InventoryCommandDimId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    foreach (ItemInventoryByArtifact FIISF in FIISF_xp)
                    {
                        try
                        {
                            dt.Columns.Add(FIISF.InventoryCommandDimId.InventoryCommandDimId.ToString());
                        }
                        catch { continue; }
                    }
                }
                return dt;
            }
            catch (Exception) { return null; }
        }

        #endregion




    }
}