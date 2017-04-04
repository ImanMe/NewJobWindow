using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.Linq;

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

        public Occupation GetOccupation(int occupationId)
        {
            return _context.Occupations.SingleOrDefault(j => j.Id == occupationId);
        }
    }
}
