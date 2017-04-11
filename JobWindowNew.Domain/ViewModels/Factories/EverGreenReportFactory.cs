using JobWindowNew.Domain.Model;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public class EverGreenReportFactory
    {
        public EverGreenReportViewModel Create(JobCategoryMap jobWithCategory)
        {
            return new EverGreenReportViewModel
            {
                Id = jobWithCategory.JobId,
                Title = jobWithCategory.Job.Title,
                EmploymentType = jobWithCategory.Job.EmploymentType?.Type,
                Salary = jobWithCategory.Job.Salary?.ToString(),
                SalaryTpe = jobWithCategory.Job.SalaryType?.Type,
                Country = jobWithCategory.Job.Country?.CountryName,
                State = jobWithCategory.Job.State?.StateName,
                ZipCode = jobWithCategory.Job.ZipCode,
                City = jobWithCategory.Job.City,
                Address = jobWithCategory.Job.Address,
                EverGreen = jobWithCategory.Job.IsEverGreen ? "Yes" : "No",
                BestPerforming = jobWithCategory.Job.IsBestPerforming ? "Yes" : "No",
                PodId = jobWithCategory.Job.SchedulingPod?.ToString(),
                OfficeId = jobWithCategory.Job.OfficeId?.ToString(),
                MaxExperience = jobWithCategory.Job.MaximumExperience,
                MinExperience = jobWithCategory.Job.MinimumExperience,
                ActivationDate = jobWithCategory.Job.ActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ExpirationDate = jobWithCategory.Job.ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                CompanyName = jobWithCategory.Job.CompanyName,
                EmailTo = jobWithCategory.Job.EmailTo,
                Author = jobWithCategory.Job.Author,
                CreatedBy = jobWithCategory.Job.CreatedBy,
                CreatedDate = jobWithCategory.Job.CreatedDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                EditedBy = jobWithCategory.Job.EditedBy,
                EditedDate = jobWithCategory.Job.EditedDate?.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                Division = jobWithCategory.Job.Division,
                Currency = jobWithCategory.Job.Currency,
                JobBoard = jobWithCategory.Job.JobBoard?.JobBoardName
            };
        }
    }
}
