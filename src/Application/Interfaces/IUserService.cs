using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<IEnumerable<User>> GetUserAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}
