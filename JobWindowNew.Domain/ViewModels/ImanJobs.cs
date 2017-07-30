using System;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels
{
    public class ImanJobs
    {
        public long Id { get; set; }
        public long? CloneFrom { get; set; }
        public long? EverGreenId { get; set; }
        public string Title { get; set; }
        public string TitleTruncated => Title.Substring(0, 10);
        public string City { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string CompanyName { get; set; }
        public int? SchedulingPod { get; set; }
        public string Division { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByTruncated => CreatedBy.Substring(0, 10);
        public DateTime CreatedDate { get; set; }
        public string OnlineUrl { get; set; }
        public string EmailTo { get; set; }
        public string EmailToTruncated => EmailTo.Substring(0, 10);
        public int? OfficeId { get; set; }
        public int? Bob { get; set; }
        public int? Intvs { get; set; }
        public int? Intvs2 { get; set; }
        public int? ApsCl { get; set; }
        public string JobBoard { get; set; }
        public string Category { get; set; }
        public bool IsOnlineApply { get; set; }
        public string ActDate => ActivationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        public string ExpDate => ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        public string CreDate => CreatedDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        public bool IsExpired => ExpirationDate < DateTime.Now;
        public int? NumberOfActiveDays =>
            DateTime.Now < ExpirationDate
                ? (DateTime.Now - ActivationDate).Days
                : (ExpirationDate - ActivationDate).Days;
    }
}
