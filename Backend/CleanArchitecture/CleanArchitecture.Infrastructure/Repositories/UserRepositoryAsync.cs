using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Repository;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _users;

        public UserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsUniqueUsernameAsync(string username)
        {
            return await _users.AllAsync(u => u.Name != username);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.FirstOrDefaultAsync(u => u.Name == username);
        }

        public Task<User> GetByIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}

