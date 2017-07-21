using JobWindowNew.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace JobWindowNew.Domain.ViewModels
{
    public class CreatedByReportViewModel
    {
        [Display(Name = "User")]
        public IEnumerable<ApplicationUser> Users { get; set; }

        [DisplayName("User")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "From")]
        public string FromDate => DateTime.Now.AddMonths(-1)
            .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

        [Required]
        [Display(Name = "To")]
        public string ToDate => DateTime.Now
         .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

        public DateTime GetFromDate() => DateTime.Parse($"{FromDate}");

        public DateTime GetToDate() => DateTime.Parse($"{ToDate}").AddDays(1);
    }
}
