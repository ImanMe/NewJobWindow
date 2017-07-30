using System;

namespace JobWindowNew.Domain.ViewModels
{
    public class JobGridViewModel
    {
        public long Id { get; set; }
        public long? CloneFrom { get; set; }
        public long? EverGreenId { get; set; }
        public string Title { get; set; }
        public string TitleTruncated { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string CompanyName { get; set; }
        public int? SchedulingPod { get; set; }
        public string Division { get; set; }
        public string ActivationDate { get; set; }
        public string ExpirationDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByTruncated { get; set; }
        public string CreatedDate { get; set; }
        public string OnlineUrl { get; set; }
        public string EmailTo { get; set; }
        public string EmailToTruncated { get; set; }
        public int? OfficeId { get; set; }
        public int? Bob { get; set; }
        public int? Intvs { get; set; }
        public int? Intvs2 { get; set; }
        public int? ApsCl { get; set; }
        public string JobBoard { get; set; }
        public string Category { get; set; }
        public bool IsOnlineApply { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime CreDate { get; set; }

        //public bool IsExpired => DateTime.Parse(ExpirationDate) < DateTime.Now;

        public bool IsExpired { get; set; }

        public PaginationInfoViewModel PaginationInfoViewModel { get; set; }

        public int? NumberOfActiveDays =>
            DateTime.Now < ExpDate
            ? (DateTime.Now - ActiveDate).Days + 1
            : (ExpDate - ActiveDate).Days;

        public int ActiveForThisWk => GetNumberOfActiveDaysInThisWeek();

        public int GetNumberOfActiveDaysInThisWeek()
        {
            if (ExpDate < DateTime.Now)
                return 0;

            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            monday = monday > DateTime.Today ? monday.AddDays(-7) : monday;
            var startingDate = monday < ActiveDate ? ActiveDate : monday;

            var todayMinusMonday = (DateTime.Today - startingDate).Days + 1;

            return todayMinusMonday;
        }
    }
}
