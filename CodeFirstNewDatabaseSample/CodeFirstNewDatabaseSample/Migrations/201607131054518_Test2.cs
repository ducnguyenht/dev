namespace CodeFirstNewDatabaseSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Test2", c => c.String());
            AddColumn("dbo.Posts", "Test2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Test2");
            DropColumn("dbo.Blogs", "Test2");
        }
    }
}
