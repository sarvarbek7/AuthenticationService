using AuthenticationService.Api.Foundations.Users;
using AuthenticationService.Api.Models.Users;
using AuthenticationService.Api.Models.Users.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AuthenticationService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : RESTFulController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserService userService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("postuser")]
        public async ValueTask<ActionResult<User>> PostUser(string phone, string role)
        {
            try
            {
                var user = new User 
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = phone,
                    UserName = Guid.NewGuid().ToString(),
                    CreatedDate = DateTimeOffset.UtcNow
                };

                await this.userService.RegisterUserAsync(user, role);

                return Created(user);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
                when(userDependencyValidationException.InnerException is AlreadyExistsUserException)
            {
                return Conflict(userDependencyValidationException.InnerException);
            }
        }
    }
}