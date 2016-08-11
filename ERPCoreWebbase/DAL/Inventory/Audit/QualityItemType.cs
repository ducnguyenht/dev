using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Audit
{
    public class QualityItemType: XPCustomObject
    {
        public QualityItemType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        Guid _QualityItemTypeId;
        [Key(true)]
        public Guid QualityItemTypeId
        {
            get { return _QualityItemTypeId; }
            set { SetPropertyValue<Guid>("QualityItemTypeId", ref _QualityItemTypeId, value); }
        }

        string _Description;
        [Size(1024)]
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue<string>("Description", ref _Description, value); }
        }

        string _Name;
        [Size(255)]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>("Name", ref _Name, value); }
        }

        short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }

        [Association(@"QualityItemReferencesQualityItemType", typeof(QualityItem))]
        public XPCollection<QualityItem> QualityItems { get { return GetCollection<QualityItem>("QualityItems"); } }
    }
}
