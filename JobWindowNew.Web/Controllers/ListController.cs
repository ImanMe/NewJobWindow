using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using JobWindowNew.Web.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace JobWindowNew.Web.Controllers
{
    public class ListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: List
        public ActionResult Index()
        {
            var info = new PaginationInfoViewModel
            {
                SortField = "job.Id",
                SortDirection = "ascending",
                PageSize = 10,
                CurrentPage = 0,
            };

            info.TotalCount = Convert.ToInt32(Math.Ceiling((double)(_unitOfWork.JobRepository.GetJobsForJobList().Count() / info.PageSize)));
            info.TotalCount += 1;
            ViewBag.SortingPagingInfo = info;

            var query = _unitOfWork.JobRepository.GetJobsForJobList()
                .OrderBy(j => j.Job.Id).Take(info.PageSize).ToList();

            var mappedResult = query.Select(j => new JobGridViewModel
            {
                Id = j.Job.Id,
                CloneFrom = j.Job.CloneFrom,
                EverGreenId = j.Job.EverGreenId,
                Title = j.Job.Title,
                JobBoard = j.Job.JobBoard.JobBoardName,
                City = j.Job.City,
                StateName = j.Job.State.StateName,
                CountryName = j.Job.Country.CountryCode,
                CompanyName = j.Job.CompanyName,
                SchedulingPod = j.Job.SchedulingPod,
                Division = j.Job.Division,
                CreatedBy = j.Job.CreatedBy,
                IsExpired = j.Job.ExpirationDate < DateTime.Now,
                Category = j.Category.CategoryName,
                IsOnlineApply = j.Job.IsOnlineApply,
            });
            return View(mappedResult);
        }

        [HttpPost]
        public ActionResult Index(PaginationInfoViewModel info)
        {
            IQueryable<JobCategoryMap> query = null;

            query = _unitOfWork.JobRepository.GetJobsForJobList().OrderBy(j => j.Job.ExpirationDate);

            if (!string.IsNullOrEmpty(info.IdFilter))
            {
                query = query.Where(j => j.Job.Id.ToString().Contains(info.IdFilter));
            }

            if (!string.IsNullOrEmpty(info.TitleFilter))
            {
                query = query.Where(j => j.Job.Title.ToString().Contains(info.TitleFilter));
            }

            query = info.SortDirection == "ascending" ?
                query.ApplySort(info.SortField) :
                query.ApplySort("-" + info.SortField);

            ViewBag.SortingPagingInfo = info;
            info.TotalCount = Convert.ToInt32(Math.Ceiling((double)(query.Count() / info.PageSize)));

            var result = query.Skip(info.CurrentPage
                               * info.PageSize).Take(info.PageSize).ToList();

            var mappedResult = result?.Select(j => new JobGridViewModel
            {
                Id = j.Job.Id,
                CloneFrom = j.Job.CloneFrom,
                EverGreenId = j.Job.EverGreenId,
                Title = j.Job.Title,
                JobBoard = j.Job.JobBoard.JobBoardName,
                City = j.Job.City,
                StateName = j.Job.State.StateName,
                CountryName = j.Job.Country.CountryCode,
                CompanyName = j.Job.CompanyName,
                SchedulingPod = j.Job.SchedulingPod,
                Division = j.Job.Division,
                CreatedBy = j.Job.CreatedBy,
                IsExpired = j.Job.ExpirationDate < DateTime.Now,
                Category = j.Category.CategoryName,
                IsOnlineApply = j.Job.IsOnlineApply,
            });

            return View(mappedResult);
        }
    }
}