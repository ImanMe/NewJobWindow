using JobWindowNew.Domain.Model;
using System.Linq;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IJobRepository
    {
        Job GetJob(long jobId);
        void Add(Job job);
        void Update(Job job);
        IQueryable<Job> GetJobs();
        IQueryable<Job> GetJobsForList();
        IQueryable<Job> GetJobWindowJobs();
        Job GetJobWindowJob(long id);
        IQueryable<Job> GetDuplicateJobs();
        IQueryable<Job> GetJobsWithStats();
        Job GetJobForOnlineApply(long jobId);
        void Delete(long id);
        void MassDelete(int id);
        void MassExpire(int id);
        IQueryable<Job> GetJobsForEverGreenReport();
        IQueryable<Job> GetJobsForInActiveReport();
        IQueryable<Job> GetJobsForActiveReport();
    }
}
