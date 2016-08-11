using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;

namespace NAS.DAL.Accounting.JournalAllocation
{
    public class GeneralJournalCustomType : XPCustomObject
    {
        public GeneralJournalCustomType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fGeneralJournalCustomTypeId;
        [Key(true)]
        public Guid GeneralJournalCustomTypeId
        {
            get { return fGeneralJournalCustomTypeId; }
            set { SetPropertyValue<Guid>("GeneralJournalCustomTypeId", ref fGeneralJournalCustomTypeId, value); }
        }

        private NAS.DAL.CMS.ObjectDocument.ObjectType fObjectTypeId;
        [Association(@"GeneralJournalCustomTypeReferencesNAS.DAL.CMS.ObjectDocument.ObjectType")]
        public NAS.DAL.CMS.ObjectDocument.ObjectType ObjectTypeId
        {
            get { return fObjectTypeId; }
            set { SetPropertyValue<NAS.DAL.CMS.ObjectDocument.ObjectType>("ObjectTypeId", ref fObjectTypeId, value); }
        }

        private GeneralJournal fGeneralJournalId;
        [Association(@"GeneralJournalCustomTypeReferencesGeneralJournal")]
        public GeneralJournal GeneralJournalId
        {
            get { return fGeneralJournalId; }
            set { SetPropertyValue<GeneralJournal>("GeneralJournalId", ref fGeneralJournalId, value); }
        }
    }
}
