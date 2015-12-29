using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FoodService.Business;
using FoodService.Business.ServiceInterfaces;
using FoodService.Business.Services;
using FoodService.DAL;
using FoodService.DAL.Interfaces;
using FoodServiceWebApplication.Controllers;

namespace FoodServiceWebApplication.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Classes.FromThisAssembly()
            //    .BasedOn<IController>()
            //    .WithService.AllInterfaces() // new
            //    .LifestyleTransient());

            //Register controllers.
            container.Register(Classes
                .FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest()
                );

            //container.Register(Classes
            //    .FromThisAssembly()
            //    .Pick()
            //    .WithServiceAllInterfaces()
            //    .Configure(configurer => configurer.Named(configurer.Implementation.Name))
            //    .LifestylePerWebRequest()
            //    );


            //container.Register(
            //    Classes.FromThisAssembly()
            //    .BasedOn<IController>()
            //    .LifestylePerWebRequest()
            //    .Configure(x => x.Named(x.Implementation.FullName)));

            container.Register(Classes.FromAssembly(Assembly.GetAssembly(typeof(IDishService)))
                           .Where(Component.IsInSameNamespaceAs<FoodService.Business.Services.DishService>())
                           .WithService.DefaultInterfaces()
                           .LifestyleTransient());//);

            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnityOfWork>().LifestyleTransient());
            //var r = container.Resolve<IUnitOfWork>();

        }
    }
}