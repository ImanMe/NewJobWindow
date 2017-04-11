using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IJobCategoryMapRepository
    {
        void Add(JobCategoryMap map);
        void Update(JobCategoryMap map);
        IList<int> GetCategoriesForJob(long jobId);
        void Delete(long jobId);
        string GetFirstCategoryTypeForJob(long jobId);
        IQueryable<JobCategoryMap> GetJobsForEverGreenReport();
        IQueryable<JobCategoryMap> GetJobsForActiveReport();
        IQueryable<JobCategoryMap> GetJobsForInActiveReport(int podId);
    }
}
