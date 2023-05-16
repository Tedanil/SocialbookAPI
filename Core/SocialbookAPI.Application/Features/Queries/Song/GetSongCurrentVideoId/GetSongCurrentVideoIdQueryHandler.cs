using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Queries.Song.GetSongCurrentVideoId
{
    public class GetSongCurrentVideoIdQueryHandler : IRequestHandler<GetSongCurrentVideoIdQueryRequest, GetSongCurrentVideoIdQueryResponse>
    {
        public Task<GetSongCurrentVideoIdQueryResponse> Handle(GetSongCurrentVideoIdQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
