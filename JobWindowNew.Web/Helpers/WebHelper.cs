using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobWindowNew.Web.Helpers
{
    public static class WebHelper
    {
        public static void ImportToExcel(IEnumerable<ReportViewModel> result, HttpResponseBase response, string title)
        {
            var gv = new GridView { DataSource = result };

            Import(response, title, gv);
        }

        public static void ImportToExcelOfJobList(IEnumerable<JobListExportViewModel> result, HttpResponseBase response, string title)
        {
            var gv = new GridView { DataSource = result };

            Import(response, title, gv);
        }

        private static void Import(HttpResponseBase response, string title, GridView gv)
        {
            var head = "attachment; filename=" + title + ".xls";

            gv.DataBind();

            response.ClearContent();
            response.Buffer = true;
            response.AddHeader("content-disposition", head);
            response.ContentType = "application/ms-excel";
            response.Charset = "";

            var objStringWriter = new StringWriter();

            var objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            response.Output.Write(objStringWriter.ToString());
            response.Flush();
            response.End();
        }

        public static int GetNumberOfPages(int totalNumberOfJobs, int pageSize)
        {
            return Convert.ToInt32(Math.Ceiling(
                (double)(totalNumberOfJobs / pageSize)) + 1);
        }

        public static IQueryable<Job> SortResult(PaginationInfoViewModel info, IQueryable<Job> query)
        {
            return info.SortDirection == "ascending" ?
                query.ApplySort(info.SortField) :
                query.ApplySort("-" + info.SortField);
        }

        public static int GetNumberOfPages(PaginationInfoViewModel info, IQueryable<Job> query)
        {
            return Convert.ToInt32(Math.Ceiling((double)(query.Count() / info.PageSize)) + 1);
        }
    }
}