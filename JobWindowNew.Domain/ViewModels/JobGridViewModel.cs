﻿using System;

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

    }
}
