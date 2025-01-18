using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ApplicantDto
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string EducationLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public List<string> Skills { get; set; }
        public string Resume { get; set; }
        public string LinkedInProfile { get; set; }
    }
}