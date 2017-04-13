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
                //.Where(j => j.ExpirationDate < todayMinusOneMonth)
                .Include(j => j.State)
                .Include(j => j.Country)
                .Include(j => j.JobBoard)
                .OrderBy(j => j.SchedulingPod)
                .ThenBy(j => j.JobBoard)
                .ThenBy(j => j.Title);

            return result;
        }

        public void Delete(long id)
        {
            var job = _context.Jobs.Find(id);
            if (job != null) _context.Jobs.Remove(job);
        }
    }
}
