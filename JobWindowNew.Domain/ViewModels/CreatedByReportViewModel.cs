﻿using JobWindowNew.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public string FromDate { get; set; }

        [Required]
        [Display(Name = "To")]
        public string ToDate { get; set; }
    }
}
