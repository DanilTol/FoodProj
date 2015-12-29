using System;
using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor;
using FoodService.Business.ServiceInterfaces;
using FoodService.DAL.Interfaces;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;


namespace FoodServiceWebApplication.Plumbing
{
    public class DependencyResolverForWindsor : IDependencyResolver
    {

        //private readonly IKernel _kernel;

        //public DependencyResolverForWindsor(IKernel kernel)
        //{
        //    _kernel = kernel;
        //}

        //public object GetService(Type serviceType)
        //{
        //    //return _kernel.TryGet(serviceType);
        //}

        //public IEnumerable<object> GetServices(Type serviceType)
        //{
        //    //return _kernel.GetAll(serviceType);
        //}






        private readonly IWindsorContainer _container;

        //public DependencyResolverForWindsor() : this(new WindsorContainer())
        //{
        //}

        public DependencyResolverForWindsor(IWindsorContainer container)
        {
            _container = container;
        }

        public IUnitOfWork GetUOF()
        {
            return _container.Resolve<IUnitOfWork>();
        }
        public IUserService GetuserService()
        {
            return _container.Resolve<IUserService>();
        }


        public object GetService(Type serviceType)
        {
            return _container.Resolve<IUserService>();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}