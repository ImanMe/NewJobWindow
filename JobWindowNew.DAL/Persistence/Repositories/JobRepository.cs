using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Job> GetJobs()
        {
            return _context.Jobs;
        }

        public void Update(Job job)
        {
            _context.Entry(job).State = EntityState.Modified;
        }

        public void Add(Job job)
        {
            _context.Jobs.Add(job);
        }

        public Job GetJob(long jobId)
        {
            return _context.Jobs.SingleOrDefault(j => j.Id == jobId);
        }

        //public IQueryable<Job> GetJobsForGrid()
        //{
        //    var result = _context.Jobs.AsNoTracking()
        //        .Include(j => j.State)
        //        .Include(j => j.Country)
        //        .Include(j => j.JobBoard);
        //    return result;
        //}

        public IQueryable<Job> GetDuplicateJobs()
        {
            return _context.Jobs.AsNoTracking()
                .Where(j => j.CloneFrom != null)
                .GroupBy(j => new { j.CloneFrom, j.SchedulingPod, j.City, j.CompanyName })
                .Where(x => x.Count() > 1)
                .SelectMany(x => x.Select(r => r))
                .Include(j => j.State)
                .Include(j => j.Country)
                .Include(j => j.JobBoard)
                .OrderBy(j => j.CloneFrom)
                .ThenBy(j => j.Id);
        }

        public Job GetJobForOnlineApply(long jobId)
        {
            return _context.Jobs.AsNoTracking()
                .Include(j => j.State)
                .Include(j => j.Country)
                .FirstOrDefault(j => j.Id == jobId);
        }

        public IQueryable<Job> GetJobsWithStats()
        {
            var todayMinusOneMonth = DateTime.Now.AddDays(-30);
            var result = _context.Jobs.AsNoTracking()
                .Where(j => j.HasStats == false)
                .Where(j => j.ExpirationDate < todayMinusOneMonth)
                .Include(j => j.State)
                .Include(j => j.Country)
                .Include(j => j.JobBoard)
                .OrderByDescending(j => j.ExpirationDate)
                .ThenByDescending(j => j.ActivationDate)
                .ThenBy(j => j.JobBoard.JobBoardName)
                .ThenBy(j => j.SchedulingPod)
                .ThenBy(j => j.Title)
                .ThenBy(j => j.Id);


            return result;
        }

        public IQueryable<JobCategoryMap> GetJobsForGrid()
        {
            return _context.JobCategoryMaps
                .GroupBy(m => m.Job.Id).SelectMany(gr => gr.Take(1))
                .Include(m => m.Job)
                .Include(m => m.Job.Country)
                .Include(m => m.Job.State)
                .Include(m => m.Job.JobBoard)
                .Include(m => m.Category);
        }

        public void Delete(long id)
        {
            var job = _context.Jobs.Find(id);
            if (job != null) _context.Jobs.Remove(job);
        }

        public void MassDelete(int id)
        {
            _context.Jobs.RemoveRange(_context.Jobs.Where(j => j.SchedulingPod == id));
        }

        public void MassExpire(int id)
        {
            _context.Jobs
                .Where(j => j.SchedulingPod == id)
                .ToList()
                .ForEach(e =>
                {
                    if (e.ExpirationDate > DateTime.Now)
                    {
                        e.ExpirationDate = DateTime.Now.AddDays(-1);
                    }
                });

        }

        public IQueryable<JobCategoryMap> GetJobSForJobListxx(string idSearch = "", string titleSearch = "", string podIdSearch = "", string citySearch = "", string stateSearch = "", string countrySearch = "", string categorySearch = "", string jobBoardSearch = "", string divisionSearch = "", string companySearch = "", string statusSearch = "")
        {
            var result = _context.JobCategoryMaps
                .GroupBy(m => m.Job.Id).SelectMany(gr => gr.Take(1))
                .Include(m => m.Job)
                .Include(m => m.Job.Country)
                .Include(m => m.Job.State)
                .Include(m => m.Job.JobBoard)
                .Include(m => m.Category);

            result = FilterByInput(idSearch, titleSearch, podIdSearch, citySearch, stateSearch, countrySearch, categorySearch, jobBoardSearch, divisionSearch, companySearch, statusSearch, result);
            result = SortForJobList(result);


            return result;
        }


        public IQueryable<JobCategoryMap> GetJobSForConversionListxx(string idSearch = "", string titleSearch = "", string podIdSearch = "", string citySearch = "", string stateSearch = "", string countrySearch = "", string categorySearch = "", string jobBoardSearch = "", string divisionSearch = "", string companySearch = "", string statusSearch = "")
        {
            var result = _context.JobCategoryMaps
                .GroupBy(m => m.Job.Id).SelectMany(gr => gr.Take(1))
                .Include(m => m.Job)
                .Include(m => m.Job.Country)
                .Include(m => m.Job.State)
                .Include(m => m.Job.JobBoard)
                .Include(m => m.Category);

            result = FilterByInput(idSearch, titleSearch, podIdSearch, citySearch, stateSearch, countrySearch, categorySearch, jobBoardSearch, divisionSearch, companySearch, statusSearch, result);
            result = SortForConversionList(result);


            return result;
        }

        private static IOrderedQueryable<JobCategoryMap> SortForJobList(IQueryable<JobCategoryMap> result)
        {
            return result
                .OrderByDescending(j => j.Job.ExpirationDate >= DateTime.Now)
                .ThenBy(j => j.Job.SchedulingPod)
                .ThenBy(j => j.Job.JobBoard.JobBoardName)
                .ThenBy(j => j.Job.City)
                .ThenBy(j => j.Category.CategoryName)
                .ThenByDescending(j => j.Job.ExpirationDate)
                .ThenByDescending(j => j.Job.ApsCl)
                .ThenByDescending(j => j.Job.Bob)
                .ThenByDescending(j => j.Job.Intvs2)
                .ThenByDescending(j => j.Job.Intvs)
                .ThenBy(j => j.Job.Id);
        }

        private static IOrderedQueryable<JobCategoryMap> SortForConversionList(IQueryable<JobCategoryMap> result)
        {
            return result
                .OrderBy(j => j.Job.SchedulingPod)
                .ThenBy(j => j.Job.JobBoard.JobBoardName)
                .ThenByDescending(j => j.Job.Bob)
                .ThenByDescending(j => j.Job.Intvs2)
                .ThenByDescending(j => j.Job.Intvs)
                .ThenByDescending(j => j.Job.ApsCl)
                .ThenByDescending(j => j.Job.Id);
        }

        private static IQueryable<JobCategoryMap> FilterByInput(string idSearch, string titleSearch, string podIdSearch, string citySearch,
            string stateSearch, string countrySearch, string categorySearch, string jobBoardSearch, string divisionSearch,
            string companySearch, string statusSearch, IQueryable<JobCategoryMap> result)
        {
            if (!string.IsNullOrEmpty(idSearch))
            {
                result = result.Where(j => j.Job.Id.ToString().Contains(idSearch));
            }

            if (!string.IsNullOrEmpty(titleSearch))
            {
                result = result.Where(j => j.Job.Title.ToString().Contains(titleSearch));
            }

            if (!string.IsNullOrEmpty(podIdSearch))
            {
                result = result.Where(j => j.Job.SchedulingPod.ToString().Contains(podIdSearch));
            }

            if (!string.IsNullOrEmpty(divisionSearch))
            {
                result = result.Where(j => j.Job.Division.ToString().Contains(divisionSearch));
            }

            if (!string.IsNullOrEmpty(jobBoardSearch))
            {
                result = result.Where(j => j.Job.JobBoard.JobBoardName.ToString().Contains(jobBoardSearch));
            }

            if (!string.IsNullOrEmpty(companySearch))
            {
                result = result.Where(j => j.Job.CompanyName.ToString().Contains(companySearch));
            }

            if (!string.IsNullOrEmpty(countrySearch))
            {
                result = result.Where(j => j.Job.Country.CountryName.ToString().Contains(countrySearch));
            }

            if (!string.IsNullOrEmpty(categorySearch))
            {
                result = result.Where(j => j.Category.CategoryName.ToString().Contains(categorySearch));
            }

            if (!string.IsNullOrEmpty(stateSearch))
            {
                result = result.Where(j => j.Job.State.StateName.ToString().Contains(stateSearch));
            }

            if (!string.IsNullOrEmpty(citySearch))
            {
                result = result.Where(j => j.Job.City.ToString().Contains(citySearch));
            }

            if (!string.IsNullOrEmpty(statusSearch))
            {
                if (statusSearch.StartsWith("E") || statusSearch.StartsWith("e"))
                {
                    result = result.Where(j => j.Job.ExpirationDate < DateTime.Now);
                }
                else if (statusSearch.StartsWith("A") || statusSearch.StartsWith("a"))
                {
                    result = result.Where(j => j.Job.ExpirationDate >= DateTime.Now);
                }

            }
            return result;
        }
    }
}
