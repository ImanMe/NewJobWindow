using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users;
        }
    }
}
