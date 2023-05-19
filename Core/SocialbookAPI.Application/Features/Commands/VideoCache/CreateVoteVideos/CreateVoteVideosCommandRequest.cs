using MediatR;

namespace SocialbookAPI.Application.Features.Commands.VideoCache.CreateVoteVideos
{
    public class CreateVoteVideosCommandRequest : IRequest<CreateVoteVideosCommandResponse>
    {
        public List<string> VideoIds { get; set; }

    }
}