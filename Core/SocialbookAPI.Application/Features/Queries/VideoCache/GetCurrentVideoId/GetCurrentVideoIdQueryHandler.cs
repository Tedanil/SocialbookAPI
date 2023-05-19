using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.VideoCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Queries.VideoCache.GetCurrentVideoId
{
    public class GetCurrentVideoIdQueryHandler : IRequestHandler<GetCurrentVideoIdQueryRequest, GetCurrentVideoIdQueryResponse>
    {
        readonly IVideoCacheService _videoCacheService;

        public GetCurrentVideoIdQueryHandler(IVideoCacheService videoCacheService)
        {
            _videoCacheService = videoCacheService;
        }

        public async Task<GetCurrentVideoIdQueryResponse> Handle(GetCurrentVideoIdQueryRequest request, CancellationToken cancellationToken)
        {
            VideoIdAndTime videoIdAndTime = await _videoCacheService.GetCurrentVideoId();

            GetCurrentVideoIdQueryResponse response = new GetCurrentVideoIdQueryResponse
            {
                VideoId = videoIdAndTime.VideoId,
                VideoTime = videoIdAndTime.VideoTime
            };

            return response;
        }
    }
}
