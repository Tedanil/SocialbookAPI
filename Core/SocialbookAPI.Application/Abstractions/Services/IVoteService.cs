using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Abstractions.Services
{
    public interface IVoteService
    {
        void Vote(string videoId);
        KeyValuePair<string, int> GetMaxVotedVideo();
        int GetVotes(string videoId);
        // ... diğer metotlar
        void ResetVotes();
    }
}
