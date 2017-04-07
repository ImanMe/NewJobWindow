using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetJobs();
        Job GetJob(long jobId);
        void Add(Job job);
        void Update(Job job);
        IQueryable<Job> GetJobsForGrid();
    }
}
