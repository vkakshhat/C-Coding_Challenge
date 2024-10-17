using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class JobApplication
    {
        public int ApplicationID { get; set; } //  primary key
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CoverLetter { get; set; }

        public virtual JobListing Job { get; set; }
        public virtual Applicant Applicant { get; set; }
    }

}
