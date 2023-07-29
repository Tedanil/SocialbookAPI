using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR.Services
{
    public class VoteService : IVoteService
    {
        private Dictionary<string, int> videoVotes = new Dictionary<string, int>();

        public void Vote(string videoId)
        {
            if (!videoVotes.ContainsKey(videoId))
            {
                videoVotes[videoId] = 0;
            }
            videoVotes[videoId]++;
        }

        public KeyValuePair<string, int> GetMaxVotedVideo()
        {
            if (videoVotes.Count == 0) return default;
            return videoVotes.Aggregate((x, y) => x.Value > y.Value ? x : y);
        }

        public int GetVotes(string videoId)
        {
            return videoVotes.ContainsKey(videoId) ? videoVotes[videoId] : 0;
        }

        public void ResetVotes()
        {
            foreach (var key in videoVotes.Keys.ToList())
            {
                videoVotes[key] = 0;
            }
        }

        // ... diğer metot implementasyonları
    }
}
