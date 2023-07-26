using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.Song.RemoveSong
{
    public class RemoveSongCommandHandler : IRequestHandler<RemoveSongCommandRequest, RemoveSongCommandResponse>
    {
        readonly ISongService _songService;

        public RemoveSongCommandHandler(ISongService songService)
        {
            _songService = songService;
        }

        public async Task<RemoveSongCommandResponse> Handle(RemoveSongCommandRequest request, CancellationToken cancellationToken)
        {
            await _songService.RemoveSongAsync(request.Id);
            return new();
        }
    }
}
