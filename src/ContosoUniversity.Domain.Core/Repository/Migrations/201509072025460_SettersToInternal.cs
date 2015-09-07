namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SettersToInternal : DbMigration
    {
        public override void Up()
        {
            AlterStoredProcedure(
                "dbo.Department_Insert",
                p => new
                {
                    Name = p.String(maxLength: 50),
                    Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                    StartDate = p.DateTime(),
                    InstructorID = p.Int(),
                    CreatedBy = p.String(),
                    ModifiedBy = p.String(),
                    CreatedOn = p.DateTime(),
                    ModifiedOn = p.DateTime(),
                },
                body:
                    @"INSERT [dbo].[Department]([Name], [Budget], [StartDate], [InstructorID], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn])
                      VALUES (@Name, @Budget, @StartDate, @InstructorID, @CreatedBy, @ModifiedBy, @CreatedOn, @ModifiedOn)
                      
                      DECLARE @DepartmentID int
                      SELECT @DepartmentID = [DepartmentID]
                      FROM [dbo].[Department]
                      WHERE @@ROWCOUNT > 0 AND [DepartmentID] = scope_identity()
                      
                      SELECT t0.[DepartmentID], t0.[RowVersion]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );

            AlterStoredProcedure(
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
                    ModifiedBy = p.String(),
                    CreatedOn = p.DateTime(),
                    ModifiedOn = p.DateTime(),
                },
                body:
                    @"UPDATE [dbo].[Department]
                      SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [InstructorID] = @InstructorID, [CreatedBy] = @CreatedBy, [ModifiedBy] = @ModifiedBy, [CreatedOn] = @CreatedOn, [ModifiedOn] = @ModifiedOn
                      WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );

            CreateStoredProcedure(
                "dbo.Department_Delete",
                p => new
                {
                    DepartmentID = p.Int(),
                    RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                },
                body:
                    @"DELETE [dbo].[Department]
                      WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );

        }

        public override void Down()
        {
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
