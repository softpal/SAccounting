using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StratusAccounting.Helper;

namespace StratusAccounting
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new CustomMetadataValidationProvider());

            //Rebind<ModelValidatorProvider>().To<CustomMetadataValidationProvider>();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            Application["AppParam"] = BAL.RegisterAppParameters.GetApplicationParameters();

        }
    }
}