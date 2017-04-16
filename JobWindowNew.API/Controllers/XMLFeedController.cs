using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JobWindowNew.API.Entities;
using System.Xml.Linq;
using System.Web.Hosting;
using JobWindowNew.API.DTOs;

namespace JobWindowNew.API.Controllers
{

    public static class PagingExtensions
    {
        //used by LINQ to SQL
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        //used by LINQ
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

    }
    public class XMLFeedController : ApiController
    {
        JobWindowContext _context = new JobWindowContext();
        JobWindowContext db = new JobWindowContext();
        ApplicantFactory _factory = new ApplicantFactory();
        
        [HttpGet]
        [AllowAnonymous]
        [Route("api/applicants/feed")]
        public IHttpActionResult Applicants(long applicantId, string Token = "")
        {
            if (Token != "KLKtjv1Hz5h1js9He9ZWv32EfJIR6KB7")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            try
            {
                IQueryable<ApplicantApstream> applicants = _context.ApplicantApstreams;

                var allApplicants = applicants.Where(a => a.Id > applicantId).ToList().OrderBy(ai => ai.Id);

                return Ok(allApplicants.ToList().Select(p => _factory.Create(p)));
                //return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/Jobs2Careers")]
        public IHttpActionResult Jobs2Careers(int Page = 1, string Token = "")
        {
            if (Token != "26BKpBB9NjY8Aa9Ogln6kSIA7KFyj0kG")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_Jobs2CareersFeed_Result> x = db.sp_Jobs2CareersFeed().ToList();

            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("Results", (from jp in x
                                                         select new XElement("Result",
                                                     new XElement("title", jp.Title),
                                                     new XElement("date", jp.ActivationDate),
                                                     new XElement("referencenumber", jp.JobPostingsID),
                                                     new XElement("url", ""),
                                                     new XElement("CompanyName", jp.CompanyName),
                                                     new XElement("postalcode", jp.ZipCode),
                                                     new XElement("description", jp.JobDescription),
                                                     new XElement("city", jp.City),
                                                     new XElement("state", jp.State),
                                                     new XElement("experience", jp.MinimumExperience + "-" + jp.MaximumExperience),
                                                     new XElement("category", jp.Category),
                                                     new XElement("experience", jp.JobDescription),
                                                     new XElement("salary", jp.Salary),
                                                     new XElement("education", ""),
                                                     new XElement("jobtype", jp.EmploymentType),
                                                     new XElement("email", jp.EmailTo),
                                                     new XElement("pod_id", jp.SchedulingPod)
                                                        )).Page(Page, 20)));
            return Ok(results);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/ZipRecruiter")]
        public HttpResponseMessage ZipRecruiter(int Page = 1, string Token = "")
        {
            if (Token != "Q0nI1kPEuUu3sktTI628xnO8IJfH9ZH0")
            {

                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return new HttpResponseMessage
                {
                    Content = new StringContent(res.ToString(), null, "application/xml")
                };
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_ZipRecruiterFeed_Result> x = db.sp_ZipRecruiterFeed().ToList();

            int totalNumberOfJobs = x.Count();
            var results =
                new XDocument(
                                new XDeclaration("1.0", "utf-8", "yes"),

                                new XElement("source", (from jp in x
                                                        select new XElement("job",

                                                    new XElement("title", new XCData(jp.Title != null ? jp.Title : "")),
                                                    new XElement("date", new XCData(Convert.ToString(jp.ActivationDate) != null ? Convert.ToString(jp.ActivationDate) : "")),
                                                    new XElement("referencenumber", new XCData(Convert.ToString(jp.JobPostingsID) != null ? Convert.ToString(jp.JobPostingsID) : null)),
                                                    new XElement("email", new XCData(jp.EmailTo != null ? jp.EmailTo : "")),
                                                    new XElement("company", new XCData(jp.CompanyName != null ? jp.CompanyName : "")),
                                                    new XElement("city", new XCData(jp.City != null ? jp.City : "")),
                                                    new XElement("state", new XCData(jp.State != null ? jp.State : "")),
                                                    new XElement("country", new XCData(jp.Country != null ? jp.Country : "")),
                                                    new XElement("category", new XCData(jp.Category != null ? jp.Category : "")),
                                                    new XElement("postalcode", new XCData(jp.ZipCode != null ? jp.ZipCode : "")),
                                                    new XElement("description", new XCData(jp.JobDescription != null ? jp.JobDescription : "" + jp.JobRequirements != null ? jp.JobRequirements : ""))
                                                       ))));

            string xml = Convert.ToString(results.Declaration) + results.ToString(SaveOptions.DisableFormatting);
            var t = results.Declaration;
            return new HttpResponseMessage
            {
                Content = new StringContent(xml, null, "application/xml")
            };
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/SnagAJob")]
        public IHttpActionResult SnagAJob(int Page = 1, string Token = "")
        {
            if (Token != "KLKtjv1Hz5h1js9He9ZWv32EfJIR6KB7")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;
            IEnumerable<sp_SnagaJobFeed_Result> x = db.sp_SnagaJobFeed().ToList();
            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("Postings", new XElement("CustomerId", "252863"),
                                                        (from jp in x
                                                         select new XElement("Item",
                                                         new XElement("Brand", jp.CompanyName),
                                                     new XElement("Job_Title", jp.Title),
                                                     new XElement("Location_Name", jp.Address + "Pod ID:" + jp.SchedulingPod),
                                                     new XElement("Address_Line_1", jp.Address + "Pod ID:" + jp.SchedulingPod),
                                                     new XElement("City", jp.City),
                                                     new XElement("State", jp.State),
                                                     new XElement("Zip", jp.ZipCode),
                                                     new XElement("URL", "http://board.thejobwindow.com/home/onlineapply/" + jp.JobPostingsID),
                                                     new XElement("Primary_Industry", jp.Category),
                                                     new XElement("Job_Description", jp.JobDescription),
                                                     new XElement("Additional_Requirements", jp.JobRequirements),
                                                     new XElement("Posted_Date", jp.ActivationDate),
                                                     new XElement("External_Posting_ID", jp.JobPostingsID)


                                                        )).Page(Page, 20)));
            return Ok(results);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/Indeed")]
        public IHttpActionResult Indeed(int Page = 1, string Token = "")
        {
            if (Token != "q1z894u44Gi1W96fal0z0d3PY8n7QJrI")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;
            IEnumerable<sp_IndeedFeed_Result> x = db.sp_IndeedFeed().ToList();
            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                //new XElement("Page", Page),
                                //new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("source", (from jp in x
                                                        select new XElement("job",
                                                    new XElement("title", jp.Title),
                                                    new XElement("date", jp.ActivationDate),
                                                    new XElement("referencenumber", jp.JobPostingsID),
                                                    new XElement("url", "http://www.lbsind.com/"),
                                                    new XElement("company", jp.CompanyName),
                                                    new XElement("description", jp.JobDescription),
                                                    new XElement("requirements", jp.JobRequirements),
                                                    new XElement("city", jp.City),
                                                    new XElement("state", jp.State),
                                                    new XElement("country", jp.Country),
                                                    new XElement("postalcode", jp.ZipCode),
                                                    new XElement("experience", jp.MinimumExperience + "-" + jp.MaximumExperience),
                                                    new XElement("category", jp.Category),
                                                    new XElement("salary", jp.Salary),
                                                    new XElement("jobtype", jp.EmploymentType),
                                                    new XElement("email", jp.EmailTo)
                                                       ))));
            return Ok(results);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/Beyondsss")]
        public string Beyond()
        {
            IEnumerable<sp_BeyondFeed_Result> x = db.sp_BeyondFeed().ToList();
            string res = "CompanyName|InformationStatus|InformationTitle|ExternalTrackingID|InformationCity|InformationState|InformationZip|InformationCBSAList|InformationCountry|InformationAreaCode|NumberOfPositions|PositionType1|PositionType2|PositionType3|CareerFocus1|CareerFocus2|ExpectedCost|SalaryMin|SalaryMax|MinimumEducation|ExperienceLevelID|Keywords|Description|Requirement|ApplyURL|ApplyEmail|ApplyFirstName|ApplyLastName|PartnerList|ExternalReferenceID|CompanyID|Source|ExpirationDate";
            string CompanyName, InformationStatus, InformationTitle, ExternalTrackingID, InformationCity, InformationState, InformationZip, InformationCBSAList, InformationCountry, InformationAreaCode, NumberOfPositions, PositionType1, PositionType2, PositionType3, CareerFocus1, CareerFocus2, ExpectedCost, SalaryMin, SalaryMax, MinimumEducation, ExperienceLevelID, Keyword, Description, Requirement, ApplyURL, ApplyEmail, ApplyFirstName, ApplyLastName, PartnerListr, ExternalReferenceID, CompanyID, Source, ExpirationDate;

            foreach (var job in x)
            {
                CompanyName = job.CompanyName;
                InformationStatus = "A";
                InformationTitle = job.Title;
                ExternalTrackingID = job.JobPostingsID.ToString();
                InformationCity = job.City;
                InformationState = job.State;
                InformationZip = job.ZipCode;
                InformationCBSAList = "";
                InformationCountry = job.Country;
                InformationAreaCode = "";
                NumberOfPositions = "";
                PositionType1 = "139";
                PositionType2 = "";
                PositionType3 = "";
                if (job.Category == "Accounting")
                {
                    CareerFocus1 = "7";

                }
                else if (job.Category == "Admin-Clerical")
                {
                    CareerFocus1 = "8";

                }
                else if (job.Category == "Automotive")
                {
                    CareerFocus1 = "29";

                }
                else if (job.Category == "Banking")
                {
                    CareerFocus1 = "7";

                }
                else if (job.Category == "Biotech")
                {
                    CareerFocus1 = "32";

                }
                else if (job.Category == "Business Development")
                {
                    CareerFocus1 = "8";

                }
                else if (job.Category == "Construction")
                {
                    CareerFocus1 = "9";

                }
                else if (job.Category == "Consultant")
                {
                    CareerFocus1 = "8";

                }
                else if (job.Category == "Customer Service")
                {
                    CareerFocus1 = "180";

                }
                else if (job.Category == "Design")
                {
                    CareerFocus1 = "21";
                }
                else if (job.Category == "Distribution-Shipping")
                {
                    CareerFocus1 = "29";
                }
                else if (job.Category == "Education")
                {
                    CareerFocus1 = "24";
                }
                else if (job.Category == "Engineering")
                {
                    CareerFocus1 = "25";

                }
                else if (job.Category == "Marketing")
                {
                    CareerFocus1 = "14";

                }
                else if (job.Category == "Entry Level")
                {
                    CareerFocus1 = "14";//,26,28,180,8,30,20

                }
                else if (job.Category == "Executive")
                {
                    CareerFocus1 = "26";

                }
                else if (job.Category == "Facilities")
                {
                    CareerFocus1 = "12";

                }
                else if (job.Category == "Finance")
                {
                    CareerFocus1 = "7";

                }
                else if (job.Category == "Advertising")
                {//Franchise
                    CareerFocus1 = "12";

                }
                else if (job.Category == "General Business")
                {
                    CareerFocus1 = "8";//,23,14,26

                }
                else if (job.Category == "General Labor")
                {
                    CareerFocus1 = "9";

                }
                else if (job.Category == "Government")
                {
                    CareerFocus1 = "194";

                }
                else if (job.Category == "Grocery")
                {
                    CareerFocus1 = "180";

                }
                else if (job.Category == "Health Care")
                {
                    CareerFocus1 = "10";

                }
                else if (job.Category == "Hospitality-Hotel")
                {
                    CareerFocus1 = "19";

                }
                else if (job.Category == "Human Resources")
                {
                    CareerFocus1 = "11";

                }
                else if (job.Category == "Information Technology")
                {
                    CareerFocus1 = "23";

                }
                else if (job.Category == "Installation-Maint-Repair")
                {
                    CareerFocus1 = "9";//,15,12

                }
                else if (job.Category == "Insurance")
                {
                    CareerFocus1 = "16";

                }
                else if (job.Category == "Inventory")
                {
                    CareerFocus1 = "30";

                }
                else if (job.Category == "Legal")
                {
                    CareerFocus1 = "27";

                }
                else if (job.Category == "Management")
                {
                    CareerFocus1 = "28";

                }
                else if (job.Category == "Manufacturing")
                {
                    CareerFocus1 = "15";

                }

                else if (job.Category == "Public Relations")
                {
                    CareerFocus1 = "14";

                }
                else if (job.Category == "Media-Journalism")
                {
                    CareerFocus1 = "22";

                }
                else if (job.Category == "Nonprofit-Social Services")
                {
                    CareerFocus1 = "17";//,31,12

                }
                else if (job.Category == "Nurse")
                {
                    CareerFocus1 = "10";

                }
                else if (job.Category == "Other")
                {
                    CareerFocus1 = "12";

                }
                else if (job.Category == "Pharmaceutical")
                {
                    CareerFocus1 = "10";

                }
                else
                {
                    CareerFocus1 = "14";
                }
                CareerFocus2 = "";
                ExpectedCost = "";
                SalaryMin = "";
                SalaryMax = "";
                MinimumEducation = "";
                ExperienceLevelID = "";
                Keyword = "";
                Description = job.JobDescription;
                Requirement = job.JobRequirements;
                ApplyURL = "";
                ApplyEmail = job.EmailTo;
                ApplyFirstName = "";
                ApplyLastName = "";
                PartnerListr = "";
                ExternalReferenceID = "";
                CompanyID = "4329693";
                Source = "TheJobWindow";
                ExpirationDate = job.ExpirationDate.ToString();
                res = res + CompanyName + "|" + InformationStatus + "|" + InformationTitle + "|" + ExternalTrackingID + "|" + InformationCity + "|" + InformationState + "|" + InformationZip + "|" + InformationCBSAList + "|" + InformationCountry + "|" + InformationAreaCode + "|" + NumberOfPositions + "|" + PositionType1 + "|" + PositionType2 + "|" + PositionType3 + "|" + CareerFocus1 + "|" + CareerFocus2 + "|" + ExpectedCost + "|" + SalaryMin + "|" + SalaryMax + "|" + MinimumEducation + "|" + ExperienceLevelID + "|" + Keyword + "|" + "" + "|" + Description + "|" + Requirement + "|" + ApplyURL + "|" + ApplyEmail + "|" + ApplyFirstName + "|" + ApplyLastName + "|" + PartnerListr + "|" + ExternalReferenceID + "|" + CompanyID + "|" + Source + "|" + ExpirationDate + "|" + Environment.NewLine;
            }
            System.IO.File.WriteAllText(@"F:/JobWindow/JobBoard.WebAPI/BeyondFeed/4329693.txt", res);
            //System.IO.File.WriteAllText("z" + @"BeyondFeed\4329693.txt", res);

            string filePath = HostingEnvironment.MapPath("/BeyondFeed/4329693.txt");
            return res;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/Beyond")]
        public IHttpActionResult Beyonds(int Page = 1, string Token = "")
        {
            if (Token != "RzN0PL05BBlkVham5F4l888g9xlvnhH3")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;
            IEnumerable<sp_BeyondFeed_Result> x = db.sp_BeyondFeed().ToList();
            int totalNumberOfJobs = x.Count();

            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("source", (from jp in x
                                                        select new XElement("job",
                                                    new XElement("InformationStatus", "A"),
                                                    new XElement("InformationTitle", jp.Title),
                                                    new XElement("InformationCity", jp.City),
                                                    new XElement("InformationState", jp.State),
                                                    new XElement("InformationZip", jp.ZipCode),
                                                    new XElement("InformationCBSAList", ""),
                                                    new XElement("InformationCountry", jp.Country),
                                                    new XElement("InformationAreaCode", ""),
                                                    new XElement("NumberOfPositions", ""),
                                                    new XElement("PositionType1", "139"),
                                                    new XElement("PositionType2", ""),
                                                    new XElement("PositionType3", ""),
                                                    new XElement("CareerFocus1", jp.Category),
                                                    new XElement("CareerFocus2", ""),
                                                    new XElement("ExpectedCost", ""),
                                                    new XElement("SalaryMin", jp.Salary),
                                                    new XElement("SalaryMax", ""),
                                                    new XElement("MinimumEducation", ""),
                                                    new XElement("ExperienceLevelID", ""),
                                                    new XElement("Keyword", ""),
                                                    new XElement("Description", jp.JobDescription),
                                                    new XElement("Requirement", jp.JobRequirements),
                                                    new XElement("ApplyURL", ""),
                                                    new XElement("ApplyEmail", jp.EmailTo),
                                                    new XElement("ApplyFirstName", ""),
                                                    new XElement("ApplyLastName", ""),
                                                    new XElement("PartnerListr", ""),
                                                    new XElement("ExternalReferenceID", ""),
                                                    new XElement("CompanyID", "4329693"),
                                                    new XElement("Source", "TheJobWindow"),
                                                    new XElement("ExpirationDate", jp.ExpirationDate)
                                                      )).Page(Page, 20)));
            return Ok(results);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/Glassdoor")]
        public IHttpActionResult Glassdoor(string Token = "")
        {
            if (Token != "26BKpBB9NjY8Aa9Ogln6kSIA7KFyj3456")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_GlassdoorFeed_Result> x = db.sp_GlassdoorFeed().ToList();

            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("jobs", (from jp in x
                                                      select new XElement("job",
                                                  new XElement("title", jp.Title),
                                                  new XElement("job-board-name", "Job Window"),
                                                  new XElement("job-board-url", ""),
                                                  new XElement("job-code", jp.JobPostingsID),
                                                  new XElement("detail-url", ""),
                                                  new XElement("apply-url", ""),
                                                  new XElement("job-category", jp.Category),
                                                  new XElement("description", new XElement("summary", jp.JobDescription), new XElement("requried-skills", jp.JobRequirements), new XElement("required-experience", jp.MinimumExperience + "-" + jp.MaximumExperience),
                                                  new XElement("full-time", jp.EmploymentType == "Full Time" ? "1" : ""), new XElement("part-time", jp.EmploymentType == "Part Time" ? "1" : ""), new XElement("internship", jp.EmploymentType == "Intern" ? "1" : ""), new XElement("contract", jp.EmploymentType == "Contractor" ? "1" : ""), new XElement("temporary", jp.EmploymentType == "Seasonal" ? "1" : "")),
                                                  new XElement("compensation", new XElement("salary-amount", jp.Salary)),
                                                  new XElement("posted-date", jp.ActivationDate),
                                                  new XElement("close-date", jp.ExpirationDate),
                                                  new XElement("location", new XElement("address", jp.Address), new XElement("city", jp.City),
                                                  new XElement("state", jp.State),
                                                  new XElement("zip", jp.ZipCode),
                                                  new XElement("country", jp.Country)),
                                                  new XElement("contact", new XElement("name", ""), new XElement("email", ""), new XElement("hiring-manager-name", ""), new XElement("hiring-manager-email", ""), new XElement("phone", ""), new XElement("fax", "")),
                                                  new XElement("company", new XElement("name", "The Job Window"), new XElement("description", ""), new XElement("industry", ""), new XElement("url", "http://thejobwindow.com/")),
                                                  new XElement("contact_email", jp.EmailTo),
                                                  new XElement("pod_id", jp.SchedulingPod)
                                                     ))));
            return Ok(results);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/Jobing")]
        public IHttpActionResult Jobing(int Page = 1, string Token = "")
        {
            if (Token != "2diKpBB9NjY8Aa9Ogln6kSIA7KFyj3456")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_JobingFeed_Result> x = db.sp_JobingFeed().ToList();

            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("jobs", (from jp in x
                                                      select new XElement("job",
                                                  new XElement("title", jp.Title),
                                                  new XElement("job-board-name", "Job Window"),
                                                  new XElement("job-board-url", ""),
                                                  new XElement("job-code", jp.JobPostingsID),
                                                  new XElement("detail-url", ""),
                                                  new XElement("apply-url", ""),
                                                  new XElement("job-category", jp.Category),
                                                  new XElement("description", new XElement("summary", jp.JobDescription), new XElement("requried-skills", jp.JobRequirements), new XElement("required-experience", jp.MinimumExperience + "-" + jp.MaximumExperience),
                                                  new XElement("full-time", jp.EmploymentType == "Full Time" ? "1" : ""), new XElement("part-time", jp.EmploymentType == "Part Time" ? "1" : ""), new XElement("internship", jp.EmploymentType == "Intern" ? "1" : ""), new XElement("contract", jp.EmploymentType == "Contractor" ? "1" : ""), new XElement("temporary", jp.EmploymentType == "Seasonal" ? "1" : "")),
                                                  new XElement("compensation", new XElement("salary-amount", jp.Salary)),
                                                  new XElement("posted-date", jp.ActivationDate),
                                                  new XElement("close-date", jp.ExpirationDate),
                                                  new XElement("location", new XElement("address", jp.Address), new XElement("city", jp.City),
                                                  new XElement("state", jp.State),
                                                  new XElement("zip", jp.ZipCode),
                                                  new XElement("country", jp.Country)),
                                                  new XElement("contact", new XElement("name", ""), new XElement("email", ""), new XElement("hiring-manager-name", ""), new XElement("hiring-manager-email", ""), new XElement("phone", ""), new XElement("fax", "")),
                                                  new XElement("company", new XElement("name", "The Job Window"), new XElement("description", ""), new XElement("industry", ""), new XElement("url", "http://thejobwindow.com/")),
                                                  new XElement("contact_email", jp.EmailTo),
                                                  new XElement("pod_id", jp.SchedulingPod)
                                                     )).Page(Page, 20)));
            return Ok(results);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/Monster")]
        public IHttpActionResult Monster(int Page = 1, string Token = "")
        {
            if (Token != "07rY6V3694buj8Z473Je8u8tDzj37hXM")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_Monster_Result> x = db.sp_Monster().ToList();

            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("source", (from jp in x
                                                        select new XElement("job",
                                                    new XElement("title", jp.Title),
                                                    new XElement("date", jp.ActivationDate),
                                                    new XElement("referencenumber", jp.JobPostingsID),
                                                    new XElement("email", jp.EmailTo),
                                                    new XElement("CompanyName", jp.CompanyName),
                                                    new XElement("city", jp.City),
                                                    new XElement("state", jp.State),
                                                    new XElement("country", jp.Country),
                                                    new XElement("category", jp.Category),
                                                    new XElement("postalcode", jp.ZipCode),
                                                    new XElement("description", jp.JobDescription + "<br>" + jp.JobRequirements)
                                                       )).Page(Page, 20)));
            return Ok(results);
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/CareerBuilder")]
        public IHttpActionResult CareerBuilder(int Page = 1, string Token = "")
        {
            if (Token != "gj5i0K7quIyON1ILW5y32P8yUt9dFtSd")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_CareerBuilder_Result> x = db.sp_CareerBuilder().ToList();

            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("source", (from jp in x
                                                        select new XElement("job",
                                                    new XElement("title", jp.Title),
                                                    new XElement("date", jp.ActivationDate),
                                                    new XElement("referencenumber", jp.JobPostingsID),
                                                    new XElement("email", jp.EmailTo),
                                                    new XElement("CompanyName", jp.CompanyName),
                                                    new XElement("city", jp.City),
                                                    new XElement("state", jp.State),
                                                    new XElement("country", jp.Country),
                                                    new XElement("category", jp.Category),
                                                    new XElement("postalcode", jp.ZipCode),
                                                    new XElement("description", jp.JobDescription + "<br>" + jp.JobRequirements)
                                                       )).Page(Page, 20)));
            return Ok(results);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/Craigslist")]
        public IHttpActionResult Craigslist(int Page = 1, string Token = "")
        {
            if (Token != "e7x0f1qw6z0HDa8X4DkpY8YJTm929qOP")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_Craigslist_Result> x = db.sp_Craigslist().ToList();

            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("source", (from jp in x
                                                        select new XElement("job",
                                                    new XElement("title", jp.Title),
                                                    new XElement("date", jp.ActivationDate),
                                                    new XElement("referencenumber", jp.JobPostingsID),
                                                    new XElement("email", jp.EmailTo),
                                                    new XElement("CompanyName", jp.CompanyName),
                                                    new XElement("city", jp.City),
                                                    new XElement("state", jp.State),
                                                    new XElement("country", jp.Country),
                                                    new XElement("category", jp.Category),
                                                    new XElement("postalcode", jp.ZipCode),
                                                    new XElement("description", jp.JobDescription + "<br>" + jp.JobRequirements)
                                                       )).Page(Page, 20)));
            return Ok(results);
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/TweetMyJob")]
        public IHttpActionResult TweetMyJob(int Page = 1, string Token = "")
        {
            if (Token != "e7x0f1qw6z0HDa86uy67TpY8YJTm572qOP")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_TweetMyJobs_Result> x = db.sp_TweetMyJobs().ToList();

            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("results", (from jp in x
                                                         select new XElement("job",
                                                     new XElement("jobid", jp.JobPostingsID),
                                                     new XElement("title", jp.Title),
                                                     new XElement("company", jp.CompanyName),
                                                     new XElement("posting_url", "http://board.thejobwindow.com/home/onlineapply/" + jp.JobPostingsID),
                                                     new XElement("application_url", "http://board.thejobwindow.com/home/onlineapply/" + jp.JobPostingsID),
                                                     new XElement("city", jp.City),
                                                     new XElement("state", jp.State),
                                                     new XElement("postal_code", jp.ZipCode),
                                                     new XElement("country", jp.Country),
                                                     new XElement("description", jp.JobDescription + "<br/><br/>" + jp.JobRequirements),
                                                     new XElement("position_type", jp.EmploymentType),
                                                     new XElement("salary", jp.Salary),
                                                     new XElement("source", "Job Window")
                                                        )).Page(Page, 20)));
            return Ok(results);
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("api/XMLFeed/JobWindow")]
        public IHttpActionResult JobWindow(int Page = 1, string Token = "")
        {
            if (Token != "e7x0f1qw6z0H45Trs7TpY8YJTm572qOP")
            {
                var res = new XElement("Error", new XElement("Message", "Authorization has been denied for this request."));
                return Ok(res);
            }
            _context.Configuration.ProxyCreationEnabled = false;

            IEnumerable<sp_JobWindow_Result> x = db.sp_JobWindow().ToList();

            int totalNumberOfJobs = x.Count();
            var results = new XElement("XML_Version_1.0",
                                new XElement("Page", Page),
                                new XElement("PerPage", "20"),
                                new XElement("Total", totalNumberOfJobs),
                                new XElement("results", (from jp in x
                                                         select new XElement("job",
                                                     new XElement("jobid", jp.JobPostingsID),
                                                     new XElement("title", jp.Title),
                                                     new XElement("company", jp.CompanyName),
                                                     new XElement("posting_url", "http://board.thejobwindow.com/home/onlineapply/" + jp.JobPostingsID),
                                                     new XElement("city", jp.City),
                                                     new XElement("state", jp.State),
                                                     new XElement("zipCode", jp.ZipCode),
                                                     new XElement("country", jp.Country),
                                                     new XElement("description", jp.JobDescription),
                                                     new XElement("requirements", jp.JobRequirement),
                                                     new XElement("employmentType", jp.EmploymentType),
                                                     new XElement("salary", jp.Salary)
                                                        )).Page(Page, 20)));
            return Ok(results);
        }
    }
}
