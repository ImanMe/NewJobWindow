using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IStateRepository
    {
        IEnumerable<State> GetStates(int id);
        IEnumerable<State> GetStates();
    }
}
