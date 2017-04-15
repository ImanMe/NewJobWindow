using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers();
    }
}
