using CleanArchitecture.Core.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IAidPointRepositoryAsync : IGenericRepositoryAsync<AidPoint>
    {
        Task GetByLocationAsync(string latitude, object longitude);
        Task<GetAidPointByLocationResponse> GetByLocationAsync(string location);
        Task<IEnumerable<AidPoint>> SearchAsync(string searchTerm);

        Task<IEnumerable<AidPoint>> GetAllAsync();
        Task<string> GetAidPointIdByName(string aidPointName); 
    }
}


