using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxGridView;
using System.Data;
using DevExpress.XtraPrintingLinks;
using NAS.DAL;
using System.Globalization;
using NAS.DAL.Accounting.AccountChart;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a10_DN : System.Web.UI.UserControl
    {
        Account m_Account = null;
        CriteriaOperator m_Filter = null;

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
        char acccountType = 'D';

        public void load_data()
        {
            int month = Int32.Parse(hS04a10dnMonth.Get("month_id").ToString());
            int year = Int32.Parse(hS04a10dnYear.Get("year_id").ToString());
            string owner = hS04a10dnOwner.Get("owner_id").ToString();
            string asset = hS04a10dnAsset.Get("asset_id").ToString();
            string account = hS04a10dnAccount.Get("account_id").ToString();

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

            //account = "331";
            //acccountType = 'D';

            m_Filter = new BinaryOperator("Code", account, BinaryOperatorType.Equal);
            m_Account = session.FindObject<Account>(m_Filter);


            if (m_Account != null)
            {
                if (m_Account.AccountTypeId.AccountCategoryId.Code == "ASSET")
                {
                    acccountType = 'D';
                }
                else if (m_Account.AccountTypeId.AccountCategoryId.Code == "LIABILITY")
                {
                    acccountType = 'C';
                }
                else
                {
                    throw new Exception("Báo cáo S04a10-DN chỉ hỗ trợ cho loại tài khoản (ASSET|LIABILITY) ?");
                }
            }
            else
            {
                throw new Exception("Tài khoản này không tồn tại hoặc đã bị xóa ?");
            }

            if (acccountType == 'D')
            {
                m_Sql = "" +
                "select isnull(Name, '') as Name, [Description], isnull(Debit,0) as Debit, isnull(Credit,0) as Credit, " +
                        "DebitAccount, CreditAccount, AccountType " +
                "from " +
                "( " +
                "select null as Name, null as IssueDate, N'Số dư đầu tháng' as [description], sum(isnull(Debit,0)) as Debit , 0 as Credit,  " +
                    "null as DebitAccount, null as CreditAccount, 0 as AccountType " +
                "from " +
                "(select distinct d.Name, d.issuedate, isnull(d.Description,'') as [description], b.Credit as Debit, 0 as Credit, " +
                        "c.Code as DebitAccount, e.Code as CreditAccount, 0 as AccountType " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c,  " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                "and c.Code like '" + account + "%'" +
                "and (month(d.IssueDate) <= " + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + ")  " +
                "and b.Credit > 0 " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
                "and (b.FinancialAccountDimId is null) " +
                "and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
                "union all " +
                "select distinct d.Name, d.issuedate, isnull(d.Description,'') as [description], -b.Debit, 0 as Credit, " +
                        "e.Code as DebitAccount, c.Code as CreditAccount, 0 as AccountType " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c,  " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e  " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                "and c.Code like '" + account + "%'" +
                "and (month(d.IssueDate) <= " + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + ")  " +
                "and b.Debit > 0 " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
                "and (b.FinancialAccountDimId is null) " +
                "and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
                "union all " +
                "select null as Name, null as IssueDate, null as Decription, b.Debit, 0 as Credit,  " +
                    "null as DebitAccount, null as CreditAccount, 0 as AccountType " +
                "from BalanceForwardTransaction a, GeneralJournal b, Account c, AccountingPeriod d, [Transaction] e " +
                "where a.TransactionId = b.TransactionId " +
                "and b.AccountId = c.AccountId " +
                "and a.TransactionId = e.TransactionId " +
                "and d.AccountingPeriodId = e.AccountingPeriodId " +
                "and b.Debit > 0 " +
                "and c.Code like '" + account + "%'" +
                "and (month(d.FromDateTime) = 1 and year(d.FromDateTime) = " + lastyear.ToString() + ")  " +
                ") aa  " +
                "union all " +
                "select distinct d.Name, d.issuedate, isnull(d.Description,'') as [description], b.Credit as Debit, 0 as Credit, " +
                        "c.Code as DebitAccount, e.Code as CreditAccount, 1 as AccountType " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c,  " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                "and c.Code like '" + account + "%'" +
                "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + ")  " +
                "and b.Credit > 0 " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
                "and (b.FinancialAccountDimId is null) " +
                "and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
                "union all " +
                "select distinct d.Name, d.issuedate, isnull(d.Description,'') as [description], 0 as Debit, b.Debit as Credit, " +
                        "e.Code as DebitAccount, c.Code as CreditAccount, 2 as AccountType " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c,  " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                "and c.Code like '" + account + "%'" +
                "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + ")  " +
                "and b.Debit > 0 " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')  " +
                "and (b.FinancialAccountDimId is null)  " +
                "and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
                ") aa  " +
                "order by issuedate ";
            }
            else
            {
                m_Sql = "" +
                   "select isnull(Name, '') as Name, [Description], isnull(Debit,0) as Debit, isnull(Credit,0) as Credit, " +
                           "DebitAccount, CreditAccount, AccountType " +
                   "from " +
                   "( " +
                   "select null as Name, null as IssueDate, N'Số dư đầu tháng' as [description], sum(isnull(Debit,0)) as Debit , 0 as Credit,  " +
                       "null as DebitAccount, null as CreditAccount, 0 as AccountType " +
                   "from " +
                   "(select distinct d.Name, d.issuedate, isnull(d.Description,'') as [description], 0 as Debit, b.Debit as Credit, " +
                           "c.Code as DebitAccount, e.Code as CreditAccount, 0 as AccountType " +
                   "from DiaryJournal_Fact a, " +
                           "DiaryJournal_Detail b, " +
                           "FinancialAccountDim c,  " +
                           "FinancialTransactionDim d, " +
                           "CorrespondFinancialAccountDim e " +
                   "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                   "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                   "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                   "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                   "and c.Code like '" + account + "%'" +
                   "and (month(d.IssueDate) <= " + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + ")  " +
                   "and b.Debit > 0 " +
                   "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
                   "and (b.FinancialAccountDimId is null) " +
                   "and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
                   "union all " +
                   "select distinct d.Name, d.issuedate, isnull(d.Description,'') as [description], 0 as Debit, -b.Credit as Credit, " +
                           "e.Code as DebitAccount, c.Code as CreditAccount, 0 as AccountType " +
                   "from DiaryJournal_Fact a, " +
                           "DiaryJournal_Detail b, " +
                           "FinancialAccountDim c,  " +
                           "FinancialTransactionDim d, " +
                           "CorrespondFinancialAccountDim e  " +
                   "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                   "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                   "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                   "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                   "and c.Code like '" + account + "%'" +
                   "and (month(d.IssueDate) <= " + lastmonth.ToString() + " and year(d.IssueDate) =  " + lastyear.ToString() + ")  " +
                   "and b.Credit > 0 " +
                   "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
                   "and (b.FinancialAccountDimId is null) " +
                   "and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
                   "union all " +
                   "select null as Name, null as IssueDate, null as Decription, 0 as Debit, b.Credit,  " +
                       "null as DebitAccount, null as CreditAccount, 0 as AccountType " +
                   "from BalanceForwardTransaction a, GeneralJournal b, Account c, AccountingPeriod d, [Transaction] e " +
                   "where a.TransactionId = b.TransactionId " +
                   "and b.AccountId = c.AccountId " +
                   "and a.TransactionId = e.TransactionId " +
                   "and d.AccountingPeriodId = e.AccountingPeriodId " +
                   "and b.Debit > 0 " +
                   "and c.Code like '" + account + "%'" +
                   "and (month(d.FromDateTime) = 1 and year(d.FromDateTime) = " + lastyear.ToString() + ")  " +
                   ") aa  " +
                   "union all " +
                   "select distinct d.Name, d.issuedate, isnull(d.Description,'') as [description], b.Credit as Debit, 0 as Credit, " +
                           "c.Code as DebitAccount, e.Code as CreditAccount, 1 as AccountType " +
                   "from DiaryJournal_Fact a, " +
                           "DiaryJournal_Detail b, " +
                           "FinancialAccountDim c,  " +
                           "FinancialTransactionDim d, " +
                           "CorrespondFinancialAccountDim e " +
                   "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                   "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                   "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                   "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                   "and c.Code like '" + account + "%'" +
                   "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + ")  " +
                   "and b.Credit > 0 " +
                   "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
                   "and (b.FinancialAccountDimId is null) " +
                   "and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
                   "union all " +
                   "select distinct d.Name, d.issuedate, isnull(d.Description,'') as [description], 0 as Debit, b.Debit as Credit, " +
                           "e.Code as DebitAccount, c.Code as CreditAccount, 2 as AccountType " +
                   "from DiaryJournal_Fact a, " +
                           "DiaryJournal_Detail b, " +
                           "FinancialAccountDim c,  " +
                           "FinancialTransactionDim d, " +
                           "CorrespondFinancialAccountDim e " +
                   "where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
                   "and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
                   "and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
                   "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
                   "and c.Code like '" + account + "%'" +
                   "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + ")  " +
                   "and b.Debit > 0 " +
                   "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')  " +
                   "and (b.FinancialAccountDimId is null)  " +
                   "and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
                   ") aa  " +
                   "order by issuedate ";
            }

            // header
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("stt");            
            dataTable.Columns.Add("diengiai");

            GridViewDataColumn caption = new GridViewDataColumn();
            caption.Caption = "Số TT";
            caption.FieldName = "stt";
            S04a10dnASPxGridView.Columns.Add(caption);

            caption = new GridViewDataColumn();
            caption.Caption = "Diễn giải";
            caption.FieldName = "diengiai";
            S04a10dnASPxGridView.Columns.Add(caption);

            bandColumn = new GridViewBandColumn("Số dư đầu tháng");
            S04a10dnASPxGridView.Columns.Add(bandColumn);

            caption = new GridViewDataTextColumn();
            caption.Caption = "Nợ";
            caption.FieldName = "no";
            bandColumn.Columns.Add(caption);
            dataTable.Columns.Add("no", typeof(double));            

            caption = new GridViewDataTextColumn();
            caption.Caption = "Có";
            caption.FieldName = "co";
            bandColumn.Columns.Add(caption);
            dataTable.Columns.Add("co", typeof(double));

          
            try
            {
                seletectedData = session.ExecuteQuery(m_Sql);
            }
            catch
            {
            }

            //Debit

            List<int> listHeader = new List<int>();
            bandColumn = new GridViewBandColumn("Ghi Nợ tài khoản " + account + " Ghi Có các tài khoản");
            S04a10dnASPxGridView.Columns.Add(bandColumn);


            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if ( col.Values[6].ToString() != "0")
                    {
                        if (!listHeader.Contains(Int32.Parse(col.Values[5].ToString())) && col.Values[6].ToString() == "1")
                        {
                            listHeader.Add(Int32.Parse(col.Values[5].ToString()));
                        }
                    }
                }
            }

            listHeader.Sort();
            int index;

            for (index = 0; index < listHeader.Count; index++)
            {
                GridViewDataTextColumn c = new GridViewDataTextColumn();
                c.Caption = "C-" + listHeader[index].ToString();
                c.FieldName = "C-" + listHeader[index].ToString();
                c.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(c);
                dataTable.Columns.Add(c.FieldName, typeof(double));
            }

            caption = new GridViewDataTextColumn();
            String space = new String(' ', 240 - maxMonth * 20);
            caption.Caption = space + "Cộng Nợ TK " + account;
            caption.FieldName = "totalno";
            caption.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(caption);

            dataTable.Columns.Add("totalno", typeof(double));

            // Credit

            listHeader = new List<int>();
            bandColumn = new GridViewBandColumn("Ghi Có tài khoản " + account + " Ghi Nợ các tài khoản");
            S04a10dnASPxGridView.Columns.Add(bandColumn);

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (col.Values[6].ToString() != "0")
                    {
                        if (!listHeader.Contains(Int32.Parse(col.Values[4].ToString())) && col.Values[6].ToString() == "2")
                        {
                            listHeader.Add(Int32.Parse(col.Values[4].ToString()));
                        }
                    }
                }
            }

            listHeader.Sort();           

            for (index = 0; index < listHeader.Count; index++)
            {
                GridViewDataTextColumn c = new GridViewDataTextColumn();
                c.Caption = "D-" + listHeader[index].ToString();
                c.FieldName = "D-" + listHeader[index].ToString();
                c.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(c);
                dataTable.Columns.Add(c.FieldName, typeof(double));
            }

            caption = new GridViewDataTextColumn();        
            caption.Caption = space + "Cộng Có TK " + account;
            caption.FieldName = "totalco";
            caption.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(caption);

            dataTable.Columns.Add("totalco", typeof(double));


            bandColumn = new GridViewBandColumn("Số dư cuối tháng");
            S04a10dnASPxGridView.Columns.Add(bandColumn);

            caption = new GridViewDataTextColumn();
            caption.Caption = space + "Nợ" + account;
            caption.FieldName = "endtotalno";
            caption.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(caption);

            dataTable.Columns.Add("endtotalno", typeof(double));

            caption = new GridViewDataTextColumn();
            caption.Caption = space + "Có" + account;
            caption.FieldName = "endtotalco";
            caption.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(caption);

            dataTable.Columns.Add("endtotalco", typeof(double));

            // data
            index = 0;

            double amount = 0;
            codeBegin = new String(' ', 10);

            foreach (var row in seletectedData.ResultSet)
            {

                foreach (var col in row.Rows)
                {
                    if (!codeBegin.Equals(col.Values[0].ToString()))
                    {
                        line = dataTable.NewRow();

                        codeBegin = col.Values[0].ToString();
                        index++;

                        line[0] = index;
                        dataTable.Rows.Add(line);

                        total = 0;
                    }

                    line["diengiai"] = col.Values[1].ToString();
                    if (col.Values[6].ToString() == "0")
                    {
                        if (acccountType == 'D')
                        {
                            line["no"] = double.Parse(col.Values[2].ToString());
                        }
                        else
                        {
                            line["co"] = double.Parse(col.Values[3].ToString());
                        }
                    }
                    else
                    {
                        if (col.Values[6].ToString() == "1")
                        {
                            amount = double.Parse(col.Values[2].ToString());
                            total += double.Parse(col.Values[2].ToString());
                            line["C-"+col.Values[5].ToString()] = amount;
                            line["totalno"] = total;
                            
                        }
                        else
                        {
                            amount = double.Parse(col.Values[3].ToString());
                            total += double.Parse(col.Values[3].ToString());
                            line["D-" + col.Values[4].ToString()] = amount;
                            line["totalco"] = total;
                        }
                                               
                    }

                }

            }

            // sum total

            widthEndCol = 0;

            line = dataTable.NewRow();
            line["diengiai"] = "Cộng";
            dataTable.Rows.Add(line);
            

            for (int i = 2; i < dataTable.Columns.Count - 2; i++)
            {
                line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
            }

            if (acccountType == 'D')
            {
                line["endtotalno"] = double.Parse((line["no"].ToString())) +
                                    double.Parse(line["totalno"].ToString().Trim() == "" ? "0" : line["totalno"].ToString()) -
                                    double.Parse(line["totalco"].ToString().Trim() == "" ? "0" : line["totalco"].ToString());
            }
            else
            {
                line["endtotalco"] = double.Parse((line["co"].ToString())) +
                                    double.Parse(line["totalco"].ToString().Trim() == "" ? "0" : line["totalco"].ToString()) -
                                    double.Parse(line["totalco"].ToString().Trim() == "" ? "0" : line["totalco"].ToString());       
            }

            //if (240 - widthEndCol * 3 > 0)
            //{
            //    space = new String(' ', 240 - widthEndCol * 3);
            //}
            
            S04a10dnASPxGridView.Columns["diengiai"].Caption = space + "Cộng";

            S04a10dnASPxGridView.DataSource = dataTable;
            S04a10dnASPxGridView.KeyFieldName = "stt";
            S04a10dnASPxGridView.DataBind();

            WebModule.Accounting.Report.S04a10_DN ctd = new WebModule.Accounting.Report.S04a10_DN();
            ctd.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = S04a10dnASPxGridViewExporter };
            ctd.Parameters["accountName"].Value = account;
            ctd.Parameters["datePeriod"].Value = new DateTime(year, month, 1);

            S04a10dnReportViewer.Report = ctd;
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hS04a10dn.Contains("show"))
            {
                load_data();
            }
        }
    }
}