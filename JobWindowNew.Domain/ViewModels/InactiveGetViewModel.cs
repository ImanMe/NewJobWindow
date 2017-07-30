using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.ViewModels
{
    public class InactiveGetViewModel
    {
        [DisplayName("Job Board")]
        public int JobBoardId { get; set; }

        public IEnumerable<JobBoard> JobBoards { get; set; }

        [DisplayName("Scheduling Pod Id")]
        public int PodId { get; set; }

        [Required]
        [Display(Name = "From")]
        public string FromDate { get; set; }

        [Required]
        [Display(Name = "To")]
        public string ToDate { get; set; }
    }
}
