namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DAL;
    using InstructorApplicationService;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;
    using System.Linq;

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

            // Removals first
            instructor.Courses.Clear();
            if (instructor.OfficeAssignment != null && commandModel.OfficeLocation == null)
                _Repository.Delete(instructor.OfficeAssignment);

            // Update properties
            instructor.FirstMidName = commandModel.FirstMidName;
            instructor.LastName = commandModel.LastName;
            instructor.HireDate = commandModel.HireDate;
            instructor.OfficeAssignment = new OfficeAssignment { Location = commandModel.OfficeLocation };

            if (commandModel.SelectedCourses != null)
            {
                instructor.Courses = _Repository.GetEntities<Course>(
                    new FindByIdsSpecificationStrategy<Course>(p => p.CourseID, commandModel.SelectedCourses))
                    .ToList();
            }

            _Repository.Modify(instructor);
            validationDetails = _Repository.SaveWithValidation();

            return new ModifyInstructorAndCourses.Response(validationDetails);
        }
    }
}