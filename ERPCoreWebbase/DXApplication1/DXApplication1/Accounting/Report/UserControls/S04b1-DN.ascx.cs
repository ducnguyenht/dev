using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.XtraReports.Web;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrintingLinks;
using DevExpress.Xpo.DB;
using DevExpress.Web.ASPxGridView;
using System.Data;
using System.Globalization;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Inventory;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting.SupplierLiability;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04b1_DN : System.Web.UI.UserControl
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

            if (hS04b1dn.Contains("show"))
            {
                int month = Int32.Parse(hS04b1dnMonth.Get("month_id").ToString());
                int year = Int32.Parse(hS04b1dnYear.Get("year_id").ToString());
                string owner = hS04b1dnOwner.Get("owner_id").ToString();
                string asset = hS04b1dnAsset.Get("asset_id").ToString();

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
                "and c.Code like '111%' " +
                "and (month(d.IssueDate) <= " + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + " ) " +
                "and (b.Credit > 0) " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "union all " +
                "select distinct d.IssueDate, -b.Debit as beginbalance, e.Name, d.FinancialTransactionDimId " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c, " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and c.Code like '111%' " +
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
                "and c.Code like '111%' " +
                "and (month(d.FromDateTime) = 1" + " and year(d.FromDateTime) = " + year.ToString() + ") " +
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
                    "( " +
                    "select distinct d.IssueDate, b.Credit, e.Code, d.FinancialTransactionDimId " +
                    "from DiaryJournal_Fact a, " +
                            "DiaryJournal_Detail b," +
                            "FinancialAccountDim c, " +
                            "FinancialTransactionDim d," +
                            "CorrespondFinancialAccountDim e " +
                    "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                    "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                    "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                    "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                    "and c.Code like '111%' " +
                    "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
                    "and (b.Credit > 0) " +
                    "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                    "and (b.FinancialAccountDimId is null) " +
                    "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                    "union all " +
                    "select distinct d.IssueDate, -b.Debit as Credit, e.Code, d.FinancialTransactionDimId " +
                    "from DiaryJournal_Fact a, " +
                            "DiaryJournal_Detail b, " +
                            "FinancialAccountDim c,  " +
                            "FinancialTransactionDim d, " +
                            "CorrespondFinancialAccountDim e " +
                    "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                    "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                    "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                    "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                    "and c.Code like '111%'  " +
                    "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
                    "and (b.Debit > 0) " +
                    "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                    "and (b.FinancialAccountDimId is null) " +
                    "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                    ") aa " +
                    "order by aa.IssueDate ";
              

                //header
                dataTable = new DataTable();
                dataTable.Columns.Add("stt");
                dataTable.Columns.Add("ngay");

                seletectedData = session.ExecuteQuery(sql);

                GridViewDataColumn caption_s04b1 = new GridViewDataColumn();
                caption_s04b1.Caption = "Số TT";
                caption_s04b1.FieldName = "stt";
                S04b1dnASPxGridView11.Columns.Add(caption_s04b1);

                caption_s04b1 = new GridViewDataColumn();
                caption_s04b1.Caption = "Ngày";
                caption_s04b1.FieldName = "ngay";
                S04b1dnASPxGridView11.Columns.Add(caption_s04b1);

                bandColumn = new GridViewBandColumn("Ghi nợ tài khoản 111, ghi có các tài khoản");

                S04b1dnASPxGridView11.Columns.Add(bandColumn);

                List<int> listHeader = new List<int>();
                
                foreach (var row in seletectedData.ResultSet)
                {
                    foreach (var col in row.Rows)
                    {
                        if (double.Parse(col.Values[1].ToString()) > 0  && !listHeader.Contains(Int32.Parse(col.Values[2].ToString())))
                        {
                            listHeader.Add(Int32.Parse(col.Values[2].ToString()));
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
                String space_s04b1 = new String(' ', 240 - maxMonth * 20);
                caption_s04b1.Caption = space_s04b1 + "Cộng nợ TK 111";
                caption_s04b1.Caption = "Cộng nợ TK 111";
                caption_s04b1.FieldName = "total";
                caption_s04b1.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(caption_s04b1);
                dataTable.Columns.Add("total", typeof(double));


                dataTable.Columns.Add("balance", typeof(double));
                caption_s04b1 = new GridViewDataTextColumn();
                caption_s04b1.Caption = "Số dư cuối ngày";                
                caption_s04b1.FieldName = "balance";
                caption_s04b1.PropertiesEdit.DisplayFormatString = "#,#";
                S04b1dnASPxGridView11.Columns.Add(caption_s04b1);
               
                int index_s04b1 = 0;

                String dateBegin_s04b1 = new String(' ', 10);

                double totalend = beginBalance;

                foreach (var row in seletectedData.ResultSet)
                {
                    foreach (var col in row.Rows)
                    {
                        if (!dateBegin_s04b1.Substring(0, 10).Equals(col.Values[0].ToString().Substring(0, 10)))
                        {
                            line = dataTable.NewRow();

                            dateBegin_s04b1 = col.Values[0].ToString();
                            index_s04b1++;

                            line[0] = index_s04b1;
                            dataTable.Rows.Add(line);

                            total = 0;
                        }


                        line["ngay"] = DateTime.Parse(col.Values[0].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        

                        //if (!col.Values[2].ToString().Contains("111"))

                        if (double.Parse(col.Values[1].ToString()) > 0)
                        {
                            line[col.Values[2].ToString()] = (line[col.Values[2].ToString()].ToString().Trim() == "" ? 0 : double.Parse(line[col.Values[2].ToString()].ToString())) +
                                                             double.Parse(col.Values[1].ToString());
                            total += double.Parse(col.Values[1].ToString());
                            line["total"] = total;
                        }                        

                        totalend += double.Parse(col.Values[1].ToString());
                        line["balance"] = totalend;

                        //endBalance = totalend;
                    }

                }

                endBalance = totalend;

                widthEndCol = 0;

                line = dataTable.NewRow();
                line["ngay"] = "Cộng";
                dataTable.Rows.Add(line);
                

                for (int i = 2; i < dataTable.Columns.Count - 1; i++)
                {
                    line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                    widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
                }

                //line["total"] = dataTable.Compute("Sum([total])", string.Empty);

                if (240 - widthEndCol * 3 > 0)
                {
                    space_s04b1 = new String(' ', 240 - widthEndCol * 3);
                }
                S04b1dnASPxGridView11.Columns["total"].Caption = space_s04b1 + "Cộng có TK 111";


                Report.S04b1_DN reportS04B1_DN = new Report.S04b1_DN();

                S04b1dnASPxGridView11.DataSource = dataTable;
                S04b1dnASPxGridView11.KeyFieldName = "stt";
                S04b1dnASPxGridView11.DataBind();

                reportS04B1_DN.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = S04b1dnGridViewExporter };
                reportS04B1_DN.Parameters["datePeriod"].Value = DateTime.Now;
                reportS04B1_DN.Parameters["yearPeriod"].Value = year;
                reportS04B1_DN.Parameters["monthPeriod"].Value = month;
                reportS04B1_DN.Parameters["beginBalance"].Value = beginBalance;
                reportS04B1_DN.Parameters["endBalance"].Value = endBalance;

                S04b1dnReportViewer.Report = reportS04B1_DN;
            }
        }
    }
}