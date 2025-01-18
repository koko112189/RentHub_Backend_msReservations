using Application.Interfaces;
using Application.Settings;
using Domain.Entities;
using Domain.Exceptions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;

namespace Application.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicantService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateApplicantAsync(Applicant applicant)
        {  
            try
            {
                await _unitOfWork.ApplicantRepository.AddAsync(applicant);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task DeleteApplicantAsync(Guid id)
        {
            var existingApplicant = await _unitOfWork.ApplicantRepository.GetByIdAsync(id);
            if (existingApplicant == null)
            {
                throw new ApplicantNotFoundException(id);
            }
            await _unitOfWork.ApplicantRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

      

        public async Task<Applicant?> GetApplicantByIdAsync(Guid id)
        {
            return await _unitOfWork.ApplicantRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Applicant>> GetApplicantsAsync()
        {
            try
            {
               return await _unitOfWork.ApplicantRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateApplicantAsync(Applicant applicant)
        {
            try
            {
                var existingApplicant = await _unitOfWork.ApplicantRepository.GetByIdAsync(applicant.Id);
                if (existingApplicant == null)
                {
                    throw new ApplicantNotFoundException(applicant.Id);
                }
                await _unitOfWork.ApplicantRepository.UpdateAsync(applicant);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
