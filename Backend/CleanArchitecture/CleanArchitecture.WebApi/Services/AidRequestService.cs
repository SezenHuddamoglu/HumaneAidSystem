using CleanArchitecture.Core.DTOs.AidRequest;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Enums;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public class AidRequestService : IAidRequestService
    {
        private readonly IAidRequestRepositoryAsync _aidRequestRepository;
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public AidRequestService(
            IAidRequestRepositoryAsync aidRequestRepository,
            IAidPointRepositoryAsync aidPointRepository)
        {
            _aidRequestRepository = aidRequestRepository;
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<int>> AddAidRequestAsync(AddAidRequestRequest request, string userId)
        {
            var aidPointId = await _aidPointRepository.GetAidPointIdByName(request.AidPointName);
            if (aidPointId == null)
            {
                throw new ApiException($"Aid point with name {request.AidPointName} not found.");
            }

            var products = request.Products.Select(p => new Product
            {
                Name = p.Name,
                Category = p.Category,
                Amount = 0 // Default or initial value for Amount
            }).ToList();

            var aidRequest = new AidRequest
            {
                UserId = userId, // Kullanıcı kimliğini burada ayarla
                Products = products,
                AidPointName = request.AidPointName,
                AidPointId = aidPointId
            };

            await _aidRequestRepository.AddAsync(aidRequest);

            return new Response<int>(aidRequest.Id);
        }

        public async Task<Response<int>> UpdateAidRequestStatusAsync(UpdateAidRequestStatusRequest request)
        {
            var aidRequest = await _aidRequestRepository.GetByIdAsync(request.Id);

            if (aidRequest == null)
            {
                throw new EntityNotFoundException(nameof(AidRequest), request.Id);
            }

            aidRequest.Status = request.Status;

            await _aidRequestRepository.UpdateAsync(aidRequest);

            return new Response<int>(aidRequest.Id);
        }
    }
}

/*using CleanArchitecture.Core.DTOs.AidRequest;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public class AidRequestService : IAidRequestService
    {
        private readonly IAidRequestRepositoryAsync _aidRequestRepository;
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public AidRequestService(
            IAidRequestRepositoryAsync aidRequestRepository,
            IAidPointRepositoryAsync aidPointRepository)
        {
            _aidRequestRepository = aidRequestRepository;
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<int>> AddAidRequestAsync(AddAidRequestRequest request)
        {
            var aidPointId = await _aidPointRepository.GetAidPointIdByName(request.AidPointName);
            if (aidPointId == null)
            {
                throw new ApiException($"Aid point with name {request.AidPointName} not found.");
            }

            var products = request.Products.Select(p => new Product
            {
                Name = p.Name,
                Category = p.Category,
                Amount = 0 // Default or initial value for Amount
            }).ToList();

            var aidRequest = new AidRequest
            {
                UserId = request.UserId,
                Products = products,
                AidPointName = request.AidPointName,
                AidPointId = aidPointId
            };

            await _aidRequestRepository.AddAsync(aidRequest);

            return new Response<int>(aidRequest.Id);
        }

        public async Task<Response<int>> UpdateAidRequestStatusAsync(UpdateAidRequestStatusRequest request)
        {
            var aidRequest = await _aidRequestRepository.GetByIdAsync(request.Id);

            if (aidRequest == null)
            {
                throw new EntityNotFoundException(nameof(AidRequest), request.Id);
            }

            // aidRequest.Status = request.Status;

            await _aidRequestRepository.UpdateAsync(aidRequest);

            return new Response<int>(aidRequest.Id);
        }
    }
}*/

