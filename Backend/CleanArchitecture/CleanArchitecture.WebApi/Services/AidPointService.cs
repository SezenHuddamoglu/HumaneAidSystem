using AutoMapper;
using CleanArchitecture.Application.DTOs.AidPoint;
using CleanArchitecture.Core.DTOs.AidPoint;
using CleanArchitecture.Core.DTOs.Product;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public class AidPointService : IAidPointService
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;
        private readonly IGeocodingService _geocodingService;
        private readonly IRoutingService _routingService;
        private IRoutingService routingService;
        private readonly IMapper _mapper;

        public AidPointService(
            IAidPointRepositoryAsync aidPointRepository,
            IGeocodingService geocodingService,
            IRoutingService routingService, IMapper mapper)
        {
            _aidPointRepository = aidPointRepository;
            _geocodingService = geocodingService;
            _routingService = routingService;
            _mapper = mapper;
        }

        public async Task<Response<int>> AddAidPointAsync(AddAidPointRequest request)
        {
            var aidPoint = new AidPoint
            {
                Name = request.Name,
                Location = request.Location,
                Status = request.Status,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
               // AidPointId= request.AidPointId
            };

            await _aidPointRepository.AddAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }

        public async Task<Response<int>> RemoveAidPointAsync(object id)
        {
            var aidPoint = await _aidPointRepository.GetByIdAsync(id);

            if (aidPoint == null)
            {
                throw new EntityNotFoundException(nameof(AidPoint), id);
            }

            await _aidPointRepository.DeleteAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }

        public async Task<Response<int>> UpdateAidPointStatusAsync(object id, UpdateAidPointStatusRequest request)
        {
            var aidPoint = await _aidPointRepository.GetByIdAsync(id);

            if (aidPoint == null)
            {
                throw new EntityNotFoundException(nameof(AidPoint), id);
            }

            aidPoint.Status = request.Status;

            await _aidPointRepository.UpdateAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }

        public async Task<Response<AidPointResponse>> GetAidPointByLocationAsync(string location)
        {
            var aidPoint = await _aidPointRepository.GetByLocationAsync(location);

            if (aidPoint == null)
            {
                return new Response<AidPointResponse>(null, $"Aid point with location {location} not found.", false);
            }

            var response = new AidPointResponse
            {
                Id = aidPoint.Id,
                Name = aidPoint.Name,
                Location = aidPoint.Location,
                Status = aidPoint.Status
            };

            return new Response<AidPointResponse>(response);
        }

        public async Task<Response<List<SearchAidPointsResponse>>> SearchAidPointsAsync(SearchAidPointsRequest request)
        {
            var aidPoints = await _aidPointRepository.SearchAsync(request.Keyword);

            if (aidPoints == null || !aidPoints.Any())
            {
                throw new ApiException($"No aid points found for search term: {request.Keyword}.");
            }

            var response = aidPoints.Select(ap => new SearchAidPointsResponse
            {
                Id = ap.Id,
                Name = ap.Name,
                Location = ap.Location,
                Status = ap.Status,
                 //AidPointId = ap.AidPointId,
                Latitude = ap.Latitude,
                Longitude = ap.Longitude
            }).ToList();

            return new Response<List<SearchAidPointsResponse>>(response);
        }
        //Google Maps için en kýsa route yolu izlenimi
    
    
        public async Task<AidPoint> GetAidPointLocationAsync(string address)
        {
            return await _geocodingService.GetCoordinatesAsync(address);
        }

        
        public async Task<Response<string>> GetRouteAsync(AidPoint origin, AidPoint destination)
        {
            var route = await _routingService.GetRouteAsync(origin, destination);
            return new Response<string>(route);
        }

        public async Task<Response<List<AidPointResponse>>> GetAllAidPointsAsync()
        {
            var aidPoints = await _aidPointRepository.GetAllAsync();
            var aidPointResponses = aidPoints.Select(aidPoint => new AidPointResponse
            {
                Id = aidPoint.Id,
                Name = aidPoint.Name,
                Location = aidPoint.Location,
                //AidPointId = aidPoint.AidPointId,
                Status = aidPoint.Status,
                Latitude = aidPoint.Latitude,
                Longitude = aidPoint.Longitude
            }).ToList();

            return new Response<List<AidPointResponse>>(aidPointResponses);
        }

    }
}
/*
 * using CleanArchitecture.Application.DTOs.AidPoint;
using CleanArchitecture.Core.DTOs.AidPoint;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public class AidPointService : IAidPointService
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public AidPointService(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<int>> AddAidPointAsync(AddAidPointRequest request)
        {
            var aidPoint = new AidPoint
            {
                Name = request.Name,
                Location = request.Location,
                Status = request.Status,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
               // AidPointId= request.AidPointId
            };

            await _aidPointRepository.AddAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }

        public async Task<Response<int>> RemoveAidPointAsync(object id)
        {
            var aidPoint = await _aidPointRepository.GetByIdAsync(id);

            if (aidPoint == null)
            {
                throw new EntityNotFoundException(nameof(AidPoint), id);
            }

            await _aidPointRepository.DeleteAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }

        public async Task<Response<int>> UpdateAidPointStatusAsync(object id, UpdateAidPointStatusRequest request)
        {
            var aidPoint = await _aidPointRepository.GetByIdAsync(id);

            if (aidPoint == null)
            {
                throw new EntityNotFoundException(nameof(AidPoint), id);
            }

            aidPoint.Status = request.Status;

            await _aidPointRepository.UpdateAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }

        public async Task<Response<AidPointResponse>> GetAidPointByLocationAsync(string location)
        {
            var aidPoint = await _aidPointRepository.GetByLocationAsync(location);

            if (aidPoint == null)
            {
                return new Response<AidPointResponse>(null, $"Aid point with location {location} not found.", false);
            }

            var response = new AidPointResponse
            {
                Id = aidPoint.Id,
                Name = aidPoint.Name,
                Location = aidPoint.Location,
                Status = aidPoint.Status
            };

            return new Response<AidPointResponse>(response);
        }

        public async Task<Response<List<SearchAidPointsResponse>>> SearchAidPointsAsync(SearchAidPointsRequest request)
        {
            var aidPoints = await _aidPointRepository.SearchAsync(request.Keyword);

            if (aidPoints == null || !aidPoints.Any())
            {
                throw new ApiException($"No aid points found for search term: {request.Keyword}.");
            }

            var response = aidPoints.Select(ap => new SearchAidPointsResponse
            {
                Id = ap.Id,
                Name = ap.Name,
                Location = ap.Location,
                Status = ap.Status,
               // AidPointId = ap.AidPointId,
                Latitude = ap.Latitude,
                Longitude = ap.Longitude
            }).ToList();

            return new Response<List<SearchAidPointsResponse>>(response);
        }
    }
}



 */