using JobWindowNew.Domain;
using System;
using System.Web.Http;

namespace JobWindowNew.Web.Controllers.api
{
    [AllowAnonymous]
    public class JobsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobsController()
        {

        }

        public JobsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                _unitOfWork.JobOccupationMapRepository.Delete(id);
                _unitOfWork.JobRepository.Delete(id);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Expire([FromUri]long id)
        {
            try
            {
                var job = _unitOfWork.JobRepository.GetJob(id);

                if (job.ExpirationDate > DateTime.Now)
                {
                    job.ExpirationDate = DateTime.Now.AddDays(-1);
                }

                _unitOfWork.JobRepository.Update(job);

                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
