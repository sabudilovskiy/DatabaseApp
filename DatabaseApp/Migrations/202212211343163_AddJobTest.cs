namespace DatabaseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "test");
        }
    }
}
