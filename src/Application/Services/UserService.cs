using Application.Interfaces;
using Domain.Exceptions.UserExceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task CreateUserAsync(User User)
        {
            var existingUser = await _UserRepository.GetByIdAsync(User.Id);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(User.Name);
            }

            await _UserRepository.AddAsync(User);
            await _UserRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var existingUser = await _UserRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                throw new UserNotFoundException(id);
            }

            await _UserRepository.DeleteAsync(id);
            await _UserRepository.SaveChangesAsync();
        }



    public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var User = await _UserRepository.GetByIdAsync(id);

            if (User == null)
            {
                throw new UserNotFoundException(id);
            }

            return User;
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await _UserRepository.GetAllAsync();
        }

        public async Task UpdateUserAsync(User User)
        {
            var existingUser = await _UserRepository.GetByIdAsync(User.Id);

            if (existingUser == null)
            {
                throw new UserNotFoundException(User.Id);
            }

            await _UserRepository.UpdateAsync(User);
            await _UserRepository.SaveChangesAsync();
        }
}
}