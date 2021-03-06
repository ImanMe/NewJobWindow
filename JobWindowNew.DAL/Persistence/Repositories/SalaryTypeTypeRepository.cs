using System.Collections.Generic;
using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class SalaryTypeTypeRepository : ISalaryTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public SalaryTypeTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SalaryType> GetSalaryTypes()
        {
            return _context.SalaryTypes;
        }
    }
}