namespace ContosoUniversity.Domain.AppServices.Tests.CourseApplicationService
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using Core.Behaviours.Courses;
    using NRepository.TestKit;
    using NUnit.Framework;
    using ServiceBehaviours;
    using System;
    using System.Linq;

    [TestFixture]
    public class CourseCreateTests
    {
        public CourseCreate.Request CreateValidRequest(params Action<CourseCreate.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<CourseCreate.CommandModel>();

            var request = new CourseCreate.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<CourseCreate.Request, ValidationMessageCollection> CallSut = request =>
            {
                var response = CourseHandlers.Handle(null, request);
                return response.ValidationDetails;
            };

            Assert2.CheckContextualValidation("Title", "The field Title must be a string with a minimum length of 3 and a maximum length of 50.", () => CallSut(CreateValidRequest(p => p.CommandModel.Title = "X")));
            Assert2.CheckContextualValidation("Credits", "The field Credits must be between 1 and 5.", () => CallSut(CreateValidRequest(p => p.CommandModel.Credits = 0)));
        }

        [Test]
        public void WhenCourseCreateIsCalledThenIExpectItToDoSomething()
        {
            // Arrange
            var repository = new InMemoryRecordedRepository();
             
            // Act
            var request = CreateValidRequest();
            var response = CourseHandlers.Handle(repository, request);

            // Assert
            response.HasValidationIssues.ShouldEqual(false);

            var events = repository.CommandRepository.CommandEvents;
            var course = (Course)events.AddedEvents.First().Entity;
            course.CourseID.ShouldEqual(request.CommandModel.CourseID);
            course.Credits.ShouldEqual(request.CommandModel.Credits);
            course.DepartmentID.ShouldEqual(request.CommandModel.DepartmentID);
            course.Title.ShouldEqual(request.CommandModel.Title);

            events.SavedEvents.Count.ShouldEqual(1);
            events.ModifiedEvents.Count.ShouldEqual(0);
            events.DeletedEvents.Count.ShouldEqual(0);
            events.AddedEvents.Count.ShouldEqual(1);
        }
    }
}
