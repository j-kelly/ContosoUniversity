namespace ContosoUniversity.Domain.AppServices.ServiceBehaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Behaviours.Instructors;
    using Core.Factories;
    using Core.Repository.Containers;
    using Core.Repository.Entities;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;

    public static class InstructorHandlers
    {
        // Update instructor with course 
        public static InstructorCreateWithCourses.Response Handle(IRepository repository, InstructorCreateWithCourses.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new InstructorCreateWithCourses.Response(validationDetails);

            var container = new EntityStateWrapperContainer();
            container.AddEntity(InstructorFactory.Create(repository, request.CommandModel));
            validationDetails = repository.Save(container);

            var instructorId = default(int?);
            if (!validationDetails.HasValidationIssues)
                instructorId = container.FindEntity<Instructor>().ID;

            return new InstructorCreateWithCourses.Response(validationDetails, instructorId);
        }

        // Modify instructor with course 
        public static InstructorModifyAndCourses.Response Handle(IRepository repository, InstructorModifyAndCourses.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new InstructorModifyAndCourses.Response(validationDetails);

            var commandModel = request.CommandModel;
            var instructor = repository.GetEntity<Instructor>(
                p => p.ID == commandModel.InstructorId,
                new EagerLoadingQueryStrategy<Instructor>(
                    p => p.Courses,
                    p => p.OfficeAssignment));

            var container = instructor.Modify(repository, request.CommandModel);
            validationDetails = repository.Save(container);

            return new InstructorModifyAndCourses.Response(validationDetails);
        }

        // Delete instructor
        public static InstructorDelete.Response Handle(IRepository repository, InstructorDelete.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new InstructorDelete.Response(validationDetails);

            var container = new EntityStateWrapperContainer();
            var depts = repository.GetEntities<Department>(p => p.InstructorID == request.CommandModel.InstructorId);
            foreach (var dept in depts)
                container.Add(dept.SetInstructorId(null));

            var deletedInstructor = repository.GetEntity<Instructor>(
                p => p.ID == request.CommandModel.InstructorId,
                new EagerLoadingQueryStrategy<Instructor>(
                    p => p.OfficeAssignment));

            container.Add(deletedInstructor.Delete());
            validationDetails = repository.Save(container);

            return new InstructorDelete.Response(validationDetails);
        }
    }
}