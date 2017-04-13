using JobWindowNew.DAL.Persistence.Repositories;
using JobWindowNew.Domain;
using JobWindowNew.Domain.IRepositories;
using System;
using System.Data.Entity.Validation;

namespace JobWindowNew.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IJobRepository JobRepository { get; private set; }
        public IJobBoardRepository JobBoardRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IStateRepository StateRepository { get; }
        public IOccupationRepository OccupationRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IEmploymentTypeRepository EmploymentTypeRepository { get; }
        public ISalaryTypeRepository SalaryTypeRepository { get; }
        public IJobOccupationMapRepository JobOccupationMapRepository { get; }
        public IJobCategoryMapRepository JobCategoryMapRepository { get; }
        public IApplicantRepository ApplicantRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IJobBoardRepository jobBoardRepository, ICountryRepository countryRepository, IStateRepository stateRepository, IOccupationRepository occupationRepository, ICategoryRepository categoryRepository, IEmploymentTypeRepository employmentTypeRepository, ISalaryTypeRepository salaryTypeRepository, IJobOccupationMapRepository jobOccupationMapRepository, IJobCategoryMapRepository jobCategoryMapRepository)
        {
            _context = context;
            ApplicantRepository = new ApplicantRepository(context);
            JobCategoryMapRepository = new JobCategoryMapRepository(context);
            JobOccupationMapRepository = new JobOccupationMapRepository(context);
            JobBoardRepository = new JobBoardRepository(context);
            CountryRepository = new CountryRepository(context);
            StateRepository = new StateRepository(context);
            OccupationRepository = new OccupationRepository(context);
            CategoryRepository = new CategoryRepository(context);
            EmploymentTypeRepository = new EmploymentTypeRepository(context);
            SalaryTypeRepository = new SalaryTypeTypeRepository(context);
            JobRepository = new JobRepository(context);
        }

        public void Complete()
        {
            try
            {

                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }


    }
}
