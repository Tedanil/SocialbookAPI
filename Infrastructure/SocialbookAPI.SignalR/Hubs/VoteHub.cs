using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR.Hubs
{
    public class VoteHub : Hub
    {
        public async Task UpdateVoteList(string videoId, VideoData videoData)
        {
            var voteUpdate = new
            {
                VideoId = videoId,
                VideoData = videoData
            };

            await Clients.All.SendAsync(ReceiveFunctionNames.VoteListUpdated, voteUpdate);
        }

        public class VideoData
        {
            public string Title { get; set; }
            public string Thumbnail { get; set; }
            public int Votes { get; set; } // Added Votes property to represent vote count of the video
                                           // other properties...
        }
    }


}
