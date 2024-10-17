using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class Applicant
    {
        // Attributes
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }

        // Constructor (optional if you want to initialize the attributes directly)
        public Applicant(int applicantID, string firstName, string lastName, string email, string phone, string resume)
        {
            ApplicantID = applicantID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Resume = resume;
        }

        // Method to create an applicant profile
        public void CreateProfile(string email, string firstName, string lastName, string phone)
        {
            // Logic to update the applicant's profile information
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;

            Console.WriteLine($"Profile Created for: {FirstName} {LastName}, Email: {Email}, Phone: {Phone}");
        }

        public void ApplyForJob(int jobID, string coverLetter)
        {
            Console.WriteLine($"Applicant {FirstName} {LastName} applied for Job ID {jobID} with cover letter: {coverLetter}");
        }

        public void DisplayProfile()
        {
            Console.WriteLine($"Applicant ID: {ApplicantID}");
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"Resume: {Resume}");
        }
    }
}
