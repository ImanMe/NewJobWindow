using JobWindowNew.Domain;
using JobWindowNew.Domain.ViewModels;
using System.Web.Mvc;

namespace JobWindowNew.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult OnlineApply(long id)
        {
            var job = _unitOfWork.JobRepository.GetJobForOnlineApply(id);

            if (job == null)
            {
                return HttpNotFound();
            }

            var viewModel = new JobOnlineApplyViewModel
            {
                Id = job.Id,
                Title = job.Title,
                JobRequirements = job.JobRequirements,
                JobDescription = job.JobDescription,
                City = job.City,
                StateName = job.State?.StateName,
                CountryName = job.Country?.CountryName,
                EmploymentType = job.EmploymentType?.Type
            };



            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult OnlineApply(JobOnlineApplyViewModel viewModel)
        {
            var job = _unitOfWork.JobRepository.GetJobForOnlineApply(viewModel.Id);

            if (!ModelState.IsValid)
            {
                viewModel.Id = job.Id;
                viewModel.Title = job.Title;
                viewModel.JobRequirements = job.JobRequirements;
                viewModel.JobDescription = job.JobDescription;
                viewModel.City = job.City;
                viewModel.StateName = job.State?.StateName;
                viewModel.CountryName = job.Country?.CountryName;
                viewModel.EmploymentType = job.EmploymentType?.Type;
                return View(viewModel);
            }

            return null;
        }
    }
}