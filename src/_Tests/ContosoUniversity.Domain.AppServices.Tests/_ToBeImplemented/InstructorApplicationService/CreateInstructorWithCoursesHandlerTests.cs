namespace ContosoUniversity.Domain.AppServices.Tests.InstructorApplicationService
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.InstructorApplicationService;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class CreateInstructorWithCoursesHandlerTests
    {
        // Prefer to leave this class at the top so easy to see what contistutes a valid command object
        public CreateInstructorWithCourses.Request CreateValidRequest(params Action<CreateInstructorWithCourses.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<CreateInstructorWithCourses.CommandModel>();

            var request = new CreateInstructorWithCourses.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidationRules()
        {
            Action<CreateInstructorWithCourses.Request> CallSut = request =>
            {
                var serviceUnderTest = new CreateInstructorWithCoursesHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<CreateInstructorWithCourses.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new CreateInstructorWithCoursesHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenCreateInstructorWithCoursesIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
