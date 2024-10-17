using Microsoft.EntityFrameworkCore;
using EntityLibrary;
using System;
using UtilityLibrary;

namespace DAOLibrary
{
    public class JobBoardContext : DbContext
    {
        private readonly string _connectionString;

        public JobBoardContext()
        {
            _connectionString = DBPropertyUtil.ReturnCn("dbCn");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Connection string not found.");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<JobListing> JobListing { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobListing>().HasKey(j => j.JobID);
            modelBuilder.Entity<JobApplication>().HasKey(a => a.ApplicationID);
            modelBuilder.Entity<Company>().HasKey(c => c.CompanyID);
            modelBuilder.Entity<Applicant>().HasKey(a => a.ApplicantID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
