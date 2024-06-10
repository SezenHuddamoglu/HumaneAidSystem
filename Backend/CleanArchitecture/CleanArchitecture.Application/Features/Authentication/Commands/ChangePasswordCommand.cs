using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
//using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Infrastructure.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.Authentication.Commands
{
    public class ChangePasswordCommand : IRequest<Response<bool>>
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response<bool>>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public ChangePasswordCommandHandler(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new EntityNotFoundException(nameof(User), request.UserId);

          /*  if (!PasswordVerificationHelper.VerifyPassword(user.PasswordHash, request.CurrentPassword))
                throw new InvalidPasswordException();*/

            /*user.PasswordHash = PasswordVerificationHelper.HashPassword(request.NewPassword);
            await _userRepository.UpdateAsync(user);*/

            return new Response<bool>(true);
        }
    }
}
