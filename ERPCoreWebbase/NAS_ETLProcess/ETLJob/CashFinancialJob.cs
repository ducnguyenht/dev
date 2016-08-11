using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;
using System.Data;
using NAS.DAL;
using Utility.ETL;
using DevExpress.Xpo;
using NAS.ETLBO.System.ETL;
using NAS.ETLBO.System.Object;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.BO.ETL.Accounting;
using NAS.BO.ETL.Inventory;
using DevExpress.Data.Filtering;
using NAS.BO.ETL.Accounting.TempData;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.ETL;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Accounting.Journal;
using NAS.BO.Accounting;
using NAS.DAL.BI.Accounting.GeneralLedger;
using NAS.DAL.Report;
using NAS.DAL.BI.Accounting;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.Vouches;
using NAS.DAL.BI.Accounting.Cash;
using NAS.DAL.Accounting.Currency;
using System.Windows.Forms;


namespace NAS_ETLProcess.ETLJob
{
    public class CashFinancialJob : IETLJob
    {
        UnitOfWork m_Uow = null;
        CriteriaOperator m_Filter = null;
        string m_BookingType;
        string m_Sql = "";

        Currency m_Currency = null;

        MonthDim m_MonthDim = null;
        YearDim m_YearDim = null;
        NAS.DAL.BI.Accounting.Account.FinancialAccountDim m_FinancialAccountDim = null;
        CorrespondFinancialAccountDim m_CorrespondFinancialAccountDim = null;


        FinancialVoucherDim m_FinancialVoucherDim = null;
        FinancialCash_Fact m_FinancialCash_Fact = null;
        FinancialCashTypeDim m_FinancialCashTypeDim = null;
        PaymentVouchesTransaction m_PaymentVouchesTransaction = null;
        ReceiptVouchesTransaction m_ReceiptVouchesTransaction = null;

        private string fJobRegisterCode;

        public string JobRegisterCode
        {
            get { return "CashFinancialJob8"; }
        }

        private int fBusinessObjectTypeId;
        public List<int> BusinessObjectTypeId
        {
            get
            {
                return new List<int> { //Constant.BusinessObjectType_Transaction,
                                            Constant.BusinessObjectType_FinancialTransaction,
                                            Constant.BusinessObjectType_PaymentVoucherTransaction,
                                            Constant.BusinessObjectType_ReceiptVoucherTransaction//,
                                            //Constant.BusinessObjectType_SalesFinancialTransaction,
                                            //Constant.BusinessObjectType_PurcharseFinancialTransaction
                                            };
            }
        }

        Guid JobId;
        Session session;

        #region InitBO
        ETLEntryObjectHistoryBO _ETLEntryObjectHistoryBO = new ETLEntryObjectHistoryBO();
        ObjectEntryLogBO _ObjectEntryLogBO = new ObjectEntryLogBO();
        ETLJobBO _ETLJobBO = new ETLJobBO();
        ETLLogBO _ETLLogBO = new ETLLogBO();
        BusinessObjectBO _BusinessObjectBO = new BusinessObjectBO();
        DiaryJournalBO diaryJournalBO = new DiaryJournalBO();
        ETLAccountingBO accountingBO = new ETLAccountingBO();
        ETLInventoryBO etlInventoryBO = new ETLInventoryBO();

        #endregion

        Guid RefId = Guid.Empty;
        ETL_Transaction transaction;
        List<DiaryJournalTemplate> detailList = new List<DiaryJournalTemplate>();
        private bool fStop;
        public bool Stop
        {
            get { return fStop; }
            set { fStop = value; }
        }


        public void GetJobId(Session session)
        {
            try
            {
                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_1 = new BinaryOperator("Code", JobRegisterCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("Code", "Accounting", BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                CriteriaOperator criteria_cat = new GroupOperator(GroupOperatorType.And, criteria_2);
                NAS.DAL.ETL.ETLJob _ETLJob = session.FindObject<NAS.DAL.ETL.ETLJob>(criteria);
                if (_ETLJob == null)
                {
                    _ETLJob = new NAS.DAL.ETL.ETLJob(session);
                    _ETLJob.Code = JobRegisterCode;
                    _ETLJob.Description = "";
                    _ETLJob.ETLCategory = session.FindObject<ETLCategory>(criteria_cat);
                    _ETLJob.Is24x7 = true;
                    _ETLJob.Priority = 1;
                    _ETLJob.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    _ETLJob.Save();
                }
                JobId = _ETLJob.ETLJobId;
            }
            catch (Exception ex)
            {
                ETLUtils etlUtil = new ETLUtils();
                etlUtil.logs("d:/logs/Process_history.txt", DateTime.Now.ToString() + " : FinancialCustomerLiability GetJobId:" + ex.Message);
                return;
            }
        }

        public bool IsRelateAccount(Session session, Guid ParentAccountID, Guid ChildAccountID)
        {
            bool result = false;
            try
            {
                if (ParentAccountID == ChildAccountID) return true;
                Account parentAccount = session.GetObjectByKey<Account>(ParentAccountID);
                Account childAccount = session.GetObjectByKey<Account>(ChildAccountID);

                if (parentAccount == null || childAccount == null) return false;
                while (childAccount.ParentAccountId != null)
                {
                    if (parentAccount == childAccount.ParentAccountId) return true;
                    childAccount = childAccount.ParentAccountId;
                }
                return false;
            }
            catch (Exception)
            {
            }
            return result;
        }

        public ETL_Transaction ExtractTransaction(Session session, Guid TransactionId)
        {
            ETL_Transaction resultTransaction = null;
            try
            {           
                Organization defaultOrg = Organization.GetDefault(session, OrganizationEnum.NAAN_DEFAULT);
                Organization currentDeployOrg = Organization.GetDefault(session, OrganizationEnum.QUASAPHARCO);
                Account defaultAccount = Account.GetDefault(session, DefaultAccountEnum.NAAN_DEFAULT);
           
                Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);           

                Util util = new Util();
                
                resultTransaction = new ETL_Transaction();
                if (currentDeployOrg != null)
                    resultTransaction.OwnerOrgId = currentDeployOrg.OrganizationId;
                else
                    resultTransaction.OwnerOrgId = defaultOrg.OrganizationId;
              
                if (transaction is PaymentVouchesTransaction)
                {
                    PaymentVoucherTransactionBO paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();

                    Organization SuppOrg = paymentVoucherTransactionBO.GetAllocatedSupplier(session, transaction.TransactionId);
                    Organization CustOrg = paymentVoucherTransactionBO.GetAllocatedCustomer(session, transaction.TransactionId);

                    if (SuppOrg != null)
                        resultTransaction.SupplierOrgId = SuppOrg.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                    if (CustOrg != null)
                        resultTransaction.CustomerOrgId = CustOrg.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                }
                if (transaction is ReceiptVouchesTransaction)
                {
                    ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
                    Organization SuppOrg = receiptVoucherTransactionBO.GetAllocatedSupplier(session, transaction.TransactionId);
                    Organization CustOrg = receiptVoucherTransactionBO.GetAllocatedCustomer(session, transaction.TransactionId);

                    if (SuppOrg != null)
                        resultTransaction.SupplierOrgId = SuppOrg.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                    if (CustOrg != null)
                        resultTransaction.CustomerOrgId = CustOrg.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                }
          
                resultTransaction.TransactionId = transaction.TransactionId;
                resultTransaction.Amount = transaction.Amount;
                resultTransaction.Code = transaction.Code;
                resultTransaction.CreateDate = transaction.CreateDate;
                resultTransaction.Description = transaction.Description;
                resultTransaction.IsBalanceForward = (transaction is BalanceForwardTransaction);
                resultTransaction.IssuedDate = transaction.IssueDate;
                resultTransaction.UpdateDate = transaction.UpdateDate;                
                resultTransaction.GeneralJournalList = new List<ETL_GeneralJournal>();
                foreach (GeneralJournal journal in transaction.GeneralJournals)
                {
                    if (journal.RowStatus >= 1)
                    {
                        ETL_GeneralJournal tempJournal = new ETL_GeneralJournal();

                        if (journal.AccountId != null)
                        {
                            tempJournal.AccountId = journal.AccountId.AccountId;
                            tempJournal.AccountCode = journal.AccountId.Code;
                            tempJournal.AccountName = journal.AccountId.Name;
                        }
                        else
                        {
                            tempJournal.AccountId = defaultAccount.AccountId;
                            tempJournal.AccountCode = defaultAccount.Code;
                            tempJournal.AccountName = defaultAccount.Name;
                        }

                        tempJournal.CreateDate = journal.CreateDate;
                        tempJournal.Debit = journal.Debit;
                        tempJournal.Credit = journal.Credit;
                        if (journal.CurrencyId == null)
                        {
                            tempJournal.CurrencyId = CurrencyBO.DefaultCurrency(session).CurrencyId;
                            tempJournal.CurrencyCode = CurrencyBO.DefaultCurrency(session).Code;
                            tempJournal.CurrencyName = CurrencyBO.DefaultCurrency(session).Name;
                        }
                        else
                        {
                            tempJournal.CurrencyId = journal.CurrencyId.CurrencyId;
                            tempJournal.CurrencyCode = journal.CurrencyId.Code;
                            tempJournal.CurrencyName = journal.CurrencyId.Name;
                        }
                       
                        tempJournal.Description = journal.Description;
                        tempJournal.GeneralJournalId = journal.GeneralJournalId;
                        tempJournal.JournalType = journal.JournalType;
                        resultTransaction.GeneralJournalList.Add(tempJournal);
                    }  
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultTransaction;
        }

        public void Extract()
        {
            RefId = _ETLJobBO.ETLGetNextProcessObject(session, JobId, BusinessObjectTypeId);
            if (RefId == null || RefId == Guid.Empty) return;
            transaction = ExtractTransaction(session, RefId);
        }

        public void Transform()
        {
            if (RefId == Guid.Empty)
            {
                return;
            }
            _ETLLogBO.JobLog(session, JobId, "Running", "Status1");            
        }

        public void Load()
        {
         
                    
            // MonthDim

            //m_Filter = new BinaryOperator("Name", transaction.IssuedDate.Month, BinaryOperatorType.Equal);
            //m_MonthDim = session.FindObject<MonthDim>(m_Filter);

            //if (m_MonthDim == null)
            //{
            //    m_MonthDim = new MonthDim(session);
            //    m_MonthDim.Name = transaction.IssuedDate.Month.ToString();
            //    m_MonthDim.Description = "Tháng " + transaction.IssuedDate.Month.ToString();
            //    m_MonthDim.RowStatus = 1;

            //    m_MonthDim.Save();
            //}

            //// YearDim

            //m_Filter = new BinaryOperator("Name", transaction.IssuedDate.Year, BinaryOperatorType.Equal);
            //m_YearDim = session.FindObject<YearDim>(m_Filter);

            //if (m_YearDim == null)
            //{
            //    m_YearDim = new YearDim(session);
            //    m_YearDim.Name = transaction.IssuedDate.Year.ToString();
            //    m_YearDim.Description = "Năm " + transaction.IssuedDate.Year.ToString();
            //    m_YearDim.RowStatus = 1;

            //    m_YearDim.Save();
            //}


            if (RefId != Guid.Empty)
            {

                // FinancialVoucherDim
                m_Filter = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                m_FinancialVoucherDim = session.FindObject<FinancialVoucherDim>(m_Filter);
                if (m_FinancialVoucherDim == null)
                {
                    m_FinancialVoucherDim = new FinancialVoucherDim(session);
                    m_FinancialVoucherDim.BookingDate = transaction.IssuedDate;
                    m_FinancialVoucherDim.IssueDate = transaction.CreateDate;
                    m_FinancialVoucherDim.Name = transaction.Code;
                    m_FinancialVoucherDim.RefId = RefId;
                    m_FinancialVoucherDim.Description = transaction.Description;
                }
                m_FinancialVoucherDim.Save();

                // FinancialCash_Fact
                foreach (ETL_GeneralJournal item in transaction.GeneralJournalList)
                {
                    // FinancialCashTypeDim
                    m_Currency = session.GetObjectByKey<Currency>(item.CurrencyId);

                    m_Filter = new BinaryOperator("Name", item.CurrencyCode, BinaryOperatorType.Equal);
                    m_FinancialCashTypeDim = session.FindObject<FinancialCashTypeDim>(m_Filter);
                    if (m_FinancialCashTypeDim == null)
                    {
                        m_FinancialCashTypeDim = new FinancialCashTypeDim(session);
                    }
                    m_FinancialCashTypeDim.Name = item.CurrencyCode;
                    m_FinancialCashTypeDim.Description = item.CurrencyName;

                    m_FinancialCashTypeDim.Save();

                    // FinancialCash_Fact
                    m_FinancialCash_Fact = session.FindObject<FinancialCash_Fact>(m_FinancialVoucherDim.FinancialVoucherDimId);
                    if (m_FinancialCash_Fact == null)
                    {
                        m_FinancialCash_Fact = new FinancialCash_Fact(session);
                    }
                    m_FinancialCash_Fact.FinancialVoucherDimId = m_FinancialVoucherDim;
                    m_FinancialCash_Fact.FinancialCashTypeDimId = m_FinancialCashTypeDim;

                    if (item.AccountCode.Contains("111") || item.AccountCode.Contains("112"))
                    {
                        m_Filter = new BinaryOperator("Code", item.AccountCode, BinaryOperatorType.Equal);
                        m_FinancialAccountDim = session.FindObject<FinancialAccountDim>(m_Filter);

                        if (m_FinancialAccountDim == null)
                        {
                            m_FinancialAccountDim = new FinancialAccountDim(session);
                        }
                        m_FinancialAccountDim.Code = item.AccountCode;
                        m_FinancialAccountDim.Name = item.AccountName;
                        m_FinancialAccountDim.Description = item.AccountName;
                        m_FinancialAccountDim.RowStatus = 1;

                        m_FinancialAccountDim.Save();
                    }
                    else
                    {
                        m_Filter = new BinaryOperator("Code", item.AccountCode, BinaryOperatorType.Equal);
                        m_CorrespondFinancialAccountDim = session.FindObject<CorrespondFinancialAccountDim>(m_Filter);
                        if (m_CorrespondFinancialAccountDim == null)
                        {
                            m_CorrespondFinancialAccountDim = new CorrespondFinancialAccountDim(session);
                            m_CorrespondFinancialAccountDim.Code = item.AccountCode;
                            m_CorrespondFinancialAccountDim.Name = item.AccountName;
                            m_CorrespondFinancialAccountDim.Description = item.AccountName;
                            m_CorrespondFinancialAccountDim.RowStatus = 1;

                            m_CorrespondFinancialAccountDim.Save();
                        }
                    }

                    if (item.Credit > 0 && (item.AccountCode.Contains("111") || item.AccountCode.Contains("112")))
                    {
                        m_FinancialCash_Fact.FinancialAccountDimId = m_FinancialAccountDim;
                        m_FinancialCash_Fact.Credit = item.Credit;
                    }
                    else if (item.Debit > 0 && (item.AccountCode.Contains("111") || item.AccountCode.Contains("112")))
                    {
                        m_FinancialCash_Fact.FinancialAccountDimId = m_FinancialAccountDim;
                        m_FinancialCash_Fact.Debit = item.Debit;
                    }
                    else
                    {
                        if (m_CorrespondFinancialAccountDim.Code != "NAAN_DEFAULT")
                        {
                            if (item.Credit > 0)
                            {
                                m_FinancialCash_Fact.CorrespondFinancialAccountDimId = m_CorrespondFinancialAccountDim;
                                m_FinancialCash_Fact.Credit = item.Credit;
                            }
                            else
                            {
                                m_FinancialCash_Fact.CorrespondFinancialAccountDimId = m_CorrespondFinancialAccountDim;
                                m_FinancialCash_Fact.Debit = item.Debit;
                            }
                        }
                    }

                    m_FinancialCash_Fact.Save();

                }
            }



            m_Filter = CriteriaOperator.And(
                CriteriaOperator.Or(
                    new BinaryOperator("SumOfDebit", 0, BinaryOperatorType.Greater),
                    new BinaryOperator("SumOfCredit", 0, BinaryOperatorType.Greater)
                ),
                new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)
            );
            XPCollection<Vouches> vouches = new XPCollection<Vouches>(session, m_Filter);

            m_Sql = "" +
            "delete from FinancialCash_Fact " +
            "	where exists (select null from FinancialVoucherDim aa, Vouches bb  " +
            "					where bb.RowStatus = 4  " +
            "                   and aa.RefId = bb.VouchesId " +
            "					and aa.FinancialVoucherDimId = FinancialCash_Fact.FinancialVoucherDimId) " +
            " delete from FinancialVoucherDim " +
            "    where exists (select null from Vouches bb  " +
            "					where bb.RowStatus = 4  " +
            "                   and bb.VouchesId = FinancialVoucherDim.Refid) ";            

            session.ExecuteNonQuery(m_Sql);

            foreach (Vouches item in vouches)
            {
                //if (item is PaymentVouches)
                //{
                //    m_Filter = new BinaryOperator("PaymentVouchesId", item.VouchesId, BinaryOperatorType.Equal);
                //    m_PaymentVouchesTransaction = session.FindObject<PaymentVouchesTransaction>(m_Filter);
                //    if (m_PaymentVouchesTransaction != null)
                //    {
                //        m_Sql = "" +
                //        "delete from FinancialCash_Fact " +
                //        "	where exists (select null from FinancialVoucherDim bb  " +
                //        "					where bb.BookingDate is null  " +
                //        "					and bb.FinancialVoucherDimId = FinancialCash_Fact.FinancialVoucherDimId " +
                //        "                   and bb.RefId = '" + m_PaymentVouchesTransaction.TransactionId + "') " +
                //        " delete from FinancialVoucherDim where RefId = '" + m_PaymentVouchesTransaction.TransactionId + "'";

                //        session.ExecuteNonQuery(m_Sql);
                //        continue;
                //    }
                //}
                //else
                //{
                //    m_Filter = new BinaryOperator("ReceiptVouchesId", item.VouchesId, BinaryOperatorType.Equal);
                //    m_ReceiptVouchesTransaction = session.FindObject<ReceiptVouchesTransaction>(m_Filter);
                //    if (m_ReceiptVouchesTransaction != null)
                //    {
                //        m_Sql = "" +
                //     "delete from FinancialCash_Fact " +
                //     "	where exists (select null from FinancialVoucherDim bb  " +
                //     "					where bb.BookingDate is null  " +
                //     "					and bb.FinancialVoucherDimId = FinancialCash_Fact.FinancialVoucherDimId " +
                //     "                  and bb.RefId = '" + m_ReceiptVouchesTransaction.TransactionId + "') " +
                //     " delete from FinancialVoucherDim where RefId = '" + m_ReceiptVouchesTransaction.TransactionId + "'";

                //        continue;
                //    }
                //}

                // FinancialVoucherDim                
                m_Filter = new BinaryOperator("RefId", item.VouchesId, BinaryOperatorType.Equal);
                m_FinancialVoucherDim = session.FindObject<FinancialVoucherDim>(m_Filter);
                if (m_FinancialVoucherDim == null)
                {
                    m_FinancialVoucherDim = new FinancialVoucherDim(session);
                    m_FinancialVoucherDim.IssueDate = item.IssuedDate;
                    m_FinancialVoucherDim.Name = item.Code;
                    m_FinancialVoucherDim.RefId = item.VouchesId;
                    m_FinancialVoucherDim.Description = item.Description;
                    m_FinancialVoucherDim.Save();
                }

                //FinancialCashTypeDim
                m_Filter = new BinaryOperator("Name", item.VouchesAmounts[0].CurrencyId.Code, BinaryOperatorType.Equal);
                m_FinancialCashTypeDim = session.FindObject<FinancialCashTypeDim>(m_Filter);
                if (m_FinancialCashTypeDim == null)
                {
                    m_FinancialCashTypeDim = new FinancialCashTypeDim(session);
                    m_FinancialCashTypeDim.Name = item.VouchesAmounts[0].CurrencyId.Code;
                    m_FinancialCashTypeDim.Description = item.VouchesAmounts[0].CurrencyId.Name;

                    m_FinancialCashTypeDim.Save();
                }
               
                // FinancialCash_Fact
                m_Filter = new BinaryOperator("FinancialVoucherDimId", m_FinancialVoucherDim.FinancialVoucherDimId, BinaryOperatorType.Equal);
                m_FinancialCash_Fact = session.FindObject<FinancialCash_Fact>(m_Filter);
                if (m_FinancialCash_Fact == null)
                {
                    m_FinancialCash_Fact = new FinancialCash_Fact(session);
                    m_FinancialCash_Fact.FinancialVoucherDimId = m_FinancialVoucherDim;
                    m_FinancialCash_Fact.FinancialCashTypeDimId = m_FinancialCashTypeDim;

                    if (item is PaymentVouches)
                    {
                        m_FinancialCash_Fact.Credit = item.SumOfCredit;
                    }
                    else if (item is ReceiptVouches)
                    {
                        m_FinancialCash_Fact.Debit = item.SumOfDebit;
                    }

                    m_FinancialCash_Fact.Save();
                }
                
            }

            _ETLLogBO.JobLog(session, JobId, "State1", "Status1");
            _ETLLogBO.SetETLBusinessObjectStatus(session, JobId, RefId, 1);
            _ETLEntryObjectHistoryBO.SetETLEntryObjectHistoryStatus(session, JobId, RefId, 1);
        }

        public void WorkFlow()
        {
            for (int i = 1; i <= 100; i++)
            {
                Extract();
                Transform();
                Load();

                
                if (RefId == Guid.Empty)
                {
                    return;
                }
                else
                {
                    RefId = Guid.Empty;
                }

            }              

        }

        public void Run()
        {
            ETLUtils etlUtil = new ETLUtils();
            string FilePath = Application.StartupPath + "\\";
            session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));             

            GetJobId(session);
            WorkFlow();
        }

    }
}
