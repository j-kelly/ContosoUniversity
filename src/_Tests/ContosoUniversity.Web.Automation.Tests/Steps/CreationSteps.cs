namespace ContosoUniversity.Web.Automation.Tests.Steps.Behaviours
{
    using ContosoUniversity.Core.Domain.Services;
    using ContosoUniversity.Web.Automation.Tests.Scaffolding;
    using ContosoUniversity.Web.Automation.Tests.Scaffolding.Data;
    using Core.Repository;
    using Domain.Core.Behaviours.Courses;
    using Domain.Core.Behaviours.Departments;
    using Domain.Core.Behaviours.Instructors;
    using Domain.Core.Behaviours.Students;
    using System;
    using System.Linq;
    using TechTalk.SpecFlow;

    [Binding]
    public class CreationSteps
    {
        [Given(@"I have the following departments")]
        public void GivenIHaveTheFollowingDepartments(Table table)
        {
            using (var repository = new ContosoUniversityEntityFrameworkRepository())
            {
                foreach (var row in table.Rows)
                {
                    var commandModel = DataHelper.CreateCommandModelFromTable<DepartmentCreate.CommandModel>(table, row);
                    var response = DomainServices.CallService<DepartmentCreate.Response>(new DepartmentCreate.Request("test", commandModel));
                    if (response.HasValidationIssues)
                        throw new ApplicationException(string.Join(" | ", response.ValidationDetails.Select(p => p.ErrorMessage)));

                    DataHelper.AddEntityToRemove(EntityType.Department, response.DepartmentId);
                }
            }
        }

        [Given(@"I have the following students")]
        public void GivenIHaveTheFollowingStudents(Table table)
        {
            foreach (var row in table.Rows)
            {
                var commandModel = DataHelper.CreateCommandModelFromTable<StudentCreate.CommandModel>(table, row);
                var response = DomainServices.CallService<StudentCreate.Response>(new StudentCreate.Request("test", commandModel));

                if (response.HasValidationIssues)
                    throw new ApplicationException(string.Join(" | ", response.ValidationDetails.Select(p => p.ErrorMessage)));

                DataHelper.AddEntityToRemove(EntityType.Student, response.StudentId);
            }
        }

        [Given(@"I have the following instructors")]
        public void GivenIHaveTheFollowingInstructors(Table table)
        {
            foreach (var row in table.Rows)
            {
                var commandModel = DataHelper.CreateCommandModelFromTable<InstructorCreateWithCourses.CommandModel>(table, row);
                var response = DomainServices.CallService<InstructorCreateWithCourses.Response>(new InstructorCreateWithCourses.Request(
                    "test",
                    commandModel));

                if (response.HasValidationIssues)
                    throw new ApplicationException(string.Join(" | ", response.ValidationDetails.Select(p => p.ErrorMessage)));

                DataHelper.AddEntityToRemove(EntityType.Instructor, response.InstructorId);
            }
        }

        [Given(@"I have the following courses")]
        public void GivenIHaveTheFollowingCourses(Table table)
        {
            foreach (var row in table.Rows)
            {
                var commandModel = DataHelper.CreateCommandModelFromTable<CourseCreate.CommandModel>(table, row);
                var response = DomainServices.CallService<CourseCreate.Response>(new CourseCreate.Request("test", commandModel));

                if (response.HasValidationIssues)
                    throw new ApplicationException(string.Join(" | ", response.ValidationDetails.Select(p => p.ErrorMessage)));

                DataHelper.AddEntityToRemove(EntityType.Course, response.CourseId);
            }
        }
    }
}
