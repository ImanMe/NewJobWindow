using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.Model
{
    public class EmploymentType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        public long? SId { get; set; }
    }
}
