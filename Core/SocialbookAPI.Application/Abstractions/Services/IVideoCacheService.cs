using SocialbookAPI.Application.DTOs.VideoCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Abstractions.Services
{
    public interface IVideoCacheService
    {
        Task<List<string>> GetVideoIdsAsync();
        Task<List<string>> CreateVoteVideoCacheAsync(List<string> videoIds);
        Task<List<string>> UpdateVoteVideoCacheAsync(List<string> videoIds);
        Task<string> UpdateCurrentVideoId(VideoIdAndTime videoIdAndTime);
        Task<VideoIdAndTime> GetCurrentVideoId();
        Task UpdateVoteIdsInCacheAsync();


    }
}
