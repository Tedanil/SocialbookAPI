using Microsoft.AspNetCore.Builder;
using SocialbookAPI.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<MessageHub>("/messages-hub");
            webApplication.MapHub<VoteHub>("/votes-hub");
            webApplication.MapHub<PlayerHub>("/players-hub");
            webApplication.MapHub<VideoIdHub>("/videoIds-hub");



        }
    }
}
