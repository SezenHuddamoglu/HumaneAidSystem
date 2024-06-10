using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidPoint.Commands
{
    public class AddAidPointCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string AidPointId { get; set; }

    }


    public class AddAidPointCommandHandler : IRequestHandler<AddAidPointCommand, Response<int>>
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public AddAidPointCommandHandler(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<int>> Handle(AddAidPointCommand request, CancellationToken cancellationToken)
        {
            var aidPoint = new CleanArchitecture.Application.Entities.AidPoint 
            {
                Name = request.Name,
                Location = request.Location,
                Status = request.Status,
                //AidPointId = request.AidPointId
            };

            await _aidPointRepository.AddAsync(aidPoint);

            return new Response<int>(aidPoint.Id);
        }
    }

}
