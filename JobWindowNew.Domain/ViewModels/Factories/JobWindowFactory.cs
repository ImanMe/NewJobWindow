using JobWindowNew.Domain.Model;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public static class JobWindowFactory
    {
        public static JobWindowViewModel Create(Job job)
        {
            return new JobWindowViewModel
            {
                Id = job.Id,
                Title = job.Title,
                JobDescription = job.JobDescription,
                JobRequirements = job.JobRequirements,
                State = job.State.StateName,
                StateCode = job.State.StateCode,
                Country = job.Country.CountryCode,
                City = job.City,
                Category = job.Category.CategoryName,
                ActivationDate = job.ActivationDate,
                EmploymentType = job.EmploymentType.Type
            };
        }
    }
}
