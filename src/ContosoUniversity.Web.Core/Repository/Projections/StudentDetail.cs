namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using ContosoUniversity.Models;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class StudentDetail
    {
        public class FactoryQuery : FactoryQuery<StudentDetail>
        {
            public override IQueryable<object> Query<T>(IQueryRepository repository, object additionalQueryData)
            {
                var students = repository.GetEntities<Student>()
                    .Select(student => new StudentDetail
                    {
                        StudentId = student.ID,
                        EnrollmentDate = student.EnrollmentDate,
                        FirstMidName = student.FirstMidName,
                        LastName = student.LastName,
                        EnrollmentDetails = student.Enrollments.Select(enrollment => new StudentDetail.EnrollmentDetail
                        {
                            CourseTitle = enrollment.Course.Title,
                            Grade = enrollment.Grade
                        }),
                    });

                return students;
            }
        }

        public class EnrollmentDetail
        {
            public string CourseTitle { get; set; }
            public Grade? Grade { get; set; }
        }

        public int StudentId { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Enrollment Details")]
        public IEnumerable<EnrollmentDetail> EnrollmentDetails { get; set; }
    }
}
