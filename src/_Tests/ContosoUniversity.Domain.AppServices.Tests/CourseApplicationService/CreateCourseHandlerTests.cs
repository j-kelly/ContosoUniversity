namespace ContosoUniversity.Domain.AppServices.Tests.CourseApplicationService
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using Core.Behaviours.CourseApplicationService;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class CreateCourseHandlerTests
    {
        public CreateCourse.Request CreateValidRequest(params Action<CreateCourse.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<CreateCourse.CommandModel>();

            var request = new CreateCourse.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<CreateCourse.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new CreateCourseHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            Assert2.CheckContextualValidation("Title", "The field Title must be a string with a minimum length of 3 and a maximum length of 50.", () => CallSut(CreateValidRequest(p => p.CommandModel.Title = "X")));
            Assert2.CheckContextualValidation("Credits", "The field Credits must be between 1 and 5.", () => CallSut(CreateValidRequest(p => p.CommandModel.Credits = 0)));
        }

        [Test]
        public void WhenCreateCourseIsCalledThenIExpectItToDoSomething()
        {
            // Arrange
            var repository = new InMemoryRecordedRepository();
            var factory = new CreateCourseHandlerFactory();
            factory._SetRepository(repository);

            // Act
            var request = CreateValidRequest();
            var response = factory.Object.Handle(request);

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
