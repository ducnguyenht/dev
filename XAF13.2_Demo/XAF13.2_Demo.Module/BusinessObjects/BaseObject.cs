using DevExpress.Xpo;
using XAF13_2_Demo.Module.Properties;

namespace XAF13_2_Demo.Module.BusinessObjects
{
    [NonPersistent]
    public abstract class BaseObject : XPObject
    {
        protected BaseObject(Session session)
            : base(session)
        {
        }

        [NotifyPropertyChangedInvocator]
        protected new virtual bool SetPropertyValue<T>(string propertyName, ref T propertyValueHolder, T newValue)
        {
            return base.SetPropertyValue(propertyName, ref propertyValueHolder, newValue);
        }
    }
}