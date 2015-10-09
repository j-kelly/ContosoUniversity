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
            DomainServices.AddService<CourseCreate.Request>(request => Default(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseDelete.Request>(request => Default(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseUpdate.Request>(request => Default(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseUpdateCredits.Request>(request => Default(request, p => CourseHandlers.Handle(CreateRepository(), request)));

            // Instructors
            DomainServices.AddService<InstructorDelete.Request>(request => Default(request, p => InstructorHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<InstructorModifyAndCourses.Request>(request => Default(request, p => InstructorHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<InstructorCreateWithCourses.Request>(request => Default(request, p => InstructorHandlers.Handle(CreateRepository(), request)));

            // Students
            DomainServices.AddService<StudentDelete.Request>(request => Default(request, p => StudentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<StudentCreate.Request>(request => Default(request, p => StudentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<StudentModify.Request>(request => Default(request, p => StudentHandlers.Handle(CreateRepository(), request)));

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

            return Default(request, handler);
        }

        private static IDomainResponse Default<T>(T request, Expression<Func<T, IDomainResponse>> handler) where T : class, IDomainRequest
        {
            // create chain (last to first)
            Expression<Func<T, IDomainResponse>> autoDispose = p => Decorators.AutoDispose(handler);
            Expression<Func<T, IDomainResponse>> logTimings = p => Decorators.Log(request, autoDispose);

            // Run it
            return logTimings.Compile().Invoke(request);
        }

        // Example on how to auto validate
        public static IDomainResponse AutoValidate<TRequest, TResponse>(TRequest request, Expression<Func<TRequest, IDomainResponse>> handler)
            where TRequest : class, IDomainRequest
            where TResponse : class, IDomainResponse
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return (IDomainResponse)Activator.CreateInstance(typeof(TResponse), validationDetails);

            return Default(request, handler);
        }
    }
}