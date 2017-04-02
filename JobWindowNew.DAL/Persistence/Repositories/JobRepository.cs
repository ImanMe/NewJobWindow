using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Collections.Generic;

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

        public void Add(Job job)
        {
            _context.Jobs.Add(job);
        }

    }
}
