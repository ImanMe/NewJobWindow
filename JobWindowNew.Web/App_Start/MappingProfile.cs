using AutoMapper;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;

namespace JobWindowNew.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Job, JobFormViewModel>();
            Mapper.CreateMap<JobFormViewModel, Job>();
            Mapper.CreateMap<JobOccupationMap, JobOccupationMapViewModel>();
            Mapper.CreateMap<JobOccupationMap, JobOccupationMap>();
            Mapper.CreateMap<JobCategoryMap, JobCategoryMapViewModel>();
            Mapper.CreateMap<JobCategoryMapViewModel, JobCategoryMap>();
        }
    }
}
