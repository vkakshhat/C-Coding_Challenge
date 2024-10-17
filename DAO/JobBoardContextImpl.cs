using EntityLibrary;
using ExceptionLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary
{
    public class JobBoardContextImpl
    {
        private readonly JobBoardContext _context;

        public JobBoardContextImpl()
        {
            _context = new JobBoardContext();
        }

        // 1. Job Listing Retrieval
        public List<JobListing> GetJobListings()
        {
            try
            {
                return _context.JobListing.Include("Company").ToList(); 
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException($"Error retrieving job listings: {ex.Message}");
            }
        }

        // 2. Applicant Profile Creation
        public void CreateApplicant(Applicant applicant)
        {
            try
            {
                _context.Applicant.Add(applicant);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException($"Error creating applicant profile: {ex.Message}");
            }
        }

        // 3. Job Application Submission
        public void SubmitJobApplication(JobApplication application)
        {
            try
            {
                _context.JobApplication.Add(application);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException($"Error submitting job application: {ex.Message}");
            }
        }

        // 4. Company Job Posting
        public void PostJob(JobListing job)
        {
            try
            {
                _context.JobListing.Add(job);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException($"Error posting job listing: {ex.Message}");
            }
        }

        // 5. Salary Range Query
        public List<JobListing> GetJobsBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            try
            {
                return _context.JobListing
                    .Where(j => j.Salary >= minSalary && j.Salary <= maxSalary)
                    .Include("Company")
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException($"Error retrieving jobs by salary range: {ex.Message}");
            }
        }
    }
}
