﻿using JobWindowNew.Domain.Helpers;
using JobWindowNew.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.ViewModels
{
    public class JobFormViewModel : IValidatableObject
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
        [Range(1, int.MaxValue, ErrorMessage = "Insert a valid number")]
        [Display(Name = "Scheduling Pod")]
        public int? SchedulingPod { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Insert a valid number")]
        [Display(Name = "Office Id")]
        public int? OfficeId { get; set; }

        [Display(Name = "Minimum Experience")]
        public short? MinimumExperience { get; set; }

        [Display(Name = "Maximum Experience")]
        public short? MaximumExperience { get; set; }

        [Required]
        [StringLength(255)]
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

        public bool IsOnlineApply { get; set; }

        public string Author { get; set; }

        [DisplayName("Job Board")]
        public int JobBoardId { get; set; }

        public IEnumerable<JobBoard> JobBoards { get; set; }

        public bool IsEmailApply { get; set; }

        [DisplayName("Category")]
        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        [Display(Name = "Employment Type")]
        public int EmploymentTypeId { get; set; }


        public IEnumerable<EmploymentType> EmploymentTypes { get; set; }

        [Display(Name = "Salary Type")]
        public int SalaryTypeId { get; set; }


        public IEnumerable<SalaryType> SalaryTypes { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        [DisplayName("State")]
        public int StateId { get; set; }

        public IEnumerable<State> States { get; set; }

        [Display(Name = "Occupations")]
        public int[] OccupationsSelected { get; set; }

        public IEnumerable<Occupation> Occupations { get; set; }

        public DateTime GetActivationDate() => DateTime.Parse($"{ActivationDate}");

        public DateTime GetExpirationDate() => DateTime.Parse($"{ExpirationDate}");

        [DisplayName("BOB")]
        [Range(0, 999999)]
        public int? Bob { get; set; }

        [Range(0, 999999)]
        public int? Intvs { get; set; }

        [Range(0, 999999)]
        public int? Intvs2 { get; set; }

        [Range(0, 999999)]
        public int? ApsCl { get; set; }

        public bool IsClone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsEmailApply && string.IsNullOrEmpty(EmailTo))
            {
                yield return new ValidationResult("Email Field is Required", new[] { "EmailTo" });
            }

            if (IsEmailApply && !string.IsNullOrEmpty(EmailTo) && !ValidationCheck.ValidateEmail(EmailTo))
            {
                yield return new ValidationResult("Email is not in valid format", new[] { "EmailTo" });
            }
        }
    }
}
