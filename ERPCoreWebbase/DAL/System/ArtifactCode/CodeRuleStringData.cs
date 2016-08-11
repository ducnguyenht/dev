using System;
using DevExpress.Xpo;

namespace NAS.DAL.System.ArtifactCode
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class CodeRuleStringData : CodeRuleData
    {
        public CodeRuleStringData(Session session)
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
        private string _StringValue;

        public string StringValue
        {
            get
            {
                return _StringValue;
            }
            set
            {
                SetPropertyValue("StringValue", ref _StringValue, value);
            }
        }
    }

}