using Microsoft.AspNetCore.SignalR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR.Hubs
{
    public class VoteHub : Hub
    {
        private readonly IVoteService _voteService;

        public VoteHub(IVoteService voteService)
        {
            _voteService = voteService;
        }

        public async Task UpdateVoteList(string videoId, VideoData videoData)
        {
            _voteService.Vote(videoId);

            var votes = _voteService.GetVotes(videoId);

            var voteUpdate = new
            {
                VideoId = videoId,
                VideoData = videoData,
                Votes = votes
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