using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Queries.Song.GetSongVideoId
{
    public class GetSongVideoIdQueryHandler : IRequestHandler<GetSongVideoIdQueryRequest, GetSongVideoIdQueryResponse>
    {
        public Task<GetSongVideoIdQueryResponse> Handle(GetSongVideoIdQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
