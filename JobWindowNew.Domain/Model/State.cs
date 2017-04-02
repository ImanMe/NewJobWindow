using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.Model
{
    public class State
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string StateName { get; set; }

        [Required]
        [StringLength(255)]
        public string StateCode { get; set; }

        public long? SId { get; set; }

        public Country Country { get; set; }

        public int CountryId { get; set; }
    }
}
