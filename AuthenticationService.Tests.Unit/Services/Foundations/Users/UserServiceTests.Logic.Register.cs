using AuthenticationService.Api.Models.Users;
using Fare;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace AuthenticationService.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async void ShouldRegisterUserAsync()
        {
            // given
            string uzbPhoneNumberFormat = @"^\+998[9873][01345789][0-9]{7}$";
            var xeger = new Xeger(uzbPhoneNumberFormat);
            var generatedPhoneNumber = xeger.Generate();
            User randomUser = CreateRandomUser();
            randomUser.PhoneNumber = generatedPhoneNumber;
            User inputUser = randomUser;
            User returningUser = inputUser;
            User expectedUser = returningUser.DeepClone();
            string randomRoleName = CreateRandomRole();

            this.userManagement.Setup(broker =>
                broker.InsertUserAsync(randomUser, randomRoleName))
                .ReturnsAsync(returningUser);

            // when
            User actualUser = await this.userService
                .RegisterUserAsync(inputUser, randomRoleName);

            // then
            actualUser.Should().BeEquivalentTo(expectedUser);

            this.userManagement.Verify(broker =>
                broker.InsertUserAsync(randomUser, randomRoleName), 
                Times.Once());

            this.userManagement.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}
