using Microsoft.Extensions.DependencyInjection;
using SocialbookAPI.Application.Abstractions.Hubs;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.SignalR.HubServices;
using SocialbookAPI.SignalR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IMessageHubService, MessageHubService>();
            collection.AddSignalR();
            collection.AddSingleton<IVoteService, VoteService>();

        }
    }
}
