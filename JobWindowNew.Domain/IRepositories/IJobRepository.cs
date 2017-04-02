using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetJobs();
        void Add(Job job);
    }
}
