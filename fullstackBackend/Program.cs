using Autofac.Extensions.DependencyInjection;
using Autofac;
using fullstackBackend.WebApi.App.DependencyInjection;
using fullstackBackend.WebApi.App.Util.GlobalConfig;
using fullstackBackend.WebApi.App.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using AutoMapper;
using fullstackBackend.Application.Common.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using fullstackBackend.WebApi.App.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

//cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
   options.AddPolicy(MyAllowSpecificOrigins,
       policy =>
       {
          policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
       });
});

builder.Services.AddAuthentication(options =>
{
   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

// Register Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    builder.RegisterModule(new MediatorModule()));
// Add services to the container.

builder.Services.AddRepositories();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Custom dependencies
builder.Services.AddGlobalSettings();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddHealthChecks();

// AutoMapper
var config = new MapperConfiguration(cfg =>
{
   cfg.AddProfile(new EmployeeMaps());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton<TokenService>();

builder.Services.ConfigureSwagger();

var app = builder.Build();

app.UseSwaggerApi();

app.Logger.LogInformation("Use Health Checks");
app.UseHealthChecks("/Health", new HealthCheckOptions()
{
   ResultStatusCodes =
            {
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status200OK,
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status201Created,
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status404NotFound,
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
});

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
