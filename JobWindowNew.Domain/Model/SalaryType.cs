using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.Model
{
    public class SalaryType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

    }
}
