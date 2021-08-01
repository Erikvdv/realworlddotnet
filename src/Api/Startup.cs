using System.Security.Cryptography.X509Certificates;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using realworlddotnet.Domain.Mappers;
using realworlddotnet.Domain.Services;
using realworlddotnet.Domain.Services.Interfaces;
using realworlddotnet.Infrastructure.Contexts;
using realworlddotnet.Infrastructure.Extensions.Authentication;
using realworlddotnet.Infrastructure.Extensions.ProblemDetails;
using realworlddotnet.Infrastructure.Services;
using realworlddotnet.Infrastructure.Utils;
using Serilog;
using ILogger = Serilog.ILogger;

namespace realworlddotnet.Api
{
    public class Startup
    {
        private const string DEFAULT_DATABASE_CONNECTIONSTRING = "Filename=../realworld.db";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining(typeof(Startup));
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
            });
            services.AddAutoMapper(typeof(Startup), typeof(MappingProfile));

            services.AddScoped<IConduitRepository, ConduitRepository>();
            services.AddScoped<IUserInteractor, UserInteractor>();
            services.AddSingleton<CertificateProvider>();
            
            services.AddSingleton<ITokenGenerator>(container =>
            {
                var logger = container.GetRequiredService<ILogger<CertificateProvider>>();
                var certificateProvider = new CertificateProvider(logger);
                var cert = certificateProvider.LoadFromUserStore("4B5FE072C7AD8A9B5DCFDD1A20608BB54DE0954F");

                return new TokenGenerator(cert);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<ILogger<CertificateProvider>>((o, logger) => {
                    var certificateProvider = new CertificateProvider(logger);
                    var cert = certificateProvider.LoadFromUserStore("4B5FE072C7AD8A9B5DCFDD1A20608BB54DE0954F");

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new RsaSecurityKey(cert!.GetRSAPublicKey())
                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = CustomOnMessageReceivedHandler.OnMessageReceived
                    };
                });

            services.AddDbContext<ConduitContext>(options => { options.UseSqlite(DEFAULT_DATABASE_CONNECTIONSTRING); });

            services.AddSwaggerGen(c =>
            {
                c.SupportNonNullableReferenceTypes();
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "realworlddotnet", Version = "v1"});
            });
            services.AddProblemDetails();
            services.ConfigureOptions<ProblemDetailsLogging>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();
            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "realworlddotnet v1"));
        }
    }
}