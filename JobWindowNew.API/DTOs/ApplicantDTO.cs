using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobWindowNew.API.DTOs
{
    public class ApplicantDTO
    {
        public int applicantID { get; set; }
        public long listingID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<int> schedulingPod { get; set; }
        public string File { get; set; }
        public string listingTitle { get; set; }
        public Nullable<int> officeID { get; set; }
        public Nullable<int> specificSourceID { get; set; }
        public string specificSourceName { get; set; }


        public string Resume
        {
            get
            {
                return "http://board.thejobwindow.com/Resumes/" + File;
            }
        }
    }
}