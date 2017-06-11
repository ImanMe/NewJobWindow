using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.Model
{
    public class JobBoard
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string JobBoardName { get; set; }

        public int? SId { get; set; }

        public int EstreamJBsID { get; set; }

        public bool IsOnlineApply { get; set; }

        public bool IsEmailApply { get; set; }
    }
}
