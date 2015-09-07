namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using InstructorApplicationService;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;

    [GenerateTestFactory]
    public class DeleteInstructorHandler
    {
        private readonly IRepository _Repository;

        public DeleteInstructorHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public DeleteInstructor.Response Handle(DeleteInstructor.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new DeleteInstructor.Response(validationDetails);

            var depts = _Repository.GetEntities<Department>(p => p.InstructorID == request.CommandModel.InstructorId);
            foreach (var dept in depts)
            {
                dept.InstructorID = null;
                _Repository.Modify(dept);
            }

            var deletedInstructor = _Repository.GetEntity<Instructor>(
                p => p.ID == request.CommandModel.InstructorId,
                new EagerLoadingQueryStrategy<Instructor>(
                    p => p.OfficeAssignment));

            deletedInstructor.OfficeAssignment = null;

            _Repository.Delete(deletedInstructor);
            validationDetails = _Repository.SaveWithValidation();

            return new DeleteInstructor.Response(validationDetails);
        }
    }
}