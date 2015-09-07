namespace ContosoUniversity.Domain.AppServices.Tests.CourseApplicationService
{
    using Core.Behaviours.CourseApplicationService;
    using NRepository.EntityFramework;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit;
    using TestKit.Factories;

    [TestFixture]
    public class UpdateCourseCreditsHandlerTests
    {
        public UpdateCourseCredits.Request CreateValidRequest(params Action<UpdateCourseCredits.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<UpdateCourseCredits.CommandModel>();

            var request = new UpdateCourseCredits.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void WhenUpdateCourseCreditsIsCalledThenIExpectItToDoSomething()
        {
            var efTestExtensions = new EfRepositoryTestExtension();
            EntityFrameworkRepositoryExtensions.SetDefaultImplementation(efTestExtensions);

            // Arrange
            var repository = new InMemoryRecordedRepository();
            var factory = new UpdateCourseCreditsHandlerFactory();
            factory._SetRepository(repository);

            // Act
            var request = CreateValidRequest();
            var response = factory.Object.Handle(request);

            // Assert
            response.HasValidationIssues.ShouldEqual(false);

            efTestExtensions.ExecuteStoredProcudureCallCount.ShouldEqual(1);
        }
    }
}
