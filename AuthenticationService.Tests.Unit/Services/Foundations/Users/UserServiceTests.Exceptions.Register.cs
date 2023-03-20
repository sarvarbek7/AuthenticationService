using AuthenticationService.Api.Models.Users;
using AuthenticationService.Api.Models.Users.Exceptions;
using EFxceptions.Models.Exceptions;
using Moq;
using Xunit;

namespace AuthenticationService.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async void ShouldThrowUserDependencyValidationExceptionOnRegisterIfDuplicateKeyErrorOccursAndLogITAsync()
        {
            // given
            User randomUser = CreateRandomUser();
            User inputUser = randomUser;
            string roleName = CreateRandomRole();
            string randomMessage = GetRandomMessage();
            var duplicateKeyException = new DuplicateKeyException(randomMessage);
            var failedUserStorageException = new FailedUserStorageException(duplicateKeyException);

            var expectedUserDependencyValidationException =
                new UserDependencyValidationException(failedUserStorageException);

            this.userManagement.Setup(broker =>
                broker.InsertUserAsync(inputUser, roleName)).ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<User> registerUserTask =
                this.userService.RegisterUserAsync(inputUser, roleName);

            // then
            await Assert.ThrowsAsync<UserDependencyValidationException>(() => 
                registerUserTask.AsTask());

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedUserDependencyValidationException))), 
                Times.Once);

            this.userManagement.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}
