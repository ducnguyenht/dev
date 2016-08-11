using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Nomenclature.Organization
{
    public class OrganizationObject : XPCustomObject
    {
        public OrganizationObject(Session session)
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
        private NAS.DAL.CMS.ObjectDocument.Object _ObjectId;
        private Guid _OrganizationObjectId;

        [Key(true)]
        public Guid OrganizationObjectId
        {
            get
            {
                return _OrganizationObjectId;
            }
            set
            {
                SetPropertyValue("OrganizationObjectId", ref _OrganizationObjectId, value);
            }
        }

        [Association("OrganizationObjectReferencesObject")]
        public NAS.DAL.CMS.ObjectDocument.Object ObjectId
        {
            get
            {
                return _ObjectId;
            }
            set
            {
                SetPropertyValue("ObjectId", ref _ObjectId, value);
            }
        }


        [Association("OrganizationObjectReferencesOrganization")]
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
