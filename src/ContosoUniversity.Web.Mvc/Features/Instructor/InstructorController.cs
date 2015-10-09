namespace ContosoUniversity.Web.Mvc.Features.Instructor
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.Services;
    using ContosoUniversity.Domain.Core.Behaviours.Instructors;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using ContosoUniversity.Web.Core.Repository.Projections;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ViewModels;

    [GenerateTestFactory]
    public class InstructorController : Controller
    {
        private readonly IQueryRepository _QueryRepository;

        public InstructorController(IQueryRepository queryRepository)
        {
            _QueryRepository = queryRepository;
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var request = new InstructorDelete.Request(
                SystemPrincipal.Name,
                new InstructorDelete.CommandModel { InstructorId = id });

            await DomainServices.CallServiceAsync(request);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateInstructorWithCoursesViewModel viewModel)
        {
            var request = new InstructorCreateWithCourses.Request(SystemPrincipal.Name, viewModel.CommandModel);
            var response = DomainServices.CallService(request);

            if (response.HasValidationIssues)
            {
                ModelState.AddRange(response.ValidationDetails);
                await PopulateAssignedCourseData(viewModel.SelectedCourses);
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ModifyInstructorAndCoursesViewModel viewModel)
        {
            var request = new InstructorModifyAndCourses.Request(SystemPrincipal.Name, viewModel.CommandModel);
            var response = DomainServices.CallService(request);

            if (response.HasValidationIssues)
            {
                ModelState.AddRange(response.ValidationDetails);
                await PopulateAssignedCourseData(viewModel.SelectedCourses);
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexDataViewModel();
            viewModel.Instructors = GetInstructorDetails().ToArray();

            if (id != null)
            {
                ViewBag.InstructorID = id.Value;
                viewModel.Courses = viewModel.Instructors.Single(p => p.InstructorId == id).CourseDetails;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                viewModel.Enrollments = await _QueryRepository.GetEntities<Enrollment>(
                    p => p.CourseID == courseID)
                    .Select(p => new EnrollmentDetailViewModel
                    {
                        FirstMidName = p.Student.FirstMidName,
                        LastName = p.Student.LastName,
                        Grade = p.Grade
                    }).AsAsync();
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var instructorDetail = await GetInstructorDetails(p => p.InstructorId == id.Value).SingleOrDefaultAsync();
            if (instructorDetail == null)
                return HttpNotFound();

            return View(instructorDetail);
        }

        public async Task<ActionResult> Create()
        {
            var commandModel = new CreateInstructorWithCoursesViewModel();
            await PopulateAssignedCourseData(new int[0]);
            return View(commandModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var instructor = await _QueryRepository.GetEntities<Instructor>(
                p => p.ID == id.Value)
                .Select(ins => new ModifyInstructorAndCoursesViewModel
                {
                    FirstMidName = ins.FirstMidName,
                    HireDate = ins.HireDate,
                    InstructorId = ins.ID,
                    LastName = ins.LastName,
                    OfficeLocation = ins.OfficeAssignment.Location,
                    SelectedCourses = ins.Courses.Select(p => p.CourseID)
                }).SingleOrDefaultAsync();

            if (instructor == null)
                return HttpNotFound();

            await PopulateAssignedCourseData(instructor.SelectedCourses.Select(p => p));
            return View(instructor);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var instructorDetail = await GetInstructorDetails(p => p.InstructorId == id.Value).SingleOrDefaultAsync();
            if (instructorDetail == null)
                return HttpNotFound();

            return View(instructorDetail);
        }

        private async Task PopulateAssignedCourseData(IEnumerable<int> courseIds)
        {
            var allCourses = await _QueryRepository.GetEntitiesAsync<Course>();
            var instructorCourses = new HashSet<int>(courseIds ?? new int[0]);
            var viewModel = new List<AssignedCourseDataViewModel>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseDataViewModel
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }

            ViewBag.Courses = viewModel;
        }

        private IQueryable<InstructorDetailViewModel> GetInstructorDetails(Expression<Func<InstructorDetailViewModel, bool>> expression = null)
        {
            var instructors = _QueryRepository.GetEntities<Instructor>(
                new OrderByQueryStrategy<Instructor>(p => p.LastName),
                new AsNoTrackingQueryStrategy())
                  .Select(p => new InstructorDetailViewModel
                  {
                      FirstMidName = p.FirstMidName,
                      HireDate = p.HireDate,
                      InstructorId = p.ID,
                      LastName = p.LastName,
                      OfficeLocation = p.OfficeAssignment.Location,
                      CourseDetails = p.Courses.Select(p1 => new CourseDetail
                      {
                          CourseID = p1.CourseID,
                          Credits = p1.Credits,
                          DepartmentID = p1.DepartmentID,
                          DepartmentName = p1.Department.Name,
                          Title = p1.Title
                      }),
                  });

            return expression != null
                ? instructors.Where(expression)
                : instructors;
        }
    }
}