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
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting;
using DevExpress.Web.ASPxGridView;
using DevExpress.XtraPrintingLinks;
using DevExpress.Xpo.DB;
using System.Globalization;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a3_DN : System.Web.UI.UserControl
    {
        Session session;


        private void load_data()
        {

            int month = Int32.Parse(hS04a3dnMonth.Get("month_id").ToString());
            int year = Int32.Parse(hS04a3dnYear.Get("year_id").ToString());
            string owner = hS04a3dnOwner.Get("owner_id").ToString();

            double beginBalance = 0;
            double endBalance = 0;

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
            string sql = "" +
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
            "and c.Code like '113%' " +
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
            "and c.Code like '113%' " +
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
            "and c.Code like '113%' " +
            "and (month(d.FromDateTime) = 1" + " and year(d.FromDateTime) = " + year.ToString() + ") " +
            ") aa ";

            SelectedData seletectedData = session.ExecuteQuery(sql);

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

            // endBalance           
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
            "and c.Code like '113%' " +
            "and (month(d.IssueDate) <= " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
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
            "and c.Code like '113%' " +
            "and (month(d.IssueDate) <= " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
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
            "and c.Code like '113%' " +
            "and (month(d.FromDateTime) = 1" + " and year(d.FromDateTime) = " + year.ToString() + ") " +
            ") aa ";

            seletectedData = session.ExecuteQuery(sql);

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (col.Values[0] != null)
                    {
                        endBalance = double.Parse(col.Values[0].ToString());
                    }
                }
            }


            sql = "" +
            "select distinct d.Name, d.IssueDate, d.Description, b.Debit, e.Code " +
            "from DiaryJournal_Fact a, " +
                    "DiaryJournal_Detail b, " +
                    "FinancialAccountDim c, " +
                    "FinancialTransactionDim d, " +
                    "CorrespondFinancialAccountDim e " +
            "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
            "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
            "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
            "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
            "and c.Code like '113%' " +
            "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) = " + year.ToString() + ") " +
            "and b.Debit > 0 " +
            "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
            "and (b.FinancialAccountDimId is null) " +
            "and e.Code not in ('1','2','3','4','5','6','7','8') " +
            "order by d.IssueDate ";


            DataTable dataTable = null;
            seletectedData = null;

            GridViewDataColumn dataColumn = null;
            GridViewDataTextColumn textColumn = null;
            GridViewBandColumn bandColumn = null;

            //string spaceString = "";

            int widthEndCol = 0;
            int maxMonth = 0; //Int16.Parse(DateTime.Parse(txtToDate.Value.ToString()).ToString("MM"));

            double total = 0;
            double quantity = 0;

            double beginDebit = 0;
            double beginCredit = 0;

            string codeBegin = "";
            int index = 0;
            string dateBegin;

            DataRow line = null;

            string account = "113";


            dataTable = new DataTable();

            dataTable.Columns.Add("stt");
            dataTable.Columns.Add("sohieu", typeof(string));
            dataTable.Columns.Add("ngay");
            dataTable.Columns.Add("diengiai", typeof(string));

            dataColumn = new GridViewDataColumn();
            dataColumn.Caption = "Số TT";
            dataColumn.FieldName = "stt";
            ASPxGridViewS04a3.Columns.Add(dataColumn);

            bandColumn = new GridViewBandColumn("Chứng từ");
            ASPxGridViewS04a3.Columns.Add(bandColumn);

            dataColumn = new GridViewDataColumn();
            dataColumn.Caption = "Số hiệu";
            dataColumn.FieldName = "sohieu";
            bandColumn.Columns.Add(dataColumn);

            dataColumn = new GridViewDataColumn();
            dataColumn.Caption = "Ngày";
            dataColumn.FieldName = "ngay";
            bandColumn.Columns.Add(dataColumn);

            dataColumn = new GridViewDataColumn();
            dataColumn.Caption = "Diễn giải";
            dataColumn.FieldName = "diengiai";
            dataColumn.Width = 200;
            ASPxGridViewS04a3.Columns.Add(dataColumn);

            bandColumn = new GridViewBandColumn("Ghi Có tài khoản " + account + ", ghi Nợ các tài khoản");
            ASPxGridViewS04a3.Columns.Add(bandColumn);

            codeBegin = new String(' ', 10);

            try
            {
                seletectedData = session.ExecuteQuery(sql);
            }
            catch
            {
            }

            List<int> listHeader = new List<int>();

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (!col.Values[4].ToString().Contains("113") && !listHeader.Contains(Int32.Parse(col.Values[4].ToString() == "" ? "0" : col.Values[4].ToString())))
                    {
                        listHeader.Add(Int32.Parse(col.Values[4].ToString()));
                    }
                }
            }

            listHeader.Sort();
            for (index = 0; index < listHeader.Count; index++)
            {
                textColumn = new GridViewDataTextColumn();
                textColumn.Caption = listHeader[index].ToString();
                textColumn.FieldName = listHeader[index].ToString();
                textColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(textColumn);

                dataTable.Columns.Add(listHeader[index].ToString(), typeof(double));
            }

            textColumn = new GridViewDataTextColumn();
            textColumn.Caption = "Cộng có TK " + account;
            textColumn.FieldName = "total";
            textColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(textColumn);

            dataTable.Columns.Add("total", typeof(double));

            // data
            index = 0;
            total = 0;

            dateBegin = new String(' ', 10);
            codeBegin = new String(' ', 10);

            line = null;

            double totalend = beginBalance;


            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (!codeBegin.Equals(col.Values[0].ToString()))
                    {
                        line = dataTable.NewRow();

                        codeBegin = col.Values[0].ToString();
                        //dateBegin = col.Values[1].ToString();
                        index++;

                        line[0] = index;
                        dataTable.Rows.Add(line);

                        total = 0;
                    }

                    line["sohieu"] = col.Values[0].ToString();
                    line["ngay"] = DateTime.Parse(col.Values[1].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (col.Values[2] != null)
                    {
                        if (col.Values[2].ToString().Length > 10)
                        {
                            line["diengiai"] = col.Values[2].ToString().Substring(0, 10) + "...";
                        }
                        else
                        {
                            line["diengiai"] = col.Values[2].ToString();
                        }
                    }

                    line[col.Values[4].ToString()] = double.Parse(col.Values[3].ToString());

                    total += double.Parse(col.Values[3].ToString());
                    totalend += double.Parse(col.Values[3].ToString());
                    line["total"] = total;
                }

            }

            //endBalance = totalend;

            // sum total

            widthEndCol = 0;

            line = dataTable.NewRow();
            line["ngay"] = "Cộng";
            dataTable.Rows.Add(line);

            codeBegin = "";

            for (int i = 4; i < dataTable.Columns.Count - 1; i++)
            {
                line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
            }

            line["total"] = dataTable.Compute("Sum([total])", string.Empty);

            string space = "";
            if (220 - widthEndCol * 5 > 0)
            {
                space = new String(' ', 220 - widthEndCol * 5);
            }
            ASPxGridViewS04a3.Columns["total"].Caption = space + "Cộng có TK " + account;

            ASPxGridViewS04a3.DataSource = dataTable;
            ASPxGridViewS04a3.KeyFieldName = "stt";
            ASPxGridViewS04a3.DataBind();

            Report.S04a3_DN reportS04A3_DN = new Report.S04a3_DN();
            reportS04A3_DN.printableS04a3.PrintableComponent = new PrintableComponentLinkBase() { Component = ASPxGridViewExporterS04a3 };
            reportS04A3_DN.Parameters["datePeriod"].Value = new DateTime(year, month, 1);
            reportS04A3_DN.Parameters["beginBalance"].Value = beginBalance;
            reportS04A3_DN.Parameters["endBalance"].Value = endBalance;

            S04a3dnReportViewer.Report = reportS04A3_DN;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hS04a3dn.Contains("show"))
            {
                load_data();
            }
        }

      
    }
}