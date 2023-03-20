using System.Linq.Expressions;
using AuthenticationService.Api.Brokers.Loggings;
using AuthenticationService.Api.Brokers.UserManagement;
using AuthenticationService.Api.Foundations.Users;
using AuthenticationService.Api.Models.Users;
using Fare;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace AuthenticationService.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        private readonly Mock<IUserManagement> userManagement;
        private readonly Mock<ILoggingBroker> loggingBroker;
        private readonly IUserService userService;

        public UserServiceTests()
        {
            this.userManagement = new Mock<IUserManagement>();
            this.loggingBroker = new Mock<ILoggingBroker>();

            this.userService = new UserService(
                userManagement: this.userManagement.Object,
                loggingBroker: this.loggingBroker.Object);
        }

        private static User CreateRandomUser() => 
            CreateUserFiller(GetRandomDateTimeOffset()).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string CreateRandomRole() =>
            new MnemonicString(1).GetValue();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException => actualException.SameExceptionAs(expectedException);
        }

        private static string GetRandomMessage() =>
            new MnemonicString(3, 6, 9).GetValue();

        private static Filler<User> CreateUserFiller(DateTimeOffset date)
        {
            var filler = new Filler<User>();

            string uzbPhoneNumberFormat = @"^\+998[9873][01345789][0-9]{7}$";
            var xeger = new Xeger(uzbPhoneNumberFormat);
            var generatedPhoneNumber = xeger.Generate();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date)
                .OnProperty(user => user.FirstName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(user => user.LastName).Use(new RealNames(NameStyle.LastName))
                .OnProperty(user => user.PhoneNumber).Use(generatedPhoneNumber)
                .OnProperty(user => user.Email).Use(new EmailAddresses(".com"))
                .IgnoreInheritance()
                .IgnoreAllUnknownTypes();

            return filler;
        }
    }
}
