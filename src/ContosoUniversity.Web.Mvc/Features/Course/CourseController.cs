namespace ContosoUniversity.Web.Mvc.Features.Course
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Domain.Core.Behaviours.Courses;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Web.Core.Repository.Projections;

    [GenerateTestFactory]
    public class CourseController : Controller
    {
        private readonly IQueryRepository _QueryRepository;

        public CourseController(IQueryRepository queryRepository)
        {
            _QueryRepository = queryRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseCreate.CommandModel commandModel)
        {
            var request = new CourseCreate.Request(CurrentPrincipalHelper.Name, commandModel);
            var response = DomainServices.CallService(request);

            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            ViewBag.DepartmentID = await CreateDepartmentSelectList(commandModel.DepartmentID);
            return View(commandModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CourseUpdate.CommandModel commandModel)
        {
            var request = new CourseUpdate.Request(CurrentPrincipalHelper.Name, commandModel);
            var response = DomainServices.CallService(request);

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
            var request = new CourseDelete.Request(CurrentPrincipalHelper.Name, new CourseDelete.CommandModel { CourseId = id });
            DomainServices.CallService(request);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateCourseCredits(int? multiplier)
        {
            if (multiplier != null)
            {
                var request = new CourseUpdateCredits.Request(CurrentPrincipalHelper.Name, new CourseUpdateCredits.CommandModel { Multiplier = multiplier.Value });
                var response = DomainServices.CallService<CourseUpdateCredits.Response>(request);

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
            return View(new CourseUpdate.CommandModel
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
