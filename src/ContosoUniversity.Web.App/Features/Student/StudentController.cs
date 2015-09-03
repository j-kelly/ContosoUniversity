namespace ContosoUniversity.Features.Student
{
    using ContosoUniversity.Core.Annotations;
    using Domain.Core.Behaviours.StudentApplicationService;
    using Domain.Core.Behaviours.StudentApplicationService.CreateStudent;
    using Domain.Core.Behaviours.StudentApplicationService.DeleteStudent;
    using Domain.Core.Behaviours.StudentApplicationService.ModifyStudent;
    using Models;
    using NRepository.Core.Query;
    using PagedList;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Web.Core.Repository.Projections;

    [GenerateTestFactoryAttribute]
    public class StudentController : Controller
    {
        private readonly IQueryRepository _QueryRepository;
        private readonly IStudentApplicationService _StudentApplicationService;

        public StudentController(
            IStudentApplicationService studentApplicationService,
            IQueryRepository queryRepository)
        {
            _StudentApplicationService = studentApplicationService;
            _QueryRepository = queryRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var response = _StudentApplicationService.DeleteStudent(new DeleteStudentRequest(
                CurrentPrincipalHelper.UserId,
                new DeleteStudentCommandModel { StudentId = id }));

            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            return RedirectToAction("Delete", new { id = id, saveChangesError = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStudentCommandModel commandModel)
        {
            var response = _StudentApplicationService.CreateStudent(new CreateStudentRequest(CurrentPrincipalHelper.UserId, commandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            ModelState.AddRange(response.ValidationDetails.AllValidationMessages);
            return View(commandModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModifyStudentCommandModel commandModel)
        {
            var response = _StudentApplicationService.ModifyStudent(new ModifyStudentRequest(CurrentPrincipalHelper.UserId, commandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            ModelState.AddRange(response.ValidationDetails.AllValidationMessages);
            return View(commandModel);

        }

        public async Task<ViewResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var students = await _QueryRepository.GetEntitiesAsync<StudentDetail>(
                new MultipleTextSearchSpecificationStrategy<StudentDetail>(
                        searchString,
                        p => p.LastName,
                        p => p.FirstMidName).OnCondition(!string.IsNullOrEmpty(searchString)),
                new SwitchQueryStrategy(
                    new OrderByQueryStrategy<StudentDetail>(p => p.LastName),
                    new ConditionalQueryStrategy(sortOrder == "Date", new OrderByQueryStrategy<StudentDetail>(p => p.EnrollmentDate)),
                    new ConditionalQueryStrategy(sortOrder == "name_desc", new OrderByDescendingQueryStrategy<StudentDetail>(p => p.LastName)),
                    new ConditionalQueryStrategy(sortOrder == "date_desc", new OrderByDescendingQueryStrategy<StudentDetail>(p => p.EnrollmentDate))));

            return View(students.ToPagedList(pageNumber: page ?? 1, pageSize: 3));
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = await _QueryRepository.GetEntityAsync<Student>(p => p.ID == id.Value, false);
            if (student == null)
                return HttpNotFound();

            return View(new ModifyStudentCommandModel
            {
                EnrollmentDate = student.EnrollmentDate,
                FirstMidName = student.FirstMidName,
                ID = student.ID,
                LastName = student.LastName
            });
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = await _QueryRepository.GetEntityAsync<StudentDetail>(p => p.StudentId == id.Value, false);
            if (student == null)
                return HttpNotFound();

            return View(student);
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (saveChangesError.GetValueOrDefault())
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";

            var student = await _QueryRepository.GetEntityAsync<StudentDetail>(p => p.StudentId == id.Value, false);
            if (student == null)
                return HttpNotFound();

            return View(student);
        }
    }
}
