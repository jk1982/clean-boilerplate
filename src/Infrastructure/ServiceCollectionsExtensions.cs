using Core.Abstraction.Repositories;
using Core.Abstraction.Services;
using Infrastructure.Data.Abstraction;
using Infrastructure.Data.Postgres;
using Infrastructure.Data.Postgres.Repositories;
using Infrastructure.Services.Email;
using Infrastructure.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPersonRepository, PersonRepository>()
                    .AddTransient<IUserRepository, UserRepository>()
                    .AddPostgres(configuration)
                    .AddJwt(configuration)
                    .AddEmail(configuration);

            return services;
        }

        private static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbSession>((provider) => { return new DbSession(configuration.GetConnectionString("Postgres")); });

            return services;
        }

        private static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection(TokenSettings.SectionName))                    
                    .AddTransient<IJwtFactory, JwtFactory>();

            return services;
        }

        private static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.SectionName))
                    .AddTransient<IEmailSender, EmailSender>()
                    .AddTransient<IUserEmailManager, UserEmailManager>();

            return services;
        }
    }
}
