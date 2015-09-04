namespace ContosoUniversity.Web.App.Features.Instructor
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Web.Core.Repository.Projections;
    using Domain.Core.Behaviours.InstructorApplicationService;
    using Domain.Core.Behaviours.InstructorApplicationService.CreateInstructorWithCourses;
    using Domain.Core.Behaviours.InstructorApplicationService.DeleteInstructor;
    using Domain.Core.Behaviours.InstructorApplicationService.ModifyInstructorAndCourses;
    using Models;
    using NRepository.Core.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [GenerateTestFactoryAttribute]
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
            _InstructorAppService.DeleteInstructor(new DeleteInstructorRequest(
                CurrentPrincipalHelper.Name,
                new DeleteInstructorCommandModel { InstructorId = id }));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateInstructorWithCoursesCommandModel commandModel)
        {
            var response = _InstructorAppService.CreateInstructorWithCourses(new CreateInstructorWithCoursesRequest(
               CurrentPrincipalHelper.Name,
               commandModel));

            if (response.HasValidationIssues)
            {
                ModelState.AddRange(response.ValidationDetails);
                await PopulateAssignedCourseData(commandModel.SelectedCourses);
                return View(commandModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ModifyInstructorAndCoursesCommandModel commandModel)
        {
            var response = _InstructorAppService.ModifyInstructorAndCourses(new ModifyInstructorAndCoursesRequest(
               CurrentPrincipalHelper.Name,
               commandModel));

            if (response.HasValidationIssues)
            {
                ModelState.AddRange(response.ValidationDetails);
                await PopulateAssignedCourseData(commandModel.SelectedCourses);
                return View(commandModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexData();
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
                    .Select(p => new EnrollmentDetail
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
            var commandModel = new CreateInstructorWithCoursesCommandModel();
            await PopulateAssignedCourseData(new int[0]);
            return View(commandModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var instructor = await _QueryRepository.GetEntities<Instructor>(
                p => p.ID == id.Value)
                .Select(ins => new ModifyInstructorAndCoursesCommandModel
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
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }

            ViewBag.Courses = viewModel;
        }

        private IQueryable<InstructorDetail> GetInstructorDetails(Expression<Func<InstructorDetail, bool>> expression = null)
        {
            var instructors = _QueryRepository.GetEntities<Instructor>(
                new OrderByQueryStrategy<Instructor>(p => p.LastName))
                  .Select(p => new InstructorDetail
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