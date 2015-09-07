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
    public class UpdateCourseHandlerTests
    {
        public UpdateCourse.Request CreateValidRequest(params Action<UpdateCourse.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<UpdateCourse.CommandModel>();

            var request = new UpdateCourse.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidation()
        {
            Action<UpdateCourse.Request> CallSut = request =>
            {
                var serviceUnderTest = new UpdateCourseHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = null )));
        }

        [Test]
        public void CheckValidationRules()
        {
            Func<UpdateCourse.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new UpdateCourseHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation( "[ExpectedMessage]", "[PropertyName]", () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenUpdateCourseIsCalledThenIExpectItToDoSomething()
        {
        }
    }
}