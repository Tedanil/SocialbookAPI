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
using SocialbookAPI.Application.Features.Queries.VideoCache.GetCurrentVideoId;
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
        }

        [HttpGet("getCurrentVideoId")]
        public async Task<IActionResult> GetCurrentVideoId()
        {
            GetCurrentVideoIdQueryRequest request = new GetCurrentVideoIdQueryRequest();
            GetCurrentVideoIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);           
        }



    }
}
