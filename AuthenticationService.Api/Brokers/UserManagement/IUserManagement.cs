using AuthenticationService.Api.Models.Users;

namespace AuthenticationService.Api.Brokers.UserManagement
{
    public interface IUserManagement
    {
        ValueTask<User> InsertUserAsync(User user, string roleName);
    }
}
