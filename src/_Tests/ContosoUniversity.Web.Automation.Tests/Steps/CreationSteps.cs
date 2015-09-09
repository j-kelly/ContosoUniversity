namespace ContosoUniversity.Web.Automation.Tests.Steps.Behaviours
{
    using ContosoUniversity.Web.Automation.Tests.Scaffolding;
    using ContosoUniversity.Web.Automation.Tests.Scaffolding.Data;
    using Core.Repository;
    using Domain.AppServices;
    using Domain.Core.Behaviours.CourseApplicationService;
    using Domain.Core.Behaviours.DepartmentApplicationService;
    using Domain.Core.Behaviours.InstructorApplicationService;
    using Domain.Core.Behaviours.StudentApplicationService;
    using Domain.Services.DepartmentApplicationService;
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
                    var commandModel = DataHelper.CreateCommandModelFromTable<CreateDepartment.CommandModel>(table, row);
                    var response = new DepartmentApplicationService(repository).CreateDepartment(
                        new CreateDepartment.Request(
                        "test",
                        commandModel));

                    if (response.HasValidationIssues)
                        throw new ApplicationException(string.Join(" | ", response.ValidationDetails.Select(p => p.ErrorMessage)));

                    DataHelper.AddEntityToRemove(EntityType.Department, response.DepartmentId);
                }
            }
        }

        [Given(@"I have the following students")]
        public void GivenIHaveTheFollowingStudents(Table table)
        {
            using (var repository = new ContosoUniversityEntityFrameworkRepository())
            {
                foreach (var row in table.Rows)
                {
                    var commandModel = DataHelper.CreateCommandModelFromTable<CreateStudent.CommandModel>(table, row);
                    var response = new StudentApplicationService(repository).CreateStudent(
                        new CreateStudent.Request(
                        "test",
                        commandModel));

                    if (response.HasValidationIssues)
                        throw new ApplicationException(string.Join(" | ", response.ValidationDetails.Select(p => p.ErrorMessage)));

                    DataHelper.AddEntityToRemove(EntityType.Student, response.StudentId);
                }
            }
        }

        [Given(@"I have the following instructors")]
        public void GivenIHaveTheFollowingInstructors(Table table)
        {
            using (var repository = new ContosoUniversityEntityFrameworkRepository())
            {
                foreach (var row in table.Rows)
                {
                    var commandModel = DataHelper.CreateCommandModelFromTable<CreateInstructorWithCourses.CommandModel>(table, row);
                    var response = new InstructorApplicationService(repository).CreateInstructorWithCourses(
                        new CreateInstructorWithCourses.Request(
                        "test",
                        commandModel));

                    if (response.HasValidationIssues)
                        throw new ApplicationException(string.Join(" | ", response.ValidationDetails.Select(p => p.ErrorMessage)));

                    DataHelper.AddEntityToRemove(EntityType.Instructor, response.InstructorId);
                }
            }
        }

        [Given(@"I have the following courses")]
        public void GivenIHaveTheFollowingCourses(Table table)
        {
            using (var repository = new ContosoUniversityEntityFrameworkRepository())
            {
                foreach (var row in table.Rows)
                {
                    var commandModel = DataHelper.CreateCommandModelFromTable<CreateCourse.CommandModel>(table, row);
                    var response = new CourseApplicationService(repository).CreateCourse(
                        new CreateCourse.Request(
                        "test",
                        commandModel));

                    if (response.HasValidationIssues)
                        throw new ApplicationException(string.Join(" | ", response.ValidationDetails.Select(p => p.ErrorMessage)));

                    DataHelper.AddEntityToRemove(EntityType.Course, response.CourseId);
                }
            }
        }
    }
}
