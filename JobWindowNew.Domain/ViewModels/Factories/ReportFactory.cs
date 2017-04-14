using JobWindowNew.Domain.Model;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public class EverGreenReportFactory
    {
        public ReportViewModel Create(JobCategoryMap jobWithCategory)
        {
            var model = new ReportViewModel();
            model.Id = jobWithCategory.JobId;
            model.Title = jobWithCategory.Job.Title;
            model.EverGreenId = jobWithCategory.Job.EverGreenId;
            model.CloneFrom = jobWithCategory.Job.CloneFrom;
            model.Country = jobWithCategory.Job.Country?.CountryName;
            model.State = jobWithCategory.Job.State?.StateName;
            model.City = jobWithCategory.Job.City;
            model.PodId = jobWithCategory.Job.SchedulingPod?.ToString();
            model.ActivationDate = jobWithCategory.Job.ActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.ExpirationDate = jobWithCategory.Job.ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.CompanyName = jobWithCategory.Job.CompanyName;
            model.CreatedBy = jobWithCategory.Job.CreatedBy;
            model.CreatedDate = jobWithCategory.Job.CreatedDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.Division = jobWithCategory.Job.Division;
            model.JobBoard = jobWithCategory.Job.JobBoard?.JobBoardName;
            model.Category = jobWithCategory.Category.CategoryName;
            model.IsEverGreen = jobWithCategory.Job.IsEverGreen ? "Yes" : "No";
            model.IsBestPerforming = jobWithCategory.Job.IsBestPerforming ? "Yes" : "No";
            model.BOB = jobWithCategory.Job.Bob;
            model.ApsCl = jobWithCategory.Job.ApsCl;
            model.Intvs = jobWithCategory.Job.Intvs;
            model.Intvs2 = jobWithCategory.Job.Intvs2;

            return model;
        }
    }
}
