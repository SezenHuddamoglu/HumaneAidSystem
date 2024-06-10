using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.Authentication.Queries
{
    public class CheckEmailExistsQuery : IRequest<Response<bool>>
    {
        public string Email { get; set; }
    }

    public class CheckEmailExistsQueryHandler : IRequestHandler<CheckEmailExistsQuery, Response<bool>>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public CheckEmailExistsQueryHandler(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<bool>> Handle(CheckEmailExistsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            return new Response<bool>(user != null);
        }
    }
}
