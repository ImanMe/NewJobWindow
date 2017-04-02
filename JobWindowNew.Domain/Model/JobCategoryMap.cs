using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobWindowNew.Domain.Model
{
    public class JobCategoryMap
    {
        public Job Job { get; set; }

        public Category Category { get; set; }

        [Key]
        [Column(Order = 1)]
        public long JobId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CategoryId { get; set; }

    }
}
