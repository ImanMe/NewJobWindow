using JobWindowNew.Domain;
using JobWindowNew.Domain.ViewModels;
using JobWindowNew.Domain.ViewModels.Factories;
using System.Linq;
using System.Web.Mvc;

namespace JobWindowNew.Web.Controllers
{
    public class BoardManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BoardManagementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Index()
        {
            var query = _unitOfWork.JobBoardRepository
                .GetJobBoards().AsQueryable();

            var result = query.ToList()
                .Select(j => JobBoardFactory.Create(j, ""));

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Create()
        {
            ;
            return View("BoardManagementForm",
                new JobBoardViewModel { Action = "Create" });
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Create(JobBoardViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("BoardManagementForm", viewModel);

            var jobBoard = JobBoardFactory.Create(viewModel);

            _unitOfWork.JobBoardRepository.Add(jobBoard);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Edit(int id)
        {
            var jobBoard = _unitOfWork.JobBoardRepository.GetJobBoard(id);

            return View("BoardManagementForm",
                JobBoardFactory.Create(jobBoard, "Edit"));
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Edit(JobBoardViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("BoardManagementForm", viewModel);

            var jobBoard = JobBoardFactory.Create(viewModel);

            _unitOfWork.JobBoardRepository.Update(jobBoard);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Delete(int id)
        {
            _unitOfWork.JobBoardRepository.Delete(id);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}