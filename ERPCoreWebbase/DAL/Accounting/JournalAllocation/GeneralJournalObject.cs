using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;

namespace NAS.DAL.Accounting.JournalAllocation
{
    public class GeneralJournalObject : XPCustomObject
    {
        public GeneralJournalObject(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fGeneralJournalObjectId;
        [Key(true)]
        public Guid GeneralJournalObjectId
        {
            get { return fGeneralJournalObjectId; }
            set { SetPropertyValue<Guid>("GeneralJournalObjectId", ref fGeneralJournalObjectId, value); }
        }

        private NAS.DAL.CMS.ObjectDocument.Object fObjectId;
        [Association(@"GeneralJournalObjectReferencesNAS.DAL.CMS.ObjectDocument.Object")]
        public NAS.DAL.CMS.ObjectDocument.Object ObjectId
        {
            get { return fObjectId; }
            set { SetPropertyValue<NAS.DAL.CMS.ObjectDocument.Object>("ObjectId", ref fObjectId, value); }
        }

        private GeneralJournal fGeneralJournalId;
        [Association(@"GeneralJournalObjectReferencesGeneralJournal")]
        public GeneralJournal GeneralJournalId
        {
            get { return fGeneralJournalId; }
            set { SetPropertyValue<GeneralJournal>("GeneralJournalId", ref fGeneralJournalId, value); }
        }
    }
}
