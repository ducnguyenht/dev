namespace CodeFirstNewDatabaseSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Test3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Test3");
        }
    }
}
