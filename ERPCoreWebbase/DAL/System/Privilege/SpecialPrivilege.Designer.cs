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
using NAS.DAL.System.Resource;
using NAS.DAL.Nomenclature.Organization;
namespace NAS.DAL.System.Privileage
{

    public partial class SpecialPrivilege : XPCustomObject
    {
        Guid fSpecialPrivilegeId;
		[Key(true)]
        public Guid SpecialPrivilegeId
        {
            get { return fSpecialPrivilegeId; }
            set { SetPropertyValue<Guid>("SpecialPrivilegeId", ref fSpecialPrivilegeId, value); }
		}
        bool fIsAccessable;
        public bool IsAccess
        {
            get { return fIsAccessable; }
            set { SetPropertyValue<bool>("IsAccessable", ref fIsAccessable, value); }
        }
        AppComponentOperation fAppComponentOrganizationId;
        [Association(@"SpecialPrivilegeReferencesAppComponentOperation")]
        public AppComponentOperation AppComponentOrganizationId
        {
            get { return fAppComponentOrganizationId; }
            set { SetPropertyValue<AppComponentOperation>("AppComponentOrganizationId", ref fAppComponentOrganizationId, value); }
        }
        Person fPersonId;
        [Association(@"SpecialPrivilegeReferencesPerson")]
        public Person PersonId
        {
            get { return fPersonId; }
            set { SetPropertyValue<Person>("PersonId", ref fPersonId, value); }
        }
	}

}
