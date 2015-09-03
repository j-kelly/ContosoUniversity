namespace ContosoUniversity.App_Start
{
    using ContosoUniversity.DAL;
    using ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService;
    using ContosoUniversity.Web.Core.Repository.Interceptors;
    using Domain.AppServices;
    using Domain.Core.Behaviours.CourseApplicationService;
    using Domain.Core.Behaviours.InstructorApplicationService;
    using Domain.Core.Behaviours.StudentApplicationService;
    using Domain.Services.DepartmentApplicationService;
    using Microsoft.Practices.Unity;
    using NRepository.Core;
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

            // Queries and commands
            container.RegisterType<IRepository, EntityFrameworkRepository>(new PerRequestLifetimeManager());
            container.RegisterType<EntityFrameworkRepository>(new InjectionConstructor(
                typeof(ContosoDbContext),
                typeof(ContosoUniversityRepositoryInterceptors)));

            // Domain application services
            container.RegisterType<ICourseApplicationService, CourseApplicationService>();
            container.RegisterType<IDepartmentApplicationService, DepartmentApplicationService>();
            container.RegisterType<IStudentApplicationService, StudentApplicationService>();
            container.RegisterType<IInstructorApplicationService, InstructorApplicationService>();
        }
    }
}
