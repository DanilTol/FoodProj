using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using FoodService.Business.Mapping;
using FoodService.WebApi2.Dependency;
using Microsoft.Practices.ServiceLocation;

namespace FoodService.WebApi2
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitMap initMap = new InitMap();
            initMap.InitAllMaps();



            //GlobalConfiguration.Configuration.Services.Replace(
            //                typeof(IHttpControllerActivator),
            //                new PoorMansCompositionRoot());


            //IoC
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(this.container));



            //Service locator for attributes
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));


        }


        private readonly IWindsorContainer container;

        public WebApiApplication()
        {
            this.container =
                new WindsorContainer().Install(new ControllerInstaller());
        }

        public override void Dispose()
        {
            this.container.Dispose();
            base.Dispose();
        }








    }
}
