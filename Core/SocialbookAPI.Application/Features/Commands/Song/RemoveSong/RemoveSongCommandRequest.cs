using MediatR;

namespace SocialbookAPI.Application.Features.Commands.Song.RemoveSong
{
    public class RemoveSongCommandRequest : IRequest<RemoveSongCommandResponse>
    {
        public string Id { get; set; }
    }
}