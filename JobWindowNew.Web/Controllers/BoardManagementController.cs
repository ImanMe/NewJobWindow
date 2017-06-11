using JobWindowNew.Domain;
using JobWindowNew.Domain.Model;
using JobWindowNew.Domain.ViewModels;
using System.Linq;
using System.Linq.Dynamic;
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
        // GET: BoardManagement
        [HttpGet]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Index()
        {
            var query = _unitOfWork.JobBoardRepository.GetJobBoards().AsQueryable();
            var result = query.Select(j => new JobBoardViewModel
            {
                Id = j.Id,
                JobBoardName = j.JobBoardName,
                IsEmailApply = j.IsEmailApply,
                IsOnlineApply = j.IsOnlineApply
            });

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Create()
        {
            var viewModel = new JobBoardViewModel { Action = "Create" };
            return View("BoardManagementForm", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Create(JobBoardViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("BoardManagementForm", viewModel);
            }

            var jobBoard = new JobBoard
            {
                JobBoardName = viewModel.JobBoardName,
                IsEmailApply = viewModel.IsEmailApply,
                IsOnlineApply = viewModel.IsOnlineApply
            };

            _unitOfWork.JobBoardRepository.Add(jobBoard);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Edit(int id)
        {
            var jobBoard = _unitOfWork.JobBoardRepository.GetJobBoard(id);
            var viewModel = new JobBoardViewModel
            {
                Action = "Edit",
                JobBoardName = jobBoard.JobBoardName,
                IsOnlineApply = jobBoard.IsOnlineApply,
                IsEmailApply = jobBoard.IsEmailApply
            };

            return View("BoardManagementForm", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Root, Admin")]
        public ActionResult Edit(JobBoardViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("BoardManagementForm", viewModel);
            }

            var jobBoard = new JobBoard
            {
                Id = viewModel.Id,
                JobBoardName = viewModel.JobBoardName,
                IsOnlineApply = viewModel.IsOnlineApply,
                IsEmailApply = viewModel.IsEmailApply
            };

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