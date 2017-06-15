using System;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.ViewModels
{
    public class JobStatsViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string TitleTruncated { get; set; }
        public int? SchedulingPod { get; set; }
        public string ActivationDate { get; set; }
        public string ExpirationDate { get; set; }
        public string JobBoard { get; set; }

        public DateTime ActiveDate { get; set; }
        public DateTime ExpDate { get; set; }

        [Required]
        [Range(0, 999999)]
        public int? Bob { get; set; }

        [Required]
        [Range(0, 999999)]
        public int? Intvs { get; set; }

        [Required]
        [Range(0, 999999)]
        public int? Intvs2 { get; set; }

        [Required]
        [Range(0, 999999)]
        public int? ApsCl { get; set; }
    }
}
