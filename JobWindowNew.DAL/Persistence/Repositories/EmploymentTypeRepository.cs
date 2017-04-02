using System.Collections.Generic;
using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class EmploymentTypeRepository : IEmploymentTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmploymentTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EmploymentType> GetEmploymentTypes()
        {
            return _context.EmploymentTypes;
        }
    }
}