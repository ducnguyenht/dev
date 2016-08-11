using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Inventory.Command.CommanDynamicField;

namespace NAS.DAL.Inventory.Command
{
    public partial class InventoryCommand : XPCustomObject
    {
        public InventoryCommand(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private string _Code;
        [Size(32)]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

        Guid _InventoryCommandId;
        [Key(true)]
        public Guid InventoryCommandId
        {
            get { return _InventoryCommandId; }
            set { SetPropertyValue<Guid>("InventoryCommandId", ref _InventoryCommandId, value); }
        }     

        private char _CommandType;
        public char CommandType
        {
            get { return _CommandType; }
            set { SetPropertyValue<char>("CommandType", ref _CommandType, value); }
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

        private string _Description;
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

        private DateTime _LastUpdateDate;
        public DateTime LastUpdateDate
        {
            get
            {
                return _LastUpdateDate;
            }
            set
            {
                SetPropertyValue("LastUpdateDate", ref _LastUpdateDate, value);
            }
        }

        private string _Name;
        [Size(255)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        NAS.DAL.Nomenclature.Inventory.Inventory _RelevantInventoryId;
        [NonPersistent]
        public NAS.DAL.Nomenclature.Inventory.Inventory RelevantInventoryId
        {
            get
            {
                if (this.InventoryCommandItemTransactions == null || this.InventoryCommandItemTransactions.Count == 0)
                    return NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(Session, Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE);

                IEnumerable<InventoryJournal> journals = this.InventoryCommandItemTransactions.SelectMany(j => j.InventoryJournals).Where(
                    j => (j.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ||
                              j.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) && 
                         (j.Credit > 0 || j.Debit > 0) &&
                         (j.JournalType == 'A' || j.JournalType == 'P'));

                if (journals == null || journals.Count() == 0)
                    return NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(Session, Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE);

                NAS.DAL.Nomenclature.Inventory.Inventory rs = null;

                if (this.CommandType == 'I')
                    rs = journals.Where(j => j.Debit > 0 && j.Credit == 0).Select(j => j.InventoryId).FirstOrDefault();

                else if (this.CommandType == 'O')
                    rs = journals.Where(j => j.Credit > 0 && j.Debit== 0).Select(j => j.InventoryId).FirstOrDefault();

                if (rs == null)
                    return NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(Session, Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE);

                return rs;
            }
            set
            {
                SetPropertyValue("RelevantInventoryId", ref _RelevantInventoryId, value);
            }
        }

        [NonPersistent]
        public NAS.DAL.Nomenclature.Inventory.Inventory CorrespondInventoryId
        {
            get
            {
                if (ParentInventoryCommandId != null && ParentInventoryCommandId.CommandType.Equals('M'))
                    return NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(Session, Nomenclature.Inventory.DefaultInventoryEnum.TRANSITINVENTORY);
                
                if (this.InventoryCommandItemTransactions == null || this.InventoryCommandItemTransactions.Count == 0)
                    return NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(Session, Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE);
                
                return NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(Session, Nomenclature.Inventory.DefaultInventoryEnum.DEFAULTCST);
            }
        }

        InventoryCommand _ParentInventoryCommandId;
        [Association(@"ParentOf")]
        public InventoryCommand ParentInventoryCommandId
        {
            get { return _ParentInventoryCommandId; }
            set { SetPropertyValue<InventoryCommand>("ParentInventoryCommandId", ref _ParentInventoryCommandId, value); }
        }

        [Association(@"ParentOf", typeof(InventoryCommand)), Aggregated]
        public XPCollection<InventoryCommand> InventoryCommands
        {
            get
            {
                return GetCollection<InventoryCommand>("InventoryCommands");
            }
        }

        short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }

        [Association(@"InventoryCommandFinancialTransactionReferencesInventoryCommand", typeof(InventoryCommandFinancialTransaction))]
        public XPCollection<InventoryCommandFinancialTransaction> InventoryCommandFinancialTransactions { 
            get { 
                return GetCollection<InventoryCommandFinancialTransaction>("InventoryCommandFinancialTransactions"); 
        } }

        [Association(@"InventoryCommandItemTransactionReferencesInventoryCommand", typeof(InventoryCommandItemTransaction))]
        public XPCollection<InventoryCommandItemTransaction> InventoryCommandItemTransactions
        {
            get
            {
                return GetCollection<InventoryCommandItemTransaction>("InventoryCommandItemTransactions");
            }
        }
        [Association(@"InventoryCommandReferencesInventoryCommandActor", typeof(InventoryCommandActor)), Aggregated]
        public XPCollection<InventoryCommandActor> InventoryCommandActors
        {
            get
            {
                return GetCollection<InventoryCommandActor>("InventoryCommandActors");
            }
        }

        [Association(@"InventoryCommandObjectReferencesInventoryCommand", typeof(InventoryCommandObject)), Aggregated]
        public XPCollection<InventoryCommandObject> InventoryCommandObjects
        {
            get
            {
                return GetCollection<InventoryCommandObject>("InventoryCommandObjects");
            }
        }

        [Association(@"InventoryCommandCustomTypeReferencesInventoryCommand", typeof(InventoryCommandCustomType)), Aggregated]
        public XPCollection<InventoryCommandCustomType> InventoryCommandCustomTypes
        {
            get
            {
                return GetCollection<InventoryCommandCustomType>("InventoryCommandCustomTypes");
            }
        }
    }
}
