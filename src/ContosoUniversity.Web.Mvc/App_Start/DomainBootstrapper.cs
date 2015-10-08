namespace ContosoUniversity.Web.Mvc.App_Start
{
    using ContosoUniversity.Core.Domain;
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
            DomainServices.AddService<CourseCreate.Request>(request => CommonDecorator(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseDelete.Request>(request => CommonDecorator(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseUpdate.Request>(request => CommonDecorator(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseUpdateCredits.Request>(request => CommonDecorator(request, p => CourseHandlers.Handle(CreateRepository(), request)));

            // Instructors
            DomainServices.AddService<InstructorDelete.Request>(request => CommonDecorator(request, p => InstructorHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<InstructorModifyAndCourses.Request>(request => CommonDecorator(request, p => InstructorHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<InstructorCreateWithCourses.Request>(request => CommonDecorator(request, p => InstructorHandlers.Handle(CreateRepository(), request)));

            // Students
            DomainServices.AddService<StudentDelete.Request>(request => CommonDecorator(request, p => StudentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<StudentCreate.Request>(request => CommonDecorator(request, p => StudentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<StudentModify.Request>(request => CommonDecorator(request, p => StudentHandlers.Handle(CreateRepository(), request)));

            // Departments
            DomainServices.AddService<DepartmentDelete.Request>(request => CommonDecorator(request, p => DepartmentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<DepartmentCreate.Request>(request => CommonDecorator(request, p => DepartmentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<DepartmentUpdate.Request>(request => CommonDecorator(request, p1 => DepartmentHandlers.Handle(CreateRepository(), request)));
        }

        private static IDomainResponse CommonDecorator<T>(T command, Expression<Func<T, IDomainResponse>> next) where T : class, IDomainRequest
        {
            // create chain
            Expression<Func<T, IDomainResponse>> autoDispose = p => Decorators.AutoDispose(next);
            Expression<Func<T, IDomainResponse>> log = p => Decorators.Log(command, autoDispose);

            // Run it
            return log.Compile().Invoke(command);
        }
    }
}