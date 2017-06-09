using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System;
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

        public void Add(Job job)
        {
            _context.Jobs.Add(job);
        }

        public void Update(Job job)
        {
            _context.Entry(job).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            var job = _context.Jobs.Find(id);
            if (job != null) _context.Jobs.Remove(job);
        }

        public Job GetJob(long jobId)
        {
            return _context.Jobs.SingleOrDefault(j => j.Id == jobId);
        }

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

        public IQueryable<Job> GetJobsForEverGreenReport()
        {
            return _context.Jobs
                .Where(j => j.IsEverGreen)
                    .Include(j => j.Country)
                    .Include(j => j.State)
                    .Include(j => j.EmploymentType)
                    .Include(j => j.JobBoard)
                    .Include(j => j.SalaryType)
                    .Include(j => j.Category);
        }

        public IQueryable<Job> GetJobsForActiveReport()
        {
            return _context.Jobs
                .Where(j => j.ExpirationDate > DateTime.Now)
                .Include(j => j.Country)
                .Include(j => j.State)
                .Include(j => j.EmploymentType)
                .Include(j => j.JobBoard)
                .Include(j => j.SalaryType)
                .Include(j => j.Category)
                .Where(j => j.IsEverGreen == false);
        }


        public IQueryable<Job> GetJobsForInActiveReport(int podId)
        {
            return _context.Jobs
                .Where(j => j.SchedulingPod == podId)
                .Where(j => j.IsEverGreen == false)
                .Where(j => j.ExpirationDate < DateTime.Now)
                .Include(m => m.Country)
                .Include(m => m.State)
                .Include(m => m.EmploymentType)
                .Include(m => m.JobBoard)
                .Include(m => m.SalaryType)
                .Include(m => m.Category)
                .OrderBy(m => m.SchedulingPod)
                .ThenBy(m => m.JobBoard.JobBoardName)
                .ThenBy(m => m.City)
                .ThenBy(m => m.Category.CategoryName)
                .ThenBy(m => m.ExpirationDate)
                .ThenByDescending(j => j.ApsCl)
                .ThenByDescending(j => j.Bob)
                .ThenByDescending(j => j.Intvs2)
                .ThenByDescending(j => j.Intvs);
        }

        public IQueryable<Job> GetJobs()
        {
            return _context.Jobs
                .Include(m => m.Country)
                .Include(m => m.State)
                .Include(m => m.JobBoard)
                .Include(m => m.Category);
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
    }
}
