namespace ContosoUniversity.Domain.AppServices.Tests.Handlers
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ServiceBehaviours;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using Core.Behaviours.Courses;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using ContosoUniversity.Core.Domain.Services;

    [TestFixture]
    public class CourseDeleteTests
    {
        public CourseDelete.Request CreateValidRequest(params Action<CourseDelete.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<CourseDelete.CommandModel>();

            var request = new CourseDelete.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<CourseDelete.Request, ValidationMessageCollection> CallSut = request =>
            {
                var response = DomainServices.Dispatch(request);
                return response.ValidationDetails;
            };

            Assert2.CheckContextualValidation("CourseId", "CourseId must have a minimum value of 1", () => CallSut(CreateValidRequest(p => p.CommandModel.CourseId = 0)));
        }

        [Test]
        public void WhenCourseDeleteIsCalledThenIExpectItToDoSomething()
        {
            // Arrange
            var repository = new InMemoryRecordedRepository();

            // Act
            var request = CreateValidRequest();
            var response = CourseHandlers.Handle(repository, request);

            // Assert
            response.HasValidationIssues.ShouldEqual(false);

            var events = repository.CommandRepository.CommandEvents;
            var course = (Course)events.DeletedEvents.First().Entity;
            course.CourseID.ShouldEqual(request.CommandModel.CourseId);

            events.SavedEvents.Count.ShouldEqual(1);
            events.ModifiedEvents.Count.ShouldEqual(0);
            events.DeletedEvents.Count.ShouldEqual(1);
            events.AddedEvents.Count.ShouldEqual(0);
        }
    }
}
