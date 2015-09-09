namespace ContosoUniversity.App_Start
{
    using System.Web.Mvc;

    public class FeatureViewLocationRazorViewEngine : RazorViewEngine
    {
        public FeatureViewLocationRazorViewEngine()
        {
            MasterLocationFormats = ViewLocationFormats = new[]
            {
                    "~/Features/{1}/{0}.cshtml",
                    "~/Features/_Shared/{0}.cshtml",
                };

            PartialViewLocationFormats = new[]
            {
                    "~/Features/{1}/{0}.cshtml",
                    "~/Features/_Shared/{0}.cshtml",
                };
        }
    }
}
