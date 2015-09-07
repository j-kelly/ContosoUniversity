namespace ContosoUniversity.Web.App.Features.Instructor
{
    using Core.Repository.Projections;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InstructorDetailViewModel
    {
        //public class InstructorDetailFactoryQuery : FactoryQuery<InstructorDetail>
        //{
        //    public override IQueryable<object> Query<T>(IQueryRepository repository, object additionalQueryData)
        //    {
        //        var instructors = repository.GetEntities<Instructor>()
        //            .Select(p => new InstructorDetail
        //            {
        //                FirstMidName = p.FirstMidName,
        //                HireDate = p.HireDate,
        //                InstructorId = p.ID,
        //                LastName = p.LastName,
        //                OfficeLocation = p.OfficeAssignment.Location,
        //                CourseDetails = p.Courses.Select(p1 => new CourseDetail
        //                {
        //                    CourseID = p1.CourseID,
        //                    Credits = p1.Credits,
        //                    DepartmentID = p1.DepartmentID,
        //                    DepartmentName = p1.Department.Name,
        //                    Title = p1.Title
        //                }),
        //            });

        //        return instructors;
        //    }
        //}

        public int InstructorId { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Office Location")]
        public string OfficeLocation { get; set; }

        public IEnumerable<CourseDetail> CourseDetails { get; set; }
    }
}
