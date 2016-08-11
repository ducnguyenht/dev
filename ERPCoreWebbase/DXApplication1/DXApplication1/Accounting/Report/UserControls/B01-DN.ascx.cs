using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebModule.Accounting.Report.Data;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using NAS.DAL;
using System.Data;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class B01_DN : System.Web.UI.UserControl
    {
        List<CB01DN> m_ListInitB01dn = null;
        CB01DN m_CB01DN = null;

        string m_Sql = "";
        Session m_Session = null;
        SelectedData m_SelectedData;
        XPDataView xpDataView;
        DataTable m_DataTable;

        private string SumByExpression(string code)
        {
            double resultBegin = 0;
            double resultEnd = 0;       
            string[] array = null;

            DataRow[] row = m_DataTable.Select("Code = '" + code + "'");

            if (row[0]["Expresstion"].ToString() == "")
            {
                return row[0]["BeginBalance"].ToString() + "|" + row[0]["EndBalance"].ToString();
            }
            else
            {
                array = row[0]["Expresstion"].ToString().Split('+');
                for (int index = 0; index < array.Count(); index++)
                {
                    string result = SumByExpression(array[index]);

                    resultBegin += double.Parse(result.Split('|')[0].ToString());
                    resultEnd += double.Parse(result.Split('|')[1].ToString());                    
                }

                m_DataTable.Select("Code = '" + code + "'")[0]["Expresstion"] = "";
                m_DataTable.Select("Code = '" + code + "'")[0]["BeginBalance"] = resultBegin;
                m_DataTable.Select("Code = '" + code + "'")[0]["EndBalance"] = resultEnd;

                return resultBegin.ToString() + "|" + resultEnd.ToString();
            }            
        }              

        private void InitB01dnData()
        {
            m_ListInitB01dn = new List<CB01DN>();

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 1,
                AccountName = "A. TÀI SẢN NGẮN HẠN (100=110+120+130+140+150)",
                OrderCode = "100",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "110+120+130+140+150",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 2,
                AccountName = "I. Tiền và các khoản tương đương tiền",
                OrderCode = "110",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "110+120+130+140+150",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 3,
                AccountName = "1. Tiền ",
                OrderCode = "111",
                Description = "V.01",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "111A+111B+111C",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 4,
                AccountName = "- Tiền mặt",
                OrderCode = "111A",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "111",
                Expression = "",
                Detail = true
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 5,
                AccountName = "- Tiền gởi ngân hàng",
                OrderCode = "111B",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "112",
                Expression = "",
                Detail = true
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 6,
                AccountName = "- Tiền đang chuyển",
                OrderCode = "111C",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "113",
                Expression = "",
                Detail = true
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 7,
                AccountName = "2. Các khoản tương đương tiền",
                OrderCode = "112",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "121",
                Expression = "",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 8,
                AccountName = "II. Các khoản đầu tư tài chính ngắn hạn",
                OrderCode = "120",
                Description = "V.02",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "121+129",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 9,
                AccountName = "1. Đầu tư ngắn hạn",
                OrderCode = "121",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "121",
                Expression = "",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 10,
                AccountName = "2. Dự phòng giảm giá đầu tư ngắn hạn",
                OrderCode = "129",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "129",
                Expression = "",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 11,
                AccountName = "III. Các khoản phải thu ngắn hạn",
                OrderCode = "130",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "131+132+133+134+135+139",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 11,
                AccountName = "1. Phải thu của khách hàng",
                OrderCode = "131",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "131",
                Expression = "",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "2. Trả trước cho người bán",
                OrderCode = "132",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "331",
                Expression = "",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "3. Phải thu nội bộ ngắn hạn",
                OrderCode = "133",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "133A+133B",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Vốn KD ở các đơn vị trực thuộc",
                OrderCode = "133A",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "13611",
                Expression = "",
                Detail = true
            };

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Phải thu nội bộ khác",
                OrderCode = "133B",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "13681",
                Expression = "",
                Detail = true
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "4. Phải thu theo tiến độ kế hoạch hợp đồng xây dựng",
                OrderCode = "134",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "337",
                Expression = "",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "5. Các khoản phải thu khác",
                OrderCode = "135",
                Description = "V.03",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "135A+135B+135C+135D",
                Detail = false
            };

            m_ListInitB01dn.Add(m_CB01DN);
            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Phải thu khác (13881)",
                OrderCode = "135A",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "13881",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Phải thu khác (33881)",
                OrderCode = "135B",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "33881",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Phải thu khác (1385)",
                OrderCode = "135C",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "1385",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Phải thu khác (334)",
                OrderCode = "135D",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "334",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "6. Dự phòng phải thu ngắn hạn khó đòi (*)",
                OrderCode = "135",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "135A+135B+135C+135D",
                Detail = false
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "IV. Hàng tồn kho",
                OrderCode = "140",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "141+149",
                Detail = false
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "1. Hàng tồn kho",
                OrderCode = "141",
                Description = "V.04",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "",
                Expression = "141A+141B+141C+141D+141E+141F+141G+141H",
                Detail = false
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Hàng mua đang đi trên đường",
                OrderCode = "141A",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "151",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Nguyên liệu, vật liệu tồn kho",
                OrderCode = "141B",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "152",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Công cụ, dụng cụ trong kho",
                OrderCode = "141C",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "153",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Chi phí SXKD dở dang",
                OrderCode = "141D",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "154",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);

            m_CB01DN = new CB01DN()
            {
                OrderNumber = 12,
                AccountName = "- Chi phí SXKD dở dang",
                OrderCode = "141D",
                Description = "",
                EndBalance = 0,
                BeginBalance = 0,
                AccountCode = "154",
                Expression = "",
                Detail = true
            };
            m_ListInitB01dn.Add(m_CB01DN);


        }

        private void load_data()
        {
            int month = Int32.Parse(hB01dnMonth.Get("month_id").ToString());
            int year = Int32.Parse(hB01dnYear.Get("year_id").ToString());
            string owner = hB01dnOwner.Get("owner_id").ToString();
            string asset = hB01dnAsset.Get("asset_id").ToString();

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
"select fact.OrderNumber, fact.Name, fact.Code, fact.Description, " +
"		case value.BeginDebit " +
"			when 0 THEN value.BeginCredit ELSE isnull(BeginDebit,0) end as BeginBalance, 				 " +
"		case value.EndDebit " +
"			when 0 THEN value.EndCredit ELSE isnull(EndDebit,0) end as EndBalance,  " +
"		fact.Expresstion, fact.Detail		 " +
"from FinancialBalanceAccount_Fact fact " +
"left join  " +
"( " +
" " +
"	select result.FinancialAccountDimId, result.Code as AccountCode, result.Name as AccountName,  " +
"		sum(result.beginDebit) as BeginDebit,  " +
"		sum(result.beginCredit) as BeginCredit, " +
"		sum(result.Debit) as Debit,  " +
"		sum(result.Credit) as Credit, " +
"		case result.accounttype " +
"			when 'ASSET' THEN (sum(result.beginDebit) + sum(result.Debit) - sum(result.Credit)) ELSE 0  end as EndDebit, 		 " +
"		case result.accounttype	 " +
"			when 'LIABILITY' THEN (sum(result.beginCredit) + sum(result.Credit) - sum(result.Debit)) ELSE 0 end as EndCredit	 " +
"	from	 " +
"		(select transact.FinancialAccountDimId, transact.Code, transact.Name,  " +
"			case accttype.accounttype " +
"				when 'ASSET' THEN (sum(isnull(balance.beginbalance,0)) + sum(transact.Debit) - sum(Credit)) ELSE 0  end as beginDebit, 		 " +
"			case accttype.accounttype	 " +
"				when 'LIABILITY' THEN (sum(isnull(balance.beginbalance,0)) + sum(transact.Credit) - sum(Debit)) ELSE 0 end as beginCredit,	 " +
"			0 as Debit, 0 as Credit, " +
"			accttype.accounttype	 " +
"		from	 " +
"			(select e.FinancialAccountDimId, e.Code, e.Name, d.Name as YearPeriod, CreditSum as Debit, DebitSum as Credit " +
"			from FinancialGeneralLedgerByMonth a, " +
"				FinancialGeneralLedgerByYear_Fact b, " +
"				MonthDim c, " +
"				YearDim d, " +
"				FinancialAccountDim e " +
"			where a.FinancialGeneralLedgerByYear_FactId = b.FinancialGeneralLedgerByYear_FactId " +
"			and a.MonthDimId = c.MonthDimId " +
"			and b.YearDimId = d.YearDimId " +
"			and b.FinancialAccountDimId = e.FinancialAccountDimId " +
"			and c.Name <= " + lastmonth.ToString() + 
"			and d.Name = " + lastyear.ToString() + ") transact  " +
"			 " +
"		left join  " +
"		 " +
"			(select year(d.FromDateTime) as YearPeriod, b.Debit as beginbalance, c.Code 		 " +
"			from BalanceForwardTransaction a, GeneralJournal b, Account c,  " +
"				AccountingPeriod d, [Transaction] e " +
"			where a.TransactionId = b.TransactionId " +
"			and b.AccountId = c.AccountId " +
"			and a.TransactionId = e.TransactionId " +
"			and d.AccountingPeriodId = e.AccountingPeriodId " +
"			and b.Debit > 0	 " +
"			and (month(d.FromDateTime) = 1 and year(d.FromDateTime) = 2014)) balance		 " +
"		on transact.YearPeriod = balance.YearPeriod " +
"		and charindex(transact.Code, balance.Code) != 0 " +
"		 " +
"		left join  " +
"		 " +
"			(select a.Code, b.Code as accounttype " +
"			from account a, AccountCategory b, AccountType c  " +
"			where a.AccountTypeId = c.AccountTypeId " +
"			and b.AccountCategoryId = c.AccountCategoryId) accttype " +
"		on transact.Code = accttype.Code " +
"		 " +
"		group by transact.FinancialAccountDimId, transact.Code, accttype.accounttype, transact.Name " +
"		 " +
"		union all " +
"		select transact.FinancialAccountDimId, transact.Code, transact.Name, " +
"				0 as beginDebit, 0 as beginCredit, " +
"				transact.Debit, transact.Credit, " +
"				accttype.accounttype " +
"		from  " +
"			(select e.FinancialAccountDimId, e.Code, e.Name, 			 " +
"				sum(CreditSum) as Debit, sum(DebitSum) as Credit	 " +
"			from FinancialGeneralLedgerByMonth a, " +
"				FinancialGeneralLedgerByYear_Fact b, " +
"				MonthDim c, " +
"				YearDim d, " +
"				FinancialAccountDim e " +
"			where a.FinancialGeneralLedgerByYear_FactId = b.FinancialGeneralLedgerByYear_FactId " +
"			and a.MonthDimId = c.MonthDimId " +
"			and b.YearDimId = d.YearDimId " +
"			and b.FinancialAccountDimId = e.FinancialAccountDimId " +
"			and c.Name = " + month.ToString() +
"			and d.Name = " + year.ToString() + 
"			group by e.FinancialAccountDimId, b.FinancialAccountDimId, e.Code, e.Name) transact " +
"			left join 	 " +
"				(select a.Code, b.Code as accounttype " +
"				from account a, AccountCategory b, AccountType c  " +
"				where a.AccountTypeId = c.AccountTypeId " +
"				and b.AccountCategoryId = c.AccountCategoryId) accttype " +
"			on transact.Code = accttype.Code	 " +
"		) result " +
"	group by result.FinancialAccountDimId, result.Code, result.Name, result.accounttype " +
") value " +
"on fact.FinancialAccountDimId = value.FinancialAccountDimId ";

            m_SelectedData = m_Session.ExecuteQuery(m_Sql);

            m_DataTable = new DataTable();
            m_DataTable.Columns.Add("OrderNumber", typeof(int));
            m_DataTable.Columns.Add("Name", typeof(string));
            m_DataTable.Columns.Add("Code", typeof(string));
            m_DataTable.Columns.Add("Description", typeof(string));
            m_DataTable.Columns.Add("BeginBalance", typeof(double));
            m_DataTable.Columns.Add("EndBalance", typeof(double));
            m_DataTable.Columns.Add("Expresstion", typeof(string));
            m_DataTable.Columns.Add("Detail", typeof(bool));


            foreach (var row in m_SelectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    DataRow line = m_DataTable.NewRow();
                    
                    line["OrderNumber"] = int.Parse(col.Values[0].ToString());
                    line["Name"] = col.Values[1].ToString();
                    line["Code"] = col.Values[2].ToString();
                    line["Description"] = col.Values[3].ToString();
                    line["BeginBalance"] = double.Parse(col.Values[4].ToString());
                    line["EndBalance"] = double.Parse(col.Values[5].ToString());
                    line["Expresstion"] = col.Values[6].ToString();
                    line["Detail"] = bool.Parse(col.Values[7].ToString());
                    
                    m_DataTable.Rows.Add(line);
                }
            }
         
            DataRow[] rowSum = m_DataTable.Select("Expresstion <> ''");
         
            while (rowSum.Count() > 0)
            {
                for (int index = 0; index < m_DataTable.Rows.Count; index++)
                {
                    if (m_DataTable.Rows[index]["Code"] != null)
                    {
                        SumByExpression(m_DataTable.Rows[index]["Code"].ToString());
                    }
                }       

                rowSum = m_DataTable.Select("Expresstion <> ''");
            }

            DataTable result = m_DataTable.Clone();

            foreach(DataRow row in m_DataTable.Rows)
            {
                if (!bool.Parse(row["Detail"].ToString()))
                {

                    result.Rows.Add(row.ItemArray);
                }
            }
            
            WebModule.Accounting.Report.B01_DN report = new WebModule.Accounting.Report.B01_DN();
            report.DataSource = result;
            
            report.DataMember = "";

            B01dnReportViewer.Report = report;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Session = XpoHelper.GetNewSession();
            if (hB01dn.Contains("show"))
            {
                load_data();
            }
        }
    }
}