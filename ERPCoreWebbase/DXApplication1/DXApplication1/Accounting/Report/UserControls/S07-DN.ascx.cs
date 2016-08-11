using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Data;
using NAS.DAL;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S07_DN : System.Web.UI.UserControl
    {        
        string m_Sql = "";
        Session m_Session = null;
        SelectedData m_SelectedData;
        XPDataView xpDataView;
        DataTable m_DataTable;
      
        double m_Balance = 0;

        private void load_data()
        {
            DateTime fromDate = (DateTime)hS07dnFromDate.Get("fromDate");
            DateTime toDate = (DateTime)hS07dnToDate.Get("toDate");

            string currency = hS07dnCurrencyName.Get("currency_id").ToString();
            string owner = hS07dnOwner.Get("owner_id").ToString();
            string asset = hS07dnAsset.Get("asset_id").ToString();
          


            m_Sql = "" +
"select result.BookingDate, result.IssueDate, result.ReceiptCode, result.PaymentCode, " +
"		result.Debit, result.Credit, result.Description, result.Balance, " +
"		result.CurrencyName " +
"from " +
"(select null as BookingDate, null as IssueDate, " +
"		'' as ReceiptCode, '' as PaymentCode, " +
"		sum(isnull(a.BeginDebitBalance, 0)) as Debit,		  " +
"		sum(isnull(a.BeginCreditBalance, 0)) as Credit, " +
"		N'Số dư đầu kỳ' as Description, 0 as Balance, " +
"		'' as CurrencyName " +
"from FinancialGeneralLedgerByYear_Fact a, YearDim b, FinancialAccountDim c " +
"where a.YearDimId = b.YearDimId " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId " +
"and b.Name = " + fromDate.Year + " " +
"and c.Code like '" + "111" + "%' " +
"union all		 " +
"select b.BookingDate, b.IssueDate,   " +
"		case when a.Debit > 0 then b.Name else '' end as ReceiptCode,    " +
"		case when a.Credit > 0 then b.Name else '' end as PaymentCode,   " +
" 		round(a.Debit,0) Debit, round(a.Credit,0) as Credit,   " +
"		isnull(b.Description, '') as Description, 0 as Balance, " +
"		c.Name as CurrencyName  " +
"from FinancialCash_Fact a, FinancialVoucherDim b,  " +
"	FinancialCashTypeDim c	   " +
"where a.FinancialVoucherDimId = b.FinancialVoucherDimId   " +
"and a.FinancialCashTypeDimId = c.FinancialCashTypeDimId   " +
"and CorrespondFinancialAccountDimId is null " +
"and b.IssueDate >= '" + string.Format("{0}-{1}-{2}", fromDate.Year, fromDate.Month, fromDate.Day) + " 00:00:00' " +
"and b.IssueDate <= '" + string.Format("{0}-{1}-{2}", toDate.Year, toDate.Month, toDate.Day) + " 23:59:00') result " +
"where (result.Debit > 0 or result.Credit > 0) " +
"order by result.IssueDate  ";

//"select b.BookingDate, b.IssueDate,   " +
//"		case when a.Debit > 0 then b.Name else '' end as ReceiptCode,    " +
//"		case when a.Credit > 0 then b.Name else '' end as PaymentCode,   " +
//" 		a.Debit, a.Credit,   " +
//"		isnull(b.Description, '') as Description, 0 as Balance, " +
//"		c.Name as CurrencyName  " +
//"from FinancialCash_Fact a, FinancialVoucherDim b,  " +
//"	FinancialCashTypeDim c	   " +
//"where a.FinancialVoucherDimId = b.FinancialVoucherDimId   " +
//"and a.FinancialCashTypeDimId = c.FinancialCashTypeDimId " +
//"and (b.BookingDate is null or CorrespondFinancialAccountDimId is null)   " +

//"order by b.IssueDate ";

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
                
                    line["ReceiptCode"] = col.Values[2].ToString();
                    line["PaymentCode"] = col.Values[3].ToString();
                    line["Debit"] = double.Parse(col.Values[4].ToString());
                    line["Credit"] = double.Parse(col.Values[5].ToString());
                    line["Description"] = col.Values[6].ToString();
               
                    
                    m_Balance += (double)(col.Values[4]) - (double)(col.Values[5]);

                    line["Balance"] = m_Balance;
                    line["CurrencyName"] = col.Values[8].ToString();
               
                    m_DataTable.Rows.Add(line);
                }
            }
                  
            WebModule.Accounting.Report.S07_DN report = new WebModule.Accounting.Report.S07_DN();
            report.DataSource = m_DataTable;
            report.Parameters["currencyName"].Value = currency;

            report.DataMember = "";

            S07dnReportViewer.Report = report;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Session = XpoHelper.GetNewSession();
            if (hS07dn.Contains("show"))
            {
                load_data();
            }
        }
    }
    
}