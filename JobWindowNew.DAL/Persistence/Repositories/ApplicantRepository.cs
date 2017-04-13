using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System.Data.Entity;
using System.Linq;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Applicant applicant)
        {
            _context.Applicants.Add(applicant);
        }

        public IQueryable<Applicant> GetApplicants()
        {
            return _context.Applicants;
        }

        public void Update(Applicant applicant)
        {
            _context.Entry(applicant).State = EntityState.Modified;
        }
    }
}
