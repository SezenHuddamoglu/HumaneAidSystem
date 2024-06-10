using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.Account.Commands
{
    public class UpdateEmailCommand : IRequest<Response<int>>
    {
        public int UserId { get; set; }
        public string NewEmail { get; set; }
    }

    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public UpdateEmailCommandHandler(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<Response<int>> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}