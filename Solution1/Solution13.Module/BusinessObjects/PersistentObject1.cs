using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;

namespace Solution13.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    public class Person : BaseObject
    {
        public Person(Session session) : base(session) { }

        private string name;
        [RuleRequiredField("RuleRequiredField for Person.Name", DefaultContexts.Save, "Fill in a name.")]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }
    }

    [DefaultClassOptions]
    [DefaultProperty("Number")]
    public class Delivery : BaseObject
    {
        public Delivery(Session session) : base(session) { }

        private int number;
        [RuleRequiredField("RuleRequiredField for Delivery.Number", DefaultContexts.Save, "Fill in a number.")]
        public int Number
        {
            get { return number; }
            set { SetPropertyValue("Number", ref number, value); }
        }
        private DateTime _date;
        public DateTime Date {
            get { return _date; }
            set { SetPropertyValue("Date", ref _date, value); }
        }
        private string carCode;
        [RuleRequiredField("RuleRequiredField for Delivery.CarCode", DefaultContexts.Save, "Fill in a carcode.")]
        public string CarCode
        {
            get { return carCode; }
            set { SetPropertyValue("CarCode", ref carCode, value); }
        }

        private Person carOwner;
        public Person CarOwner
        {
            get { return carOwner; }
            set { SetPropertyValue("CarOwner", ref carOwner, value); }
        }

        public XPCollection<Person> FindPossibleCarOwnersByHistory()
        {
            XPCollection<Person> foundPersons = new XPCollection<Person>(this.Session, false);
            if (this.CarCode.Trim() != "")
            {
                XPCollection<Delivery> foundDeliveries = new XPCollection<Delivery>(this.Session);
                CriteriaOperator crit = new GroupOperator(GroupOperatorType.And,
                 new CriteriaOperator[] {
                             new BinaryOperator("CarCode", this.CarCode.Trim(), BinaryOperatorType.Like),
                             new BinaryOperator("Oid", this.Oid, BinaryOperatorType.NotEqual)
                         });
                foundDeliveries.Criteria = crit;

                foreach (Delivery foundLevering in foundDeliveries)
                {
                    if ((foundLevering.CarOwner != null))
                    {
                        foundPersons.Add(foundLevering.CarOwner);
                    }
                }
            }
            return foundPersons;
        }
    }

}