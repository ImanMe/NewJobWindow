using AutoMapper;
using JobWindowNew.Domain;
using JobWindowNew.Domain.ViewModels;
using JobWindowNew.Domain.ViewModels.Factories;
using JobWindowNew.Web.Helpers;
using System.Collections.Generic;
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

        public ActionResult Index()
        {
            var totalNumberOfJobs = _unitOfWork.JobRepository
                .GetJobs().Count();

            var info = PaginationInfoFactory.Create(totalNumberOfJobs);

            ViewBag.SortingPagingInfo = info;

            var query = _unitOfWork.JobRepository.GetJobsForList()
                .Take(info.PageSize).ToList();

            var result = Mapper.Map<IEnumerable<ImanJobs>>(query);

            return View(result);
        }

        [HttpPost]
        public ActionResult Index(PaginationInfoViewModel info)
        {
            var query = _unitOfWork.JobRepository.GetJobsForList();

            if (!string.IsNullOrEmpty(info.IdFilter))
            {
                query = query.Where(j => j.Id.ToString().Contains(info.IdFilter));
            }

            if (!string.IsNullOrEmpty(info.TitleFilter))
            {
                query = query.Where(j => j.Title.ToString().Contains(info.TitleFilter));
            }

            query = WebHelper.SortResult(info, query);

            info.TotalPages = WebHelper.GetNumberOfPages(info, query);

            ViewBag.SortingPagingInfo = info;

            var result = query.Skip(info.CurrentPage
                               * info.PageSize).Take(info.PageSize).ToList();

            return View(Mapper.Map<IEnumerable<ImanJobs>>(result));
        }


    }
}