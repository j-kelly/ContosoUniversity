namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SoftDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "IsDeleted");
        }
    }
}
