using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IUserRepositoryAsync : IGenericRepositoryAsync<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(int userId);
    }
}

