﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL;
using NAS.DAL.Vouches;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Accounting.AccountChart;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.Accounting.JournalAllocation;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.BO.Accounting.Journal
{
    public class PaymentVoucherTransactionBO : VoucherTransactionBOBase
    {
        public override void CreateTransaction(Guid voucherId, string code, DateTime issuedDate, double amount, string description)
        {
            UnitOfWork uow = null;
            try
            {
                GeneralJournalBO generalJournalBO = new GeneralJournalBO();
                uow = XpoHelper.GetNewUnitOfWork();
                //Get the origin artifact
                PaymentVouches paymnetVouches = uow.GetObjectByKey<PaymentVouches>(voucherId);

                //Create new transaction
                PaymentVouchesTransaction transaction = new PaymentVouchesTransaction(uow)
                {
                    TransactionId = Guid.NewGuid(),
                    Amount = amount,
                    Code = code,
                    CreateDate = DateTime.Now,
                    Description = description,
                    IssueDate = issuedDate,
                    PaymentVouchesId = paymnetVouches,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    UpdateDate = DateTime.Now
                };
                uow.FlushChanges();
                //Create double entry
                //Create debit jounal
                GeneralJournal debitGeneralJournal = generalJournalBO.CreateGeneralJournal
                    (
                        uow,
                        transaction.TransactionId,
                        Account.GetDefault(uow, DefaultAccountEnum.NAAN_DEFAULT).AccountId,
                        Side.DEBIT,
                        amount,
                        description,
                        JounalTypeFlag.ACTUAL
                    );
                //Create credit jounal
                GeneralJournal creditGeneralJournal = generalJournalBO.CreateGeneralJournal
                    (
                        uow,
                        transaction.TransactionId,
                        Account.GetDefault(uow, DefaultAccountEnum.NAAN_DEFAULT).AccountId,
                        Side.CREDIT,
                        amount,
                        description,
                        JounalTypeFlag.ACTUAL
                    );

                ObjectBO objectBO = new ObjectBO();
                //Create CMS object for transaction
                NAS.DAL.CMS.ObjectDocument.Object transactionCMSObject =
                    objectBO.CreateCMSObject(uow,
                        DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_PAYMENT);

                TransactionObject transactionObject = new TransactionObject(uow)
                {
                    ObjectId = transactionCMSObject,
                    TransactionId = transaction
                };

                GeneralJournalObject debitGeneralJournalObject = null;
                GeneralJournalObject creditGeneralJournalObject = null;
                //Create CMS object for debit jounal
                NAS.DAL.CMS.ObjectDocument.Object debitJounalCMSObject =
                    objectBO.CreateCMSObject(uow,
                        DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_PAYMENT);
                debitGeneralJournalObject = new GeneralJournalObject(uow)
                {
                    GeneralJournalId = debitGeneralJournal,
                    ObjectId = debitJounalCMSObject
                };

                //Create CMS object for debit jounal
                NAS.DAL.CMS.ObjectDocument.Object creditJounalCMSObject =
                    objectBO.CreateCMSObject(uow,
                        DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_PAYMENT);
                creditGeneralJournalObject = new GeneralJournalObject(uow)
                {
                    GeneralJournalId = creditGeneralJournal,
                    ObjectId = creditJounalCMSObject
                };

                uow.FlushChanges();

                //Copy readonly data from original artifact
                //Get origin CMS object
                VoucherObject voucherObject = paymnetVouches.VoucherObjects.FirstOrDefault();
                if (voucherObject != null)
                {
                    NAS.DAL.CMS.ObjectDocument.Object CMSVoucherObject = voucherObject.ObjectId;
                    //Copy artifact's data to cms object of transaction
                    objectBO.CopyReadOnlyCustomFieldData(
                        CMSVoucherObject.ObjectId,
                        transactionCMSObject.ObjectId);
                    //Copy artifact's data to cms object of default general journals
                    objectBO.CopyReadOnlyCustomFieldData(
                        CMSVoucherObject.ObjectId,
                        debitJounalCMSObject.ObjectId);

                    objectBO.CopyReadOnlyCustomFieldData(
                        CMSVoucherObject.ObjectId,
                        creditJounalCMSObject.ObjectId);
                }

                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }

        public override void DeleteTransaction(Guid transactionId)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                PaymentVouchesTransaction transaction =
                    uow.GetObjectByKey<PaymentVouchesTransaction>(transactionId);
                if (transaction == null)
                {
                    throw new Exception("Could not found the transaction");
                }
                //Update rowstatus for the transaction
                transaction.RowStatus = Utility.Constant.ROWSTATUS_DELETED;

                //Update rowstatus for all journals of transacion
                foreach (var generalJounal in transaction.GeneralJournals)
                {
                    generalJounal.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    generalJounal.Save();
                }

                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }

        public override void UpdateTransaction(Guid transactionId, string code, DateTime issuedDate, double amount, string description)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Update transaction
                PaymentVouchesTransaction transaction =
                    uow.GetObjectByKey<PaymentVouchesTransaction>(transactionId);
                if (transaction == null)
                {
                    throw new Exception("Could not found the transaction");
                }
                transaction.Amount = amount;
                transaction.Code = code;
                transaction.Description = description;
                transaction.IssueDate = issuedDate;
                transaction.UpdateDate = DateTime.Now;

                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }

        public override bool CanBookEntries(Guid voucherId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get voucher
                PaymentVouches voucher = session.GetObjectByKey<PaymentVouches>(voucherId);
                if (voucher == null)
                {
                    throw new Exception("Could not found voucher");
                }
                TransactionBOBase transactionBOBase = new TransactionBOBase();
                foreach (var transaction in voucher.PaymentVouchesTransactions)
                {
                    if (transactionBOBase.CanBookingEntry(transaction.TransactionId, true) != CanBookingEntryReturnValue.BALANCED)
                    {
                        return false;
                    }
                }
                return true;
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

        public override XPCollection<Transaction> GetRelatedTransactionsWithBill(Session session, Guid billId)
        {
            try
            {
                XPCollection<Transaction> ret = null;
                ObjectBO objectBO = new ObjectBO();
                List<Guid> billIds = new List<Guid>();
                billIds.Add(billId);

                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session,
                        DefaultObjectTypeCustomFieldEnum.PAYMENT_VOUCHER_PURCHASE_INVOICE);

                var relatedObjectList =
                    objectBO.FindCMSObjectsOfBuiltInCustomField(
                        session,
                        objectTypeCustomField.ObjectTypeCustomFieldId,
                        billIds);

                if (relatedObjectList == null)
                    return null;

                var relatedObjectIdList = relatedObjectList.Select(r => r.ObjectId);

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual),
                    new ContainsOperator("TransactionObjects",
                        new InOperator("ObjectId.ObjectId", relatedObjectIdList)));

                ret = new XPCollection<Transaction>(session,
                    new XPCollection<PaymentVouchesTransaction>(session), criteria);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override XPCollection<GeneralJournal> GetActuallyCollectedOfBill(Session session, Guid billId)
        {
            try
            {
                XPCollection<GeneralJournal> ret = null;
                ObjectBO objectBO = new ObjectBO();
                List<Guid> billIds = new List<Guid>();
                billIds.Add(billId);

                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session,
                        DefaultObjectTypeCustomFieldEnum.PAYMENT_VOUCHER_PURCHASE_INVOICE);

                var relatedObjectList =
                    objectBO.FindCMSObjectsOfBuiltInCustomField(
                        session,
                        objectTypeCustomField.ObjectTypeCustomFieldId,
                        billIds);

                if (relatedObjectList == null)
                    return null;

                var relatedObjectIdList = relatedObjectList.Select(r => r.ObjectId);

                XPCollection<PaymentVouchesTransaction> paymentVouchesTransactions
                    = new XPCollection<PaymentVouchesTransaction>(session,
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual));

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_TEMP, BinaryOperatorType.GreaterOrEqual),
                    new BinaryOperator("Credit", 0, BinaryOperatorType.Greater),
                    new ContainsOperator("GeneralJournalObjects",
                        new InOperator("ObjectId.ObjectId", relatedObjectIdList)));

                ret = new XPCollection<GeneralJournal>(session,
                        paymentVouchesTransactions.SelectMany(r => r.GeneralJournals), criteria);

                //var generalJournalObjects =
                //    receiptVouchesTransactions
                //    .SelectMany(r => r.GeneralJournals)
                //    .SelectMany(r => r.GeneralJournalObjects)
                //    .Where(r => r.GeneralJournalId.RowStatus >= Utility.Constant.ROWSTATUS_TEMP
                //        && relatedObjectIdList.Contains(r.ObjectId.ObjectId));

                //ret = new XPCollection<GeneralJournal>(session, 
                //    generalJournalObjects.Select(r => r.GeneralJournalId));

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get supplier that allocated for the PaymentVouchesTransaction
        /// </summary>
        /// <param name="session">DevExpress.Xpo.Session</param>
        /// <param name="transactionId">Id of type PaymentVouchesTransaction</param>
        /// <returns></returns>
        public Organization GetAllocatedSupplier(Session session, Guid transactionId)
        {
            Organization ret = null;
            try
            {
                PaymentVouchesTransaction transaction =
                    session.GetObjectByKey<PaymentVouchesTransaction>(transactionId);
                TransactionObject transactionObject = transaction.TransactionObjects.First();
                if (transactionObject == null)
                    return null;
                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session, DefaultObjectTypeCustomFieldEnum.PAYMENT_VOUCHER_SUPPLIER);

                ObjectCustomField objectCustomField = transactionObject.ObjectId.ObjectCustomFields.Where(
                        r => r.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId ==
                            objectTypeCustomField.ObjectTypeCustomFieldId).FirstOrDefault();
                if (objectCustomField == null)
                    return null;

                ObjectCustomFieldData objectCustomFieldData =
                    objectCustomField.ObjectCustomFieldDatas.FirstOrDefault();
                if (objectCustomFieldData == null)
                    return null;

                PredefinitionData predefinitionData =
                    (PredefinitionData)objectCustomFieldData.CustomFieldDataId;

                ret = session.GetObjectByKey<Organization>(predefinitionData.RefId);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get customer that allocated for the PaymentVouchesTransaction
        /// </summary>
        /// <param name="session">DevExpress.Xpo.Session</param>
        /// <param name="transactionId">Id of type PaymentVouchesTransaction</param>
        /// <returns></returns>
        public Organization GetAllocatedCustomer(Session session, Guid transactionId)
        {
            Organization ret = null;
            try
            {
                PaymentVouchesTransaction transaction =
                    session.GetObjectByKey<PaymentVouchesTransaction>(transactionId);
                TransactionObject transactionObject = transaction.TransactionObjects.First();
                if (transactionObject == null)
                    return null;
                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session, DefaultObjectTypeCustomFieldEnum.PAYMENT_VOUCHER_CUSTOMER);

                ObjectCustomField objectCustomField = transactionObject.ObjectId.ObjectCustomFields.Where(
                        r => r.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId ==
                            objectTypeCustomField.ObjectTypeCustomFieldId).FirstOrDefault();
                if (objectCustomField == null)
                    return null;

                ObjectCustomFieldData objectCustomFieldData =
                    objectCustomField.ObjectCustomFieldDatas.FirstOrDefault();
                if (objectCustomFieldData == null)
                    return null;

                PredefinitionData predefinitionData =
                    (PredefinitionData)objectCustomFieldData.CustomFieldDataId;

                ret = session.GetObjectByKey<Organization>(predefinitionData.RefId);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
