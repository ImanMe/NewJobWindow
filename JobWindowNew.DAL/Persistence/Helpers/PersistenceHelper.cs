using JobWindowNew.Domain.Model;
using System;
using System.Linq;

namespace JobWindowNew.DAL.Persistence.Helpers
{
    public class PersistenceHelper
    {
        public static IOrderedQueryable<Job> SortForJobList(IQueryable<Job> result)
        {
            return result
                .OrderBy(j => j.SchedulingPod)
                .ThenBy(j => j.JobBoard.JobBoardName)
                .ThenBy(j => j.City)
                .ThenBy(j => j.Category.CategoryName)
                .ThenByDescending(j => j.ExpirationDate >= DateTime.Now)
                .ThenByDescending(j => j.ApsCl)
                .ThenByDescending(j => j.Bob)
                .ThenByDescending(j => j.Intvs2)
                .ThenByDescending(j => j.Intvs)
                .ThenByDescending(j => j.Id);
        }

        public static IOrderedQueryable<Job> SortForConversionList(IQueryable<Job> result)
        {
            return result
                .OrderBy(j => j.SchedulingPod)
                .ThenBy(j => j.JobBoard.JobBoardName)
                .ThenBy(j => j.City)
                .ThenBy(j => j.Category.CategoryName)
                .ThenByDescending(j => j.ExpirationDate >= DateTime.Now)
                .ThenByDescending(j => j.Bob)
                .ThenByDescending(j => j.Intvs2)
                .ThenByDescending(j => j.Intvs)
                .ThenByDescending(j => j.ApsCl)
                .ThenByDescending(j => j.Id);
        }

        public static IQueryable<Job> FilterByInput(string idSearch, string titleSearch, string podIdSearch, string citySearch,
            string stateSearch, string countrySearch, string categorySearch, string jobBoardSearch, string divisionSearch,
            string companySearch, string statusSearch, string createdBySearch, IQueryable<Job> result)
        {
            if (!string.IsNullOrEmpty(idSearch))
            {
                result = result.Where(j => j.Id.ToString().Contains(idSearch));
            }

            if (!string.IsNullOrEmpty(titleSearch))
            {
                result = result.Where(j => j.Title.ToString().Contains(titleSearch));
            }

            if (!string.IsNullOrEmpty(podIdSearch))
            {
                result = result.Where(j => j.SchedulingPod.ToString().Contains(podIdSearch));
            }

            if (!string.IsNullOrEmpty(divisionSearch))
            {
                result = result.Where(j => j.Division.ToString().Contains(divisionSearch));
            }

            if (!string.IsNullOrEmpty(jobBoardSearch))
            {
                result = result.Where(j => j.JobBoard.JobBoardName.ToString().Contains(jobBoardSearch));
            }

            if (!string.IsNullOrEmpty(companySearch))
            {
                result = result.Where(j => j.CompanyName.ToString().Contains(companySearch));
            }

            if (!string.IsNullOrEmpty(countrySearch))
            {
                result = result.Where(j => j.Country.CountryName.ToString().Contains(countrySearch));
            }

            if (!string.IsNullOrEmpty(categorySearch))
            {
                result = result.Where(j => j.Category.CategoryName.ToString().Contains(categorySearch));
            }

            if (!string.IsNullOrEmpty(stateSearch))
            {
                result = result.Where(j => j.State.StateName.ToString().Contains(stateSearch));
            }

            if (!string.IsNullOrEmpty(citySearch))
            {
                result = result.Where(j => j.City.ToString().Contains(citySearch));
            }

            if (!string.IsNullOrEmpty(createdBySearch))
            {
                result = result.Where(j => j.CreatedBy.ToString().Contains(createdBySearch));
            }

            if (!string.IsNullOrEmpty(statusSearch))
            {
                if (statusSearch.StartsWith("E") || statusSearch.StartsWith("e"))
                {
                    result = result.Where(j => j.ExpirationDate < DateTime.Now);
                }
                else if (statusSearch.StartsWith("A") || statusSearch.StartsWith("a"))
                {
                    result = result.Where(j => j.ExpirationDate >= DateTime.Now);
                }

            }
            return result;
        }
    }
}
