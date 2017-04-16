using AutoMapper;
using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using JobWindowNew.Web.Helpers;
using Microsoft.AspNet.Identity;
using PagedList;
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

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        //[HttpGet]
        public ActionResult Index(string sortOrder, string idFilter, string titleFilter, string podIdFilter, string idSearch, string titleSearch, string podIdSearch, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.IdParm = sortOrder == "Id" ? "-Id" : "Id";
                ViewBag.CloneFromParm = sortOrder == "CloneFrom" ? "-CloneFrom" : "CloneFrom";
                ViewBag.EverGreenIdParm = sortOrder == "EverGreenId" ? "-EverGreenId" : "EverGreenId";
                ViewBag.TitleParm = sortOrder == "Title" ? "-Title" : "Title";
                ViewBag.JobBoardParm = sortOrder == "JobBoard.JobBoardName" ? "-JobBoard.JobBoardName" : "JobBoard.JobBoardName";
                ViewBag.CityParm = sortOrder == "City" ? "-City" : "City";
                ViewBag.StateNameParm = sortOrder == "State.StateName" ? "-State.StateName" : "State.StateName";
                ViewBag.CountryNameParm = sortOrder == "Country.CountryName" ? "-Country.CountryName" : "Country.CountryName";
                ViewBag.CompanyNameParm = sortOrder == "CompanyName" ? "-CompanyName" : "CompanyName";
                ViewBag.SchedulingPodParm = sortOrder == "SchedulingPod" ? "-SchedulingPod" : "SchedulingPod";
                ViewBag.DivisionParm = sortOrder == "Division" ? "-Division" : "Division";
                ViewBag.ActivationDateParm = sortOrder == "ActivationDate" ? "-ActivationDate" : "ActivationDate";
                ViewBag.ExpirationDateParm = sortOrder == "ExpirationDate" ? "-ExpirationDate" : "ExpirationDate";
                ViewBag.CreatedByParm = sortOrder == "CreatedBy" ? "-CreatedBy" : "CreatedBy";
                ViewBag.CreatedDateParm = sortOrder == "CreatedDate" ? "-CreatedDate" : "CreatedDate";
                ViewBag.BobParm = sortOrder == "Bob" ? "-Bob" : "Bob";
                ViewBag.IntvsParm = sortOrder == "Intvs" ? "-Intvs" : "Intvs";
                ViewBag.Intvs2Parm = sortOrder == "Intvs2" ? "-Intvs2" : "Intvs2";
                ViewBag.ApsClParm = sortOrder == "ApsCl" ? "-ApsCl" : "ApsCl";
                sortOrder = ViewBag.CurrentSort;
                //if (string.IsNullOrEmpty(sortOrder))
                //{
                //    sortOrder = "Id";
                //}

                if (idSearch != null || titleSearch != null || podIdSearch != null)
                {
                    page = 1;
                }
                else
                {
                    idSearch = idFilter;
                    titleSearch = titleFilter;
                    podIdSearch = podIdFilter;
                }

                ViewBag.IdFilter = idSearch;
                ViewBag.TitleFilter = titleSearch;
                ViewBag.PodIdFilter = podIdSearch;

                if (string.IsNullOrEmpty(sortOrder))
                {
                    sortOrder = "Id";
                }

                IQueryable<Job> query;
                if (string.IsNullOrEmpty(idSearch) && string.IsNullOrEmpty(titleSearch) && string.IsNullOrEmpty(podIdSearch))
                {
                    query = _unitOfWork.JobRepository.GetJobsForGrid()
                          .ApplySort(sortOrder);
                }
                else
                {
                    if (string.IsNullOrEmpty(idSearch))
                    {
                        idSearch = "";
                    }

                    if (string.IsNullOrEmpty(titleSearch))
                    {
                        titleSearch = "";
                    }

                    if (string.IsNullOrEmpty(podIdSearch))
                    {
                        podIdSearch = "";
                    }

                    query = _unitOfWork.JobRepository.GetJobsForGrid()
                         .Where(j => j.Id.ToString().Contains(idSearch))
                            .Where(j => j.Title.ToString().Contains(titleSearch))
                            .Where(j => j.SchedulingPod.ToString().Contains(podIdSearch))
                            .ApplySort(sortOrder);
                }

                var mappedResult = query.Select(j => new JobGridViewModel
                {
                    Id = j.Id,
                    CloneFrom = j.CloneFrom,
                    EverGreenId = j.EverGreenId,
                    Title = j.Title,
                    JobBoard = j.JobBoard.JobBoardName,
                    City = j.City,
                    StateName = j.State.StateName,
                    CountryName = j.Country.CountryName,
                    CompanyName = j.CompanyName,
                    SchedulingPod = j.SchedulingPod,
                    Division = j.Division,
                    CreatedBy = j.CreatedBy,
                    Bob = j.Bob,
                    Intvs = j.Intvs,
                    Intvs2 = j.Intvs2,
                    ApsCl = j.ApsCl,
                    ActiveDate = j.ActivationDate,
                    ExpDate = j.ExpirationDate,
                    CreDate = j.CreatedDate
                });

                var pageSize = 15;
                var pageNumber = (page ?? 1);

                var finalResult = mappedResult.ToPagedList(pageNumber, pageSize);
                foreach (var item in finalResult)
                {
                    item.ActivationDate = item.ActiveDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    item.ExpirationDate = item.ExpDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    item.CreatedDate = item.CreDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    item.IsExpired = DateTime.Parse(item.ExpirationDate) < DateTime.Now;
                }

                return View(finalResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        //[HttpGet]
        public ActionResult Duplicates(string sortOrder, string idFilter, string titleFilter, string idSearch, string titleSearch, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.IdParm = sortOrder == "Id" ? "-Id" : "Id";
                ViewBag.CloneFromParm = sortOrder == "CloneFrom" ? "-CloneFrom" : "CloneFrom";
                ViewBag.EverGreenIdParm = sortOrder == "EverGreenId" ? "-EverGreenId" : "EverGreenId";
                ViewBag.TitleParm = sortOrder == "Title" ? "-Title" : "Title";
                ViewBag.JobBoardParm = sortOrder == "JobBoard.JobBoardName" ? "-JobBoard.JobBoardName" : "JobBoard.JobBoardName";
                ViewBag.CityParm = sortOrder == "City" ? "-City" : "City";
                ViewBag.StateNameParm = sortOrder == "State.StateName" ? "-State.StateName" : "State.StateName";
                ViewBag.CountryNameParm = sortOrder == "Country.CountryName" ? "-Country.CountryName" : "Country.CountryName";
                ViewBag.CompanyNameParm = sortOrder == "CompanyName" ? "-CompanyName" : "CompanyName";
                ViewBag.SchedulingPodParm = sortOrder == "SchedulingPod" ? "-SchedulingPod" : "SchedulingPod";
                ViewBag.DivisionParm = sortOrder == "Division" ? "-Division" : "Division";
                ViewBag.ActivationDateParm = sortOrder == "ActivationDate" ? "-ActivationDate" : "ActivationDate";
                ViewBag.ExpirationDateParm = sortOrder == "ExpirationDate" ? "-ExpirationDate" : "ExpirationDate";
                ViewBag.CreatedByParm = sortOrder == "CreatedBy" ? "-CreatedBy" : "CreatedBy";
                ViewBag.CreatedDateParm = sortOrder == "CreatedDate" ? "-CreatedDate" : "CreatedDate";
                ViewBag.BobParm = sortOrder == "Bob" ? "-Bob" : "Bob";
                ViewBag.IntvsParm = sortOrder == "Intvs" ? "-Intvs" : "Intvs";
                ViewBag.Intvs2Parm = sortOrder == "Intvs2" ? "-Intvs2" : "Intvs2";
                ViewBag.ApsClParm = sortOrder == "ApsCl" ? "-ApsCl" : "ApsCl";
                sortOrder = ViewBag.CurrentSort;

                if (idSearch != null || titleSearch != null)
                {
                    page = 1;
                }
                else
                {
                    idSearch = idFilter;
                    titleSearch = titleFilter;
                }

                ViewBag.IdFilter = idSearch;
                ViewBag.TitleFilter = titleSearch;

                IQueryable<Job> query;
                if (string.IsNullOrEmpty(idSearch) && string.IsNullOrEmpty(titleSearch))
                {
                    query = _unitOfWork.JobRepository.GetDuplicateJobs()
                          .ApplySort(sortOrder);
                }
                else
                {
                    if (string.IsNullOrEmpty(idSearch))
                    {
                        idSearch = "";
                    }

                    if (string.IsNullOrEmpty(titleSearch))
                    {
                        titleSearch = "";
                    }
                    query = _unitOfWork.JobRepository.GetDuplicateJobs()
                         .Where(j => j.Id.ToString().Contains(idSearch))
                            .Where(j => j.Title.ToString().Contains(titleSearch))
                            .ApplySort(sortOrder);
                }

                var mappedResult = query.Select(j => new JobGridViewModel
                {
                    Id = j.Id,
                    CloneFrom = j.CloneFrom,
                    EverGreenId = j.EverGreenId,
                    Title = j.Title,
                    JobBoard = j.JobBoard.JobBoardName,
                    City = j.City,
                    StateName = j.State.StateName,
                    CountryName = j.Country.CountryName,
                    CompanyName = j.CompanyName,
                    SchedulingPod = j.SchedulingPod,
                    Division = j.Division,
                    CreatedBy = j.CreatedBy,
                    Bob = j.Bob,
                    Intvs = j.Intvs,
                    Intvs2 = j.Intvs2,
                    ApsCl = j.ApsCl,
                    ActiveDate = j.ActivationDate,
                    ExpDate = j.ExpirationDate,
                    CreDate = j.CreatedDate
                });

                var pageSize = 15;
                var pageNumber = (page ?? 1);

                var finalResult = mappedResult.ToPagedList(pageNumber, pageSize);

                foreach (var item in finalResult)
                {
                    item.ActivationDate = item.ActiveDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    item.ExpirationDate = item.ExpDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    item.CreatedDate = item.CreDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    item.IsExpired = DateTime.Parse(item.ExpirationDate) < DateTime.Now;
                }

                return View(finalResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetStates(int countryId)
        {
            var states = _unitOfWork.StateRepository.GetStates(countryId);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new JobFormViewModel();
            viewModel.Action = "Create";
            viewModel = InitializeJobViewModel(viewModel);
            return View("JobForm", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
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

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
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

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
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

            return RedirectToAction("Index", "Jobs");
        }

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
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

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
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

            return RedirectToAction("Index", "Jobs");
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public ActionResult Delete(long id)
        {
            _unitOfWork.JobCategoryMapRepository.Delete(id);
            _unitOfWork.JobOccupationMapRepository.Delete(id);
            _unitOfWork.Complete();

            _unitOfWork.JobRepository.Delete(id);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
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
            if (viewModel.OccupationsSelected != null)
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
            viewModel.ActivationDate = job.ActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            viewModel.ExpirationDate = job.ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            viewModel.EmploymentTypeId = job.EmploymentTypeId;
            viewModel.SalaryTypeId = job.SalaryTypeId;
            viewModel.JobBoardId = job.JobBoardId;
            viewModel.StateId = job.StateId;
            viewModel.CountryId = job.CountryId;
            viewModel.Currency = job.Currency;
            viewModel.ApsCl = job.ApsCl;
            viewModel.Intvs = job.Intvs;
            viewModel.Intvs2 = job.Intvs2;
            viewModel.Bob = job.Bob;
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
            viewModel.CompanyName = job.CompanyName;
            viewModel.Division = job.Division;
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
            viewModel.ApsCl = job.ApsCl;
            viewModel.Intvs = job.Intvs;
            viewModel.Intvs2 = job.Intvs2;
            viewModel.Bob = job.Bob;
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
            job.ApsCl = viewModel.ApsCl;
            job.Intvs = viewModel.Intvs;
            job.Intvs2 = viewModel.Intvs2;
            job.Bob = viewModel.Bob;
        }
    }
}