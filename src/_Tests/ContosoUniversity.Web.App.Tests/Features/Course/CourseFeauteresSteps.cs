namespace ContosoUniversity.Web.App.Tests.Features.Course
{
    using Domain.Core.Behaviours.DepartmentApplicationService;
    using Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    public class CourseFeauteresSteps
    {
        [Given(@"I have a department with the following values")]
        public void GivenIHaveADepartmentWithTheFollowingValues(Table table)
        {
            var commandModel = DataGeneration.CreateCommandModelFromTable<CreateDepartment.CommandModel>(table);
        }
    }
}
