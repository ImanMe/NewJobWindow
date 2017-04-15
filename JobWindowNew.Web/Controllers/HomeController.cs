using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using System;
using System.IO;
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
        [AllowAnonymous]
        public ActionResult OnlineApply(JobOnlineApplyViewModel viewModel)
        {
            var job = _unitOfWork.JobRepository.GetJobForOnlineApply(viewModel.Id);
            if (job == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                viewModel.JobId = job.Id;
                viewModel.Title = job.Title;
                viewModel.JobRequirements = job.JobRequirements;
                viewModel.JobDescription = job.JobDescription;
                viewModel.City = job.City;
                viewModel.StateName = job.State?.StateName;
                viewModel.CountryName = job.Country?.CountryName;
                viewModel.EmploymentType = job.EmploymentType?.Type;
                return View(viewModel);
            }

            var applicant = new Applicant
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                Date = DateTime.Now,
                JobId = viewModel.Id,
                //FileName = viewModel.FileName ?? string.Empty
            };



            var n1 = viewModel.Id.ToString();
            var n2 = applicant.Id.ToString();

            foreach (string upload in Request.Files)
            {
                var httpPostedFileBase = Request.Files[upload];
                if (httpPostedFileBase != null && httpPostedFileBase.ContentLength == 0)
                {
                    applicant.FileName = "NoResume.txt";
                }
                else
                {
                    var pathToSave = Server.MapPath("~/Resumes/");
                    var filename = Path.GetFileName(Request.Files[upload]?.FileName);
                    var formattedFileName = string.Format("{1}{2}"
                        , Path.GetFileNameWithoutExtension(filename)
                        //, Guid.NewGuid().ToString("N")
                        , n1 + "-" + n2
                        , Path.GetExtension(filename));
                    Request.Files[upload]?.SaveAs(Path.Combine(pathToSave, formattedFileName));
                    //n3 = formattedFileName;
                    applicant.FileName = formattedFileName;
                }


                _unitOfWork.ApplicantRepository.Add(applicant);
                _unitOfWork.Complete();
            }
            return RedirectToAction("ThankYou", new { id = n1 });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Thankyou(long id)
        {
            var job = _unitOfWork.JobRepository.GetJob(id);

            if (job == null)
            {
                return HttpNotFound();
            }

            var viewModel = new JobOnlineApplyViewModel { Title = job.Title };

            return View(viewModel);
        }
    }
}