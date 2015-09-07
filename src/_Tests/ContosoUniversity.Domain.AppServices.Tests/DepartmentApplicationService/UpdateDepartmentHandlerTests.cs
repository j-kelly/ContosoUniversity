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
    public class UpdateDepartmentHandlerTests
    {
        // Prefer to leave this class at the top so easy to see what contistutes a valid command object
        public UpdateDepartment.Request CreateValidRequest(params Action<UpdateDepartment.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<UpdateDepartment.CommandModel>();

            var request = new UpdateDepartment.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidation()
        {
            Action<UpdateDepartment.Request> CallSut = request =>
            {
                var serviceUnderTest = new UpdateDepartmentHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckValidationRules()
        {
            Func<UpdateDepartment.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new UpdateDepartmentHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenUpdateDepartmentIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
