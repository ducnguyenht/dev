namespace CodeFirstNewDatabaseSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Test4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Test4");
        }
    }
}
