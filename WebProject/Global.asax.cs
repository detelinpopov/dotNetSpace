using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using InversionOfControl;

namespace WebProject
{
	public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

			Register.Start();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);          
        }
    }
}
