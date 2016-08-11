using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;

namespace WebDemo1.Module.BusinessObjects.WebDemoORMDataModelCode
{
    [NavigationItem("Planning")]
    public class ProjectTask : BaseObject
    {
        public ProjectTask(Session session)
            : base(session)
        {
        }

        [Size(255)]
        public string Subject { get; set; }

        public ProjectTaskStatus Status { get; set; }
        public Person AssignedTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Size(SizeAttribute.Unlimited)]
        public string Notes { get; set; }

        [Association]
        public Project Project { get; set; }
    }

    [NavigationItem("Planning")]
    public class Project : BaseObject
    {
        public Project(Session session)
            : base(session)
        {
        }

        public string Name { get; set; }
        public Person Manager { get; set; }

        [Size(SizeAttribute.Unlimited)]
        public string Description { get; set; }

        [Association, Aggregated]
        public XPCollection<ProjectTask> Tasks
        {
            get { return GetCollection<ProjectTask>("Tasks"); }
        }
    }

    public enum ProjectTaskStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Deferred = 3
    }
}