using JobWindowNew.DAL.Persistence.Helpers;
using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using JobWindowNew.Domain.ViewModels.Factories;
using JobWindowNew.Web.Helpers;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace JobWindowNew.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Reports
        [HttpGet]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void EverGreenReport()
        {
            var factory = new EverGreenReportFactory();

            var query = _unitOfWork.JobRepository.GetJobsForEverGreenReport();
            var result = query.ToList().Select(j => factory.Create(j));

            WebHelper.ImportToExcel(result, Response, "EverGreen");
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public ActionResult ActiveReport()
        {
            var viewModel = new ReportGetViewModel
            {
                JobBoards = _unitOfWork.JobBoardRepository.GetJobBoards(),
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public ActionResult CreatedByReport()
        {
            var viewModel = new CreatedByReportViewModel
            {
                FromDate = DateTime.Now.AddMonths(-1)
                    .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ToDate = DateTime.Now
                    .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                Users = _unitOfWork.UserRepository.GetUsers()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void ActiveReport(ReportGetViewModel viewModel)
        {
            var factory = new EverGreenReportFactory();

            var query = _unitOfWork.JobRepository
                .GetJobsForActiveReport();

            if (viewModel.PodId != 0)
            {
                query = query.Where(j => j.SchedulingPod == viewModel.PodId);
            }

            if (viewModel.JobBoardId != 0)
            {
                query = query.Where(j => j.JobBoardId == viewModel.JobBoardId);
            }

            query = query
                .OrderBy(j => j.SchedulingPod)
                .ThenBy(j => j.JobBoard.JobBoardName)
                .ThenBy(j => j.City)
                .ThenBy(j => j.Category.CategoryName)
                .ThenBy(j => j.ExpirationDate)
                .ThenByDescending(j => j.ApsCl)
                .ThenByDescending(j => j.Bob)
                .ThenByDescending(j => j.Intvs2)
                .ThenByDescending(j => j.Intvs);


            var result = query.ToList().Select(j => factory.Create(j));


            WebHelper.ImportToExcel(result, Response, "ActiveReport");
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void InActiveReport(InactiveGetViewModel viewModel)
        {
            var pId = viewModel.PodId;

            var fromDate = DateTime.Parse($"{viewModel.FromDate}");
            var toDate = DateTime.Parse($"{viewModel.ToDate}");
            var jobBoardId = viewModel.JobBoardId;

            var factory = new EverGreenReportFactory();

            var query = _unitOfWork.JobRepository
                .GetJobsForInActiveReport();

            if (jobBoardId != 0)
            {
                query = query.Where(j => j.JobBoardId == jobBoardId);
            }

            if (pId != 0)
            {
                query = query.Where(j => j.SchedulingPod == pId);
            }

            query = query.Where(j => j.ExpirationDate >= fromDate)
                    .Where(j => j.ExpirationDate < toDate);

            var result = query.ToList()
                    .Select(j => factory.Create(j));

            WebHelper.ImportToExcel(result, Response, "InActiveReport");

        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void CreatedByReport(CreatedByReportViewModel viewModel)
        {
            var factory = new EverGreenReportFactory();

            var user = viewModel.UserName;
            var fromDate = DateTime.Parse($"{viewModel.FromDate}");
            var toDate = DateTime.Parse($"{viewModel.ToDate}");

            IQueryable<Job> query;
            if (string.IsNullOrEmpty(user))
            {
                query = _unitOfWork.JobRepository
                    .GetJobs()
                    .Where(j => j.CreatedDate <= toDate)
                    .Where(j => j.CreatedDate >= fromDate);
            }
            else
            {
                query = _unitOfWork.JobRepository
                    .GetJobs()
                    .Where(j => j.CreatedBy == user)
                    .Where(j => j.CreatedDate <= toDate)
                    .Where(j => j.CreatedDate >= fromDate);
            }

            query = PersistenceHelper.SortForJobList(query);

            var result = query.ToList().Select(j => factory.Create(j));

            WebHelper.ImportToExcel(result, Response, "CreatedBy");
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public ActionResult InActiveReport()
        {
            var viewModel = new InactiveGetViewModel
            {
                FromDate = DateTime.Now.AddMonths(-1)
                    .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ToDate = DateTime.Now
                .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                JobBoards = _unitOfWork.JobBoardRepository.GetJobBoards(),
            };
            return View(viewModel);
        }


    }
}