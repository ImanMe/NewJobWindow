using JobWindowNew.Domain;
using JobWindowNew.Domain.ViewModels.Factories;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace JobWindowNew.Site.Controllers
{
    public class JobsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public IHttpActionResult Get(int page = 1, int pageSize = 10, string search = "", string location = "")
        {
            pageSize = pageSize > 100 ? 10 : pageSize;

            var query = _unitOfWork.JobRepository.GetJobWindowJobs();

            if (!string.IsNullOrEmpty(search))
            {
                var searchWords = search.ToLower().Split(new[] { ' ', ',', '.', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (query.Any(j => j.Title.Contains(search)))
                {
                    query = query.Where(j => j.Title.Contains(search));
                }
                else if (query.Any(j => searchWords.All(c => j.Title.Contains(c))))
                {
                    query = query.Where(j => searchWords.All(c => j.Title.Contains(c)));
                }
                else if (query.Any(j => searchWords.Any(c => j.Title.Contains(c))))
                {
                    query = query.Where(j => searchWords.Any(c => j.Title.Contains(c)));
                }
            }

            if (!string.IsNullOrEmpty(location))
            {
                var locationWords = location.ToLower()
                    .Split(new[] { ' ', ',', '.', '-', '_' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (query.Any(j => locationWords.Any(c => j.City.Contains(c))))
                {
                    query = query.Where(j => locationWords.Any(c => j.City.Contains(c)));
                }

                else if (query.Any(j => locationWords.Any(c => j.State.StateName.Contains(c))))
                {
                    query = query.Where(j => locationWords.Any(c => j.State.StateName.Contains(c)));
                }

                else if (query.Any(j => locationWords.Any(c => j.Country.CountryName.Contains(c))))
                {
                    query = query.Where(j => locationWords.Any(c => j.Country.CountryName.Contains(c)));
                }

                else if (query.Any(j => locationWords.Any(c => j.State.StateCode.Contains(c))))
                {
                    query = query.Where(j => locationWords.Any(c => j.State.StateCode.Contains(c)));
                }

                else if (query.Any(j => locationWords.Any(c => j.Country.CountryCode.Contains(c))))
                {
                    query = query.Where(j => locationWords.Any(c => j.Country.CountryCode.Contains(c)));
                }
            }

            //Total Records
            var totalCount = query.Count();

            //Total Pages
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            //Header Object
            var paginationHeader = new
            {
                numberOfJobs = totalCount,
                numberOfPages = totalPages,
            };

            //Serializing the pagination Object
            HttpContext.Current.Response.Headers.Add("x-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            return Ok(query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList()
                .Select(JobWindowFactory.Create));
        }

        [AllowAnonymous]
        public IHttpActionResult Get(long id)
        {
            var job = _unitOfWork.JobRepository.GetJobWindowJob(id);
            var result = JobWindowFactory.Create(job);
            return Ok(result);
        }
    }
}
