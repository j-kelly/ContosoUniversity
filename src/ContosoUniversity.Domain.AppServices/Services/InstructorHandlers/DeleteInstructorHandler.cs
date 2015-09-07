namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using InstructorApplicationService;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;
    using Repository.Containers;

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

            var container = new EntityStateWrapperContainer();
            var depts = _Repository.GetEntities<Department>(p => p.InstructorID == request.CommandModel.InstructorId);
            foreach (var dept in depts)
                container.Add(dept.SetInstructorId(null));

            var deletedInstructor = _Repository.GetEntity<Instructor>(
                p => p.ID == request.CommandModel.InstructorId,
                new EagerLoadingQueryStrategy<Instructor>(
                    p => p.OfficeAssignment));

            container.Add(deletedInstructor.Delete());
            validationDetails = _Repository.Save(container);

            return new DeleteInstructor.Response(validationDetails);
        }
    }
}