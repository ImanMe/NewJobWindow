using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IJobOccupationMapRepository
    {
        void Add(JobOccupationMap map);
        void Update(JobOccupationMap map);
        IList<int> GetOccupationForJob(long jobId);
        void Delete(long jobId);
    }
}
