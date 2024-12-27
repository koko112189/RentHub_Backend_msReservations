using Application.Interfaces;
using Application.Settings;
using Domain.Exceptions;
using Domain.Exceptions.ReservationExceptions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ReservationsSettings _settings;

        public ReservationService( IUnitOfWork unitOfWork, IOptions<ReservationsSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _settings = settings.Value;
        }

        public async Task CreateReservationAsync(Reservation reservation)
        {
            // Validar tiempo mínimo y máximo
            var duration = (reservation.EndDateTime - reservation.StartDateTime).TotalMinutes;
            if (duration < _settings.MinDurationMinutes || duration > _settings.MaxDurationMinutes) 
            {
                throw new InvalidReservationDurationException("La duración de la reserva es inválida.");
            }

            // Validar solapamiento con otras reservas
            var overlaps = await _unitOfWork.ReservationRepository.GetOverlappingReservationsAsync(reservation.SpaceId, reservation.StartDateTime, reservation.EndDateTime);
               

            if (overlaps.Count() > 0)
            {
                throw new ReservationConflictException("El espacio ya está reservado en el intervalo especificado.");
            }

            try
            {
                await _unitOfWork.ReservationRepository.AddAsync(reservation);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task DeleteReservationAsync(Guid id)
        {
            var existingReservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id);
            if (existingReservation == null)
            {
                throw new ReservationNotFoundException(id);
            }
            await _unitOfWork.ReservationRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(Guid id)
        {
            return await _unitOfWork.ReservationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync(Guid? userId,
         Guid? spaceId,
         DateTime? startDate,
         DateTime? endDate)
        {
            try
            {
               return await _unitOfWork.ReservationRepository.GetAllAsync(userId, spaceId, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            try
            {
                var existingReservation = await _unitOfWork.ReservationRepository.GetByIdAsync(reservation.Id);
                if (existingReservation == null)
                {
                    throw new ReservationNotFoundException(reservation.Id);
                }
                await _unitOfWork.ReservationRepository.UpdateAsync(reservation);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
