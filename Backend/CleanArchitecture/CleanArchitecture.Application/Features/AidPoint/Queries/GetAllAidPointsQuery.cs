using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using MediatR;

namespace CleanArchitecture.Core.Features.AidPoints.Queries.GetAllAidPoints
{
    public class GetAllAidPointsQuery : IRequest<Response<IEnumerable<AidPoint>>>
    {
    }

    public class GetAllAidPointsQueryHandler : IRequestHandler<GetAllAidPointsQuery, Response<IEnumerable<AidPoint>>>
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public GetAllAidPointsQueryHandler(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<IEnumerable<AidPoint>>> Handle(GetAllAidPointsQuery query, CancellationToken cancellationToken)
        {
            var aidPoints = await _aidPointRepository.GetAllAsync();
            return new Response<IEnumerable<AidPoint>>(aidPoints);
        }
    }
}
