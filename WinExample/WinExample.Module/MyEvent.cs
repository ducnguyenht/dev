using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo.Metadata;
using System.ComponentModel;
using System.Xml;
using System.Text;

namespace WinExample.Module {
    [DefaultClassOptions]
    public class MyEvent : DevExpress.Persistent.BaseImpl.BaseObject, IRecurrentEvent
    {
#if MediumTrust
		[Persistent("ResourceIds"), Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
		public String resourceIds;
#else
        [Persistent("ResourceIds"), Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        private String resourceIds;
#endif
        private void UpdateResources() {
            Resources.SuspendChangedEvents();
            try {
                while (Resources.Count > 0) {
                    Resources.Remove(Resources[0]);
                }
                if (!String.IsNullOrEmpty(resourceIds)) {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(resourceIds);
                    foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes) {
                        MyUser resource = Session.GetObjectByKey<MyUser>(new Guid(xmlNode.Attributes["Value"].Value));
                        if (resource != null) {
                            Resources.Add(resource);
                        }
                    }
                }
            } finally {
                Resources.ResumeChangedEvents();
            }
        }
        private void Resources_CollectionChanged(object sender, XPCollectionChangedEventArgs e) {
            if ((e.CollectionChangedType == XPCollectionChangedType.AfterAdd) || (e.CollectionChangedType == XPCollectionChangedType.AfterRemove)) {
                UpdateResourceIds();
                OnChanged("ResourceId");
            }
        }
        private void session_ObjectSaving(object sender, ObjectManipulationEventArgs e) {
        }
        protected override XPCollection CreateCollection(XPMemberInfo property) {
            XPCollection result = base.CreateCollection(property);
            if (property.Name == "Resources") {
                result.CollectionChanged += new XPCollectionChangedEventHandler(Resources_CollectionChanged);
            }
            return result;
        }
        public MyEvent(Session session)
            : base(session) {
            session.ObjectSaving += new ObjectManipulationEventHandler(session_ObjectSaving);
        }
        public void UpdateResourceIds() {
            resourceIds = String.Empty;
            Resources.SuspendChangedEvents();
            StringBuilder sb = new StringBuilder();
            try {
                sb.AppendLine("<ResourceIds>");
                foreach (MyUser resource in Resources)
                {
                    sb.AppendFormat(@"<ResourceId Type=""{0}"" Value=""{1}"" />", resource.Id.GetType().FullName, resource.Id);
                }
                sb.AppendLine("</ResourceIds>");
            } finally {
                Resources.ResumeChangedEvents();
            }
            resourceIds = sb.ToString();
        }
        [NonPersistent, Browsable(false)]
        public object AppointmentId {
            get { return Oid; }
        }
        private string _Subject;
        [Size(250)]
        public string Subject {
            get { return _Subject; }
            set {
                SetPropertyValue("Subject", ref _Subject, value);
            }
        }
        private string _Description;
        [Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        public string Description {
            get { return _Description; }
            set {
                SetPropertyValue("Description", ref _Description, value);
            }
        }
        private DateTime _StartOn;
        [Indexed]
        public DateTime StartOn {
            get { return _StartOn; }
            set {
                SetPropertyValue("StartOn", ref _StartOn, value);
            }
        }
        private DateTime _EndOn;
        [Indexed]
        public DateTime EndOn {
            get { return _EndOn; }
            set {
                SetPropertyValue("EndOn", ref _EndOn, value);
            }
        }
        private bool _AllDay;
        public bool AllDay {
            get { return _AllDay; }
            set {
                SetPropertyValue("AllDay", ref _AllDay, value);
            }
        }
        private string _Location;
        public string Location {
            get { return _Location; }
            set {
                SetPropertyValue("Location", ref _Location, value);
            }
        }
        private int _Label;
        public int Label {
            get { return _Label; }
            set {
                SetPropertyValue("Label", ref _Label, value);
            }
        }
        private int _Status;
        public int Status {
            get { return _Status; }
            set {
                SetPropertyValue("Status", ref _Status, value);
            }
        }
        private int _Type;
        public int Type {
            get { return _Type; }
            set {
                SetPropertyValue("Type", ref _Type, value);
            }
        }
        [Association("MyEvent-MyUser", typeof(MyUser))]
        public XPCollection Resources {
            get { return GetCollection("Resources"); }
        }
        [NonPersistent(), Browsable(false)]
        public String ResourceId {
            get {
                if (resourceIds == null) {
                    UpdateResourceIds();
                }
                return resourceIds;
            }
            set {
                if (resourceIds != value) {
                    resourceIds = value;
                    UpdateResources();
                }
            }
        }
        private string recurrenceInfoXml;
        [DevExpress.Xpo.DisplayName("Recurrence"), Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        public string RecurrenceInfoXml {
            get {
                return recurrenceInfoXml;
            }
            set {
                recurrenceInfoXml = value;
                OnChanged("RecurrenceInfoXml");
            }
        }
        private MyEvent recurrencePattern;
        public MyEvent RecurrencePattern {
            get {
                return recurrencePattern;
            }
            set {
                recurrencePattern = (MyEvent)value;
                OnChanged("RecurrencePattern");
            }
        }
        IRecurrentEvent ISupportRecurrences.RecurrencePattern {
            get {
                return RecurrencePattern;
            }
            set {
                RecurrencePattern = (MyEvent)value;
            }
        }

        [NonPersistent]
        [Browsable(false)]
        [RuleFromBoolProperty("MyEventIntervalValid", DefaultContexts.Save, "The start date must be less than the end date", UsedProperties = "StartOn, EndOn")]
        public bool IsIntervalValid { get { return StartOn < EndOn; } }
    }

}
