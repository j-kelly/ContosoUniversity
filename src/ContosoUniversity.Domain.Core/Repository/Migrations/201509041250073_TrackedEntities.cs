namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TrackedEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Department", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Department", "ModifiedBy", c => c.String(nullable: false));
            AddColumn("dbo.Department", "ModifiedOn", c => c.DateTime(nullable: false));
            CreateStoredProcedure(
                "dbo.Department_Insert",
                p => new
                {
                    Name = p.String(maxLength: 50),
                    Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                    StartDate = p.DateTime(),
                    InstructorID = p.Int(),
                    CreatedBy = p.String(),
                    CreatedOn = p.DateTime(),
                    ModifiedBy = p.String(),
                    ModifiedOn = p.DateTime(),
                },
                body:
                    @"INSERT [dbo].[Department]([Name], [Budget], [StartDate], [InstructorID], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn])
                      VALUES (@Name, @Budget, @StartDate, @InstructorID, @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn)

                      DECLARE @DepartmentID int
                      SELECT @DepartmentID = [DepartmentID]
                      FROM [dbo].[Department]
                      WHERE @@ROWCOUNT > 0 AND [DepartmentID] = scope_identity()

                      SELECT t0.[DepartmentID], t0.[RowVersion]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );

            CreateStoredProcedure(
                "dbo.Department_Update",
                p => new
                {
                    DepartmentID = p.Int(),
                    Name = p.String(maxLength: 50),
                    Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                    StartDate = p.DateTime(),
                    InstructorID = p.Int(),
                    RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    CreatedBy = p.String(),
                    CreatedOn = p.DateTime(),
                    ModifiedBy = p.String(),
                    ModifiedOn = p.DateTime(),
                },
                body:
                    @"UPDATE [dbo].[Department]
                      SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [InstructorID] = @InstructorID, [CreatedBy] = @CreatedBy, [CreatedOn] = @CreatedOn, [ModifiedBy] = @ModifiedBy, [ModifiedOn] = @ModifiedOn
                      WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))

                      SELECT t0.[RowVersion]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );

        }

        public override void Down()
        {
            DropColumn("dbo.Department", "ModifiedOn");
            DropColumn("dbo.Department", "ModifiedBy");
            DropColumn("dbo.Department", "CreatedOn");
            DropColumn("dbo.Department", "CreatedBy");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
