using AuthenticationService.Api.Models.Users;

namespace AuthenticationService.Api.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> RegisterUserAsync(User user, string roleName);
    }
}
