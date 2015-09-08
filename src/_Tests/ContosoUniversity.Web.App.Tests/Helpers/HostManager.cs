namespace ContosoUniversity.Web.App.Tests
{
    using TechTalk.SpecFlow;

    [Binding]
    public class HostManager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Unused member")]
        public static string ServerUrl { get; } = $"http://localhost:{IisExpressHelper.Port}/";

        public static Host Host { get; private set; }

        public static Page Page { get { return Host.Page; } }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            IisExpressHelper.StartIis();
            Host = new Host();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Host.WebDriver.Quit();
            IisExpressHelper.StopIis();
        }

        //[BeforeScenario]
        //public static void BeforeScenario()
        //{
        //}

        //[AfterScenario]
        //public static void AfterScenario()
        //{
        //}

    }
}
