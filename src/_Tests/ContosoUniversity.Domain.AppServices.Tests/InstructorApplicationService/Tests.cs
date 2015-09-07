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
    public class ModifyInstructorAndCoursesHandlerTests
    {
        // Prefer to leave this class at the top so easy to see what contistutes a valid command object
        public ModifyInstructorAndCourses.Request CreateValidRequest(params Action<ModifyInstructorAndCourses.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<ModifyInstructorAndCourses.CommandModel>();

            var request = new ModifyInstructorAndCourses.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidation()
        {
            Action<ModifyInstructorAndCourses.Request> CallSut = request =>
            {
                var serviceUnderTest = new ModifyInstructorAndCoursesHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckValidationRules()
        {
            Func<ModifyInstructorAndCourses.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new ModifyInstructorAndCoursesHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenModifyInstructorAndCoursesIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
