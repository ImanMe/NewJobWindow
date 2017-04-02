using System.Collections.Generic;
using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries;
        }
    }
}