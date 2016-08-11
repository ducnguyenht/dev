using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace WebDemo1.Module.BusinessObjects.DBWebDemo
{
    [DefaultClassOptions, ImageName("BO_Employee")]
    public partial class Employee
    {
        public Employee(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}