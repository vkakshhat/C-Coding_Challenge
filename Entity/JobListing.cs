using System;

namespace EntityLibrary
{
    public class JobListing
    {
        // Attributes
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }


        public JobListing()
        {
        }

        public JobListing(int jobId, int companyId, Company company, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType, DateTime postedDate)
        {
            JobID = jobId;
            CompanyID = companyId;
            Company = company;
            JobTitle = jobTitle;
            JobDescription = jobDescription;
            JobLocation = jobLocation;
            Salary = salary;
            JobType = jobType;
            PostedDate = postedDate;
        }

        public void DisplayJobDetails()
        {
            Console.WriteLine($"Job ID: {JobID}");
            Console.WriteLine($"Company: {Company.CompanyName}");
            Console.WriteLine($"Job Title: {JobTitle}");
            Console.WriteLine($"Description: {JobDescription}");
            Console.WriteLine($"Location: {JobLocation}");
            Console.WriteLine($"Salary: {Salary}");
            Console.WriteLine($"Job Type: {JobType}");
            Console.WriteLine($"Posted Date: {PostedDate.ToShortDateString()}");
        }

        public class Applicant
        {
            public int ApplicantID { get; set; }
            public string CoverLetter { get; set; }

            public Applicant(int applicantID, string coverLetter)
            {
                ApplicantID = applicantID;
                CoverLetter = coverLetter;
            }
        }
    }
}
