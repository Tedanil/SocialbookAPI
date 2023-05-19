using MediatR;

namespace SocialbookAPI.Application.Features.Commands.VideoCache.UpdateCurrentVideoId
{
    public class UpdateCurrentVideoIdCommandRequest : IRequest<UpdateCurrentVideoIdCommandResponse>
    {
        public string VideoId { get; set; }
        public string VideoTime { get; set; }
    }
}