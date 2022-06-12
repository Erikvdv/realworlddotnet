using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Realworlddotnet.Core.Services;
using Realworlddotnet.Core.Services.Interfaces;
using Realworlddotnet.Data.Contexts;
using Realworlddotnet.Data.Services;
using Realworlddotnet.Infrastructure.Extensions.Authentication;
using Realworlddotnet.Infrastructure.Extensions.ProblemDetails;
using Realworlddotnet.Infrastructure.Utils;
using Realworlddotnet.Infrastructure.Utils.Interfaces;

namespace Realworlddotnet.Api
{
    public class Startup
    {

        private const string CORS_POLICY = "GlobalCorsPolicy";
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //logging
            //TODO
            ////db
            //const string connectionString = "Filename=:memory:";
            //var connection = new SqliteConnection(connectionString);
            //connection.Open();
            services.AddDbContext<ConduitContext>(options => options.UseSqlServer(_config.GetConnectionString("PrimaryConnection")));
            services.AddProblemDetails();
            services.ConfigureOptions<ProblemDetailsLogging>();
            //routing/params
            services.AddCors(options => options.AddPolicy(CORS_POLICY, policy =>
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()));
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
            });
            //services.AddEndpointsApiExplorer();//??
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SupportNonNullableReferenceTypes();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "realworlddotnet", Version = "v1" });
            });
            //handlers
            services.AddScoped<IConduitRepository, ConduitRepository>();
            services.AddScoped<IUserHandler, UserHandler>();
            services.AddScoped<IArticlesHandler, ArticlesHandler>();
            services.AddScoped<IProfilesHandler, ProfilesHandler>();
            services.AddSingleton<CertificateProvider>();
            //auth
            services.AddSingleton<ITokenGenerator>(container =>
            {
                var logger = container.GetRequiredService<ILogger<CertificateProvider>>();
                var certificateProvider = new CertificateProvider(logger);
                var cert = certificateProvider.LoadFromFile("identityserver_testing.pfx", "password");
                return new TokenGenerator(cert);
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<ILogger<CertificateProvider>>((o, logger) =>
                {
                    var certificateProvider = new CertificateProvider(logger);
                    var cert = certificateProvider.LoadFromFile("identityserver_testing.pfx", "password");

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new RsaSecurityKey(cert.GetRSAPublicKey())
                    };
                    o.Events = new JwtBearerEvents { OnMessageReceived = CustomOnMessageReceivedHandler.OnMessageReceived };
                });
        }

        public void Configure(IApplicationBuilder builder)
        {
            //var app = builder.Build();
            builder.ApplicationServices.GetRequiredService<ConduitContext>().Database.EnsureCreated();
            //builder.UseSerilogRequestLogging();
            builder.UseProblemDetails();
            builder.UseAuthentication();
            builder.UseRouting();
            builder.UseCors(CORS_POLICY);
            builder.UseAuthorization();
            builder.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            builder.UseSwagger();
            builder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "realworlddotnet v1"));
        }
    }
}
