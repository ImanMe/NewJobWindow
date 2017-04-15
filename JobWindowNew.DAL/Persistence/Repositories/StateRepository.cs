using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _context;

        public StateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<State> GetStates(int id)
        {
            return _context.States.Where(s => s.CountryId == id)
                .OrderBy(s => s.StateName);
        }

        public IEnumerable<State> GetStates()
        {
            return _context.States.OrderBy(s => s.StateName);
        }
    }
}