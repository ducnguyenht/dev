using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Accounting.Currency;

namespace NAS.DAL.Inventory.Ledger
{
    //public class COGS : InventoryLedger
    //{
    //    double fAmount;
    //    public double Amount
    //    {
    //        get { return fAmount; }
    //        set { SetPropertyValue("Amount", ref fAmount, value); }
    //    }

    //    short fAssumption;
    //    public short Assumption
    //    {
    //        get { return fAssumption; }
    //        set { SetPropertyValue("Assumption", ref fAssumption, value); }
    //    }

    //    double fCOGSPrice;
    //    public double COGSPrice
    //    {
    //        get { return fCOGSPrice; }
    //        set { SetPropertyValue("COGSPrice", ref fCOGSPrice, value); }
    //    }

    //    double fPrice;
    //    public double Price
    //    {
    //        get { return fPrice; }
    //        set { SetPropertyValue("Price", ref fPrice, value); }
    //    }

    //    double fTotal;
    //    public double Total
    //    {
    //        get { return fTotal; }
    //        set { SetPropertyValue("Total", ref fTotal, value); }
    //    }

    //    public COGS(Session session) : base(session) { }
    //    public override void AfterConstruction() { base.AfterConstruction(); }
    //}

    public partial class COGS : XPCustomObject
    {

        public COGS(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction() { base.AfterConstruction(); }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (CurrencyId == null)
            {
                CriteriaOperator criteria = new BinaryOperator("Code", "NAAN_DEFAULT", BinaryOperatorType.Equal);
                Currency currency = this.Session.FindObject<Currency>(criteria);
                CurrencyId = currency;
            }
        }

        Guid _COGSId;
        [Key(true)]
        public Guid COGSId
        {
            get { return _COGSId; }
            set { SetPropertyValue<Guid>("COGSId", ref _COGSId, value); }
        }        
        private InventoryTransaction _InventoryTransactionId;
        private NAS.DAL.Nomenclature.Inventory.Inventory _InventoryId;
        private ItemUnit _ItemUnitId;
        private InventoryJournal _InventoryJournalId;
        private double _Balance;
        private double _Debit;
        private double _Credit;
        private double _Total;
        private string _Description;

        public double Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                SetPropertyValue("Balance", ref _Balance, value);
            }
        }
        public double Total
        {
            get
            {
                return _Total;
            }
            set
            {
                SetPropertyValue("Total", ref _Total, value);
            }
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
        private DateTime _UpdateDate;
        public DateTime UpdateDate
        {
            get
            {
                return _UpdateDate;
            }
            set
            {
                SetPropertyValue("UpdateDate", ref _UpdateDate, value);
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
        double fAmount;
        public double Amount
        {
            get { return fAmount; }
            set { SetPropertyValue("Amount", ref fAmount, value); }
        }
        short fAssumption;
        public short Assumption
        {
            get { return fAssumption; }
            set { SetPropertyValue("Assumption", ref fAssumption, value); }
        }
        double fCOGSPrice;
        public double COGSPrice
        {
            get { return fCOGSPrice; }
            set { SetPropertyValue("COGSPrice", ref fCOGSPrice, value); }
        }
        double fPrice;
        public double Price
        {
            get { return fPrice; }
            set { SetPropertyValue("Price", ref fPrice, value); }
        }
        private DateTime _IssueDate;
        public DateTime IssueDate
        {
            get
            {
                return _IssueDate;
            }
            set
            {
                SetPropertyValue("IssueDate", ref _IssueDate, value);
            }
        }
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

        Currency fCurrencyId;
        [Association("COGSReferencesCurrency")]
        public Currency CurrencyId
        {
            get
            {
                return fCurrencyId;
            }
            set
            {
                SetPropertyValue("CurrencyId", ref fCurrencyId, value);
            }
        }

        [Association("InventoryCOGSReferencesInventoryTransaction")]
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
        [Association("InventoryCOGSReferencesInventory")]
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
        [Association("InventoryCOGSReferencesItemUnit")]
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

        [NonPersistent]
        public bool IsOriginal
        {
            get;
            set;
        }
    }
}
