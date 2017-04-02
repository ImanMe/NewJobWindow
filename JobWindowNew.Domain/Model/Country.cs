using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.Model
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string CountryName { get; set; }

        [Required]
        [StringLength(255)]
        public string CountryCode { get; set; }

        public long? SId { get; set; }

        public ICollection<State> States { get; set; }

        public Country()
        {
            States = new Collection<State>();
        }
    }
}
