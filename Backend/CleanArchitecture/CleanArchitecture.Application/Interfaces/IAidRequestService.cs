using CleanArchitecture.Core.DTOs.AidRequest;
using CleanArchitecture.Core.Wrappers;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IAidRequestService
    {
        Task<Response<int>> AddAidRequestAsync(AddAidRequestRequest request, string userId); // userId parametresi eklendi
        Task<Response<int>> UpdateAidRequestStatusAsync(UpdateAidRequestStatusRequest request);
    }
}
