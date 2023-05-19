using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.VideoCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.VideoCache.UpdateCurrentVideoId
{
    public class UpdateCurrentVideoIdCommandHandler : IRequestHandler<UpdateCurrentVideoIdCommandRequest, UpdateCurrentVideoIdCommandResponse>
    {
        readonly IVideoCacheService _videoCacheService;

        public UpdateCurrentVideoIdCommandHandler(IVideoCacheService videoCacheService)
        {
            _videoCacheService = videoCacheService;
        }

        public async Task<UpdateCurrentVideoIdCommandResponse> Handle(UpdateCurrentVideoIdCommandRequest request, CancellationToken cancellationToken)
        {
            VideoIdAndTime videoIdAndTime = new VideoIdAndTime
            {
                VideoId = request.VideoId,
                VideoTime = request.VideoTime
            };

            string videoId = await _videoCacheService.UpdateCurrentVideoId(videoIdAndTime);

            
            UpdateCurrentVideoIdCommandResponse response = new UpdateCurrentVideoIdCommandResponse
            {
               
                VideoId = videoId
            };
            return response;

        }
    }
}
