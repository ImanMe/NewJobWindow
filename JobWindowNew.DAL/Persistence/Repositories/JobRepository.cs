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

        public IQueryable<Job> GetJobsForGrid()
        {
            var result = _context.Jobs.AsNoTracking()
                .Include(j => j.State)
                .Include(j => j.Country)
                .Include(j => j.JobBoard);
            return result;
        }

        public IQueryable<Job> GetDuplicateJobs()
        {
            return _context.Jobs.AsNoTracking()
                .Where(j => j.CloneFrom != null)
                .GroupBy(j => new { j.CloneFrom, j.City })
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

        public void Delete(long id)
        {
            var job = _context.Jobs.Find(id);
            if (job != null) _context.Jobs.Remove(job);
        }
    }
}
