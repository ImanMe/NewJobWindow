using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IJobCategoryMapRepository
    {
        void Add(JobCategoryMap map);
        void Update(JobCategoryMap map);
        IList<int> GetCategoriesForJob(long jobId);
        void Delete(long jobId);
    }
}
