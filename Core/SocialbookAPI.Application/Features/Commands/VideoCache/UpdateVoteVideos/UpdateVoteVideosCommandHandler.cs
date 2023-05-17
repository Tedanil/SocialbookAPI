using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.VideoCache.UpdateVoteVideos
{
    public class UpdateVoteVideosCommandHandler : IRequestHandler<UpdateVoteVideosCommandRequest, UpdateVoteVideosCommandResponse>
    {
        readonly IVideoCacheService _videoCacheService;

        public UpdateVoteVideosCommandHandler(IVideoCacheService videoCacheService)
        {
            _videoCacheService = videoCacheService;
        }

        public async Task<UpdateVoteVideosCommandResponse> Handle(UpdateVoteVideosCommandRequest request, CancellationToken cancellationToken)
        {
            var videoIds = await _videoCacheService.UpdateVideoCacheAsync(request.VideoIds);
            return new()
            {
                VideoIds = videoIds
            };
        }
    }
}
