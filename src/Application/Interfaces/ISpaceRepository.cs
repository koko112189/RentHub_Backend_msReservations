using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISpaceRepository
    {
        Task<Space?> GetByIdAsync(Guid id);
        Task<IEnumerable<Space>> GetAllAsync();
        Task AddAsync(Space reservation);
        Task UpdateAsync(Space reservation);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
