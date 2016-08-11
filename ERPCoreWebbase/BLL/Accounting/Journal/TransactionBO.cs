using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Accounting.AccountChart;
using System.Collections;
using NAS.DAL;
using Utility;
using DevExpress.Data.Filtering;

namespace NAS.BO.Accounting.Journal
{
    public class TransactionBO
    {
        //public generalJournal[] _generalJournal;
        //private ArrayList _generalJournal;
        //public ArrayList GeneralJournal
        //{
        //    get { return _generalJournal; }
        //    set { GeneralJournal = value; }

        //}
        //override public int Create()
        //{
        //    return 0;
        //}
        // Lop o tren 
        //public class PurchaseInvoiceTransactionBO : TransactionBO
        //{
        // override public int Create(a,b,c)
        //{
        // Nap du lieu 
        // GeneralJournal gj1 = new GeneralJournal();
        // GeneralJournal gj2 = new GeneralJournal();
        // GeneralJournal gj = new GeneralJournal();
        // GeneralJournal.Add(gj1);
        //GeneralJournal.Add(gj2);
        //...
        // base.Create();
        //}
        //}
        //

        public class GeneralJournalEntity // GeneralJournal
        {
            [Size(36)]
            public string AccountCode;
            [Size(1024)]
            public string Description;
            [Size(36)]
            public string GeneralJournalCode;
            public double Credit;
            public double Debit;
        }

        //public static byte CreateTransaction(Session session
        //                    , string _AccountingPeriodCode
        //                    , DateTime _IssueDate                            
        //                    , string _Description
        //                    , Guid _OriginalArtifactKey
        //                    , GeneralJournalEntity[] _generaljournal
        //                    , Transaction _Transaction
        //                    ) 
        //    //where TTransaction : Transaction, new()
        //    //where TOriginalArtifact : NAS.DAL.Invoice.Bill
        //{
        //    //try
        //    //{                
        //    //    XPQuery<AccountingPeriod> AccountingPeriodquery = session.Query<AccountingPeriod>();
        //    //    AccountingPeriod ActPer = AccountingPeriodquery.Where(r => r.Code == _AccountingPeriodCode).FirstOrDefault();
        //    //    Transaction transaction = new Transaction(session)
        //    //    {
        //    //           AccountingPeriodId = ActPer, 
        //    //           IssueDate = _IssueDate,
        //    //           CreateDate = DateTime.Now,
        //    //           Description = _description,
        //    //    };
        //    //    transaction.Save();
        //    XPQuery<Account> Accountquery = session.Query<Account>();
        //    foreach (generalJournal gj in _generaljournal)
        //    {
        //        Account acount = Accountquery.Where(r => r.Code == gj.AccountCode).FirstOrDefault();
        //        GeneralJournal generalJournal = new GeneralJournal(session)
        //        {
        //            AccountId = acount,
        //            TransactionId = _Transaction,
        //            Credit = gj.Credit,
        //            Debit = gj.Debit,
        //            RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
        //        };
        //        generalJournal.Save();
        //        General.BalanceUpdate(session, acount, generalJournal, false, (gj.Debit - gj.Credit) * acount.BalanceType);
        //    }
        //    //}
        //    //catch(Exception)
        //    //{
        //    //    return 1;
        //    //    throw;
        //    //}
        //    //finally
        //    //{
        //    //    if (session != null) session.Dispose();
        //    //}

        //    return 0;
        //}


        public static void ProcessApproveCosting<T>(Session session, Guid originalArtifactId)
        {
            UnitOfWork uow = null;
            AccountingBO accountingBO = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();

                accountingBO = new AccountingBO();

                List<Transaction> transactionList = null;
                General generalBO = new General();
                if (typeof(T).Equals(typeof(DAL.Vouches.ReceiptVouches)))
                {
                    //Get all transaction of ReceiptVouches    
                    XPQuery<ReceiptVouchesTransaction> receiptVouchesTransactionQuery =
                        uow.Query<ReceiptVouchesTransaction>();
                    transactionList = receiptVouchesTransactionQuery.Where
                        (r => r.ReceiptVouchesId.VouchesId == originalArtifactId).ToList<Transaction>();
                }
                else if (typeof(T).Equals(typeof(DAL.Vouches.PaymentVouches)))
                {
                    //Get all transaction of PaymentVouches
                    XPQuery<PaymentVouchesTransaction> receiptVouchesTransactionQuery =
                        uow.Query<PaymentVouchesTransaction>();
                    transactionList = receiptVouchesTransactionQuery.Where
                        (r => r.PaymentVouchesId.VouchesId == originalArtifactId).ToList<Transaction>();
                }
                if (transactionList != null)
                {

                    if (transactionList.Count == 0)
                    {
                        throw new Exception("Xử lý duyệt thất bại vì không có dòng bút toán nào");
                    }

                    //Check approved
                    //int approveTransactionCount = transactionList.Count(r => r.RowStatus == Constant.ROWSTATUS_ACTIVE);
                    //if (approveTransactionCount > 0)
                    //{
                    //    throw new Exception("Chứng từ này đã được duyệt hạch toán");
                    //}

                    foreach (Transaction transaction in transactionList)
                    {

                        short isAvailable = accountingBO.IsAvailableApprove(session, transaction.TransactionId);
                        /// -1: OK. Balance forward. 
                        /// 0: OK. You can approve it. 
                        /// (order: Can't approve) 
                        /// 1: Credit != Debit. 
                        /// 2: Approved. 
                        /// 3: Credit = Debit = 0. 
                        /// 4: Credit &lt; 0. 
                        /// 5: Debit  &lt; 0. 
                        /// 6: Don't have any journal 
                        /// 7: Transaction not exist 
                        /// 8: Credit and Debit &lt; 0 
                        switch (isAvailable)
                        {
                            case 1:
                                throw new Exception(String.Format("Dư Nợ và dư Có không bằng nhau trong bút toán '{0}'", 
                                    transaction.Code));
                            case 2:
                                throw new Exception(String.Format("Chứng từ này đã được duyệt hạch toán",
                                    transaction.Code));
                            case 3:
                                throw new Exception(String.Format("Dư Nợ và dư Có bằng 0 trong bút toán '{0}'", 
                                    transaction.Code));
                            case 4:
                                throw new Exception(String.Format("Dư Nợ nhỏ hơn 0 trong bút toán '{0}'",
                                    transaction.Code));
                            case 5:
                                throw new Exception(String.Format("Dư Có nhỏ hơn 0 trong bút toán '{0}'",
                                    transaction.Code));
                            case 6:
                                throw new Exception(String.Format("Bút toán '{0}' chưa được định khoản",
                                    transaction.Code));
                            case 7:
                                throw new Exception("Bút toán không hợp lệ");
                            case 8:
                                throw new Exception(String.Format("Dư Nợ và dư Có nhỏ hơn 0 trong bút toán '{0}'", 
                                    transaction.Code));
                            default:
                                break;
                        }

                        ////Check approved
                        //if (AccountingBO.IsApproved(session, transaction.TransactionId))
                        //{
                        //    throw new Exception("Chứng từ này đã được duyệt hạch toán");
                        //}

                        //Validate credit and debit
                        if (transaction.RowStatus > 0)
                        {
                            //double credit = transaction.GeneralJournals.Sum(r => r.Credit);
                            //double debit = transaction.GeneralJournals.Sum(r => r.Debit);
                            //if (credit == debit && debit == 0)
                            //{
                            //    throw new Exception(String.Format("Dư Nợ và dư Có bằng 0 trong bút toán {0}", transaction.Code));
                            //}
                            //Update balance
                            double milsec = 0;
                            foreach (GeneralJournal generalJournal in transaction.GeneralJournals)
                            {
                                accountingBO.CreateGeneralLedger(session, generalJournal.TransactionId.TransactionId, generalJournal.GeneralJournalId, milsec);
                                milsec += 10;
                            }
                            //Change status of transaction to active                            

                        }
                    }
                }
                else
                {
                    throw new Exception("Xử lý duyệt thất bại vì không có dòng bút toán nào");
                }
                uow.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }

        public static bool isApprovedCosting<T>(Session session, Guid originalArtifactId)
        {
            //Session session = null;            
            try
            {
                session = XpoHelper.GetNewSession();
                List<Transaction> transactionList = null;
                General generalBO = new General();
                if (typeof(T).Equals(typeof(DAL.Vouches.ReceiptVouches)))
                {
                    //Get all transaction of ReceiptVouches    
                    XPQuery<ReceiptVouchesTransaction> receiptVouchesTransactionQuery =
                        session.Query<ReceiptVouchesTransaction>();
                    transactionList = receiptVouchesTransactionQuery.Where
                        (r => r.ReceiptVouchesId.VouchesId == originalArtifactId).ToList<Transaction>();
                }
                else if (typeof(T).Equals(typeof(DAL.Vouches.PaymentVouches)))
                {
                    //Get all transaction of PaymentVouches
                    XPQuery<PaymentVouchesTransaction> receiptVouchesTransactionQuery =
                        session.Query<PaymentVouchesTransaction>();
                    transactionList = receiptVouchesTransactionQuery.Where
                        (r => r.PaymentVouchesId.VouchesId == originalArtifactId).ToList<Transaction>();
                }
                if (transactionList != null)
                {
                    if (transactionList.Count > 0)
                    {
                        //Check approved
                        foreach (Transaction transaction in transactionList)
                        {
                            if (AccountingBO.IsApproved(session, transaction.TransactionId) == false) return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    //int approveTransactionCount = transactionList.Count(r => r.RowStatus == Constant.ROWSTATUS_ACTIVE);
                    //if (approveTransactionCount > 0)
                    //{
                    //    return true;
                    //}
                    //else
                    //{ 
                    //    return false;
                    //}
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public static bool isExistAccountInTransaction(Guid transactionId, Guid accountId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                XPQuery<GeneralJournal> generalJournalQuery = session.Query<GeneralJournal>();
                int count = generalJournalQuery.Where(r => r.TransactionId.TransactionId == transactionId
                                            && r.AccountId.AccountId == accountId).Count();
                if (count > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public Transaction GetPreviousTransaction(Session session, Guid _TransactionId)
        {
            Transaction transaction = null;
            Transaction result = null;
            try
            {
                transaction = session.GetObjectByKey<Transaction>(_TransactionId);
                if(transaction == null) return result;
                DateTime IssueDate = transaction.IssueDate;
                DateTime CreateDate = transaction.CreateDate;                
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                
                CriteriaOperator criteria_IssueDate_1 = new BinaryOperator("IssueDate", IssueDate, BinaryOperatorType.Less);
                CriteriaOperator criteria_IssueDate_2 = new BinaryOperator("IssueDate", IssueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("CreateDate", CreateDate, BinaryOperatorType.LessOrEqual);
                CriteriaOperator criteria_DateTime_1 = new GroupOperator(GroupOperatorType.And, criteria_IssueDate_2, criteria_CreateDate);
                CriteriaOperator criteria_DateTime = new GroupOperator(GroupOperatorType.Or, criteria_IssueDate_1, criteria_DateTime_1);
                
                CriteriaOperator criteria_Id = new BinaryOperator("TransactionId", transaction.TransactionId, BinaryOperatorType.NotEqual);
                
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And,criteria_RowStatus,criteria_DateTime,criteria_Id);
                XPCollection<Transaction> transactionCol = new XPCollection<Transaction>(session,criteria);

                if (transactionCol == null || transactionCol.Count == 0) return result;
                transactionCol.Sorting.Add(new SortProperty("IssueDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                transactionCol.Sorting.Add(new SortProperty("CreateDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                //transactionCol.OrderByDescending(r => r.IssueDate);
                //transactionCol.OrderByDescending(r => r.CreateDate);
                result = transactionCol.FirstOrDefault();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public Transaction GetNextTransaction(Session session, Guid _TransactionId)
        {
            Transaction transaction = null;
            Transaction result = null;
            try
            {
                transaction = session.GetObjectByKey<Transaction>(_TransactionId);
                if (transaction == null) return result;
                DateTime IssueDate = transaction.IssueDate;
                DateTime CreateDate = transaction.CreateDate;
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);

                CriteriaOperator criteria_IssueDate_1 = new BinaryOperator("IssueDate", IssueDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_IssueDate_2 = new BinaryOperator("IssueDate", IssueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("CreateDate", CreateDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_DateTime_1 = new GroupOperator(GroupOperatorType.And, criteria_IssueDate_2, criteria_CreateDate);
                CriteriaOperator criteria_DateTime = new GroupOperator(GroupOperatorType.Or, criteria_IssueDate_1, criteria_DateTime_1);

                CriteriaOperator criteria_Id = new BinaryOperator("TransactionId", transaction.TransactionId, BinaryOperatorType.NotEqual);

                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_RowStatus, criteria_DateTime, criteria_Id);
                XPCollection<Transaction> transactionCol = new XPCollection<Transaction>(session, criteria);

                if (transactionCol == null || transactionCol.Count == 0) return result;
                transactionCol.Sorting.Add(new SortProperty("IssueDate", DevExpress.Xpo.DB.SortingDirection.Ascending));
                transactionCol.Sorting.Add(new SortProperty("CreateDate", DevExpress.Xpo.DB.SortingDirection.Ascending));
                //transactionCol.OrderByDescending(r => r.IssueDate);
                //transactionCol.OrderByDescending(r => r.CreateDate);
                result = transactionCol.FirstOrDefault();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        //public byte Create<TOriginalArtifact>(Session session
        //                    , string _AccountingPeriodCode
        //                    , DateTime _IssueDate
        //                    , string _Description
        //                    , GeneralJournalEntity[] _generaljournal
        //                    , TOriginalArtifact originalArtifact
        //                    )
        //            where TOriginalArtifact : XPBaseObject
        //{
        //    try
        //    {
        //        General GeneralFunc = new General();
        //        XPQuery<AccountingPeriod> AccountingPeriodquery = session.Query<AccountingPeriod>();
        //        AccountingPeriod ActPer = AccountingPeriodquery.Where(r => r.Code == _AccountingPeriodCode).FirstOrDefault();

        //        XPQuery<Account> Accountquery = session.Query<Account>();
        //        Transaction transaction = null;
        //        if (originalArtifact is NAS.DAL.Invoice.SalesInvoice)
        //        {

        //            transaction = new SaleInvoiceTransaction(session)
        //            {
        //                AccountingPeriodId = ActPer,
        //                IssueDate = _IssueDate,
        //                CreateDate = DateTime.Now,
        //                Description = _Description,
        //                SalesInvoiceId = originalArtifact as NAS.DAL.Invoice.SalesInvoice
        //            };

        //        }
        //        else if (originalArtifact is NAS.DAL.Invoice.PurchaseInvoice)
        //        {
        //            transaction = new PurchaseInvoiceTransaction(session)
        //            {
        //                AccountingPeriodId = ActPer,
        //                IssueDate = _IssueDate,
        //                CreateDate = DateTime.Now,
        //                Description = _Description,
        //                PurchaseInvoiceId = originalArtifact as NAS.DAL.Invoice.PurchaseInvoice
        //            };
        //        }
        //        else if (originalArtifact is NAS.DAL.Vouches.ReceiptVouches)
        //        {
        //            transaction = new ReceiptVouchesTransaction(session)
        //            {
        //                AccountingPeriodId = ActPer,
        //                IssueDate = _IssueDate,
        //                CreateDate = DateTime.Now,
        //                Description = _Description,
        //                ReceiptVouchesId = originalArtifact as NAS.DAL.Vouches.ReceiptVouches
        //            };
        //        }
        //        else if (originalArtifact is NAS.DAL.Vouches.PaymentVouches)
        //        {
        //            transaction = new PaymentVouchesTransaction(session)
        //            {
        //                AccountingPeriodId = ActPer,
        //                IssueDate = _IssueDate,
        //                CreateDate = DateTime.Now,
        //                Description = _Description,
        //                PaymentVouchesId = originalArtifact as NAS.DAL.Vouches.PaymentVouches
        //            };
        //        }

        //        foreach (GeneralJournalEntity gj in _generaljournal)
        //        {
        //            Account acount = Accountquery.Where(r => r.Code == gj.AccountCode).FirstOrDefault();
        //            GeneralJournal generalJournal = new GeneralJournal(session)
        //            {
        //                Description = gj.Description,
        //                AccountId = acount,
        //                TransactionId = transaction,
        //                Credit = gj.Credit,
        //                Debit = gj.Debit,
        //                RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
        //            };
        //            generalJournal.Save();
        //            GeneralFunc.BalanceUpdate(session, acount, generalJournal, false, (gj.Debit - gj.Credit));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return 1;
        //        throw;
        //    }
        //    finally
        //    {
        //        if (session != null) session.Dispose();
        //    }
        //    return 0;
        //}
    }
}
