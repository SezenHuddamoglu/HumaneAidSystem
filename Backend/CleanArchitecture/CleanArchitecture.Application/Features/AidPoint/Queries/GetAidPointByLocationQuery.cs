using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint;
using System.Threading.Tasks;
using System.Threading;
using CleanArchitecture.Core.Interfaces.Repositories;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidPoint.Queries
{
    public class GetAidPointByLocationQuery : IRequest<Response<GetAidPointByLocationResponse>>
    {
        public string Location { get; set; }
    }

    public class GetAidPointByLocationQueryHandler : IRequestHandler<GetAidPointByLocationQuery, Response<GetAidPointByLocationResponse>>
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public GetAidPointByLocationQueryHandler(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<GetAidPointByLocationResponse>> Handle(GetAidPointByLocationQuery query, CancellationToken cancellationToken)
        {
            var aidPoint = await _aidPointRepository.GetByLocationAsync(query.Location);

            if (aidPoint == null)
            {
                return new Response<GetAidPointByLocationResponse>(null, $"Aid point with location {query.Location} not found.", false);
            }

            return new Response<GetAidPointByLocationResponse>(aidPoint);
        }



    }
}

