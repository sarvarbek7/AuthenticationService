using AuthenticationService.Api.Models.Users;
using AuthenticationService.Api.Models.Users.Exceptions;
using EFxceptions.Models.Exceptions;
using Fare;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace AuthenticationService.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async void ShouldThrowUserDependencyValidationExceptionOnRegisterIfDuplicateKeyWithUniqueIndexExceptionOccursAndLogItAsync()
        {
            // given
            string uzbPhoneNumberFormat = @"^\+998[9873][01345789][0-9]{7}$";
            var xeger = new Xeger(uzbPhoneNumberFormat);
            var generatedPhoneNumber = xeger.Generate();
            User randomUser = CreateRandomUser();
            randomUser.PhoneNumber = generatedPhoneNumber;
            User inputUser = randomUser;
            string roleName = CreateRandomRole();
            string randomMessage = GetRandomMessage();

            var duplicateKeyWithUniqueIndexException =
                new DuplicateKeyWithUniqueIndexException(randomMessage);

            var alreadyExistsUserException = new AlreadyExistsUserException(
                duplicateKeyWithUniqueIndexException, duplicateKeyWithUniqueIndexException.DuplicateKeyValue);

            var expectedUserDependencyValidationException =
                new UserDependencyValidationException(alreadyExistsUserException);

            this.userManagement.Setup(broker =>
                broker.InsertUserAsync(inputUser, roleName)).ThrowsAsync(duplicateKeyWithUniqueIndexException);

            // when
            ValueTask<User> registerUserTask =
                this.userService.RegisterUserAsync(inputUser, roleName);

            // then
            await Assert.ThrowsAsync<UserDependencyValidationException>(() => 
                registerUserTask.AsTask());

            this.userManagement.Verify(broker => 
                broker.InsertUserAsync(inputUser, roleName), 
                Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedUserDependencyValidationException))), 
                Times.Once);

            this.userManagement.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowUserDependencyValidationExceptionOnRegisterIfDuplicateKeyExceptionOccursAndLogItAsync()
        {
            // given
            string uzbPhoneNumberFormat = @"^\+998[9873][01345789][0-9]{7}$";
            var xeger = new Xeger(uzbPhoneNumberFormat);
            var generatedPhoneNumber = xeger.Generate();
            User randomUser = CreateRandomUser();
            randomUser.PhoneNumber = generatedPhoneNumber;
            User inputUser = randomUser;
            string roleName = CreateRandomRole();
            string randomMessage = GetRandomMessage();

            var duplicateKeyException =
                new DuplicateKeyException(randomMessage);

            var alreadyExistsUserException = new AlreadyExistsUserException(
                duplicateKeyException);

            var expectedUserDependencyValidationException =
                new UserDependencyValidationException(alreadyExistsUserException);

            this.userManagement.Setup(broker =>
                broker.InsertUserAsync(inputUser, roleName)).ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<User> registerUserTask =
                this.userService.RegisterUserAsync(inputUser, roleName);

            // then
            await Assert.ThrowsAsync<UserDependencyValidationException>(() =>
                registerUserTask.AsTask());

            this.userManagement.Verify(broker =>
                broker.InsertUserAsync(inputUser, roleName),
                Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedUserDependencyValidationException))),
                Times.Once);

            this.userManagement.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowUserDependencyExceptionOnRegisterIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            string uzbPhoneNumberFormat = @"^\+998[9873][01345789][0-9]{7}$";
            var xeger = new Xeger(uzbPhoneNumberFormat);
            var generatedPhoneNumber = xeger.Generate();
            User randomUser = CreateRandomUser();
            randomUser.PhoneNumber = generatedPhoneNumber;
            User inputUser = randomUser;
            string roleName = CreateRandomRole();
            SqlException sqlException = GetSqlException();

            var failedUserStorageException = new FailedUserStorageException(sqlException);

            var expectedUserDependencyException = new UserDependencyException(failedUserStorageException);

            this.userManagement.Setup(broker =>
                broker.InsertUserAsync(inputUser, roleName)).ThrowsAsync(sqlException);

            // when
            ValueTask<User> registerUserTask =
                this.userService.RegisterUserAsync(inputUser, roleName);

            // then
            await Assert.ThrowsAsync<UserDependencyException>(() =>
                registerUserTask.AsTask());

            this.userManagement.Verify(broker =>
                broker.InsertUserAsync(inputUser, roleName),
                Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedUserDependencyException))),
                Times.Once);

            this.userManagement.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}
