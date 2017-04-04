using JobWindowNew.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.ViewModels
{
    public class JobFormViewModel
    {
        public string Action { get; set; }

        public long Id { get; set; }

        public string Heading { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Required]
        [Display(Name = "Job Requirements")]
        public string JobRequirements { get; set; }

        public decimal? Salary { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        public string Address { get; set; }

        [Display(Name = "Is Ever Green")]
        public bool IsEverGreen { get; set; }

        [Required]
        [Display(Name = "Scheduling Pod")]
        public int? SchedulingPod { get; set; }

        [Required]
        [Display(Name = "Office Id")]
        public int? OfficeId { get; set; }

        [Display(Name = "Minimum Experience")]
        public short? MinimumExperience { get; set; }

        [Display(Name = "Maximum Experience")]
        public short? MaximumExperience { get; set; }

        [StringLength(255, MinimumLength = 0)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Activation Date")]
        [Required]
        public string ActivationDate { get; set; }



        [Display(Name = "Expiration Date")]
        [Required]
        public string ExpirationDate { get; set; }

        [EmailAddress]
        [Display(Name = "Email To")]
        public string EmailTo { get; set; }


        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }


        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Edited By")]
        [StringLength(255, MinimumLength = 0)]
        public string EditedBy { get; set; }

        [Display(Name = "Edited Date")]
        public DateTime? EditedDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Division { get; set; }

        [StringLength(255, MinimumLength = 0)]
        public string Currency { get; set; }

        public string[] Currencies { get; set; }

        [Display(Name = "Is Best Performing")]
        public bool IsBestPerforming { get; set; }

        [Display(Name = "Is Online Apply")]
        public bool IsOnlineApply { get; set; }

        public string Author { get; set; }

        public int JobBoardId { get; set; }

        public IEnumerable<JobBoard> JobBoards { get; set; }

        [Display(Name = "Employment Types")]
        public int EmploymentTypeId { get; set; }


        public IEnumerable<EmploymentType> EmploymentTypes { get; set; }

        [Display(Name = "Salary Types")]
        public int SalaryTypeId { get; set; }


        public IEnumerable<SalaryType> SalaryTypes { get; set; }

        public int CountryId { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        public int StateId { get; set; }

        public IEnumerable<State> States { get; set; }

        [Required]
        [Display(Name = "Categories")]
        public int[] CategoriesSelected { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        [Display(Name = "Occupations")]
        public int[] OccupationsSelected { get; set; }

        public IEnumerable<Occupation> Occupations { get; set; }

        public DateTime GetActivationDate() => DateTime.Parse($"{ActivationDate}");

        public DateTime GetExpirationDate() => DateTime.Parse($"{ExpirationDate}");
    }
}
