using Microsoft.Extensions.DependencyInjection;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.Abstractions.Token;
using SocialbookAPI.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IVideoCacheService, VideoCacheService>();
            services.AddScoped<ITokenHandler, TokenHandler>();



        }
    }
}
