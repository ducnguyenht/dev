using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using NAS.DAL;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S07a_DN : System.Web.UI.UserControl
    {
        string m_Sql = "";
        Session m_Session = null;
        SelectedData m_SelectedData;
        XPDataView xpDataView;
        DataTable m_DataTable;

        double m_Balance = 0;

        private void load_data()
        {
            DateTime fromDate = (DateTime)hS07adnFromDate.Get("fromDate");
            DateTime toDate = (DateTime)hS07adnToDate.Get("toDate");
            
            string owner = hS07adnOwner.Get("owner_id").ToString();
            string asset = hS07adnAsset.Get("asset_id").ToString();
            string account = hS07adnAccount.Get("account_id").ToString();
           
            m_Sql = "" +
"select result.BookingDate, result.IssueDate, result.ReceiptCode, result.PaymentCode, " +
"		result.Debit, result.Credit, result.Description, result.Balance, " +
"		result.DebitAccount, result.CreditAccount, result.CurrencyName " +
"from " +
" " +
"(select null as BookingDate, null as IssueDate, " +
"		'' as ReceiptCode, '' as PaymentCode, " +
"		sum(isnull(a.BeginDebitBalance, 0)) as Debit,		  " +
"		sum(isnull(a.BeginCreditBalance, 0)) as Credit, " +
"		N'Số dư đầu kỳ' as Description, 0 as Balance, " +
"		'' as DebitAccount, " +
"		'' as CreditAccount, " +
"		'' as CurrencyName " +
"from FinancialGeneralLedgerByYear_Fact a, YearDim b, FinancialAccountDim c " +
"where a.YearDimId = b.YearDimId " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId " +
"and b.Name = " + fromDate.Year +
"and c.Code like '" + account + "%' " +
"union all		 " +
"select b.BookingDate as BookingDate, b.IssueDate,  " +
"		case when a.Debit > 0 then b.Name else '' end as ReceiptCode,   " +
"		case when a.Credit > 0 then b.Name else '' end as PaymentCode,  " +
" 		a.Debit, a.Credit,  " +
"		isnull(b.Description, '') as Description, 0 as Balance, " +
"		isnull(d.Code, '') as DebitAccount, " +
"		isnull(e.Code, '') as CreditAccount, " +
"		c.Name as CurrencyName " +
"from FinancialCash_Fact a  " +
"	left join FinancialVoucherDim b	 " +
"		on a.FinancialVoucherDimId = b.FinancialVoucherDimId   " +
"	left join FinancialCashTypeDim c " +
"		on a.FinancialCashTypeDimId = c.FinancialCashTypeDimId " +
"	left join FinancialAccountDim d		  " +
"		on a.FinancialAccountDimId = d.FinancialAccountDimId " +
"	left join CorrespondFinancialAccountDim e " +
"		on a.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
"where b.IssueDate >= '" + string.Format("{0}-{1}-{2}", fromDate.Year, fromDate.Month, fromDate.Day) + " 00:00:00' " +
"and b.IssueDate <= '" + string.Format("{0}-{1}-{2}", toDate.Year, toDate.Month, toDate.Day) + " 23:59:00' " +
"and (exists (select null " +
"			from FinancialCash_Fact aa, FinancialAccountDim bb " +
"			where aa.FinancialAccountDimId = bb.FinancialAccountDimId " +
"			and bb.Code like '" + account + "%'  " +
"			and aa.FinancialVoucherDimId = b.FinancialVoucherDimId) " +
"		or (a.FinancialAccountDimId is null and a.CorrespondFinancialAccountDimId is null)) " +
"and a.FinancialAccountDimId is null	 " +
") result " +
"where (result.Debit > 0 or result.Credit > 0) " +
"order by result.IssueDate ";  

            m_SelectedData = m_Session.ExecuteQuery(m_Sql);

            m_DataTable = new DataTable();
            m_DataTable.Columns.Add("BookingDate", typeof(DateTime));
            m_DataTable.Columns.Add("IssueDate", typeof(DateTime));
            m_DataTable.Columns.Add("ReceiptCode", typeof(string));
            m_DataTable.Columns.Add("PaymentCode", typeof(string));
            m_DataTable.Columns.Add("Description", typeof(string));
            m_DataTable.Columns.Add("Debit", typeof(double));
            m_DataTable.Columns.Add("Credit", typeof(double));
            m_DataTable.Columns.Add("Balance", typeof(double));
            m_DataTable.Columns.Add("DebitAccount", typeof(string));
            m_DataTable.Columns.Add("CreditAccount", typeof(string));
            m_DataTable.Columns.Add("CurrencyName", typeof(string));

            foreach (var row in m_SelectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    DataRow line = m_DataTable.NewRow();

                    if (col.Values[0] != null)
                    {
                        line["BookingDate"] = DateTime.Parse(col.Values[0].ToString());
                    }
                    if (col.Values[1] != null)
                    {
                        line["IssueDate"] = DateTime.Parse(col.Values[1].ToString());
                    }
                    line["ReceiptCode"] = (string)col.Values[2];
                    line["PaymentCode"] = (string)col.Values[3];
                    line["Debit"] = double.Parse(col.Values[4].ToString());
                    line["Credit"] = double.Parse(col.Values[5].ToString());
                    line["Description"] = (string)col.Values[6];

                    m_Balance += double.Parse(col.Values[4].ToString()) - double.Parse(col.Values[5].ToString());

                    line["Balance"] = m_Balance;
                    line["DebitAccount"] = col.Values[8].ToString();
                    line["CreditAccount"] = col.Values[9].ToString();
                    line["CurrencyName"] = col.Values[10].ToString();

                    m_DataTable.Rows.Add(line);
                }
            }

            WebModule.Accounting.Report.S07a_DN report = new WebModule.Accounting.Report.S07a_DN();                        
            report.DataSource = m_DataTable;
            report.Parameters["datePeriod"].Value = fromDate;
            report.Parameters["accountName"].Value = account;
            report.Parameters["endBalance"].Value = m_Balance;

            report.DataMember = "";

            S07adnReportViewer.Report = report;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Session = XpoHelper.GetNewSession();
            if (hS07adn.Contains("show"))
            {
                load_data();
            }
        }
    }
}