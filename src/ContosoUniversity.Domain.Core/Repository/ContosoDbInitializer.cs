namespace ContosoUniversity.Domain.Core.Repository
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ContosoDbInitializer : DropCreateDatabaseIfModelChanges<ContosoDbContext>
    {
        protected override void Seed(ContosoDbContext context)
        {
            var students = new List<ContosoUniversity.Domain.Core.Repository.Entities.Student>
            {
                new ContosoUniversity.Domain.Core.Repository.Entities.Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new ContosoUniversity.Domain.Core.Repository.Entities.Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new ContosoUniversity.Domain.Core.Repository.Entities.Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new ContosoUniversity.Domain.Core.Repository.Entities.Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new ContosoUniversity.Domain.Core.Repository.Entities.Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new ContosoUniversity.Domain.Core.Repository.Entities.Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new ContosoUniversity.Domain.Core.Repository.Entities.Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new ContosoUniversity.Domain.Core.Repository.Entities.Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var department = new ContosoUniversity.Domain.Core.Repository.Entities.Department { DepartmentID = 1, Name = "MainDepartment", StartDate = DateTime.Parse("2000-01-01"), CreatedBy = "Anon", ModifiedBy = "Anon", CreatedOn = DateTimeHelper.Now, ModifiedOn = DateTimeHelper.Now };

            var courses = new List<ContosoUniversity.Domain.Core.Repository.Entities.Course>
            {
                new ContosoUniversity.Domain.Core.Repository.Entities.Course{CourseID=1050,Title="Chemistry",Credits=3, Department = department},
                new ContosoUniversity.Domain.Core.Repository.Entities.Course{CourseID=4022,Title="Microeconomics",Credits=3, Department = department},
                new ContosoUniversity.Domain.Core.Repository.Entities.Course{CourseID=4041,Title="Macroeconomics",Credits=3, Department = department},
                new ContosoUniversity.Domain.Core.Repository.Entities.Course{CourseID=1045,Title="Calculus",Credits=4, Department = department},
                new ContosoUniversity.Domain.Core.Repository.Entities.Course{CourseID=3141,Title="Trigonometry",Credits=4, Department = department},
                new ContosoUniversity.Domain.Core.Repository.Entities.Course{CourseID=2021,Title="Composition",Credits=3, Department = department},
                new ContosoUniversity.Domain.Core.Repository.Entities.Course{CourseID=2042,Title="Literature",Credits=4, Department = department},
            };

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<ContosoUniversity.Domain.Core.Repository.Entities.Enrollment>
            {
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=3,CourseID=1050, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=4,CourseID=1050, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=6,CourseID=1045, Course = courses.First()},
                new ContosoUniversity.Domain.Core.Repository.Entities.Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A, Course = courses.First()},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}