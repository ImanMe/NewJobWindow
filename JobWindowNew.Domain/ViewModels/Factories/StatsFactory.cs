using JobWindowNew.Domain.Model;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public class StatsFactory
    {
        public JobStatsViewModel Create(Job job)
        {
            return new JobStatsViewModel()
            {
                Id = job.Id,
                Title = job.Title,
                CompanyName = job.CompanyName,
                CountryName = job.Country.CountryName,
                StateName = job.State.StateName,
                City = job.City,
                SchedulingPod = job.SchedulingPod,
                JobBoard = job.JobBoard.JobBoardName,
                ActivationDate = job.ActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ExpirationDate = job.ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ApsCl = job.ApsCl,
                Intvs = job.Intvs,
                Intvs2 = job.Intvs2,
                Bob = job.Bob
            };
        }
    }
}
