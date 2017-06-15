using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Globalization;
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
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
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

            var mappedResult = query.Select(j => new JobStatsViewModel
            {
                Id = j.Id,
                Title = j.Title,
                TitleTruncated = j.Title.Substring(0, 20),
                SchedulingPod = j.SchedulingPod,
                JobBoard = j.JobBoard.JobBoardName,
                ApsCl = j.ApsCl,
                Intvs = j.Intvs,
                Intvs2 = j.Intvs2,
                Bob = j.Bob,
                ExpDate = j.ExpirationDate,
                ActiveDate = j.ActivationDate
            });

            var pageSize = 200;
            var pageNumber = (page ?? 1);

            var finalResult = mappedResult.ToPagedList(pageNumber, pageSize);
            foreach (var item in finalResult)
            {
                item.ActivationDate = item.ActiveDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                item.ExpirationDate = item.ExpDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }

            return View(finalResult);
        }

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
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