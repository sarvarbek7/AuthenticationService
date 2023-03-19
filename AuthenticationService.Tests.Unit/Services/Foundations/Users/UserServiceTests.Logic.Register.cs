using AuthenticationService.Api.Models.Users;
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
            User randomUser = CreateRandomUser();
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
