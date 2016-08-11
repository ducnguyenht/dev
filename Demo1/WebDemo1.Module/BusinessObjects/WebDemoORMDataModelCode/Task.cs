using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace WebDemo1.Module.BusinessObjects.DBWebDemo
{
    [DefaultClassOptions, ImageName("BO_Task")]
    public partial class Task
    {
        public Task(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}