namespace ContosoUniversity.Domain.AppServices.Tests.StudentApplicationService
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.StudentApplicationService;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class DeleteStudentHandlerTests
    {
        // Prefer to leave this class at the top so easy to see what contistutes a valid command object
        public DeleteStudent.Request CreateValidRequest(params Action<DeleteStudent.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<DeleteStudent.CommandModel>();

            var request = new DeleteStudent.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidationRules()
        {
            Action<DeleteStudent.Request> CallSut = request =>
            {
                var serviceUnderTest = new DeleteStudentHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckContextualValidationRules()
        {
            Func<DeleteStudent.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new DeleteStudentHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenDeleteStudentIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
