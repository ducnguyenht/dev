namespace dnSchedulev01.Migrations
{
    using dnSchedulev01.EFCFFDB;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<dnSchedulev01.EFCFFDB.DBScheduleMVCV001>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(dnSchedulev01.EFCFFDB.DBScheduleMVCV001 context)
        {
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

            context.ScheduleTypes.AddOrUpdate(
           c => c.Name,
           new ScheduleType { Name = "Task" },
           new ScheduleType { Name = "Support" },
           new ScheduleType { Name = "Feature" }
         );
            base.Seed(context);
        }
    }
}
