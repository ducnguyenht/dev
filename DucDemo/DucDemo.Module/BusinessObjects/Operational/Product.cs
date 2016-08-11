using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace WinWebSolution.Module {
    [DefaultClassOptions]
    public class Product : BaseObject {
        public Product(Session session) : base(session) { }
        private string fName;
        public string Name {
            get { return fName; }
            set { SetPropertyValue("Name", ref fName, value); }
        }
        [Association("Product-Orders")]
        public XPCollection<Order> Orders {
            get { return GetCollection<Order>("Orders"); }
        }
        //You can use ready functions to perform aggregate operations on your collections:  Avg, Count, Exists, Max, Min, Max.
        //Please refer to the "Criteria Language Syntax" help topic (http://www.devexpress.com/Help/?document=XPO/CustomDocument4928.htm) for more information.
        //Note that this variant is shorter than when using the PersistentAliasAttribute.
        //Note that use of non-persistent calculated properties implemented via the PersistentAliasAttribute and Evaluate/EvaluateAlias methods
        //may be inappropriate in certain scenarios, especially when a large number of objects must be manipulated. This is because
        //each time such a property is accessed, a query to the database must be generated to evaluate the property for each master object.

        [Persistent("OrdersTotal")]
        private decimal? fOrdersTotal = null;
        [PersistentAlias("fOrdersTotal")]
        public decimal? OrdersTotal {
            get {
                if(!IsLoading && !IsSaving && fOrdersTotal == null)
                    UpdateOrdersTotal(false);
                return fOrdersTotal;
            }
        }
        //Define a way to calculate and update the OrdersTotal;
        public void UpdateOrdersTotal(bool forceChangeEvents)
        {
            //Put your complex business logic here. Just for demo purposes, we calculate a sum here.
            decimal? oldOrdersTotal = fOrdersTotal;
            decimal tempTotal = 0m;
            //Manually iterate through the Orders collection if your calculated property requires a complex business logic which cannot be expressed via criteria language.
            foreach (Order detail in Orders)
                tempTotal += detail.Total;
            fOrdersTotal = tempTotal;
            if (forceChangeEvents)
                OnChanged("OrdersTotal", oldOrdersTotal, fOrdersTotal);
        }

        private decimal? fMaximumOrder = null;
        public decimal? MaximumOrder {
            get {
                if(!IsLoading && !IsSaving && fMaximumOrder == null)
                    UpdateMaximumOrder(false);
                return fMaximumOrder;
            }
        }
        protected override void OnLoaded() {
            //When using "lazy" calculations it's necessary to reset cached values.
            fOrdersCount = null;
            fOrdersTotal = null;
            fMaximumOrder = null;
            base.OnLoaded();
        }
       
        private int? fOrdersCount = null;
        public int? OrdersCount
        {
            get
            {
                if (!IsLoading && !IsSaving && fOrdersCount == null)
                    UpdateOrdersCount(false);
                return fOrdersCount;
            }
        }

        //Define a way to calculate and update the OrdersCount;
        public void UpdateOrdersCount(bool forceChangeEvents) {
            int? oldOrdersCount = fOrdersCount;
            //This line always evaluates the given expression on the server side so it doesn't take into account uncommitted objects.
            //fOrdersCount = Convert.ToInt32(Session.Evaluate<Product>(CriteriaOperator.Parse("Orders.Count"), CriteriaOperator.Parse("Oid=?", Oid)));
            //This line always evaluates the given expression on the client side using the objects loaded from an internal XPO cache.
            fOrdersCount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("Orders.Count"))); ;
            if (forceChangeEvents)
                OnChanged("OrdersCount", oldOrdersCount, fOrdersCount);
        }
        
        //Define a way you will calculate and update the MaximumOrder;
        public void UpdateMaximumOrder(bool forceChangeEvents) {
            decimal? oldMaximumOrder = fMaximumOrder;
            decimal tempMaximum = 0m;
            //Manually iterate through the Orders collection if your calculated property requires a complex business logic, which cannot be expressed via criteria language.
            foreach (Order detail in Orders)
                //Put your complex business logic here. Just for demo purposes, we calculate a maximum here.
                if (detail.Total > tempMaximum)
                    tempMaximum = detail.Total;
            fMaximumOrder = tempMaximum;
            if (forceChangeEvents)
                OnChanged("MaximumOrder", oldMaximumOrder, fMaximumOrder);
        }
     
    }
}