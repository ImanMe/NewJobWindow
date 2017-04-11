using System;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.ViewModels
{
    public class JobOnlineApplyViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirements { get; set; }
        public string Category { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FileName { get; set; }
        public long JobId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(13, MinimumLength = 10)]
        public long Phone { get; set; }

        public DateTime Date { get; set; }
        public string EmploymentType { get; set; }
    }
}
