namespace ContosoUniversity.Domain.AppServices.Tests
{

    // ************************************************************************************************
    // * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
    // * **********************************************************************************************

    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TestKit.Factories;

    [TestFixture]
    public class DeleteCourseHandlerTests
    {
        public DeleteCourse.Request CreateValidRequest(params Action<DeleteCourse.Request>[] updates)
        {
            var commandModel = EntityGenerator.Create<DeleteCourse.CommandModel>();

            var request = new DeleteCourse.Request("UserId", commandModel);
            updates.ToList().ForEach(func => func(request));
            return request;
        }

        [Test]
        public void CheckInvariantValidation()
        {
            Action<DeleteCourse.Request> CallSut = request =>
            {
                var serviceUnderTest = new DeleteCourseHandlerFactory().Object;
                serviceUnderTest.Handle(request);
            };

            // Assert2.CheckInvariantValidation("[ErrorMessage]", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
        }

        [Test]
        public void CheckValidationRules()
        {
            Func<DeleteCourse.Request, ValidationMessageCollection> CallSut = request =>
            {
                var serviceUnderTest = new DeleteCourseHandlerFactory().Object;
                var reponse = serviceUnderTest.Handle(request);
                return reponse.ValidationDetails;
            };

            // Assert2.CheckValidation(
            //     "The effective date must not be before today",
            //     "EffectiveFrom",
            //     () => CallSut(CreateValidRequest(p => p.CommandModel.DummyValue = "1")));           
        }

        [Test]
        public void WhenDeleteCourseIsCalledThnIExpectItToDoSomething()
        {
        }
    }
}
