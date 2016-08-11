using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Xpo.DB;
using WebModule.Accounting.Report.Data;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S06_DN : System.Web.UI.UserControl
    {
        string m_Sql = "";
        Session m_Session = null;
        SelectedData m_SelectedData;
        XPDataView xpDataView;

        public void load_data()
        {
            int month = Int32.Parse(hS06dnMonth.Get("month_id").ToString());
            int year = Int32.Parse(hS06dnYear.Get("year_id").ToString());
            string owner = hS06dnOwner.Get("owner_id").ToString();
            string asset = hS06dnAsset.Get("asset_id").ToString();
            DateTime fromDate = new DateTime(year, month, 1);


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

            m_Sql = "" +
"select result.Code as AccountCode, result.Name as AccountName, " +
"	sum(result.beginDebit) as BeginDebit, " +
"	sum(result.beginCredit) as BeginCredit," +
"	sum(result.Debit) as Debit, " +
"	sum(result.Credit) as Credit," +
"	case " +
"		when sum(result.beginDebit) + sum(result.Debit) - sum(result.beginCredit)- sum(result.Credit) > 0 THEN (sum(result.beginDebit) + sum(result.Debit) - sum(result.Credit)) ELSE 0  end as EndDebit, 		 		" +
"	case " +
"		when sum(result.beginCredit) + sum(result.Credit) - sum(result.beginDebit) - sum(result.Debit) > 0 THEN (sum(result.beginCredit) + sum(result.Credit) - sum(result.Debit)) ELSE 0 end as EndCredit	" +
"from	" +
"	(select transact.Code, transact.Name, " +
"		" +
"		case " +
"			when sum(isnull(BeginDebitBalance,0)) > 0 " +
"				THEN sum(isnull(transact.BeginDebitBalance,0)) + sum(transact.Debit) - sum(transact.Credit) ELSE 0  end as beginDebit, 		" +
"		case 	" +
"			when sum(isnull(BeginCreditBalance,0)) > 0 " +
"				THEN sum(isnull(transact.BeginCreditBalance,0)) + sum(transact.Credit) - sum(transact.Debit) ELSE 0 end as beginCredit,	" +
"		0 as Debit, 0 as Credit" +
"		" +
"	from	" +
"		(		" +
"		select b.Code, b.Name, a.BeginDebitBalance, a.BeginCreditBalance, 0 as Debit, 0 as Credit   		" +
"		from FinancialGeneralLedgerByYear_Fact a, FinancialAccountDim b			" +
"		where a.FinancialAccountDimId = b.FinancialAccountDimId" +
"	" +
"		union all" +
"		" +
"		select e.Code, e.Name, 0 as BeginDebitBalance, 0 as BeginCreditBalance," +
"			a.CreditSum as Debit, a.DebitSum as Credit" +
"		from FinancialGeneralLedgerByMonth a," +
"			FinancialGeneralLedgerByYear_Fact b," +
"			MonthDim c," +
"			YearDim d," +
"			FinancialAccountDim e" +
"		where a.FinancialGeneralLedgerByYear_FactId = b.FinancialGeneralLedgerByYear_FactId" +
"		and a.MonthDimId = c.MonthDimId" +
"		and b.YearDimId = d.YearDimId" +
"		and b.FinancialAccountDimId = e.FinancialAccountDimId" +
"		and c.Name <= " + lastmonth.ToString() +
"		and d.Name = " + lastyear.ToString() + ") transact 				" +
"	group by transact.Code, transact.Name" +
"	" +
"	union all" +
"	select transact.Code, transact.Name," +
"			0 as beginDebit, 0 as beginCredit," +
"			transact.Debit, transact.Credit" +
"			" +
"	from " +
"		(select e.Code, e.Name, 			" +
"			sum(CreditSum) as Debit, sum(DebitSum) as Credit	" +
"		from FinancialGeneralLedgerByMonth a," +
"			FinancialGeneralLedgerByYear_Fact b," +
"			MonthDim c," +
"			YearDim d," +
"			FinancialAccountDim e" +
"		where a.FinancialGeneralLedgerByYear_FactId = b.FinancialGeneralLedgerByYear_FactId" +
"		and a.MonthDimId = c.MonthDimId" +
"		and b.YearDimId = d.YearDimId" +
"		and b.FinancialAccountDimId = e.FinancialAccountDimId" +
"		and c.Name = " + month.ToString() +
"		and d.Name = " + year.ToString() +
"		group by b.FinancialAccountDimId, e.Code, e.Name) transact	" +
"	) result " +
" where ( " +
	"result.beginDebit > 0 or " +
	"result.beginCredit > 0 or " +
	"result.Debit > 0 or " +
	"result.Credit > 0 " +
") " +
" group by result.Code, result.Name " +
" order by result.Code, result.Name ";

            m_SelectedData = m_Session.ExecuteQuery(m_Sql);
            
            xpDataView = new XPDataView();      
            xpDataView.AddProperty("AccountCode", typeof(string));
            xpDataView.AddProperty("AccountName", typeof(string));
            xpDataView.AddProperty("BeginDebit", typeof(double));
            xpDataView.AddProperty("BeginCredit", typeof(double));
            xpDataView.AddProperty("Debit", typeof(double));
            xpDataView.AddProperty("Credit", typeof(double));
            xpDataView.AddProperty("EndDebit", typeof(double));
            xpDataView.AddProperty("EndCredit", typeof(double));

            xpDataView.LoadData(m_SelectedData);



            WebModule.Accounting.Report.S06_DN report = new WebModule.Accounting.Report.S06_DN();
            report.DataSource = xpDataView;
            report.DataMember = "";
            report.Parameters["fromDate"].Value = fromDate;


            S06dnReportViewer.Report = report;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             m_Session = XpoHelper.GetNewSession();
             if (hS06dn.Contains("show"))
             {
                 load_data();
             }


        }
    }
}