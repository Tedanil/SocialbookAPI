using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.Song.CreateSongVoteVideoId
{
    public class CreateSongVoteVideoIdCommandHandler : IRequestHandler<CreateSongVoteVideoIdCommandRequest, CreateSongVoteVideoIdCommandResponse>
    {
        public Task<CreateSongVoteVideoIdCommandResponse> Handle(CreateSongVoteVideoIdCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
