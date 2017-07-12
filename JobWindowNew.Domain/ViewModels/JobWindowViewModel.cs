using System;

namespace JobWindowNew.Domain.ViewModels
{
    public class JobWindowViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string JobDescription { get; set; }

        public string JobRequirements { get; set; }

        public DateTime ActivationDate { get; set; }

        public string EmploymentType { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string StateCode { get; set; }

        public string Country { get; set; }

        public string Location => City + ", " + StateCode;

        public string Category { get; set; }

        public int? NumberOfActiveDays => (DateTime.Now - ActivationDate).Days;

        public string CompanyName { get; set; }
    }
}
