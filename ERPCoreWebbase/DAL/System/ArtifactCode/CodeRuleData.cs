using System;
using DevExpress.Xpo;

namespace NAS.DAL.System.ArtifactCode
{

    public class CodeRuleData : XPCustomObject
    {
        public CodeRuleData(Session session)
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
        private int _LastNumber;
        private DateTime _CreateDate;
        private RuleRepeaterType _RuleRepeaterTypeId;
        private CodeRuleDataFormat _CodeRuleDataFormatId;
        private CodeRuleDefinition _CodeRuleDefinitionId;
        private Guid _CodeRuleDataId;

        [Key(true)]
        public Guid CodeRuleDataId
        {
            get
            {
                return _CodeRuleDataId;
            }
            set
            {
                SetPropertyValue("CodeRuleDataId", ref _CodeRuleDataId, value);
            }
        }

        [Association(@"CodeRuleDataReferencesCodeRuleDefinition")]
        public CodeRuleDefinition CodeRuleDefinitionId
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

        [Association(@"CodeRuleDataReferencesCodeRuleDataFormat")]
        public CodeRuleDataFormat CodeRuleDataFormatId
        {
            get
            {
                return _CodeRuleDataFormatId;
            }
            set
            {
                SetPropertyValue("CodeRuleDataFormatId", ref _CodeRuleDataFormatId, value);
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

        public int LastNumber
        {
            get
            {
                return _LastNumber;
            }
            set
            {
                SetPropertyValue("LastNumber", ref _LastNumber, value);
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

        [NonPersistent]
        public string DataValue { get; set; }

    }

}