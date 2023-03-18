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

        public async ValueTask<User> InsertUserAsync(User user, string roleName)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);
            
            if (!roleExists)
            {
                var profession = new Profession();
                profession.Name = roleName;

                await roleManager.CreateAsync(profession);
            }

            await this.userManager.CreateAsync(user);
            await this.userManager.AddToRoleAsync(user, roleName);
  
            return user;
        }
    }
}
