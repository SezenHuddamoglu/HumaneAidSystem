using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidPoint.Commands
{
    public class UpdateAidPointStatusCommand : IRequest<Response<int>>
    {
        public string AidPointId { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateAidPointStatusCommandHandler : IRequestHandler<UpdateAidPointStatusCommand, Response<int>>
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public UpdateAidPointStatusCommandHandler(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<int>> Handle(UpdateAidPointStatusCommand request, CancellationToken cancellationToken)
        {
            var aidPoint = await _aidPointRepository.GetByIdAsync(request.AidPointId);

            if (aidPoint == null)
            {
                throw new EntityNotFoundException(nameof(AidPoint), request.AidPointId);
            }

            //aidPoint.IsActive = request.IsActive;

            await _aidPointRepository.UpdateAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }

    }
}
