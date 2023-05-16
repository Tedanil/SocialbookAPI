using Microsoft.Extensions.Caching.Distributed;
using SocialbookAPI.Application.Abstractions.Services;
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
    }
}
