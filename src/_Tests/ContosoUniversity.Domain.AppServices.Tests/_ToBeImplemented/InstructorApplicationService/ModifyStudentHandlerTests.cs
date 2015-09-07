namespace ContosoUniversity.Domain.AppServices.Tests.InstructorApplicationService
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.StudentApplicationService;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class ModifyStudentHandlerTests
    {
        // Prefer to leave this class at the top so easy to see what contistutes a valid command object
        public ModifyStudent.Request CreateValidRequest(params Action<ModifyStudent.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<ModifyStudent.CommandModel>();

            var request = new ModifyStudent.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidationRules()
        {
            Action<ModifyStudent.Request> CallSut = request =>
            {
                var serviceUnderTest = new ModifyStudentHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<ModifyStudent.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new ModifyStudentHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenModifyStudentIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
