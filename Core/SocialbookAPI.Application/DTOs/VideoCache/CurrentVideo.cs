using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.DTOs.VideoCache
{
    public class CurrentVideo
    {
        public string VideoId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
