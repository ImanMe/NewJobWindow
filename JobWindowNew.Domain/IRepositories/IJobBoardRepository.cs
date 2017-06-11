using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IJobBoardRepository
    {
        IEnumerable<JobBoard> GetJobBoards();
        JobBoard GetJobBoard(int id);
        void Add(JobBoard jobBoard);
        void Update(JobBoard jobBoard);
        void Delete(int id);
    }
}
