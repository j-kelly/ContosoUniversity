namespace ContosoUniversity.Web.Mvc.App_Start
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.Services;
    using ContosoUniversity.Domain.Core.Behaviours.Courses;
    using Core.Repository.Interceptors;
    using Domain.AppServices.ServiceBehaviours;
    using Domain.Core.Behaviours.Departments;
    using Domain.Core.Behaviours.Instructors;
    using Domain.Core.Behaviours.Students;
    using Domain.Core.Repository;
    using NRepository.Core;
    using NRepository.EntityFramework;
    using System;
    using System.Linq.Expressions;

    public static class DomainBootstrapper
    {
        public static void SetUp()
        {
            // Create repository
            Func<IRepository> CreateRepository = () => new EntityFrameworkRepository(new ContosoDbContext(), new ContosoUniversityRepositoryInterceptors());

            // Courses
            DomainServices.AddService<CourseCreate.Request>(request => DefaultWithValidation(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseDelete.Request>(request => DefaultWithValidation(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseUpdate.Request>(request => DefaultWithValidation(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseUpdateCredits.Request>(request => DefaultWithValidation(request, p => CourseHandlers.Handle(CreateRepository(), request)));

            // Instructors
            DomainServices.AddService<InstructorDelete.Request>(request => DefaultWithValidation(request, p => InstructorHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<InstructorModifyAndCourses.Request>(request => DefaultWithValidation(request, p => InstructorHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<InstructorCreateWithCourses.Request>(request => DefaultWithValidation(request, p => InstructorHandlers.Handle(CreateRepository(), request)));

            // Students
            DomainServices.AddService<StudentDelete.Request>(request => DefaultWithValidation(request, p => StudentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<StudentCreate.Request>(request => DefaultWithValidation(request, p => StudentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<StudentModify.Request>(request => DefaultWithValidation(request, p => StudentHandlers.Handle(CreateRepository(), request)));

            // Departments
            DomainServices.AddService<DepartmentDelete.Request>(request => AministratorsOnly(request, p => DepartmentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<DepartmentCreate.Request>(request => AministratorsOnly(request, p => DepartmentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<DepartmentUpdate.Request>(request => AministratorsOnly(request, p => DepartmentHandlers.Handle(CreateRepository(), request)));
        }

        // Example on how to add a security decorator
        public static IDomainResponse AministratorsOnly<T>(T request, Expression<Func<T, IDomainResponse>> handler) where T : class, IDomainRequest
        {
            var isAdministrator = true;
            if (!isAdministrator)
                throw new UnauthorizedAccessException("Bad Person alert!");

            // The department items require the repository interface for validation so we don't autovalidate 
            // (should be a better way of doing this)
            return Default(request, handler);
        }

        // Chain: LOG then AUTO-VALIDATE then run with AUTO-DISPOSE
        private static IDomainResponse DefaultWithValidation<T>(T request, Expression<Func<T, IDomainResponse>> handler) where T : class, IDomainRequest
        {
            // create chain (last to first)
            Expression<Func<T, IDomainResponse>> autoDispose = p => Decorators.AutoDispose(handler);
            Expression<Func<T, IDomainResponse>> autoValidate = p => Decorators.AutoValidate(request, autoDispose);
            Expression<Func<T, IDomainResponse>> logTimings = p => Decorators.Log(request, autoValidate);

            return logTimings.Compile().Invoke(request);
        }


        // Chain: LOG then AUTO-VALIDATE then run with AUTO-DISPOSE
        private static IDomainResponse Default<T>(T request, Expression<Func<T, IDomainResponse>> handler) where T : class, IDomainRequest
        {
            // create chain (last to first)
            Expression<Func<T, IDomainResponse>> autoDispose = p => Decorators.AutoDispose(handler);
            Expression<Func<T, IDomainResponse>> logTimings = p => Decorators.Log(request, autoDispose);

            return logTimings.Compile().Invoke(request);
        }
    }
}