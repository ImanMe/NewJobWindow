using JobWindowNew.Domain;
using JobWindowNew.Domain.ViewModels.Factories;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobWindowNew.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Reports
        [HttpGet]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void EverGreenReport()
        {
            var factory = new EverGreenReportFactory();

            var query = _unitOfWork.JobCategoryMapRepository.GetJobsForEverGreenReport();
            var result = query.ToList().Select(j => factory.Create(j));

            var gv = new GridView { DataSource = result };
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=EverGreen.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            var objStringWriter = new StringWriter();
            var objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void ActiveReport()
        {
            var factory = new EverGreenReportFactory();

            var result = _unitOfWork.JobCategoryMapRepository
                .GetJobsForActiveReport()
                .ToList()
                .Select(j => factory.Create(j));

            var gv = new GridView { DataSource = result };
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Active.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            var objStringWriter = new StringWriter();
            var objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public ActionResult InActiveReport()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public ActionResult InActiveReport(string podId)
        {
            var pId = int.Parse(podId);

            var factory = new EverGreenReportFactory();

            var result = _unitOfWork.JobCategoryMapRepository
                .GetJobsForInActiveReport(pId)
                .ToList()
                .Select(j => factory.Create(j));

            var gv = new GridView { DataSource = result };
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=InActive.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            var objStringWriter = new StringWriter();
            var objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index", "Jobs");
        }
    }
}