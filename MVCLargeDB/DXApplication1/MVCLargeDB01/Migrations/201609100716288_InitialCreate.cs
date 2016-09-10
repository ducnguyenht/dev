namespace MVCLargeDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.Premises",
                c => new
                    {
                        PremiseID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Whse_No = c.String(nullable: false),
                        Whse_Type = c.String(nullable: false),
                        Description = c.String(),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Post_Code = c.String(nullable: false),
                        CountryID = c.Int(nullable: false),
                        System_Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PremiseID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Premises", "CountryID", "dbo.Countries");
            DropIndex("dbo.Premises", new[] { "CountryID" });
            DropTable("dbo.Premises");
            DropTable("dbo.Countries");
        }
    }
}
