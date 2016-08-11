using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo.Updating;

namespace ASPxDropDownEdit.Module.BusinessObjects
{
    [DefaultClassOptions]
    [Indices("MasterName")]
    public class Master : BaseObject
    {
        public Master(Session session) : base(session) { }
        private string _MasterName;
        public string MasterName
        {
            get { return _MasterName; }
            set { SetPropertyValue("MasterName", ref _MasterName, value); }
        }

        private Detail _Detail;
        public Detail Detail
        {
            get { return _Detail; }
            set { SetPropertyValue("Detail", ref _Detail, value); }
        }


    }
}