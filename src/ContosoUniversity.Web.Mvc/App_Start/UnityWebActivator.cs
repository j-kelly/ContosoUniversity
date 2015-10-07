[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ContosoUniversity.App_Start.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(ContosoUniversity.App_Start.UnityWebActivator), "Shutdown")]

namespace ContosoUniversity.App_Start
{
    using Microsoft.Practices.Unity.Mvc;
    using System.Linq;
    using System.Web.Mvc;
    using Utility.Logging.NLog;
    using Web.Mvc.App_Start;

    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            ContosoUniversity.Core.Logging.LogManager.SetFactory(new NLogLoggerFactory());

            // Set up domain functions
            DomainBootstrapper.SetUp();

            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());

        }

        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}