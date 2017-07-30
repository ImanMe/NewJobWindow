using System;
using System.ComponentModel;

namespace JobWindowNew.Domain.ViewModels
{
    public class JobListExportViewModel
    {
        public long Id { get; set; }
        public long? CloneFrom { get; set; }
        public long? EverGreenId { get; set; }
        public int? SchedulingPod { get; set; }
        public int? OfficeId { get; set; }
        public string Title { get; set; }
        public string JobBoard { get; set; }
        public string Division { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Category { get; set; }
        public string CompanyName { get; set; }
        public int? ApsCl { get; set; }
        public int? Bob { get; set; }
        public int? Intvs { get; set; }
        public int? Intvs2 { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [DisplayName("ActiveFor")]
        public int? NumberOfActiveDays =>
            DateTime.Now < ExpirationDate
                ? (DateTime.Now - ActivationDate).Days + 1
                : (ExpirationDate - ActivationDate).Days;
        public int ActiveForThisWk => GetNumberOfActiveDaysInThisWeek();
        public string EmailApply { get; set; }
        public string OnlineUrl { get; set; }

        public int GetNumberOfActiveDaysInThisWeek()
        {
            if (ExpirationDate < DateTime.Now)
                return 0;

            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            monday = monday > DateTime.Today ? monday.AddDays(-7) : monday;
            var startingDate = monday < ActivationDate ? ActivationDate : monday;

            var todayMinusMonday = (DateTime.Today - startingDate).Days + 1;

            return todayMinusMonday;
        }

        //public bool IsExpired => DateTime.Parse(ExpirationDate) < DateTime.Now;

        //public bool IsExpired { get; set; }
    }
}
