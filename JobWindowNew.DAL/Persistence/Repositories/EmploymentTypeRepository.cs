using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.Linq;

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

        public EmploymentType GetEmploymentTypeById(int empId)
        {
            return _context.EmploymentTypes.FirstOrDefault(e => e.Id == empId);
        }
    }
}