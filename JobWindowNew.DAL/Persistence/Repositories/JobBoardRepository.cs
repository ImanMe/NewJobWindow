using System.Collections.Generic;
using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class JobBoardRepository : IJobBoardRepository
    {
        private readonly ApplicationDbContext _context;

        public JobBoardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<JobBoard> GetJobBoards()
        {
            return _context.JobBoards;
        }
    }
}