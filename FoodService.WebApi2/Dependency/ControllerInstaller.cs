using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FoodService.Business.ServiceInterfaces;
using FoodService.Business.Services;
using FoodService.DAL;
using FoodService.DAL.Interfaces;

namespace FoodService.WebApi2.Dependency
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
           //Register controllers.
            container.Register(Classes
                .FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest()
                );
         
            container.Register(Classes.FromAssembly(Assembly.GetAssembly(typeof(IDishService)))
                           .Where(Component.IsInSameNamespaceAs<DishService>())
                           .WithService.DefaultInterfaces()
                           .LifestyleTransient());//);

            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnityOfWork>().LifestyleTransient());
        }
    }
}