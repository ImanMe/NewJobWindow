//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobWindowNew.API.Entities
{
    using System;
    
    public partial class sp_JobWindow_Result
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public System.DateTime ActivationDate { get; set; }
        public long JobPostingsID { get; set; }
        public string CompanyName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Nullable<short> MinimumExperience { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public string EmploymentType { get; set; }
        public Nullable<short> MaximumExperience { get; set; }
        public string EmailTo { get; set; }
        public Nullable<short> SchedulingPod { get; set; }
    }
}
