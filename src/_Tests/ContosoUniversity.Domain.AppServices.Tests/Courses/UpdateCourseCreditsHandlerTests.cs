namespace ContosoUniversity.Domain.AppServices.Tests.CourseApplicationService
{
    using Core.Behaviours.Courses;
    using NRepository.EntityFramework;
    using NRepository.TestKit;
    using NUnit.Framework;
    using ServiceBehaviours;
    using System;
    using System.Linq;
    using TestKit;

    [TestFixture]
    public class CourseUpdateCreditsHandlerTests
    {
        public CourseUpdateCredits.Request CreateValidRequest(params Action<CourseUpdateCredits.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<CourseUpdateCredits.CommandModel>();

            var request = new CourseUpdateCredits.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void WhenCourseUpdateCreditsIsCalledThenIExpectItToDoSomething()
        {
            var efTestExtensions = new EfRepositoryTestExtension();
            EntityFrameworkRepositoryExtensions.SetDefaultImplementation(efTestExtensions);

            // Arrange
            var repository = new InMemoryRecordedRepository();

            // Act
            var request = CreateValidRequest();
            var response = CourseHandlers.Handle(repository, request);

            // Assert
            response.HasValidationIssues.ShouldEqual(false);

            efTestExtensions.ExecuteStoredProcudureCallCount.ShouldEqual(1);
        }
    }
}
