using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISpaceService
    {
        Task CreateSpaceAsync(Space space);
        Task<IEnumerable<Space>> GetSpacesAsync();
        Task<Space?> GetSpaceByIdAsync(Guid id);
        Task UpdateSpaceAsync(Space space);
        Task DeleteSpaceAsync(Guid id);
    }
}
