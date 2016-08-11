using System;
using DevExpress.Xpo;

namespace NAS.DAL.System.ArtifactCode
{

    public class ReferenceArtifact : XPCustomObject
    {
        public ReferenceArtifact(Session session)
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
        private CodeRuleDataType _CodeRuleDataTypeId;
        private ArtifactType _ArtifactTypeId;
        private string _Description;
        private string _Name;
        private Guid _ReferenceArtifactId;

        [Key(true)]
        public Guid ReferenceArtifactId
        {
            get
            {
                return _ReferenceArtifactId;
            }
            set
            {
                SetPropertyValue("ReferenceArtifactId", ref _ReferenceArtifactId, value);
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


        [Association(@"ReferenceArtifactsReferencesArtifactType")]
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

        [Association(@"ReferenceArtifactsReferencesCodeRuleDataType")]
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

    }

}