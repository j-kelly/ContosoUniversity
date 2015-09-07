namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using InstructorApplicationService;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework;
    using System.Data.Entity;
    using System.Linq;

    [GenerateTestFactory]
    public class CreateInstructorWithCoursesHandler
    {
        private readonly IRepository _Repository;

        public CreateInstructorWithCoursesHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateInstructorWithCourses.Response Handle(CreateInstructorWithCourses.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new CreateInstructorWithCourses.Response(validationDetails);

            var commandModel = request.CommandModel;
            var courses = commandModel.SelectedCourses == null
                ? new Course[0].ToList()
                : commandModel.SelectedCourses.Select(courseId =>
                 {
                     var course = new Course { CourseID = courseId };
                     _Repository.UpdateEntityState(course, EntityState.Unchanged);
                     return course;
                 }).ToList();

            var instructor = new Instructor
            {
                HireDate = commandModel.HireDate,
                FirstMidName = commandModel.FirstMidName,
                LastName = commandModel.LastName,
                Courses = courses,
                OfficeAssignment = new OfficeAssignment { Location = commandModel.OfficeLocation },
            };

            _Repository.Add(instructor);
            validationDetails = _Repository.SaveWithValidation();
            return new CreateInstructorWithCourses.Response(validationDetails);
        }
    }
}