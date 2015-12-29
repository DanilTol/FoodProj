using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CommonServiceLocator.WindsorAdapter;
using FoodService.Business.Mapping;
using FoodServiceWebApplication.Plumbing;
using Microsoft.Practices.ServiceLocation;

namespace FoodServiceWebApplication
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitMap initMap = new InitMap();
            initMap.InitAllMaps();

            //IoC
            BootstrapContainer();

            //Service locator for attributes
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
  
        }


        private static IWindsorContainer container;

        private static void BootstrapContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }




        protected void Application_End()
        {
            container.Dispose();
        }
    }
}
