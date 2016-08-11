using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Item;

namespace NAS.DAL.Inventory.Lot
{
    public enum DefaultLotEnum
    {
        NOT_AVAILABLE
    }

    public class Lot :XPCustomObject
    {
        Guid fLotId;
        [Key(true)]
        public Guid LotId
        {
            get { return fLotId; }
            set { SetPropertyValue<Guid>("LotId", ref fLotId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        DateTime fExpireDate;
        public DateTime ExpireDate
        {
            get { return fExpireDate; }
            set { SetPropertyValue<DateTime>("ExpireDate", ref fExpireDate, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        Lot fParentLotId;
        [Association(@"ParentOf")]
        public Lot ParentLotId
        {
            get { return fParentLotId; }
            set { SetPropertyValue<Lot>("ParentLotId", ref fParentLotId, value); }
        }

        [Association(@"ParentOf", typeof(Lot))]
        public XPCollection<Lot> Lots
        {
            get { return GetCollection<Lot>("Lots");}
        }

        [Association(@"BillItemReferencesLot", typeof(BillItem))]
        public XPCollection<BillItem> BillItems
        {
            get { return GetCollection<BillItem>("BillItems"); }
        }

        NAS.DAL.Nomenclature.Item.Item fItemId;
        [Association(@"LotReferencesItem")]
        public NAS.DAL.Nomenclature.Item.Item ItemId
        {
            get { return fItemId; }
            set { SetPropertyValue<NAS.DAL.Nomenclature.Item.Item>("ItemId", ref fItemId, value); }
        }

        [Association("InventoryJournalReferencesLot", typeof(NAS.DAL.Inventory.Journal.InventoryJournal))]
        public XPCollection<NAS.DAL.Inventory.Journal.InventoryJournal> InventoryJournals
        {
            get { return GetCollection<NAS.DAL.Inventory.Journal.InventoryJournal>("InventoryJournals"); }
        }

        [Association(@"InventoryLedgerReferencesLot", typeof(NAS.DAL.Inventory.Ledger.InventoryLedger))]
        public XPCollection<NAS.DAL.Inventory.Ledger.InventoryLedger> InventoryLedgers
        {
            get { return GetCollection<Inventory.Ledger.InventoryLedger>("InventoryLedgers"); }
        }

        public Lot(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Lot table
                if (!Util.isExistXpoObject<NAS.DAL.Inventory.Lot.Lot>("Code", Utility.Constant.NAAN_DEFAULT_NOTAVAILABLE))
                {
                    NAS.DAL.Inventory.Lot.Lot lot =
                        new NAS.DAL.Inventory.Lot.Lot(session)
                        {
                            Code = Utility.Constant.NAAN_DEFAULT_NOTAVAILABLE,
                            ExpireDate = DateTime.MaxValue,
                            RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                        };
                    lot.Save();
                }
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public static Lot GetDefault(Session session, DefaultLotEnum code)
        {
            try
            {
                if (Enum.GetName(typeof(DefaultLotEnum), code).Equals("NOT_AVAILABLE"))
                    return session.FindObject<Lot>(
                    new BinaryOperator("Code", "N/A"));

                return session.FindObject<Lot>(
                    new BinaryOperator("Code", Enum.GetName(typeof(DefaultLotEnum), code)));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
