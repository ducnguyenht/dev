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
    public partial class S04a7_DN : System.Web.UI.UserControl
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

        public void load_data()
        {
            DateTime fromDate = DateTime.Parse(hS04a7dnFromDate.Get("fromDate").ToString());
            DateTime toDate = DateTime.Parse(hS04a7dnToDate.Get("toDate").ToString());
                
            string owner = hS04a7dnOwner.Get("owner_id").ToString();
            string asset = hS04a7dnAsset.Get("asset_id").ToString();                

            m_Sql = "" +
"select aaa.DebitAccount, aaa.CreditAccount, " +
"	sum(aaa.Debit) as Debit, " +
"	sum(aaa.NKCT1) as NKCT1, " +
"	sum(aaa.NKCT2) as NKCT2, " +
"	sum(aaa.NKCT3) as NKCT3, " +
"	sum(aaa.NKCT4) as NKCT4, " +
"	sum(aaa.NKCT5) as NKCT5, " +
"	sum(aaa.NKCT6) as NKCT6,	 " +
"	sum(aaa.NKCT8) as NKCT8, " +
"	sum(aaa.NKCT9) as NKCT9, " +
"	sum(aaa.NKCT10) as NKCT10	 " +
"from	 " +
"( " +
"select b.Debit, e.Code as DebitAccount, c.Code as CreditAccount, 0 as NKCT1, 0 as NKCT2,  " +
"		0 as NKCT3, 0 as NKCT4, 0 as NKCT5, 0 as NKCT6, " +
"		0 as NKCT8, 0 as NKCT9, 0 as NKCT10 " +
"from DiaryJournal_Fact a, " +
"		DiaryJournal_Detail b, " +
"		FinancialAccountDim c,  " +
"		FinancialTransactionDim d, " +
"		CorrespondFinancialAccountDim e  		 " +
"where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"and (c.Code like '142%'  " +
"		or c.Code like '152%'  " +
"			or c.Code like '153%'  " +
"				or c.Code like '154%'  " +
"					or c.Code like '214%'  " +
"						or c.Code like '241%'  " +
"							or c.Code like '242%'  " +
"								or c.Code like '334%'  " +
"									or c.Code like '335%'  " +
"										or c.Code like '621%'  " +
"											or c.Code like '622%'  " +
"												or c.Code like '627%'  " +
"	)																								 " +
" and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and b.Debit > 0 " +
"and (len(e.Code) > 0) " +
"and (b.FinancialAccountDimId is null) " +
"and e.Code not in ('1','2','3','4','5','6','7','8') " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, bb.Debit as NKCT1, 0 as NKCT2, " +
"		0 as NKCT3, 0 as NKCT4, 0 as NKCT5, 0 as NKCT6, " +
"		0 as NKCT8, 0 as NKCT9, 0 as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Code as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and c.Code like '111%'  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, 0 as NKCT1, bb.Debit as NKCT2, " +
"		0 as NKCT3, 0 as NKCT4, 0 as NKCT5, 0 as NKCT6, " +
"		0 as NKCT8, 0 as NKCT9, 0 as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Name as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and c.Code like '112%'  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, 0 as NKCT1, 0 as NKCT2, " +
"		bb.Debit as NKCT3, 0 as NKCT4, 0 as NKCT5, 0 as NKCT6, " +
"		0 as NKCT8, 0 as NKCT9, 0 as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Code as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and c.Code like '113%'  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, 0 as NKCT1, 0 as NKCT2, " +
"		0 as NKCT3, bb.Debit as NKCT4, 0 as NKCT5, 0 as NKCT6, " +
"		0 as NKCT8, 0 as NKCT9, 0 as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Code as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and c.Code like '311%'  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, 0 as NKCT1, 0 as NKCT2, " +
"		0 as NKCT3, 0 as NKCT4, bb.Debit as NKCT5, 0 as NKCT6, " +
"		0 as NKCT8, 0 as NKCT9, 0 as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Code as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and c.Code like '331%'  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, 0 as NKCT1, 0 as NKCT2, " +
"		0 as NKCT3, 0 as NKCT4, 0 as NKCT5, bb.Debit as NKCT6, " +
"		0 as NKCT8, 0 as NKCT9, 0 as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Code as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and c.Code like '151%'  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, 0 as NKCT1, 0 as NKCT2, " +
"		0 as NKCT3, 0 as NKCT4, 0 as NKCT5, 0 as NKCT6, " +
"		bb.Debit as NKCT8, 0 as NKCT9, 0 as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Code as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and (c.Code like '131%'  " +
"		or c.Code like '155%'  " +
"			or c.Code like '156%'  " +
"				or	c.Code like '157%'  " +
"					or c.Code like '159%' " +
"						or c.Code like '511%'  " +
"							or c.Code like '515%'  " +
"								or c.Code like '521%'  " +
"									or	c.Code like '531%'  " +
"										or c.Code like '532%' " +
"											or c.Code like '632%'  " +
"												or c.Code like '635%'  " +
"													or c.Code like '641%'  " +
"														or	c.Code like '642%'  " +
"															or c.Code like '711%' " +
"																or c.Code like '811%'  " +
"																	or c.Code like '911%' " +
"	)  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, 0 as NKCT1, 0 as NKCT2, " +
"		0 as NKCT3, 0 as NKCT4, 0 as NKCT5, 0 as NKCT6, " +
"		0 as NKCT8, bb.Debit as NKCT9, 0 as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Code as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and (c.Code like '211%' " +
"			or c.Code like '212%' " +
"				or c.Code like '213%' " +
"					or c.Code like '217%')  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
"union all " +
"select aa.Debit, aa.DebitAccount, aa.Code as CreditAccount, 0 as NKCT1, 0 as NKCT2, " +
"		0 as NKCT3, 0 as NKCT4, 0 as NKCT5, 0 as NKCT6, " +
"		0 as NKCT8, 0 as NKCT9, bb.Debit as NKCT10 " +
"from " +
"( " +
"select 0 as Debit, nkct1_e.Code as DebitAccount, nkct1_c.Code, nkct1_b.Debit as NKCT1 " +
"from DiaryJournal_Fact nkct1_a, " +
"		DiaryJournal_Detail nkct1_b, " +
"		FinancialAccountDim nkct1_c,  " +
"		FinancialTransactionDim nkct1_d, " +
"		CorrespondFinancialAccountDim nkct1_e  		 " +
"where nkct1_a.DiaryJournal_FactId = nkct1_b.DiaryJournal_FactId  " +
"and nkct1_a.FinancialAccountDimId = nkct1_c.FinancialAccountDimId  " +
"and nkct1_b.FinancialTransactionDimId = nkct1_d.FinancialTransactionDimId  " +
"and nkct1_b.CorrespondFinancialAccountDimId = nkct1_e.CorrespondFinancialAccountDimId  " +
"and (nkct1_c.Code like '142%'  " +
"		or nkct1_c.Code like '152%'  " +
"			or nkct1_c.Code like '153%'  " +
"				or nkct1_c.Code like '154%'  " +
"					or nkct1_c.Code like '214%'  " +
"						or nkct1_c.Code like '241%'  " +
"							or nkct1_c.Code like '242%'  " +
"								or nkct1_c.Code like '334%'  " +
"									or nkct1_c.Code like '335%'  " +
"										or nkct1_c.Code like '621%'  " +
"											or nkct1_c.Code like '622%'  " +
"												or nkct1_c.Code like '627%'  " +
"	)	 " +
" and (nkct1_d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
" and nkct1_d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"and nkct1_b.Debit > 0 " +
"and (len(nkct1_e.Code) > 0) " +
"and (nkct1_b.FinancialAccountDimId is null) " +
"and nkct1_e.Code not in ('1','2','3','4','5','6','7','8')) aa, " +
"( " +
"	select b.Debit, e.Code " +
"	from DiaryJournal_Fact a, " +
"			DiaryJournal_Detail b, " +
"			FinancialAccountDim c,  " +
"			FinancialTransactionDim d, " +
"			CorrespondFinancialAccountDim e  		 " +
"	where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"	and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"	and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"	and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"	and c.Code like '133%'  " +
"   and (d.IssueDate >= " + string.Format("'{0}-{1}-{2} 00:00:00'", fromDate.Year, fromDate.Month, fromDate.Day) +
"   and d.IssueDate <=  " + string.Format("'{0}-{1}-{2} 23:59:00'", toDate.Year, toDate.Month, toDate.Day) + " )  " +
"	and b.Debit > 0 " +
"	and (len(e.Code) > 0) " +
"	and (b.FinancialAccountDimId is null) " +
"	and e.Code not in ('1','2','3','4','5','6','7','8')) bb	 " +
"where aa.DebitAccount = bb.Code " +
") aaa " +
"group by aaa.CreditAccount, aaa.DebitAccount ";

            // header
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("stt");
            dataTable.Columns.Add("tk");

            GridViewDataColumn caption = new GridViewDataColumn();
            caption.Caption = "Số TT";
            caption.FieldName = "stt";
            S04a7dnASPxGridView.Columns.Add(caption);

            caption = new GridViewDataColumn();
            caption.Caption = "Các TK ghi có / Các TK ghi nợ ";
            caption.FieldName = "tk";
            S04a7dnASPxGridView.Columns.Add(caption);

            //bandColumn = new GridViewBandColumn("Số dư đầu tháng");
            //S04a7dnASPxGridView.Columns.Add(bandColumn);

            //caption = new GridViewDataTextColumn();
            //caption.Caption = "Nợ";
            //caption.FieldName = "no";
            //bandColumn.Columns.Add(caption);
            //dataTable.Columns.Add("no", typeof(double));

            //caption = new GridViewDataTextColumn();
            //caption.Caption = "Có";
            //caption.FieldName = "co";
            //bandColumn.Columns.Add(caption);
            //dataTable.Columns.Add("co", typeof(double));


            try
            {
                seletectedData = session.ExecuteQuery(m_Sql);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //Debit

            List<string> listHeader = new List<string>();
            //bandColumn = new GridViewBandColumn("Ghi Nợ tài khoản " + account + " Ghi Có các tài khoản");
            //S04a7dnASPxGridView.Columns.Add(bandColumn);

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {                    
                    if (!listHeader.Contains(col.Values[1].ToString()))
                    {
                        listHeader.Add(col.Values[1].ToString());
                    }                    
                }
            }

            listHeader.Sort();

            listHeader.Add("NKCT1");listHeader.Add("NKCT2");listHeader.Add("NKCT3");listHeader.Add("NKCT4");listHeader.Add("NKCT5");
            listHeader.Add("NKCT6");listHeader.Add("NKCT8");listHeader.Add("NKCT9");listHeader.Add("NKCT10");

            int index;

            for (index = 0; index < listHeader.Count; index++)
            {

                GridViewDataTextColumn c = new GridViewDataTextColumn();
                c.Caption = listHeader[index].ToString();
                c.FieldName = listHeader[index].ToString();
                c.PropertiesEdit.DisplayFormatString = "#,#";
                
                if (listHeader[index].ToString().Contains("NKCT"))
                {
                    if (listHeader[index].ToString() == "NKCT1")
                    {
                        bandColumn = new GridViewBandColumn("Các TK phản ánh ở các NKCT khác");
                        S04a7dnASPxGridView.Columns.Add(bandColumn);
                    }

                    bandColumn.Columns.Add(c);                   
                }
                else
                {
                    S04a7dnASPxGridView.Columns.Add(c);
                }

                dataTable.Columns.Add(c.FieldName, typeof(double));
            }

            caption = new GridViewDataTextColumn();
            String space = new String(' ', 240 - maxMonth * 20);
            caption.Caption = space + "Tổng cộng chi phí";
            caption.FieldName = "total";
            caption.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(caption);

            dataTable.Columns.Add("total", typeof(double));
           
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

                    line["tk"] = col.Values[0].ToString();
                    line["NKCT1"] = col.Values[3].ToString();
                    line["NKCT2"] = col.Values[4].ToString();
                    line["NKCT3"] = col.Values[5].ToString();
                    line["NKCT4"] = col.Values[6].ToString();
                    line["NKCT5"] = col.Values[7].ToString();
                    line["NKCT6"] = col.Values[8].ToString();
                    //line["NKCT7"] = col.Values[9].ToString();   
                    line["NKCT8"] = col.Values[9].ToString();
                    line["NKCT9"] = col.Values[10].ToString();
                    line["NKCT10"] = col.Values[11].ToString();
                    line[col.Values[1].ToString()] = col.Values[2].ToString();

                    total += double.Parse(col.Values[2].ToString())
                            + double.Parse(col.Values[3].ToString()) +
                            +double.Parse(col.Values[4].ToString()) +
                            +double.Parse(col.Values[5].ToString()) +
                            +double.Parse(col.Values[6].ToString()) +
                            +double.Parse(col.Values[7].ToString()) +
                            +double.Parse(col.Values[8].ToString()) +
                            +double.Parse(col.Values[9].ToString()) +
                            +double.Parse(col.Values[10].ToString()) +
                            +double.Parse(col.Values[11].ToString());
                            //+double.Parse(col.Values[12].ToString());

                    line["total"] = total;
                }

            }

            // sum total

            widthEndCol = 0;

            line = dataTable.NewRow();
            line["tk"] = "Tổng Cộng";
            dataTable.Rows.Add(line);


            for (int i = 2; i < dataTable.Columns.Count; i++)
            {
                line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
            }
       

            //if (240 - widthEndCol * 3 > 0)
            //{
            //    space = new String(' ', 240 - widthEndCol * 3);
            //}

            //S04a7dnASPxGridView.Columns["tk"].Caption = space + "Cộng";

            S04a7dnASPxGridView.DataSource = dataTable;
            S04a7dnASPxGridView.KeyFieldName = "stt";
            S04a7dnASPxGridView.DataBind();

            WebModule.Accounting.Report.S04a7_DN ctd = new WebModule.Accounting.Report.S04a7_DN();
            ctd.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = S04a7dnASPxGridViewExporter };

            ctd.Parameters["datePeriod"].Value = new DateTime(fromDate.Year, fromDate.Month, 1);

            S04a7dnReportViewer.Report = ctd;
            
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hS04a7dn.Contains("show"))
            {
                load_data();
            }
        }
    }
}