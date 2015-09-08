namespace ContosoUniversity.Domain.Core.Repository
{
    using Domain.Core.Repository.Entities;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class ContosoDbContext : DbContext
    {
        static ContosoDbContext()
        {
            Database.SetInitializer(new ContosoDbInitializer());
        }

        public ContosoDbContext()
            : base("Name=NRepository_Contoso")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<AuditPropertyTrail> AuditPropertyTrails { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));
            modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}