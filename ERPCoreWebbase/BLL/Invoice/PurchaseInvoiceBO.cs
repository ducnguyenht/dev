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
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.JournalAllocation;

namespace NAS.BO.Invoice
{
    public class PurchaseInvoiceBO : BillBOBase
    {

        public override bool Delete(DevExpress.Xpo.Session session, Guid billId)
        {
            try
            {
                //Get bill
                NAS.DAL.Invoice.PurchaseInvoice bill =
                    session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);

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

                var invoiceTransactions = bill.PurchaseInvoiceTransactions.Where(r => r.RowStatus >= 0);
                //Mark delete status on bill invoice transactions
                if (invoiceTransactions != null)
                {
                    foreach (var item in invoiceTransactions)
                    {
                        item.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        item.Save();
                    }
                }

                var generalJournals = bill.PurchaseInvoiceTransactions.Where(r => r.RowStatus >= 0)
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

                var inventoryTransactions = bill.PurchaseInvoiceInventoryTransactions.Where(r => r.RowStatus >= 0);
                //Mark delete status on bill invoice inventory transactions
                if (inventoryTransactions != null)
                {
                    foreach (var item in inventoryTransactions)
                    {
                        item.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        item.Save();
                    }
                }

                var inventoryJournals = bill.PurchaseInvoiceInventoryTransactions.Where(r => r.RowStatus >= 0)
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
                NAS.DAL.Invoice.PurchaseInvoice ret = null;
                ret = new DAL.Invoice.PurchaseInvoice(session)
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
                NAS.DAL.Invoice.PurchaseInvoice bill =
                    session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);
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

                var actualPurchaseInvoiceTransactions = bill.PurchaseInvoiceTransactions;

                actualPurchaseInvoiceTransactions.Criteria = criteria;

                if (actualPurchaseInvoiceTransactions == null
                    || actualPurchaseInvoiceTransactions.Count == 0)
                {
                    PurchaseInvoiceTransaction purchaseInvoiceTransaction
                        = new PurchaseInvoiceTransaction(session)
                        {
                            Code = "BT_" + bill.Code,
                            CreateDate = DateTime.Now,
                            Description = "BT_" + bill.Code,
                            IssueDate = issuedDate,
                            RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                            UpdateDate = DateTime.Now,
                            PurchaseInvoiceId = bill
                        };
                    purchaseInvoiceTransaction.Save();

                    ObjectBO objectBO = new ObjectBO();
                    NAS.DAL.CMS.ObjectDocument.Object cmsObject =
                        objectBO.CreateCMSObject(session, DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_PURCHASE);

                    TransactionObject transactionObject = new TransactionObject(session)
                    {
                        ObjectId = cmsObject,
                        TransactionId = purchaseInvoiceTransaction
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
            Guid buyerPersonId, Guid chiefAccountantPersonId, Guid directorPersonId)
        {
            try
            {
                //Get bill
                Bill bill = session.GetObjectByKey<Bill>(billId);
                if (bill == null)
                    throw new Exception("Could not foung bill");

                //Get creator
                Person creator = session.GetObjectByKey<Person>(creatorPersonId);
                Person buyer = session.GetObjectByKey<Person>(buyerPersonId);
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

                //Update BUYER bill actor type
                creatorType = BillActorType.GetDefault(session, BillActorTypeEnum.BUYER);
                countExistType = bill.BillActors.Count(r => r.BillActorTypeId == creatorType);
                if (countExistType == 0)
                {
                    BillActor billActor = new BillActor(session)
                    {
                        BillActorTypeId = creatorType,
                        PersonId = buyer,
                        BillId = bill
                    };
                }
                else
                {
                    BillActor billActor = bill.BillActors.FirstOrDefault(r => r.BillActorTypeId == creatorType);
                    billActor.PersonId = buyer;
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
            NAS.DAL.Invoice.PurchaseInvoice bill =
                    session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId, true);
            return bill;
        }

        protected override void CloneBillData(Session session, Guid billId, ref Bill ret)
        {
            try
            {
                NAS.DAL.Invoice.PurchaseInvoice source =
                    session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);

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
            NAS.DAL.Invoice.PurchaseInvoice source =
                    session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);

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
            NAS.DAL.Invoice.PurchaseInvoice source =
                    session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);

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
            NAS.DAL.Invoice.PurchaseInvoice source =
                    session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);
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
            PurchaseInvoiceTransactionBO invoiceTransactionBO = new PurchaseInvoiceTransactionBO();
            var transactions = invoiceTransactionBO.GetTransactions(session, billId);
            ObjectBO objectBO = new ObjectBO();
            foreach (var invoiceTransaction in transactions)
            {
                PurchaseInvoiceTransaction sourcePurchaseInvoiceTransaction = 
                    (PurchaseInvoiceTransaction)invoiceTransaction;
                PurchaseInvoiceTransaction purchaseInvoiceTransaction = new PurchaseInvoiceTransaction(session)
                {
                    Amount = sourcePurchaseInvoiceTransaction.Amount,
                    Code = String.Format("BT_{0}", ret.Code),
                    CreateDate = DateTime.Now,
                    Description = String.Format("BT_{0}", ret.Code),
                    IssueDate = sourcePurchaseInvoiceTransaction.IssueDate,
                    PurchaseInvoiceId = (NAS.DAL.Invoice.PurchaseInvoice)ret,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    UpdateDate = DateTime.Now                    
                };
                purchaseInvoiceTransaction.Save();
                //Create CMS Object for Transaction
                NAS.DAL.CMS.ObjectDocument.Object transactionCMSObject =
                    objectBO.CreateCMSObject(session, DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_PURCHASE);
                TransactionObject transactionObject = new TransactionObject(session)
                {
                    ObjectId = transactionCMSObject,
                    TransactionId = purchaseInvoiceTransaction
                };
                transactionObject.Save();
                //Clone GeneralJournal
                var generalJournals = sourcePurchaseInvoiceTransaction.GeneralJournals
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
                        TransactionId = purchaseInvoiceTransaction
                    };
                    generalJournal.Save();
                    //Create CMS object for GeneralJournal
                    NAS.DAL.CMS.ObjectDocument.Object generalJournalCMSObject =
                        objectBO.CreateCMSObject(session, DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_PURCHASE);
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
