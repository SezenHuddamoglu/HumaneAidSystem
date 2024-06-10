using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Enums;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidRequest.Commands
{
    public class UpdateAidRequestStatusCommand : IRequest<Response<bool>>
    {
        public int AidRequestId { get; set; }
        public AidRequestStatus Status { get; set; }
    }

    public class UpdateAidRequestStatusCommandHandler : IRequestHandler<UpdateAidRequestStatusCommand, Response<bool>>
    {
        private readonly IAidRequestRepositoryAsync _aidRequestRepository;

        public UpdateAidRequestStatusCommandHandler(IAidRequestRepositoryAsync aidRequestRepository)
        {
            _aidRequestRepository = aidRequestRepository;
        }

        public async Task<Response<bool>> Handle(UpdateAidRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var aidRequest = await _aidRequestRepository.GetByIdAsync(request.AidRequestId);

            if (aidRequest == null)
            {
                throw new ApiException($"Aid request with Id {request.AidRequestId} not found.");
            }

            aidRequest.Status = request.Status;
            await _aidRequestRepository.UpdateAsync(aidRequest);

            return new Response<bool>(true);
        }
    }
}
