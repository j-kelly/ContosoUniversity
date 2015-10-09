namespace ContosoUniversity.Domain.Core.Factories
{
    using ContosoUniversity.Domain.Core.Behaviours.Students;
    using Repository.Entities;

    public static class StudentFactory
    {
        public static Student CreatePartial(int studentId)
        {
            return new Student { ID = studentId };
        }

        public static Student Create(StudentCreate.CommandModel commandModel)
        {
            var student = new Student
            {
                EnrollmentDate = commandModel.EnrollmentDate,
                FirstMidName = commandModel.FirstMidName,
                LastName = commandModel.LastName,
            };

            return student;
        }
    }
}
