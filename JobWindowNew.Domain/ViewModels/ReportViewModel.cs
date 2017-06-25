namespace JobWindowNew.Domain.ViewModels
{
    public class ReportViewModel
    {
        public long Id { get; set; }
        public long? CloneFrom { get; set; }
        public long? EverGreenId { get; set; }
        public string Title { get; set; }
        public string JobBoard { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CompanyName { get; set; }
        public string Category { get; set; }
        public int? PodId { get; set; }
        public int? OfficeId { get; set; }
        public string Division { get; set; }
        public string ActivationDate { get; set; }
        public string ExpirationDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? ActiveFor { get; set; }
        public int? BOB { get; set; }
        public int? Intvs { get; set; }
        public int? Intvs2 { get; set; }
        public int? ApsCl { get; set; }
        public string IsEverGreen { get; set; }
        public string IsBestPerforming { get; set; }
        public string EmailApply { get; set; }
        public string OnlineUrl { get; set; }
    }
}
