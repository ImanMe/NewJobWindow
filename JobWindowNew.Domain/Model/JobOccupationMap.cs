using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobWindowNew.Domain.Model
{
    public class JobOccupationMap
    {
        public Job Job { get; set; }

        public Occupation Occupation { get; set; }

        [Key]
        [Column(Order = 1)]
        public long JobId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int OccupationId { get; set; }
    }
}
