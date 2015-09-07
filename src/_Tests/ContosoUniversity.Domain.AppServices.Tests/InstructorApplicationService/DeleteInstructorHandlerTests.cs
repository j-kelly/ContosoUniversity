namespace ContosoUniversity.Domain.AppServices.Tests.InstructorApplicationService
{
    using System;
    using System.Linq;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using NRepository.TestKit;
    using NUnit.Framework;
    using TestKit.Factories;
    using Core.Behaviours.InstructorApplicationService;

    [TestFixture]
    public class DeleteInstructorHandlerTests
    {
        // Prefer to leave this class at the top so easy to see what contistutes a valid command object
        public DeleteInstructor.Request CreateValidRequest(params Action<DeleteInstructor.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<DeleteInstructor.CommandModel>();

            var request = new DeleteInstructor.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidation()
        {
            Action<DeleteInstructor.Request> CallSut = request =>
            {
                var serviceUnderTest = new DeleteInstructorHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckValidationRules()
        {
            Func<DeleteInstructor.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new DeleteInstructorHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenDeleteInstructorIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
