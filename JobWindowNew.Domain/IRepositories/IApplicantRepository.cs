using JobWindowNew.Domain.Model;
using System.Linq;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IApplicantRepository
    {
        void Add(Applicant applicant);
        IQueryable<Applicant> GetApplicants();
        void Update(Applicant applicant);
    }
}
