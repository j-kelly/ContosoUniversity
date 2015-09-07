namespace ContosoUniversity.Domain.AppServices.Tests.DepartmentApplicationService
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.DepartmentApplicationService;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class DeleteDepartmentHandlerTests
    {
        // Prefer to leave this class at the top so easy to see what contistutes a valid command object
        public DeleteDepartment.Request CreateValidRequest(params Action<DeleteDepartment.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<DeleteDepartment.CommandModel>();

            var request = new DeleteDepartment.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidation()
        {
            Action<DeleteDepartment.Request> CallSut = request =>
            {
                var serviceUnderTest = new DeleteDepartmentHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckValidationRules()
        {
            Func<DeleteDepartment.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new DeleteDepartmentHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenDeleteDepartmentIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
