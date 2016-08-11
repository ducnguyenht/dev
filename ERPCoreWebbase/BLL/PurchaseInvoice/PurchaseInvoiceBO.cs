using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Inventory;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Accounting.Tax;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.Journal;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.Accounting.JournalAllocation;
using Utility;

namespace NAS.BO.PurchaseInvoice
{
    public class PurchaseInvoiceBO
    {

        public BillTax BillTaxInit(Session session,
                                            Bill BillId,
                                            Guid BillTaxId,
                                            Tax TaxId,
                                            double TaxInNumber,
                                            double TaxInPercentage
                                            )
        {
            BillTax billTax = session.GetObjectByKey<BillTax>(BillTaxId);

            if (billTax == null)
            {
                billTax = new BillTax(session);
                billTax.BillTaxId = BillTaxId;
            }

            billTax.BillId = BillId;
            billTax.TaxId = TaxId;
            billTax.TaxInNumber = TaxInNumber;
            billTax.TaxInPercentage = TaxInPercentage;

            return billTax;
        }

        public BillItemTax BillItemTaxInit(Session session,
                                            BillItem BillItemId,
                                            Guid BillItemTaxId,
                                            ItemTax ItemTaxId,
                                            double TaxInPercentage,
                                            double TaxInNumber
                                            )
        {
            BillItemTax billItemTax = session.GetObjectByKey<BillItemTax>(BillItemTaxId);

            if (billItemTax == null)
            {
                billItemTax = new BillItemTax(session);
                billItemTax.BillItemTaxId = BillItemTaxId;
            }

            billItemTax.BillItemId = BillItemId;
            billItemTax.TaxInNumber = TaxInNumber;
            billItemTax.TaxInPercentage = TaxInPercentage;
            billItemTax.ItemTaxId = ItemTaxId;

            return billItemTax;
        }

        public void BillTransactionInit(Session session,
                                               Guid BillId,
                                               double TotalAmount,
                                               double DiscountAmount,
                                               double TaxAmount,
                                               double ItemAmount
                                        )
        {

            CriteriaOperator filter = new BinaryOperator("Code", DefaultAccountEnum.NAAN_DEFAULT.ToString(), BinaryOperatorType.Equal);
            Account account = session.FindObject<Account>(filter);

            PurchaseInvoiceTransaction saleInvoiceTransaction = new PurchaseInvoiceTransaction(session);

            saleInvoiceTransaction.TransactionId = Guid.NewGuid();
            saleInvoiceTransaction.CreateDate = saleInvoiceTransaction.IssueDate = DateTime.Now;
            saleInvoiceTransaction.Amount = TotalAmount;
            saleInvoiceTransaction.RowStatus = 0;

            saleInvoiceTransaction.Save();

            ObjectBO objectBO = new ObjectBO();
            NAS.DAL.CMS.ObjectDocument.Object transactionCMSObject =
                objectBO.CreateCMSObject(session,
                    DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_SALE);

            TransactionObject transactionObject = new TransactionObject(session)
            {
                ObjectId = transactionCMSObject,
                TransactionId = saleInvoiceTransaction
            };

            transactionObject.Save();

            // total
            GeneralJournal generalJournal = new GeneralJournal(session);
            generalJournal.GeneralJournalId = Guid.NewGuid();
            generalJournal.TransactionId = session.GetObjectByKey<Transaction>(saleInvoiceTransaction.TransactionId);
            generalJournal.RowStatus = 0;
            generalJournal.JournalType = 'A';

            generalJournal.AccountId = account;
            generalJournal.Debit = TotalAmount;
            generalJournal.Save();

            GeneralJournalObject debitGeneralJournalObject = null;

            NAS.DAL.CMS.ObjectDocument.Object debitJounalCMSObject =
                objectBO.CreateCMSObject(session,
                    DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_SALE);
            debitGeneralJournalObject = new GeneralJournalObject(session)
            {
                GeneralJournalId = generalJournal,
                ObjectId = debitJounalCMSObject
            };

            debitGeneralJournalObject.Save();

            // Discount
            generalJournal = new GeneralJournal(session);
            generalJournal.GeneralJournalId = Guid.NewGuid();
            generalJournal.TransactionId = session.GetObjectByKey<Transaction>(saleInvoiceTransaction.TransactionId);
            generalJournal.RowStatus = 0;
            generalJournal.JournalType = 'A';

            generalJournal.AccountId = account;
            generalJournal.Credit = DiscountAmount;
            generalJournal.Save();

            debitJounalCMSObject =
               objectBO.CreateCMSObject(session,
                   DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_SALE);
            debitGeneralJournalObject = new GeneralJournalObject(session)
            {
                GeneralJournalId = generalJournal,
                ObjectId = debitJounalCMSObject
            };

            debitGeneralJournalObject.Save();

            // Tax

            generalJournal = new GeneralJournal(session);
            generalJournal.GeneralJournalId = Guid.NewGuid();
            generalJournal.TransactionId = session.GetObjectByKey<Transaction>(saleInvoiceTransaction.TransactionId);
            generalJournal.RowStatus = 0;
            generalJournal.JournalType = 'A';

            generalJournal.AccountId = account;
            generalJournal.Credit = DiscountAmount;
            generalJournal.Save();

            debitJounalCMSObject =
             objectBO.CreateCMSObject(session,
                 DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_SALE);
            debitGeneralJournalObject = new GeneralJournalObject(session)
            {
                GeneralJournalId = generalJournal,
                ObjectId = debitJounalCMSObject
            };

            debitGeneralJournalObject.Save();

            // Item
            generalJournal = new GeneralJournal(session);
            generalJournal.GeneralJournalId = Guid.NewGuid();
            generalJournal.TransactionId = session.GetObjectByKey<Transaction>(saleInvoiceTransaction.TransactionId);
            generalJournal.RowStatus = 0;
            generalJournal.JournalType = 'A';

            generalJournal.AccountId = account;
            generalJournal.Credit = ItemAmount;
            generalJournal.Save();

            debitJounalCMSObject =
             objectBO.CreateCMSObject(session,
                 DAL.CMS.ObjectDocument.ObjectTypeEnum.INVOICE_SALE);
            debitGeneralJournalObject = new GeneralJournalObject(session)
            {
                GeneralJournalId = generalJournal,
                ObjectId = debitJounalCMSObject
            };

            debitGeneralJournalObject.Save();
        }


        public NAS.DAL.Invoice.PurchaseInvoice PurchaseInvoiceInit(Session session,
                                                                            Guid BillId,
                                                                            string PurchasingStatus,
                                                                            string Code,
                                                                            DateTime CreateDate,
                                                                            DateTime IssueDate,
                                                                            double SumOfItemPrice,
                                                                            double SumOfPromotion,
                                                                            double SumOfTax,
                                                                            double Total,
                                                                            short RowStatus,
                                                                            string SourceOrganizationId,
                                                                            string TargetOrganizationId,
                                                                            Guid BillTypeId
                                                                   )
        {
            NAS.DAL.Invoice.PurchaseInvoice purchaseInvoice = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(BillId);

            if (purchaseInvoice == null)
            {
                purchaseInvoice = new DAL.Invoice.PurchaseInvoice(session);
            }

            purchaseInvoice.PurchasingStatus = PurchasingStatus;
            purchaseInvoice.BillId = BillId;
            purchaseInvoice.Code = Code;
            purchaseInvoice.CreateDate = CreateDate;
            purchaseInvoice.IssuedDate = IssueDate;
            purchaseInvoice.SumOfItemPrice = SumOfItemPrice;
            purchaseInvoice.SumOfPromotion = SumOfPromotion;
            purchaseInvoice.SumOfTax = SumOfTax;
            purchaseInvoice.Total = Total;
            purchaseInvoice.RowStatus = RowStatus;

            CriteriaOperator filter = new BinaryOperator("Code", SourceOrganizationId, BinaryOperatorType.Equal);
            Organization supplierOrg = session.FindObject<Organization>(filter);

            if (supplierOrg != null)
            {
                purchaseInvoice.SourceOrganizationId = supplierOrg;
            }

            purchaseInvoice.SourceOrganizationId = supplierOrg;

            filter = new BinaryOperator("Code", TargetOrganizationId, BinaryOperatorType.Equal);
            Person person = session.FindObject<Person>(filter);

            if (person != null)
            {
                purchaseInvoice.TargetOrganizationId = person;
            }

            return purchaseInvoice;
        }

        public Bill BillInit(Session session,
                                Guid BillId,
                                string Code,
                                DateTime CreateDate,
                                DateTime IssueDate,
                                float SumOfItemPrice,
                                float SumOfPromotion,
                                float SumOfTax,
                                float Total,                                
                                short RowStatus,
                                Guid SourceOrganizationId,
                                Guid TargetOrganizationId,
                                Guid BillTypeId
                                )
        {


            Bill bill = session.GetObjectByKey<Bill>(BillId);
            if (bill == null)
            {
                bill = new Bill(session);
            }

            bill.BillId = BillId;
            bill.Code = Code;
            bill.CreateDate = CreateDate;
            bill.IssuedDate = IssueDate;
            bill.SumOfItemPrice = SumOfItemPrice;
            bill.SumOfPromotion = SumOfPromotion;
            bill.SumOfTax = SumOfTax;
            bill.Total = Total;
            bill.RowStatus = RowStatus;

            Organization supplierOrg = session.GetObjectByKey<Organization>(SourceOrganizationId);
            bill.SourceOrganizationId = supplierOrg;

            Person person = session.GetObjectByKey<Person>(TargetOrganizationId);
            bill.TargetOrganizationId = person;

            return bill;
        }
      
        public BillPromotion BillPromotionInit(Session session,
                                                Guid BillPromotionId,
                                                double PromotionInPercentage,
                                                double PromotionInNumber,
                                                Guid PromotionTypeId,
                                                Guid BillId                                                
                                               )
        {


            BillPromotion billPromotion = session.GetObjectByKey<BillPromotion>(BillPromotionId);

            if (billPromotion == null)
            {
                billPromotion = new BillPromotion(session);
            }

            billPromotion.BillPromotionId = BillPromotionId;            
            billPromotion.PromotionInPercentage = PromotionInPercentage;
            billPromotion.PromotionInNumber = PromotionInNumber;
            billPromotion.PromotionTypeId = session.GetObjectByKey<PromotionType>(PromotionTypeId);
            billPromotion.BillId = session.GetObjectByKey<Bill>(BillId);

            return billPromotion;
        }


        public BillTax BillTaxInit(Session session,
                                        Guid BillTaxId,
                                        double TaxInPercentage,
                                        double TaxInNumber,
                                        Guid TaxTypeId,
                                        Guid BillId                                        
                                    ) 
        {


            BillTax billTax = session.GetObjectByKey<BillTax>(BillTaxId);

            if (billTax == null)
            {
                billTax = new BillTax(session);
            }

            billTax.BillTaxId = BillTaxId;
            billTax.TaxInPercentage = TaxInPercentage;
            billTax.TaxInNumber = TaxInNumber;
            //billTax.TaxTypeId = session.GetObjectByKey<TaxType>(TaxTypeId);
            billTax.BillId = session.GetObjectByKey<Bill>(BillId);


            return billTax;
        }
        
        public TaxType TaxTypeInit(Session session,
                                        Guid TaxTypeId,
                                        string Code,
                                        DateTime RowCreationTimeStamp,
                                        short RowStatus
                                    )
        {
            TaxType taxType = new TaxType(session);            

            taxType.TaxTypeId = TaxTypeId;
            //----DucVN -----Apply Issue ERP-965---
            //taxType.TaxTypeName = TaxTypeName;
            taxType.Code = Code;
            //----DucVN -----Apply Issue ERP-965---
            taxType.RowCreationTimeStamp = RowCreationTimeStamp;
            taxType.RowStatus = RowStatus;

            return taxType;
        }

        public BillActor BillActorInit(Session session,
                                            Guid BillActorId,
                                            short ActorType,
                                            Guid OrganizationId,
                                            Guid PersonId,
                                            Guid BillId
                                        )
        {
            BillActor billActor = new BillActor(session);
            billActor.BillActorId = BillActorId;
            //billActor.ActorType = ActorType;
            billActor.OrganizationId = session.GetObjectByKey<Organization>(OrganizationId);
            billActor.PersonId = session.GetObjectByKey<Person>(PersonId);
            billActor.BillId = session.GetObjectByKey<Bill>(BillId);

            return billActor;
        }

        /////////////////////////////////////////////////

        public Guid insertEmptyPurchaseInvoice(Session session)
        {
            NAS.DAL.Invoice.PurchaseInvoice purchaseInvoice = new NAS.DAL.Invoice.PurchaseInvoice(session);

            purchaseInvoice.BillId = Guid.NewGuid();
            purchaseInvoice.RowStatus = -1;
            purchaseInvoice.PurchasingStatus = "PO";
            purchaseInvoice.Save();

            XPCollection<BillActorType> collectBillActorType = new XPCollection<BillActorType>(session);
            if (collectBillActorType.Count <= 0)
            {
                BillActorType billActorType = new BillActorType(session);

                billActorType.Description = "Người lập phiếu";
                billActorType.Name = "CREATOR";
                billActorType.RowStatus = Constant.ROWSTATUS_ACTIVE;

                billActorType.Save();

                billActorType = new BillActorType(session);

                billActorType.Description = "Người mua hàng";
                billActorType.Name = "BUYER";
                billActorType.RowStatus = Constant.ROWSTATUS_ACTIVE;

                billActorType.Save();

                billActorType = new BillActorType(session);
                billActorType.Description = "Người bán hàng";
                billActorType.Name = "SALES";
                billActorType.RowStatus = Constant.ROWSTATUS_ACTIVE;

                billActorType.Save();

                billActorType = new BillActorType(session);
                billActorType.Description = "Kế toán trưởng";
                billActorType.Name = "CHIEFACCOUNTANT";
                billActorType.RowStatus = Constant.ROWSTATUS_ACTIVE;

                billActorType.Save();

                billActorType = new BillActorType(session);
                billActorType.Description = "Giám đốc";
                billActorType.Name = "DIRECTOR";
                billActorType.RowStatus = Constant.ROWSTATUS_ACTIVE;

                billActorType.Save();
            }

            collectBillActorType = new XPCollection<BillActorType>(session);
            foreach (BillActorType billActorType in collectBillActorType)
            {
                BillActor billActor = new BillActor(session);
                billActor.BillId = purchaseInvoice;
                billActor.BillActorTypeId = billActorType;
                billActor.Save();
            }
            
            return purchaseInvoice.BillId;
        }

        public void insertPurchaseInvoice(Session session, NAS.DAL.Invoice.PurchaseInvoice purchaseInvoice)
        {

            purchaseInvoice.Save();
        }

        public void updatePurchaseInvoice(Session session, 
                                            NAS.DAL.Invoice.PurchaseInvoice purchaseInvoice,
                                            BillPromotion billPromotionProduct,
                                            BillTax billTaxProduct,
                                            BillPromotion billPromotionService,
                                            BillTax billTaxServce
                                            )
        {
            UnitOfWork uow;

            using (uow = XpoHelper.GetNewUnitOfWork())
            {
                purchaseInvoice.Save();
                billPromotionProduct.Save();
                billTaxProduct.Save();
                billPromotionService.Save();
                billTaxServce.Save();

                uow.CommitChanges();
            }
            
        }

        public void deletePurchaseInvoice(Session session)
        {

        }

    }
}
