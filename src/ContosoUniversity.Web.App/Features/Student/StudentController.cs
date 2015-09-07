namespace ContosoUniversity.Web.App.Features.Student
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using Domain.Core.Behaviours.StudentApplicationService;
    using NRepository.Core.Query;
    using PagedList;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ViewModels;
    using Web.Core.Repository.Projections;

    [GenerateTestFactory]
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
            var response = _StudentApplicationService.DeleteStudent(new DeleteStudent.Request(
                CurrentPrincipalHelper.Name,
                new DeleteStudent.CommandModel { StudentId = id }));

            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            return RedirectToAction("Delete", new { id = id, saveChangesError = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStudentViewModel viewModel)
        {
            var response = _StudentApplicationService.CreateStudent(new CreateStudent.Request(CurrentPrincipalHelper.Name, viewModel.CommandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            ModelState.AddRange(response.ValidationDetails);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModifyStudent.CommandModel commandModel)
        {
            var response = _StudentApplicationService.ModifyStudent(new ModifyStudent.Request(CurrentPrincipalHelper.Name, commandModel));
            if (!response.HasValidationIssues)
                return RedirectToAction("Index");

            ModelState.AddRange(response.ValidationDetails);
            return View(commandModel);

        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ViewResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            return View(new ModifyStudent.CommandModel
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
