using AuthenticationService.Api.Brokers.Storages;
using AuthenticationService.Api.Brokers.UserManagement;
using AuthenticationService.Api.Foundations.Users;
using AuthenticationService.Api.Models.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext and Identity
builder.Services.AddDbContext<StorageBroker>();

builder.Services.AddIdentityCore<User>()
    .AddRoles<Profession>()
    .AddEntityFrameworkStores<StorageBroker>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<IUserManagement, UserManagement>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();