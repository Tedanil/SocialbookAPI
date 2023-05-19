using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using SocialbookAPI.Application.Abstractions.Hubs;
using SocialbookAPI.Application.Features.Commands.Song.CreateSong;
using SocialbookAPI.Application.Features.Commands.VideoCache.CreateVoteVideos;
using SocialbookAPI.Application.Features.Commands.VideoCache.UpdateCurrentVideoId;
using SocialbookAPI.Application.Features.Commands.VideoCache.UpdateVoteVideos;
using SocialbookAPI.Application.Features.Queries.VideoCache.GetVideoIds;
using SocialbookAPI.Application.Repositories;
using SocialbookAPI.Application.ViewModels.Songs;
using System.Globalization;
using System.Text.Json;

namespace SocialbookAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        readonly private ISongWriteRepository _songWriteRepository;
        readonly private ISongReadRepository _songReadRepository;
        readonly private IMessageHubService _messageHubService;
        readonly IDistributedCache _distributedCache;
        readonly IMediator _mediator;

        public SongsController(ISongWriteRepository songWriteRepository, ISongReadRepository songReadRepository, IMessageHubService messageHubService, IDistributedCache distributedCache, IMediator mediator)
        {
            _songWriteRepository = songWriteRepository;
            _songReadRepository = songReadRepository;
            _messageHubService = messageHubService;
            _distributedCache = distributedCache;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSongCommandRequest createSongCommandRequest)
        {          
            CreateSongCommandResponse response = await _mediator.Send(createSongCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetVideoIds()
        {
            GetVideoIdsQueryRequest getVideoIdsQueryRequest = new GetVideoIdsQueryRequest();
            // Parametreleri URL üzerinden alın ve getVideoIdsQueryRequest nesnesini doldurun

            GetVideoIdsQueryResponse response = await _mediator.Send(getVideoIdsQueryRequest);
            return Ok(response); 
        }

        [HttpPost("videoIds")]
        public async Task<IActionResult> PostVideoIds([FromBody] List<string> videoIds)
        {
            CreateVoteVideosCommandRequest request = new CreateVoteVideosCommandRequest { VideoIds = videoIds };
            CreateVoteVideosCommandResponse response = await _mediator.Send(request);
            return Ok(response.VideoIds);        
        }


        [HttpPost("updateVideoIds")]
        public async Task<IActionResult> UpdateVideoIds([FromBody] List<string> videoIds)
        {
            UpdateVoteVideosCommandRequest request = new UpdateVoteVideosCommandRequest { VideoIds= videoIds };
            UpdateVoteVideosCommandResponse response = await _mediator.Send(request);
            return Ok(response.VideoIds);         
        }

        [HttpPost("updateCurrentVideoId")]
        public async Task<IActionResult> UpdateCurrentVideoId([FromBody] UpdateCurrentVideoIdCommandRequest updateCurrentVideoIdCommandRequest)
        {



            UpdateCurrentVideoIdCommandResponse response = await _mediator.Send(updateCurrentVideoIdCommandRequest);
            return Ok(response.VideoId);
            //var jsonCurrentVideo = _distributedCache.GetString("currentVideoId");

            //CurrentVideo currentVideo;
            //if (jsonCurrentVideo != null)
            //{
            //    currentVideo = JsonSerializer.Deserialize<CurrentVideo>(jsonCurrentVideo);
            //    // Check if 1 minutes has passed since the last update
            //    if ((DateTime.Now - currentVideo.LastUpdated).TotalMinutes < 1)
            //    {
            //        return Ok(currentVideo.VideoId);
            //    }
            //    else
            //    {
            //        // Remove the existing value if it's older than 1 minute
            //        _distributedCache.Remove("currentVideoId");
            //    }
            //}

            //currentVideo = new CurrentVideo
            //{
            //    VideoId = videoIdAndTime.VideoId,
            //    LastUpdated = DateTime.Now
            //};

            //var json = JsonSerializer.Serialize(currentVideo);

            //double videoTimeInSeconds;
            //if (double.TryParse(videoIdAndTime.VideoTime, out videoTimeInSeconds))
            //{
            //    TimeSpan cacheDuration = TimeSpan.FromSeconds(videoTimeInSeconds);
            //    _distributedCache.SetString("currentVideoId", json, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = cacheDuration });
            //}
            //else
            //{
            //    // Handle error if videoTime cannot be parsed to a double
            //}

            

            //return Ok(currentVideo.VideoId);
        }
        public class CurrentVideo
        {
            public string VideoId { get; set; }
            public DateTime LastUpdated { get; set; }
        }

        public class VideoIdAndTime
        {
            public string VideoId { get; set; }
            public string VideoTime { get; set; }
        }

        [HttpGet("getCurrentVideoId")]
        public async Task<IActionResult> GetCurrentVideoId()
        {
            var jsonCurrentVideo = _distributedCache.GetString("currentVideoId");
            VideoIdAndTime videoIdAndTime = new VideoIdAndTime();

            if (jsonCurrentVideo != null)
            {
                var currentVideo = JsonSerializer.Deserialize<CurrentVideo>(jsonCurrentVideo);
                videoIdAndTime.VideoId = currentVideo.VideoId;

                // Calculate the video time (current time - last updated time) in seconds
                var timeSpan = DateTime.Now - currentVideo.LastUpdated;
                videoIdAndTime.VideoTime = timeSpan.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                videoIdAndTime.VideoId = "5OeoVyUOorY"; // Default videoId
                videoIdAndTime.VideoTime = "130"; // Default videoTime
            }

            return Ok(videoIdAndTime);
        }



    }
}
