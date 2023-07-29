using Microsoft.Extensions.DependencyInjection;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.Abstractions.Services.Configurations;
using SocialbookAPI.Application.Abstractions.Token;
using SocialbookAPI.Infrastructure.Services;
using SocialbookAPI.Infrastructure.Services.Configurations;
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
            services.AddSingleton<IVideoCacheService, VideoCacheService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddSingleton<IYouTubeService, YouTubeService>();


        }
    }
}
