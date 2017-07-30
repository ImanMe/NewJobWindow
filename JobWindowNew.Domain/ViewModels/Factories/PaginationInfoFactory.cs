using System;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public static class PaginationInfoFactory
    {
        public static PaginationInfoViewModel Create(int totalNumberOfJobs)
        {
            var info = new PaginationInfoViewModel
            {
                SortField = "Id",
                SortDirection = "ascending",
                PageSize = 10,
                CurrentPage = 0,
            };

            info.TotalPages = Convert.ToInt32(Math.Ceiling(
                (double)(totalNumberOfJobs / info.PageSize))) + 1;

            return info;
        }
    }
}
