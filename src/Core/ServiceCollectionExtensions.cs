using Core.Abstraction.Services;
using Core.Abstraction.UseCases;
using Core.Abstraction.UseCases.Auth;
using Core.Abstraction.UseCases.Users;
using Core.UseCases;
using Core.UseCases.Auth;
using Core.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IPersonUseCase, PersonUseCase>()
                    .AddTransient<ICreateUserUseCase, CreateUserUseCase>()
                    .AddTransient<IGetUserUseCase, GetUserUseCase>()
                    .AddTransient<ILoginUseCase, LoginUseCase>();
                        
            return services;
        }
    }
}