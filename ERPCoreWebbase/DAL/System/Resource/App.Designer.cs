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
namespace NAS.DAL.System.Resource
{

    public partial class App : XPCustomObject
    {
        Guid fAppId;
		[Key(true)]
        public Guid AppId
        {
            get { return fAppId; }
            set { SetPropertyValue<Guid>("AppId", ref fAppId, value); }
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
        string fUrl;
        [Size(255)]
        public string Url
        {
            get { return fUrl; }
            set { SetPropertyValue<string>("Url", ref fUrl, value); }
        }
		DateTime fRowCreationTimeStamp;
		public DateTime RowCreationTimeStamp {
			get { return fRowCreationTimeStamp; }
			set { SetPropertyValue<DateTime>("RowCreationTimeStamp", ref fRowCreationTimeStamp, value); }
		}
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        [Association(@"AppComponentReferencesApp", typeof(AppComponent))]
        public XPCollection<AppComponent> AppComponents { get { return GetCollection<AppComponent>("AppComponents"); } }
         
		
	}

}
