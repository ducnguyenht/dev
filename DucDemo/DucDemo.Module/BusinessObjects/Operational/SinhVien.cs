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

namespace DucDemo.Module.BusinessObjects.Operational
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("DisplayName")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    [Indices("Id", "Name", "DisplayName")]
     public class SinhVien : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
      
        public SinhVien(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }
        // Fields...
   
        private Lop _Lop;
        private string _Name;
        private string _Id;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleUniqueValue(DefaultContexts.Save)]
        //[Indices]

        public string DisplayName2
        {
            get { return Id + ": " + Name; }
        }

        [VisibleInDetailView(false)]
        [Persistent]
        public string DisplayName
        {
            get { return Id +": "+Name; }    
        }

        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                SetPropertyValue("Id", ref _Id, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
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
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        [Association("Lop-SinhViens")]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
            }
        }
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        [Association("SinhVien-BangDiems")]
        public XPCollection<BangDiem> BangDiems
        {
            get
            {
                return GetCollection<BangDiem>("BangDiems");
            }
        }
        protected override void OnSaving()
        {

            base.OnSaving();
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
