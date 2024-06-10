using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
//using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Infrastructure.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.Authentication.Commands
{
    public class ResetPasswordCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<bool>>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public ResetPasswordCommandHandler(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new EntityNotFoundException("User", request.Email);
            }

           /* user.PasswordHash = PasswordHashHelper.HashPassword(request.NewPassword);
            await _userRepository.UpdateAsync(user);*/

            return new Response<bool>(true);
        }
    }
}
