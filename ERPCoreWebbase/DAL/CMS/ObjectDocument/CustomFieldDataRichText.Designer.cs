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
namespace NAS.DAL.CMS.ObjectDocument
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class CustomFieldDataRichText : CustomFieldData
    {
        // Fields...
        private byte[] _RichTextData;

        public byte[] RichTextData
        {
            get
            {
                return _RichTextData;
            }
            set
            {
                SetPropertyValue("RichTextData", ref _RichTextData, value);
            }
        }
	}
}