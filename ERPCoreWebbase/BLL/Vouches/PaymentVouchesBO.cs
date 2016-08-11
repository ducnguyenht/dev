using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Vouches;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Accounting.Currency;

namespace NAS.BO.Vouches
{
    public class PaymentVouchesBO
    {

        public PaymentVouches CreateNewObject(Session session)
        {
            try
            {
                PaymentVouches paymentVouches = new PaymentVouches(session)
                {
                    VouchesId = Guid.NewGuid(),
                    RowStatus = Utility.Constant.ROWSTATUS_TEMP
                };
                paymentVouches.Save();

                ObjectBO objectBO = new ObjectBO();

                NAS.DAL.CMS.ObjectDocument.Object CMSObject = objectBO.CreateCMSObject(session, 
                    DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_PAYMENT);

                VoucherObject voucherObject = new VoucherObject(session)
                {
                    ObjectId = CMSObject,
                    VoucherId = paymentVouches
                };
                voucherObject.Save();

                VoucherCustomType voucherCustomType = new VoucherCustomType(session)
                {
                    VoucherId = paymentVouches,
                    ObjectTypeId = ObjectType.GetDefault(session, ObjectTypeEnum.VOUCHER_PAYMENT)
                };
                voucherCustomType.Save();

                return paymentVouches;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        #region 2013-12-05 Khoa.Truong TEMP START
        //public void Insert(Guid tempReceiptVouchesId,
        //                            string code,
        //                            DateTime issuedDate,
        //                            string description,
        //                            string address,
        //                            string payer,
        //                            short rowStatus,
        //                            Guid sourceOrganizationId,
        //                            Guid targetOrganizationId,
        //                            Guid vouchesTypeId)
        //{
        //    UnitOfWork uow = null;
        //    try
        //    {
        //        uow = XpoHelper.GetNewUnitOfWork();
        //        //Update temp data
        //        ReceiptVouches newReceiptVouches =
        //            uow.GetObjectByKey<ReceiptVouches>(tempReceiptVouchesId);
        //        newReceiptVouches.Code = code;
        //        newReceiptVouches.IssuedDate = issuedDate;
        //        newReceiptVouches.Description = description;
        //        newReceiptVouches.Address = address;
        //        newReceiptVouches.RowStatus = rowStatus;
        //        newReceiptVouches.SourceOrganizationId =
        //            uow.GetObjectByKey<Organization>(sourceOrganizationId);
        //        newReceiptVouches.TargetOrganizationId =
        //            uow.GetObjectByKey<Organization>(targetOrganizationId);
        //        newReceiptVouches.VouchesTypeId =
        //            uow.GetObjectByKey<VouchesType>(vouchesTypeId);

        //        newReceiptVouches.Payer = payer;

        //        newReceiptVouches.CreateDate = DateTime.Now;
        //        newReceiptVouches.LastUpdateDate = newReceiptVouches.CreateDate;

        //        //Missing logic here...
        //        uow.CommitTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (uow != null) uow.Dispose();
        //    }
        //}
        #endregion

        #region 2013-12-05 Khoa.Truong INS START
        public void Insert(Guid tempPaymentVouchesId,
                            string code,
                            DateTime issuedDate,
                            string description,
                            string address,
                            string payee,
                            Guid sourceOrganizationId,
                            Guid targetOrganizationId,
                            double credit,
                            Guid currencyId,
                            double exchangeRate)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Update temp data
                PaymentVouches newPaymentVouches =
                    uow.GetObjectByKey<PaymentVouches>(tempPaymentVouchesId);
                newPaymentVouches.Code = code;
                newPaymentVouches.IssuedDate = issuedDate;
                newPaymentVouches.Description = description;
                newPaymentVouches.Address = address;
                newPaymentVouches.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                newPaymentVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                newPaymentVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                newPaymentVouches.Payee = payee;
                newPaymentVouches.CreateDate = DateTime.Now;
                newPaymentVouches.LastUpdateDate = newPaymentVouches.CreateDate;
                newPaymentVouches.SumOfCredit = credit * exchangeRate;
                newPaymentVouches.SumOfDebit = 0;

                //Insert data into VoucherAmount table
                NAS.DAL.Vouches.VouchesAmount voucherAmount = new VouchesAmount(uow)
                {
                    Credit = credit,
                    Debit = 0,
                    ExchangeRate = exchangeRate,
                    CurrencyId = uow.GetObjectByKey<Currency>(currencyId),
                    VouchesId = newPaymentVouches
                };

                //2014-02-18 ERP-1540 Khoa.Truong INS START
                //Update issue date for all voucher transactions
                if (newPaymentVouches.PaymentVouchesTransactions != null)
                {
                    foreach (var paymentVouchesTransaction in newPaymentVouches.PaymentVouchesTransactions)
                    {
                        paymentVouchesTransaction.IssueDate = newPaymentVouches.IssuedDate;
                        paymentVouchesTransaction.Save();
                    }
                }
                //2014-02-18 ERP-1540 Khoa.Truong INS END

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
        #endregion

        #region 2013-12-05 Khoa.Truong INS START
        public void Update(Guid paymentVouchesId,
                            string code,
                            DateTime issuedDate,
                            string description,
                            string address,
                            string payee,
                            Guid sourceOrganizationId,
                            Guid targetOrganizationId,
                            double credit,
                            Guid currencyId,
                            double exchangeRate)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Update voucher data
                PaymentVouches paymentVouches =
                    uow.GetObjectByKey<PaymentVouches>(paymentVouchesId);
                paymentVouches.Code = code;
                paymentVouches.IssuedDate = issuedDate;
                paymentVouches.Description = description;
                paymentVouches.Address = address;
                paymentVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                paymentVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                paymentVouches.Payee = payee;
                paymentVouches.LastUpdateDate = paymentVouches.CreateDate;
                paymentVouches.SumOfCredit = credit * exchangeRate;
                paymentVouches.SumOfDebit = 0;

                VouchesAmount voucherAmount = paymentVouches.VouchesAmounts.FirstOrDefault();
                if (voucherAmount == null)
                {
                    throw new Exception("The payment voucher is invalid in inserting");
                }
                //update VoucherAmount data
                voucherAmount.Credit = credit;
                voucherAmount.Debit = 0;
                voucherAmount.ExchangeRate = exchangeRate;
                voucherAmount.CurrencyId = uow.GetObjectByKey<Currency>(currencyId);

                //2014-02-18 ERP-1540 Khoa.Truong INS START
                //Update issue date for all voucher transactions
                if (paymentVouches.PaymentVouchesTransactions != null)
                {
                    foreach (var paymentVouchesTransaction in paymentVouches.PaymentVouchesTransactions)
                    {
                        paymentVouchesTransaction.IssueDate = paymentVouches.IssuedDate;
                        paymentVouchesTransaction.Save();
                    }
                }
                //2014-02-18 ERP-1540 Khoa.Truong INS END

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
        #endregion


        public static void Insert(Guid tempPaymentVouchesId,
                                    string code,
                                    DateTime issuedDate,
                                    string description,
                                    string address,
                                    string payee,
                                    short rowStatus,
                                    Guid sourceOrganizationId,
                                    Guid targetOrganizationId,
                                    Guid vouchesTypeId)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Update temp data
                PaymentVouches newPaymentVouches =
                    uow.GetObjectByKey<PaymentVouches>(tempPaymentVouchesId);
                newPaymentVouches.Code = code;
                newPaymentVouches.IssuedDate = issuedDate;
                newPaymentVouches.Description = description;
                newPaymentVouches.Address = address;
                newPaymentVouches.RowStatus = rowStatus;
                newPaymentVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                newPaymentVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                newPaymentVouches.VouchesTypeId =
                    uow.GetObjectByKey<VouchesType>(vouchesTypeId);
                newPaymentVouches.Payee = payee;
                newPaymentVouches.CreateDate = DateTime.Now;

                //Missing logic here...
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

        public static void Update(Guid paymentVouchesId,
                                    string code,
                                    DateTime issuedDate,
                                    string description,
                                    string address,
                                    string payee,
                                    short rowStatus,
                                    Guid sourceOrganizationId,
                                    Guid targetOrganizationId,
                                    Guid vouchesTypeId)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Update temp data
                PaymentVouches paymentVouches =
                    uow.GetObjectByKey<PaymentVouches>(paymentVouchesId);
                paymentVouches.Code = code;
                paymentVouches.IssuedDate = issuedDate;
                paymentVouches.Description = description;
                paymentVouches.Address = address;
                paymentVouches.LastUpdateDate = DateTime.Now;
                paymentVouches.RowStatus = rowStatus;
                paymentVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                paymentVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                paymentVouches.VouchesTypeId =
                    uow.GetObjectByKey<VouchesType>(vouchesTypeId);
                paymentVouches.Payee = payee;
                //Missing logic here...
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

        public static void UpdateSumOfCredit(Guid paymentVouchesId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                PaymentVouches paymentVouches = 
                    session.GetObjectByKey<PaymentVouches>(paymentVouchesId);
                double sumOfCredit = 0;
                foreach (var item in paymentVouches.VouchesAmounts)
                {
                    sumOfCredit += item.Credit * item.ExchangeRate;
                }
                paymentVouches.SumOfCredit = sumOfCredit;
                paymentVouches.Save();
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

        public static void DeleteLogical(Guid vouchesId)
        {

        }

    }
}
