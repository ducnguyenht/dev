using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Vouches;
using NAS.DAL;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Accounting.Currency;

namespace NAS.BO.Vouches
{
    public class ReceiptVouchesBO
    {

        public ReceiptVouches CreateNewObject(Session session)
        {
            try
            {
                ReceiptVouches receiptVouches = new ReceiptVouches(session)
                {
                    VouchesId = Guid.NewGuid(),
                    RowStatus = Utility.Constant.ROWSTATUS_TEMP
                };
                receiptVouches.Save();

                ObjectBO objectBO = new ObjectBO();

                NAS.DAL.CMS.ObjectDocument.Object CMSObject = objectBO.CreateCMSObject(session,
                    DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_RECEIPT);

                VoucherObject voucherObject = new VoucherObject(session)
                {
                    ObjectId = CMSObject,
                    VoucherId = receiptVouches
                };
                voucherObject.Save();

                VoucherCustomType voucherCustomType = new VoucherCustomType(session)
                {
                    VoucherId = receiptVouches,
                    ObjectTypeId = ObjectType.GetDefault(session, ObjectTypeEnum.VOUCHER_RECEIPT)
                };
                voucherCustomType.Save();

                return receiptVouches;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        /*2013-12-05 Khoa.Truong DEL START
         * Changes to non static method
        //public static void Insert(Guid tempReceiptVouchesId,
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
        2013-12-05 Khoa.Truong DEL END*/

        #region 2013-12-05 Khoa.Truong INS START
        public void Insert(Guid tempReceiptVouchesId,
                                    string code,
                                    DateTime issuedDate,
                                    string description,
                                    string address,
                                    string payer,
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
                ReceiptVouches newReceiptVouches =
                    uow.GetObjectByKey<ReceiptVouches>(tempReceiptVouchesId);
                newReceiptVouches.Code = code;
                newReceiptVouches.IssuedDate = issuedDate;
                newReceiptVouches.Description = description;
                newReceiptVouches.Address = address;
                newReceiptVouches.RowStatus = rowStatus;
                newReceiptVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                newReceiptVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                newReceiptVouches.VouchesTypeId =
                    uow.GetObjectByKey<VouchesType>(vouchesTypeId);

                newReceiptVouches.Payer = payer;

                newReceiptVouches.CreateDate = DateTime.Now;
                newReceiptVouches.LastUpdateDate = newReceiptVouches.CreateDate;

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
        #endregion

        #region 2013-12-05 Khoa.Truong INS START
        public void Insert( Guid tempReceiptVouchesId,
                            string code,
                            DateTime issuedDate,
                            string description,
                            string address,
                            string payer,
                            Guid sourceOrganizationId,
                            Guid targetOrganizationId,
                            double debit,
                            Guid currencyId,
                            double exchangeRate)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Update temp data
                ReceiptVouches newReceiptVouches =
                    uow.GetObjectByKey<ReceiptVouches>(tempReceiptVouchesId);
                newReceiptVouches.Code = code;
                newReceiptVouches.IssuedDate = issuedDate;
                newReceiptVouches.Description = description;
                newReceiptVouches.Address = address;
                newReceiptVouches.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                newReceiptVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                newReceiptVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                newReceiptVouches.Payer = payer;
                newReceiptVouches.CreateDate = DateTime.Now;
                newReceiptVouches.LastUpdateDate = newReceiptVouches.CreateDate;
                newReceiptVouches.SumOfCredit = 0;
                newReceiptVouches.SumOfDebit = debit * exchangeRate;
                
                //Insert data into VoucherAmount table
                NAS.DAL.Vouches.VouchesAmount voucherAmount = new VouchesAmount(uow)
                {
                    Credit = 0,
                    Debit = debit,
                    ExchangeRate = exchangeRate,
                    CurrencyId = uow.GetObjectByKey<Currency>(currencyId),
                    VouchesId = newReceiptVouches
                };

                //2014-02-18 ERP-1540 Khoa.Truong INS START
                //Update issue date for all voucher transaction
                if (newReceiptVouches.ReceiptVouchesTransactions != null)
                {
                    foreach (var receiptVouchesTransaction in newReceiptVouches.ReceiptVouchesTransactions)
                    {
                        receiptVouchesTransaction.IssueDate = newReceiptVouches.IssuedDate;
                        receiptVouchesTransaction.Save();
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
        public void Update(Guid receiptVouchesId,
                            string code,
                            DateTime issuedDate,
                            string description,
                            string address,
                            string payer,
                            Guid sourceOrganizationId,
                            Guid targetOrganizationId,
                            double debit,
                            Guid currencyId,
                            double exchangeRate)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Update voucher data
                ReceiptVouches receiptVouches =
                    uow.GetObjectByKey<ReceiptVouches>(receiptVouchesId);
                receiptVouches.Code = code;
                receiptVouches.IssuedDate = issuedDate;
                receiptVouches.Description = description;
                receiptVouches.Address = address;
                receiptVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                receiptVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                receiptVouches.Payer = payer;
                receiptVouches.LastUpdateDate = receiptVouches.CreateDate;
                receiptVouches.SumOfCredit = 0;
                receiptVouches.SumOfDebit = debit * exchangeRate;

                VouchesAmount voucherAmount = receiptVouches.VouchesAmounts.FirstOrDefault();
                if (voucherAmount == null)
                {
                    throw new Exception("The receipt voucher is invalid in inserting");
                }
                //update VoucherAmount data
                voucherAmount.Credit = 0;
                voucherAmount.Debit = debit;
                voucherAmount.ExchangeRate = exchangeRate;
                voucherAmount.CurrencyId = uow.GetObjectByKey<Currency>(currencyId);

                //2014-02-18 ERP-1540 Khoa.Truong INS START
                //Update issue date for all voucher transaction
                if (receiptVouches.ReceiptVouchesTransactions != null)
                {
                    foreach (var receiptVouchesTransaction in receiptVouches.ReceiptVouchesTransactions)
                    {
                        receiptVouchesTransaction.IssueDate = receiptVouches.IssuedDate;
                        receiptVouchesTransaction.Save();
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

        /*2013-12-05 Khoa.Truong DEL START
         * Changes to non static method
        public static void Update(Guid receiptVouchesId,
                                    string code,
                                    DateTime issuedDate,
                                    string description,
                                    string address,
                                    string payer,
                                    short rowStatus,
                                    Guid sourceOrganizationId,
                                    Guid targetOrganizationId,
                                    Guid vouchesTypeId)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();

                ReceiptVouches receiptVouches =
                    uow.GetObjectByKey<ReceiptVouches>(receiptVouchesId);
                receiptVouches.Code = code;
                receiptVouches.IssuedDate = issuedDate;
                receiptVouches.Description = description;
                receiptVouches.Address = address;
                receiptVouches.LastUpdateDate = DateTime.Now;
                receiptVouches.RowStatus = rowStatus;
                receiptVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                receiptVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                receiptVouches.VouchesTypeId =
                    uow.GetObjectByKey<VouchesType>(vouchesTypeId);

                receiptVouches.Payer = payer;

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
        */

        public void Update(Guid receiptVouchesId,
                                    string code,
                                    DateTime issuedDate,
                                    string description,
                                    string address,
                                    string payer,
                                    short rowStatus,
                                    Guid sourceOrganizationId,
                                    Guid targetOrganizationId,
                                    Guid vouchesTypeId)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();

                ReceiptVouches receiptVouches =
                    uow.GetObjectByKey<ReceiptVouches>(receiptVouchesId);
                receiptVouches.Code = code;
                receiptVouches.IssuedDate = issuedDate;
                receiptVouches.Description = description;
                receiptVouches.Address = address;
                receiptVouches.LastUpdateDate = DateTime.Now;
                receiptVouches.RowStatus = rowStatus;
                receiptVouches.SourceOrganizationId =
                    uow.GetObjectByKey<Organization>(sourceOrganizationId);
                receiptVouches.TargetOrganizationId =
                    uow.GetObjectByKey<Organization>(targetOrganizationId);
                receiptVouches.VouchesTypeId =
                    uow.GetObjectByKey<VouchesType>(vouchesTypeId);

                receiptVouches.Payer = payer;

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

        public static void UpdateSumOfDebit(Guid receiptVouchesId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                ReceiptVouches receiptVouches =
                    session.GetObjectByKey<ReceiptVouches>(receiptVouchesId);
                double sumOfDebit = 0;
                foreach (var item in receiptVouches.VouchesAmounts)
                {
                    sumOfDebit += item.Debit * item.ExchangeRate;
                }
                receiptVouches.SumOfDebit = sumOfDebit;
                receiptVouches.Save();
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

        //DND
        public String searchOrganizationId(Session session)
        {
            XPCollection<Organization> collectionOrg = new XPCollection<Organization>(session);
            collectionOrg.Criteria = CriteriaOperator.And(
                new BinaryOperator("Code", "NAAN_DEFAULT", BinaryOperatorType.Equal),
                new BinaryOperator("RowStatus", -1, BinaryOperatorType.Equal)
                );
            Organization orgId = collectionOrg.First();

            return orgId.OrganizationId.ToString();
        }

        public Boolean searchOrgDefault(Session session, string OrgId)
        {
            bool check = false;
            XPCollection<Organization> collectionOrgDefault = new XPCollection<Organization>(session);
            collectionOrgDefault.Criteria = CriteriaOperator.And(
                    new BinaryOperator("OrganizationId", OrgId, BinaryOperatorType.Equal),
                    new BinaryOperator("Code", "NAAN_DEFAULT", BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", -1, BinaryOperatorType.Equal)
                );
            if (collectionOrgDefault.Count > 0)
                check = true;
            return check;
        }

        public String searchForeignCurrencyId(Session session)
        {
            XPCollection<ForeignCurrency> collectionFC = new XPCollection<ForeignCurrency>(session);
            collectionFC.Criteria = CriteriaOperator.And(
                new BinaryOperator("Name", "NAAN_DEFAULT", BinaryOperatorType.Equal),
                new BinaryOperator("RowStatus", -1, BinaryOperatorType.Equal)
                );
            ForeignCurrency FCId = collectionFC.First();

            return FCId.ForeignCurrencyId.ToString();
        }

        public String searchVouchesTypeId(Session session, string value)
        {
            XPCollection<VouchesType> collection = new XPCollection<VouchesType>(session);
            collection.Criteria = CriteriaOperator.And(
                new BinaryOperator("Description", value, BinaryOperatorType.Equal),
                new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)
                );

            VouchesType voucherType_id = collection.First();
            return voucherType_id.VouchesTypeId.ToString();
        }
        public String searchOrgnAdress(Session session, string value)
        {
            string a = "";
            if (!value.Equals(""))
            {
                XPCollection<Organization> collectionOrgAd = new XPCollection<Organization>(session);
                collectionOrgAd.Criteria = CriteriaOperator.And(
                    new BinaryOperator("Code", value, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)
                    );

                Organization orgAdress = collectionOrgAd.First();
                a = orgAdress.Address.ToString();
            }
            return a.ToString();
        }
        public String searchOrgId(Session session, string value)
        {
            string a = "";
            if (!value.Equals(""))
            {
                XPCollection<Organization> collectionOrgAd = new XPCollection<Organization>(session);
                collectionOrgAd.Criteria = CriteriaOperator.And(
                    new BinaryOperator("Code", value, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)
                    );

                Organization orgAdress = collectionOrgAd.First();
                a = orgAdress.OrganizationId.ToString();
            }
            return a.ToString();
        }



        //END DND
    }
}
