using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.XtraPrintingLinks;
using System.Data;
using DevExpress.Web.ASPxGridView;
using NAS.DAL.BI.Accounting.Account;
using DevExpress.Data.Filtering;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using NAS.BO.ETL.Accounting;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04b3_DN : System.Web.UI.UserControl
    {
        #region first
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS04b3dn.Contains("show"))
            {
                load_data();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region load data
        public void load_data()
        {
            WebModule.Accounting.Report.S04b3_DN s04b3_dn = new Report.S04b3_DN();
            //try
            //{
            #region tham số truyền
            int month = Int32.Parse(this.hS04b3DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b3DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            //string asset = "";
            s04b3_dn.xrMonth.Text = month.ToString();
            s04b3_dn.xrYear.Text = year.ToString();

            #endregion

            #region Id
            XPCollection<FinancialAccountDim> FinancialAccountDim_152 = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code like '152%' AND Code!='152'"));
            XPCollection<FinancialAccountDim> FinancialAccountDim_153 = new XPCollection<FinancialAccountDim>(session, CriteriaOperator.Parse("Code like '153%' AND Code!='153'"));
            //int FinancialAccountDimId_152 = session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Name='152'")).FinancialAccountDimId;
            //int FinancialAccountDimId_153 = session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Name='153'")).FinancialAccountDimId;
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
            // danh sach doi ung 152
            List<string> list_Correspond_152_153 = new List<string>();
            if (FinancialAccountDim_152.Count != 0)
            {
                foreach (FinancialAccountDim each_FinancialAccountDim_152 in FinancialAccountDim_152)
                {
                    FinancialActualPriceSummary_Fact = FinancialActualPriceSummary_Fact_152_153(each_FinancialAccountDim_152.FinancialAccountDimId,
                        OwnerOrgDimId, YearDimId, MonthDimId);
                    list_correstpond(FinancialActualPriceSummary_Fact, each_FinancialAccountDim_152.FinancialAccountDimId, CorrespondFinancialAccountDimId_default, list_Correspond_152_153);
                }
            }

            // danh sach doi ung 153
            if (FinancialAccountDim_153.Count != 0)
            {
                foreach (FinancialAccountDim each_FinancialAccountDim_153 in FinancialAccountDim_153)
                {
                    FinancialActualPriceSummary_Fact = FinancialActualPriceSummary_Fact_152_153(each_FinancialAccountDim_153.FinancialAccountDimId,
                        OwnerOrgDimId, YearDimId, MonthDimId);
                    list_correstpond(FinancialActualPriceSummary_Fact, each_FinancialAccountDim_153.FinancialAccountDimId, CorrespondFinancialAccountDimId_default, list_Correspond_152_153);
                }
            }
            list_Correspond_152_153.Sort();
            #endregion

            #region đổ dữ liệu
            #region dòng tĩnh
            // dòng 2
            DataRow dr = datatable.NewRow();
            dr["stt"] = 1;
            dr["chi_tieu"] = "I. Số dư đầu tháng";
            foreach (string each_column_TT in column_gridview())
            {
                FinancialActualPriceSummary_Fact fapsf = FinancialActualPriceSummary_Fact_152_153(FinancialAccountDimId(each_column_TT), OwnerOrgDimId, YearDimId, MonthDimId);
                dr[each_column_TT + "TT"] = fapsf != null ? fapsf.BeginDebitBalance : 0;

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
            // từng dòng
            foreach (string each_TK in list_Correspond_152_153)
            {
                dr = datatable.NewRow();
                dr["stt"] = STTu++;

                #region cột chỉ tiêu
                if (each_TK == "111")
                {
                    dr["chi_tieu"] = "Từ NKCT số 1 (Ghi có TK " + each_TK + ")";
                }
                else if (each_TK == "112")
                {
                    dr["chi_tieu"] = "Từ NKCT số 2 (Ghi có TK " + each_TK + ")";
                }
                else if (each_TK == "113")
                {
                    dr["chi_tieu"] = "Từ NKCT số 3 (Ghi có TK " + each_TK + ")";
                }
                else if (each_TK == "331")
                {
                    dr["chi_tieu"] = "Từ NKCT số 5 (Ghi có TK " + each_TK + ")";
                }
                else if (each_TK == "151")
                {
                    dr["chi_tieu"] = "Từ NKCT số 6 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT4().Contains(each_TK))
                {
                    dr["chi_tieu"] = "Từ NKCT số 4 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT7().Contains(each_TK))
                {
                    dr["chi_tieu"] = "Từ NKCT số 7 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT8().Contains(each_TK))
                {
                    dr["chi_tieu"] = "Từ NKCT số 8 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT9().Contains(each_TK))
                {
                    dr["chi_tieu"] = "Từ NKCT số 9 (Ghi có TK " + each_TK + ")";
                }
                else if (list_NKCT10().Contains(each_TK))
                {
                    dr["chi_tieu"] = "Từ NKCT số 10 (Ghi có TK " + each_TK + ")";
                }
                else
                {
                    dr["chi_tieu"] = "Ghi có TK " + each_TK + "";
                }
                #endregion

                #region cột dữ liệu
                // từng cột
                foreach (string each_column in column_gridview())
                {
                    XPCollection<CorrespondFinancialAccountDim> cfad = new XPCollection<CorrespondFinancialAccountDim>(session, CriteriaOperator.Parse(String.Format("Code like '{0}%'", each_TK)));
                    double column_mount = 0;
                    if (cfad.Count != 0)
                    {
                        foreach (CorrespondFinancialAccountDim each_cfad in cfad)
                        {
                            FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact_15 =
                                FinancialActualPriceSummary_Fact_152_153(FinancialAccountDimId(each_column), OwnerOrgDimId, YearDimId, MonthDimId);
                            if (FinancialActualPriceSummary_Fact_15 != null)
                            {

                                XPCollection<FinancialActualPriceDetail> fapd_15 = new XPCollection<FinancialActualPriceDetail>(session, CriteriaOperator.Parse(String.Format("FinancialAccountDimId={0} AND FinancialActualPriceSummary_FactId='{1}' AND CorrespondFinancialAccountDimId={2} AND Credit>0 AND RowStatus='1'",
                                    FinancialAccountDimId(each_column),
                                    FinancialActualPriceSummary_Fact_15.FinancialActualPriceSummary_FactId,
                                    each_cfad.CorrespondFinancialAccountDimId
                                    )));

                                if (fapd_15.Count != 0)
                                {
                                    foreach (FinancialActualPriceDetail each_fapd_152 in fapd_15)
                                    {
                                        column_mount += (double)each_fapd_152.Credit;
                                    }
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


            //XPCollection<CorrespondFinancialAccountDim> cfad_TK6 = new XPCollection<CorrespondFinancialAccountDim>(session, CriteriaOperator.Parse("Code like '6%'"));
            //XPCollection<CorrespondFinancialAccountDim> cfad_TK8 = new XPCollection<CorrespondFinancialAccountDim>(session, CriteriaOperator.Parse("Code like '8%'"));

            foreach (string name_152_153 in column_gridview())
            {
                //double debit_TK6_TK8_152 = 0;
                int FinancialAccountDimId = session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Code='" + name_152_153 + "'")).FinancialAccountDimId;
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
                //                    debit_TK6_TK8_152 += (double)detail.Debit;
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
                //                    debit_TK6_TK8_152 += (double)detail.Debit;
                //                }
                //            }
                //        }
                //    }
                //}
                //dr["" + name_152_153 + "TT"] = debit_TK6_TK8_152;
                dr["" + name_152_153 + "TT"] = FinancialActualPriceSummary_Fact_15.CreditSum;
            }
            datatable.Rows.Add(dr);
            #endregion

            #region row Tồn kho cuối tháng
            dr = datatable.NewRow();
            dr["stt"] = STTu;
            dr["chi_tieu"] = "VI. Tồn kho cuối tháng(III-V)";
            foreach (string name_152_153 in column_gridview())
            {
                //double TK_III = (from DataRow dr1 in datatable.Rows where dr1["chi_tieu"] == "III. Cộng số dư đầu tháng và phát sinh trong tháng(I+II)" select (double)dr1["" + name_152_153 + "TT"]).FirstOrDefault();
                //double TK_V = (from DataRow dr1 in datatable.Rows where dr1["chi_tieu"] == "V. Xuất dùng trong tháng" select (double)dr1["" + name_152_153 + "TT"]).FirstOrDefault();
                //dr["" + name_152_153 + "TT"] = TK_III - TK_V;
                int FinancialAccountDimId = session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Code='" + name_152_153 + "'")).FinancialAccountDimId;
                FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact_15 = session.FindObject<FinancialActualPriceSummary_Fact>(CriteriaOperator.Parse("FinancialAccountDimId='" + FinancialAccountDimId + "' AND OwnerOrgDimId='" + OwnerOrgDimId + "' AND YearDimId='" + YearDimId + "' AND MonthDimId='" + MonthDimId + "' AND RowStatus='1'"));
                dr["" + name_152_153 + "TT"] = FinancialActualPriceSummary_Fact_15.EndDebitBalance;
            }
            datatable.Rows.Add(dr);
            #endregion
            #endregion

            GridView_S04b3DN.DataSource = datatable;
            GridView_S04b3DN.DataBind();
            //}
            //catch
            //{
            //}
            #region export report
            s04b3_dn.printableCC_S04b3DN.PrintableComponent = new PrintableComponentLinkBase() { Component = GridViewExporter_S04b3DN };
            ReportViewer_S04b3DN.Report = s04b3_dn;
            #endregion
        }
        #endregion

        // danh sách cột gridview
        public List<string> column_gridview()
        {
            List<string> column_gridview = new List<string>();

            #region tham số truyền
            int month = Int32.Parse(this.hS04b3DN_month.Get("month_id").ToString());
            int year = Int32.Parse(this.hS04b3DN_year.Get("year_id").ToString());
            string owner = "QUASAPHARCO";
            #endregion

            #region Id
            MonthDim MonthDimId = session.FindObject<MonthDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", month)));
            YearDim YearDimId = session.FindObject<YearDim>(CriteriaOperator.Parse(String.Format("Name='{0}'", year)));
            OwnerOrgDim OwnerOrgDimId = session.FindObject<OwnerOrgDim>(CriteriaOperator.Parse(String.Format("Code='{0}'", owner)));
            #endregion

            XPCollection<FinancialActualPriceSummary_Fact> collec_fact =
                new XPCollection<FinancialActualPriceSummary_Fact>(session, CriteriaOperator.Parse(
                    String.Format("MonthDimId='{0}' AND "
                    + "YearDimId='{1}' AND "
                    + "OwnerOrgDimId='{2}' AND "
                    + "RowStatus='1'",
                    MonthDimId.MonthDimId,
                    YearDimId.YearDimId,
                    OwnerOrgDimId.OwnerOrgDimId
                    )));
            if (collec_fact.Count != 0)
            {
                foreach (FinancialActualPriceSummary_Fact each_fact in collec_fact)
                {
                    if (each_fact.FinancialAccountDimId.Code != "152"
                        && each_fact.FinancialAccountDimId.Code.Substring(0, 3) == "152")
                    {
                        if (!column_gridview.Contains(each_fact.FinancialAccountDimId.Code))
                        {
                            column_gridview.Add(each_fact.FinancialAccountDimId.Code);
                        }
                    }
                    if (each_fact.FinancialAccountDimId.Code != "153"
                        && each_fact.FinancialAccountDimId.Code.Substring(0, 3) == "153")
                    {
                        if (!column_gridview.Contains(each_fact.FinancialAccountDimId.Code))
                        {
                            column_gridview.Add(each_fact.FinancialAccountDimId.Code);
                        }
                    }
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
            GridView_S04b3DN.Columns.Add(fieldColumn);
            fieldColumn = new GridViewDataTextColumn { FieldName = "chi_tieu", Caption = "Chỉ tiêu" };
            GridView_S04b3DN.Columns.Add(fieldColumn);

            foreach (string each_152_153 in column_gridview())
            {
                string last = string.Empty;
                if (each_152_153.Substring(0, 3) == "152")
                {
                    last = "Nguyên liệu, vật liệu";
                }
                else if (each_152_153.Substring(0, 3) == "153")
                {
                    last = "Công cụ, dụng cụ";
                }
                bandColumn = new GridViewBandColumn("TK " + each_152_153 + " - " + last + "");
                fieldColumn = new GridViewDataTextColumn { FieldName = "" + each_152_153 + "HT", Caption = "Giá hạch toán" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(fieldColumn);

                fieldColumn = new GridViewDataTextColumn { FieldName = "" + each_152_153 + "TT", Caption = "Giá thực tế" };
                fieldColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(fieldColumn);
                GridView_S04b3DN.Columns.Add(bandColumn);
            }
        }

        //Table data
        public DataTable table_pri()
        {
            DataTable table_pri = new DataTable();
            table_pri.Columns.Add("stt");
            table_pri.Columns.Add("chi_tieu");
            foreach (string each_152_153 in column_gridview())
            {
                table_pri.Columns.Add("" + each_152_153 + "HT", typeof(double));
                table_pri.Columns.Add("" + each_152_153 + "TT", typeof(double));
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
            int FinancialAccountDimId, int CorrespondFinancialAccountDimId_default, List<string> list_Correspond_152_153)
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
                        if (!list_Correspond_152_153.Contains(each_Colection_Correspond.CorrespondFinancialAccountDimId.Code.Substring(0, 3)))
                        {
                            list_Correspond_152_153.Add(each_Colection_Correspond.CorrespondFinancialAccountDimId.Code.Substring(0, 3));
                        }
                    }
                }
            }
        }




        // get  FinancialActualPriceSummary_Fact
        public FinancialActualPriceSummary_Fact FinancialActualPriceSummary_Fact_152_153(int FinancialAccountDimId,
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
        public int FinancialAccountDimId(string Name)
        {
            return session.FindObject<FinancialAccountDim>(CriteriaOperator.Parse("Code='" + Name + "'")).FinancialAccountDimId;
        }

        #region nhật ký chứng từ
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