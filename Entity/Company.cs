using System;
using System.Collections.Generic;

namespace EntityLibrary
{
    public class Company
    {
        // Attributes
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        private List<JobListing> jobs = new List<JobListing>();

        public Company(int companyId, string companyName, string location)
        {
            CompanyID = companyId;
            CompanyName = companyName;
            Location = location;
        }


        public Company()
        {
        }

        // Method to post a job
        public void PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            JobListing job = new JobListing(
                jobId: 0, 
                companyId: this.CompanyID,
                company: this,
                jobTitle: jobTitle,
                jobDescription: jobDescription,
                jobLocation: jobLocation,
                salary: salary,
                jobType: jobType,
                postedDate: DateTime.Now
            );

            jobs.Add(job);

            Console.WriteLine($"Job '{jobTitle}' posted by {CompanyName}.");
        }

        public List<JobListing> GetJobs()
        {
            return jobs;
        }
    }
}
