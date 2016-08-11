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
using NAS.DAL.BI.Accounting;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;

namespace WebModule
{
    public partial class TestDiana : System.Web.UI.Page
    {
        UnitOfWork m_Uow = null;
        CriteriaOperator m_Filter = null;
        string m_BookingType;
        string m_Sql = "";

        MonthDim m_MonthDim = null;
        YearDim m_YearDim = null;
        NAS.DAL.BI.Accounting.Account.FinancialAccountDim m_FinancialAccountDim = null;
        CorrespondFinancialAccountDim m_CorrespondFinancialAccountDim = null;


        FinancialTransactionDim m_FinancialTransactionDim = null;
        DiaryJournal_Fact m_DiaryJournal_Fact = null;
        DiaryJournal_Detail m_DiaryJournal_Detail = null;

        private string fJobRegisterCode;

        public string JobRegisterCode
        {
            get { return "DiaryJournalJob"; }
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
                bool Acceptable = false;
                //CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);

                //CriteriaOperator criteria_Code = new BinaryOperator("Code", AccountCode, BinaryOperatorType.Equal);
                //CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
                //Account account = session.FindObject<Account>(criteria);
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
                        }
                        else
                        {
                            tempJournal.CurrencyId = journal.CurrencyId.CurrencyId;
                        }
                       
                        tempJournal.Description = journal.Description;
                        tempJournal.GeneralJournalId = journal.GeneralJournalId;
                        tempJournal.JournalType = journal.JournalType;
                        resultTransaction.GeneralJournalList.Add(tempJournal);
                    }
                    //if (IsRelateAccount(session, account.AccountId, tempJournal.AccountId))
                    //{
                    //    Acceptable = true;
                    //}
                }
                //if (!Acceptable) return null;
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



            //detailList = diaryJournalBO.TransformTransactionToTemplateArea(session, transaction);
        }




        public void Load()
        {
            if (RefId == Guid.Empty)
            {
                return;
            }

            m_Sql = "delete from DiaryJournal_Detail where FinancialTransactionDimId = (select FinancialTransactionDimId from FinancialTransactionDim where RefId = '" + RefId + "') " +                
                "delete from DiaryJournal_Fact where DiaryJournal_FactId not in (select DiaryJournal_FactId from DiaryJournal_Detail ) ";
            
            session.ExecuteNonQuery(m_Sql);


            //using (m_Uow = XpoHelper.GetNewUnitOfWork())
            //{
                // MonthDim
                m_Filter = new BinaryOperator("Name", transaction.IssuedDate.Month, BinaryOperatorType.Equal);
                m_MonthDim = session.FindObject<MonthDim>(m_Filter);

                if (m_MonthDim == null)
                {
                    m_MonthDim = new MonthDim(session);
                    m_MonthDim.Name = transaction.IssuedDate.Month.ToString();
                    m_MonthDim.Description = "Tháng " + transaction.IssuedDate.Month.ToString();
                    m_MonthDim.RowStatus = 1;

                    m_MonthDim.Save();
                }

                // YearDim

                m_Filter = new BinaryOperator("Name", transaction.IssuedDate.Year, BinaryOperatorType.Equal);
                m_YearDim = session.FindObject<YearDim>(m_Filter);

                if (m_YearDim == null)
                {
                    m_YearDim = new YearDim(session);
                    m_YearDim.Name = transaction.IssuedDate.Year.ToString();
                    m_YearDim.Description = "Năm " + transaction.IssuedDate.Year.ToString();
                    m_YearDim.RowStatus = 1;

                    m_YearDim.Save();
                }

                // FinancialTransactionDim

                m_Filter = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                m_FinancialTransactionDim = session.FindObject<FinancialTransactionDim>(m_Filter);
                if (m_FinancialTransactionDim == null)
                {
                    m_FinancialTransactionDim = new FinancialTransactionDim(session);
                    m_FinancialTransactionDim.Description = transaction.Description;
                    m_FinancialTransactionDim.Name = transaction.Code;
                    m_FinancialTransactionDim.RefId = transaction.TransactionId;
                    m_FinancialTransactionDim.IssueDate = transaction.IssuedDate;
                    m_FinancialTransactionDim.RowStatus = 1;

                    m_FinancialTransactionDim.Save();
                }

                if (transaction.GeneralJournalList.Count == 1)
                {
                    // Continue...
                }
                else
                {
                    //transaction.GeneralJournalList.Sort((x, y) => x.Debit.CompareTo(y.Credit));

                    transaction.GeneralJournalList = transaction.GeneralJournalList.OrderByDescending(x => x.Debit).ThenByDescending(x => x.Credit).ToList<ETL_GeneralJournal>();

                    if (transaction.GeneralJournalList.Count == 2)
                    {
                        m_BookingType = "1Debit_1Credit";
                    }
                    else if (transaction.GeneralJournalList[0].Debit > transaction.GeneralJournalList[transaction.GeneralJournalList.Count - 1].Credit)
                    {
                        m_BookingType = "1Debit_NCredit";
                    }
                    else if (transaction.GeneralJournalList[0].Debit < transaction.GeneralJournalList[transaction.GeneralJournalList.Count - 1].Credit)
                    {
                        m_BookingType = "NDebit_1Credit";
                    }

                    int _index = 0;

                    foreach (ETL_GeneralJournal item in transaction.GeneralJournalList)
                    {


                        // FinancialAccountDim
                        m_Filter = new BinaryOperator("Code", item.AccountCode, BinaryOperatorType.Equal);
                        m_FinancialAccountDim = session.FindObject<FinancialAccountDim>(m_Filter);
                        if (m_FinancialAccountDim == null)
                        {
                            m_FinancialAccountDim = new FinancialAccountDim(session);
                            m_FinancialAccountDim.Code = item.AccountCode;
                            m_FinancialAccountDim.Name = item.AccountName;
                            m_FinancialAccountDim.Description = item.AccountName;
                            m_FinancialAccountDim.RowStatus = 1;

                            m_FinancialAccountDim.Save();
                        }


                        // DiaryJournal_Fact
                        m_Filter = CriteriaOperator.And
                            (
                                new BinaryOperator("MonthDimId", m_MonthDim.MonthDimId, BinaryOperatorType.Equal),
                                new BinaryOperator("YearDimId", m_YearDim.YearDimId, BinaryOperatorType.Equal),
                                new BinaryOperator("FinancialAccountDimId", m_FinancialAccountDim, BinaryOperatorType.Equal)
                            );

                        m_DiaryJournal_Fact = session.FindObject<DiaryJournal_Fact>(m_Filter);
                        if (m_DiaryJournal_Fact == null)
                        {
                            m_DiaryJournal_Fact = new DiaryJournal_Fact(session);
                            m_DiaryJournal_Fact.BeginCreditBalance = 0;
                            m_DiaryJournal_Fact.BeginDebitBalance = 0;
                            m_DiaryJournal_Fact.CreditSum = 0;
                            m_DiaryJournal_Fact.DebitSum = 0;
                            m_DiaryJournal_Fact.EndCreditBalance = 0;
                            m_DiaryJournal_Fact.EndDebitBalance = 0;
                            m_DiaryJournal_Fact.FinancialAccountDimId = m_FinancialAccountDim;
                            m_DiaryJournal_Fact.MonthDimId = m_MonthDim;
                            m_DiaryJournal_Fact.YearDimId = m_YearDim;
                            m_DiaryJournal_Fact.OwnerOrgDimId = null;

                            m_DiaryJournal_Fact.Save();
                        }

                        // DiaryJournal_Detail


                        if (m_BookingType == "1Debit_1Credit")
                        {

                            m_Filter = CriteriaOperator.And
                                (
                                    new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                    new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                    new BinaryOperator("FinancialAccountDimId", m_FinancialAccountDim, BinaryOperatorType.Equal)
                                );

                            m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                            if (m_DiaryJournal_Detail == null)
                            {
                                m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                            }



                            m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                            m_DiaryJournal_Detail.Debit = (transaction.GeneralJournalList[_index].Debit > transaction.GeneralJournalList[_index].Credit ? transaction.GeneralJournalList[_index].Debit : 0);
                            m_DiaryJournal_Detail.Credit = (transaction.GeneralJournalList[_index].Debit < transaction.GeneralJournalList[_index].Credit ? transaction.GeneralJournalList[_index].Credit : 0);
                            m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = null;
                            m_DiaryJournal_Detail.CurrencyDimId = null;
                            m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                            m_DiaryJournal_Detail.FinancialAccountDimId = m_FinancialAccountDim;

                            m_DiaryJournal_Detail.Save();

                            // opposite

                            m_Filter = CriteriaOperator.And
                                (
                                    new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                    new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                    new BinaryOperator("CorrespondFinancialAccountDimId.Code", transaction.GeneralJournalList[(_index == 0 ? 1 : 0)].AccountCode, BinaryOperatorType.Equal)
                                );

                            m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                            if (m_DiaryJournal_Detail == null)
                            {
                                m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                            }


                            // CorrespondFinancialAccountDim  
                            m_Filter = new BinaryOperator("Code", transaction.GeneralJournalList[(_index == 0 ? 1 : 0)].AccountCode, BinaryOperatorType.Equal);
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


                            m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                            m_DiaryJournal_Detail.Debit = (transaction.GeneralJournalList[_index == 0 ? 1 : 0].Debit > transaction.GeneralJournalList[_index == 0 ? 1 : 0].Credit ? transaction.GeneralJournalList[_index == 0 ? 1 : 0].Debit : 0); ;
                            m_DiaryJournal_Detail.Credit = (transaction.GeneralJournalList[_index == 0 ? 1 : 0].Debit < transaction.GeneralJournalList[_index == 0 ? 1 : 0].Credit ? transaction.GeneralJournalList[_index == 0 ? 1 : 0].Credit : 0); ;
                            m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = m_CorrespondFinancialAccountDim;
                            m_DiaryJournal_Detail.CurrencyDimId = null;
                            m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                            m_DiaryJournal_Detail.FinancialAccountDimId = null;

                            m_DiaryJournal_Detail.Save();
                        }
                        else if (m_BookingType == "1Debit_NCredit")
                        {
                            if (_index == 0)
                            {
                                m_Filter = CriteriaOperator.And
                                 (
                                     new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                     new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                     new BinaryOperator("FinancialAccountDimId", m_FinancialAccountDim, BinaryOperatorType.Equal)
                                 );

                                m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                                if (m_DiaryJournal_Detail == null)
                                {
                                    m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                }



                                m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                                m_DiaryJournal_Detail.Debit = item.Debit;
                                m_DiaryJournal_Detail.Credit = 0;
                                m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = null;
                                m_DiaryJournal_Detail.CurrencyDimId = null;
                                m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                                m_DiaryJournal_Detail.FinancialAccountDimId = m_FinancialAccountDim;

                                m_DiaryJournal_Detail.Save();

                                // opposite
                                for (int i = 1; i < transaction.GeneralJournalList.Count; i++)
                                {
                                    m_Filter = CriteriaOperator.And
                                     (
                                         new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                         new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                         new BinaryOperator("CorrespondFinancialAccountDimId.Code", transaction.GeneralJournalList[i].AccountCode, BinaryOperatorType.Equal)
                                     );

                                    m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                                    if (m_DiaryJournal_Detail == null)
                                    {
                                        m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                    }

                                    // CorrespondFinancialAccountDim  
                                    m_Filter = new BinaryOperator("Code", transaction.GeneralJournalList[i].AccountCode, BinaryOperatorType.Equal);
                                    m_CorrespondFinancialAccountDim = session.FindObject<CorrespondFinancialAccountDim>(m_Filter);
                                    if (m_CorrespondFinancialAccountDim == null)
                                    {
                                        m_CorrespondFinancialAccountDim = new CorrespondFinancialAccountDim(session);
                                        m_CorrespondFinancialAccountDim.Code = transaction.GeneralJournalList[i].AccountCode;
                                        m_CorrespondFinancialAccountDim.Name = transaction.GeneralJournalList[i].AccountName;
                                        m_CorrespondFinancialAccountDim.Description = transaction.GeneralJournalList[i].AccountName;
                                        m_CorrespondFinancialAccountDim.RowStatus = 1;

                                        m_CorrespondFinancialAccountDim.Save();
                                    }


                                    m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                                    m_DiaryJournal_Detail.Debit = 0;
                                    m_DiaryJournal_Detail.Credit = transaction.GeneralJournalList[i].Credit;
                                    m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = m_CorrespondFinancialAccountDim;
                                    m_DiaryJournal_Detail.CurrencyDimId = null;
                                    m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                                    m_DiaryJournal_Detail.FinancialAccountDimId = null;

                                    m_DiaryJournal_Detail.Save();
                                }
                            }
                            else
                            {
                                m_Filter = CriteriaOperator.And
                               (
                                   new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                   new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                   new BinaryOperator("FinancialAccountDimId", m_FinancialAccountDim, BinaryOperatorType.Equal)
                               );

                                m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                                if (m_DiaryJournal_Detail == null)
                                {
                                    m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                }

                                m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                                m_DiaryJournal_Detail.Debit = 0;
                                m_DiaryJournal_Detail.Credit = item.Credit;
                                m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = null;
                                m_DiaryJournal_Detail.CurrencyDimId = null;
                                m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                                m_DiaryJournal_Detail.FinancialAccountDimId = m_FinancialAccountDim;

                                m_DiaryJournal_Detail.Save();

                                // opposite

                                m_Filter = CriteriaOperator.And
                                 (
                                     new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                     new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                     new BinaryOperator("CorrespondFinancialAccountDimId.Code", transaction.GeneralJournalList[0].AccountCode, BinaryOperatorType.Equal)
                                 );

                                m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);
                                if (m_DiaryJournal_Detail == null)
                                {
                                    m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                }

                                // CorrespondFinancialAccountDim  
                                m_Filter = new BinaryOperator("Code", transaction.GeneralJournalList[0].AccountCode, BinaryOperatorType.Equal);
                                m_CorrespondFinancialAccountDim = session.FindObject<CorrespondFinancialAccountDim>(m_Filter);
                                if (m_CorrespondFinancialAccountDim == null)
                                {
                                    m_CorrespondFinancialAccountDim = new CorrespondFinancialAccountDim(session);
                                    m_CorrespondFinancialAccountDim.Code = transaction.GeneralJournalList[0].AccountCode;
                                    m_CorrespondFinancialAccountDim.Name = transaction.GeneralJournalList[0].AccountName;
                                    m_CorrespondFinancialAccountDim.Description = item.AccountName;
                                    m_CorrespondFinancialAccountDim.RowStatus = 1;

                                    m_CorrespondFinancialAccountDim.Save();
                                }


                                m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                                m_DiaryJournal_Detail.Debit = item.Credit;
                                m_DiaryJournal_Detail.Credit = 0;
                                m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = m_CorrespondFinancialAccountDim;
                                m_DiaryJournal_Detail.CurrencyDimId = null;
                                m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                                m_DiaryJournal_Detail.FinancialAccountDimId = null;

                                m_DiaryJournal_Detail.Save();

                            }

                        }
                        else if (m_BookingType == "NDebit_1Credit")
                        {
                            if (_index == transaction.GeneralJournalList.Count - 1)
                            {
                                m_Filter = CriteriaOperator.And
                                 (
                                     new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                     new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                     new BinaryOperator("FinancialAccountDimId", m_FinancialAccountDim, BinaryOperatorType.Equal)
                                 );

                                m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                                if (m_DiaryJournal_Detail == null)
                                {
                                    m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                }

                                m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                                m_DiaryJournal_Detail.Debit = 0;
                                m_DiaryJournal_Detail.Credit = item.Credit;
                                m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = null;
                                m_DiaryJournal_Detail.CurrencyDimId = null;
                                m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                                m_DiaryJournal_Detail.FinancialAccountDimId = m_FinancialAccountDim;

                                m_DiaryJournal_Detail.Save();

                                // opposite
                                for (int i = 0; i < transaction.GeneralJournalList.Count - 1; i++)
                                {
                                    m_Filter = CriteriaOperator.And
                                     (
                                         new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                         new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                         new BinaryOperator("CorrespondFinancialAccountDimId.Code", transaction.GeneralJournalList[i].AccountCode, BinaryOperatorType.Equal)
                                     );

                                    m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                                    if (m_DiaryJournal_Detail == null)
                                    {
                                        m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                    }

                                    // CorrespondFinancialAccountDim  
                                    m_Filter = new BinaryOperator("Code", transaction.GeneralJournalList[i].AccountCode, BinaryOperatorType.Equal);
                                    m_CorrespondFinancialAccountDim = session.FindObject<CorrespondFinancialAccountDim>(m_Filter);
                                    if (m_CorrespondFinancialAccountDim == null)
                                    {
                                        m_CorrespondFinancialAccountDim = new CorrespondFinancialAccountDim(session);
                                        m_CorrespondFinancialAccountDim.Code = transaction.GeneralJournalList[i].AccountCode;
                                        m_CorrespondFinancialAccountDim.Name = transaction.GeneralJournalList[i].AccountName;
                                        m_CorrespondFinancialAccountDim.Description = transaction.GeneralJournalList[i].AccountName;
                                        m_CorrespondFinancialAccountDim.RowStatus = 1;

                                        m_CorrespondFinancialAccountDim.Save();
                                    }


                                    m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                                    m_DiaryJournal_Detail.Debit = transaction.GeneralJournalList[i].Debit;
                                    m_DiaryJournal_Detail.Credit = 0;
                                    m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = m_CorrespondFinancialAccountDim;
                                    m_DiaryJournal_Detail.CurrencyDimId = null;
                                    m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                                    m_DiaryJournal_Detail.FinancialAccountDimId = null;

                                    m_DiaryJournal_Detail.Save();
                                }
                            }
                            else
                            {
                                m_Filter = CriteriaOperator.And
                               (
                                   new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                   new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                   new BinaryOperator("FinancialAccountDimId", m_FinancialAccountDim, BinaryOperatorType.Equal)
                               );

                                m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                                if (m_DiaryJournal_Detail == null)
                                {
                                    m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                }

                                m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                                m_DiaryJournal_Detail.Debit = item.Debit;
                                m_DiaryJournal_Detail.Credit = 0;
                                m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = null;
                                m_DiaryJournal_Detail.CurrencyDimId = null;
                                m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                                m_DiaryJournal_Detail.FinancialAccountDimId = m_FinancialAccountDim;

                                m_DiaryJournal_Detail.Save();

                                // opposite

                                m_Filter = CriteriaOperator.And
                                 (
                                     new BinaryOperator("FinancialTransactionDimId", m_FinancialTransactionDim, BinaryOperatorType.Equal),
                                     new BinaryOperator("DiaryJournal_FactId", m_DiaryJournal_Fact, BinaryOperatorType.Equal),
                                     new BinaryOperator("CorrespondFinancialAccountDimId.Code", transaction.GeneralJournalList[0].AccountCode, BinaryOperatorType.Equal)
                                 );

                                m_DiaryJournal_Detail = session.FindObject<DiaryJournal_Detail>(m_Filter);

                                if (m_DiaryJournal_Detail == null)
                                {
                                    m_Filter = new BinaryOperator("Code", item.AccountCode, BinaryOperatorType.Equal);
                                }

                                // CorrespondFinancialAccountDim  
                                m_Filter = new BinaryOperator("Code", transaction.GeneralJournalList[transaction.GeneralJournalList.Count - 1].AccountCode, BinaryOperatorType.Equal);
                                m_CorrespondFinancialAccountDim = session.FindObject<CorrespondFinancialAccountDim>(m_Filter);
                                if (m_CorrespondFinancialAccountDim == null)
                                {
                                    m_CorrespondFinancialAccountDim = new CorrespondFinancialAccountDim(session);
                                    m_CorrespondFinancialAccountDim.Code = transaction.GeneralJournalList[transaction.GeneralJournalList.Count - 1].AccountCode;
                                    m_CorrespondFinancialAccountDim.Name = transaction.GeneralJournalList[transaction.GeneralJournalList.Count - 1].AccountName;
                                    m_CorrespondFinancialAccountDim.Description = item.AccountName;
                                    m_CorrespondFinancialAccountDim.RowStatus = 1;

                                    m_CorrespondFinancialAccountDim.Save();
                                }

                                m_DiaryJournal_Detail = new DiaryJournal_Detail(session);
                                m_DiaryJournal_Detail.FinancialTransactionDimId = m_FinancialTransactionDim;
                                m_DiaryJournal_Detail.Debit = 0;
                                m_DiaryJournal_Detail.Credit = item.Debit;
                                m_DiaryJournal_Detail.CorrespondFinancialAccountDimId = m_CorrespondFinancialAccountDim;
                                m_DiaryJournal_Detail.CurrencyDimId = null;
                                m_DiaryJournal_Detail.DiaryJournal_FactId = m_DiaryJournal_Fact;
                                m_DiaryJournal_Detail.FinancialAccountDimId = null;

                                m_DiaryJournal_Detail.Save();

                            }

                        }

                        // FinancialGeneralLedgerByYear_Fact, FinancialGeneralLedgerByMonth

                        m_Sql = "" +
"delete from FinancialGeneralLedgerByMonth " +
"where exists ( " +
    "select null from  " +
    "FinancialGeneralLedgerByYear_Fact aa, FinancialAccountDim bb " +
    "where aa.FinancialAccountDimId = bb.FinancialAccountDimId " +
    "and aa.FinancialGeneralLedgerByYear_FactId = FinancialGeneralLedgerByMonth.FinancialGeneralLedgerByYear_FactId " +
    "and bb.Code = '" + item.AccountCode + "'" +
") " +

"delete from FinancialGeneralLedgerByYear_Fact  " +
"where exists ( " +
    "select null from  " +
    "FinancialGeneralLedgerByYear_Fact aa, FinancialAccountDim bb " +
    "where aa.FinancialAccountDimId = bb.FinancialAccountDimId " +
    "and aa.FinancialGeneralLedgerByYear_FactId = FinancialGeneralLedgerByYear_Fact.FinancialGeneralLedgerByYear_FactId " +
    "and bb.Code = '" + item.AccountCode + "'" +
") " +

"insert into FinancialGeneralLedgerByYear_Fact  " +
"select 				 " +
"		case accttype.accounttype	 " +
"			when 'LIABILITY' THEN sum(balance.beginbalance) ELSE 0 end as BeginCreditBalance,				 " +
"		case accttype.accounttype " +
"			when 'ASSET' THEN sum(balance.beginbalance) ELSE 0 end as BeginDebitBalance, 				 " +
"		0 as EndCreditBalance,  " +
"		0 as EndDebitBalance,  " +
"		1 as RowStatus, " +
"		account.YearDimId, " +
"		account.FinancialAccountDimId, " +
"		NULL as OptimisticLockField, " +
"		NULL as GCRecord		 " +
"from " +
"	(select distinct a.FinancialAccountDimId, a.Code, d.YearDimId, year(c.IssueDate) as YearPeriod		 " +
"	from FinancialAccountDim a, DiaryJournal_Detail b,  " +
"		FinancialTransactionDim c, YearDim d " +
"	where a.FinancialAccountDimId = b.FinancialAccountDimId " +
"	and b.FinancialTransactionDimId = c.FinancialTransactionDimId " +
"	and cast(d.Name as int) = year(c.IssueDate) " +
"	and a.Code = '" + item.AccountCode + "') account " +
"	left join ( " +
"	select year(d.FromDateTime) as YearPeriod,  " +
"		case when b.Debit > b.Credit then b.Debit else b.Credit end as beginbalance 		 " +
"	from BalanceForwardTransaction a, GeneralJournal b, Account c,  " +
"		AccountingPeriod d, [Transaction] e " +
"	where a.TransactionId = b.TransactionId " +
"	and b.AccountId = c.AccountId " +
"	and a.TransactionId = e.TransactionId " +
"	and d.AccountingPeriodId = e.AccountingPeriodId " +
"	and b.Debit > 0 " +
"	and c.Code = '" + item.AccountCode + "' " +
"	and (month(d.FromDateTime) = 1 and year(d.FromDateTime) = 2014)) balance " +
"on account.YearPeriod = balance.YearPeriod " +
"	left join ( " +
"	select a.Code, b.Code as accounttype " +
"	from account a, AccountCategory b, AccountType c  " +
"	where a.AccountTypeId = c.AccountTypeId " +
"	and b.AccountCategoryId = c.AccountCategoryId) accttype " +
"on account.Code = accttype.Code " +
"group by account.FinancialAccountDimId, accttype.accounttype, account.YearDimId " +
" " +
"insert into FinancialGeneralLedgerByMonth " +
"select sum(b.Credit) as CreditSum, 0 as DebitSum, 1 as RowStatus, " +
"		h.FinancialGeneralLedgerByYear_FactId, f.MonthDimId,  " +
"		1 as CurrencyDimId,	 " +
"		e.CorrespondFinancialAccountDimId, " +
"		NULL as OptimisticLockField, " +
"		NULL as GCRecord	 " +
"from DiaryJournal_Fact a, " +
"		DiaryJournal_Detail b, " +
"		FinancialAccountDim c,  " +
"		FinancialTransactionDim d, " +
"		CorrespondFinancialAccountDim e, " +
"		MonthDim f, " +
"		YearDim g, " +
"		FinancialGeneralLedgerByYear_Fact h " +
"where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"and a.FinancialAccountDimId = h.FinancialAccountDimId " +
"and month(d.IssueDate) = f.Name " +
"and year(d.IssueDate) = g.Name " +
"and g.YearDimId = h.YearDimId " +
"and c.Code = '" + item.AccountCode + "'  " +
"and (b.Credit > 0) " +
"and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
"and (b.FinancialAccountDimId is null) " +
"and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
"group by h.FinancialGeneralLedgerByYear_FactId, f.MonthDimId, e.CorrespondFinancialAccountDimId " +
"union all " +
"select 0 as CreditSum, sum(b.Debit) as DebitSum, 1 as RowStatus, " +
"		h.FinancialGeneralLedgerByYear_FactId, f.MonthDimId,  " +
"		1 as CurrencyDimId,	 " +
"		e.CorrespondFinancialAccountDimId, " +
"		NULL as OptimisticLockField, " +
"		NULL as GCRecord	 " +
"from DiaryJournal_Fact a, " +
"		DiaryJournal_Detail b, " +
"		FinancialAccountDim c,  " +
"		FinancialTransactionDim d, " +
"		CorrespondFinancialAccountDim e, " +
"		MonthDim f, " +
"		YearDim g, " +
"		FinancialGeneralLedgerByYear_Fact h " +
"where a.DiaryJournal_FactId = b.DiaryJournal_FactId  " +
"and a.FinancialAccountDimId = c.FinancialAccountDimId  " +
"and b.FinancialTransactionDimId = d.FinancialTransactionDimId  " +
"and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId  " +
"and a.FinancialAccountDimId = h.FinancialAccountDimId " +
"and month(d.IssueDate) = f.Name " +
"and year(d.IssueDate) = g.Name " +
"and g.YearDimId = h.YearDimId " +
"and c.Code = '" + item.AccountCode + "'  " +
"and (b.Debit > 0) " +
"and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT') " +
"and (b.FinancialAccountDimId is null) " +
"and cast(e.Code as int) not in (1,2,3,4,5,6,7,8) " +
"group by h.FinancialGeneralLedgerByYear_FactId, f.MonthDimId, e.CorrespondFinancialAccountDimId ";


                        session.ExecuteNonQuery(m_Sql);

                        _index++;
                    }
                }







                // 

                //m_Uow.CommitChanges();
                //}            

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
            }
            //if (RefId == Guid.Empty)
            //{
            //    return;
            //}
            //else
            //{
            //    RefId = Guid.Empty;
            //}
            //}
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Run();
        }
    }
}