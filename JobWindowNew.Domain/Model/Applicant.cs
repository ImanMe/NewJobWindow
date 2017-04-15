using System;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.Model
{
    public class Applicant
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        public long JobId { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(15, MinimumLength = 10)]
        public string Phone { get; set; }

        public string FileName { get; set; }

        public DateTime Date { get; set; }
    }
}
