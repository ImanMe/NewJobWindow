using System;
using System.Linq;
using System.Linq.Dynamic;

namespace JobWindowNew.Web.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            if (sort == null)
            {
                return source;
            }
            //Split the sort string
            var lstSort = sort.Split(',');
            //run through the sorting options and create a sort expression string from them

            var completeSortExpression = "";
            foreach (var sortOption in lstSort)
            {
                //If sort option starts with "-" we order
                //descending, otherwise ascending

                if (sortOption.StartsWith("-"))
                {
                    completeSortExpression = completeSortExpression + sortOption.Remove(0, 1) + " descending,";
                }
                else
                {
                    completeSortExpression = completeSortExpression + sortOption + ",";
                }
            }

            if (!string.IsNullOrWhiteSpace(completeSortExpression))
            {
                source = source.OrderBy(completeSortExpression.Remove(completeSortExpression.Count() - 1));
            }
            return source;
        }
    }
}