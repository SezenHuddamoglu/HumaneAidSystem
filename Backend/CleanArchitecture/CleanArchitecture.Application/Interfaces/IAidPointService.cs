//using CleanArchitecture.Application.DTOs.AidPoint;
using CleanArchitecture.Core.DTOs.AidPoint;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IAidPointService
    {
        Task<Response<int>> AddAidPointAsync(AddAidPointRequest request);
        Task<Response<int>> RemoveAidPointAsync(object id);
        Task<Response<int>> UpdateAidPointStatusAsync(object id, UpdateAidPointStatusRequest request);
        Task<Response<AidPointResponse>> GetAidPointByLocationAsync(string location);
        Task<Response<List<SearchAidPointsResponse>>> SearchAidPointsAsync(SearchAidPointsRequest request);
        Task<Response<List<AidPointResponse>>> GetAllAidPointsAsync();
    }
}