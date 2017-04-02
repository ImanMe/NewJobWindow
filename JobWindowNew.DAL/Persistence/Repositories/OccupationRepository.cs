using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class OccupationRepository : IOccupationRepository
    {
        private readonly ApplicationDbContext _context;

        public OccupationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Occupation> GetOccupations()
        {
            return _context.Occupations;
        }
    }
}
