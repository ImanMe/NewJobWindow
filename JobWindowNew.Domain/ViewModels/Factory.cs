using JobWindowNew.Domain.Model;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels
{
    public class Factory
    {

        public JobGridViewModel Create(Job job)
        {
            return new JobGridViewModel()
            {
                Id = job.Id,
                CloneFrom = job.CloneFrom,
                EverGreenId = job.EverGreenId,
                Title = job.Title,
                JobBoard = job.JobBoard.JobBoardName,
                City = job.City,
                StateName = job.State.StateName,
                CountryName = job.Country.CountryName,
                CompanyName = job.CompanyName,
                SchedulingPod = job.SchedulingPod,
                Division = job.Division,
                CreatedBy = job.CreatedBy,
                Bob = job.Bob,
                Intvs = job.Intvs,
                Intvs2 = job.Intvs2,
                ApsCl = job.ApsCl,
                RemovedCl = job.RemovedCl,
                ActivationDate = job.ActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ExpirationDate = job.ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                CreatedDate = job.CreatedDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
                // PaginationInfoViewModel = paginationInfo
            };
        }

    }
}
