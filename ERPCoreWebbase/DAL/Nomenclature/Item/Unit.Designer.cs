//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.LegalInvoice;
namespace NAS.DAL.Nomenclature.Item
{

    public partial class Unit : XPCustomObject
    {
        Guid fUnitId;
        [Key(true)]
        public Guid UnitId
        {
            get { return fUnitId; }
            set { SetPropertyValue<Guid>("UnitId", ref fUnitId, value); }
        }
        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        DateTime fRowCreationTimeStamp;
        public DateTime RowCreationTimeStamp
        {
            get { return fRowCreationTimeStamp; }
            set { SetPropertyValue<DateTime>("RowCreationTimeStamp", ref fRowCreationTimeStamp, value); }
        }
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        #region References

        [Association(@"ItemUnitReferencesUnit", typeof(ItemUnit))]
        public XPCollection<ItemUnit> ItemUnits { get { return GetCollection<ItemUnit>("ItemUnits"); } }

        private NAS.DAL.Nomenclature.UnitItem.UnitType fUnitTypeId;
        [Association("UnitReferencesNAS.DAL.Nomenclature.UnitItem.UnitType")]
        public NAS.DAL.Nomenclature.UnitItem.UnitType UnitTypeId
        {
            get { return fUnitTypeId; }
            set { SetPropertyValue<NAS.DAL.Nomenclature.UnitItem.UnitType>("UnitTypeId", ref fUnitTypeId, value); }
        }

        private NAS.DAL.Nomenclature.Organization.Organization fOrganizationId;
        [Association("UnitReferencesNAS.DAL.Nomenclature.Organization.Organization")]
        public NAS.DAL.Nomenclature.Organization.Organization OrganizationId
        {
            get { return fOrganizationId; }
            set { SetPropertyValue<NAS.DAL.Nomenclature.Organization.Organization>("OrganizationId", ref fOrganizationId, value); }
        }

        [Association(@"LegalInvoiceArtifactDetailReferencesUnit", typeof(LegalInvoiceArtifactDetail))]
        public XPCollection<LegalInvoiceArtifactDetail> legalInvoiceArtifactDetails { get { return GetCollection<LegalInvoiceArtifactDetail>("legalInvoiceArtifactDetails"); } }
        #endregion

    }
}