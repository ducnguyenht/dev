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
using NAS.DAL.System.Privileage;
namespace NAS.DAL.Nomenclature.Inventory
{

    public partial class InventoryUnit : XPCustomObject
    {
        Guid fInventoryUnitId;
		[Key(true)]
        public Guid InventoryUnitId
        {
            get { return fInventoryUnitId; }
            set { SetPropertyValue<Guid>("InventoryUnitId", ref fInventoryUnitId, value); }
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

        NAS.DAL.Nomenclature.Organization.Organization fOrganizationId;
        [Association(@"InventoryUnitReferencesOrganization")]
        public NAS.DAL.Nomenclature.Organization.Organization OrganizationId
        {
            get { return fOrganizationId; }
            set { SetPropertyValue<NAS.DAL.Nomenclature.Organization.Organization>("OrganizationId", ref fOrganizationId, value); }
        }
        [Association(@"InventoryReferencesInventoryUnit", typeof(Inventory))]
        public XPCollection<Inventory> Inventorys { get { return GetCollection<Inventory>("Inventorys"); } } 
		
	}

}