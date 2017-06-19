using JobWindowNew.Domain.Model;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public class EverGreenReportFactory
    {
        public ReportViewModel Create(Job job)
        {
            var model = new ReportViewModel();
            model.Id = job.Id;
            model.Title = job.Title;
            model.EverGreenId = job.EverGreenId;
            model.CloneFrom = job.CloneFrom;
            model.Country = job.Country?.CountryName;
            model.State = job.State?.StateName;
            model.City = job.City;
            model.PodId = job.SchedulingPod;
            model.OfficeId = job.OfficeId;
            model.EmailApply = job.EmailTo;
            model.OnlineUrl = @"http://board.thejobwindow.com/home/onlineapply/" + job.Id;
            model.ActivationDate = job.ActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.ExpirationDate = job.ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.CompanyName = job.CompanyName;
            model.CreatedBy = job.CreatedBy;
            model.CreatedDate = job.CreatedDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.Division = job.Division;
            model.JobBoard = job.JobBoard?.JobBoardName;
            model.Category = job.Category.CategoryName;
            model.IsEverGreen = job.IsEverGreen ? "Yes" : "No";
            model.IsBestPerforming = job.IsBestPerforming ? "Yes" : "No";
            model.BOB = job.Bob;
            model.ApsCl = job.ApsCl;
            model.Intvs = job.Intvs;
            model.Intvs2 = job.Intvs2;

            return model;
        }
    }
}
