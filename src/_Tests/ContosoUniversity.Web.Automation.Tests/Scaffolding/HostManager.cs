namespace ContosoUniversity.Web.Automation.Tests.Scaffolding
{
    using Core.Repository;
    using Domain.Core.Repository;
    using Domain.Core.Repository.Entities;
    using System.Configuration;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;

    [Binding]
    public class HostManager
    {
        public static Host Host
        {
            get; private set;
        }

        public static Page Page => Host.Page;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var task = Task.Run(() => CreateEmptyDatabase());

            IisExpressHelper.StartIis();
            Host = new Host();

            task.Wait();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Host.WebDriver.Quit();
            IisExpressHelper.StopIis();
        }

        private static void CreateEmptyDatabase()
        {
            Debug.WriteLine("Starting CreateEmptyDatabase()");

            var sw = Stopwatch.StartNew();
            var conString = ConfigurationManager.ConnectionStrings["NRepository_Contoso"].ConnectionString;
            if (Database.Exists(conString))
                Database.Delete(conString);

            // Now force the database recreation
            ContosoDbInitializer.AllowDatabaseSeed = false;
            using (var repository = new ContosoUniversityEntityFrameworkRepository())
                repository.GetEntities<Department>().Any();

            Debug.WriteLine($"CreateEmptyDatabase completed in {sw.ElapsedMilliseconds} ms");
        }
    }
}
