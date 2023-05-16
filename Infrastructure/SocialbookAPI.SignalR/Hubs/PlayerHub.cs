using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR.Hubs
{
    public class PlayerHub : Hub
    {
        // ...

        public async Task PlayVideo(string videoId, bool isPlaying)
        {
            // Check if the videoId is valid, if not return an error
            // ...

            // If the videoId is valid, update the video state
            var videoState = new
            {
                VideoId = videoId,
                IsPlaying = isPlaying,
                StartTime = DateTime.UtcNow // This can be used by the clients to sync the video playback
            };
            // Broadcast the video state to all clients
            await Clients.All.SendAsync(ReceiveFunctionNames.VideoStateUpdated, videoState);
        }
    }


}
