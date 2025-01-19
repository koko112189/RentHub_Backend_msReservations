using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicantRepository
    {
        Task<Applicant> GetByIdAsync(Guid id);
        Task<Applicant?> GetByDocumentAsync(string document);
        Task<IEnumerable<Applicant>> GetAllAsync();
        Task AddAsync(Applicant reservation);
        Task UpdateAsync(Applicant reservation);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
