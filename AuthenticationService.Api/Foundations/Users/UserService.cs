using AuthenticationService.Api.Brokers.Loggings;
using AuthenticationService.Api.Brokers.UserManagement;
using AuthenticationService.Api.Models.Users;

namespace AuthenticationService.Api.Foundations.Users
{
    public partial class UserService : IUserService
    {
        private readonly IUserManagement userManagement;

        private readonly ILoggingBroker loggingBroker;

        public UserService(IUserManagement userManagement, ILoggingBroker loggingBroker)
        {
            this.userManagement = userManagement;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<User> RegisterUserAsync(User user, string roleName) =>
            TryCatch(async () =>
            {
                return await userManagement.InsertUserAsync(user, roleName);
            });
    }
}
