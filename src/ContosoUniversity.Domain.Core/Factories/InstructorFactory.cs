namespace ContosoUniversity.Domain.Core.Factories
{
    
    using ContosoUniversity.Domain.Core.Repository.Strategies;
    using NRepository.Core.Query;
    using Repository.Containers;
    using Repository.Entities;
    using System.Linq;

    public static class InstructorFactory
    {
        public static EntityStateWrapperContainer Create(IQueryRepository queryRepository, ContosoUniversity.Domain.Core.Behaviours.Instructors.InstructorCreateWithCourses.CommandModel commandModel)
        {
            // could use Course.CreatePartial here and attachEntities using EntityStateWrapperContainer
            var courses = commandModel.SelectedCourses == null
                ? new Course[0].ToList()
                : queryRepository.GetEntities<Course>(new FindByIdsSpecificationStrategy<Course>(p => p.CourseID, commandModel.SelectedCourses)).ToList();

            var instructor = new Instructor
            {
                HireDate = commandModel.HireDate,
                FirstMidName = commandModel.FirstMidName,
                LastName = commandModel.LastName,
                Courses = courses,
                OfficeAssignment = new OfficeAssignment { Location = commandModel.OfficeLocation },
            };

            return new EntityStateWrapperContainer().AddEntity(instructor);
        }
    }
}
