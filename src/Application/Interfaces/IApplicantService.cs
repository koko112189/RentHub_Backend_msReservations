using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicantService
    {
        Task CreateApplicantAsync(Applicant applicant);
        Task<IEnumerable<Applicant>> GetApplicantsAsync();
        Task<Applicant?> GetApplicantByIdAsync(Guid id);
        Task UpdateApplicantAsync(Applicant applicant);
        Task DeleteApplicantAsync(Guid id);
    }
}
