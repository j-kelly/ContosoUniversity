namespace ContosoUniversity.Web.App.Features.Course
{
    using ContosoUniversity.Core.Annotations;
    using Domain.Core.Behaviours.CourseApplicationService;
    using Domain.Core.Behaviours.CourseApplicationService.CreateCourse;
    using Domain.Core.Behaviours.CourseApplicationService.DeleteCourse;
    using Domain.Core.Behaviours.CourseApplicationService.UpdateCourse;
    using Domain.Core.Behaviours.CourseApplicationService.UpdateCourseCredits;
    using Models;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Web.Core.Repository.Projections;

    [GenerateTestFactoryAttribute]
    public class CourseController : Controller
    {
        private readonly IQueryRepository _QueryRepository;
        private readonly ICourseApplicationService _CourseAppService;

        public CourseController(
            ICourseApplicationService courseAppService,
            IQueryRepository queryRepository)
        {
            _CourseAppService = courseAppService;
            _QueryRepository = queryRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCourseCommandModel commandModel)
        {
            var response = _CourseAppService.CreateCourse(new CreateCourseRequest(CurrentPrincipalHelper.Name, commandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            ViewBag.DepartmentID = await CreateDepartmentSelectList(commandModel.DepartmentID);
            return View(commandModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateCourseCommandModel commandModel)
        {
            var response = _CourseAppService.UpdateCourse(new UpdateCourseRequest(CurrentPrincipalHelper.Name, commandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            ModelState.AddRange(response.ValidationDetails.AllValidationMessages);
            ViewBag.DepartmentID = await CreateDepartmentSelectList(commandModel.DepartmentID);
            return View(commandModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _CourseAppService.DeleteCourse(new DeleteCourseRequest(
                CurrentPrincipalHelper.Name,
                new DeleteCourseCommandModel { CourseId = id }));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateCourseCredits(int? multiplier)
        {
            if (multiplier != null)
            {
                var response = _CourseAppService.UpdateCourseCredits(new UpdateCourseCreditsRequest(
                    CurrentPrincipalHelper.Name,
                    new UpdateCourseCreditsCommandModel { Multiplier = multiplier.Value }));

                ViewBag.RowsAffected = response.RowsEffected;
            }

            return View();
        }

        public async Task<ActionResult> Index(int? selectedDepartment)
        {
            ViewBag.SelectedDepartment = await CreateDepartmentSelectList(selectedDepartment);

            int departmentID = selectedDepartment.GetValueOrDefault();
            var courseDetails = await _QueryRepository.GetEntitiesAsync<CourseDetail>(
                c => !selectedDepartment.HasValue || c.DepartmentID == departmentID,
                new AsNoTrackingQueryStrategy());

            return View(courseDetails);
        }

        public async Task<ActionResult> Details(int id)
        {
            var course = await _QueryRepository.GetEntityAsync<CourseDetail>(
                p => p.CourseID == id,
                new AsNoTrackingQueryStrategy(),
                false);

            if (course == null)
                return HttpNotFound();

            return View(course);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.DepartmentID = await CreateDepartmentSelectList();
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var course = await _QueryRepository.GetEntityAsync<Course>(p => p.CourseID == id);
            if (course == null)
                return HttpNotFound();

            ViewBag.DepartmentID = await CreateDepartmentSelectList(course.DepartmentID);
            return View(new UpdateCourseCommandModel
            {
                CourseID = course.CourseID,
                Credits = course.Credits,
                DepartmentID = course.DepartmentID,
                Title = course.Title
            });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var course = await _QueryRepository.GetEntityAsync<CourseDetail>(p => p.CourseID == id, false);
            if (course == null)
                return HttpNotFound();

            return View(course);
        }

        public ActionResult UpdateCourseCredits()
        {
            return View();
        }

        private async Task<SelectList> CreateDepartmentSelectList(object selectedDepartment = null)
        {
            var departmentsQuery = await _QueryRepository.GetEntitiesAsync<Department>(
                    new AsNoTrackingQueryStrategy(),
                    new OrderByDescendingQueryStrategy<Department>(p => p.Name));

            return new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }
    }
}
