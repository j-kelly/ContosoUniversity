﻿namespace ContosoUniversity.Web.Automation.Tests.Scaffolding
{
    using Mvc.App_Start;
    using Core.Repository;
    using Domain.Core.Repository;
    using Domain.Core.Repository.Entities;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.SqlClient;
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

            // Sets up all our domain services
            DomainBootstrapper.SetUp();

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
            {
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    new SqlCommand("ALTER DATABASE [NRepository_Contoso_Test] SET OFFLINE  WITH ROLLBACK IMMEDIATE", con).ExecuteNonQuery();
                    new SqlCommand("ALTER DATABASE [NRepository_Contoso_Test] SET ONLINE", con).ExecuteNonQuery();
                    new SqlCommand("DROP DATABASE [NRepository_Contoso_Test]", con).ExecuteNonQuery();
                }

                //      Database.Delete(conString);
            }

            // Now force the database recreation
            ContosoDbInitializer.AllowDatabaseSeed = false;
            using (var repository = new ContosoUniversityEntityFrameworkRepository())
                repository.GetEntities<Department>().Any();

            Debug.WriteLine($"CreateEmptyDatabase completed in {sw.ElapsedMilliseconds} ms");
        }
    }
}
