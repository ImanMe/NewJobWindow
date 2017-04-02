using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.Model
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; }

        public long? SId { get; set; }
    }
}
