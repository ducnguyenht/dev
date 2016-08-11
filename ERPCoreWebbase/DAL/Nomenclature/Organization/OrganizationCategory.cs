using System;
using DevExpress.Xpo;

namespace NAS.DAL.Nomenclature.Organization
{

    public class OrganizationCategory : XPCustomObject
    {

        public OrganizationCategory(Session session)
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
        private TradingCategory _TradingCategoryId;
        private Organization _OrganizationId;
        private short _RowStatus;
        private Guid _OrganizationCategoryId;

        [Key(true)]
        public Guid OrganizationCategoryId
        {
            get
            {
                return _OrganizationCategoryId;
            }
            set
            {
                SetPropertyValue("OrganizationCategoryId", ref _OrganizationCategoryId, value);
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

        [Association(@"OrganizationCategoryReferencesOrganization")]
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

        [Association(@"OrganizationCategoryReferencesTradingCategory")]
        public TradingCategory TradingCategoryId
        {
            get
            {
                return _TradingCategoryId;
            }
            set
            {
                SetPropertyValue("TradingCategoryId", ref _TradingCategoryId, value);
            }
        }

    }

}