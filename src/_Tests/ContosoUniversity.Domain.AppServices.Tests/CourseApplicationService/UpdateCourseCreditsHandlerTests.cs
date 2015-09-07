namespace ContosoUniversity.Domain.AppServices.Tests.CourseApplicationService
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.CourseApplicationService;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class UpdateCourseCreditsHandlerTests
    {
        public UpdateCourseCredits.Request CreateValidRequest(params Action<UpdateCourseCredits.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<UpdateCourseCredits.CommandModel>();

            var request = new UpdateCourseCredits.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidation()
        {
            Action<UpdateCourseCredits.Request> CallSut = request =>
            {
                var serviceUnderTest = new UpdateCourseCreditsHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckValidationRules()
        {
            Func<UpdateCourseCredits.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new UpdateCourseCreditsHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenUpdateCourseCreditsIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}
