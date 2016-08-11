using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Inventory.Command;
using NAS.DAL.Accounting.Currency;
namespace NAS.DAL.Inventory.Journal
{

    public partial class InventoryJournal : XPCustomObject
    {
        public InventoryJournal(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private InventoryTransaction _InventoryTransactionId;
        private ItemUnit _ItemUnitId;
        private double _Debit;
        private double _Credit;

        Guid fInventoryJournalId;
        [Key(true)]
        public Guid InventoryJournalId
        {
            get { return fInventoryJournalId; }
            set { SetPropertyValue<Guid>("InventoryJournalId", ref fInventoryJournalId, value); }
        }
        public double Credit
        {
            get
            {
                return _Credit;
            }
            set
            {
                SetPropertyValue("Credit", ref _Credit, value);
            }
        }
        public double Debit
        {
            get
            {
                return _Debit;
            }
            set
            {
                SetPropertyValue("Debit", ref _Debit, value);
            }
        }
        private DateTime _CreateDate;
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }
        string _Description;
        [Size(512)]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                SetPropertyValue("Description", ref _Description, value);
            }
        }
        char fJournalType;        
        public char JournalType
        {
            get
            {
                return fJournalType;
            }
            set
            {
                SetPropertyValue("JournalType", ref fJournalType, value);
            }
        }
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        NAS.DAL.Nomenclature.Inventory.Inventory _InventoryId;
        [Association("InventoryJournalReferencesInventory")]
        public NAS.DAL.Nomenclature.Inventory.Inventory InventoryId
        {
            get
            {
                return _InventoryId;
            }
            set
            {
                SetPropertyValue("InventoryId", ref _InventoryId, value);
            }
        }
        [Association("InventoryJournalReferencesItemUnit")]
        public ItemUnit ItemUnitId
        {
            get
            {
                return _ItemUnitId;
            }
            set
            {
                SetPropertyValue("ItemUnitId", ref _ItemUnitId, value);
            }
        }

        [Association("InventoryJournalReferencesInventoryTransaction")]
        public InventoryTransaction InventoryTransactionId
        {
            get
            {
                return _InventoryTransactionId;
            }
            set
            {
                SetPropertyValue("InventoryTransactionId", ref _InventoryTransactionId, value);
            }
        }
        private NAS.DAL.Inventory.Lot.Lot _LotId;
        [Association("InventoryJournalReferencesLot")]
        public NAS.DAL.Inventory.Lot.Lot LotId
        {
            get
            {
                return _LotId;
            }
            set
            {
                SetPropertyValue("LotId", ref _LotId, value);
            }
        }

        private NAS.DAL.Accounting.AccountChart.Account _AccountId;
        [Association("InventoryJournalReferencesAccount")]
        public NAS.DAL.Accounting.AccountChart.Account AccountId
        {
            get
            {
                return _AccountId;
            }
            set
            {
                SetPropertyValue("AccountId", ref _AccountId, value);
            }
        }

        [Association("InventoryJournalObjectReferenceInventoryJournal", typeof(InventoryJournalObject)), Aggregated]
        public XPCollection<InventoryJournalObject> InventoryJournalObjects
        {
            get
            {
                return GetCollection<InventoryJournalObject>("InventoryJournalObjects");
            }
        }

        [Association("InventoryJournalCustomTypeReferencesInventoryJournal", typeof(InventoryJournalCustomType)), Aggregated]
        public XPCollection<InventoryJournalCustomType> InventoryJournalCustomTypes
        {
            get
            {
                return GetCollection<InventoryJournalCustomType>("InventoryJournalCustomTypes");
            }
        }

        [Association("InventoryJournalFinancialReferenceInventoryJournal")]
        public XPCollection<InventoryJournalFinancial> InventoryJournalFinancials
        {
            get
            {
                return GetCollection<InventoryJournalFinancial>("InventoryJournalFinancials");
            }
        }

        NAS.DAL.Nomenclature.Inventory.Inventory _FromInventoryId;
        [NonPersistent]
        public NAS.DAL.Nomenclature.Inventory.Inventory FromInventoryId
        {
            get
            {
                if (Credit > 0 && Debit == 0)
                {
                    return InventoryId;
                }

                return null;
            }
            set
            {
                SetPropertyValue("FromInventoryId", ref _FromInventoryId, value);
            }
        }

        NAS.DAL.Nomenclature.Inventory.Inventory _ToInventoryId;
        [NonPersistent]
        public NAS.DAL.Nomenclature.Inventory.Inventory ToInventoryId
        {
            get
            {
                InventoryJournal tmp = null;
                if (Credit > 0 && Debit == 0)
                {
                    XPCollection<InventoryJournal> IJLst = new XPCollection<InventoryJournal>(Session,
                        new BinaryOperator("InventoryTransactionId", InventoryTransactionId, BinaryOperatorType.Equal));
                    foreach (InventoryJournal ij in IJLst)
                    {
                        if (ij.InventoryTransactionId == InventoryTransactionId && 
                            ij.InventoryJournalId != InventoryJournalId && 
                            ij.Debit == Credit && ij.ItemUnitId == ItemUnitId && 
                            ij.LotId == LotId && ij.JournalType == 'A')
                        {
                            tmp = ij;
                        }
                    }

                    if (tmp != null)
                    {
                        return tmp.InventoryId;
                    }
                    return null;
                }

                return null;
            }
            set
            {
                SetPropertyValue("ToInventoryId", ref _ToInventoryId, value);
            }
        }

        private double _PlanCredit;
        [NonPersistent]
        public double PlanCredit
        {
            get
            {
                InventoryJournal tmp = null;
                if (Credit > 0 && Debit == 0 && JournalType == 'A')
                {
                    XPCollection<InventoryJournal> IJLst = new XPCollection<InventoryJournal>(Session,
                        new BinaryOperator("InventoryTransactionId", InventoryTransactionId, BinaryOperatorType.Equal));
                    foreach (InventoryJournal ij in IJLst)
                    {
                        if (ij.InventoryTransactionId == InventoryTransactionId &&
                            ij.InventoryJournalId != InventoryJournalId &&
                            ij.Credit > 0 && ij.ItemUnitId == ItemUnitId &&
                            ij.JournalType == 'P' &&
                            ij.AccountId == AccountId)
                        {
                            tmp = ij;
                        }
                    }

                    if (tmp != null)
                    {
                        return tmp.Credit;
                    }
                    return 0;
                }

                return 0;
            }
            set
            {
                SetPropertyValue("PlanCredit", ref _PlanCredit, value);
            }
        }
    }

}
