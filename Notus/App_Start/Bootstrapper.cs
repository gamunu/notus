using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Notus.Data;
using Notus.Data.Infrastructure;
using Notus.Data.Repository;
using Notus.Mappings;
using Notus.Model.Models;
using Notus.Service;
using Notus.Web.Core.Authentication;

namespace Notus
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof (FocusRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof (GoalService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof (DefaultFormsAuthentication).Assembly)
                .Where(t => t.Name.EndsWith("Authentication"))
                .AsImplementedInterfaces().InstancePerHttpRequest();

            builder.Register(
                c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new NotusEntities())))
                .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();

            builder.RegisterFilterProvider();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}