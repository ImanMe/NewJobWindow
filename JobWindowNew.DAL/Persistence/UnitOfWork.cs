using JobWindowNew.DAL.Persistence.Repositories;
using JobWindowNew.Domain;
using JobWindowNew.Domain.IRepositories;

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

        public UnitOfWork(ApplicationDbContext context, IJobBoardRepository jobBoardRepository, ICountryRepository countryRepository, IStateRepository stateRepository, IOccupationRepository occupationRepository, ICategoryRepository categoryRepository, IEmploymentTypeRepository employmentTypeRepository, ISalaryTypeRepository salaryTypeRepository, IJobOccupationMapRepository jobOccupationMapRepository, IJobCategoryMapRepository jobCategoryMapRepository)
        {
            _context = context;
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
            _context.SaveChanges();
        }


    }
}
