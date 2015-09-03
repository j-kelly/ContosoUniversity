namespace ContosoUniversity.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class ContosoDbInitializer : DropCreateDatabaseIfModelChanges<ContosoDbContext>
    {
        protected override void Seed(ContosoDbContext context)
        {
            var students = new List<Student>
            {
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var department = new Department { DepartmentID = 1, Name = "MainDepartment", StartDate = DateTime.Parse("2000-01-01") };

            var courses = new List<Course>
            {
                new Course{CourseID=1050,Title="Chemistry",Credits=3, Department = department},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3, Department = department},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3, Department = department},
                new Course{CourseID=1045,Title="Calculus",Credits=4, Department = department},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4, Department = department},
                new Course{CourseID=2021,Title="Composition",Credits=3, Department = department},
                new Course{CourseID=2042,Title="Literature",Credits=4, Department = department},
            };

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A, Course = courses.First()},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C, Course = courses.First()},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B, Course = courses.First()},
                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B, Course = courses.First()},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F, Course = courses.First()},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F, Course = courses.First()},
                new Enrollment{StudentID=3,CourseID=1050, Course = courses.First()},
                new Enrollment{StudentID=4,CourseID=1050, Course = courses.First()},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F, Course = courses.First()},
                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C, Course = courses.First()},
                new Enrollment{StudentID=6,CourseID=1045, Course = courses.First()},
                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A, Course = courses.First()},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}