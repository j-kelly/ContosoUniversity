namespace ContosoUniversity.App_Start
{
    using ContosoUniversity.Web.Core.Repository.Interceptors;
    using Domain.Core.Repository;
    using Microsoft.Practices.Unity;
    using NRepository.Core.Query;
    using NRepository.EntityFramework;
    using System;

    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // Queries only
            container.RegisterType<IQueryRepository, EntityFrameworkQueryRepository>(new PerRequestLifetimeManager());
            container.RegisterType<EntityFrameworkQueryRepository>(new InjectionConstructor(
                typeof(ContosoDbContext),
                typeof(ContosoFactoryQueryInterceptor)));
        }
    }
}
