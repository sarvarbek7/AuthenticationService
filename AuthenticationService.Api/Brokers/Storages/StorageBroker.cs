using AuthenticationService.Api.Models.Users;
using EFxceptions.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsIdentityContext<User, Profession, Guid>, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            string connectionString = this.configuration.GetConnectionString("localdb");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
