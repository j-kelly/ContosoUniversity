namespace ContosoUniversity.Web.Mvc.Features.Instructor
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using ContosoUniversity.Web.Core.Repository.Projections;
    using Domain.Core.Behaviours.InstructorApplicationService;
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
        private readonly IInstructorApplicationService _InstructorAppService;
        private readonly IQueryRepository _QueryRepository;

        public InstructorController(
            IInstructorApplicationService instructorAppService,
            IQueryRepository queryRepository)
        {
            _QueryRepository = queryRepository;
            _InstructorAppService = instructorAppService;
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _InstructorAppService.DeleteInstructor(new DeleteInstructor.Request(
                CurrentPrincipalHelper.Name,
                new DeleteInstructor.CommandModel { InstructorId = id }));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateInstructorWithCoursesViewModel viewModel)
        {
            var response = _InstructorAppService.CreateInstructorWithCourses(new CreateInstructorWithCourses.Request(
               CurrentPrincipalHelper.Name,
               viewModel.CommandModel));

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
            var response = _InstructorAppService.ModifyInstructorAndCourses(new ModifyInstructorAndCourses.Request(
               CurrentPrincipalHelper.Name,
               viewModel.CommandModel));

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
            var instructorCourses = new HashSet<int>(courseIds);
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