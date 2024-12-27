using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationsDbContext _context;

        public ReservationRepository(ReservationsDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            var reservation = await _context.Reservations
            .Include(r => r.User) 
            .Include(r => r.Space) 
            .FirstOrDefaultAsync(r => r.Id == id);

            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync(
         Guid? userId,
         Guid? spaceId,
         DateTime? startDate,
         DateTime? endDate)
        {
            var query =  _context.Reservations.AsNoTracking().AsQueryable();

            if (userId.HasValue)
                query = query.Where(u => u.UserId == userId.Value);

            if(spaceId.HasValue)
                query = query.Where(s => s.SpaceId == spaceId.Value);

            if(startDate.HasValue)
                query = query.Where(s => s.StartDateTime >= startDate.Value);

            if(endDate.HasValue)
                query = query.Where(s => s.EndDateTime <= endDate.Value);

            var reservations = await query.Include(r => r.User).Include(r => r.Space).ToListAsync();
            return reservations;
        }

        public async Task<IEnumerable<Reservation>> GetOverlappingReservationsAsync(Guid spaceId, DateTime startDateTime, DateTime endDateTime)
        {
            return await _context.Reservations
                .Where(r => r.SpaceId == spaceId &&
                            r.StartDateTime < endDateTime &&
                            r.EndDateTime > startDateTime)
                .ToListAsync();
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task DeleteAsync(Guid id)
        {
            var reservation = await GetByIdAsync(id);
            if (reservation != null)
                _context.Reservations.Remove(reservation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            var existingReservation = await GetByIdAsync(reservation.Id);
            if (existingReservation != null)
            {
                _context.Entry(existingReservation).CurrentValues.SetValues(reservation);
            }
        }
    }
}
