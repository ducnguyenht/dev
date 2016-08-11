using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Actor;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using DevExpress.Web.ASPxGridView;
using System.Data;
using DevExpress.XtraPrintingLinks;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Inventory;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04b8_DN : System.Web.UI.UserControl
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS04b8dn.Contains("show"))
            {
                load_data();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void load_data()
        {
            WebModule.Accounting.Report.S04b8_DN s04b8_dn = new Report.S04b8_DN();

            #region tham số truyền
            int month = Int32.Parse(this.hS04b8DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b8DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            string fAccount = this.hS04b8dnAccount.Get("account_id").ToString();
            //string asset = "";
            #endregion

            if (fAccount != "155" && fAccount != "156" && fAccount != "158") return;

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

                #region display label report
                s04b8_dn.xrMonth.Text = month.ToString();
                s04b8_dn.xrYear.Text = year.ToString();
                if (fAccount == "155")
                {
                    s04b8_dn.xrTitle.Text = String.Format("Thành phẩm (TK {0})", fAccount);
                }
                if (fAccount == "156")
                {
                    s04b8_dn.xrTitle.Text = String.Format("Hàng hóa (TK {0})", fAccount);
                }
                if (fAccount == "158")
                {
                    s04b8_dn.xrTitle.Text = String.Format("Hàng hóa kho bảo thuế (TK {0})", fAccount);
                }

                // số dư đẩu và cuối kì
                double no_dau_ki = 0, no_cuoi_ki = 0;
                if (f_c_FinancialAccountDim.Count != 0)
                {
                    foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                    {
                        if (md != null && yd != null && ood != null)
                        {
                            GoodsInInventorySummary_Fact FinancialSummary_Fact =
                                session.FindObject<GoodsInInventorySummary_Fact>(CriteriaOperator.Parse(
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
                                no_dau_ki += (double)FinancialSummary_Fact.BeginDebitBalance;
                                no_cuoi_ki += (double)FinancialSummary_Fact.EndDebitBalance;
                            }
                        }
                    }
                }

                s04b8_dn.xrdauki.Text = String.Format("{0:#,#}", no_dau_ki);
                s04b8_dn.xrCuoiKi.Text = String.Format("{0:#,#}", no_cuoi_ki);
                #endregion

                #region header và table báo cáo
                grid_header();
                DataTable datatable = table_pri();
                #endregion

                #region all row list_inventory
                List<int> list_inventory = new List<int>();
                if (f_c_FinancialAccountDim.Count != 0)
                {
                    foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                    {
                        if (md != null && yd != null && ood != null)
                        {
                            GoodsInInventorySummary_Fact GoodsSummary_Fact =
                                session.FindObject<GoodsInInventorySummary_Fact>(CriteriaOperator.Parse(
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
                            if (GoodsSummary_Fact != null)
                            {
                                ////
                                XPCollection<GoodsInInventoryDetail> collec_detail =
                                    new XPCollection<GoodsInInventoryDetail>(session, CriteriaOperator.Parse(
                                        String.Format("GoodsInInventorySummary_FacftId='{0}' AND "
                                        + "Credit>0 AND "
                                        + "CorrespondFinancialAccountDimId!='{1}' AND "
                                        + "RowStatus='{2}'",
                                        GoodsSummary_Fact.GoodsInInventorySummary_FactId,
                                        CorrespondFinancialAccountDimId_default,
                                        rowStatusActive
                                        )));
                                if (collec_detail.Count != 0)
                                {
                                    foreach (GoodsInInventoryDetail each_detail in collec_detail)
                                    {
                                        if (!list_inventory.Contains(each_detail.InventoryCommandDimId.InventoryCommandDimId))
                                        {
                                            list_inventory.Add(each_detail.InventoryCommandDimId.InventoryCommandDimId);
                                        }
                                    }
                                }
                                ////
                                collec_detail =
                                    new XPCollection<GoodsInInventoryDetail>(session, CriteriaOperator.Parse(
                                        String.Format("GoodsInInventorySummary_FacftId='{0}' AND "
                                        + "Debit>0 AND "
                                        + "CorrespondFinancialAccountDimId!='{1}' AND "
                                        + "RowStatus='{2}'",
                                        GoodsSummary_Fact.GoodsInInventorySummary_FactId,
                                        CorrespondFinancialAccountDimId_default,
                                        rowStatusActive
                                        )));
                                if (collec_detail.Count != 0)
                                {
                                    foreach (GoodsInInventoryDetail each_detail in collec_detail)
                                    {
                                        if (!list_inventory.Contains(each_detail.InventoryCommandDimId.InventoryCommandDimId))
                                        {
                                            list_inventory.Add(each_detail.InventoryCommandDimId.InventoryCommandDimId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region đổ dữ liệu
                DataRow dr;
                int STTu = 1;
                // từng dòng
                foreach (int each_row in list_inventory)
                {
                    #region object Inventory
                    InventoryCommandDim inventory = session.FindObject<InventoryCommandDim>(
                            CriteriaOperator.Parse(String.Format("InventoryCommandDimId='{0}' AND "
                            + "RowStatus='{1}'",
                            each_row,
                            rowStatusActive
                            )));
                    #endregion

                    dr = datatable.NewRow();
                    dr["stt"] = STTu++;
                    dr["so_hieu"] = inventory.Code;
                    dr["ngay_thang"] = String.Format("{0:dd/MM/yyyy}", inventory.IssueDate);
                    dr["dien_giai"] = inventory.Description;

                    // từng cột
                    #region credit correspond
                    double cong_no_TT = 0;
                    foreach (string header_column in header_credit_correspond())
                    {
                        double cell = 0, quantity = 0; ;
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
                                    GoodsInInventorySummary_Fact GoodsSummary_Fact =
                                        session.FindObject<GoodsInInventorySummary_Fact>(CriteriaOperator.Parse(
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
                                    if (GoodsSummary_Fact != null)
                                    {
                                        XPCollection<GoodsInInventoryDetail> collec_detail_credit =
                                            new XPCollection<GoodsInInventoryDetail>(session, CriteriaOperator.Parse(
                                                String.Format("GoodsInInventorySummary_FacftId='{0}' AND "
                                                + "Credit>0 AND "
                                                + "CorrespondFinancialAccountDimId!='{1}' AND "
                                                + "RowStatus='{2}' AND "
                                                + "InventoryCommandDimId='{3}' AND "
                                                + "CorrespondFinancialAccountDimId='{4}'",
                                                GoodsSummary_Fact.GoodsInInventorySummary_FactId,
                                                CorrespondFinancialAccountDimId_default,
                                                rowStatusActive,
                                                each_row,
                                                CorrespondId.CorrespondFinancialAccountDimId
                                                )));
                                        if (collec_detail_credit.Count != 0)
                                        {

                                            foreach (GoodsInInventoryDetail each_detail in collec_detail_credit)
                                            {
                                                cell += (double)each_detail.Credit;
                                                cong_no_TT += (double)each_detail.Credit;
                                                quantity += each_detail.Quantity;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //
                        dr[header_column + "_SL_co"] = quantity;
                        dr[header_column + "_TT_co"] = cell;
                    }
                    dr["cong_no_TT"] = cong_no_TT;
                    #endregion

                    // từng cột
                    #region debit correspond
                    double cong_co_TT = 0;
                    foreach (string header_column in header_debit_correspond())
                    {
                        double cell = 0, quantity = 0;
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
                                    GoodsInInventorySummary_Fact GoodsSummary_Fact =
                                        session.FindObject<GoodsInInventorySummary_Fact>(CriteriaOperator.Parse(
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
                                    if (GoodsSummary_Fact != null)
                                    {
                                        XPCollection<GoodsInInventoryDetail> collec_detail_debit =
                                            new XPCollection<GoodsInInventoryDetail>(session, CriteriaOperator.Parse(
                                                String.Format("GoodsInInventorySummary_FacftId='{0}' AND "
                                                + "Debit>0 AND "
                                                + "CorrespondFinancialAccountDimId!='{1}' AND "
                                                + "RowStatus='{2}' AND "
                                                + "InventoryCommandDimId='{3}' AND "
                                                + "CorrespondFinancialAccountDimId='{4}'",
                                                GoodsSummary_Fact.GoodsInInventorySummary_FactId,
                                                CorrespondFinancialAccountDimId_default,
                                                rowStatusActive,
                                                each_row,
                                                CorrespondId.CorrespondFinancialAccountDimId
                                                )));
                                        if (collec_detail_debit.Count != 0)
                                        {
                                            foreach (GoodsInInventoryDetail each_detail in collec_detail_debit)
                                            {
                                                cell += (double)each_detail.Debit;
                                                cong_co_TT += (double)each_detail.Debit;
                                                quantity += each_detail.Quantity;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        dr[header_column + "_SL_no"] = quantity;
                        dr[header_column + "_TT_no"] = cell;
                    }
                    dr["cong_co_TT"] = cong_co_TT;
                    #endregion

                    datatable.Rows.Add(dr);
                }
                #endregion

                #region dòng cộng
                dr = datatable.NewRow();
                dr["dien_giai"] = "Cộng";

                int column_count = datatable.Columns.Count - 1;
                int row_count = datatable.Rows.Count - 1;
                for (int c = 4; c <= column_count; c++)
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
                GridView_S04b8DN.DataSource = datatable;
                GridView_S04b8DN.DataBind();
                #endregion
            }
            catch { }

            #region export report
            s04b8_dn.printableCC_S04b8DN.PrintableComponent = new PrintableComponentLinkBase() { Component = GridViewExporter_S04b8DN };
            ReportViewer_S04b8DN.Report = s04b8_dn;
            #endregion
        }

        public List<string> header_credit_correspond()
        {
            List<string> credit_correspond = new List<string>();
            #region tham số
            int month = Int32.Parse(this.hS04b8DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b8DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            string fAccount = this.hS04b8dnAccount.Get("account_id").ToString();
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

            //
            XPCollection<FinancialAccountDim> f_c_FinancialAccountDim = new XPCollection<FinancialAccountDim>(session,
                CriteriaOperator.Parse(String.Format("Code like '{0}%' AND RowStatus='{1}'", fAccount, rowStatusActive)));
            if (f_c_FinancialAccountDim.Count != 0)
            {
                foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                {
                    if (md != null && yd != null && ood != null)
                    {
                        GoodsInInventorySummary_Fact GoodsSummary_Fact =
                            session.FindObject<GoodsInInventorySummary_Fact>(CriteriaOperator.Parse(
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
                        if (GoodsSummary_Fact != null)
                        {
                            XPCollection<GoodsInInventoryDetail> collec_detail =
                                new XPCollection<GoodsInInventoryDetail>(session, CriteriaOperator.Parse(
                                    String.Format("GoodsInInventorySummary_FacftId='{0}' AND "
                                    + "Credit>0 AND "
                                    + "CorrespondFinancialAccountDimId!='{1}' AND "
                                    + "RowStatus='{2}'",
                                    GoodsSummary_Fact.GoodsInInventorySummary_FactId,
                                    CorrespondFinancialAccountDimId_default,
                                    rowStatusActive
                                    )));
                            if (collec_detail.Count != 0)
                            {
                                foreach (GoodsInInventoryDetail each_detail in collec_detail)
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
            credit_correspond.Sort();
            return credit_correspond;
        }

        public List<string> header_debit_correspond()
        {
            List<string> debit_correspond = new List<string>();
            #region tham số
            int month = Int32.Parse(this.hS04b8DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b8DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            string fAccount = this.hS04b8dnAccount.Get("account_id").ToString();
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

            XPCollection<FinancialAccountDim> f_c_FinancialAccountDim = new XPCollection<FinancialAccountDim>(session,
                CriteriaOperator.Parse(String.Format("Code like '{0}%' AND RowStatus='{1}'", fAccount, rowStatusActive)));
            if (f_c_FinancialAccountDim.Count != 0)
            {
                foreach (FinancialAccountDim each_tk in f_c_FinancialAccountDim)
                {
                    if (md != null && yd != null && ood != null)
                    {
                        GoodsInInventorySummary_Fact GoodsSummary_Fact = session.FindObject<GoodsInInventorySummary_Fact>
                            (CriteriaOperator.Parse(String.Format("MonthDimId='{0}' AND "
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
                        if (GoodsSummary_Fact != null)
                        {
                            XPCollection<GoodsInInventoryDetail> collec_detail =
                                new XPCollection<GoodsInInventoryDetail>(session, CriteriaOperator.Parse(
                                    String.Format("GoodsInInventorySummary_FacftId='{0}' AND "
                                    + "Debit>0 AND "
                                    + "CorrespondFinancialAccountDimId!='{1}' AND "
                                    + "RowStatus='{2}'",
                                    GoodsSummary_Fact.GoodsInInventorySummary_FactId,
                                    CorrespondFinancialAccountDimId_default,
                                    rowStatusActive
                                    )));
                            if (collec_detail.Count != 0)
                            {
                                foreach (GoodsInInventoryDetail each_detail in collec_detail)
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

            debit_correspond.Sort();
            return debit_correspond;
        }

        public void grid_header()
        {
            string fAccount = this.hS04b8dnAccount.Get("account_id").ToString();

            GridViewBandColumn bandColumn, miniBandColumn;
            // stt
            GridViewDataTextColumn fieldColumn = new GridViewDataTextColumn { FieldName = "stt", Caption = "Số TT" };
            GridView_S04b8DN.Columns.Add(fieldColumn);
            // so hieu, ngay thang
            bandColumn = new GridViewBandColumn("Chứng từ");
            fieldColumn = new GridViewDataTextColumn { FieldName = "so_hieu", Caption = "Số hiệu" };
            bandColumn.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "ngay_thang", Caption = "Ngày tháng" };
            bandColumn.Columns.Add(fieldColumn);
            GridView_S04b8DN.Columns.Add(bandColumn);
            // dien giai
            fieldColumn = new GridViewDataTextColumn { FieldName = "dien_giai", Caption = "Diễn giải" };
            GridView_S04b8DN.Columns.Add(fieldColumn);

            //ghi co cac TK
            bandColumn = new GridViewBandColumn(String.Format("Ghi nợ TK {0}, ghi có các TK", fAccount));
            foreach (string TK_credit in header_credit_correspond())
            {
                miniBandColumn = new GridViewBandColumn(TK_credit);
                //
                fieldColumn = new GridViewDataTextColumn { FieldName = TK_credit + "_SL_co", Caption = "Số lượng" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                miniBandColumn.Columns.Add(fieldColumn);
                //
                fieldColumn = new GridViewDataTextColumn { FieldName = TK_credit + "_HT_co", Caption = "Giá HT" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                miniBandColumn.Columns.Add(fieldColumn);
                //
                fieldColumn = new GridViewDataTextColumn { FieldName = TK_credit + "_TT_co", Caption = "Giá TT" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                miniBandColumn.Columns.Add(fieldColumn);
                //
                bandColumn.Columns.Add(miniBandColumn);
            }
            // cong no TK
            miniBandColumn = new GridViewBandColumn(String.Format("Cộng nợ TK {0}", fAccount));

            fieldColumn = new GridViewDataTextColumn { FieldName = "cong_no_HT", Caption = "Giá HT" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            miniBandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "cong_no_TT", Caption = "Giá TT" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            miniBandColumn.Columns.Add(fieldColumn);
            bandColumn.Columns.Add(miniBandColumn);
            GridView_S04b8DN.Columns.Add(bandColumn);

            // ghi no cac TK
            bandColumn = new GridViewBandColumn(String.Format("Ghi có TK {0}, ghi nợ các TK", fAccount));
            foreach (string TK_debit in header_debit_correspond())
            {
                miniBandColumn = new GridViewBandColumn(TK_debit);

                fieldColumn = new GridViewDataTextColumn { FieldName = TK_debit + "_SL_no", Caption = "Số lượng" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                miniBandColumn.Columns.Add(fieldColumn);

                fieldColumn = new GridViewDataTextColumn { FieldName = TK_debit + "_HT_no", Caption = "Giá HT" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                miniBandColumn.Columns.Add(fieldColumn);

                fieldColumn = new GridViewDataTextColumn { FieldName = TK_debit + "_TT_no", Caption = "Giá TT" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                miniBandColumn.Columns.Add(fieldColumn);

                bandColumn.Columns.Add(miniBandColumn);
            }
            //
            miniBandColumn = new GridViewBandColumn(String.Format("Cộng có TK {0}", fAccount));

            fieldColumn = new GridViewDataTextColumn { FieldName = "cong_co_HT", Caption = "Giá HT" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            miniBandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "cong_co_TT", Caption = "Giá TT" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            miniBandColumn.Columns.Add(fieldColumn);

            bandColumn.Columns.Add(miniBandColumn);
            GridView_S04b8DN.Columns.Add(bandColumn);
        }

        public DataTable table_pri()
        {
            DataTable table_pri = new DataTable();
            DataColumn dc = table_pri.Columns.Add("stt");
            dc = table_pri.Columns.Add("so_hieu");
            dc = table_pri.Columns.Add("ngay_thang");
            dc = table_pri.Columns.Add("dien_giai");
            foreach (string TK_credit in header_credit_correspond())
            {
                dc = table_pri.Columns.Add(TK_credit + "_SL_co", typeof(double));
                dc = table_pri.Columns.Add(TK_credit + "_HT_co", typeof(double));
                dc = table_pri.Columns.Add(TK_credit + "_TT_co", typeof(double));
            }
            dc = table_pri.Columns.Add("cong_no_HT", typeof(double));
            dc = table_pri.Columns.Add("cong_no_TT", typeof(double));
            foreach (string TK_debit in header_debit_correspond())
            {
                dc = table_pri.Columns.Add(TK_debit + "_SL_no", typeof(double));
                dc = table_pri.Columns.Add(TK_debit + "_HT_no", typeof(double));
                dc = table_pri.Columns.Add(TK_debit + "_TT_no", typeof(double));
            }
            dc = table_pri.Columns.Add("cong_co_HT", typeof(double));
            dc = table_pri.Columns.Add("cong_co_TT", typeof(double));

            // first row
            DataRow dr = table_pri.NewRow();
            dr[0] = "A";
            dr[1] = "B";
            dr[2] = "C";
            dr[3] = "D";
            int STTc = 1;
            for (int c = 4; c < table_pri.Columns.Count; c++)
            {
                dr[c] = STTc++;
            }
            table_pri.Rows.Add(dr);
            return table_pri;
        }
    }
}