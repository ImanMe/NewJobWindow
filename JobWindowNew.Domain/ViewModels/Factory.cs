using JobWindowNew.Domain.Model;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels
{
    public class Factory
    {

        public JobGridViewModel Create(Job job)
        {
            var model = new JobGridViewModel();
            model.Id = job.Id;
            model.CloneFrom = job.CloneFrom;
            model.EverGreenId = job.EverGreenId;
            model.Title = job.Title;
            model.JobBoard = job.JobBoard.JobBoardName;
            model.City = job.City;
            model.StateName = job.State.StateName;
            model.CountryName = job.Country.CountryName;
            model.CompanyName = job.CompanyName;
            model.SchedulingPod = job.SchedulingPod;
            model.Division = job.Division;
            model.CreatedBy = job.CreatedBy;
            model.Bob = job.Bob;
            model.Intvs = job.Intvs;
            model.Intvs2 = job.Intvs2;
            model.ApsCl = job.ApsCl;
            model.ActivationDate = job.ActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.ExpirationDate = job.ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.CreatedDate = job.CreatedDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            return model;
        }

    }
}
