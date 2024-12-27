using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReservationService
    {
        Task CreateReservationAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetReservationsAsync(Guid? userId,
         Guid? spaceId,
         DateTime? startDate,
         DateTime? endDate);
        Task<Reservation?> GetReservationByIdAsync(Guid id);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Guid id);
    }
}
