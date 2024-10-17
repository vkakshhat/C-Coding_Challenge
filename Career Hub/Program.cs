using DAOLibrary;
using EntityLibrary;
using ExceptionLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace CareerHub
{
    class Program
    {
        static void Main(string[] args)
        {
            JobBoardContextImpl contextImpl = new JobBoardContextImpl();

            // Main menu loop
            while (true)
            {
                Console.WriteLine("\n===== Job Board System Menu =====");
                Console.WriteLine("1. Display Job Listings");
                Console.WriteLine("2. Create Applicant Profile");
                Console.WriteLine("3. Submit Job Application");
                Console.WriteLine("4. Post Job");
                Console.WriteLine("5. Get Jobs by Salary Range");
                Console.WriteLine("6. Calculate Average Salary");
                Console.WriteLine("7. Upload Resume");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option (1-8): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayJobListings();
                        break;
                    case "2":
                        CreateApplicantProfile();
                        break;
                    case "3":
                        SubmitJobApplication();
                        break;
                    case "4":
                        PostJob();
                        break;
                    case "5":
                        GetJobsBySalaryRange();
                        break;
                    case "6":
                        CalculateAverageSalary();
                        break;
                    case "7":
                        UploadResume();
                        break;
                    case "8":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        // Method to display job listings
        static void DisplayJobListings()
        {
            JobBoardContextImpl contextImpl = new JobBoardContextImpl();
            try
            {
                var jobListings = contextImpl.GetJobListings();
                if (jobListings.Count > 0)
                {
                    foreach (var job in jobListings)
                    {
                        Console.WriteLine($"Job Title: {job.JobTitle}, Company: {job.Company.CompanyName}, Salary: {job.Salary}, Job Type: {job.JobType}, Job Description: {job.JobDescription}, Job Location: {job.JobLocation}, Posted Date: {job.PostedDate}");
                    }
                }
                else
                {
                    Console.WriteLine("No job listings available.");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error retrieving job listings: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        // Method to create a new applicant profile
        static void CreateApplicantProfile()
        {
            JobBoardContextImpl contextImpl = new JobBoardContextImpl();
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            string email;
            while (true)
            {
                Console.Write("Enter Email: ");
                email = Console.ReadLine();
                if (IsValidEmail(email))
                    break;
                Console.WriteLine("Invalid email format. Please enter a valid email.");
            }

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            Applicant applicant = new Applicant(0, firstName, lastName, email, phone, null); 

            try
            {
                contextImpl.CreateApplicant(applicant);
                Console.WriteLine("Applicant profile created successfully.");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error creating applicant profile: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        // Method to submit a job application
        static void SubmitJobApplication()
        {
            JobBoardContextImpl contextImpl = new JobBoardContextImpl();

            try
            {
                Console.Write("Enter Applicant ID: ");
                int applicantId = int.Parse(Console.ReadLine());

                Console.Write("Enter Job ID: ");
                int jobId = int.Parse(Console.ReadLine());

                // Check application deadline
                DateTime applicationDeadline = DateTime.Now.AddDays(30); // Example deadline, should be fetched from job details
                if (DateTime.Now > applicationDeadline)
                {
                    Console.WriteLine("The application deadline has passed. You cannot submit your application.");
                    return;
                }

                Console.Write("Enter Cover Letter: ");
                string coverLetter = Console.ReadLine();

                JobApplication application = new JobApplication
                {
                    ApplicantID = applicantId,
                    JobID = jobId,
                    ApplicationDate = DateTime.Now,
                    CoverLetter = coverLetter
                };

                contextImpl.SubmitJobApplication(application);
                Console.WriteLine("Job application submitted successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter numeric values for Applicant ID and Job ID.");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error submitting job application: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        // Method to post a new job
        static void PostJob()
        {
            JobBoardContextImpl contextImpl = new JobBoardContextImpl();

            try
            {
                Console.Write("Enter Company ID: ");
                int companyId = int.Parse(Console.ReadLine());

                Console.Write("Enter Job Title: ");
                string jobTitle = Console.ReadLine();

                Console.Write("Enter Job Description: ");
                string jobDescription = Console.ReadLine();

                Console.Write("Enter Job Location: ");
                string jobLocation = Console.ReadLine();

                Console.Write("Enter Salary: ");
                decimal salary = decimal.Parse(Console.ReadLine());

                if (salary < 0)
                {
                    throw new ArgumentOutOfRangeException("Salary cannot be negative.");
                }

                Console.Write("Enter Job Type: ");
                string jobType = Console.ReadLine();

                JobListing jobListing = new JobListing(0, companyId, null, jobTitle, jobDescription, jobLocation, salary, jobType, DateTime.Now);

                contextImpl.PostJob(jobListing);
                Console.WriteLine("Job posted successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter numeric values for Company ID and Salary.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error posting job: {ex.Message}");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error posting job: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        // Method to get jobs by salary range
        static void GetJobsBySalaryRange()
        {
            JobBoardContextImpl contextImpl = new JobBoardContextImpl();

            try
            {
                Console.Write("Enter Minimum Salary: ");
                decimal minSalary = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Maximum Salary: ");
                decimal maxSalary = decimal.Parse(Console.ReadLine());

                if (minSalary < 0 || maxSalary < 0)
                {
                    throw new ArgumentOutOfRangeException("Salary values cannot be negative.");
                }

                var jobsInRange = contextImpl.GetJobsBySalaryRange(minSalary, maxSalary);
                if (jobsInRange.Count > 0)
                {
                    foreach (var job in jobsInRange)
                    {
                        Console.WriteLine($"Job Title: {job.JobTitle}, Company: {job.Company.CompanyName}, Salary: {job.Salary}");
                    }
                }
                else
                {
                    Console.WriteLine("No jobs found in the specified salary range.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter valid numeric values for salary range.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error retrieving jobs by salary range: {ex.Message}");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error retrieving jobs by salary range: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        // Method to calculate average salary of job listings
        static void CalculateAverageSalary()
        {
            JobBoardContextImpl contextImpl = new JobBoardContextImpl();

            try
            {
                var jobListings = contextImpl.GetJobListings();
                decimal totalSalary = 0;
                int count = 0;

                foreach (var job in jobListings)
                {
                    if (job.Salary < 0)
                    {
                        Console.WriteLine($"Invalid salary found for Job Title: {job.JobTitle}. Salary cannot be negative.");
                        continue;
                    }
                    totalSalary += job.Salary;
                    count++;
                }

                if (count == 0)
                {
                    Console.WriteLine("No valid job listings available to calculate average salary.");
                    return;
                }

                decimal averageSalary = totalSalary / count;
                Console.WriteLine($"The average salary of job listings is: {averageSalary:C}");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error calculating average salary: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        // Method to upload a resume
        static void UploadResume()
        {
            Console.Write("Enter the file path of your resume: ");
            string filePath = Console.ReadLine();

            try
            {
                // Simulated checks for file upload
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("The specified file does not exist.");
                }

                // Simulate file size check (for example, limiting to 2MB)
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 2 * 1024 * 1024) // 2MB
                {
                    throw new IOException("File size exceeds the allowed limit of 2MB.");
                }

                // Simulated file format check (e.g., only PDF and DOCX)
                string fileExtension = Path.GetExtension(filePath);
                if (fileExtension != ".pdf" && fileExtension != ".docx")
                {
                    throw new InvalidOperationException("File format not supported. Please upload a PDF or DOCX file.");
                }

                // Assuming the upload is successful
                Console.WriteLine("Resume uploaded successfully.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error uploading resume: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error uploading resume: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error uploading resume: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while uploading the resume: {ex.Message}");
            }
        }

        // Method to validate email format
        static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
