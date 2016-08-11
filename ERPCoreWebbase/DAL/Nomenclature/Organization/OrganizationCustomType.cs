using System;
using DevExpress.Xpo;

namespace NAS.DAL.Nomenclature.Organization
{

    public class OrganizationCustomType : XPCustomObject
    {
        public OrganizationCustomType(Session session)
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
        private Organization _OrganizationId;
        private NAS.DAL.CMS.ObjectDocument.ObjectType _ObjectTypeId;
        private Guid _OrganizationCustomTypeId;
        [Key(true)]
        public Guid OrganizationCustomTypeId
        {
            get
            {
                return _OrganizationCustomTypeId;
            }
            set
            {
                SetPropertyValue("OrganizationCustomTypeId", ref _OrganizationCustomTypeId, value);
            }
        }


        [Association("OrganizationCustomTypeReferencesObjectType")]
        public NAS.DAL.CMS.ObjectDocument.ObjectType ObjectTypeId
        {
            get
            {
                return _ObjectTypeId;
            }
            set
            {
                SetPropertyValue("ObjectTypeId", ref _ObjectTypeId, value);
            }
        }


        [Association("OrganizationCustomTypeReferencesOrganization")]
        public Organization OrganizationId
        {
            get
            {
                return _OrganizationId;
            }
            set
            {
                SetPropertyValue("OrganizationId", ref _OrganizationId, value);
            }
        }
    }

}