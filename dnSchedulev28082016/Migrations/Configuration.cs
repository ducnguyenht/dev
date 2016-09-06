namespace dnSchedulev01.Migrations
{
    using dnSchedulev01.EFCFFDB;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<dnSchedulev01.EFCFFDB.DBScheduleMVCV001>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "dnSchedulev01.EFCFFDB.DBScheduleMVCV001";
        }

        protected override void Seed(dnSchedulev01.EFCFFDB.DBScheduleMVCV001 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Opportunitys.AddOrUpdate(
              p => p.Name,
              new Opportunity { Name = "Opp 1" },
              new Opportunity { Name = "Opp 2" },
              new Opportunity { Name = "Opp 3" }
            );

            context.Customers.AddOrUpdate(
             c => c.Name,
             new Customer { Name = "cus 1" },
             new Customer { Name = "cus 2" },
             new Customer { Name = "cus 3" }
           );
           base.Seed(context);
        }
    }
}
