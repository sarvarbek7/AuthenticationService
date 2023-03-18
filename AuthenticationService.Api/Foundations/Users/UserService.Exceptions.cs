using AuthenticationService.Api.Models.Users;

namespace AuthenticationService.Api.Foundations.Users
{
    public partial class UserService
    {
        private delegate ValueTask<User> ReturningUserFunction();

        private async ValueTask<User> TryCatch(ReturningUserFunction returningUserFunction)
        {
            try
            {
                return await returningUserFunction();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
