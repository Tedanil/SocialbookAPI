using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Queries.VideoCache.GetVideoIds
{
    public class GetVideoIdsQueryHandler : IRequestHandler<GetVideoIdsQueryRequest, GetVideoIdsQueryResponse>
    {
        readonly IVideoCacheService _videoCacheService;

        public GetVideoIdsQueryHandler(IVideoCacheService videoCacheService)
        {
            _videoCacheService = videoCacheService;
        }

        public async Task<GetVideoIdsQueryResponse> Handle(GetVideoIdsQueryRequest request, CancellationToken cancellationToken)
        {
            var videoIds = await _videoCacheService.GetVideoIdsAsync();

            return new()
            {
                VideoIds = videoIds 
            };
        }
    }
}
