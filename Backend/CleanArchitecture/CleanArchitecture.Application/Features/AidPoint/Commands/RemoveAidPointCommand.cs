using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidPoint.Commands
{
    public class RemoveAidPointCommand : IRequest<Response<int>>
    {
        public string AidPointId { get; set; }
    }

    public class RemoveAidPointCommandHandler : IRequestHandler<RemoveAidPointCommand, Response<int>>
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public RemoveAidPointCommandHandler(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<int>> Handle(RemoveAidPointCommand request, CancellationToken cancellationToken)
        {
            var aidPoint = await _aidPointRepository.GetByIdAsync(request.AidPointId);

            if (aidPoint == null)
            {
                throw new EntityNotFoundException(nameof(AidPoint), request.AidPointId);
            }

            await _aidPointRepository.DeleteAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }
    }
}
