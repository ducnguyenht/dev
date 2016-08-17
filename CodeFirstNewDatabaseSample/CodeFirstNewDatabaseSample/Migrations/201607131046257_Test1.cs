namespace CodeFirstNewDatabaseSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Test1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Test1");
        }
    }
}
