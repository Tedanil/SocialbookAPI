using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.VideoCache.CreateVoteVideos
{
    public class CreateVoteVideosCommandHandler : IRequestHandler<CreateVoteVideosCommandRequest, CreateVoteVideosCommandResponse>
    {
        readonly IVideoCacheService _videoCacheService;

        public CreateVoteVideosCommandHandler(IVideoCacheService videoCacheService)
        {
            _videoCacheService = videoCacheService;
        }

        public async Task<CreateVoteVideosCommandResponse> Handle(CreateVoteVideosCommandRequest request, CancellationToken cancellationToken)
        {
            var videoIds = await _videoCacheService.CreateVoteVideoCacheAsync(request.VideoIds);
            return new()
            {
                VideoIds = videoIds
            };
        }
    }
}
