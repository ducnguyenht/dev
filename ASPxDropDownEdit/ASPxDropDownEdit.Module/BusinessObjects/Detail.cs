using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace ASPxDropDownEdit.Module.BusinessObjects
{
    [DefaultClassOptions]
    [Indices("DetailCode","DetailName")]
    public class Detail : BaseObject
    {
        public Detail(Session session) : base(session) { }

        private string _Description;
        private string _DetailCode;
        public string DetailCode
        {
            get { return _DetailCode; }
            set { SetPropertyValue("DetailCode", ref _DetailCode, value); }
        }

        private string _DetailName;
        public string DetailName {
            get { return _DetailName; }
            set { SetPropertyValue("DetailName", ref _DetailName, value); }
        }

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                SetPropertyValue("Description", ref _Description, value);
            }
        }
        private string _DetailCode1;
        public string DetailCode1
        {
            get { return _DetailCode1; }
            set { SetPropertyValue("DetailCode1", ref _DetailCode1, value); }
        }
        private string _DetailCode2;
        public string DetailCode2
        {
            get { return _DetailCode2; }
            set { SetPropertyValue("DetailCode2", ref _DetailCode2, value); }
        }
        private string _DetailCode3;
        public string DetailCode3
        {
            get { return _DetailCode3; }
            set { SetPropertyValue("DetailCode3", ref _DetailCode3, value); }
        }
        
    }
}