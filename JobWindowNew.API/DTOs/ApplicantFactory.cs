using JobWindowNew.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobWindowNew.API.DTOs
{
    public class ApplicantFactory
    {
        public ApplicantDTO Create(ApplicantApstream applicant)
        {
            return new ApplicantDTO()
            {
                applicantID = (int)applicant.Id,
                listingID = applicant.listingID,
                firstName = applicant.FirstName,
                lastName = applicant.LastName,
                email = applicant.Email,
                phone = applicant.Phone,
                date = applicant.date,
                schedulingPod = applicant.SchedulingPod,
                File = "http://board.thejobwindow.com/Resumes/" + applicant.File,
                listingTitle = applicant.listingTitle,
                officeID = applicant.OfficeId,
                specificSourceID = applicant.specificSourceID,
                specificSourceName = applicant.specificSourceName,
            };
        }
    }
}