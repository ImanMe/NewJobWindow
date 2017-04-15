using JobWindowNew.Domain.Model;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public class ApplicantsFactory
    {
        public ApplicantViewModel Create(Applicant applicant)
        {
            return new ApplicantViewModel
            {
                FirstName = applicant.FirstName,
                LastName = applicant.LastName,
                Phone = applicant.Phone,
                Email = applicant.Email,
                JobId = applicant.JobId,
                Id = applicant.Id,
                FileName = applicant.FileName,
                Date = applicant.Date
            };
        }
    }
}
