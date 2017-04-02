using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class JobCategoryMapRepository : IJobCategoryMapRepository
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryMapRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(JobCategoryMap map)
        {
            _context.JobCategoryMaps.Add(map);
        }
    }
}
