using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ContosoUniversity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ContosoUniversity.App_Start.FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ContosoUniversity.App_Start.RouteConfig.RegisterRoutes(RouteTable.Routes);
            ContosoUniversity.App_Start.BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
