using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidOffer.Commands
{
    public class AcceptAidOfferCommand : IRequest<Response<int>>
    {
        public int OfferId { get; set; }
        public int UserId { get; set; }
    }

    public class AcceptAidOfferCommandHandler : IRequestHandler<AcceptAidOfferCommand, Response<int>>
    {
        private readonly IAidOfferRepositoryAsync _aidOfferRepository;
        private readonly IUserRepositoryAsync _userRepository;

        public AcceptAidOfferCommandHandler(IAidOfferRepositoryAsync aidOfferRepository, IUserRepositoryAsync userRepository)
        {
            _aidOfferRepository = aidOfferRepository;
            _userRepository = userRepository;
        }

        public Task<Response<int>> Handle(AcceptAidOfferCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
