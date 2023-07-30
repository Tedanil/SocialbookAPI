using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.VideoCache;
using SocialbookAPI.Application.Repositories;
using SocialbookAPI.SignalR;
using SocialbookAPI.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialbookAPI.Persistence.Services
{
    public class RedisStartupService : IHostedService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IVoteService voteService;
        private readonly IYouTubeService _youTubeService;
        private readonly IVideoCacheService _videoCacheService;
        private readonly IHubContext<VideoIdHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;




        public RedisStartupService(IDistributedCache distributedCache, IServiceScopeFactory serviceScopeFactory,
            IVoteService voteService, IYouTubeService youTubeService, IVideoCacheService videoCacheService,
            IHubContext<VideoIdHub> hubContext, IServiceScopeFactory scopeFactory)
        {
            _distributedCache = distributedCache;
            _serviceScopeFactory = serviceScopeFactory;
            this.voteService = voteService;
            _youTubeService = youTubeService;
            _videoCacheService = videoCacheService;
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var songReadRepository = scope.ServiceProvider.GetRequiredService<ISongReadRepository>();

            var songs = await songReadRepository.GetAll()
                                                 .Select(s => s.VideoId)
                                                 .Take(17)
                                                 .ToListAsync();

            Random random = new Random();
            List<string> shuffledSongs = songs.OrderBy(x => random.Next()).ToList();
            List<string> selectedSongs = shuffledSongs.Take(3).ToList();

            //videoId'leri redise kaydetme
            var json = JsonSerializer.Serialize(songs);
            await _distributedCache.SetStringAsync("videoIds", json,
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7) }, cancellationToken);

        
            CurrentVideo currentVideo;
            currentVideo = new CurrentVideo
            {
                VideoId = songs[0],
                LastUpdated = DateTime.Now
            };

            var json2 = JsonSerializer.Serialize(currentVideo);

            
           
            //currentvideoId kaydetme
             TimeSpan cacheDuration = TimeSpan.FromSeconds(100);
            _distributedCache.SetString("currentVideoId", json2,
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = cacheDuration });


            //votevideoId kaydetme
            VoteVideos voteVideos;
            voteVideos = new VoteVideos
            {
                VideoIds = selectedSongs,
                LastUpdated = DateTime.Now
            };
            var json3 = JsonSerializer.Serialize(voteVideos);          
            await _distributedCache.SetStringAsync("voteIds", json3);

            



            Task delayTask = Task.Delay(50000, cancellationToken);

            delayTask.ContinueWith(async t =>
            {
                // Bu kod, bekleme işlemi tamamlandıktan sonra çalışır.
                var maxVotedVideo = voteService.GetMaxVotedVideo();
                Console.WriteLine($"en çok oy alan video {maxVotedVideo}");

                VideoIdAndTime videoIdAndTime = await _youTubeService.GetMaxVotedVideoWithContentDetails();
                _videoCacheService.UpdateCurrentVideoId(videoIdAndTime);
                var cts = new CancellationTokenSource();
                await StartPolling(cts.Token);


            }, TaskContinuationOptions.OnlyOnRanToCompletion);

        }
        public async Task StartPolling(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var videoIdAndTime = await _youTubeService.GetMaxVotedVideoWithContentDetails();

                // UpdateCurrentVideoId'ı çağırarak video bilgilerini Redis'e kaydediyoruz.
                await _videoCacheService.UpdateCurrentVideoId(videoIdAndTime);
                voteService.ResetVotes();
                await _videoCacheService.UpdateVoteIdsInCacheAsync();
                using (var scope = _scopeFactory.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                    //await userService.UpdateUserVoteCountsBasedOnLevels();
                }

                await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.VideoIdSent, videoIdAndTime.VideoId);


                // videoIdAndTime.VideoTime'a göre bekleme süresini belirliyoruz.
                if (double.TryParse(videoIdAndTime.VideoTime, out double videoTimeInSeconds))
                {
                    await Task.Delay(TimeSpan.FromSeconds(videoTimeInSeconds), cancellationToken);
                }
                else
                {
                    // videoIdAndTime.VideoTime parse edilemezse bir hata işlemi yapabiliriz.
                }
            }
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        

    }
}
