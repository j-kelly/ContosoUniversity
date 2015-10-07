namespace ContosoUniversity.Web.Mvc.App_Start
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Logging;
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

    public static class DomainBootstrapper
    {
        public static void SetUp()
        {
            // create repository
            Func<IRepository> CreateRepository = () =>
            new EntityFrameworkRepository(
                new ContosoDbContext(),
                new ContosoUniversityRepositoryInterceptors());

            // Courses
            DomainServices.AddService<CourseCreate.Request>(request => Log(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseDelete.Request>(request => Log(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseUpdate.Request>(request => Log(request, p => CourseHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<CourseUpdateCredits.Request>(request => Log(request, p => CourseHandlers.Handle(CreateRepository(), request)));

            // Instructors
            DomainServices.AddService<InstructorDelete.Request>(request => Log(request, p => InstructorHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<InstructorModifyAndCourses.Request>(request => Log(request, p => InstructorHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<InstructorCreateWithCourses.Request>(request => Log(request, p => InstructorHandlers.Handle(CreateRepository(), request)));

            // Students
            DomainServices.AddService<StudentDelete.Request>(request => Log(request, p => StudentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<StudentCreate.Request>(request => Log(request, p => StudentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<StudentModify.Request>(request => Log(request, p => StudentHandlers.Handle(CreateRepository(), request)));

            // Departments
            DomainServices.AddService<DepartmentDelete.Request>(request => Log(request, p => DepartmentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<DepartmentUpdate.Request>(request => Log(request, p => DepartmentHandlers.Handle(CreateRepository(), request)));
            DomainServices.AddService<DepartmentCreate.Request>(request => Log(request, p => DepartmentHandlers.Handle(CreateRepository(), request)));
        }

        private static IDomainResponse Log<T>(T Command, Func<T, IDomainResponse> next) where T : class, IDomainRequest
        {
            var logger = LogManager.CreateLogger<T>();
            logger.Debug("Calling: {0} function",typeof(T).Name);

            // this call logs timings to the timings log if call over x ms.
            var retVal = logger.TraceCall(() => next(Command));
            return retVal;
        }
    }
}