using Microsoft.Extensions.Caching.Distributed;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.VideoCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialbookAPI.Infrastructure.Services
{
    public class VideoCacheService : IVideoCacheService
    {
        readonly IDistributedCache _distributedCache;

        public VideoCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<List<string>> GetVideoIdsAsync()
        {
            var json = await _distributedCache.GetStringAsync("videoIds");

            if (json == null)
            {
                // Handle the case where the data is not in Redis
                return new List<string>(); // Return an empty list or a default list if necessary
            }

            // Convert the JSON string back to a list of videoIds
            var videoIds = JsonSerializer.Deserialize<List<string>>(json);

            return videoIds;
        }

        public async Task<List<string>> CreateVoteVideoCacheAsync(List<string> videoIds)
        {
            // Kontrol et: Redis'de bir değer var mı?
            var jsonVoteVideos = _distributedCache.GetString("voteIds");

            VoteVideos voteVideos;

            if (jsonVoteVideos == null)
            {
                // Redis'de veri yoksa, videoId'lerini yarat ve Redis'e kaydet
                if (videoIds != null)
                {
                    voteVideos = new VoteVideos
                    {
                        VideoIds = videoIds,
                        LastUpdated = DateTime.Now
                    };

                    var json = JsonSerializer.Serialize(voteVideos);

                    // JSON stringini Redis'e kaydet
                    await _distributedCache.SetStringAsync("voteIds", json);
                }
            }

            jsonVoteVideos = _distributedCache.GetString("voteIds");

            // JSON stringini VoteVideos objesine geri dönüştür
            voteVideos = JsonSerializer.Deserialize<VoteVideos>(jsonVoteVideos);

            return voteVideos.VideoIds;
        }

        public async Task<List<string>> UpdateVoteVideoCacheAsync(List<string> videoIds)
        {
            var jsonVoteVideos = _distributedCache.GetString("voteIds");

            VoteVideos voteVideos;
            if (jsonVoteVideos != null)
            {
                voteVideos = JsonSerializer.Deserialize<VoteVideos>(jsonVoteVideos);
                // Check if 1 minutes has passed since the last update
                if ((DateTime.Now - voteVideos.LastUpdated).TotalMinutes < 1)
                {
                    return voteVideos.VideoIds;
                }
            }

            voteVideos = new VoteVideos
            {
                VideoIds = videoIds,
                LastUpdated = DateTime.Now
            };

            var json = JsonSerializer.Serialize(voteVideos);
            _distributedCache.SetString("voteIds", json);

            return voteVideos.VideoIds;
        }
    }
}
