using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialbookAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialbookAPI.Infrastructure.Services
{
    public class RedisStartupService : IHostedService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RedisStartupService(IDistributedCache distributedCache, IServiceScopeFactory serviceScopeFactory)
        {
            _distributedCache = distributedCache;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var songReadRepository = scope.ServiceProvider.GetRequiredService<ISongReadRepository>();

            var songs = await songReadRepository.GetAll()
                                                 .Select(s => s.VideoId)
                                                 .Take(17)
                                                 .ToListAsync();

            var json = JsonSerializer.Serialize(songs);
            await _distributedCache.SetStringAsync("videoIds", json, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7) }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
