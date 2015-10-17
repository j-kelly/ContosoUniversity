namespace ContosoUniversity.Domain.AppServices.Tests.Handlers
{
    using ContosoUniversity.Web.Mvc.App_Start;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpDomainServices
    {
        [SetUp]
        public void SetUp()
        {
            // set up handlers
            DomainBootstrapper.SetUp();
        }
    }
}
