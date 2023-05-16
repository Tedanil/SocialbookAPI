using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.Song.UpdateSongCurrentVideoId
{
    public class UpdateSongCurrentVideoIdCommandHandler : IRequestHandler<UpdateSongCurrentVideoIdCommandRequest, UpdateSongCurrentVideoIdCommandResponse>
    {
        public Task<UpdateSongCurrentVideoIdCommandResponse> Handle(UpdateSongCurrentVideoIdCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
