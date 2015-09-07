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
    public class CreateDepartmentHandlerTests
    {
        // Prefer to leave this class at the top so easy to see what contistutes a valid command object
        public CreateDepartment.Request CreateValidRequest(params Action<CreateDepartment.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<CreateDepartment.CommandModel>();

            var request = new CreateDepartment.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidation()
        {
            Action<CreateDepartment.Request> CallSut = request =>
          {
              var serviceUnderTest = new CreateDepartmentHandlerFactory().Object;
              serviceUnderTest.Handle(request);
          };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckValidationRules()
        {
            Func<CreateDepartment.Request, ValidationMessageCollection> CallSut = request =>
          {
              var serviceUnderTest = new CreateDepartmentHandlerFactory().Object;
              var reponse = serviceUnderTest.Handle(request);
              return reponse.ValidationDetails;
          };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenCreateDepartmentIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
