namespace ContosoUniversity.Web.App.Tests
{
    using Domain.Core.Behaviours.CourseApplicationService;
    using NRepository.TestKit;
    using NUnit.Framework;
    using System.Linq;
    using System.Web.Mvc;
    using TestKit.Factories;

    [TestFixture]
    public class CourseControllerTests
    {
        [Test]
        public void CheckCreateAddsANewCourseWhenNoErrors()
        {
            var commandModel = EntityGenerator.Create<CreateCourse.CommandModel>();
            var response = EntityGenerator.Create<CreateCourse.Response>();

            var factory = new CourseControllerFactory();
            factory.CourseApplicationServiceMock.Setup(p => p.CreateCourse(Take.Any<CreateCourse.Request>())).Returns(response);

            var result = (RedirectToRouteResult)factory.Object.Create(commandModel).Result;

            // Assert
            result.RouteValues.First().Value.ShouldEqual("Index");
            factory.VerifyAll();
        }
    }
}
