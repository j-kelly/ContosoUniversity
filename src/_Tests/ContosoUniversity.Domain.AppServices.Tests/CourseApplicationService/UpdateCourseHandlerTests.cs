namespace ContosoUniversity.Domain.AppServices.Tests.CourseApplicationService
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.CourseApplicationService;
    using Models;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class UpdateCourseHandlerTests
    {
        public UpdateCourse.Request CreateValidRequest(params Action<UpdateCourse.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<UpdateCourse.CommandModel>();

            var request = new UpdateCourse.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidationRules()
        {
            Action<UpdateCourse.Request> CallSut = request =>
            {
                var serviceUnderTest = new UpdateCourseHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Example (not really an invariant rule)
            Assert2.CheckInvariantValidation("Title cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel.Title = null)));
            Assert2.CheckInvariantValidation("Title cannot be set to Title", () => CallSut(CreateValidRequest(p => p.CommandModel.Title = "Title")));
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<UpdateCourse.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new UpdateCourseHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            Assert2.CheckContextualValidation("CourseId", "CourseId cannot be less than 1", () => CallSut(CreateValidRequest(p => p.CommandModel.CourseID = 0)));
            Assert2.CheckContextualValidation("DepartmentId", "DepartmentId cannot be less than 1", () => CallSut(CreateValidRequest(p => p.CommandModel.DepartmentID = 0)));
        }

        [Test]
        public void WhenUpdateCourseIsCalledThenIExpectItToDoSomething()
        {
            // Arrange
            var repository = new InMemoryRecordedRepository();
            var factory = new UpdateCourseHandlerFactory();
            factory._SetRepository(repository);

            // Act
            var request = CreateValidRequest();
            var response = factory.Object.Handle(request);

            // Assert
            response.HasValidationIssues.ShouldEqual(false);

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