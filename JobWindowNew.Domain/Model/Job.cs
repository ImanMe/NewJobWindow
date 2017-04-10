using System;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.Model
{
    public class Job
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public string JobRequirements { get; set; }

        public decimal? Salary { get; set; }

        [Required]
        [StringLength(255)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        public string Address { get; set; }

        public bool IsEverGreen { get; set; }

        [Required]
        public int? SchedulingPod { get; set; }

        [Required]
        public int? OfficeId { get; set; }

        public short? MinimumExperience { get; set; }

        public short? MaximumExperience { get; set; }

        [StringLength(255, MinimumLength = 0)]
        public string CompanyName { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        [StringLength(255, MinimumLength = 0)]
        public string EmailTo { get; set; }

        public long? SId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(255, MinimumLength = 0)]
        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Division { get; set; }

        [StringLength(255, MinimumLength = 0)]
        public string Currency { get; set; }

        public bool IsBestPerforming { get; set; }

        public bool IsOnlineApply { get; set; }

        public string Author { get; set; }

        public JobBoard JobBoard { get; set; }

        [Required]
        public int JobBoardId { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public int EmploymentTypeId { get; set; }

        public int SalaryTypeId { get; set; }

        public SalaryType SalaryType { get; set; }

        [Required]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        public int StateId { get; set; }

        public State State { get; set; }

        public long? CloneFrom { get; set; }

        public long? EverGreenId { get; set; }

        [Range(0, 999999)]
        public int? Bob { get; set; }

        [Range(0, 999999)]
        public int? Intvs { get; set; }

        [Range(0, 999999)]
        public int? Intvs2 { get; set; }

        [Range(0, 999999)]
        public int? ApsCl { get; set; }

        [Range(0, 999)]
        public decimal? RemovedCl { get; set; }

        [StringLength(100)]
        public string RemovedReason { get; set; }

        public bool HasStats { get; set; }
    }
}
