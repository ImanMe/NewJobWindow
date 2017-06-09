using JobWindowNew.Domain;
using JobWindowNew.Domain.ViewModels;
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

            var query = _unitOfWork.JobRepository.GetJobsForEverGreenReport();
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
        public ActionResult ActiveReport()
        {
            var viewModel = new ReportGetViewModel
            {
                JobBoards = _unitOfWork.JobBoardRepository.GetJobBoards(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public void ActiveReport(ReportGetViewModel viewModel)
        {
            var factory = new EverGreenReportFactory();

            var query = _unitOfWork.JobRepository
                .GetJobsForActiveReport();
            if (viewModel.PodId != 0)
            {
                query = query.Where(j => j.SchedulingPod == viewModel.PodId);
            }

            if (viewModel.JobBoardId != 0)
            {
                query = query.Where(j => j.JobBoardId == viewModel.JobBoardId);
            }

            query = query
                .OrderBy(j => j.SchedulingPod)
                .ThenBy(j => j.JobBoard.JobBoardName)
                .ThenBy(j => j.City)
                .ThenBy(j => j.Category.CategoryName)
                .ThenBy(j => j.ExpirationDate)
                .ThenByDescending(j => j.ApsCl)
                .ThenByDescending(j => j.Bob)
                .ThenByDescending(j => j.Intvs2)
                .ThenByDescending(j => j.Intvs);


            var result = query.ToList().Select(j => factory.Create(j));


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
            int n;

            if (podId.Length == 0 || !int.TryParse(podId, out n))
            {
                return View();
            }

            var pId = int.Parse(podId);

            var factory = new EverGreenReportFactory();

            var result = _unitOfWork.JobRepository
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