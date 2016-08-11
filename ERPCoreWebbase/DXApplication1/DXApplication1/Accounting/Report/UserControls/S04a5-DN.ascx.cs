using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using System.Data;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using DevExpress.Data.Filtering;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting;
using DevExpress.Web.ASPxGridView;
using DevExpress.XtraPrintingLinks;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting.SupplierLiability;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a5_DN : System.Web.UI.UserControl
    {
        #region the first

        Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS04a5dn.Contains("show"))
            {
                load_data();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region load_data
        public void load_data()
        {
            WebModule.Accounting.Report.S04a5_DN s04a5_dn = new WebModule.Accounting.Report.S04a5_DN();
            try
            {
                #region tham số truyền vào
                int Account = 331;
                int month = Int32.Parse(this.hS04a5month.Get("month_id").ToString());
                int year = Int32.Parse(this.hS04a5year.Get("year_id").ToString());
                string owner = "";
                string asset = "";//this.hAsset.Get("asset_id").ToString();

                s04a5_dn.xrLabel_month.Text = month.ToString();
                s04a5_dn.xrLabel_year.Text = year.ToString();

                CorrespondFinancialAccountDim cfad = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                #endregion

                #region tập hợp sử dụng lấy dữ liệu
                XPCollection<MonthDim> md = new XPCollection<MonthDim>(session, CriteriaOperator.Parse("Name='" + month + "'"));
                XPCollection<YearDim> yd = new XPCollection<YearDim>(session, CriteriaOperator.Parse("Name='" + year + "'"));
                XPCollection<FinancialAccountDim> fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code='" + Account + "'"));

                //XPCollection<FinancialAssetDim> fad = new XPCollection<FinancialAssetDim>(session, CriteriaOperator.Parse("Name='" + asset + "'"));
                #endregion

                #region xây dựng header và table báo cáo
                GridView_header(Account, month, year, asset);
                DataTable data_table = table_pri(Account, month, year, asset);
                #endregion

                #region dòng STT cột
                int STT = 1;
                DataRow dr = data_table.NewRow();
                foreach (DataColumn dc in data_table.Columns)
                {
                    dr[dc.ColumnName] = STT++;
                }
                data_table.Rows.Add(dr);
                #endregion

                #region danh sách supplier
                XPCollection<FinancialSupplierLiabilitySummary_Fact> fslsf_1 = new XPCollection<FinancialSupplierLiabilitySummary_Fact>(session, CriteriaOperator.Parse("MonthDimId='" + md[0].MonthDimId + "' AND YearDimId='" + yd[0].YearDimId + "' AND FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND RowStatus='1'"));
                DataTable table_supplier = new DataTable();
                foreach (FinancialSupplierLiabilitySummary_Fact each_fslsf in fslsf_1)
                {
                    try
                    {
                        if (each_fslsf.SupplierOrgDimId.Name == "NAAN_DEFAULT")
                        {
                            table_supplier.Columns.Add("Nhà cung cấp mặc định");
                        }
                        else
                        {
                            table_supplier.Columns.Add(each_fslsf.SupplierOrgDimId.Name);
                        }
                    }
                    catch { continue; }
                }
                #endregion

                #region đổ dữ liệu
                // dựa theo danh sách supplier
                int STTu = 1;
                foreach (DataColumn each_supplier in table_supplier.Columns)
                {
                    DataRow[] drow = data_table.Select("ten_don_vi='" + each_supplier.ColumnName + "'");
                    if (drow.Length == 0)
                    {
                        // supplier id
                        Guid return_supplierID;
                        if (each_supplier.ColumnName == "Nhà cung cấp mặc định")
                        {
                            XPCollection<SupplierOrgDim> supplier_id = new XPCollection<SupplierOrgDim>(session, CriteriaOperator.Parse("Name='NAAN_DEFAULT'"));
                            return_supplierID = supplier_id[0].SupplierOrgDimId;
                        }
                        else
                        {
                            XPCollection<SupplierOrgDim> supplier_id = new XPCollection<SupplierOrgDim>(session, CriteriaOperator.Parse("Name='" + each_supplier.ColumnName + "'"));
                            return_supplierID = supplier_id[0].SupplierOrgDimId;
                        }

                        DataRow row = data_table.NewRow();
                        row["STT"] = STTu++;
                        row["ten_don_vi"] = each_supplier.ColumnName;

                        XPCollection<FinancialSupplierLiabilitySummary_Fact> fslsf =
                                        new XPCollection<FinancialSupplierLiabilitySummary_Fact>(
                                        session, CriteriaOperator.Parse("MonthDimId='" + md[0].MonthDimId + "' AND YearDimId='" + yd[0].YearDimId + "' AND FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND SupplierOrgDimId='" + return_supplierID + "' AND RowStatus='1'"));


                        row["no_dau"] = (double)fslsf[0].BeginDebitBalance > 0 ? (double)fslsf[0].BeginDebitBalance : 0;
                        row["co_dau"] = (double)fslsf[0].BeginCreditBalance > 0 ? (double)fslsf[0].BeginCreditBalance : 0;

                        #region xét từng cột phần Có TK 331
                        double row_sum_credit = 0;
                        foreach (DataColumn dcCredit331 in table_Credit_331(Account, month, year, asset).Columns)
                        {
                            double column_mount = 0;

                            #region không chứa GiaHT, không chứa GiaTT
                            if (!dcCredit331.ColumnName.Contains("_GiaHT") && !dcCredit331.ColumnName.Contains("_GiaTT"))
                            {

                                //chứa credit 331
                                XPCollection<FinancialSupplierLiabilityDetail> fsld = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND Credit>0 AND CorrespondFinancialAccountDimId='" + cfad.CorrespondFinancialAccountDimId + "' AND FinancialSupplierLiabilitySummary_FactId='" + fslsf[0].FinancialSupplierLiabilitySummary_FactId + "' AND RowStatus='1'"));
                                if (fsld.Count != 0)
                                {
                                    foreach (FinancialSupplierLiabilityDetail each_fsld in fsld)
                                    {
                                        // chứa 2 transaction
                                        XPCollection<FinancialSupplierLiabilityDetail> fsld1 = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialTransactionDimId='" + each_fsld.FinancialTransactionDimId.FinancialTransactionDimId + "' AND FinancialSupplierLiabilitySummary_FactId='" + fslsf[0].FinancialSupplierLiabilitySummary_FactId + "' AND RowStatus='1'"));
                                        foreach (FinancialSupplierLiabilityDetail each_fsld1 in fsld1)
                                        {
                                            if (each_fsld1.FinancialCustomerLiabilityDetailId != each_fsld.FinancialCustomerLiabilityDetailId)
                                            {
                                                if (each_fsld1.CorrespondFinancialAccountDimId.Code == dcCredit331.ColumnName)
                                                {
                                                    column_mount += (double)each_fsld1.Debit;
                                                    row_sum_credit += (double)each_fsld1.Debit;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (column_mount != 0)
                                {
                                    row[dcCredit331.ColumnName] = column_mount;
                                }
                                else
                                {
                                    //row[dcCredit331.ColumnName] = "";
                                }
                            }
                            #endregion

                            #region không chứa GiaHT, chứa GiaTT
                            if (!dcCredit331.ColumnName.Contains("_GiaHT") && dcCredit331.ColumnName.Contains("_GiaTT"))
                            {
                                // id debit
                                XPCollection<FinancialSupplierLiabilityDetail> fsld = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND Credit>0 AND CorrespondFinancialAccountDimId='" + cfad.CorrespondFinancialAccountDimId + "' AND FinancialSupplierLiabilitySummary_FactId='" + fslsf[0].FinancialSupplierLiabilitySummary_FactId + "' AND RowStatus='1'"));
                                if (fsld.Count != 0)
                                {
                                    foreach (FinancialSupplierLiabilityDetail each_fsld in fsld)
                                    {
                                        // chứa 2 transaction
                                        XPCollection<FinancialSupplierLiabilityDetail> fsld1 = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialTransactionDimId='" + each_fsld.FinancialTransactionDimId.FinancialTransactionDimId + "' AND FinancialSupplierLiabilitySummary_FactId='" + fslsf[0].FinancialSupplierLiabilitySummary_FactId + "' AND RowStatus='1'"));
                                        foreach (FinancialSupplierLiabilityDetail each_fsld1 in fsld1)
                                        {
                                            if (each_fsld1.FinancialCustomerLiabilityDetailId != each_fsld.FinancialCustomerLiabilityDetailId && each_fsld1.CorrespondFinancialAccountDimId.Code == dcCredit331.ColumnName.Substring(0, dcCredit331.ColumnName.Length - 6))
                                            {
                                                column_mount += (double)each_fsld1.Debit;
                                                row_sum_credit += (double)each_fsld1.Debit;
                                            }
                                        }
                                    }
                                }
                                if (column_mount != 0)
                                {
                                    row[dcCredit331.ColumnName] = column_mount;
                                }
                                else
                                {
                                    //row[dcCredit331.ColumnName] = "";
                                }
                            }
                            #endregion
                        }
                        row["Cong_Co"] = row_sum_credit;
                        #endregion

                        #region xét từng cột phần Nợ TK 331
                        double row_sum_debit = 0;
                        foreach (DataColumn dcDebit331 in table_Debit_331(Account, month, year, asset).Columns)
                        {
                            double column_mount = 0;
                            // id credit
                            XPCollection<FinancialSupplierLiabilityDetail> fsld = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND Debit>0 AND CorrespondFinancialAccountDimId='" + cfad.CorrespondFinancialAccountDimId + "' AND FinancialSupplierLiabilitySummary_FactId='" + fslsf[0].FinancialSupplierLiabilitySummary_FactId + "' AND RowStatus='1'"));
                            if (fsld.Count != 0)
                            {
                                foreach (FinancialSupplierLiabilityDetail each_fsld in fsld)
                                {
                                    // chứa 2 transaction
                                    XPCollection<FinancialSupplierLiabilityDetail> fsld1 = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialTransactionDimId='" + each_fsld.FinancialTransactionDimId.FinancialTransactionDimId + "' AND FinancialSupplierLiabilitySummary_FactId='" + fslsf[0].FinancialSupplierLiabilitySummary_FactId + "' AND RowStatus='1'"));
                                    foreach (FinancialSupplierLiabilityDetail each_fsld1 in fsld1)
                                    {
                                        if (each_fsld1.FinancialCustomerLiabilityDetailId != each_fsld.FinancialCustomerLiabilityDetailId && each_fsld1.CorrespondFinancialAccountDimId.Code == dcDebit331.ColumnName.Substring(0, 3))
                                        {
                                            column_mount += (double)each_fsld1.Credit;
                                            row_sum_debit += (double)each_fsld1.Credit;
                                        }
                                    }
                                }
                            }
                            if (column_mount != 0)
                            {
                                row[dcDebit331.ColumnName] = column_mount;
                            }
                            else
                            {
                                //row[dcDebit331.ColumnName] = "";
                            }
                        }
                        row["Cong_No"] = row_sum_debit;
                        #endregion
                        row["no_cuoi"] = (double)fslsf[0].EndDebitBalance > 0 ? (double)fslsf[0].EndDebitBalance : 0;
                        row["co_cuoi"] = (double)fslsf[0].EndCreditBalance > 0 ? (double)fslsf[0].EndCreditBalance : 0;

                        data_table.Rows.Add(row);
                    }
                }
                #endregion

                #region dòng tính tổng
                DataRow row1 = data_table.NewRow();
                row1["ten_don_vi"] = "Tổng Cộng";

                int column_count = data_table.Columns.Count - 1;
                int row_count = data_table.Rows.Count - 1;
                for (int c = 2; c <= column_count; c++)
                {
                    double sumT = 0;
                    for (int r = 1; r <= row_count; r++)
                    {
                        double tt;
                        double.TryParse(data_table.Rows[r][c].ToString(), out tt);
                        sumT += tt;
                    }
                    row1[data_table.Columns[c]] = sumT;
                }

                data_table.Rows.Add(row1);
                #endregion

                #region bind data vào gridview
                xGridView.DataSource = data_table;
                xGridView.DataBind();
                #endregion

            }
            catch
            { }
            #region xuất report
            xGridViewExporter.GridViewID = "xGridView";
            s04a5_dn.printableCC.PrintableComponent = new PrintableComponentLinkBase() { Component = xGridViewExporter };
            ReportViewer.Report = s04a5_dn;
            #endregion
        }
        #endregion

        #region lấy danh sách tài khoản "Nợ" đối ứng
        public DataTable table_temple(int Account, int month, int year, string asset)
        {
            DataTable data_table_temple = new DataTable();
            XPCollection<MonthDim> md = new XPCollection<MonthDim>(session, CriteriaOperator.Parse("Name='" + month + "'"));
            XPCollection<YearDim> yd = new XPCollection<YearDim>(session, CriteriaOperator.Parse("Name='" + year + "'"));
            XPCollection<FinancialAccountDim> fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Name='" + Account + "'"));
            //TK Default
            CorrespondFinancialAccountDim cfad = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);

            if (md.Count != 0 && yd.Count != 0 && fad.Count != 0)
            {
                XPCollection<FinancialSupplierLiabilitySummary_Fact> fslsf =
                    new XPCollection<FinancialSupplierLiabilitySummary_Fact>(
                        session, CriteriaOperator.Parse("MonthDimId='" + md[0].MonthDimId + "' AND YearDimId='" + yd[0].YearDimId + "' AND FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND RowStatus='1'"));
                if (fslsf.Count != 0)
                {
                    foreach (FinancialSupplierLiabilitySummary_Fact each_fslsf in fslsf)
                    {
                        //all credit 331
                        XPCollection<FinancialSupplierLiabilityDetail> fsld = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND Credit>0 AND CorrespondFinancialAccountDimId='" + cfad.CorrespondFinancialAccountDimId + "' AND FinancialSupplierLiabilitySummary_FactId='" + each_fslsf.FinancialSupplierLiabilitySummary_FactId + "' AND RowStatus='1'"));
                        if (fsld.Count != 0)
                        {
                            foreach (FinancialSupplierLiabilityDetail each_fsld in fsld)
                            {
                                //chứa 2 transaction
                                XPCollection<FinancialSupplierLiabilityDetail> fsld1 = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialTransactionDimId='" + each_fsld.FinancialTransactionDimId.FinancialTransactionDimId + "' AND RowStatus='1'"));
                                foreach (FinancialSupplierLiabilityDetail each_fsld1 in fsld1)
                                {
                                    if (each_fsld1.FinancialCustomerLiabilityDetailId != each_fsld.FinancialCustomerLiabilityDetailId)
                                    {
                                        if (each_fsld1.CorrespondFinancialAccountDimId.CorrespondFinancialAccountDimId != cfad.CorrespondFinancialAccountDimId)
                                        {
                                            try
                                            {
                                                data_table_temple.Columns.Add(each_fsld1.CorrespondFinancialAccountDimId.Code);
                                            }
                                            catch { continue; }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return data_table_temple;
        }
        #endregion

        #region lấy danh sách tài khoản "Có" đối ứng
        public DataTable table_temple_debit(int Account, int month, int year, string asset)
        {
            DataTable data_table_temple = new DataTable();
            XPCollection<MonthDim> md = new XPCollection<MonthDim>(session, CriteriaOperator.Parse("Name='" + month + "'"));
            XPCollection<YearDim> yd = new XPCollection<YearDim>(session, CriteriaOperator.Parse("Name='" + year + "'"));
            XPCollection<FinancialAccountDim> fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code='" + Account + "'"));
            //TK Default
            CorrespondFinancialAccountDim cfad = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);


            if (md.Count != 0 && yd.Count != 0 && fad.Count != 0)
            {
                XPCollection<FinancialSupplierLiabilitySummary_Fact> fslsf =
                    new XPCollection<FinancialSupplierLiabilitySummary_Fact>(
                        session, CriteriaOperator.Parse("MonthDimId='" + md[0].MonthDimId + "' AND YearDimId='" + yd[0].YearDimId + "' AND FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND RowStatus='1'"));
                if (fslsf.Count != 0)
                {
                    foreach (FinancialSupplierLiabilitySummary_Fact each_fslsf in fslsf)
                    {
                        // all debit 331
                        XPCollection<FinancialSupplierLiabilityDetail> fsld = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialAccountDimId='" + fad[0].FinancialAccountDimId + "' AND Debit>0 AND CorrespondFinancialAccountDimId='" + cfad.CorrespondFinancialAccountDimId + "' AND FinancialSupplierLiabilitySummary_FactId='" + each_fslsf.FinancialSupplierLiabilitySummary_FactId + "' AND RowStatus='1'"));
                        if (fsld.Count != 0)
                        {
                            foreach (FinancialSupplierLiabilityDetail each_fsld in fsld)
                            {
                                // chứa 2 transaction
                                XPCollection<FinancialSupplierLiabilityDetail> fsld1 = new XPCollection<FinancialSupplierLiabilityDetail>(session, CriteriaOperator.Parse("FinancialTransactionDimId='" + each_fsld.FinancialTransactionDimId.FinancialTransactionDimId + "' AND RowStatus='1'"));
                                foreach (FinancialSupplierLiabilityDetail each_fsld1 in fsld1)
                                {
                                    if (each_fsld1.FinancialCustomerLiabilityDetailId != each_fsld.FinancialCustomerLiabilityDetailId)
                                    {
                                        if (each_fsld1.CorrespondFinancialAccountDimId.CorrespondFinancialAccountDimId != cfad.CorrespondFinancialAccountDimId)
                                        {
                                            try
                                            {
                                                data_table_temple.Columns.Add(each_fsld1.CorrespondFinancialAccountDimId.Code);
                                            }
                                            catch { continue; }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return data_table_temple;
        }
        #endregion

        #region tạo cột cho gridview
        public void GridView_header(int Account, int month, int year, string asset)
        {
            #region
            GridViewBandColumn bandColumn;

            GridViewDataTextColumn fieldColumn = new GridViewDataTextColumn { FieldName = "STT", Caption = "Số TT" };
            xGridView.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "ten_don_vi", Caption = "Tên đơn vị hoặc người bán" };
            xGridView.Columns.Add(fieldColumn);

            bandColumn = new GridViewBandColumn("Số dư đầu tháng");
            fieldColumn = new GridViewDataTextColumn { FieldName = "no_dau", Caption = "Nợ" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "co_dau", Caption = "Có" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            xGridView.Columns.Add(bandColumn);
            #endregion

            #region danh sách cột các tài khoản "Nợ" đối ứng

            bandColumn = new GridViewBandColumn("Ghi Có TK " + Account + ", Ghi Nợ các tài khoản");
            GridViewBandColumn layerBandColumn;
            foreach (DataColumn dt in table_temple(Account, month, year, asset).Columns)
            {
                switch (dt.ColumnName.Substring(0, 3))
                {
                    case "152":
                        layerBandColumn = new GridViewBandColumn(dt.ColumnName);
                        bandColumn.Columns.Add(layerBandColumn);

                        fieldColumn = new GridViewDataTextColumn { FieldName = "" + dt.ColumnName + "_GiaTT", Caption = "Giá TT" };
                        fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                        layerBandColumn.Columns.Add(fieldColumn);

                        fieldColumn = new GridViewDataTextColumn { FieldName = "" + dt.ColumnName + "_GiaHT", Caption = "Giá HT" };
                        fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                        layerBandColumn.Columns.Add(fieldColumn);
                        break;

                    case "153":
                        layerBandColumn = new GridViewBandColumn(dt.ColumnName);
                        bandColumn.Columns.Add(layerBandColumn);

                        fieldColumn = new GridViewDataTextColumn { FieldName = "" + dt.ColumnName + "_GiaTT", Caption = "Giá TT" };
                        fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                        layerBandColumn.Columns.Add(fieldColumn);

                        fieldColumn = new GridViewDataTextColumn { FieldName = "" + dt.ColumnName + "_GiaHT", Caption = "Giá HT" };
                        fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                        layerBandColumn.Columns.Add(fieldColumn);
                        break;

                    default:
                        fieldColumn = new GridViewDataTextColumn { FieldName = dt.ColumnName };
                        fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                        bandColumn.Columns.Add(fieldColumn);
                        break;
                }
            }
            fieldColumn = new GridViewDataTextColumn { FieldName = "Cong_Co", Caption = "Cộng Có TK " + Account + "" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            xGridView.Columns.Add(bandColumn);

            #endregion

            #region danh sách cột các tài khoản "Có" đối ứng

            bandColumn = new GridViewBandColumn("Theo dõi thanh toán (Ghi Nợ TK " + Account + ")");
            foreach (DataColumn dc in table_temple_debit(Account, month, year, asset).Columns)
            {
                fieldColumn = new GridViewDataTextColumn { FieldName = dc.ColumnName + " " };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(fieldColumn);
            }
            fieldColumn = new GridViewDataTextColumn { FieldName = "Cong_No", Caption = "Cộng Nợ TK " + Account + "" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            xGridView.Columns.Add(bandColumn);

            #endregion

            #region
            bandColumn = new GridViewBandColumn("Số dư cuối tháng");
            fieldColumn = new GridViewDataTextColumn { FieldName = "no_cuoi", Caption = "Nợ" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);

            fieldColumn = new GridViewDataTextColumn { FieldName = "co_cuoi", Caption = "Có" };
            fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(fieldColumn);
            xGridView.Columns.Add(bandColumn);
            #endregion
        }
        #endregion

        #region datatable dùng đổ dữ liệu
        public DataTable table_pri(int Account, int month, int year, string asset)
        {
            DataTable data_table = new DataTable();
            data_table.Columns.Add("STT");
            data_table.Columns.Add("ten_don_vi");
            data_table.Columns.Add("no_dau", typeof(double));
            data_table.Columns.Add("co_dau", typeof(double));

            #region Có TK 331

            foreach (DataColumn dt in table_temple(Account, month, year, asset).Columns)
            {
                try
                {
                    switch (dt.ColumnName.Substring(0, 3))
                    {
                        case "152":
                            data_table.Columns.Add("" + dt.ColumnName + "_GiaTT", typeof(double));
                            data_table.Columns.Add("" + dt.ColumnName + "_GiaHT", typeof(double));
                            break;
                        case "153":
                            data_table.Columns.Add("" + dt.ColumnName + "_GiaTT", typeof(double));
                            data_table.Columns.Add("" + dt.ColumnName + "_GiaHT", typeof(double));
                            break;
                        default:
                            data_table.Columns.Add(dt.ColumnName, typeof(double));
                            break;
                    }
                }
                catch
                {
                    continue;
                }
            }
            data_table.Columns.Add("Cong_Co", typeof(double));

            #endregion

            #region Nợ TK 331

            foreach (DataColumn dc in table_temple_debit(Account, month, year, asset).Columns)
            {
                try
                {
                    data_table.Columns.Add(dc.ColumnName + " ", typeof(double));
                }
                catch
                {
                    continue;
                }
            }
            data_table.Columns.Add("Cong_No", typeof(double));

            #endregion

            data_table.Columns.Add("no_cuoi", typeof(double));
            data_table.Columns.Add("co_cuoi", typeof(double));
            return data_table;
        }
        #endregion

        #region dùng xét dữ liệu
        public DataTable table_Credit_331(int Account, int month, int year, string asset)
        {
            DataTable data_table = new DataTable();
            foreach (DataColumn dt in table_temple(Account, month, year, asset).Columns)
            {
                try
                {
                    switch (dt.ColumnName.Substring(0, 3))
                    {
                        case "152":
                            data_table.Columns.Add("" + dt.ColumnName + "_GiaTT");
                            data_table.Columns.Add("" + dt.ColumnName + "_GiaHT");
                            break;
                        case "153":
                            data_table.Columns.Add("" + dt.ColumnName + "_GiaTT");
                            data_table.Columns.Add("" + dt.ColumnName + "_GiaHT");
                            break;
                        default:
                            data_table.Columns.Add(dt.ColumnName);
                            break;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return data_table;
        }
        #endregion

        #region dùng xét dữ liệu
        public DataTable table_Debit_331(int Account, int month, int year, string asset)
        {
            DataTable data_table = new DataTable();
            foreach (DataColumn dc in table_temple_debit(Account, month, year, asset).Columns)
            {
                try
                {
                    data_table.Columns.Add(dc.ColumnName + " ");
                }
                catch
                {
                    continue;
                }
            }
            return data_table;
        }
        #endregion

        protected void xGridViewExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            GridViewDataColumn dataColumn = e.Column as GridViewDataColumn;
            //if (e.RowType == GridViewRowType.Data && dataColumn.FieldName == "no_dau")
            //{
            //    if (e.Text == "")
            //    {
            //        e.Text = "0";
            //    }
            //}
        }
    }
}