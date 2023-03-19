using AuthenticationService.Api.Models.Users;
using AuthenticationService.Api.Models.Users.Exceptions;
using Moq;
using Xunit;

namespace AuthenticationService.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async void ShouldThrowUserValidationExceptionOnRegisterIfUserIsNullAsync()
        {
            // given
            User noUser = null;
            string randomRoleName = CreateRandomRole();
            var nullUserException = new NullUserException();

            var expectedUserValidationException = 
                new UserValidationException(nullUserException);

            // when
            ValueTask<User> registerUserTask = 
                this.userService.RegisterUserAsync(noUser, randomRoleName);

            // then
            await Assert.ThrowsAsync<UserValidationException>(() => 
                registerUserTask.AsTask());

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedUserValidationException))),
                Times.Once);

            this.userManagement.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}
