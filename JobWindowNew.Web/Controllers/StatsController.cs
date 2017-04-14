using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using JobWindowNew.Domain.ViewModels.Factories;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JobWindowNew.Web.Controllers
{
    public class StatsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Stats
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create(string podIdFilter, string podIdSearch, int? page)
        {

            if (podIdSearch != null)
            {
                page = 1;
            }
            else
            {
                podIdSearch = podIdFilter;
            }

            ViewBag.PodIdFilter = podIdSearch;

            IQueryable<Job> query;

            if (string.IsNullOrEmpty(podIdSearch))
            {
                query = _unitOfWork.JobRepository.GetJobsWithStats();
            }
            else
            {
                if (string.IsNullOrEmpty(podIdSearch))
                {
                    podIdSearch = "";
                }

                query = _unitOfWork.JobRepository.GetJobsWithStats()
                    .Where(j => j.SchedulingPod.ToString() == podIdSearch);
            }

            var statsFactory = new StatsFactory();

            var result = query.ToList().Select(j => statsFactory.Create(j));

            var pageSize = 5;

            var pageNumber = (page ?? 1);

            return View(result.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(IEnumerable<JobStatsViewModel> viewModel)
        {
            foreach (var vm in viewModel)
            {
                if (vm.Bob != null && vm.ApsCl != null && vm.Intvs != null
                    && vm.Intvs2 != null)
                {
                    var job = _unitOfWork.JobRepository.GetJob(vm.Id);
                    job.ApsCl = vm.ApsCl;
                    job.Intvs = vm.Intvs;
                    job.Intvs2 = vm.Intvs2;
                    job.Bob = vm.Bob;
                    job.HasStats = true;
                    _unitOfWork.JobRepository.Update(job);
                    _unitOfWork.Complete();
                }
            }

            return Redirect("Create");
        }
    }

}