using JobWindowNew.Domain.Model;
using System;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public static class JobListFactory
    {
        public static JobGridViewModel Create(JobCategoryMap jobCategoryMap)
        {
            return new JobGridViewModel
            {
                Id = jobCategoryMap.Job.Id,
                CloneFrom = jobCategoryMap.Job.CloneFrom,
                EverGreenId = jobCategoryMap.Job.EverGreenId,
                Title = jobCategoryMap.Job.Title,
                TitleTruncated = jobCategoryMap.Job.Title.Substring(0, 20),
                JobBoard = jobCategoryMap.Job.JobBoard.JobBoardName,
                City = jobCategoryMap.Job.City,
                StateName = jobCategoryMap.Job.State.StateName,
                CountryName = jobCategoryMap.Job.Country.CountryName,
                CompanyName = jobCategoryMap.Job.CompanyName,
                SchedulingPod = jobCategoryMap.Job.SchedulingPod,
                Division = jobCategoryMap.Job.Division,
                CreatedBy = jobCategoryMap.Job.CreatedBy,
                EmailTo = jobCategoryMap.Job.EmailTo,
                EmailToTruncated = jobCategoryMap.Job.EmailTo.Substring(0, 10),
                OfficeId = jobCategoryMap.Job.OfficeId,
                Bob = jobCategoryMap.Job.Bob,
                Intvs = jobCategoryMap.Job.Intvs,
                Intvs2 = jobCategoryMap.Job.Intvs2,
                ApsCl = jobCategoryMap.Job.ApsCl,
                ActiveDate = jobCategoryMap.Job.ActivationDate,
                ExpDate = jobCategoryMap.Job.ExpirationDate,
                CreDate = jobCategoryMap.Job.CreatedDate,
                IsExpired = jobCategoryMap.Job.ExpirationDate < DateTime.Now,
                Category = jobCategoryMap.Category.CategoryName,
                IsOnlineApply = jobCategoryMap.Job.IsOnlineApply
            };
        }
    }
}
