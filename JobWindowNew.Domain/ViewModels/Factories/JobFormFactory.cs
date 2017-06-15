using JobWindowNew.Domain.Model;
using System;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public class JobFormFactory
    {
        public JobFormViewModel Create(IUnitOfWork unitOfWork, string heading)
        {
            return new JobFormViewModel
            {
                Heading = heading,
                Action = heading,
                SalaryTypes = unitOfWork.SalaryTypeRepository.GetSalaryTypes(),
                EmploymentTypes = unitOfWork.EmploymentTypeRepository.GetEmploymentTypes(),
                JobBoards = unitOfWork.JobBoardRepository.GetJobBoards(),
                Countries = unitOfWork.CountryRepository.GetCountries(),
                States = unitOfWork.StateRepository.GetStates(),
                Categories = unitOfWork.CategoryRepository.GetCategories(),
                Occupations = unitOfWork.OccupationRepository.GetOccupations(),
                Currencies = new[] { "USD", "CAD" },
                ActivationDate = DateTime.Now
                .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.Now.AddMonths(1)
                .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
            };
        }

        public Job Create(JobFormViewModel viewModel)
        {
            var job = new Job
            {
                Title = viewModel.Title,
                JobDescription = viewModel.JobDescription,
                JobRequirements = viewModel.JobRequirements,
                EmploymentTypeId = viewModel.EmploymentTypeId,
                Salary = viewModel.Salary,
                Currency = viewModel.Currency,
                SalaryTypeId = viewModel.SalaryTypeId,
                CountryId = viewModel.CountryId,
                StateId = viewModel.StateId,
                CategoryId = viewModel.CategoryId,
                City = viewModel.City,
                ZipCode = viewModel.ZipCode,
                Address = viewModel.Address,
                MinimumExperience = viewModel.MinimumExperience,
                MaximumExperience = viewModel.MaximumExperience,
                CompanyName = viewModel.CompanyName,
                ActivationDate = viewModel.GetActivationDate(),
                ExpirationDate = viewModel.GetExpirationDate(),
                SchedulingPod = viewModel.SchedulingPod,
                OfficeId = viewModel.OfficeId,
                Division = viewModel.Division,
                JobBoardId = viewModel.JobBoardId,
                Author = viewModel.Author,
                EmailTo = viewModel.EmailTo,
                IsBestPerforming = viewModel.IsBestPerforming,
                IsEverGreen = viewModel.IsEverGreen,
                EditedBy = null,
                EditedDate = null,
                ApsCl = viewModel.ApsCl,
                Intvs = viewModel.Intvs,
                Intvs2 = viewModel.Intvs2,
                Bob = viewModel.Bob,
                CreatedDate = DateTime.Now
            };
            return job;
        }

        public JobOccupationMap GetSelectedOccupation(Job job, int occupationId)
        {
            var jobOccupationMap = new JobOccupationMap()
            {
                JobId = job.Id,
                OccupationId = occupationId
            };
            return jobOccupationMap;
        }
    }
}
