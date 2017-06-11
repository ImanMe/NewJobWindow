using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            return _context.JobBoards.OrderBy(j => j.JobBoardName);
        }

        public JobBoard GetJobBoard(int id)
        {
            return _context.JobBoards.SingleOrDefault(j => j.Id == id);
        }

        public void Add(JobBoard jobBoard)
        {
            _context.JobBoards.Add(jobBoard);
        }

        public void Update(JobBoard jobBoard)
        {
            _context.Entry(jobBoard).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var jobBoard = _context.JobBoards.Find(id);
            if (jobBoard != null) _context.JobBoards.Remove(jobBoard);
        }
    }
}