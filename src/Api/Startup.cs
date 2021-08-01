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

namespace realworlddotnet.Api
{
    public class Startup
    {
        public const string DEFAULT_DATABASE_CONNECTIONSTRING = "Filename=../realworld.db";
        public const string DEFAULT_DATABASE_PROVIDER = "sqlite";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            var sp = services.BuildServiceProvider();
            var certificateProvider = sp.GetService<CertificateProvider>();
            var cert = certificateProvider.LoadFromUserStore("4B5FE072C7AD8A9B5DCFDD1A20608BB54DE0954F");


            services.AddSingleton<ITokenGenerator>(new TokenGenerator(cert));

            // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new RsaSecurityKey(cert.GetRSAPublicKey())
                };
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = CustomOnMessageReceivedHandler.OnMessageReceived
                };
            });


            var connectionString = DEFAULT_DATABASE_CONNECTIONSTRING;

            services.AddDbContext<ConduitContext>(options => { options.UseSqlite(connectionString); });

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