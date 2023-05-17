using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.DTOs.VideoCache
{
    public class VoteVideos
    {
        public List<string> VideoIds { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
