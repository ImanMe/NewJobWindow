using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobWindowNew.Domain.Model
{
    public class Occupation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string OccupationName { get; set; }

        [Required]
        [StringLength(255)]
        public string OccupationCategory { get; set; }

        [NotMapped]
        public string FullOccupationName => OccupationCategory + " - " + OccupationName;

        public long? SId { get; set; }
    }
}
