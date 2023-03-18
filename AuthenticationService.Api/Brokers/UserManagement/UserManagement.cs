using AuthenticationService.Api.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Api.Brokers.UserManagement
{
    public class UserManagement : IUserManagement
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Profession> roleManager;

        public UserManagement(UserManager<User> userManager, RoleManager<Profession> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


    }
}
