[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(JobWindowNew.Site.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(JobWindowNew.Site.App_Start.NinjectWebCommon), "Stop")]

namespace JobWindowNew.Site.App_Start
{
    using DAL.Persistence;
    using DAL.Persistence.Repositories;
    using Domain;
    using Domain.IRepositories;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;
    using System.Web.Http;
    using WebApiContrib.IoC.Ninject;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IJobRepository>().To<JobRepository>();
            kernel.Bind<IJobBoardRepository>().To<JobBoardRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IOccupationRepository>().To<OccupationRepository>();
            kernel.Bind<ISalaryTypeRepository>().To<SalaryTypeTypeRepository>();
            kernel.Bind<IEmploymentTypeRepository>().To<EmploymentTypeRepository>();
            kernel.Bind<ICountryRepository>().To<CountryRepository>();
            kernel.Bind<IStateRepository>().To<StateRepository>();
            kernel.Bind<IJobOccupationMapRepository>().To<JobOccupationMapRepository>();
            kernel.Bind<IApplicantRepository>().To<ApplicantRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}