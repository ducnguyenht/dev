using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Web.ASPxGridView;
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

namespace WebModule
{
    public partial class TestSolution : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase, IETLJob
    {
        //Session session;
        string m_AccountRun = "111";
        char m_DebitOrCredit = 'C';

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_TEST_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }
        
        // Begin ETL

        private string fJobRegisterCode;
        public string JobRegisterCode
        {
            get { return "DiaryJournal"; }
        }

        private int fBusinessObjectTypeId;
        public List<int> BusinessObjectTypeId
        {
            get
            {
                return new List<int> { Constant.BusinessObjectType_Transaction,
                                            Constant.BusinessObjectType_FinancialTransaction,
                                            Constant.BusinessObjectType_PaymentVoucherTransaction,
                                            Constant.BusinessObjectType_ReceiptVoucherTransaction,
                                            Constant.BusinessObjectType_SalesFinancialTransaction,
                                            Constant.BusinessObjectType_PurcharseFinancialTransaction
                                            };
            }
        }

        Guid JobId;
        Session session;
        //public void Dispose()
        //{
        //    if (session != null)
        //    {
        //        session.Dispose();
        //        session = null;
        //    }
        //}

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

        //string Data = null;
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
        public void Extract()
        {            
            RefId = _ETLJobBO.ETLGetNextProcessObject(session, JobId, BusinessObjectTypeId);
            if (RefId == null || RefId == Guid.Empty) return;
            transaction = ExtractTransaction(session, RefId, m_AccountRun);          
        }

        public void Transform()
        {            
            if (RefId == Guid.Empty) return;
            _ETLLogBO.JobLog(session, JobId, "Running", "Status1");
            detailList = diaryJournalBO.TransformTransactionToTemplateArea(session, transaction, m_AccountRun);
        }

        public void Load()
        {
            if (RefId == Guid.Empty) return;
            {
                 diaryJournalBO.LoadTemplateAreaToDiaryJournalDetail(session, detailList, m_AccountRun, m_DebitOrCredit);
                _ETLLogBO.JobLog(session, JobId, "State1", "Status1");
                _ETLLogBO.SetETLBusinessObjectStatus(session, JobId, RefId, 1);
                _ETLEntryObjectHistoryBO.SetETLEntryObjectHistoryStatus(session, JobId, RefId, 1);


            }
        }

        public void WorkFlow()
        {
            for (int i = 1; i <= 100; i++)
            {
                Extract();
                Transform();
                Load();
                //if (RefId == Guid.Empty)
                //{
                //    return;
                //}
                //else
                //{
                //    RefId = Guid.Empty;
                //}
            }
        }

        public void Run()
        {
            ETLUtils etlUtil = new ETLUtils();
            //string FilePath = Application.StartupPath + "\\";
            //session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));     



            session = XpoHelper.GetNewSession();
            GetJobId(session);
            WorkFlow();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Run();
            session = XpoHelper.GetNewSession();
            //FinancialGeneralLedgerByMonth b = new FinancialGeneralLedgerByMonth(session);
            //b.DebitSum = 10;
            //b.Save();          
        }



        public ETL_Transaction ExtractTransaction(Session session, Guid TransactionId, string AccountCode)
        {
            ETL_Transaction resultTransaction = null;
            try
            {
                bool Acceptable = false;
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", AccountCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
                Account account = session.FindObject<Account>(criteria);
                /*2014/02/20 Duc.Vo INS START*/
                Organization defaultOrg = Organization.GetDefault(session, OrganizationEnum.NAAN_DEFAULT);
                Organization currentDeployOrg = Organization.GetDefault(session, OrganizationEnum.QUASAPHARCO);
                Account defaultAccount = Account.GetDefault(session, DefaultAccountEnum.NAAN_DEFAULT);
                /*2014/02/20 Duc.Vo INS END*/
                Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);

                //if (transaction == null)
                //{
                //    return resultTransaction;
                //}

                Util util = new Util();

                /*2014/02/20 Duc.Vo MOD START*/
                resultTransaction = new ETL_Transaction();
                if (currentDeployOrg != null)
                    resultTransaction.OwnerOrgId = currentDeployOrg.OrganizationId;
                else
                    resultTransaction.OwnerOrgId = defaultOrg.OrganizationId;

                if (transaction is SaleInvoiceTransaction)
                {
                    if ((transaction as SaleInvoiceTransaction).SalesInvoiceId.SourceOrganizationId != null)
                        resultTransaction.CustomerOrgId = (transaction as SaleInvoiceTransaction).SalesInvoiceId.SourceOrganizationId.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                }
                if (transaction is PurchaseInvoiceTransaction)
                {
                    if ((transaction as PurchaseInvoiceTransaction).PurchaseInvoiceId.SourceOrganizationId != null)
                        resultTransaction.SupplierOrgId = (transaction as PurchaseInvoiceTransaction).PurchaseInvoiceId.SourceOrganizationId.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;
                }
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
                /*2014/02/20 Duc.Vo MOD END*/
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
                    ETL_GeneralJournal tempJournal = new ETL_GeneralJournal();

                    if (journal.AccountId != null)
                        tempJournal.AccountId = journal.AccountId.AccountId;
                    else
                        tempJournal.AccountId = defaultAccount.AccountId;

                    tempJournal.CreateDate = journal.CreateDate;
                    tempJournal.Credit = journal.Credit;
                    if (journal.CurrencyId == null)
                    {
                        tempJournal.CurrencyId = CurrencyBO.DefaultCurrency(session).CurrencyId;
                    }
                    else
                    {
                        tempJournal.CurrencyId = journal.CurrencyId.CurrencyId;
                    }
                    tempJournal.Debit = journal.Debit;
                    tempJournal.Description = journal.Description;
                    tempJournal.GeneralJournalId = journal.GeneralJournalId;
                    tempJournal.JournalType = journal.JournalType;
                    resultTransaction.GeneralJournalList.Add(tempJournal);
                    if (IsRelateAccount(session, account.AccountId, tempJournal.AccountId))
                    {
                        Acceptable = true;
                    }
                }
                if (!Acceptable) return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultTransaction;
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
    }
}