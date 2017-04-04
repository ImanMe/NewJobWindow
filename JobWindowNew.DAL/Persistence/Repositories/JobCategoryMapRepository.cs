using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public IList<int> GetCategoriesForJob(long jobId)
        {
            return _context.JobCategoryMaps.Where(j => j.JobId == jobId)
                .Select(j => j.CategoryId).ToList();
        }

        public void Delete(long jobId)
        {
            _context.JobCategoryMaps
                .RemoveRange(_context.JobCategoryMaps
                    .Where(j => j.JobId == jobId));
        }

        public void Update(JobCategoryMap map)
        {
            _context.Entry(map).State = EntityState.Modified;
        }
    }
}
