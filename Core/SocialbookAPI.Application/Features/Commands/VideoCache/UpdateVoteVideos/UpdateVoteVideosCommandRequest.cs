using MediatR;

namespace SocialbookAPI.Application.Features.Commands.VideoCache.UpdateVoteVideos
{
    public class UpdateVoteVideosCommandRequest : IRequest<UpdateVoteVideosCommandResponse>
    {
        public List<string> VideoIds { get; set; }
    }
}