using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using InterviewTask.API.Persistence;
using InterviewTask.API.Shared;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace InterviewTask.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesConfigration(this IServiceCollection services, IConfiguration configuration)
        {

            // Swagger Documintation
            services.AddSwaggerDocumentationServices();

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            // services.AddScoped<CanceletionState>();
            services.AddControllersWithViews(opt => opt.Filters.Add<CanceletionTokenCaptureFilter>());

            // FluentValidation
            services.AddFluenentValidtionConfigration();

            // Mapster
            services.AddMapsterConfigration();

            // Mediator
            services.AddMediatRConfiguration();

            // HangFire
            services.AddHangfireJobsConfigration(configuration);

            // jwt
            services.AddJwtServiceConfigration(configuration);
            return services;
        }

        private static IServiceCollection AddSwaggerDocumentationServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(C =>
            {
                var SecuritySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorizations",
                    Description = " Jwt Auth Bearer Schema",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme,

                    }
                };

                C.AddSecurityDefinition("Bearer", SecuritySchema);
                var ScurityRequirments = new OpenApiSecurityRequirement
                {
                    {
                        SecuritySchema , new [] {"Bearer"}
                    }
                };

                C.AddSecurityRequirement(ScurityRequirments);
            });
            return services;



        }

        private static IServiceCollection AddFluenentValidtionConfigration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        private static IServiceCollection AddMapsterConfigration(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(config));
            return services;
        }

        private static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }

        private static IServiceCollection AddHangfireJobsConfigration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                                            .UseSimpleAssemblyNameTypeSerializer()
                                                      .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHangfireServer();
            return services;
        }


        private static IServiceCollection AddJwtServiceConfigration(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Token:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Token:ValidAudiance"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            return services;
        }
    }

}