using AutoMapper;
using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Globalization;
using System.Linq;
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
            var viewModel = new JobFormViewModel();
            viewModel.Action = "Create";
            viewModel = InitializeJobViewModel(viewModel);
            return View("JobForm", viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(JobFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel = InitializeJobViewModel(viewModel);
                return View("JobForm", viewModel);
            }

            viewModel.Action = "Create";
            viewModel.CreatedBy = User.Identity.GetUserName();
            viewModel.CreatedDate = DateTime.Now;

            var job = Mapper.Map<Job>(viewModel);

            _unitOfWork.JobRepository.Add(job);
            _unitOfWork.Complete();

            PopulateMappingEntities(viewModel, job);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var job = _unitOfWork.JobRepository.GetJob(id);

            if (job == null)
            {
                return HttpNotFound();
            }

            var currentUser = User.Identity.GetUserName();

            var categories = _unitOfWork.JobCategoryMapRepository.GetCategoriesForJob(id).ToList();

            var occupations = _unitOfWork.JobOccupationMapRepository.GetOccupationForJob(id).ToList();

            var viewModel = new JobFormViewModel
            {
                Action = "Edit",
                Heading = "Edit",
                CategoriesSelected = categories.ToArray(),
                OccupationsSelected = occupations.ToArray()
            };

            PopulateViewModelForEdit(viewModel, job, currentUser);

            return View("JobForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(JobFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel = InitializeJobViewModel(viewModel);
                return View("JobForm", viewModel);
            }

            var job = _unitOfWork.JobRepository.GetJob(viewModel.Id);

            if (job == null)
            {
                return HttpNotFound();
            }

            var currentUser = User.Identity.GetUserName();
            var currentDate = DateTime.Now;

            PopulateJobFromViewModel(viewModel, job, currentUser, currentDate);

            _unitOfWork.JobRepository.Update(job);
            _unitOfWork.Complete();

            _unitOfWork.JobCategoryMapRepository.Delete(job.Id);
            _unitOfWork.JobOccupationMapRepository.Delete(job.Id);

            _unitOfWork.Complete();

            PopulateMappingEntities(viewModel, job);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Clone(int id)
        {
            var job = _unitOfWork.JobRepository.GetJob(id);

            if (job == null)
            {
                return HttpNotFound();
            }

            var currentUser = User.Identity.GetUserName();

            var categories = _unitOfWork.JobCategoryMapRepository
                .GetCategoriesForJob(id).ToList();

            var occupations = _unitOfWork.JobOccupationMapRepository
               .GetOccupationForJob(id).ToList();

            var viewModel = new JobFormViewModel
            {
                Action = "Clone",
                Heading = "Clone",
                CategoriesSelected = categories.ToArray(),
                OccupationsSelected = occupations.ToArray()
            };

            PopulateViewModelForClone(viewModel, job, currentUser);

            return View("JobForm", viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Clone(JobFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel = InitializeJobViewModel(viewModel);
                return View("JobForm", viewModel);
            }

            var job = _unitOfWork.JobRepository.GetJob(viewModel.Id);

            if (job == null)
            {
                return HttpNotFound();
            }

            var currentUser = User.Identity.GetUserName();
            var currentDate = DateTime.Now;

            PopulateJobFromViewModel(viewModel, job, currentUser, currentDate);

            job.CloneFrom = job.Id;

            if (job.IsEverGreen && job.EverGreenId == null)
            {
                job.EverGreenId = job.Id;
            }

            _unitOfWork.JobRepository.Add(job);

            _unitOfWork.Complete();

            PopulateMappingEntities(viewModel, job);

            return RedirectToAction("Index", "Home");
        }

        private JobFormViewModel InitializeJobViewModel(JobFormViewModel viewModel)
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

            return viewModel;
        }
        private void PopulateMappingEntities(JobFormViewModel viewModel, Job job)
        {
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
        }
        private void PopulateViewModelForEdit(JobFormViewModel viewModel, Job job, string currentUser)
        {
            viewModel.Title = job.Title;
            viewModel.JobDescription = job.JobDescription;
            viewModel.JobRequirements = job.JobRequirements;
            viewModel.Salary = job.Salary;
            viewModel.City = job.City;
            viewModel.Address = job.Address;
            viewModel.ZipCode = job.ZipCode;
            viewModel.MinimumExperience = job.MinimumExperience;
            viewModel.MaximumExperience = job.MaximumExperience;
            viewModel.SchedulingPod = job.SchedulingPod;
            viewModel.OfficeId = job.OfficeId;
            viewModel.Author = job.Author;
            viewModel.EmailTo = job.EmailTo;
            viewModel.IsBestPerforming = job.IsBestPerforming;
            viewModel.IsEverGreen = job.IsEverGreen;
            viewModel.IsOnlineApply = job.IsOnlineApply;
            viewModel.EditedDate = DateTime.Now;
            viewModel.CompanyName = job.CompanyName;
            viewModel.Division = job.Division;
            viewModel.EditedBy = currentUser;
            viewModel.SalaryTypes = _unitOfWork.SalaryTypeRepository.GetSalaryTypes();
            viewModel.EmploymentTypes = _unitOfWork.EmploymentTypeRepository.GetEmploymentTypes();
            viewModel.JobBoards = _unitOfWork.JobBoardRepository.GetJobBoards();
            viewModel.Countries = _unitOfWork.CountryRepository.GetCountries();
            viewModel.States = _unitOfWork.StateRepository.GetStates();
            viewModel.Categories = _unitOfWork.CategoryRepository.GetCategories();
            viewModel.Occupations = _unitOfWork.OccupationRepository.GetOccupations();
            viewModel.Currencies = new[] { "USD", "CAD" };

            if (job.ActivationDate != null)
            {
                var convertedActivationDate = (DateTime)job.ActivationDate;
                viewModel.ActivationDate = convertedActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }

            if (job.ExpirationDate != null)
            {
                var convertedExpirationDate = (DateTime)job.ExpirationDate;
                viewModel.ExpirationDate = convertedExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }

            viewModel.EmploymentTypeId = job.EmploymentTypeId;
            viewModel.SalaryTypeId = job.SalaryTypeId;
            viewModel.JobBoardId = job.JobBoardId;
            viewModel.StateId = job.StateId;
            viewModel.CountryId = job.CountryId;
            viewModel.Currency = job.Currency;
        }
        private void PopulateViewModelForClone(JobFormViewModel viewModel, Job job, string currentUser)
        {
            viewModel.Title = job.Title;
            viewModel.JobDescription = job.JobDescription;
            viewModel.JobRequirements = job.JobRequirements;
            viewModel.Salary = job.Salary;
            viewModel.City = job.City;
            viewModel.Address = job.Address;
            viewModel.ZipCode = job.ZipCode;
            viewModel.MinimumExperience = job.MinimumExperience;
            viewModel.MaximumExperience = job.MaximumExperience;
            viewModel.SchedulingPod = job.SchedulingPod;
            viewModel.OfficeId = job.OfficeId;
            viewModel.Author = job.Author;
            viewModel.EmailTo = job.EmailTo;
            viewModel.IsBestPerforming = job.IsBestPerforming;
            viewModel.IsEverGreen = job.IsEverGreen;
            viewModel.IsOnlineApply = job.IsOnlineApply;
            viewModel.EditedDate = DateTime.Now;
            viewModel.CompanyName = job.CompanyName;
            viewModel.Division = job.Division;
            viewModel.EditedBy = currentUser;
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

            viewModel.EmploymentTypeId = job.EmploymentTypeId;
            viewModel.SalaryTypeId = job.SalaryTypeId;
            viewModel.JobBoardId = job.JobBoardId;
            viewModel.StateId = job.StateId;
            viewModel.CountryId = job.CountryId;
            viewModel.Currency = job.Currency;
        }
        private static void PopulateJobFromViewModel(JobFormViewModel viewModel, Job job, string currentUser,
            DateTime currentDate)
        {
            job.Id = viewModel.Id;
            job.Title = viewModel.Title;
            job.JobDescription = viewModel.JobDescription;
            job.JobRequirements = viewModel.JobRequirements;
            job.EmploymentTypeId = viewModel.EmploymentTypeId;
            job.Salary = viewModel.Salary;
            job.Currency = viewModel.Currency;
            job.SalaryTypeId = viewModel.SalaryTypeId;
            job.CountryId = viewModel.CountryId;
            job.StateId = viewModel.StateId;
            job.City = viewModel.City;
            job.ZipCode = viewModel.ZipCode;
            job.Address = viewModel.Address;
            job.MinimumExperience = viewModel.MinimumExperience;
            job.MaximumExperience = viewModel.MaximumExperience;
            job.CompanyName = viewModel.CompanyName;
            job.ActivationDate = viewModel.GetActivationDate();
            job.ExpirationDate = viewModel.GetExpirationDate();
            job.SchedulingPod = viewModel.SchedulingPod;
            job.OfficeId = viewModel.OfficeId;
            job.Division = viewModel.Division;
            job.JobBoardId = viewModel.JobBoardId;
            job.Author = viewModel.Author;
            job.EmailTo = viewModel.EmailTo;
            job.IsBestPerforming = viewModel.IsBestPerforming;
            job.IsEverGreen = viewModel.IsEverGreen;
            job.IsOnlineApply = viewModel.IsOnlineApply;
            job.EditedBy = currentUser;
            job.EditedDate = currentDate;
        }
    }
}