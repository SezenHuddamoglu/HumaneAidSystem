using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidPoint.Queries
{
    public class SearchAidPointsQuery : IRequest<Response<List<SearchAidPointsResponse>>>
    {
        public string SearchTerm { get; set; }
    }

    public class SearchAidPointsQueryHandler : IRequestHandler<SearchAidPointsQuery, Response<List<SearchAidPointsResponse>>>
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public SearchAidPointsQueryHandler(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<List<SearchAidPointsResponse>>> Handle(SearchAidPointsQuery query, CancellationToken cancellationToken)
        {
            var aidPoints = await _aidPointRepository.SearchAsync(query.SearchTerm);

            if (aidPoints == null || !aidPoints.Any())
            {
                throw new ApiException($"No aid points found for search term: {query.SearchTerm}.");
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

/*using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidPoint.Queries
{
    public class SearchAidPointsQuery : IRequest<Response<IEnumerable<SearchAidPointsResponse>>>
    {
        public string SearchTerm { get; set; }
    }

    public class SearchAidPointsQueryHandler : IRequestHandler<SearchAidPointsQuery, Response<IEnumerable<SearchAidPointsResponse>>>
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public SearchAidPointsQueryHandler(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<IEnumerable<SearchAidPointsResponse>>> Handle(SearchAidPointsQuery query, CancellationToken cancellationToken)
        {
            var aidPoints = await _aidPointRepository.SearchAsync(query.SearchTerm);

            if (aidPoints == null)
            {
                throw new ApiException($"No aid points found for search term: {query.SearchTerm}.");
            }

            return new Response<IEnumerable<SearchAidPointsResponse>>();
        }

    }
}
*/