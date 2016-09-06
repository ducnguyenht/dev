namespace dnSchedulev01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Opportunities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScheduleCalendar",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Status = c.Int(),
                        Subject = c.String(maxLength: 50),
                        Description = c.String(),
                        Label = c.Int(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        Location = c.String(maxLength: 50),
                        AllDay = c.Boolean(nullable: false),
                        EventType = c.Int(),
                        RecurrenceInfo = c.String(),
                        ReminderInfo = c.String(),
                        OpportunityId = c.Int(),
                        CustomerId = c.Int(),
                        RequestedDate = c.DateTime(),
                        RequestBy = c.String(),
                        Price = c.Decimal(storeType: "smallmoney"),
                        ContactInfo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScheduleCalendar");
            DropTable("dbo.Opportunities");
            DropTable("dbo.Customers");
        }
    }
}
