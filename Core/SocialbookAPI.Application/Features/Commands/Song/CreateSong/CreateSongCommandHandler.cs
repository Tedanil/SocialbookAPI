using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.Song.CreateSong
{
    public class CreateSongCommandHandler : IRequestHandler<CreateSongCommandRequest, CreateSongCommandResponse>
    {
        readonly ISongService _songService;

        public CreateSongCommandHandler(ISongService songService)
        {
            _songService = songService;
        }

        public async Task<CreateSongCommandResponse> Handle(CreateSongCommandRequest request, CancellationToken cancellationToken)
        {
            await _songService.CreateSongAsync(new()
            {
                VideoId = request.VideoId,
                Genre = request.Genre,
            });

            return new();
        }
    }
}
