using AutoMapper;
using JobWindowNew.DAL.Persistence.Helpers;
using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using JobWindowNew.Web.Helpers;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


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
        public ActionResult Index(string sortOrder, string idFilter, string titleFilter, string podIdFilter,
            string idSearch, string titleSearch,
            string cityFilter, string countryFilter, string categoryFilter, string stateFilter,
            string jobBoardFilter, string divisionFilter,
            string companyFilter, string statusSearch, string statusFilter,
            string citySearch, string countrySearch, string categorySearch, string stateSearch,
            string companySearch, string divisionSearch, string jobBoardSearch,
            string podIdSearch, int? page)
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
                ViewBag.CategoryNameParm = sortOrder == "Category.CategoryName" ? "-Category.CategoryName" : "Category.CategoryName";
                ViewBag.CompanyNameParm = sortOrder == "CompanyName" ? "-CompanyName" : "CompanyName";
                ViewBag.SchedulingPodParm = sortOrder == "SchedulingPod" ? "-SchedulingPod" : "SchedulingPod";
                ViewBag.DivisionParm = sortOrder == "Division" ? "-Division" : "Division";
                ViewBag.ActivationDateParm = sortOrder == "ActivationDate" ? "-ActivationDate" : "ActivationDate";
                ViewBag.ExpirationDateParm = sortOrder == "ExpirationDate" ? "-ExpirationDate" : "ExpirationDate";
                ViewBag.CreatedByParm = sortOrder == "CreatedBy" ? "-CreatedBy" : "CreatedBy";
                ViewBag.CreatedDateParm = sortOrder == "CreatedDate" ? "-CreatedDate" : "CreatedDate";
                ViewBag.EmailParm = sortOrder == "EmailTo" ? "-EmailTo" : "EmailTo";
                ViewBag.OfficeParm = sortOrder == "OfficeId" ? "-OfficeId" : "OfficeId";
                ViewBag.BobParm = sortOrder == "Bob" ? "-Bob" : "Bob";
                ViewBag.IntvsParm = sortOrder == "Intvs" ? "-Intvs" : "Intvs";
                ViewBag.Intvs2Parm = sortOrder == "Intvs2" ? "-Intvs2" : "Intvs2";
                ViewBag.ApsClParm = sortOrder == "ApsCl" ? "-ApsCl" : "ApsCl";
                sortOrder = ViewBag.CurrentSort;

                if (idSearch != null || titleSearch != null || podIdSearch != null || citySearch != null ||
                    stateSearch != null || countrySearch != null || categorySearch != null || jobBoardSearch != null || companySearch != null ||
                    divisionSearch != null || statusSearch != null)
                {
                    page = 1;
                }
                else
                {
                    idSearch = idFilter;
                    titleSearch = titleFilter;
                    podIdSearch = podIdFilter;
                    citySearch = cityFilter;
                    countrySearch = countryFilter;
                    categorySearch = categoryFilter;
                    stateSearch = stateFilter;
                    divisionSearch = divisionFilter;
                    companySearch = companyFilter;
                    jobBoardSearch = jobBoardFilter;
                    statusSearch = statusFilter;
                }

                ViewBag.IdFilter = idSearch;
                ViewBag.TitleFilter = titleSearch;
                ViewBag.PodIdFilter = podIdSearch;
                ViewBag.CityFilter = citySearch;
                ViewBag.CountryFilter = countrySearch;
                ViewBag.CategoryFilter = categorySearch;
                ViewBag.StateFilter = stateSearch;
                ViewBag.DivisionFilter = divisionSearch;
                ViewBag.CompanyFilter = companySearch;
                ViewBag.JobBoardFilter = jobBoardSearch;
                ViewBag.StatusFilter = statusSearch;

                var query = _unitOfWork.JobRepository.GetJobs();

                query = PersistenceHelper.FilterByInput(idSearch, titleSearch, podIdSearch,
                    citySearch, stateSearch, countrySearch, categorySearch, jobBoardSearch,
                    divisionSearch, companySearch, statusSearch, query);

                query = PersistenceHelper.SortForJobList(query);

                query = query.ApplySort(sortOrder);

                var mappedResult = query.Select(j => new JobGridViewModel
                {
                    Id = j.Id,
                    CloneFrom = j.CloneFrom,
                    EverGreenId = j.EverGreenId,
                    Title = j.Title,
                    TitleTruncated = j.Title.Substring(0, 20),
                    JobBoard = j.JobBoard.JobBoardName,
                    City = j.City,
                    StateName = j.State.StateName,
                    CountryName = j.Country.CountryCode,
                    CompanyName = j.CompanyName,
                    SchedulingPod = j.SchedulingPod,
                    Division = j.Division,
                    CreatedBy = j.CreatedBy,
                    CreatedByTruncated = j.CreatedBy.Substring(0, 10),
                    EmailTo = j.EmailTo,
                    EmailToTruncated = j.EmailTo.Substring(0, 10),
                    OfficeId = j.OfficeId,
                    Bob = j.Bob,
                    Intvs = j.Intvs,
                    Intvs2 = j.Intvs2,
                    ApsCl = j.ApsCl,
                    ActiveDate = j.ActivationDate,
                    ExpDate = j.ExpirationDate,
                    CreDate = j.CreatedDate,
                    IsExpired = j.ExpirationDate < DateTime.Now,
                    Category = j.Category.CategoryName,
                    IsOnlineApply = j.IsOnlineApply
                });

                var pageSize = 200;
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
        public ActionResult Duplicates(string sortOrder, string idFilter, string titleFilter, string podIdFilter,
            string idSearch, string titleSearch,
            string cityFilter, string countryFilter, string stateFilter,
            string jobBoardFilter, string divisionFilter,
            string companyFilter,
            string citySearch, string countrySearch, string stateSearch,
            string companySearch, string divisionSearch, string jobBoardSearch,
            string podIdSearch, int? page)
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
                ViewBag.EmailParm = sortOrder == "Job.EmailTo" ? "-Job.EmailTo" : "Job.EmailTo";
                ViewBag.OfficeParm = sortOrder == "Job.OfficeId" ? "-Job.OfficeId" : "Job.OfficeId";
                sortOrder = ViewBag.CurrentSort;

                if (idSearch != null || titleSearch != null || podIdSearch != null || citySearch != null ||
                    stateSearch != null || countrySearch != null || jobBoardSearch != null || companySearch != null ||
                    divisionSearch != null)
                {
                    page = 1;
                }
                else
                {
                    idSearch = idFilter;
                    titleSearch = titleFilter;
                    podIdSearch = podIdFilter;
                    citySearch = cityFilter;
                    countrySearch = countryFilter;
                    stateSearch = stateFilter;
                    divisionSearch = divisionFilter;
                    companySearch = companyFilter;
                    jobBoardSearch = jobBoardFilter;
                }

                ViewBag.IdFilter = idSearch;
                ViewBag.TitleFilter = titleSearch;
                ViewBag.PodIdFilter = podIdSearch;
                ViewBag.CityFilter = citySearch;
                ViewBag.CountryFilter = countrySearch;
                ViewBag.StateFilter = stateSearch;
                ViewBag.DivisionFilter = divisionSearch;
                ViewBag.CompanyFilter = companySearch;
                ViewBag.JobBoardFilter = jobBoardSearch;

                IQueryable<Job> query;
                if (string.IsNullOrEmpty(idSearch) && string.IsNullOrEmpty(titleSearch) && string.IsNullOrEmpty(podIdSearch) &&
                    string.IsNullOrEmpty(citySearch) && string.IsNullOrEmpty(stateSearch) && string.IsNullOrEmpty(countrySearch)
                    && string.IsNullOrEmpty(divisionSearch) && string.IsNullOrEmpty(jobBoardSearch) && string.IsNullOrEmpty(divisionSearch)
                    && string.IsNullOrEmpty(companySearch))
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

                    if (string.IsNullOrEmpty(podIdSearch))
                    {
                        podIdSearch = "";
                    }
                    if (string.IsNullOrEmpty(citySearch))
                    {
                        citySearch = "";
                    }

                    if (string.IsNullOrEmpty(stateSearch))
                    {
                        stateSearch = "";
                    }

                    if (string.IsNullOrEmpty(countrySearch))
                    {
                        countrySearch = "";
                    }

                    if (string.IsNullOrEmpty(divisionSearch))
                    {
                        divisionSearch = "";
                    }

                    if (string.IsNullOrEmpty(jobBoardSearch))
                    {
                        jobBoardSearch = "";
                    }

                    if (string.IsNullOrEmpty(companySearch))
                    {
                        companySearch = "";
                    }

                    query = _unitOfWork.JobRepository.GetDuplicateJobs()
                        .Where(j => j.Id.ToString().Contains(idSearch))
                        .Where(j => j.Title.ToString().Contains(titleSearch))
                        .Where(j => j.SchedulingPod.ToString().Contains(podIdSearch))
                        .Where(j => j.Division.ToString().Contains(divisionSearch))
                        .Where(j => j.JobBoard.JobBoardName.ToString().Contains(jobBoardSearch))
                        .Where(j => j.CompanyName.ToString().Contains(companySearch))
                        .Where(j => j.Country.CountryName.ToString().Contains(countrySearch))
                        .Where(j => j.State.StateName.ToString().Contains(stateSearch))
                        .Where(j => j.City.ToString().Contains(citySearch))
                            .ApplySort(sortOrder);
                }

                var mappedResult = query.Select(j => new JobGridViewModel
                {
                    Id = j.Id,
                    CloneFrom = j.CloneFrom,
                    EverGreenId = j.EverGreenId,
                    Title = j.Title,
                    TitleTruncated = j.Title.Substring(0, 20),
                    JobBoard = j.JobBoard.JobBoardName,
                    City = j.City,
                    StateName = j.State.StateName,
                    CountryName = j.Country.CountryName,
                    CompanyName = j.CompanyName,
                    SchedulingPod = j.SchedulingPod,
                    Division = j.Division,
                    CreatedBy = j.CreatedBy,
                    CreatedByTruncated = j.CreatedBy.Substring(0, 10),
                    OfficeId = j.OfficeId,
                    EmailTo = j.EmailTo,
                    EmailToTruncated = j.EmailTo.Substring(0, 10),
                    Bob = j.Bob,
                    Intvs = j.Intvs,
                    Intvs2 = j.Intvs2,
                    ApsCl = j.ApsCl,
                    ActiveDate = j.ActivationDate,
                    ExpDate = j.ExpirationDate,
                    CreDate = j.CreatedDate,
                    IsOnlineApply = j.IsOnlineApply,
                    Category = j.Category.CategoryName
                });

                var pageSize = 200;
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
        public ActionResult Conversion(string sortOrder, string idFilter, string titleFilter, string podIdFilter,
            string idSearch, string titleSearch,
            string cityFilter, string countryFilter, string categoryFilter, string stateFilter,
            string jobBoardFilter, string divisionFilter,
            string companyFilter, string statusFilter, string statusSearch,
            string citySearch, string countrySearch, string categorySearch, string stateSearch,
            string companySearch, string divisionSearch, string jobBoardSearch,
            string podIdSearch, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.IdParm = sortOrder == "Job.Id" ? "-Job.Id" : "Job.Id";
                ViewBag.CloneFromParm = sortOrder == "Job.CloneFrom" ? "-Job.CloneFrom" : "Job.CloneFrom";
                ViewBag.EverGreenIdParm = sortOrder == "Job.EverGreenId" ? "-Job.EverGreenId" : "Job.EverGreenId";
                ViewBag.TitleParm = sortOrder == "Job.Title" ? "-Job.Title" : "Job.Title";
                ViewBag.JobBoardParm = sortOrder == "Job.JobBoard.JobBoardName" ? "-Job.JobBoard.JobBoardName" : "Job.JobBoard.JobBoardName";
                ViewBag.CityParm = sortOrder == "Job.City" ? "-Job.City" : "Job.City";
                ViewBag.StateNameParm = sortOrder == "Job.State.StateName" ? "-Job.State.StateName" : "Job.State.StateName";
                ViewBag.CountryNameParm = sortOrder == "Job.Country.CountryName" ? "-Job.Country.CountryName" : "Job.Country.CountryName";
                ViewBag.CategoryNameParm = sortOrder == "Category.CategoryName" ? "-Category.CategoryName" : "Category.CategoryName";
                ViewBag.CompanyNameParm = sortOrder == "Job.CompanyName" ? "-Job.CompanyName" : "Job.CompanyName";
                ViewBag.SchedulingPodParm = sortOrder == "Job.SchedulingPod" ? "-Job.SchedulingPod" : "Job.SchedulingPod";
                ViewBag.DivisionParm = sortOrder == "Job.Division" ? "-Job.Division" : "Job.Division";
                ViewBag.ActivationDateParm = sortOrder == "Job.ActivationDate" ? "-Job.ActivationDate" : "Job.ActivationDate";
                ViewBag.ExpirationDateParm = sortOrder == "Job.ExpirationDate" ? "-Job.ExpirationDate" : "Job.ExpirationDate";
                ViewBag.CreatedByParm = sortOrder == "Job.CreatedBy" ? "-Job.CreatedBy" : "Job.CreatedBy";
                ViewBag.CreatedDateParm = sortOrder == "Job.CreatedDate" ? "-Job.CreatedDate" : "Job.CreatedDate";
                ViewBag.BobParm = sortOrder == "Job.Bob" ? "-Job.Bob" : "Job.Bob";
                ViewBag.IntvsParm = sortOrder == "Job.Intvs" ? "-Job.Intvs" : "Job.Intvs";
                ViewBag.Intvs2Parm = sortOrder == "Job.Intvs2" ? "-Job.Intvs2" : "Job.Intvs2";
                ViewBag.ApsClParm = sortOrder == "Job.ApsCl" ? "-Job.ApsCl" : "Job.ApsCl";
                ViewBag.EmailParm = sortOrder == "Job.EmailTo" ? "-Job.EmailTo" : "Job.EmailTo";
                ViewBag.OfficeParm = sortOrder == "Job.OfficeId" ? "-Job.OfficeId" : "Job.OfficeId";
                sortOrder = ViewBag.CurrentSort;

                if (idSearch != null || titleSearch != null || podIdSearch != null || citySearch != null ||
                    stateSearch != null || countrySearch != null || categorySearch != null || jobBoardSearch != null || companySearch != null ||
                    divisionSearch != null || statusSearch != null)
                {
                    page = 1;
                }
                else
                {
                    idSearch = idFilter;
                    titleSearch = titleFilter;
                    podIdSearch = podIdFilter;
                    citySearch = cityFilter;
                    countrySearch = countryFilter;
                    categorySearch = categoryFilter;
                    stateSearch = stateFilter;
                    divisionSearch = divisionFilter;
                    companySearch = companyFilter;
                    jobBoardSearch = jobBoardFilter;
                    statusSearch = statusFilter;
                }

                ViewBag.IdFilter = idSearch;
                ViewBag.TitleFilter = titleSearch;
                ViewBag.PodIdFilter = podIdSearch;
                ViewBag.CityFilter = citySearch;
                ViewBag.CountryFilter = countrySearch;
                ViewBag.CategoryFilter = categorySearch;
                ViewBag.StateFilter = stateSearch;
                ViewBag.DivisionFilter = divisionSearch;
                ViewBag.CompanyFilter = companySearch;
                ViewBag.JobBoardFilter = jobBoardSearch;
                ViewBag.StatusFilter = statusSearch;

                var query = _unitOfWork.JobRepository.GetJobs();

                query = PersistenceHelper.FilterByInput(idSearch, titleSearch, podIdSearch,
                    citySearch, stateSearch, countrySearch, categorySearch, jobBoardSearch,
                    divisionSearch, companySearch, statusSearch, query);

                query = PersistenceHelper.SortForConversionList(query);

                //query = query.ApplySort(sortOrder);

                var mappedResult = query.Select(j => new JobGridViewModel
                {
                    Id = j.Id,
                    CloneFrom = j.CloneFrom,
                    EverGreenId = j.EverGreenId,
                    Title = j.Title,
                    TitleTruncated = j.Title.Substring(0, 20),
                    JobBoard = j.JobBoard.JobBoardName,
                    City = j.City,
                    StateName = j.State.StateName,
                    CountryName = j.Country.CountryName,
                    CompanyName = j.CompanyName,
                    SchedulingPod = j.SchedulingPod,
                    Division = j.Division,
                    CreatedBy = j.CreatedBy,
                    CreatedByTruncated = j.CreatedBy.Substring(0, 10),
                    Bob = j.Bob,
                    Intvs = j.Intvs,
                    Intvs2 = j.Intvs2,
                    ApsCl = j.ApsCl,
                    ActiveDate = j.ActivationDate,
                    ExpDate = j.ExpirationDate,
                    CreDate = j.CreatedDate,
                    OfficeId = j.OfficeId,
                    EmailTo = j.EmailTo,
                    EmailToTruncated = j.EmailTo.Substring(0, 10),
                    Category = j.Category.CategoryName,
                    IsOnlineApply = j.IsOnlineApply
                });

                var pageSize = 200;
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
        [HttpGet]
        public void Excel(string sortOrder, string idFilter, string titleFilter, string podIdFilter,
            string idSearch, string titleSearch,
            string cityFilter, string countryFilter, string categoryFilter, string stateFilter,
            string jobBoardFilter, string divisionFilter,
            string companyFilter, string statusSearch, string statusFilter,
            string citySearch, string countrySearch, string categorySearch, string stateSearch,
            string companySearch, string divisionSearch, string jobBoardSearch,
            string podIdSearch, int? page)
        {
            string id, title, podId, country, category, state, city, jobBoard, division, company, status;
            if (!string.IsNullOrEmpty(sortOrder))
            {
                id = idSearch ?? string.Empty;
                title = titleSearch ?? string.Empty;
                podId = podIdSearch ?? string.Empty;
                country = countrySearch ?? string.Empty;
                state = stateSearch ?? string.Empty;
                city = citySearch ?? string.Empty;
                jobBoard = jobBoardSearch ?? string.Empty;
                division = divisionSearch ?? string.Empty;
                company = companySearch ?? string.Empty;
                category = categorySearch ?? string.Empty;
                status = statusSearch ?? string.Empty;
            }
            else
            {
                id = idFilter ?? string.Empty;
                title = titleFilter ?? string.Empty;
                podId = podIdFilter ?? string.Empty;
                country = countryFilter ?? string.Empty;
                category = categoryFilter ?? string.Empty;
                state = stateFilter ?? string.Empty;
                city = cityFilter ?? string.Empty;
                jobBoard = jobBoardFilter ?? string.Empty;
                division = divisionFilter ?? string.Empty;
                company = companyFilter ?? string.Empty;
                status = statusFilter ?? string.Empty;
            }

            var sort = sortOrder;


            var query = _unitOfWork.JobRepository.GetJobs();

            query = PersistenceHelper.FilterByInput(id, title, podId, city, state,
                country, category, jobBoard, division, company, status, query);

            query = PersistenceHelper.SortForJobList(query);

            query = query.ApplySort(sort);

            var pageNo = page ?? 1;

            var mappedResult = query.Select(j => new JobListExportViewModel
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
                ActivationDate = j.ActivationDate,
                ExpirationDate = j.ExpirationDate,
                CreatedBy = j.CreatedBy,
                CreatedDate = j.CreatedDate,
                Bob = j.Bob,
                Intvs = j.Intvs,
                Intvs2 = j.Intvs2,
                ApsCl = j.ApsCl,
                Category = j.Category.CategoryName
            });

            var pageSize = 200;
            int skipped;
            if (pageNo == 1)
            {
                skipped = 0;
            }
            else
            {
                skipped = (pageNo - 1) * pageSize;
            }

            var finalResult = mappedResult.Skip(skipped).Take(2000).ToList();

            var result = finalResult;
            var gv = new GridView { DataSource = result };
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=JobList.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            var objStringWriter = new StringWriter();
            var objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
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
            ModelState.Clear();
            var jobBoard = _unitOfWork.JobBoardRepository.GetJobBoard(viewModel.JobBoardId);
            viewModel.IsEmailApply = jobBoard.IsEmailApply;
            viewModel.IsOnlineApply = jobBoard.IsOnlineApply;
            this.TryValidateModel(viewModel);
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

            var occupations = _unitOfWork.JobOccupationMapRepository.GetOccupationForJob(id).ToList();

            var viewModel = new JobFormViewModel
            {
                Action = "Edit",
                Heading = "Edit",
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
            ModelState.Clear();
            var jobBoard = _unitOfWork.JobBoardRepository.GetJobBoard(viewModel.JobBoardId);
            viewModel.IsEmailApply = jobBoard.IsEmailApply;
            viewModel.IsOnlineApply = jobBoard.IsOnlineApply;
            this.TryValidateModel(viewModel);
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

            var occupations = _unitOfWork.JobOccupationMapRepository
               .GetOccupationForJob(id).ToList();

            var viewModel = new JobFormViewModel
            {
                Action = "Clone",
                Heading = "Clone",
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
            ModelState.Clear();
            var jobBoard = _unitOfWork.JobBoardRepository.GetJobBoard(viewModel.JobBoardId);
            viewModel.IsEmailApply = jobBoard.IsEmailApply;
            viewModel.IsOnlineApply = jobBoard.IsOnlineApply;
            this.TryValidateModel(viewModel);
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

            PopulateJobForClone(viewModel, job, currentUser, currentDate);
            job.CreatedDate = currentDate;
            job.CreatedBy = currentUser;

            job.CloneFrom = job.Id;

            if (job.IsEverGreen && job.EverGreenId == null)
            {
                job.EverGreenId = job.Id;
            }

            _unitOfWork.JobRepository.Add(job);

            _unitOfWork.Complete();

            PopulateMappingEntities(viewModel, job);

            return View("Close");
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void Delete(long id)
        {
            _unitOfWork.JobOccupationMapRepository.Delete(id);
            _unitOfWork.Complete();

            _unitOfWork.JobRepository.Delete(id);
            _unitOfWork.Complete();
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void Expire(long id)
        {
            var job = _unitOfWork.JobRepository.GetJob(id);

            if (job.ExpirationDate > DateTime.Now)
            {
                job.ExpirationDate = DateTime.Now.AddDays(-1);
            }

            _unitOfWork.JobRepository.Update(job);
            _unitOfWork.Complete();
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

            _unitOfWork.Complete();
        }

        [HttpGet]
        public ActionResult MassDelete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MassDelete(string podId)
        {
            int n;

            if (podId.Length == 0 || !int.TryParse(podId, out n))
            {
                return View();
            }

            var pId = int.Parse(podId);
            _unitOfWork.JobRepository.MassDelete(pId);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Jobs");
        }

        [HttpGet]
        public ActionResult MassExpire()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MassExpire(string podId)
        {
            int n;

            if (podId.Length == 0 || !int.TryParse(podId, out n))
            {
                return View();
            }

            var pId = int.Parse(podId);
            _unitOfWork.JobRepository.MassExpire(pId);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Jobs");
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
            if (job.CategoryId != null) viewModel.CategoryId = (int)job.CategoryId;
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
            viewModel.EmailTo = job.EmailTo;
            viewModel.IsBestPerforming = job.IsBestPerforming;
            viewModel.IsEverGreen = job.IsEverGreen;
            viewModel.IsOnlineApply = job.IsOnlineApply;
            viewModel.CompanyName = job.CompanyName;
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
            if (job.CategoryId != null) viewModel.CategoryId = (int)job.CategoryId;
            viewModel.StateId = job.StateId;
            viewModel.CountryId = job.CountryId;
            viewModel.Currency = job.Currency;
            viewModel.ApsCl = null;
            viewModel.Bob = null;
            viewModel.Intvs = null;
            viewModel.Intvs2 = null;
            viewModel.Author = job.Author;
            viewModel.JobBoardId = job.JobBoardId;
            viewModel.Division = job.Division;
            viewModel.IsCloneAndEverGreen = false;

            if (job.IsEverGreen)
            {
                viewModel.IsCloneAndEverGreen = true;
                viewModel.IsEverGreen = false;
                viewModel.CountryId = 0;
                viewModel.City = null;
                viewModel.StateId = 0;
                viewModel.ZipCode = null;
                viewModel.EmailTo = null;
                viewModel.SchedulingPod = 0;
                viewModel.OfficeId = 0;
            }
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
            job.CategoryId = viewModel.CategoryId;
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

        private static void PopulateJobForClone(JobFormViewModel viewModel, Job job, string currentUser,
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
            job.CategoryId = viewModel.CategoryId;
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
            job.EmailTo = viewModel.EmailTo;
            job.IsBestPerforming = viewModel.IsBestPerforming;
            job.IsOnlineApply = viewModel.IsOnlineApply;
            job.EditedBy = currentUser;
            job.EditedDate = currentDate;
            job.ApsCl = viewModel.ApsCl;
            job.Intvs = viewModel.Intvs;
            job.Intvs2 = viewModel.Intvs2;
            job.Bob = viewModel.Bob;
            job.Author = viewModel.Author;
            job.Division = viewModel.Division;
            job.JobBoardId = viewModel.JobBoardId;
            job.IsEverGreen = viewModel.IsEverGreen;
        }
    }
}