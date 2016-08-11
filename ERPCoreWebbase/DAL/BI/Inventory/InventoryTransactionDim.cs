using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.BI.Inventory
{
    public class InventoryTransactionDim : XPCustomObject
    {
        public InventoryTransactionDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid _InventoryTransactionDimId;
        [Key(true)]
        public Guid InventoryTransactionDimId
        {
            get { return _InventoryTransactionDimId; }
            set { SetPropertyValue<Guid>("InventoryTransactionDimId", ref _InventoryTransactionDimId, value); }
        }

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue<string>("Code", ref _Code, value); }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>("Name", ref _Name, value); }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue<string>("Description", ref _Description, value); }
        }

        private DateTime _IssueDate;
        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref _IssueDate, value); }
        }

        private short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }

        private Guid _RefId;
        public Guid RefId
        {
            get { return _RefId; }
            set { SetPropertyValue<Guid>("RefId", ref _RefId, value); }
        }
        #endregion

        #region Refereneces
        [Association(@"InventoryEntryDetail-InventoryTransactionDim", typeof(InventoryEntryDetail))]
        public XPCollection<InventoryEntryDetail> InventoryEntryDetails { get { return GetCollection<InventoryEntryDetail>("InventoryEntryDetails"); } }
        #endregion
    }
}
