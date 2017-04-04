using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class JobOccupationMapRepository : IJobOccupationMapRepository
    {
        private readonly ApplicationDbContext _context;

        public JobOccupationMapRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(JobOccupationMap map)
        {
            _context.JobOccupationMaps.Add(map);
        }

        public IList<int> GetOccupationForJob(long jobId)
        {
            return _context.JobOccupationMaps.Where(j => j.JobId == jobId)
                .Select(j => j.OccupationId).ToList();
        }

        public void Delete(long jobId)
        {
            _context.JobOccupationMaps
                .RemoveRange(_context.JobOccupationMaps
                    .Where(j => j.JobId == jobId));
        }

        public void Update(JobOccupationMap map)
        {
            _context.Entry(map).State = EntityState.Modified;
        }
    }
}
