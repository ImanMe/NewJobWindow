using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels.Factories;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace JobWindowNew.Web.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Applicant
        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var applicantFactory = new ApplicantsFactory();

            var query = _unitOfWork.ApplicantRepository
               .GetApplicants();

            IEnumerable<Applicant> result = query.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                var id = int.Parse(searchString);
                result = query.Where(s => s.JobId == id);
            }


            var results = result.Select(j => applicantFactory.Create(j));

            var pageSize = 200;

            var pageNumber = (page ?? 1);

            return View(results.ToPagedList(pageNumber, pageSize));

        }

        [Authorize(Roles = "Root, Admin, Internal-Employee")]
        public FileResult Download(string fName)
        {
            if (string.IsNullOrEmpty(fName))
            {
                throw new ArgumentNullException();
            }

            var virtualFilePath = Server.MapPath("~/Resumes/" + fName);

            if (string.IsNullOrEmpty(virtualFilePath))
            {
                throw new ArgumentNullException();
            }

            return File(virtualFilePath, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(virtualFilePath));
        }
    }
}