using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using DevExpress.XtraPrintingLinks;
using System.Globalization;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04b2_DN : System.Web.UI.UserControl
    {
        Session session;
        string sql;
        SelectedData seletectedData;
        string codeBegin;
        GridViewBandColumn bandColumn;
        int maxMonth;
        DataRow line;
        double total;
        int widthEndCol;
        string space;
        DataTable dataTable;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            double beginBalance = 0;
            double endBalance = 0;


            if (hS04b2dn.Contains("show"))
            {
                int month = Int32.Parse(hS04b2dnMonth.Get("month_id").ToString());
                int year = Int32.Parse(hS04b2dnYear.Get("year_id").ToString());
                string owner = hS04b2dnOwner.Get("owner_id").ToString();
                string asset = hS04b2dnAsset.Get("asset_id").ToString();
                string account = hS04b2dnAccount.Get("account_id").ToString();

                account = "112";

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

                // begin balance
                sql = "" +
               "select sum(beginbalance) as beginbalance " +
                "from " +
                "(select distinct d.IssueDate, b.Credit as beginbalance, e.Code, d.FinancialTransactionDimId " +
                "from DiaryJournal_Fact a, " +
		                "DiaryJournal_Detail b, " +
		                "FinancialAccountDim c, " +
		                "FinancialTransactionDim d, " +
		                "CorrespondFinancialAccountDim e " +  		
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and c.Code like '112%' " +
                "and (month(d.IssueDate) <= "  + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + " ) " +
                "and (b.Credit > 0) " +
                 "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "union all " +
                "select distinct d.IssueDate, -b.Debit as beginbalance, e.Code, d.FinancialTransactionDimId " +
                "from DiaryJournal_Fact a, " +
		                "DiaryJournal_Detail b, " +
		                "FinancialAccountDim c, " +
		                "FinancialTransactionDim d, " +
		                "CorrespondFinancialAccountDim e " +  		
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and c.Code like '112%' " +
                "and (month(d.IssueDate) <= "  + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + " ) " +
                "and (b.Debit > 0) " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "union all " +
                "select null as IssueDate, b.Debit as beginbalance, null as Name, null as FinancialTransactionDimId " +
                "from BalanceForwardTransaction a, GeneralJournal b, Account c, AccountingPeriod d, [Transaction] e " +
                "where a.TransactionId = b.TransactionId " +
                "and b.AccountId = c.AccountId " +
                "and a.TransactionId = e.TransactionId " +
                "and d.AccountingPeriodId = e.AccountingPeriodId " +
                "and b.Debit > 0 " +
                "and c.Code like '112%' " +
                "and (month(d.FromDateTime) = 1" + " and year(d.FromDateTime) = " + year.ToString() +  ") " +
                ") aa ";

                seletectedData = session.ExecuteQuery(sql);

                foreach (var row in seletectedData.ResultSet)
                {
                    foreach (var col in row.Rows)
                    {
                        if (col.Values[0] != null)
                        {
                            beginBalance = double.Parse(col.Values[0].ToString());
                        }
                    }
                }

                sql = "" +
                "select * from " +
                "(" +
                "select distinct d.Name as InvoiceCode, d.IssueDate, d.Description, b.Credit, e.Code, d.FinancialTransactionDimId " +
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
                "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
                "and (b.Credit > 0) " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "union all " +
                "select distinct d.Name as InvoiceCode, d.IssueDate, d.Description, -b.Debit as Credit, e.Code, d.FinancialTransactionDimId " +
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
                "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
                "and (b.Debit > 0) " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                ") aa " +
                "order by aa.IssueDate ";


               ///////////////////////////////////////////////////
                //header
                dataTable = new DataTable();
                dataTable.Columns.Add("stt");
                dataTable.Columns.Add("sohieu");
                dataTable.Columns.Add("ngay");
                dataTable.Columns.Add("diengiai");
               

                seletectedData = session.ExecuteQuery(sql);

                GridViewDataColumn caption_s04b1 = new GridViewDataColumn();
                caption_s04b1.Caption = "Số TT";
                caption_s04b1.FieldName = "stt";
                S04b2dnASPxGridView1.Columns.Add(caption_s04b1);

                caption_s04b1 = new GridViewDataColumn();
                caption_s04b1.Caption = "Số hiệu";
                caption_s04b1.FieldName = "sohieu";
                S04b2dnASPxGridView1.Columns.Add(caption_s04b1);

                caption_s04b1 = new GridViewDataColumn();
                caption_s04b1.Caption = "Ngày";
                caption_s04b1.FieldName = "ngay";
                S04b2dnASPxGridView1.Columns.Add(caption_s04b1);

                caption_s04b1 = new GridViewDataColumn();
                caption_s04b1.Caption = "Diễn giải";
                caption_s04b1.FieldName = "diengiai";
                S04b2dnASPxGridView1.Columns.Add(caption_s04b1);

                bandColumn = new GridViewBandColumn("Ghi nợ tài khoản 112, ghi có các tài khoản");

                S04b2dnASPxGridView1.Columns.Add(bandColumn);

                List<int> listHeader = new List<int>();

                foreach (var row in seletectedData.ResultSet)
                {
                    foreach (var col in row.Rows)
                    {
                        if (double.Parse(col.Values[3].ToString()) > 0 && !listHeader.Contains(Int32.Parse(col.Values[4].ToString())))
                        {
                            listHeader.Add(Int32.Parse(col.Values[4].ToString()));
                        }
                    }
                }

                listHeader.Sort();
                int index;

                for (index = 0; index < listHeader.Count; index++)
                {
                    GridViewDataTextColumn c = new GridViewDataTextColumn();
                    c.Caption = listHeader[index].ToString();
                    c.FieldName = listHeader[index].ToString();
                    c.PropertiesEdit.DisplayFormatString = "#,#";
                    bandColumn.Columns.Add(c);
                    dataTable.Columns.Add(listHeader[index].ToString(), typeof(double));
                }
               
                caption_s04b1 = new GridViewDataTextColumn();
                String space_s04b1 = new String(' ', 200 - maxMonth * 20);
                caption_s04b1.Caption = space_s04b1 + "Cộng Nợ TK 112";
                caption_s04b1.Caption = "Cộng Nợ TK 112";
                caption_s04b1.FieldName = "total";
                caption_s04b1.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(caption_s04b1);
                dataTable.Columns.Add("total", typeof(double));

                dataTable.Columns.Add("balance", typeof(double));
                caption_s04b1 = new GridViewDataTextColumn();
                caption_s04b1.Caption = "Số dư cuối ngày";
                caption_s04b1.PropertiesEdit.DisplayFormatString = "#,#";
                caption_s04b1.FieldName = "balance";
                S04b2dnASPxGridView1.Columns.Add(caption_s04b1);

                //data
                int index_s04b1 = 0;

                String noBegin_s04b1 = new String(' ', 10);
                double totalend = beginBalance;

                foreach (var row in seletectedData.ResultSet)
                {
                    foreach (var col in row.Rows)
                    {
                        if (!noBegin_s04b1.Equals(col.Values[0].ToString()))
                        {
                            line = dataTable.NewRow();

                            noBegin_s04b1 = col.Values[0].ToString();
                            index_s04b1++;

                            line[0] = index_s04b1;
                            dataTable.Rows.Add(line);

                            total = 0;
                        }

                        line["sohieu"] = col.Values[0].ToString();
                        line["ngay"] = DateTime.Parse(col.Values[1].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        if (col.Values[2] != null)
                        {
                            line["diengiai"] = col.Values[2].ToString();
                        }

                        if (double.Parse(col.Values[3].ToString()) > 0)
                        {
                            line[col.Values[4].ToString()] = Double.Parse(col.Values[3].ToString());
                            total += double.Parse(col.Values[3].ToString());
                            line["total"] = total;
                        }

                        totalend += double.Parse(col.Values[3].ToString());
                        line["balance"] = totalend;
                       
                    }

                }

                endBalance = totalend;

                widthEndCol = 0;

                line = dataTable.NewRow();
                line["ngay"] = "Cộng";
                dataTable.Rows.Add(line);                

                for (int i = 4; i < dataTable.Columns.Count - 1; i++)
                {
                    line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                    widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
                }

                //line["total"] = dataTable.Compute("Sum([total])", string.Empty);

                //if (200 - widthEndCol * 3 > 0)
                //{
                //    space_s04b1 = new String(' ', 200 - widthEndCol * 3);
                //}
                S04b2dnASPxGridView1.Columns["total"].Caption = space_s04b1 + "Cộng Nợ TK 112";

                S04b2dnASPxGridView1.DataSource = dataTable;
                S04b2dnASPxGridView1.KeyFieldName = "stt";
                S04b2dnASPxGridView1.DataBind();
                
                Report.S04b2_DN reportS04B2_DN = new Report.S04b2_DN();    

                reportS04B2_DN.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = S04b2dnGridViewExporter };
                reportS04B2_DN.Parameters["datePeriod"].Value = new DateTime(year, month, 1);
                reportS04B2_DN.Parameters["beginBalance"].Value = beginBalance;
                reportS04B2_DN.Parameters["endBalance"].Value = endBalance;

                S04b2dnReportViewer.Report = reportS04B2_DN;
            }
        }        
    }
}