using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using NAS.DAL.Invoice;
using NAS.BO.Nomenclature.Items;
using NAS.DAL.Accounting.Journal;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Xpo;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.JournalAllocation;

namespace NAS.BO.Invoice
{
    public class SalesInvoiceBO : BillBOBase
    {

        public override bool Delete(DevExpress.Xpo.Session session, Guid billId)
        {
            try
            {
                //Get bill
                NAS.DAL.Invoice.SalesInvoice bill =
                    session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId);

                if (bill == null)
                    throw new Exception("Could not found bill");

                //Validate
                if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                {
                    throw new Exception(String.Format("Không thể xóa vì hóa đơn '{0}' đã được ghi sổ.", bill.Code));
                }

                //Mark delete status on bill
                bill.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                bill.Save();

                var billItems = bill.BillItems.Where(r => r.RowStatus >= 0);
                //Mark delete status on bill items
                if (billItems != null)
                {
                    foreach (var item in billItems)
                    {
                        item.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        item.Save();
                    }
                }

                var invoiceTransactions = bill.SaleInvoiceTransactions.Where(r => r.RowStatus >= 0);
                //Mark delete status on bill invoice transactions
                if (invoiceTransactions != null)
                {
                    foreach (var item in invoiceTransactions)
                    {
                        item.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        item.Save();
                    }
                }

                var generalJournals = bill.SaleInvoiceTransactions.Where(r => r.RowStatus >= 0)
                    .SelectMany(r => r.GeneralJournals)
                    .Where(r => r.RowStatus >= 0);
                //Mark delete status on bill invoice general journals
                if (generalJournals != null)
                {
                    foreach (var item in generalJournals)
                    {
                        item.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        item.Save();
                    }
                }

                var inventoryTransactions = bill.SalesInvoiceInventoryTransactions.Where(r => r.RowStatus >= 0);
                //Mark delete status on bill invoice inventory transactions
                if (inventoryTransactions != null)
                {
                    foreach (var item in inventoryTransactions)
                    {
                        item.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        item.Save();
                    }
                }

                var inventoryJournals = bill.SalesInvoiceInventoryTransactions.Where(r => r.RowStatus >= 0)
                    .SelectMany(r => r.InventoryJournals)
                    .Where(r => r.RowStatus >= 0);
                //Mark delete status on bill invoice inventory journals
                if (inventoryJournals != null)
                {
                    foreach (var item in inventoryJournals)
                    {
                        item.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        item.Save();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override Bill InitTemporary(DevExpress.Xpo.Session session, BillTypeEnum billType)
        {
            try
            {
                NAS.DAL.Invoice.SalesInvoice ret = null;
                ret = new DAL.Invoice.SalesInvoice(session)
                {
                    BillId = Guid.NewGuid(),
                    RowStatus = Utility.Constant.ROWSTATUS_TEMP,
                    CreateDate = DateTime.Now,
                    IssuedDate = DateTime.Now,
                    TaxClaimStatus = Utility.Constant.VAT_NO_DECLARE,
                    TaxCalculationType = Utility.Constant.CALCULATION_TYPE_ON_ITEMS,
                    PromotionCalculationType = Utility.Constant.CALCULATION_TYPE_ON_ITEMS,
                    BillType = (byte)billType
                };
                ret.Save();
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Save(
            DevExpress.Xpo.Session session,
            Guid billId,
            string billCode,
            DateTime issuedDate,
            DAL.Nomenclature.Organization.Organization sourceOrganizationBill,
            DAL.Nomenclature.Organization.Person targetOrganizationBill)
        {
            try
            {
                //Get bill by ID
                NAS.DAL.Invoice.SalesInvoice bill =
                    session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId);
                if (bill == null)
                    throw new Exception("Could not found bill");

                bill.Code = billCode;
                bill.IssuedDate = issuedDate;
                bill.SourceOrganizationId = sourceOrganizationBill;
                bill.TargetOrganizationId = targetOrganizationBill;
                bill.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;

                bill.Save();

                //Create default actual transaction
                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual),
                    CriteriaOperator.Or(
                        new ContainsOperator("GeneralJournals",
                            new BinaryOperator("JournalType", JounalTypeConstant.ACTUAL)
                        ),
                        new BinaryOperator(new AggregateOperand("GeneralJournals", Aggregate.Count), 0, BinaryOperatorType.Equal)
                    )
                );

                var actualSaleInvoiceTransactions = bill.SaleInvoiceTransactions;

                actualSaleInvoiceTransactions.Criteria = criteria;

                if (actualSaleInvoiceTransactions == null
                    || actualSaleInvoiceTransactions.Count == 0)
                {
                    SaleInvoiceTransaction saleInvoiceTransaction
                        = new SaleInvoiceTransaction(session)
                        {
                            Code = "BT_" + bill.Code,
                            CreateDate = DateTime.Now,
                            Description = "BT_" + bill.Code,
                            IssueDate = issuedDate,
                            RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                            UpdateDate = DateTime.Now,
                            SalesInvoiceId = bill
                        };
                    saleInvoiceTransaction.Save();

                    ObjectBO objectBO = new ObjectBO();
                    NAS.DAL.CMS.ObjectDocument.Object cmsObject =
                        objectBO.CreateCMSObject(session, DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_SALE);

                    TransactionObject transactionObject = new TransactionObject(session)
                    {
                        ObjectId = cmsObject,
                        TransactionId = saleInvoiceTransaction
                    };

                    transactionObject.Save();

                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBillActor(Session session, Guid billId, Guid creatorPersonId,
            Guid salesPersonId, Guid chiefAccountantPersonId, Guid directorPersonId)
        {
            try
            {
                //Get bill
                Bill bill = session.GetObjectByKey<Bill>(billId);
                if (bill == null)
                    throw new Exception("Could not foung bill");

                //Get creator
                Person creator = session.GetObjectByKey<Person>(creatorPersonId);
                Person sales = session.GetObjectByKey<Person>(salesPersonId);
                Person chiefAccountant = session.GetObjectByKey<Person>(chiefAccountantPersonId);
                Person director = session.GetObjectByKey<Person>(directorPersonId);

                int countExistType;
                BillActorType creatorType;
                //Update CREATOR bill actor type
                creatorType = BillActorType.GetDefault(session, BillActorTypeEnum.CREATOR);
                countExistType = bill.BillActors.Count(r => r.BillActorTypeId == creatorType);
                if (countExistType == 0)
                {
                    BillActor billActor = new BillActor(session)
                    {
                        BillActorTypeId = creatorType,
                        PersonId = creator,
                        BillId = bill
                    };
                }
                else
                {
                    BillActor billActor = bill.BillActors.FirstOrDefault(r => r.BillActorTypeId == creatorType);
                    billActor.PersonId = creator;
                }
                session.FlushChanges();

                //Update SALES bill actor type
                creatorType = BillActorType.GetDefault(session, BillActorTypeEnum.SALES);
                countExistType = bill.BillActors.Count(r => r.BillActorTypeId == creatorType);
                if (countExistType == 0)
                {
                    BillActor billActor = new BillActor(session)
                    {
                        BillActorTypeId = creatorType,
                        PersonId = sales,
                        BillId = bill
                    };
                }
                else
                {
                    BillActor billActor = bill.BillActors.FirstOrDefault(r => r.BillActorTypeId == creatorType);
                    billActor.PersonId = sales;
                }
                session.FlushChanges();

                //Update CHIEFACCOUNTANT bill actor type
                creatorType = BillActorType.GetDefault(session, BillActorTypeEnum.CHIEFACCOUNTANT);
                countExistType = bill.BillActors.Count(r => r.BillActorTypeId == creatorType);
                if (countExistType == 0)
                {
                    BillActor billActor = new BillActor(session)
                    {
                        BillActorTypeId = creatorType,
                        PersonId = chiefAccountant,
                        BillId = bill
                    };
                }
                else
                {
                    BillActor billActor = bill.BillActors.FirstOrDefault(r => r.BillActorTypeId == creatorType);
                    billActor.PersonId = chiefAccountant;
                }
                session.FlushChanges();

                //Update DIRECTOR bill actor type
                creatorType = BillActorType.GetDefault(session, BillActorTypeEnum.DIRECTOR);
                countExistType = bill.BillActors.Count(r => r.BillActorTypeId == creatorType);
                if (countExistType == 0)
                {
                    BillActor billActor = new BillActor(session)
                    {
                        BillActorTypeId = creatorType,
                        PersonId = director,
                        BillId = bill
                    };
                }
                else
                {
                    BillActor billActor = bill.BillActors.FirstOrDefault(r => r.BillActorTypeId == creatorType);
                    billActor.PersonId = director;
                }
                session.FlushChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override Bill GetBillById(DevExpress.Xpo.Session session, Guid billId)
        {
            NAS.DAL.Invoice.SalesInvoice bill =
                    session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId, true);
            return bill;
        }

        protected override void CloneBillData(Session session, Guid billId, ref Bill ret)
        {
            try
            {
                NAS.DAL.Invoice.SalesInvoice source =
                    session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId);

                ret.IssuedDate = source.IssuedDate;
                ret.PromotionCalculationType = source.PromotionCalculationType;
                ret.SourceOrganizationId = source.SourceOrganizationId;
                ret.SumOfItemPrice = source.SumOfItemPrice;
                ret.SumOfPromotion = source.SumOfPromotion;
                ret.SumOfTax = source.SumOfTax;
                ret.TargetOrganizationId = source.TargetOrganizationId;
                ret.TaxCalculationType = source.TaxCalculationType;
                ret.Total = source.Total;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void CloneBillItemData(Session session, Guid billId, ref Bill ret)
        {
            NAS.DAL.Invoice.SalesInvoice source =
                    session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId);

            var sourceBillItemList = source.BillItems.Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE);

            if (sourceBillItemList == null) return;

            foreach (var sourceBillItem in sourceBillItemList)
            {
                //Clone BillItem
                BillItem billItem = new BillItem(session)
                {
                    BillId = ret,
                    Comment = sourceBillItem.Comment,
                    Currency = sourceBillItem.Currency,
                    ItemId = sourceBillItem.ItemId,
                    ItemUnitId = sourceBillItem.ItemUnitId,
                    LotId = sourceBillItem.LotId,
                    Price = sourceBillItem.Price,
                    PromotionInNumber = sourceBillItem.PromotionInNumber,
                    PromotionInPercentage = sourceBillItem.PromotionInPercentage,
                    Quantity = sourceBillItem.Quantity,
                    RowStatus = sourceBillItem.RowStatus,
                    TotalPrice = sourceBillItem.TotalPrice,
                    UnitId = sourceBillItem.UnitId
                };
                billItem.Save();
                //Clone BillItemTax
                foreach (var sourceBillItemTax in sourceBillItem.BillItemTaxs)
                {
                    BillItemTax billItemTax = new BillItemTax(session)
                    {
                        BillItemId = billItem,
                        ItemTaxId = sourceBillItemTax.ItemTaxId,
                        TaxInNumber = sourceBillItemTax.TaxInNumber,
                        TaxInPercentage = sourceBillItemTax.TaxInPercentage
                    };
                    billItemTax.Save();
                }
            }
        }

        protected override void CloneBillActorData(Session session, Guid billId, ref Bill ret)
        {
            NAS.DAL.Invoice.SalesInvoice source =
                    session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId);

            foreach (var sourceBillActor in source.BillActors)
            {
                BillActor billActor = new BillActor(session)
                {
                    BillActorTypeId = sourceBillActor.BillActorTypeId,
                    BillId = ret,
                    OrganizationId = sourceBillActor.OrganizationId,
                    PersonId = sourceBillActor.PersonId
                };
                billActor.Save();
            }
        }

        protected override void CloneBillPromotionData(Session session, Guid billId, ref Bill ret)
        {
            NAS.DAL.Invoice.SalesInvoice source =
                    session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId);
            foreach (var sourceBillPromotion in source.BillPromotions)
            {
                BillPromotion billPromotion = new BillPromotion(session)
                {
                    BillId = ret,
                    PromotionInNumber = sourceBillPromotion.PromotionInNumber,
                    PromotionInPercentage = sourceBillPromotion.PromotionInPercentage,
                    PromotionTypeId = sourceBillPromotion.PromotionTypeId
                };
                billPromotion.Save();
            }
        }

        protected override void CloneBillPlaningAccountingTransactionData(Session session, Guid billId, ref Bill ret)
        {
            //Pending logic here...
        }

        protected override void CloneBillPlaningInventoryTransactionData(Session session, Guid billId, ref Bill ret)
        {
            //Pending logic here...
        }

        protected override void CloneBillTransaction(Session session, Guid billId, ref Bill ret)
        {
            SaleInvoiceTransactionBO invoiceTransactionBO = new SaleInvoiceTransactionBO();
            var transactions = invoiceTransactionBO.GetTransactions(session, billId);
            ObjectBO objectBO = new ObjectBO();
            foreach (var invoiceTransaction in transactions)
            {
                SaleInvoiceTransaction sourceSaleInvoiceTransaction =
                    (SaleInvoiceTransaction)invoiceTransaction;
                SaleInvoiceTransaction saleInvoiceTransaction = new SaleInvoiceTransaction(session)
                {
                    Amount = sourceSaleInvoiceTransaction.Amount,
                    Code = String.Format("BT_{0}", ret.Code),
                    CreateDate = DateTime.Now,
                    Description = String.Format("BT_{0}", ret.Code),
                    IssueDate = sourceSaleInvoiceTransaction.IssueDate,
                    SalesInvoiceId = (NAS.DAL.Invoice.SalesInvoice)ret,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    UpdateDate = DateTime.Now
                };
                saleInvoiceTransaction.Save();
                //Create CMS Object for Transaction
                NAS.DAL.CMS.ObjectDocument.Object transactionCMSObject =
                    objectBO.CreateCMSObject(session, DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_SALE);
                TransactionObject transactionObject = new TransactionObject(session)
                {
                    ObjectId = transactionCMSObject,
                    TransactionId = saleInvoiceTransaction
                };
                transactionObject.Save();
                //Clone GeneralJournal
                var generalJournals = sourceSaleInvoiceTransaction.GeneralJournals
                    .Where(r => r.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE);
                foreach (var sourceGeneralJournal in generalJournals)
                {
                    GeneralJournal generalJournal = new GeneralJournal(session)
                    {
                        AccountId = sourceGeneralJournal.AccountId,
                        CreateDate = DateTime.Now,
                        Credit = sourceGeneralJournal.Credit,
                        CurrencyId = sourceGeneralJournal.CurrencyId,
                        Debit = sourceGeneralJournal.Debit,
                        Description = sourceGeneralJournal.Description,
                        JournalType = sourceGeneralJournal.JournalType,
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                        TransactionId = saleInvoiceTransaction
                    };
                    generalJournal.Save();
                    //Create CMS object for GeneralJournal
                    NAS.DAL.CMS.ObjectDocument.Object generalJournalCMSObject =
                        objectBO.CreateCMSObject(session, DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_SALE);
                    GeneralJournalObject generalJournalObject = new GeneralJournalObject(session)
                    {
                        GeneralJournalId = generalJournal,
                        ObjectId = generalJournalCMSObject
                    };
                    generalJournalObject.Save();
                }
            }
        }
    }
}
