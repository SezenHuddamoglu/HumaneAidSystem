using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Exceptions;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.Authentication.Commands
{
    public class RegisterUserCommand : IRequest<Response<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }  // Yeni eklenen alan
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public RegisterUserCommandHandler(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Check if the email is already registered
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new EmailAlreadyExistsException(request.Email);
            }

            // Create the user entity
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Name = request.UserName,
                PasswordHash = request.Password, 
                Role = request.Role
            };

            // Add the user to the repository
            await _userRepository.AddAsync(user);

            // Return the Id of the newly registered user
            return new Response<int>(user.Id);
        }
    }
}
