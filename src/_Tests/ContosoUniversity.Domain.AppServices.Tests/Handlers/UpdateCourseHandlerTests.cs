namespace ContosoUniversity.Domain.AppServices.Tests.Handlers
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.Services;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using Core.Behaviours.Courses;
    using NRepository.TestKit;
    using NUnit.Framework;
    using ServiceBehaviours;
    using System;
    using System.Linq;

    [TestFixture]
    public class CourseUpdateHandlerTests
    {
        public CourseUpdate.Request CreateValidRequest(params Action<CourseUpdate.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<CourseUpdate.CommandModel>();

            var request = new CourseUpdate.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidationRules()
        {
            Action<CourseUpdate.Request> CallSut = request =>
            {
                DomainServices.Dispatch(request);
            };

            // Example (not really an invariant rule)
            Assert2.CheckInvariantValidation("Title cannot be set to Title", () => CallSut(CreateValidRequest(p => p.CommandModel.Title = "Title")));
            Assert2.CheckInvariantValidation("Title cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel.Title = null)));
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<CourseUpdate.Request, ValidationMessageCollection> CallSut = request =>
            {
                var reponse = DomainServices.Dispatch(request);
                return reponse.ValidationDetails;
            };

            Assert2.CheckContextualValidation("CourseId", "CourseId cannot be less than 1", () => CallSut(CreateValidRequest(p => p.CommandModel.CourseID = 0)));
            Assert2.CheckContextualValidation("DepartmentId", "DepartmentId cannot be less than 1", () => CallSut(CreateValidRequest(p => p.CommandModel.DepartmentID = 0)));
        }

        [Test]
        public void WhenCourseUpdateIsCalledThenIExpectItToDoSomething()
        {
            // Arrange
            var repository = new InMemoryRecordedRepository();

            // Act
            var request = CreateValidRequest();
            var response = CourseHandlers.Handle(repository, request);

            // Assert
            response.HasValidationIssues.ShouldEqual(false);

            // Check modified entity
            var events = repository.CommandRepository.CommandEvents;
            var course = (Course)events.ModifiedEvents.First().Entity;
            course.CourseID.ShouldEqual(request.CommandModel.CourseID);
            course.Credits.ShouldEqual(request.CommandModel.Credits);
            course.DepartmentID.ShouldEqual(request.CommandModel.DepartmentID);
            course.Title.ShouldEqual(request.CommandModel.Title);

            events.SavedEvents.Count.ShouldEqual(1);
            events.ModifiedEvents.Count.ShouldEqual(1);
            events.DeletedEvents.Count.ShouldEqual(0);
            events.AddedEvents.Count.ShouldEqual(0);
        }
    }
}