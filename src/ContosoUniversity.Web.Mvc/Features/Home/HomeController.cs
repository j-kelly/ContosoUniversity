namespace ContosoUniversity.Controllers
{
    using ContosoUniversity.Domain.Core.Repository;
    using NRepository.Core.Query;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Mvc;
    using Web.Core.Repository.Projections;
    public class HomeController : Controller
    {
        private readonly IQueryRepository _QueryRepository;

        public HomeController(IQueryRepository queryRepository)
        {
            _QueryRepository = queryRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // Using query interceptor which uses descrete sql
            var enrollmentProjections = _QueryRepository.GetEntities<EnrollmentDateGroup>();
            return View(enrollmentProjections.ToArray());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}