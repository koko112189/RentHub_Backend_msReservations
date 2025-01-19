using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly ApplicantsDbContext _context;

        public ApplicantRepository(ApplicantsDbContext context)
        {
            _context = context;
        }

        public async Task<Applicant?> GetByIdAsync(Guid id)
        {
            var applicant = await _context.Applicants 
            .FirstOrDefaultAsync(r => r.Id == id);

            return applicant;
        }

        public async Task<IEnumerable<Applicant>> GetAllAsync()
        {
            var query =  _context.Applicants.AsNoTracking().AsQueryable();

            var applicants = await query.ToListAsync();
            return applicants;
        }

        public async Task AddAsync(Applicant applicant)
        {
            await _context.Applicants.AddAsync(applicant);
        }

        public async Task DeleteAsync(Guid id)
        {
            var applicant = await GetByIdAsync(id);
            if (applicant != null)
                _context.Applicants.Remove(applicant);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Applicant applicant)
        {
            var existingApplicant = await GetByIdAsync(applicant.Id);
            if (existingApplicant != null)
            {
                _context.Entry(existingApplicant).CurrentValues.SetValues(applicant);
            }
        }

        async public Task<Applicant?> GetByDocumentAsync(string document)
        {
            var applicant = await _context.Applicants.FirstOrDefaultAsync(r => r.Document == document);

            return applicant;
        }
    }
}
