using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IAidRequestRepositoryAsync
    {
        Task AddAsync(AidRequest aidRequest);
        Task<AidRequest> GetByIdAsync(int id);
        Task UpdateAsync(AidRequest aidRequest);
    }
}
