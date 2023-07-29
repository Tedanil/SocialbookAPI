using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR.Hubs
{
    public class VideoIdHub : Hub
    {
        public async Task VideoIdSend(string videoId)
        {
            await Clients.All.SendAsync(ReceiveFunctionNames.VideoIdSent, videoId);

        }
    }
}
