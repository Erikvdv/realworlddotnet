using System.Security.Cryptography.X509Certificates;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Realworlddotnet.Core.Mappers;
using Realworlddotnet.Core.Services;
using Realworlddotnet.Core.Services.Interfaces;
using Realworlddotnet.Data.Contexts;
using Realworlddotnet.Data.Services;
using Realworlddotnet.Infrastructure.Extensions.Authentication;
using Realworlddotnet.Infrastructure.Extensions.Logging;
using Realworlddotnet.Infrastructure.Extensions.ProblemDetails;
using Realworlddotnet.Infrastructure.Utils;
using Realworlddotnet.Infrastructure.Utils.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// add logging
builder.Host.UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
{
    loggerConfiguration.ConfigureBaseLogging("realworldDotnet");
    loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
});

// Add services to the container.
var connectionString = "Filename=../realworld.db";

builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining(typeof(Program));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SupportNonNullableReferenceTypes();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "realworlddotnet", Version = "v1" });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IConduitRepository, ConduitRepository>();
builder.Services.AddScoped<IUserHandler, UserHandler>();
builder.Services.AddScoped<IArticlesHandler, ArticlesHandler>();
builder.Services.AddScoped<IProfilesHandler, ProfilesHandler>();
builder.Services.AddSingleton<CertificateProvider>();

builder.Services.AddSingleton<ITokenGenerator>(container =>
{
    var logger = container.GetRequiredService<ILogger<CertificateProvider>>();
    var certificateProvider = new CertificateProvider(logger);
    var cert = certificateProvider.LoadFromUserStore("4B5FE072C7AD8A9B5DCFDD1A20608BB54DE0954F");

    return new TokenGenerator(cert);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
    .Configure<ILogger<CertificateProvider>>((o, logger) =>
    {
        var certificateProvider = new CertificateProvider(logger);
        var cert = certificateProvider.LoadFromUserStore("4B5FE072C7AD8A9B5DCFDD1A20608BB54DE0954F");

        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new RsaSecurityKey(cert.GetRSAPublicKey())
        };
        o.Events = new JwtBearerEvents { OnMessageReceived = CustomOnMessageReceivedHandler.OnMessageReceived };
    });

builder.Services.AddDbContext<ConduitContext>(options => { options.UseSqlite(connectionString); });
builder.Services.AddProblemDetails();
builder.Services.ConfigureOptions<ProblemDetailsLogging>();

var app = builder.Build();

// Configure the HTTP request pipeline.
Log.Information("Start configuring http request pipeline");
app.UseSerilogRequestLogging();
app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "realworlddotnet v1"));

try
{
    Log.Information("Starting web host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
    Thread.Sleep(2000);
}
