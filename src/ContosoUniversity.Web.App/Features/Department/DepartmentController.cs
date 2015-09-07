namespace ContosoUniversity.Web.App.Features.Department
{
    using ContosoUniversity.Core.Annotations;
    using Domain.Core.Behaviours.DepartmentApplicationService;
    using Models;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Web.Core.Repository.Projections;

    [GenerateTestFactory]
    public class DepartmentController : Controller
    {
        private readonly IQueryRepository _QueryRepository;
        private readonly IDepartmentApplicationService _DepartmentAppService;

        public DepartmentController(
            IDepartmentApplicationService departmentAppService,
            IQueryRepository queryRepository)
        {
            _DepartmentAppService = departmentAppService;
            _QueryRepository = queryRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDepartment.CommandModel commandModel)
        {
            var response = _DepartmentAppService.CreateDepartment(new CreateDepartment.Request(CurrentPrincipalHelper.Name, commandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            var instructors = await _QueryRepository.GetEntitiesAsync<Instructor>(new AsNoTrackingQueryStrategy()); ;
            ViewBag.InstructorID = new SelectList(instructors, "ID", "FullName", commandModel.InstructorID);

            ModelState.AddRange(response.ValidationDetails);
            return View(commandModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteDepartment.CommandModel commandModel)
        {
            var response = _DepartmentAppService.DeleteDepartment(new DeleteDepartment.Request(CurrentPrincipalHelper.Name, commandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            if (response.HasConcurrencyError.Value)
                RedirectToAction("Delete", new { concurrencyError = true, id = commandModel.DepartmentID });

            ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
            return View(commandModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateDepartment.CommandModel commandModel)
        {
            var response = _DepartmentAppService.UpdateDepartment(new UpdateDepartment.Request(CurrentPrincipalHelper.Name, commandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            if (response.RowVersion != null)
                commandModel.RowVersion = response.RowVersion;

            var instructors = await _QueryRepository.GetEntitiesAsync<Instructor>(new AsNoTrackingQueryStrategy()); ;
            ViewBag.InstructorID = new SelectList(instructors, "ID", "FullName", commandModel.InstructorID);

            ModelState.AddRange(response.ValidationDetails.AllValidationMessages);
            return View(commandModel);
        }

        public async Task<ActionResult> Index()
        {
            var departments = _QueryRepository.GetEntitiesAsync<DepartmentDetail>(new OrderByQueryStrategy<DepartmentDetail>(p => p.Name));
            return View(await departments);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var department = await _QueryRepository.GetEntityAsync<DepartmentDetail>(p => p.DepartmentID == id.Value, false);
            if (department == null)
                return HttpNotFound();

            return View(department);
        }

        public async Task<ActionResult> Create()
        {
            var instructors = await _QueryRepository.GetEntitiesAsync<Instructor>(new AsNoTrackingQueryStrategy());
            ViewBag.InstructorID = new SelectList(instructors, "ID", "FullName");
            return View();
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var department = await _QueryRepository.GetEntities<Department>(
                p => p.DepartmentID == id.Value)
                .Select(p => new UpdateDepartment.CommandModel
                {
                    Budget = p.Budget,
                    DepartmentID = p.DepartmentID,
                    InstructorID = p.InstructorID,
                    Name = p.Name,
                    RowVersion = p.RowVersion,
                    StartDate = p.StartDate
                }).SingleOrDefaultAsync();

            if (department == null)
                return HttpNotFound();

            var instructors = await _QueryRepository.GetEntitiesAsync<Instructor>(new AsNoTrackingQueryStrategy()); ;
            ViewBag.InstructorID = new SelectList(instructors, "ID", "FullName", department.InstructorID);

            return View(department);
        }

        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var departmentDetail = await _QueryRepository.GetEntityAsync<DepartmentDetail>(
                p => p.DepartmentID == id.Value,
                false);

            if (departmentDetail == null)
            {
                if (concurrencyError == true)
                    return RedirectToAction("Index");

                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                if (departmentDetail == null)
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                        + "was deleted by another user after you got the original values. "
                        + "Click the Back to List hyperlink.";
                }
                else
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                        + "was modified by another user after you got the original values. "
                        + "The delete operation was canceled and the current values in the "
                        + "database have been displayed. If you still want to delete this "
                        + "record, click the Delete button again. Otherwise "
                        + "click the Back to List hyperlink.";
                }
            }

            return View(departmentDetail);
        }
    }
}
