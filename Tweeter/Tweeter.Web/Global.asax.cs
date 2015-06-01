namespace Tweeter.Web
{
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Infrastructure.Mapping;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ViewEnginesConfig.RegisterViewEngines(ViewEngines.Engines);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }
    }
}
