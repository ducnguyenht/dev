using System;
using DevExpress.Xpo;

namespace NAS.DAL.System.ArtifactCode
{

    public class ArtifactCodeRule : XPCustomObject
    {
        public ArtifactCodeRule(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        // Fields...
        private short _RowStatus;
        private DateTime _IssueDate;
        private DateTime _LastUpdateDate;
        private DateTime _CreateDate;
        private string _Description;
        private string _Name;
        private ArtifactType _ArtifactTypeId;
        private Guid _ArtifactCodeRuleId;

        [Key(true)]
        public Guid ArtifactCodeRuleId
        {
            get
            {
                return _ArtifactCodeRuleId;
            }
            set
            {
                SetPropertyValue("ArtifactCodeRuleId", ref _ArtifactCodeRuleId, value);
            }
        }


        [Association(@"ArtifactCodeRuleReferencesArtifactType")]
        public ArtifactType ArtifactTypeId
        {
            get
            {
                return _ArtifactTypeId;
            }
            set
            {
                SetPropertyValue("ArtifactTypeId", ref _ArtifactTypeId, value);
            }
        }


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


        public short RowStatus
        {
            get
            {
                return _RowStatus;
            }
            set
            {
                SetPropertyValue("RowStatus", ref _RowStatus, value);
            }
        }

        [Association(@"CodeRuleDefinitionsReferencesArtifactCodeRule")]
        public XPCollection<CodeRuleDefinition> CodeRuleDefinitions
        {
            get
            {
                return GetCollection<CodeRuleDefinition>("CodeRuleDefinitions");
            }
        }

    }

}