using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Xpo;

namespace WebDemo1.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class PhieuChi : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public PhieuChi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            InternalId = Guid.NewGuid();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }
        // Fields...
        private string _Customer;
        private string _Partner;
        private string _Name;
        private string _Code;
        private Guid _InternalId;

        public Guid InternalId
        {
            get
            {
                return _InternalId;
            }
            set
            {
                SetPropertyValue("InternalId", ref _InternalId, value);
            }
        }

        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        public string Partner
        {
            get
            {
                return _Partner;
            }
            set
            {
                SetPropertyValue("Partner", ref _Partner, value);
            }
        }

        public string Customer
        {
            get
            {
                return _Customer;
            }
            set
            {
                SetPropertyValue("Customer", ref _Customer, value);
            }
        }
        //IObjectSpace os { get; set; }
        [Association("PhieuChi-DinhKhoan")]
        public XPCollection<DinhKhoan> DinhKhoan
        {
            get
            {
                return GetCollection<DinhKhoan>("DinhKhoan");
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            
            base.OnChanged(propertyName, oldValue, newValue);
            IObjectSpace objectSpace = XPObjectSpace.FindObjectSpaceByObject(this);
            if (propertyName=="Partner")
            {
                AcceptedPostingActor AP = new AcceptedPostingActor(Session);
                AP.InternalId = this.InternalId;            
                AP.ActorRefName = this.Partner;
                AP.ActorType = ActorType.Partner;
                AP.Save();
                objectSpace.CommitChanges();
            } 
            if (propertyName=="Customer")
            {
                AcceptedPostingActor AP = new AcceptedPostingActor(Session);
                AP.InternalId = this.InternalId;
                AP.ActorRefName = this.Customer;
                AP.ActorType = ActorType.Customer;
                AP.Save();
                objectSpace.CommitChanges();
            }
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
        //    this.PersistentProperty = "Paid";
        //}
    }
}
