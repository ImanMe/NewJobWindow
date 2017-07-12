using JobWindowNew.DAL.Persistence.Helpers;
using JobWindowNew.Domain;
using JobWindowNew.Domain.ViewModels.Factories;
using JobWindowNew.Site.Helper;
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
            try
            {
                pageSize = pageSize > 100 ? 10 : pageSize;

                var query = _unitOfWork.JobRepository.GetJobWindowJobs();

                if (!string.IsNullOrEmpty(search))
                {
                    query = PersistenceHelper.SearchByKeywords(search, query);
                }

                if (!string.IsNullOrEmpty(location))
                {
                    query = PersistenceHelper.SearchByLocation(location, query);
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
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [AllowAnonymous]
        public IHttpActionResult Get(long id)
        {
            try
            {
                var job = _unitOfWork.JobRepository.GetJobWindowJob(id);

                if (job == null)
                {
                    return BadRequest();
                }

                var result = JobWindowFactory.Create(job);

                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/jobs/contact")]
        public IHttpActionResult PostEmail(string fromAddress, string subject, string body, string name, string city)
        {
            try
            {
                body = "Name: " + name + " - " + "City/Zip: " + " - " + city + " - " + "Message: " + body;
                LogicHelper.EmailSender(fromAddress, subject, body);
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
