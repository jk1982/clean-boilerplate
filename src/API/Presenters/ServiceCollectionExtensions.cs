using API.Presenters.Auth;
using API.Presenters.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Presenters
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<PersonPresenter>()
                    .AddScoped<GetUserPresenter>()
                    .AddScoped<CreateUserPresenter>()
                    .AddScoped<LoginPresenter>();

            return services;
        }
    }
}
