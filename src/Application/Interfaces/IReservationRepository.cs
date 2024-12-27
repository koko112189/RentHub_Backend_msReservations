using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Reservation>> GetAllAsync(
         Guid? userId,
         Guid? spaceId,
         DateTime? startDate,
         DateTime? endDate);
        Task<IEnumerable<Reservation>> GetOverlappingReservationsAsync(Guid spaceId, DateTime startDateTime, DateTime endDateTime);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
