﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Api.Mappers;
using Realworlddotnet.Api.Models;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Realworlddotnet.Core.Services;
using Realworlddotnet.Data.Contexts;
using Realworlddotnet.Data.Services;
using Realworlddotnet.Infrastructure.Extensions.Authentication;
using Realworlddotnet.Infrastructure.Extensions.Logging;
using Realworlddotnet.Infrastructure.Extensions.ProblemDetails;
using Realworlddotnet.Infrastructure.Utils;
using Realworlddotnet.Infrastructure.Utils.Interfaces;
using Serilog;
using System.Threading;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Realworlddotnet.Api;

//var builder = WebApplication.CreateBuilder(args);

// add logging
//builder.Host.UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
//{
//    loggerConfiguration.ConfigureBaseLogging("realworldDotnet");
//    loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
//});

// setup database connection (used for in memory SQLite).
// SQLite in memory requires an open connection during the application lifetime
#pragma warning disable S125
// to use a file based SQLite use: "Filename=../realworld.db";
//#pragma warning restore S125
//const string connectionString = "Filename=:memory:";
//var connection = new SqliteConnection(connectionString);
//connection.Open();

//// Add services to the container.
//builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SupportNonNullableReferenceTypes();
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "realworlddotnet", Version = "v1" });
//});

//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressInferBindingSourcesForParameters = true;
//});

//builder.Services.AddScoped<IConduitRepository, ConduitRepository>();
//builder.Services.AddScoped<IUserHandler, UserHandler>();
//builder.Services.AddScoped<IArticlesHandler, ArticlesHandler>();
//builder.Services.AddScoped<IProfilesHandler, ProfilesHandler>();
//builder.Services.AddSingleton<CertificateProvider>();

//builder.Services.AddSingleton<ITokenGenerator>(container =>
//{
//    var logger = container.GetRequiredService<ILogger<CertificateProvider>>();
//    var certificateProvider = new CertificateProvider(logger);
//    var cert = certificateProvider.LoadFromFile("identityserver_testing.pfx", "password");

//    return new TokenGenerator(cert);
//});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
//builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
//    .Configure<ILogger<CertificateProvider>>((o, logger) =>
//    {
//        var certificateProvider = new CertificateProvider(logger);
//        var cert = certificateProvider.LoadFromFile("identityserver_testing.pfx", "password");

//        o.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false,
//            ValidateIssuer = false,
//            IssuerSigningKey = new RsaSecurityKey(cert.GetRSAPublicKey())
//        };
//        o.Events = new JwtBearerEvents { OnMessageReceived = CustomOnMessageReceivedHandler.OnMessageReceived };
//});

//// for SQLite in memory a connection is provided rather than a connection string
////builder.Services.AddDbContext<ConduitContext>(options => { options.UseSqlite(connection); });
//builder.Services.AddProblemDetails();
//builder.Services.ConfigureOptions<ProblemDetailsLogging>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//Log.Information("Start configuring http request pipeline");

//// when using in memory SQLite ensure the tables are created
//using (var scope = app.Services.CreateScope())
//{
//    using var context = scope.ServiceProvider.GetService<ConduitContext>();
//    context?.Database.EnsureCreated();
//}

//app.UseSerilogRequestLogging();
//app.UseProblemDetails();
//app.UseAuthentication();
//app.UseRouting();
//app.UseAuthorization();
//app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
//app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "realworlddotnet v1"));

//.net6:
//try
//{
//    Log.Information("Starting web host");
//    app.Run();
//    return 0;
//}
//catch (Exception ex)
//{
//    Log.Fatal(ex, "Host terminated unexpectedly");
//    return 1;
//}
//finally
//{
//    connection.Close();
//    Log.CloseAndFlush();
//    Thread.Sleep(2000);
//}

//.net5:
var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json")
    .Build();

var host = new WebHostBuilder()
    .UseConfiguration(config)
    .UseKestrel()
    .UseUrls($"http://+:5000")
    .UseStartup<Startup>()
    .Build();

await host.RunAsync();
