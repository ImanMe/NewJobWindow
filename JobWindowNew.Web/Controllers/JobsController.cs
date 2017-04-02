using AutoMapper;
using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace JobWindowNew.Web.Controllers
{
    public class JobsController : Controller
    {
        // GET: Jobs
        private readonly IUnitOfWork _unitOfWork;

        public JobsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetStates(int countryId)
        {
            var states = _unitOfWork.StateRepository.GetStates(countryId);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new JobFormViewModel
            {
                Heading = "Create",
                SalaryTypes = _unitOfWork.SalaryTypeRepository.GetSalaryTypes(),
                EmploymentTypes = _unitOfWork.EmploymentTypeRepository.GetEmploymentTypes(),
                JobBoards = _unitOfWork.JobBoardRepository.GetJobBoards(),
                Countries = _unitOfWork.CountryRepository.GetCountries(),
                States = _unitOfWork.StateRepository.GetStates(),
                Categories = _unitOfWork.CategoryRepository.GetCategories(),
                Occupations = _unitOfWork.OccupationRepository.GetOccupations(),
                Currencies = new[] { "USD", "CAD" },
                ActivationDate = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.Now.AddMonths(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(JobFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Heading = "Create";
                viewModel.SalaryTypes = _unitOfWork.SalaryTypeRepository.GetSalaryTypes();
                viewModel.EmploymentTypes = _unitOfWork.EmploymentTypeRepository.GetEmploymentTypes();
                viewModel.JobBoards = _unitOfWork.JobBoardRepository.GetJobBoards();
                viewModel.Countries = _unitOfWork.CountryRepository.GetCountries();
                viewModel.States = _unitOfWork.StateRepository.GetStates();
                viewModel.Categories = _unitOfWork.CategoryRepository.GetCategories();
                viewModel.Occupations = _unitOfWork.OccupationRepository.GetOccupations();
                viewModel.Currencies = new[] { "USD", "CAD" };
                viewModel.ActivationDate = DateTime.Now
                    .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                viewModel.ExpirationDate = DateTime.Now.AddMonths(1)
                    .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                return View(viewModel);
            }

            viewModel.CreatedBy = User.Identity.GetUserName();
            viewModel.CreatedDate = DateTime.Now;

            var job = Mapper.Map<Job>(viewModel);

            _unitOfWork.JobRepository.Add(job);
            _unitOfWork.Complete();

            foreach (var occupationId in viewModel.OccupationsSelected)
            {
                var jobOccupationMap = new JobOccupationMap()
                {
                    JobId = job.Id,
                    OccupationId = occupationId
                };

                _unitOfWork.JobOccupationMapRepository.Add(jobOccupationMap);
            }

            foreach (var categoryId in viewModel.CategoriesSelected)
            {
                var jobCategoryMap = new JobCategoryMap()
                {
                    JobId = job.Id,
                    CategoryId = categoryId
                };

                _unitOfWork.JobCategoryMapRepository.Add(jobCategoryMap);
            }

            _unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }
    }
}