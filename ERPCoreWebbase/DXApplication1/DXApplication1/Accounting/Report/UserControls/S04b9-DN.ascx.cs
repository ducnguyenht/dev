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
using DevExpress.XtraPrintingLinks;
using NAS.DAL.BI.Accounting.Account;
using DevExpress.Data.Filtering;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using DevExpress.Xpo.Metadata;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04b9_DN : System.Web.UI.UserControl
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS04b9dn.Contains("show"))
            {
                load_data();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region load data
        public void load_data()
        {
            WebModule.Accounting.Report.S04b9_DN s04b9_dn = new Report.S04b9_DN();
            //try
            //{
            #region tham số truyền
            int month = Int32.Parse(this.hS04b9DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b9DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            //string asset = "";
            s04b9_dn.xrMonth.Text = month.ToString();
            s04b9_dn.xrYear.Text = year.ToString();

            #endregion

            #region Id
            XPCollection<FinancialAccountDim> FinancialAccountDim_155 = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code like '155%'"));
            XPCollection<FinancialAccountDim> FinancialAccountDim_156 = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code like '156%'"));
            XPCollection<FinancialAccountDim> FinancialAccountDim_158 = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code like '158%'"));

            //

            //


            int MonthDimId = session.FindObject<MonthDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", month))).MonthDimId;
            int YearDimId = session.FindObject<YearDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", year))).YearDimId;
            int OwnerOrgDimId = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(String.Format("Code='{0}'", owner))).OwnerOrgDimId;
            int CorrespondFinancialAccountDimId_default = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT).CorrespondFinancialAccountDimId;
            FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact = null;
            #endregion

            #region header và table báo cáo
            grid_header();
            DataTable datatable = table_pri();
            #endregion

            #region danh sách đối ứng
            // danh sach doi ung 155
            List<string> list_Correspond_155_156_158 = new List<string>();
            if (FinancialAccountDim_155.Count != 0)
            {
                foreach (FinancialAccountDim each_FinancialAccountDim_155 in FinancialAccountDim_155)
                {
                    FinancialActualPriceSummary_Fact = FinancialActualPriceSummary_Fact_155_156_158(each_FinancialAccountDim_155.FinancialAccountDimId,
                        OwnerOrgDimId, YearDimId, MonthDimId);
                    list_correstpond(FinancialActualPriceSummary_Fact, each_FinancialAccountDim_155.FinancialAccountDimId, CorrespondFinancialAccountDimId_default, list_Correspond_155_156_158);
                }
            }

            // danh sach doi ung 156
            if (FinancialAccountDim_156.Count != 0)
            {
                foreach (FinancialAccountDim each_FinancialAccountDim_156 in FinancialAccountDim_156)
                {
                    FinancialActualPriceSummary_Fact = FinancialActualPriceSummary_Fact_155_156_158(each_FinancialAccountDim_156.FinancialAccountDimId,
                        OwnerOrgDimId, YearDimId, MonthDimId);
                    list_correstpond(FinancialActualPriceSummary_Fact, each_FinancialAccountDim_156.FinancialAccountDimId, CorrespondFinancialAccountDimId_default, list_Correspond_155_156_158);
                }
            }

            // danh sach doi ung 158
            if (FinancialAccountDim_158.Count != 0)
            {
                foreach (FinancialAccountDim each_FinancialAccountDim_158 in FinancialAccountDim_158)
                {
                    FinancialActualPriceSummary_Fact = FinancialActualPriceSummary_Fact_155_156_158(each_FinancialAccountDim_158.FinancialAccountDimId,
                        OwnerOrgDimId, YearDimId, MonthDimId);
                    list_correstpond(FinancialActualPriceSummary_Fact, each_FinancialAccountDim_158.FinancialAccountDimId, CorrespondFinancialAccountDimId_default, list_Correspond_155_156_158);
                }
            }
            list_Correspond_155_156_158.Sort();
            #endregion

            #region đổ dữ liệu
            #region dòng tĩnh
            // dòng 2
            DataRow dr = datatable.NewRow();
            dr["stt"] = 1;
            dr["chi_tieu"] = "I. Số dư đầu tháng";
            foreach (string each_column_TT in column_gridview())
            {
                //FinancialActualPriceSummary_Fact fapsf = FinancialActualPriceSummary_Fact_155_156_158(FinancialAccountDimId(each_column_TT), OwnerOrgDimId, YearDimId, MonthDimId);
                //dr[each_column_TT + "TT"] = fapsf != null ? fapsf.BeginDebitBalance : 0;

                double BeginDebitBalance = 0;
                XPCollection<FinancialAccountDim> FinancialAccountDim_all = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse(String.Format("Code like '{0}%'", each_column_TT)));
                if (FinancialAccountDim_all.Count != 0)
                {
                    foreach (FinancialAccountDim each_FinancialAccountDim_all in FinancialAccountDim_all)
                    {
                        //
                        int FinancialAccountDimId = session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Code='" + each_FinancialAccountDim_all.Code + "'")).FinancialAccountDimId;
                        FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact_15 = session.FindObject<FinancialActualPriceSummary_Fact>(CriteriaOperator.Parse("FinancialAccountDimId='" + FinancialAccountDimId + "' AND OwnerOrgDimId='" + OwnerOrgDimId + "' AND YearDimId='" + YearDimId + "' AND MonthDimId='" + MonthDimId + "' AND RowStatus='1'"));
                        if (FinancialActualPriceSummary_Fact_15 != null)
                        {
                            BeginDebitBalance += (double)FinancialActualPriceSummary_Fact_15.BeginDebitBalance;
                        }
                    }
                }
                dr[each_column_TT + "TT"] = BeginDebitBalance;

            }
            datatable.Rows.Add(dr);

            // dòng 3
            dr = datatable.NewRow();
            dr["stt"] = 2;
            dr["chi_tieu"] = "II. Số phát sinh trong tháng";
            datatable.Rows.Add(dr);
            #endregion

            #region dòng động
            int STTu = 3;
            foreach (string each_TK in list_Correspond_155_156_158)
            {
                dr = datatable.NewRow();
                dr["stt"] = STTu++;

                #region cột chỉ tiêu
                if (each_TK == "111")
                {
                    dr["chi_tieu"] = "    Từ NKCT số 1 (Ghi có TK " + each_TK + ")";
                }
                else if (each_TK == "112")
                {
                    dr["chi_tieu"] = "    Từ NKCT số 2 (Ghi có TK " + each_TK + ")";
                }
                else if (each_TK == "113")
                {
                    dr["chi_tieu"] = "    Từ NKCT số 3 (Ghi có TK " + each_TK + ")";
                }
                else if (each_TK == "331")
                {
                    dr["chi_tieu"] = "    Từ NKCT số 5 (Ghi có TK " + each_TK + ")";
                }
                else if (each_TK == "151")
                {
                    dr["chi_tieu"] = "    Từ NKCT số 6 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT4().Contains(each_TK))
                {
                    dr["chi_tieu"] = "    Từ NKCT số 4 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT7().Contains(each_TK))
                {
                    dr["chi_tieu"] = "    Từ NKCT số 7 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT8().Contains(each_TK))
                {
                    dr["chi_tieu"] = "    Từ NKCT số 8 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT9().Contains(each_TK))
                {
                    dr["chi_tieu"] = "    Từ NKCT số 9 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT10().Contains(each_TK))
                {
                    dr["chi_tieu"] = "    Từ NKCT số 10 (Ghi có TK " + each_TK + ")";
                }
                else
                {
                    dr["chi_tieu"] = "    Ghi có TK " + each_TK + "";
                }
                #endregion

                #region cột dữ liệu
                foreach (string each_column in column_gridview())
                {
                    XPCollection<CorrespondFinancialAccountDim> cfad = new XPCollection<CorrespondFinancialAccountDim>(session, CriteriaOperator.Parse(String.Format("Code like '{0}%'", each_TK)));
                    double column_mount = 0;
                    if (cfad.Count != 0)
                    {
                        foreach (CorrespondFinancialAccountDim each_cfad in cfad)
                        {
                            //all TK header
                            XPCollection<FinancialAccountDim> FinancialAccountDim_all = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse(String.Format("Code like '{0}%'", each_column)));
                            if (FinancialAccountDim_all.Count != 0)
                            {
                                foreach (FinancialAccountDim each_FinancialAccountDim_all in FinancialAccountDim_all)
                                {
                                    //
                                    FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact_15 =
                                    FinancialActualPriceSummary_Fact_155_156_158(FinancialAccountDimId(each_FinancialAccountDim_all.Code), OwnerOrgDimId, YearDimId, MonthDimId);
                                    if (FinancialActualPriceSummary_Fact_15 != null)
                                    {

                                        XPCollection<FinancialActualPriceDetail> fapd_15 = new XPCollection<FinancialActualPriceDetail>(session, CriteriaOperator.Parse(String.Format("FinancialAccountDimId={0} AND FinancialActualPriceSummary_FactId='{1}' AND CorrespondFinancialAccountDimId={2} AND Credit>0 AND RowStatus='1'",
                                            FinancialAccountDimId(each_FinancialAccountDim_all.Code),
                                            FinancialActualPriceSummary_Fact_15.FinancialActualPriceSummary_FactId,
                                            each_cfad.CorrespondFinancialAccountDimId
                                            )));

                                        if (fapd_15.Count != 0)
                                        {
                                            foreach (FinancialActualPriceDetail each_fapd_155 in fapd_15)
                                            {
                                                column_mount += (double)each_fapd_155.Credit;
                                            }
                                        }
                                    }
                                    //
                                }
                            }
                        }
                    }

                    if (column_mount != 0)
                    {
                        dr["" + each_column + "TT"] = column_mount;
                    }
                }
                datatable.Rows.Add(dr);
                #endregion
            }
            #endregion

            #region row Cộng số dư đầu tháng và phát sinh trong tháng
            dr = datatable.NewRow();
            dr["stt"] = STTu++;
            dr["chi_tieu"] = "III. Cộng số dư đầu tháng và phát sinh trong tháng(I+II)";
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

            #region row Hệ số chênh lệch
            dr = datatable.NewRow();
            dr["stt"] = STTu++;
            dr["chi_tieu"] = "IV. Hệ số chênh lệch";
            datatable.Rows.Add(dr);
            #endregion

            #region row Xuất dùng trong tháng
            dr = datatable.NewRow();
            dr["stt"] = STTu++;
            dr["chi_tieu"] = "V. Xuất dùng trong tháng";

            //Nợ - tài khoản 6-152
            XPCollection<CorrespondFinancialAccountDim> cfad_TK6 = new XPCollection<CorrespondFinancialAccountDim>(session, CriteriaOperator.Parse("Code like '6%'"));
            XPCollection<CorrespondFinancialAccountDim> cfad_TK8 = new XPCollection<CorrespondFinancialAccountDim>(session, CriteriaOperator.Parse("Code like '8%'"));

            foreach (string name_155_156_158 in column_gridview())
            {
                double debit_TK6_TK8 = 0;
                // all TK 
                XPCollection<FinancialAccountDim> FinancialAccountDim_all = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse(String.Format("Code like '{0}%'", name_155_156_158)));
                if (FinancialAccountDim_all.Count != 0)
                {
                    foreach (FinancialAccountDim each_FinancialAccountDim_all in FinancialAccountDim_all)
                    {
                        //
                        int FinancialAccountDimId = session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Code='" + each_FinancialAccountDim_all.Code + "'")).FinancialAccountDimId;
                        FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact_15 = session.FindObject<FinancialActualPriceSummary_Fact>(CriteriaOperator.Parse("FinancialAccountDimId='" + FinancialAccountDimId + "' AND OwnerOrgDimId='" + OwnerOrgDimId + "' AND YearDimId='" + YearDimId + "' AND MonthDimId='" + MonthDimId + "' AND RowStatus='1'"));
                        //if (FinancialActualPriceSummary_Fact_15 != null)
                        //{
                        //    //TK6
                        //    if (cfad_TK6.Count != 0)
                        //    {
                        //        foreach (CorrespondFinancialAccountDim tk6 in cfad_TK6)
                        //        {
                        //            XPCollection<FinancialActualPriceDetail> details = new XPCollection<FinancialActualPriceDetail>(session, CriteriaOperator.Parse("FinancialActualPriceSummary_FactId='" + FinancialActualPriceSummary_Fact_15.FinancialActualPriceSummary_FactId + "' AND CorrespondFinancialAccountDimId='" + tk6.CorrespondFinancialAccountDimId + "' AND Debit>0 AND RowStatus='1'"));
                        //            if (details.Count != 0)
                        //            {
                        //                foreach (FinancialActualPriceDetail detail in details)
                        //                {
                        //                    debit_TK6_TK8 += (double)detail.Debit;
                        //                }
                        //            }
                        //        }
                        //    }
                        //    //TK8
                        //    if (cfad_TK8.Count != 0)
                        //    {
                        //        foreach (CorrespondFinancialAccountDim tk8 in cfad_TK8)
                        //        {
                        //            XPCollection<FinancialActualPriceDetail> details = new XPCollection<FinancialActualPriceDetail>(session, CriteriaOperator.Parse("FinancialActualPriceSummary_FactId='" + FinancialActualPriceSummary_Fact_15.FinancialActualPriceSummary_FactId + "' AND CorrespondFinancialAccountDimId='" + tk8.CorrespondFinancialAccountDimId + "' AND Debit>0 AND RowStatus='1'"));
                        //            if (details.Count != 0)
                        //            {
                        //                foreach (FinancialActualPriceDetail detail in details)
                        //                {
                        //                    debit_TK6_TK8 += (double)detail.Debit;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //
                        if (FinancialActualPriceSummary_Fact_15 != null)
                        {
                            debit_TK6_TK8 += (double)FinancialActualPriceSummary_Fact_15.CreditSum;
                        }
                    }
                }

                dr["" + name_155_156_158 + "TT"] = debit_TK6_TK8;
            }
            datatable.Rows.Add(dr);
            #endregion

            #region row Tồn kho cuối tháng
            dr = datatable.NewRow();
            dr["stt"] = STTu;
            dr["chi_tieu"] = "VI. Tồn kho cuối tháng(III-V)";
            foreach (string name_155_156_158 in column_gridview())
            {
                //double TK_III = (from DataRow dr1 in datatable.Rows where dr1["chi_tieu"] == "III. Cộng số dư đầu tháng và phát sinh trong tháng(I+II)" select (double)dr1["" + name_155_156_158 + "TT"]).FirstOrDefault();
                //double TK_V = (from DataRow dr1 in datatable.Rows where dr1["chi_tieu"] == "V. Xuất dùng trong tháng" select (double)dr1["" + name_155_156_158 + "TT"]).FirstOrDefault();
                //dr["" + name_155_156_158 + "TT"] = TK_III - TK_V;


                double debit_TK6_TK8 = 0;
                XPCollection<FinancialAccountDim> FinancialAccountDim_all = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse(String.Format("Code like '{0}%'", name_155_156_158)));
                if (FinancialAccountDim_all.Count != 0)
                {
                    foreach (FinancialAccountDim each_FinancialAccountDim_all in FinancialAccountDim_all)
                    {
                        //
                        int FinancialAccountDimId = session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Code='" + each_FinancialAccountDim_all.Code + "'")).FinancialAccountDimId;
                        FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact_15 = session.FindObject<FinancialActualPriceSummary_Fact>(CriteriaOperator.Parse("FinancialAccountDimId='" + FinancialAccountDimId + "' AND OwnerOrgDimId='" + OwnerOrgDimId + "' AND YearDimId='" + YearDimId + "' AND MonthDimId='" + MonthDimId + "' AND RowStatus='1'"));
                        if (FinancialActualPriceSummary_Fact_15 != null)
                        {
                            debit_TK6_TK8 += (double)FinancialActualPriceSummary_Fact_15.EndDebitBalance;
                        }
                    }
                }
                dr["" + name_155_156_158 + "TT"] = debit_TK6_TK8;
            }
            datatable.Rows.Add(dr);
            #endregion
            #endregion

            GridView_S04b9DN.DataSource = datatable;
            GridView_S04b9DN.DataBind();
            //}
            //catch
            //{
            //}
            #region export report
            s04b9_dn.printableCC_S04b9DN.PrintableComponent = new PrintableComponentLinkBase() { Component = GridViewExporter_S04b9DN };
            ReportViewer_S04b9DN.Report = s04b9_dn;
            #endregion
        }
        #endregion

        // danh sách cột gridview
        public List<string> column_gridview()
        {
            List<string> column_gridview = new List<string>();
            // collection 155
            XPCollection<FinancialAccountDim> fad;
            //fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Name like '155%'"));
            fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code='155'"));
            if (fad.Count != 0)
            {
                foreach (FinancialAccountDim each_fad_155 in fad)
                {
                    //if (each_fad_155.Name != "155")
                    //{
                    if (!column_gridview.Contains(each_fad_155.Code))
                    {
                        column_gridview.Add(each_fad_155.Code);
                    }
                    //}
                }
            }
            // collection_156
            //fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Name like '156%'"));
            fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code='156'"));
            if (fad.Count != 0)
            {
                foreach (FinancialAccountDim each_fad_156 in fad)
                {
                    //if (each_fad_156.Name != "156")
                    //{
                    if (!column_gridview.Contains(each_fad_156.Code))
                    {
                        column_gridview.Add(each_fad_156.Code);
                    }
                    //}
                }
            }
            // collection_158
            //fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Name like '158%'"));
            fad = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code='158'"));
            if (fad.Count != 0)
            {
                foreach (FinancialAccountDim each_fad_158 in fad)
                {
                    //if (each_fad_158.Name != "158")
                    //{
                    if (!column_gridview.Contains(each_fad_158.Code))
                    {
                        column_gridview.Add(each_fad_158.Code);
                    }
                    //}
                }
            }
            column_gridview.Sort();
            return column_gridview;
        }

        //GridView header column
        public void grid_header()
        {
            GridViewBandColumn bandColumn;

            GridViewDataTextColumn fieldColumn = new GridViewDataTextColumn { FieldName = "stt", Caption = "Số TT" };
            GridView_S04b9DN.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "chi_tieu", Caption = "Chỉ tiêu" };
            GridView_S04b9DN.Columns.Add(fieldColumn);

            foreach (string each_155_156_158 in column_gridview())
            {
                string last = "";
                switch (each_155_156_158)
                {
                    case "155":
                        last = "Thành phẩm";
                        break;
                    case "156":
                        last = "Hàng hóa";
                        break;
                    case "158":
                        last = "Hàng hóa kho bảo thuế";
                        break;
                }

                bandColumn = new GridViewBandColumn("TK " + each_155_156_158 + " - " + last + "");
                fieldColumn = new GridViewDataTextColumn { FieldName = "" + each_155_156_158 + "HT", Caption = "Giá hạch toán" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(fieldColumn);

                fieldColumn = new GridViewDataTextColumn { FieldName = "" + each_155_156_158 + "TT", Caption = "Giá thực tế" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(fieldColumn);
                GridView_S04b9DN.Columns.Add(bandColumn);
            }
        }

        //Table data
        public DataTable table_pri()
        {
            DataTable table_pri = new DataTable();
            table_pri.Columns.Add("stt");
            table_pri.Columns.Add("chi_tieu");
            foreach (string each_155_156_158 in column_gridview())
            {
                table_pri.Columns.Add("" + each_155_156_158 + "HT", typeof(double));
                table_pri.Columns.Add("" + each_155_156_158 + "TT", typeof(double));
            }

            // first row
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

        // get danh sách đối ứng
        public void list_correstpond(FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact,
            int FinancialAccountDimId, int CorrespondFinancialAccountDimId_default, List<string> list_Correspond_155_156_158)
        {
            if (FinancialActualPriceSummary_Fact != null)
            {
                XPCollection<FinancialActualPriceDetail> Colection_Correspond =
                new XPCollection<FinancialActualPriceDetail>(session, CriteriaOperator.Parse(String.Format("FinancialAccountDimId='{0}' AND FinancialActualPriceSummary_FactId='{1}' AND CorrespondFinancialAccountDimId!='{2}' AND Credit>0 AND RowStatus='1'",
                    FinancialAccountDimId,
                    FinancialActualPriceSummary_Fact.FinancialActualPriceSummary_FactId,
                    CorrespondFinancialAccountDimId_default)));
                if (Colection_Correspond.Count != 0)
                {
                    foreach (FinancialActualPriceDetail each_Colection_Correspond in Colection_Correspond)
                    {
                        if (!list_Correspond_155_156_158.Contains(each_Colection_Correspond.CorrespondFinancialAccountDimId.Code.Substring(0, 3)))
                        {
                            list_Correspond_155_156_158.Add(each_Colection_Correspond.CorrespondFinancialAccountDimId.Code.Substring(0, 3));
                        }
                    }
                }
            }
        }


        // get  FinancialActualPriceSummary_Fact
        public FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact_155_156_158(int FinancialAccountDimId,
            int OwnerOrgDimId, int YearDimId, int MonthDimId
            )
        {
            FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact = session.FindObject<FinancialActualPriceSummary_Fact>(CriteriaOperator.Parse(String.Format("FinancialAccountDimId={0} AND OwnerOrgDimId={1} AND YearDimId={2} AND MonthDimId={3} AND RowStatus='1'",
                            FinancialAccountDimId,
                            OwnerOrgDimId,
                            YearDimId,
                            MonthDimId
                            )));
            return FinancialActualPriceSummary_Fact;
        }


        //get FinancialAccountDimId
        public int FinancialAccountDimId(string Code)
        {
            return session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Code='" + Code + "'")).FinancialAccountDimId;
        }

        #region list nhật ký chứng từ
        //List nhật ký chứng từ số 4
        public List<string> list_NKCT4()
        {
            List<string> list_NKCT4 = new List<string>();
            list_NKCT4.AddRange(new string[] { "311", "315", "341", "342", "343" });
            return list_NKCT4;
        }

        //List nhật ký chứng từ số 7
        public List<string> list_NKCT7()
        {
            List<string> list_NKCT7 = new List<string>();
            list_NKCT7.AddRange(new string[] { "142", "152", "153", "154", "214", "241", "242", "334", "335", "338", "351", "352", "611", "621", "622", "623", "627", "631" });
            return list_NKCT7;
        }

        //List nhật ký chứng từ số 8
        public List<string> list_NKCT8()
        {
            List<string> list_NKCT8 = new List<string>();
            list_NKCT8.AddRange(new string[] { "155", "156", "157", "158", "159", "131", "511", "512", "515", "521", "531", "532", "632", "635", "641", "642", "711", "811", "821", "911" });
            return list_NKCT8;
        }

        //List nhật ký chứng từ số 9
        public List<string> list_NKCT9()
        {
            List<string> list_NKCT9 = new List<string>();
            list_NKCT9.AddRange(new string[] { "211", "212", "213", "217" });
            return list_NKCT9;
        }

        //List nhật ký chứng từ số 10
        public List<string> list_NKCT10()
        {
            List<string> list_NKCT10 = new List<string>();
            list_NKCT10.AddRange(new string[] { "121","128","129","136","138","139","141","144","161","221","222","223","228","229","243",
                                    "244","333","336","338","344","347","411","412","413","414","415","418","419","421","431","441","461","466" });
            return list_NKCT10;
        }
        #endregion
    }
}