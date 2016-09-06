using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace dnSchedulev01.EFCFFDB
{
    public class DBInitializer : DropCreateDatabaseAlways<DBScheduleMVCV001>
    {
        protected override void Seed(DBScheduleMVCV001 context)
        {
            var lstOpp = new List<Opportunity>();
            lstOpp.Add(new Opportunity() { Name = "Opp 1"});
            lstOpp.Add(new Opportunity() { Name = "Opp 2" });
            lstOpp.Add(new Opportunity() { Name = "Opp 3"});
            foreach (var obj in lstOpp)
                context.Opportunitys.Add(obj);

            var lstCus = new List<Customer>();
            lstCus.Add(new Customer() { Name = "cus 1" });
            lstCus.Add(new Customer() { Name = "cus 2" });
            lstCus.Add(new Customer() { Name = "cus 3" });
            foreach (var obj in lstCus)
                context.Customers.Add(obj);
            base.Seed(context);
        }
    }
}