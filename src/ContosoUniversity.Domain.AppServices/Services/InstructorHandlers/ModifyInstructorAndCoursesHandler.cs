namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using InstructorApplicationService;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;

    [GenerateTestFactory]
    public class ModifyInstructorAndCoursesHandler
    {
        private readonly IRepository _Repository;

        public ModifyInstructorAndCoursesHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public ModifyInstructorAndCourses.Response Handle(ModifyInstructorAndCourses.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new ModifyInstructorAndCourses.Response(validationDetails);

            var commandModel = request.CommandModel;
            var instructor = _Repository.GetEntity<Instructor>(
                p => p.ID == commandModel.InstructorId,
                new EagerLoadingQueryStrategy<Instructor>(
                    p => p.Courses,
                    p => p.OfficeAssignment));

            var container = instructor.Modify(_Repository, request.CommandModel);
            validationDetails = _Repository.Save(container);

            return new ModifyInstructorAndCourses.Response(validationDetails);
        }
    }
}