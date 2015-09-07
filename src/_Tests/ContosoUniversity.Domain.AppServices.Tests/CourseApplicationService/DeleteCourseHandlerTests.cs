namespace ContosoUniversity.Domain.AppServices.Tests
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService;
    using Models;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class DeleteCourseHandlerTests
    {
        public DeleteCourse.Request CreateValidRequest(params Action<DeleteCourse.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<DeleteCourse.CommandModel>();

            var request = new DeleteCourse.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<DeleteCourse.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new DeleteCourseHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            Assert2.CheckContextualValidation("CourseId", "CourseId must have a minimum value of 1", () => CallSut(CreateValidRequest(p => p.CommandModel.CourseId = 0)));
        }

        [Test]
        public void WhenDeleteCourseIsCalledThenIExpectItToDoSomething()
        {
            // Arrange
            var repository = new InMemoryRecordedRepository();
            var factory = new DeleteCourseHandlerFactory();
            factory._SetRepository(repository);

            // Act
            var request = CreateValidRequest();
            var response = factory.Object.Handle(request);

            // Assert
            response.HasValidationIssues.ShouldEqual(false);

            var events = repository.CommandRepository.CommandEvents;
            var course = (ContosoUniversity.Domain.Core.Repository.Entities.Course)events.DeletedEvents.First().Entity;
            course.CourseID.ShouldEqual(request.CommandModel.CourseId);

            events.SavedEvents.Count.ShouldEqual(1);
            events.ModifiedEvents.Count.ShouldEqual(0);
            events.DeletedEvents.Count.ShouldEqual(1);
            events.AddedEvents.Count.ShouldEqual(0);
        }
    }
}
