using System;
using DevExpress.Xpo;

namespace NAS.DAL.System.ArtifactCode
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class CodeRuleNumberData : CodeRuleData
    {
        public CodeRuleNumberData(Session session)
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
        private RuleRepeaterType _RuleRepeaterTypeId;
        private int _Step;
        private int _EndNumberValue;
        private int _BeginNumberValue;

        public int BeginNumberValue
        {
            get
            {
                return _BeginNumberValue;
            }
            set
            {
                SetPropertyValue("BeginNumberValue", ref _BeginNumberValue, value);
            }
        }

        public int EndNumberValue
        {
            get
            {
                return _EndNumberValue;
            }
            set
            {
                SetPropertyValue("EndNumberValue", ref _EndNumberValue, value);
            }
        }

        public int Step
        {
            get
            {
                return _Step;
            }
            set
            {
                SetPropertyValue("Step", ref _Step, value);
            }
        }

        [Association(@"CodeRuleNumberDataReferencesRuleRepeaterType")]
        public RuleRepeaterType RuleRepeaterTypeId
        {
            get
            {
                return _RuleRepeaterTypeId;
            }
            set
            {
                SetPropertyValue("RuleRepeaterTypeId", ref _RuleRepeaterTypeId, value);
            }
        }
    }

}