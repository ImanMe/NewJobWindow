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
                SortField = "Id",
                SortDirection = "ascending",
                PageSize = 10,
                CurrentPage = 0,
            };

            var totalNumberOfPages = _unitOfWork.JobRepository.GetJobs().Count();
            info.TotalCount = Convert.ToInt32(Math.Ceiling((double)(totalNumberOfPages / info.PageSize)));
            info.TotalCount += 1;
            ViewBag.SortingPagingInfo = info;

            var query = _unitOfWork.JobRepository.GetJobs()
                .OrderBy(j => j.Id).Take(info.PageSize).ToList();

            var mappedResult = query.Select(j => new JobGridViewModel
            {
                Id = j.Id,
                CloneFrom = j.CloneFrom,
                EverGreenId = j.EverGreenId,
                Title = j.Title,
                JobBoard = j.JobBoard.JobBoardName,
                City = j.City,
                StateName = j.State.StateName,
                CountryName = j.Country.CountryCode,
                CompanyName = j.CompanyName,
                SchedulingPod = j.SchedulingPod,
                Division = j.Division,
                CreatedBy = j.CreatedBy,
                IsExpired = j.ExpirationDate < DateTime.Now,
                Category = j.Category.CategoryName,
                IsOnlineApply = j.IsOnlineApply,
            });
            return View(mappedResult);
        }

        [HttpPost]
        public ActionResult Index(PaginationInfoViewModel info)
        {
            IQueryable<Job> query = null;

            query = _unitOfWork.JobRepository.GetJobs().OrderBy(j => j.ExpirationDate);

            if (!string.IsNullOrEmpty(info.IdFilter))
            {
                query = query.Where(j => j.Id.ToString().Contains(info.IdFilter));
            }

            if (!string.IsNullOrEmpty(info.TitleFilter))
            {
                query = query.Where(j => j.Title.ToString().Contains(info.TitleFilter));
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
                Id = j.Id,
                CloneFrom = j.CloneFrom,
                EverGreenId = j.EverGreenId,
                Title = j.Title,
                JobBoard = j.JobBoard.JobBoardName,
                City = j.City,
                StateName = j.State.StateName,
                CountryName = j.Country.CountryCode,
                CompanyName = j.CompanyName,
                SchedulingPod = j.SchedulingPod,
                Division = j.Division,
                CreatedBy = j.CreatedBy,
                IsExpired = j.ExpirationDate < DateTime.Now,
                Category = j.Category.CategoryName,
                IsOnlineApply = j.IsOnlineApply,
            });

            return View(mappedResult);
        }

    }

}