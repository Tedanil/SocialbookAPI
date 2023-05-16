using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.Song.UpdateSongVoteVideoId
{
    public class UpdateSongVoteVideoIdCommandHandler : IRequestHandler<UpdateSongVoteVideoIdCommandRequest, UpdateSongVoteVideoIdCommandResponse>
    {
        public Task<UpdateSongVoteVideoIdCommandResponse> Handle(UpdateSongVoteVideoIdCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
