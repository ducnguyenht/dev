using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Data;
using DevExpress.Web.ASPxGridView;
using NAS.DAL;
using DevExpress.XtraPrintingLinks;
using System.Globalization;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a8_DN : System.Web.UI.UserControl
    {
        Session session;
        string sql;
        SelectedData seletectedData;
        string codeBegin = "";        
        GridViewBandColumn bandColumn;
        int maxMonth;
        DataRow line;
        double total;
        int widthEndCol;
        string space;

        string m_Sql = "";

        public void load_data()
        {
            int month = Int32.Parse(hS04a8dnMonth.Get("month_id").ToString());
            int year = Int32.Parse(hS04a8dnYear.Get("year_id").ToString());
            string owner = hS04a8dnOwner.Get("owner_id").ToString();
            string asset = hS04a8dnAsset.Get("asset_id").ToString();



            m_Sql = "" +
                "select sum(b.Debit) as Debit, e.Code as DebitAccount, c.Code as CreditAccount " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c, " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and (c.Code like '131%' " +
		            "or c.Code like '155%' " +
			            "or c.Code like '156%' " + 
				            "or	c.Code like '157%' " +
					            "or	c.Code like '157%' " + 
						            "or c.Code like '159%' " +
							            "or c.Code like '511%' " + 
								            "or c.Code like '512%' " + 
									            "or c.Code like '515%' " + 
										            "or c.Code like '521%' " + 
											            "or	c.Code like '531%' " + 
												            "or c.Code like '532%' " +
													            "or c.Code like '632%' " + 
														            "or c.Code like '635%' " + 
															            "or c.Code like '641%' " + 
																            "or	c.Code like '642%' " + 
																	            "or c.Code like '711%' " +
																		            "or c.Code like '811%' " + 
																			            "or c.Code like '821%' " +
                                                                                            "or c.Code like '911%' " +
                     ") " +
                "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) = " + year.ToString() + ") " +
                "and b.Debit > 0 " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "group by c.Code, e.Code " +
                "order by  c.Code, e.Code ";
                         
            // header
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("stt");
            dataTable.Columns.Add("tk");

            try
            {
                seletectedData = session.ExecuteQuery(m_Sql);
            }
            catch
            {
            }

            GridViewDataColumn caption = new GridViewDataColumn();
            caption.Caption = "Số TT";
            caption.FieldName = "stt";
            S04a8dnASPxGridView.Columns.Add(caption);

            caption = new GridViewDataColumn();
            caption.Caption = "Các TK ghi Có / Các TK ghi Nợ ";
            caption.FieldName = "tk";
            S04a8dnASPxGridView.Columns.Add(caption);

            //bandColumn = new GridViewBandColumn("Ghi có tài khoản 111, ghi Nợ các tài khoản");

            //S04a8dnASPxGridView.Columns.Add(bandColumn);
            
            List<int> listHeader = new List<int>();

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (!listHeader.Contains(Int32.Parse(col.Values[2].ToString())))
                    {
                        listHeader.Add(Int32.Parse(col.Values[2].ToString()));
                    }
                }
            }

            //listHeader.Sort();
            int index;

            for (index = 0; index < listHeader.Count; index++)
            {
                GridViewDataTextColumn c = new GridViewDataTextColumn();
                c.Caption = listHeader[index].ToString();
                c.FieldName = listHeader[index].ToString();
                c.PropertiesEdit.DisplayFormatString = "#,#";
                // bandColumn.Columns.Add(c);
                S04a8dnASPxGridView.Columns.Add(c);
                dataTable.Columns.Add(listHeader[index].ToString(), typeof(double));
            }

            caption = new GridViewDataTextColumn();
            String space = new String(' ', 240 - maxMonth * 20);
            caption.Caption = space + "Cộng";
            caption.FieldName = "total";
            caption.PropertiesEdit.DisplayFormatString = "#,#";
            S04a8dnASPxGridView.Columns.Add(caption);
            //bandColumn.Columns.Add(caption);

            dataTable.Columns.Add("total", typeof(double));

            // data
            index = 0;

            double amount = 0;            
            codeBegin = new String(' ', 10);

            foreach (var row in seletectedData.ResultSet)
            {

                foreach (var col in row.Rows)
                {
                    if (!codeBegin.Equals(col.Values[1].ToString()))
                    {
                        line = dataTable.NewRow();

                        codeBegin = col.Values[1].ToString();
                        index++;

                        line[0] = index;
                        dataTable.Rows.Add(line);

                        total = 0;
                    }


                    line["tk"] = col.Values[1].ToString();

                    amount = double.Parse(col.Values[0].ToString());
                    total += double.Parse(col.Values[0].ToString());

                    line[col.Values[2].ToString()] = amount;
                    line["total"] = total;

                }

            }

            // sum total

            widthEndCol = 0;

            line = dataTable.NewRow();
            line["tk"] = "Cộng";
            dataTable.Rows.Add(line);

            codeBegin = "";

            for (int i = 2; i < dataTable.Columns.Count - 1; i++)
            {
                line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
            }

            line["total"] = dataTable.Compute("Sum([total])", string.Empty);


            if (240 - widthEndCol * 3 > 0)
            {
                space = new String(' ', 240 - widthEndCol * 3);
            }
            S04a8dnASPxGridView.Columns["total"].Caption = space + "Cộng";

            S04a8dnASPxGridView.DataSource = dataTable;
            S04a8dnASPxGridView.KeyFieldName = "stt";
            S04a8dnASPxGridView.DataBind();

            WebModule.Accounting.Report.S04a8_DN ctd = new WebModule.Accounting.Report.S04a8_DN();            
            ctd.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = S04a8dnASPxGridViewExporter };
            ctd.Parameters["datePeriod"].Value = new DateTime(year, month, 1);

            S04a8dnReportViewer.Report = ctd;
        }



        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hS04a8dn.Contains("show"))
            {
                load_data();
            }
        }

    }
}