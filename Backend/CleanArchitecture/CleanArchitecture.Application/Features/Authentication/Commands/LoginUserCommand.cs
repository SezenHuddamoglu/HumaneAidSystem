using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Exceptions;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
//using CleanArchitecture.Infrastructure.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.Authentication.Commands
{
    public class LoginUserCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<string>>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public LoginUserCommandHandler(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new EntityNotFoundException("User", request.Email);
            }

           /* if (!PasswordVerificationHelper.VerifyPassword(user.PasswordHash, request.Password))
            {
                throw new BadRequestException("Invalid password.");
            }*/

            // Return user's token if authentication is successful
            return new Response<string>(user.Token);
        }
    }
}
