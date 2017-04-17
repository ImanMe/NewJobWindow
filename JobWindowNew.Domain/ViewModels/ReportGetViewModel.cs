using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace JobWindowNew.Domain.ViewModels
{
    public class ReportGetViewModel
    {
        [DisplayName("Job Board")]
        public int JobBoardId { get; set; }
        public IEnumerable<JobBoard> JobBoards { get; set; }

        [DisplayName("Scheduling Pod Id")]
        public int PodId { get; set; }
    }
}
