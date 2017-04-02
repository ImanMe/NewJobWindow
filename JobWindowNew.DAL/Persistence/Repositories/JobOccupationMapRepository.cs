using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;

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
    }
}
