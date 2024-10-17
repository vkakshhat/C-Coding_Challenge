using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAOLibrary; // Assuming this is where JobBoardContext is located
using EntityLibrary;
using Microsoft.EntityFrameworkCore;

namespace DAOLibrary
{
    public class DatabaseManager
    {
        private readonly JobBoardContext _context;

        public DatabaseManager()
        {
            _context = new JobBoardContext();
        }

        // Initialize the database schema and tables
        public void InitializeDatabase()
        {
            _context.Database.EnsureCreated(); // Creates the database if it doesn't exist
        }

        // Insert a new job listing
        public async Task InsertJobListing(JobListing job)
        {
            await _context.JobListing.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        // Insert a new company
        public async Task InsertCompany(Company company)
        {
            await _context.Company.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        // Insert a new applicant
        public async Task InsertApplicant(Applicant applicant)
        {
            await _context.Applicant.AddAsync(applicant);
            await _context.SaveChangesAsync();
        }

        // Insert a new job application
        public async Task InsertJobApplication(JobApplication application)
        {
            await _context.JobApplication.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        // Retrieve a list of all job listings
        public async Task<List<JobListing>> GetJobListings()
        {
            return await _context.JobListing.ToListAsync();
        }

        // Retrieve a list of all companies
        public async Task<List<Company>> GetCompanies()
        {
            return await _context.Company.ToListAsync();
        }

        // Retrieve a list of all applicants
        public async Task<List<Applicant>> GetApplicants()
        {
            return await _context.Applicant.ToListAsync();
        }

        // Retrieve a list of job applications for a specific job listing
        public async Task<List<JobApplication>> GetApplicationsForJob(int jobID)
        {
            return await _context.JobApplication
                .Where(app => app.JobID == jobID) // Assuming JobID is a property in JobApplication
                .ToListAsync();
        }
    }
}

