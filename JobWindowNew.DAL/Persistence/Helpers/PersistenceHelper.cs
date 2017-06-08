using JobWindowNew.Domain.Model;
using System;
using System.Linq;

namespace JobWindowNew.DAL.Persistence.Helpers
{
    public class PersistenceHelper
    {
        public static IOrderedQueryable<JobCategoryMap> SortForJobList(IQueryable<JobCategoryMap> result)
        {
            return result
                .OrderByDescending(j => j.Job.ExpirationDate >= DateTime.Now)
                .ThenBy(j => j.Job.SchedulingPod)
                .ThenBy(j => j.Job.JobBoard.JobBoardName)
                .ThenBy(j => j.Job.City)
                .ThenBy(j => j.Category.CategoryName)
                .ThenByDescending(j => j.Job.ExpirationDate)
                .ThenByDescending(j => j.Job.ApsCl)
                .ThenByDescending(j => j.Job.Bob)
                .ThenByDescending(j => j.Job.Intvs2)
                .ThenByDescending(j => j.Job.Intvs)
                .ThenBy(j => j.Job.Id);
        }

        public static IOrderedQueryable<JobCategoryMap> SortForConversionList(IQueryable<JobCategoryMap> result)
        {
            return result
                .OrderBy(j => j.Job.SchedulingPod)
                .ThenBy(j => j.Job.JobBoard.JobBoardName)
                .ThenByDescending(j => j.Job.Bob)
                .ThenByDescending(j => j.Job.Intvs2)
                .ThenByDescending(j => j.Job.Intvs)
                .ThenByDescending(j => j.Job.ApsCl)
                .ThenByDescending(j => j.Job.Id);
        }

        public static IQueryable<JobCategoryMap> FilterByInput(string idSearch, string titleSearch, string podIdSearch, string citySearch,
            string stateSearch, string countrySearch, string categorySearch, string jobBoardSearch, string divisionSearch,
            string companySearch, string statusSearch, IQueryable<JobCategoryMap> result)
        {
            if (!string.IsNullOrEmpty(idSearch))
            {
                result = result.Where(j => j.Job.Id.ToString().Contains(idSearch));
            }

            if (!string.IsNullOrEmpty(titleSearch))
            {
                result = result.Where(j => j.Job.Title.ToString().Contains(titleSearch));
            }

            if (!string.IsNullOrEmpty(podIdSearch))
            {
                result = result.Where(j => j.Job.SchedulingPod.ToString().Contains(podIdSearch));
            }

            if (!string.IsNullOrEmpty(divisionSearch))
            {
                result = result.Where(j => j.Job.Division.ToString().Contains(divisionSearch));
            }

            if (!string.IsNullOrEmpty(jobBoardSearch))
            {
                result = result.Where(j => j.Job.JobBoard.JobBoardName.ToString().Contains(jobBoardSearch));
            }

            if (!string.IsNullOrEmpty(companySearch))
            {
                result = result.Where(j => j.Job.CompanyName.ToString().Contains(companySearch));
            }

            if (!string.IsNullOrEmpty(countrySearch))
            {
                result = result.Where(j => j.Job.Country.CountryName.ToString().Contains(countrySearch));
            }

            if (!string.IsNullOrEmpty(categorySearch))
            {
                result = result.Where(j => j.Category.CategoryName.ToString().Contains(categorySearch));
            }

            if (!string.IsNullOrEmpty(stateSearch))
            {
                result = result.Where(j => j.Job.State.StateName.ToString().Contains(stateSearch));
            }

            if (!string.IsNullOrEmpty(citySearch))
            {
                result = result.Where(j => j.Job.City.ToString().Contains(citySearch));
            }

            if (!string.IsNullOrEmpty(statusSearch))
            {
                if (statusSearch.StartsWith("E") || statusSearch.StartsWith("e"))
                {
                    result = result.Where(j => j.Job.ExpirationDate < DateTime.Now);
                }
                else if (statusSearch.StartsWith("A") || statusSearch.StartsWith("a"))
                {
                    result = result.Where(j => j.Job.ExpirationDate >= DateTime.Now);
                }

            }
            return result;
        }
    }
}
