using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using System.Drawing;
using System.ComponentModel;
using DevExpress.ExpressApp.Security.Strategy;

namespace WinExample.Module {
    [DefaultClassOptions]
    public class MyUser : SecuritySystemUser, IResource
    {
        public MyUser(Session session) : base(session) { }
        [Association("OrganizationUnit-MyUsers")]
        public OrganizationUnit OrganizationUnit
        {
            get
            {
                return _OrganizationUnit;
            }
            set
            {
                SetPropertyValue("OrganizationUnit", ref _OrganizationUnit, value);
            }
        }
        #if MediumTrust
		[Persistent("Color")]
		[Browsable(false)]
		public Int32 color;
#else
		private OrganizationUnit _OrganizationUnit;
        [Persistent("Color")]
		private Int32 color;
#endif
		public override void AfterConstruction() {
			base.AfterConstruction();
			color = Color.White.ToArgb();
		}
		[NonPersistent, Browsable(false)]
		public object Id {
			get { return Oid; }
		}
		public string Caption {
			get { return UserName; }
			set {
                UserName = value;
				OnChanged("Caption");
			}
		}
		[NonPersistent, Browsable(false)]
		public Int32 OleColor {
			get { return ColorTranslator.ToOle(Color.FromArgb(color)); }
		}
        [Association("MyEvent-MyUser", typeof(MyEvent))]
		public XPCollection Events {
			get { return GetCollection("Events"); }
		}
		[NonPersistent]
		public Color Color {
			get { return Color.FromArgb(color); }
			set {
				color = value.ToArgb();
				OnChanged("Color");
			}
		}
    }

}
