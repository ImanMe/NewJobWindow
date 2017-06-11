using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.ViewModels
{
    public class JobBoardViewModel
    {
        public string Action { get; set; }
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Job Board")]
        public string JobBoardName { get; set; }

        [Required]
        [DisplayName("Online Apply")]
        public bool IsOnlineApply { get; set; }

        [Required]
        [DisplayName("Email Apply")]
        public bool IsEmailApply { get; set; }

    }
}
