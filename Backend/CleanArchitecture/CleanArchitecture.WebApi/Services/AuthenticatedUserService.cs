using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            UserId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            Role = user?.FindFirstValue(ClaimTypes.Role);
        }

        public string UserId { get; }
        public string Role { get; }
    }
}
