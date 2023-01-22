using DAL.DataAccess;
using DAL.DataAccesss;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using TelexistenceProject.DALServices;
using TelexistenceProject.Interceptions;
using TelexistenceProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.


builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ExceptionInterceptor>();
}); 


builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();
builder.Services.AddScoped<IUserDataService, UserDataService>();
var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<UserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
