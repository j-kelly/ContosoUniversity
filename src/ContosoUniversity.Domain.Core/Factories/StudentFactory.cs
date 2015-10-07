namespace ContosoUniversity.Domain.Core.Factories
{
    
    using Repository.Containers;
    using Repository.Entities;

    public static class StudentFactory
    {
        public static Student CreatePartial(int studentId)
        {
            return new Student { ID = studentId };
        }

        public static EntityStateWrapperContainer Create(ContosoUniversity.Domain.Core.Behaviours.Students.StudentCreate.CommandModel commandModel)
        {
            var student = new Student
            {
                EnrollmentDate = commandModel.EnrollmentDate,
                FirstMidName = commandModel.FirstMidName,
                LastName = commandModel.LastName,
            };

            return new EntityStateWrapperContainer().AddEntity(student);
        }
    }
}
