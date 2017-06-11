using JobWindowNew.Domain.Model;

namespace JobWindowNew.Domain.ViewModels.Factories
{
    public static class JobBoardFactory
    {
        public static JobBoardViewModel Create(JobBoard jobBoard, string action)
        {
            return new JobBoardViewModel
            {
                Action = action,
                Id = jobBoard.Id,
                JobBoardName = jobBoard.JobBoardName,
                IsOnlineApply = jobBoard.IsOnlineApply,
                IsEmailApply = jobBoard.IsEmailApply
            };
        }
        public static JobBoard Create(JobBoardViewModel jobBoard)
        {
            return new JobBoard
            {
                Id = jobBoard.Id,
                JobBoardName = jobBoard.JobBoardName,
                IsOnlineApply = jobBoard.IsOnlineApply,
                IsEmailApply = jobBoard.IsEmailApply
            };
        }
    }
}
