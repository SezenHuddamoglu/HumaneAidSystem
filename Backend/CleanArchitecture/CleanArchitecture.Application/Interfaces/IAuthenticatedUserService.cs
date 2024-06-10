namespace CleanArchitecture.Core.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string Role { get; }
    }
}


