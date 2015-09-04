namespace ContosoUniversity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AuditProperttTrial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditPropertyTrail",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    EntityId = c.Int(nullable: false),
                    EntityType = c.String(nullable: false, maxLength: 128),
                    ModifiedBy = c.String(nullable: false, maxLength: 128),
                    ModifiedDate = c.DateTime(nullable: false),
                    NewValue = c.String(nullable: false, maxLength: 128),
                    OldValue = c.String(nullable: false, maxLength: 128),
                    PropertyName = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.AuditPropertyTrail");
        }
    }
}
