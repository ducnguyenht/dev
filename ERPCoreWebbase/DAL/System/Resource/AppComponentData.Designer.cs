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

    public partial class AppComponentData : NAS.DAL.CMS.ObjectDocument.Object
    {
        // Fields...
        private short _RowStatus;

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
        [Association(@"AppComponetContentReferencesAppComponetData", typeof(AppComponentContent))]
        public XPCollection<AppComponentContent> AppComponentContents { get { return GetCollection<AppComponentContent>("AppComponentContents"); } }
	}

}
