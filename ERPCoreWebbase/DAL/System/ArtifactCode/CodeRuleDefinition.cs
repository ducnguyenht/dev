using System;
using DevExpress.Xpo;

namespace NAS.DAL.System.ArtifactCode
{

    public class CodeRuleDefinition : XPCustomObject
    {
        public CodeRuleDefinition(Session session)
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
        private CodeRuleDefinition _ParentCodeRuleDefinitionId;
        private CodeRuleDataType _CodeRuleDataTypeId;
        private ArtifactCodeRule _ArtifactCodeRuleId;
        private short _RowStatus;
        private Guid _CodeRuleDefinitionId;

        [Key(true)]
        public Guid CodeRuleDefinitionId
        {
            get
            {
                return _CodeRuleDefinitionId;
            }
            set
            {
                SetPropertyValue("CodeRuleDefinitionId", ref _CodeRuleDefinitionId, value);
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
        public ArtifactCodeRule ArtifactCodeRuleId
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


        [Association(@"CodeRuleDefinitionsReferencesCodeRuleDataType")]
        public CodeRuleDataType CodeRuleDataTypeId
        {
            get
            {
                return _CodeRuleDataTypeId;
            }
            set
            {
                SetPropertyValue("CodeRuleDataTypeId", ref _CodeRuleDataTypeId, value);
            }
        }

        [Association(@"CodeRuleDataReferencesCodeRuleDefinition")]
        public XPCollection<CodeRuleData> CodeRuleData
        {
            get
            {
                return GetCollection<CodeRuleData>("CodeRuleData");
            }
        }

        [Association(@"CodeRuleDefinitionReferencesParentCodeRuleDefinition")]
        public CodeRuleDefinition ParentCodeRuleDefinitionId
        {
            get
            {
                return _ParentCodeRuleDefinitionId;
            }
            set
            {
                SetPropertyValue("ParentCodeRuleDefinitionId", ref _ParentCodeRuleDefinitionId, value);
            }
        }

        [Association(@"CodeRuleDefinitionReferencesParentCodeRuleDefinition")]
        public XPCollection<CodeRuleDefinition> CodeRuleDefinitions
        {
            get
            {
                return GetCollection<CodeRuleDefinition>("CodeRuleDefinitions");
            }
        }

    }

}