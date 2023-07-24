using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Queries.Song.GetAllSongs
{
    public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQueryRequest, GetAllSongsQueryResponse>
    {
        readonly ISongService _songService;

        public GetAllSongsQueryHandler(ISongService songService)
        {
            _songService = songService;
        }

        public async Task<GetAllSongsQueryResponse> Handle(GetAllSongsQueryRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) =  _songService.GetAllSongs(request.Page, request.Size);


            return new()
            {
                Songs = datas,
                TotalSongCount = count

            };
        }
    }
}
