using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.CMS.ObjectDocument
{

    public partial class CustomFieldData
    {
        public CustomFieldData(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        protected override void OnDeleting()
        {
            base.OnDeleting();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }

}
