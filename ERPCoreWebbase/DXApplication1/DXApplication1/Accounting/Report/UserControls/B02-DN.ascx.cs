using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Xpo.DB;
using NAS.DAL.Report;
using Evaluant.Calculator;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class B02_DN : System.Web.UI.UserControl
    {       
        string m_Sql = "";
        Session m_Session = null;
        SelectedData m_SelectedData;
        XPDataView xpDataView;
        DataTable m_DataTable;
        bool m_Transfered = false;
        string m_SqlDebitLike = "";
        string m_SqlCreditLike = "";
        string[] m_SplitAccount;

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

        private void InitB02dnData()
        {
            XPCollection<FinancialBusinessResult_Fact> FinancialBusinessResult_Facts = new XPCollection<FinancialBusinessResult_Fact>(m_Session);
            if (FinancialBusinessResult_Facts.Count == 0)
            {
                //1
                FinancialBusinessResult_Fact financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "1. Doanh thu bán hàng và cung cấp dịch vụ";
                financialBusinessResult_Fact.Code = "01";
                financialBusinessResult_Fact.Description = "VI.25";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "911";
                financialBusinessResult_Fact.CreditAccount = "511,512";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //2
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "2. Các khoản giảm trừ doanh thu";
                financialBusinessResult_Fact.Code = "02";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "521,531,532";
                financialBusinessResult_Fact.CreditAccount = "911";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //3
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "3. Doanh thu thuần về bán hàng và cung cấp dịch vụ (10=01-02)";
                financialBusinessResult_Fact.Code = "10";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "";
                financialBusinessResult_Fact.CreditAccount = "";
                financialBusinessResult_Fact.Expresstion = "01-02";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //4
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "4. Giá vốn hàng bán";
                financialBusinessResult_Fact.Code = "11";
                financialBusinessResult_Fact.Description = "VI.27";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "611,621,622,623,627,631,632";
                financialBusinessResult_Fact.CreditAccount = "911";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();


                //5
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "5. Lợi nhuận gộp về bán hàng và cung cấp dịch vụ(20=10-11)";
                financialBusinessResult_Fact.Code = "20";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "";
                financialBusinessResult_Fact.CreditAccount = "";
                financialBusinessResult_Fact.Expresstion = "10-11";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //6
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "6. Doanh thu hoạt động tài chính";
                financialBusinessResult_Fact.Code = "21";
                financialBusinessResult_Fact.Description = "VI.26";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "911";
                financialBusinessResult_Fact.CreditAccount = "515";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //7
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "7. Chi phí tài chính";
                financialBusinessResult_Fact.Code = "22";
                financialBusinessResult_Fact.Description = "VI.28";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "635";
                financialBusinessResult_Fact.CreditAccount = "911";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();
         
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "- Trong đó : Chi phí lãi vay";
                financialBusinessResult_Fact.Code = "23";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "";
                financialBusinessResult_Fact.CreditAccount = "";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //8
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "8. Chi phí bán hàng";
                financialBusinessResult_Fact.Code = "24";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "641";
                financialBusinessResult_Fact.CreditAccount = "911";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();


                //9
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "9. Chi phí quản lý doanh nghiệp";
                financialBusinessResult_Fact.Code = "25";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "642";
                financialBusinessResult_Fact.CreditAccount = "911";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();


                //10
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "10. Lợi nhuận thuần từ hoạt động kinh doanh (30=20+(21-22)-(24+25))";
                financialBusinessResult_Fact.Code = "30";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "";
                financialBusinessResult_Fact.CreditAccount = "";
                financialBusinessResult_Fact.Expresstion = "20+21-22-24-25";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //11
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "11. Thu nhập khác";
                financialBusinessResult_Fact.Code = "31";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "911";
                financialBusinessResult_Fact.CreditAccount = "711";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //12
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "12. Chi phí khác";
                financialBusinessResult_Fact.Code = "32";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "811";
                financialBusinessResult_Fact.CreditAccount = "911";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //13
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "13. Lợi nhuận khác (40=31-32)";
                financialBusinessResult_Fact.Code = "40";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "";
                financialBusinessResult_Fact.CreditAccount = "";
                financialBusinessResult_Fact.Expresstion = "31-32";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //14
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "14. Tổng lợi nhuận kế toán trước thuế (50=30+40)";
                financialBusinessResult_Fact.Code = "50";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "";
                financialBusinessResult_Fact.CreditAccount = "";
                financialBusinessResult_Fact.Expresstion = "30+40";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //15
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "15. Chi phí thuế TNDN hiện hành";
                financialBusinessResult_Fact.Code = "51";
                financialBusinessResult_Fact.Description = "VI.30";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "911";
                financialBusinessResult_Fact.CreditAccount = "3334";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();

                //16
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "16. Chi phí thuế TNDN hoãn lại";
                financialBusinessResult_Fact.Code = "52";
                financialBusinessResult_Fact.Description = "VI.30";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "3334";
                financialBusinessResult_Fact.CreditAccount = "911";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();


                //17
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "17. Lợi nhuận sau thuế thu nhập doanh nghiệp (60=50-51-52)";
                financialBusinessResult_Fact.Code = "60";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "";
                financialBusinessResult_Fact.CreditAccount = "";
                financialBusinessResult_Fact.Expresstion = "50-51-52";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();


                //18
                financialBusinessResult_Fact = new FinancialBusinessResult_Fact(m_Session);
                financialBusinessResult_Fact.Name = "18. Lãi cơ bản trên cổ phiếu (*)";
                financialBusinessResult_Fact.Code = "70";
                financialBusinessResult_Fact.Description = "";
                financialBusinessResult_Fact.Balance = 0;
                financialBusinessResult_Fact.LastBalance = 0;
                financialBusinessResult_Fact.DebitAccount = "";
                financialBusinessResult_Fact.CreditAccount = "";
                financialBusinessResult_Fact.Expresstion = "";
                financialBusinessResult_Fact.Detail = false;
                financialBusinessResult_Fact.RowStatus = 1;

                financialBusinessResult_Fact.Save();
            }

        }

        private void load_data()
        {
            InitB02dnData();

            DateTime fromDate = DateTime.Parse(hB02dnFromDate.Get("fromDate").ToString());
            DateTime toDate = DateTime.Parse(hB02dnToDate.Get("toDate").ToString());
            string owner = hB02dnOwner.Get("owner_id").ToString();
            string asset = hB02dnAsset.Get("asset_id").ToString();

            m_Sql = "select Name, Code, Description, Balance, LastBalance, Expresstion, Detail,  " +
                        "DebitAccount, CreditAccount " +
                    "from FinancialBusinessResult_Fact ";    

            m_SelectedData = m_Session.ExecuteQuery(m_Sql);

            m_DataTable = new DataTable();            
            m_DataTable.Columns.Add("Name", typeof(string));
            m_DataTable.Columns.Add("Code", typeof(string));
            m_DataTable.Columns.Add("Description", typeof(string));
            m_DataTable.Columns.Add("Balance", typeof(double));
            m_DataTable.Columns.Add("LastBalance", typeof(double));
            m_DataTable.Columns.Add("Expresstion", typeof(string));
            m_DataTable.Columns.Add("Detail", typeof(bool));
            m_DataTable.Columns.Add("DebitAccount", typeof(string));
            m_DataTable.Columns.Add("CreditAccount", typeof(string));

            foreach (var row in m_SelectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    DataRow line = m_DataTable.NewRow();
                                        
                    line["Name"] = col.Values[0].ToString();
                    line["Code"] = col.Values[1].ToString();
                    line["Description"] = col.Values[2].ToString();
                    line["Balance"] = double.Parse(col.Values[3].ToString());
                    line["LastBalance"] = double.Parse(col.Values[4].ToString());
                    line["Expresstion"] = col.Values[5].ToString();
                    line["Detail"] = bool.Parse(col.Values[6].ToString());
                    line["DebitAccount"] = col.Values[7].ToString();
                    line["CreditAccount"] =col.Values[8].ToString();
                    
                    m_DataTable.Rows.Add(line);
                }
            }

            m_Transfered = bool.Parse(hB02dnTransfered.Get("transfered").ToString());

            if (!m_Transfered)
            {
                foreach (var row in m_SelectedData.ResultSet)
                {
                    foreach (var col in row.Rows)
                    {                        
                        if (col.Values[7].ToString() == "911")
                        {
                            m_SplitAccount = col.Values[8].ToString().Split(',');
                            foreach (string account in m_SplitAccount)
                            {
                                if (m_SqlCreditLike == "")
                                {
                                    m_SqlCreditLike = "and (c.Code like '" + account + "%'";
                                }
                                else
                                {
                                    m_SqlCreditLike += " or c.Code like '" + account + "%'";
                                }
                            }
                        }
                        else if (col.Values[8].ToString() == "911")
                        {
                            m_SplitAccount = col.Values[7].ToString().Split(',');
                            foreach (string account in m_SplitAccount)
                            {
                                if (m_SqlDebitLike == "")
                                {
                                    m_SqlDebitLike = "and (c.Code like '" + account + "%'";
                                }
                                else
                                {
                                    m_SqlDebitLike += " or c.Code like '" + account + "%'";
                                }
                            }
                        }
                    }
                }

                m_SqlDebitLike += ") ";
                m_SqlCreditLike += ") ";
            }
            else
            {
                m_SqlDebitLike = "and (c.Code like '911%' ";
                m_SqlCreditLike = "and (c.Code like '911%' ";
                m_SqlDebitLike += ") ";
                m_SqlCreditLike += ") ";
            }


            // This Year
            m_Sql = "" +
                " select * from (" +
"select distinct d.IssueDate, b.Credit as Balance, c.Code as DebitAccount, e.Code as CreditAccount, d.FinancialTransactionDimId " +
"from DiaryJournal_Fact a, " +
"		DiaryJournal_Detail b, " +
"		FinancialAccountDim c,  " +
"		FinancialTransactionDim d, " +
"		CorrespondFinancialAccountDim e  		 " +
"where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
m_SqlDebitLike +
"and d.IssueDate >= '2014-01-01 00:00:00' and d.IssueDate <= '2014-12-31 23:59:00' " +
"and (b.Credit > 0) " +
"and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
"and (b.FinancialAccountDimId is null) " +
"and e.Code not in ('1','2','3','4','5','6','7','8') " +
"union all " +
"select distinct d.IssueDate, b.Debit as Balance, e.Code as DebitAccount, c.Code as CreditAccount, d.FinancialTransactionDimId " +
"from DiaryJournal_Fact a, " +
"		DiaryJournal_Detail b, " +
"		FinancialAccountDim c,  " +
"		FinancialTransactionDim d, " +
"		CorrespondFinancialAccountDim e  		 " +
"where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
m_SqlCreditLike +
"and d.IssueDate >= '2014-01-01 00:00:00' and d.IssueDate <= '2014-12-31 23:59:00' " +
"and (b.Debit > 0) " +
"and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
"and (b.FinancialAccountDimId is null) " +
"and e.Code not in ('1','2','3','4','5','6','7','8')) result ";

            m_SelectedData = m_Session.ExecuteQuery(m_Sql);

            foreach (var row in m_SelectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    foreach(DataRow dr in m_DataTable.Rows)
                    {
                        if (dr["DebitAccount"].ToString() == "911")
                        {
                            m_SplitAccount = dr["CreditAccount"].ToString().Split(',');
                            foreach (string account in m_SplitAccount)
                            {
                                if (col.Values[3].ToString().Contains(account))
                                {
                                    dr["Balance"] = double.Parse(dr["Balance"].ToString()) + double.Parse(col.Values[1].ToString());
                                }
                            }
                        }
                        else if (dr["CreditAccount"].ToString() == "911")
                        {
                            m_SplitAccount = dr["DebitAccount"].ToString().Split(',');
                            foreach (string account in m_SplitAccount)
                            {
                                if (col.Values[2].ToString().Contains(account))
                                {
                                    dr["Balance"] = double.Parse(dr["Balance"].ToString()) + double.Parse(col.Values[1].ToString());
                                }
                            }
                        }
                    }
                }
            }


            foreach (DataRow row in m_DataTable.Rows)
            {
                if (row["Expresstion"].ToString().Trim() != "")
                {
                    foreach (DataRow dr in m_DataTable.Rows)
                    {
                        if (row["Expresstion"].ToString().Contains(dr["Code"].ToString()))
                        {
                            row["Expresstion"] = row["Expresstion"].ToString().Replace(dr["Code"].ToString(), "[" + dr["Code"].ToString() + "]");
                        }
                    }

                    Expression ex = new Expression(row["Expresstion"].ToString());
                    foreach (DataRow dr in m_DataTable.Rows)
                    {
                        if (row["Expresstion"].ToString().Contains(dr["Code"].ToString()))
                        {
                            ex.Parameters.Add(dr["Code"].ToString(), double.Parse(dr["Balance"].ToString()));
                        }
                    }

                    row["Balance"] = double.Parse(ex.Evaluate().ToString());
                }
            }


            // Last Year
            m_SqlDebitLike = "and (c.Code like '911%' ";
            m_SqlCreditLike = "and (c.Code like '911%' ";
            m_SqlDebitLike += ") ";
            m_SqlCreditLike += ") ";

            m_Sql = "" +
                " select * from (" +
"select distinct d.IssueDate, b.Credit as LastBalance, c.Code as DebitAccount, e.Code as CreditAccount, d.FinancialTransactionDimId " +
"from DiaryJournal_Fact a, " +
"		DiaryJournal_Detail b, " +
"		FinancialAccountDim c,  " +
"		FinancialTransactionDim d, " +
"		CorrespondFinancialAccountDim e  		 " +
"where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
m_SqlDebitLike +
"and d.IssueDate >= '2013-01-01 00:00:00' and d.IssueDate <= '2013-12-31 23:59:00' " +
"and (b.Credit > 0) " +
"and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
"and (b.FinancialAccountDimId is null) " +
"and e.Code not in ('1','2','3','4','5','6','7','8') " +
"union all " +
"select distinct d.IssueDate, b.Debit as LastBalance, e.Code as DebitAccount, c.Code as CreditAccount, d.FinancialTransactionDimId " +
"from DiaryJournal_Fact a, " +
"		DiaryJournal_Detail b, " +
"		FinancialAccountDim c,  " +
"		FinancialTransactionDim d, " +
"		CorrespondFinancialAccountDim e  		 " +
"where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
m_SqlCreditLike +
"and d.IssueDate >= '2013-01-01 00:00:00' and d.IssueDate <= '2013-12-31 23:59:00' " +
"and (b.Debit > 0) " +
"and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
"and (b.FinancialAccountDimId is null) " +
"and e.Code not in ('1','2','3','4','5','6','7','8')) result ";

            m_SelectedData = m_Session.ExecuteQuery(m_Sql);

            foreach (var row in m_SelectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    foreach (DataRow dr in m_DataTable.Rows)
                    {
                        if (dr["DebitAccount"].ToString() == "911")
                        {
                            m_SplitAccount = dr["CreditAccount"].ToString().Split(',');
                            foreach (string account in m_SplitAccount)
                            {
                                if (col.Values[3].ToString().Contains(account))
                                {
                                    dr["LastBalance"] = double.Parse(dr["LastBalance"].ToString()) + double.Parse(col.Values[1].ToString());
                                }
                            }
                        }
                        else if (dr["CreditAccount"].ToString() == "911")
                        {
                            m_SplitAccount = dr["DebitAccount"].ToString().Split(',');
                            foreach (string account in m_SplitAccount)
                            {
                                if (col.Values[2].ToString().Contains(account))
                                {
                                    dr["LastBalance"] = double.Parse(dr["LastBalance"].ToString()) + double.Parse(col.Values[1].ToString());
                                }
                            }
                        }
                    }
                }
            }


            foreach (DataRow row in m_DataTable.Rows)
            {
                if (row["Expresstion"].ToString().Trim() != "")
                {
                    //foreach (DataRow dr in m_DataTable.Rows)
                    //{
                    //    if (row["Expresstion"].ToString().Contains(dr["Code"].ToString()))
                    //    {
                    //        row["Expresstion"] = row["Expresstion"].ToString().Replace(dr["Code"].ToString(), "[" + dr["Code"].ToString() + "]");
                    //    }
                    //}

                    Expression ex = new Expression(row["Expresstion"].ToString());
                    foreach (DataRow dr in m_DataTable.Rows)
                    {
                        if (row["Expresstion"].ToString().Contains(dr["Code"].ToString()))
                        {
                            ex.Parameters.Add(dr["Code"].ToString(), double.Parse(dr["LastBalance"].ToString()));
                        }
                    }

                    row["LastBalance"] = double.Parse(ex.Evaluate().ToString());
                }
            }

            
            WebModule.Accounting.Report.B02_DN report = new WebModule.Accounting.Report.B02_DN();
            report.DataSource = m_DataTable;
            
            report.DataMember = "";

            B02dnReportViewer.Report = report;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Session = XpoHelper.GetNewSession();
            if (hB02dn.Contains("show"))
            {
                load_data();
            }
        }
    }
    
}